// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// this program; if not, write to the Free Software Foundation, Inc.,
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using NClass.DiagramEditor.ClassDiagram;
using NClass.DiagramEditor.ClassDiagram.Shapes;

namespace NClass.DiagramEditor
{
    public partial class Canvas : UserControl, IDocumentVisualizer
    {
        public const float MinZoom = 0.1F;
        public const float MaxZoom = 4.0F;
        private const int DiagramPadding = 10;

        private static readonly Pen borderPen;

        private IDocument document;
        private readonly List<PopupWindow> windows = new List<PopupWindow>();

        static Canvas()
        {
            borderPen = new Pen(Color.FromArgb(128, Color.Black));
            borderPen.DashPattern = new float[] {5, 5};
        }

        public Canvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            if (DiagramElement.Graphics == null)
            {
                DiagramElement.Graphics = CreateGraphics();
            }
        }

        public override Color BackColor
        {
            get
            {
                if (HasDocument)
                    return Document.BackColor;
                return base.BackColor;
            }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public int ZoomPercentage
        {
            get
            {
                if (HasDocument)
                    return (int) Math.Round(Document.Zoom*100);
                return 100;
            }
        }

        public event EventHandler DocumentRedrawed;
        public event EventHandler VisibleAreaChanged;

        [Browsable(false)]
        public bool HasDocument { get { return document != null; } }

        [Browsable(false)]
        public IDocument Document
        {
            get { return document; }
            set
            {
                if (document != value)
                {
                    if (document != null)
                    {
                        document.CloseWindows();
                        document.OffsetChanged -= document_OffsetChanged;
                        document.SizeChanged -= document_SizeChanged;
                        document.ZoomChanged -= document_ZoomChanged;
                        document.NeedsRedraw -= document_NeedsRedraw;
                        document.ShowingWindow -= document_ShowingWindow;
                        document.HidingWindow -= document_HidingWindow;
                    }
                    document = value;

                    if (document != null)
                    {
                        document.OffsetChanged += document_OffsetChanged;
                        document.SizeChanged += document_SizeChanged;
                        document.ZoomChanged += document_ZoomChanged;
                        document.NeedsRedraw += document_NeedsRedraw;
                        document.ShowingWindow += document_ShowingWindow;
                        document.HidingWindow += document_HidingWindow;
                    }

                    SetScrolls();
                    Invalidate();

                    OnDocumentRedrawed(EventArgs.Empty);
                    OnZoomChanged(EventArgs.Empty);
                    OnVisibleAreaChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public Point Offset
        {
            get { return new Point(HorizontalScroll.Value, VerticalScroll.Value); }
            set
            {
                AutoScrollPosition = value;
                UpdateDocumentOffset();
            }
        }

        Size IDocumentVisualizer.DocumentSize
        {
            get
            {
                if (HasDocument)
                    return Document.Size;
                return Size.Empty;
            }
        }

        [Browsable(false)]
        public Rectangle VisibleArea
        {
            get
            {
                if (HasDocument)
                {
                    return new Rectangle(
                        (int) (Document.Offset.X/Document.Zoom),
                        (int) (Document.Offset.Y/Document.Zoom),
                        (int) (ClientSize.Width/Document.Zoom),
                        (int) (ClientSize.Height/Document.Zoom)
                        );
                }
                return Rectangle.Empty;
            }
        }

        [Browsable(false)]
        public float Zoom
        {
            get
            {
                if (HasDocument)
                    return Document.Zoom;
                return 1.0F;
            }
            set { ChangeZoom(value); }
        }

        public void ChangeZoom(bool enlarge)
        {
            if (HasDocument)
            {
                if (Document.HasSelectedElement)
                    ChangeZoom(enlarge, Document.GetPrintingArea(true));
                else
                    ChangeZoom(enlarge, GetAbsoluteCenterPoint());
            }
        }

        public void ChangeZoom(float zoom)
        {
            if (HasDocument)
            {
                if (Document.HasSelectedElement)
                    ChangeZoom(zoom, Document.GetPrintingArea(true));
                else
                    ChangeZoom(zoom, GetAbsoluteCenterPoint());
            }
        }

        public void AutoZoom()
        {
            AutoZoom(true);
        }

        public void AutoZoom(bool selectedOnly)
        {
            if (HasDocument && !Document.IsEmpty)
            {
                const int Margin = Shape.SelectionMargin;
                selectedOnly &= Document.HasSelectedElement;

                var visibleRectangle = ClientRectangle;
                var diagramRectangle = document.GetPrintingArea(selectedOnly);
                visibleRectangle.Inflate(-Margin, -Margin);

                var scaleX = visibleRectangle.Width/diagramRectangle.Width;
                var scaleY = visibleRectangle.Height/diagramRectangle.Height;
                var scale = Math.Min(scaleX, scaleY);

                Document.Zoom = scale;

                var offsetX = (visibleRectangle.Width - diagramRectangle.Width*Zoom)/2;
                var offsetY = (visibleRectangle.Height - diagramRectangle.Height*Zoom)/2;
                Offset = new Point(
                    (int) (diagramRectangle.X*Zoom - Margin - offsetX),
                    (int) (diagramRectangle.Y*Zoom - Margin - offsetY)
                    );
            }
        }

        void IDocumentVisualizer.DrawDocument(Graphics g)
        {
            if (HasDocument)
            {
                IGraphics graphics = new GdiGraphics(g);
                Document.Print(graphics, false, Style.CurrentStyle);
            }
        }

        public event EventHandler ZoomChanged;
        public event EventHandler MouseHWheel;

        private void document_OffsetChanged(object sender, EventArgs e)
        {
            SetScrolls();
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_SizeChanged(object sender, EventArgs e)
        {
            SetScrolls();
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_ZoomChanged(object sender, EventArgs e)
        {
            Invalidate();
            SetScrolls();
            OnZoomChanged(EventArgs.Empty);
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_NeedsRedraw(object sender, EventArgs e)
        {
            Invalidate();
            OnDocumentRedrawed(EventArgs.Empty);
        }

        private void document_ShowingWindow(object sender, PopupWindowEventArgs e)
        {
            var window = e.Window;
            if (!windows.Contains(window))
            {
                windows.Add(window);
                if (ParentForm != null)
                {
                    ParentForm.Controls.Add(window);
                    var point = PointToScreen(Point.Empty);
                    var absPos = ParentForm.PointToClient(point);
                    window.ParentLocation = absPos;
                    window.BringToFront();
                }
            }
        }

        private void document_HidingWindow(object sender, PopupWindowEventArgs e)
        {
            var window = e.Window;
            if (windows.Contains(window))
            {
                windows.Remove(window);
                if (ParentForm != null)
                {
                    ParentForm.Controls.Remove(window);
                }
            }
        }

        private PointF GetAbsoluteCenterPoint()
        {
            return ConvertRelativeToAbsolute(new Point(Width/2, Height/2));
        }

        private PointF ConvertRelativeToAbsolute(Point location)
        {
            return new PointF(
                (location.X + Offset.X)/Zoom,
                (location.Y + Offset.Y)/Zoom
                );
        }

        private Point ConvertAbsoluteToRelative(PointF location)
        {
            return new Point(
                (int) (location.X*Zoom - Offset.X),
                (int) (location.Y*Zoom - Offset.Y)
                );
        }

        public void ZoomIn()
        {
            ChangeZoom(true);
        }

        public void ZoomOut()
        {
            ChangeZoom(false);
        }

        public void ChangeZoom(bool enlarge, PointF zoomingCenter)
        {
            if (HasDocument)
            {
                var zoomValue = CalculateZoomValue(enlarge);
                ChangeZoom(zoomValue, zoomingCenter);
            }
        }

        private void ChangeZoom(bool enlarge, RectangleF zoomingCenter)
        {
            if (HasDocument)
            {
                var zoomValue = CalculateZoomValue(enlarge);
                ChangeZoom(zoomValue, zoomingCenter);
            }
        }

        private float CalculateZoomValue(bool enlarge)
        {
            if (enlarge)
                return ((int) Math.Round(Document.Zoom*100) + 10)/10/10F;
            return ((int) Math.Round(Document.Zoom*100) - 1)/10/10F;
        }

        public void ChangeZoom(float zoomValue, PointF zoomingCenter)
        {
            if (HasDocument)
            {
                var oldLocation = ConvertAbsoluteToRelative(zoomingCenter);
                Document.Zoom = zoomValue;
                var newLocation = ConvertAbsoluteToRelative(zoomingCenter);

                Offset += new Size(
                    newLocation.X - oldLocation.X,
                    newLocation.Y - oldLocation.Y
                    );
            }
        }

        private void ChangeZoom(float zoomValue, RectangleF zoomingCenter)
        {
            var centerPoint = new PointF(
                zoomingCenter.Left + zoomingCenter.Width/2,
                zoomingCenter.Top + zoomingCenter.Height/2);

            Document.Zoom = zoomValue;
            var newLocation = ConvertAbsoluteToRelative(centerPoint);
            var desiredLocation = new Point(Width/2, Height/2);

            Offset += new Size(
                newLocation.X - desiredLocation.X,
                newLocation.Y - desiredLocation.Y
                );
        }

        private void SetScrolls()
        {
            if (HasDocument)
            {
                AutoScrollMinSize = new Size(
                    (int) Math.Ceiling(Document.Size.Width*Document.Zoom),
                    (int) Math.Ceiling(Document.Size.Height*Document.Zoom)
                    );
                AutoScrollPosition = Document.Offset;
            }
            else
            {
                AutoScrollMinSize = Size.Empty;
                AutoScrollPosition = Point.Empty;
            }
        }

        protected override Point ScrollToControl(Control activeControl)
        {
            if (activeControl.Parent != null && activeControl.Parent != this)
            {
                return ScrollToControl(activeControl.Parent);
            }
            var point = base.ScrollToControl(activeControl);
            if (HasDocument)
                Document.Offset = new Point(-point.X, -point.Y);
            return point;
        }

        private void DrawContent(Graphics g)
        {
            if (HasDocument)
            {
                // Set the drawing quality
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (Document.Zoom == 1.0F)
                {
                    if (Settings.Default.UseClearType == ClearTypeMode.Always)
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    else
                        g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                }
                else
                {
                    if (Settings.Default.UseClearType == ClearTypeMode.WhenZoomed ||
                        Settings.Default.UseClearType == ClearTypeMode.Always)
                    {
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }
                    else
                    {
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    }
                }

                // Transform the screen to absolute coordinate system
                g.TranslateTransform(-Document.Offset.X, -Document.Offset.Y);
                g.ScaleTransform(Document.Zoom, Document.Zoom);

                // Draw contents
                Document.Display(g);
            }
        }

        private void ScrollHorizontally(int offset)
        {
            if (HScroll)
            {
                var posX = -DisplayRectangle.X + offset;
                var maxX = DisplayRectangle.Width - ClientRectangle.Width;

                if (posX < 0)
                    posX = 0;
                if (posX > maxX)
                    posX = maxX;

                SetDisplayRectLocation(-posX, DisplayRectangle.Y);
                AdjustFormScrollbars(true);
            }
        }

        private void UpdateDocumentOffset()
        {
            if (HasDocument)
            {
                Document.Offset = Offset;
                if (MonoHelper.IsRunningOnMono)
                    Invalidate();
            }
        }

        private void UpdateWindowPositions()
        {
            if (ParentForm != null)
            {
                var point = PointToScreen(Point.Empty);
                var absPos = ParentForm.PointToClient(point);

                foreach (var window in windows)
                {
                    window.ParentLocation = absPos;
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var key = keyData & ~Keys.Modifiers;

            if (key == Keys.Up || key == Keys.Down)
                return false;
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            UpdateDocumentOffset();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                var enlarge = e.Delta > 0;

                if (ClientRectangle.Contains(e.Location))
                    ChangeZoom(enlarge, ConvertRelativeToAbsolute(e.Location));
                else
                    ChangeZoom(enlarge);
            }
            else if (ModifierKeys == Keys.Shift)
            {
                ScrollHorizontally(-e.Delta);
            }
            else
            {
                base.OnMouseWheel(e);
            }
            UpdateDocumentOffset();
        }

        protected virtual void OnMouseHWheel(EventArgs e) //TODO: MouseEventArgs kellene
        {
            UpdateDocumentOffset();
            Invalidate(); //TODO: SetDisplayRectLocation() kellene
            if (MouseHWheel != null)
                MouseHWheel(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (HasDocument)
            {
                var abs_e = new AbsoluteMouseEventArgs(e, Document);
                Document.MouseDown(abs_e);
                if (e.Button == MouseButtons.Right)
                    ContextMenuStrip = Document.GetContextMenu(abs_e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (HasDocument)
            {
                Document.MouseMove(new AbsoluteMouseEventArgs(e, Document));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (HasDocument)
            {
                Document.MouseUp(new AbsoluteMouseEventArgs(e, Document));
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (HasDocument)
            {
                Document.DoubleClick(new AbsoluteMouseEventArgs(e, Document));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (document != null)
            {
                if (e.Modifiers == Keys.Control)
                {
                    if (e.KeyCode == Keys.Add)
                    {
                        ZoomIn();
                    }
                    else if (e.KeyCode == Keys.Subtract)
                    {
                        ZoomOut();
                    }
                    else if (e.KeyCode == Keys.Multiply || e.KeyCode == Keys.NumPad0)
                    {
                        Zoom = 1.0F;
                    }
                }

                document.KeyDown(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (HasDocument)
            {
                DrawContent(e.Graphics);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x020E) // WM_MOUSEHWHEEL
            {
                OnMouseHWheel(EventArgs.Empty);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            UpdateWindowPositions();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateDocumentOffset();
            UpdateWindowPositions();
        }

        private void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
        }

        private void OnDocumentRedrawed(EventArgs e)
        {
            if (DocumentRedrawed != null)
                DocumentRedrawed(this, e);
        }

        private void OnVisibleAreaChanged(EventArgs e)
        {
            if (VisibleAreaChanged != null)
                VisibleAreaChanged(this, e);
        }
    }
}