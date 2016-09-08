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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using NClass.Core;
using NClass.DiagramEditor.ClassDiagram.Connections;
using NClass.DiagramEditor.ClassDiagram.ContextMenus;
using NClass.DiagramEditor.ClassDiagram.Dialogs;
using NClass.DiagramEditor.ClassDiagram.Shapes;
using NClass.Translations;

namespace NClass.DiagramEditor.ClassDiagram
{
    public class Diagram : Model, IDocument, IEditable, IPrintable
    {
        private const int DiagramPadding = 10;
        private const int PrecisionSize = 10;
        private const int MaximalPrecisionDistance = 500;
        private const float DashSize = 3;
        private static readonly Size MinSize = new Size(3000, 2000);
        public static readonly Pen SelectionPen;
        private DiagramElement activeElement;
        private ConnectionCreator connectionCreator;
        private PointF mouseLocation = PointF.Empty;
        private EntityType newShapeType = EntityType.Class;
        private Point offset = Point.Empty;
        private bool redrawSuspended;
        private RectangleF selectionFrame = RectangleF.Empty;
        private bool selectioning;
        private Rectangle shapeOutline = Rectangle.Empty;

        private EntityType shapeType;
        private Size size = MinSize;

        private State state = State.Normal;
        private float zoom = 1.0F;

        static Diagram()
        {
            SelectionPen = new Pen(Color.Black);
            SelectionPen.DashPattern = new[] {DashSize, DashSize};
        }

        protected Diagram()
        {
        }

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="language" /> is null.
        /// </exception>
        public Diagram(Language language)
            : base(language)
        {
        }

        /// <exception cref="ArgumentException">
        ///     <paramref name="name" /> cannot be empty string.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="language" /> is null.
        /// </exception>
        public Diagram(string name, Language language)
            : base(name, language)
        {
        }

        public IEnumerable<Shape> Shapes { get { return ShapeList; } }

        protected internal ElementList<Shape> ShapeList { get; } = new ElementList<Shape>();

        public IEnumerable<Connection> Connections { get { return ConnectionList; } }

        protected internal ElementList<Connection> ConnectionList { get; } = new ElementList<Connection>();

        public bool RedrawSuspended
        {
            get { return redrawSuspended; }
            set
            {
                if (redrawSuspended != value)
                {
                    redrawSuspended = value;
                    if (!redrawSuspended)
                    {
                        RecalculateSize();
                        RequestRedrawIfNeeded();
                    }
                }
            }
        }

        public int ShapeCount { get { return ShapeList.Count; } }

        public int ConnectionCount { get { return ConnectionList.Count; } }

        public DiagramElement ActiveElement
        {
            get { return activeElement; }
            private set
            {
                if (activeElement != null)
                {
                    activeElement.IsActive = false;
                }
                activeElement = value;
            }
        }

        public DiagramElement TopSelectedElement
        {
            get
            {
                if (SelectedConnectionCount > 0)
                    return ConnectionList.FirstValue;
                if (SelectedShapeCount > 0)
                    return ShapeList.FirstValue;
                return null;
            }
        }

        public int SelectedElementCount { get { return SelectedShapeCount + SelectedConnectionCount; } }

        public int SelectedShapeCount { get; private set; }

        public int SelectedConnectionCount { get; private set; }

        public event EventHandler OffsetChanged;
        public event EventHandler SizeChanged;
        public event EventHandler ZoomChanged;
        public event EventHandler StatusChanged;
        public event EventHandler NeedsRedraw;
        public event EventHandler ClipboardAvailabilityChanged;
        public event PopupWindowEventHandler ShowingWindow;
        public event PopupWindowEventHandler HidingWindow;

        public Point Offset
        {
            get { return offset; }
            set
            {
                if (value.X < 0)
                    value.X = 0;
                if (value.Y < 0)
                    value.Y = 0;

                if (offset != value)
                {
                    offset = value;
                    OnOffsetChanged(EventArgs.Empty);
                }
            }
        }

        public Size Size
        {
            get { return size; }
            protected set
            {
                if (value.Width < MinSize.Width)
                    value.Width = MinSize.Width;
                if (value.Height < MinSize.Height)
                    value.Height = MinSize.Height;

                if (size != value)
                {
                    size = value;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                if (value < Canvas.MinZoom)
                    value = Canvas.MinZoom;
                if (value > Canvas.MaxZoom)
                    value = Canvas.MaxZoom;

                if (zoom != value)
                {
                    zoom = value;
                    OnZoomChanged(EventArgs.Empty);
                }
            }
        }

        public Color BackColor { get { return Style.CurrentStyle.BackgroundColor; } }

        public bool CanCutToClipboard { get { return SelectedShapeCount > 0; } }

        public bool CanCopyToClipboard { get { return SelectedShapeCount > 0; } }

        public bool CanPasteFromClipboard { get { return Clipboard.Item is ElementContainer; } }

        public bool HasSelectedElement { get { return SelectedElementCount > 0; } }

        public string GetSelectedElementName()
        {
            if (HasSelectedElement && SelectedElementCount == 1)
            {
                foreach (var shape in ShapeList)
                {
                    if (shape.IsSelected)
                        return shape.Entity.Name;
                }
            }

            return null;
        }

        public void CloseWindows()
        {
            if (ActiveElement != null)
                ActiveElement.HideEditor();
        }

        public void Cut()
        {
            if (CanCutToClipboard)
            {
                Copy();
                DeleteSelectedElements(false);
            }
        }

        public void Copy()
        {
            if (CanCopyToClipboard)
            {
                var elements = new ElementContainer();
                foreach (var shape in GetSelectedShapes())
                {
                    elements.AddShape(shape);
                }
                foreach (var connection in GetSelectedConnections())
                {
                    elements.AddConnection(connection);
                }
                Clipboard.Item = elements;
            }
        }

        public void Paste()
        {
            if (CanPasteFromClipboard)
            {
                DeselectAll();
                RedrawSuspended = true;
                Clipboard.Paste(this);
                RedrawSuspended = false;
                OnClipboardAvailabilityChanged(EventArgs.Empty);
            }
        }

        public void Display(Graphics g)
        {
            var clip = g.ClipBounds;

            // Draw diagram elements
            IGraphics graphics = new GdiGraphics(g);
            foreach (var element in GetElementsInReversedDisplayOrder())
            {
                if (clip.IntersectsWith(element.GetVisibleArea(Zoom)))
                    element.Draw(graphics, true);
                element.NeedsRedraw = false;
            }
            if (state == State.CreatingShape)
            {
                g.DrawRectangle(SelectionPen,
                                shapeOutline.X,
                                shapeOutline.Y,
                                shapeOutline.Width,
                                shapeOutline.Height);
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.Draw(g);
            }

            // Draw selection lines
            var savedState = g.Save();
            g.ResetTransform();
            g.SmoothingMode = SmoothingMode.None;
            foreach (var shape in ShapeList.GetSelectedElementsReversed())
            {
                if (clip.IntersectsWith(shape.GetVisibleArea(Zoom)))
                    shape.DrawSelectionLines(g, Zoom, Offset);
            }
            foreach (var connection in ConnectionList.GetSelectedElementsReversed())
            {
                if (clip.IntersectsWith(connection.GetVisibleArea(Zoom)))
                    connection.DrawSelectionLines(g, Zoom, Offset);
            }

            if (state == State.Multiselecting)
            {
                var frame = RectangleF.FromLTRB(
                    Math.Min(selectionFrame.Left, selectionFrame.Right),
                    Math.Min(selectionFrame.Top, selectionFrame.Bottom),
                    Math.Max(selectionFrame.Left, selectionFrame.Right),
                    Math.Max(selectionFrame.Top, selectionFrame.Bottom));
                g.DrawRectangle(SelectionPen,
                                frame.X*Zoom - Offset.X,
                                frame.Y*Zoom - Offset.Y,
                                frame.Width*Zoom,
                                frame.Height*Zoom);
            }

            // Draw diagram border
            clip = g.ClipBounds;
            var borderWidth = Size.Width*Zoom;
            var borderHeight = Size.Height*Zoom;
            if (clip.Right > borderWidth || clip.Bottom > borderHeight)
            {
                SelectionPen.DashOffset = Offset.Y - Offset.X;
                g.DrawLines(SelectionPen,
                            new[]
                            {
                                new PointF(borderWidth, 0),
                                new PointF(borderWidth, borderHeight),
                                new PointF(0, borderHeight)
                            });
                SelectionPen.DashOffset = 0;
            }

            // Restore original state
            g.Restore(savedState);
        }

        public void ShowPrintDialog()
        {
            var dialog = new DiagramPrintDialog();
            dialog.Document = this;
            dialog.ShowDialog();
        }

        public void Print(IGraphics g, bool selectedOnly, Style style)
        {
            foreach (var shape in ShapeList.GetReversedList())
            {
                if (!selectedOnly || shape.IsSelected)
                    shape.Draw(g, false, style);
            }
            foreach (var connection in ConnectionList.GetReversedList())
            {
                if (!selectedOnly || connection.IsSelected)
                    connection.Draw(g, false, style);
            }
        }

        public void SelectAll()
        {
            RedrawSuspended = true;
            selectioning = true;

            foreach (var shape in ShapeList)
            {
                shape.IsSelected = true;
            }
            foreach (var connection in ConnectionList)
            {
                connection.IsSelected = true;
            }

            SelectedShapeCount = ShapeList.Count;
            SelectedConnectionCount = ConnectionList.Count;

            OnSelectionChanged(EventArgs.Empty);
            OnClipboardAvailabilityChanged(EventArgs.Empty);
            OnSatusChanged(EventArgs.Empty);

            selectioning = false;
            RedrawSuspended = false;
        }

        public void DeleteSelectedElements()
        {
            DeleteSelectedElements(true);
        }

        public void Redraw()
        {
            OnNeedsRedraw(EventArgs.Empty);
        }

        public DynamicMenu GetDynamicMenu()
        {
            DynamicMenu dynamicMenu = DiagramDynamicMenu.Default;
            dynamicMenu.SetReference(this);
            return dynamicMenu;
        }

        public ContextMenuStrip GetContextMenu(AbsoluteMouseEventArgs e)
        {
            if (HasSelectedElement)
            {
                var intersector = new Intersector<ToolStripItem>();
                ContextMenu.MenuStrip.Items.Clear();

                foreach (var shape in GetSelectedShapes())
                    intersector.AddSet(shape.GetContextMenuItems(this));
                foreach (var connection in GetSelectedConnections())
                    intersector.AddSet(connection.GetContextMenuItems(this));

                foreach (var menuItem in intersector.GetIntersection())
                    ContextMenu.MenuStrip.Items.Add(menuItem);
                return ContextMenu.MenuStrip;
            }
            ContextMenu.MenuStrip.Items.Clear();
            foreach (var menuItem in BlankContextMenu.Default.GetMenuItems(this))
                ContextMenu.MenuStrip.Items.Add(menuItem);

            return ContextMenu.MenuStrip;
        }

        public string GetStatus()
        {
            if (SelectedElementCount == 1)
            {
                return TopSelectedElement.ToString();
            }
            if (SelectedElementCount > 1)
            {
                return string.Format(Strings.ItemsSelected, SelectedElementCount);
            }
            return Strings.Ready;
        }

        public string GetShortDescription()
        {
            return Strings.Language + ": " + Language;
        }

        public void MouseDown(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;
            if (state == State.CreatingShape)
            {
                AddCreatedShape();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseDown(e);
                if (connectionCreator.Created)
                    state = State.Normal;
            }
            else
            {
                SelectElements(e);
            }

            if (e.Button == MouseButtons.Right)
            {
                ActiveElement = null;
            }

            RedrawSuspended = false;
        }

        public void MouseMove(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;

            mouseLocation = e.Location;
            if (state == State.Multiselecting)
            {
                selectionFrame = RectangleF.FromLTRB(
                    selectionFrame.Left,
                    selectionFrame.Top,
                    e.X,
                    e.Y);
                Redraw();
            }
            else if (state == State.CreatingShape)
            {
                shapeOutline.Location = new Point((int) e.X, (int) e.Y);
                Redraw();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseMove(e);
            }
            else
            {
                foreach (var element in GetElementsInDisplayOrder())
                {
                    element.MouseMoved(e);
                }
            }

            RedrawSuspended = false;
        }

        public void MouseUp(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;

            if (state == State.Multiselecting)
            {
                TrySelectElements();
                state = State.Normal;
            }
            else
            {
                foreach (var element in GetElementsInDisplayOrder())
                {
                    element.MouseUpped(e);
                }
            }

            RedrawSuspended = false;
        }

        public void DoubleClick(AbsoluteMouseEventArgs e)
        {
            foreach (var element in GetElementsInDisplayOrder())
            {
                element.DoubleClicked(e);
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            //TODO: ActiveElement.KeyDown() - de nem minden esetben (pl. törlésnél nem)
            RedrawSuspended = true;

            // Delete
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedElementCount >= 2 || ActiveElement == null ||
                    !ActiveElement.DeleteSelectedMember())
                {
                    DeleteSelectedElements();
                }
            }
            // Escape
            else if (e.KeyCode == Keys.Escape)
            {
                state = State.Normal;
                DeselectAll();
                Redraw();
            }
            // Enter
            else if (e.KeyCode == Keys.Enter && ActiveElement != null)
            {
                ActiveElement.ShowEditor();
            }
            // Up
            else if (e.KeyCode == Keys.Up && ActiveElement != null)
            {
                if (e.Shift || e.Control)
                    ActiveElement.MoveUp();
                else
                    ActiveElement.SelectPrevious();
            }
            // Down
            else if (e.KeyCode == Keys.Down && ActiveElement != null)
            {
                if (e.Shift || e.Control)
                    ActiveElement.MoveDown();
                else
                    ActiveElement.SelectNext();
            }
            // Ctrl + X
            else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
            {
                Cut();
            }
            // Ctrl + C
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Copy();
            }
            // Ctrl + V
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                Paste();
            }
            // Ctrl + Shift + ?
            else if (e.Modifiers == (Keys.Control | Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        CreateShape();
                        break;

                    case Keys.C:
                        CreateShape(EntityType.Class);
                        break;

                    case Keys.S:
                        CreateShape(EntityType.Structure);
                        break;

                    case Keys.I:
                        CreateShape(EntityType.Interface);
                        break;

                    case Keys.E:
                        CreateShape(EntityType.Enum);
                        break;

                    case Keys.D:
                        CreateShape(EntityType.Delegate);
                        break;

                    case Keys.N:
                        CreateShape(EntityType.Comment);
                        break;
                }
            }
            RedrawSuspended = false;
        }

        public RectangleF GetPrintingArea(bool selectedOnly)
        {
            RectangleF area = Rectangle.Empty;
            var first = true;

            foreach (var shape in ShapeList)
            {
                if (!selectedOnly || shape.IsSelected)
                {
                    if (first)
                    {
                        area = shape.GetPrintingClip(Zoom);
                        first = false;
                    }
                    else
                    {
                        area = RectangleF.Union(area, shape.GetPrintingClip(Zoom));
                    }
                }
            }
            foreach (var connection in ConnectionList)
            {
                if (!selectedOnly || connection.IsSelected)
                {
                    if (first)
                    {
                        area = connection.GetPrintingClip(Zoom);
                        first = false;
                    }
                    else
                    {
                        area = RectangleF.Union(area, connection.GetPrintingClip(Zoom));
                    }
                }
            }

            return area;
        }

        public event EventHandler SelectionChanged;

        public IEnumerable<Shape> GetSelectedShapes()
        {
            return ShapeList.GetSelectedElements();
        }

        public IEnumerable<Connection> GetSelectedConnections()
        {
            return ConnectionList.GetSelectedElements();
        }

        public IEnumerable<DiagramElement> GetSelectedElements()
        {
            foreach (var shape in ShapeList)
            {
                if (shape.IsSelected)
                    yield return shape;
            }
            foreach (var connection in ConnectionList)
            {
                if (connection.IsSelected)
                    yield return connection;
            }
        }

        private IEnumerable<DiagramElement> GetElementsInDisplayOrder()
        {
            foreach (var shape in ShapeList.GetSelectedElements())
                yield return shape;

            foreach (var connection in ConnectionList.GetSelectedElements())
                yield return connection;

            foreach (var connection in ConnectionList.GetUnselectedElements())
                yield return connection;

            foreach (var shape in ShapeList.GetUnselectedElements())
                yield return shape;
        }

        private IEnumerable<DiagramElement> GetElementsInReversedDisplayOrder()
        {
            foreach (var shape in ShapeList.GetUnselectedElementsReversed())
                yield return shape;

            foreach (var connection in ConnectionList.GetUnselectedElementsReversed())
                yield return connection;

            foreach (var connection in ConnectionList.GetSelectedElementsReversed())
                yield return connection;

            foreach (var shape in ShapeList.GetSelectedElementsReversed())
                yield return shape;
        }

        public void CopyAsImage()
        {
            ImageCreator.CopyAsImage(this);
        }

        public void CopyAsImage(bool selectedOnly)
        {
            ImageCreator.CopyAsImage(this, selectedOnly);
        }

        public void SaveAsImage()
        {
            ImageCreator.SaveAsImage(this);
        }

        public void SaveAsImage(bool selectedOnly)
        {
            ImageCreator.SaveAsImage(this, selectedOnly);
        }

        public void Print(IGraphics g)
        {
            Print(g, false, Style.CurrentStyle);
        }

        private void RecalculateSize()
        {
            const int Padding = 500;
            int rightMax = MinSize.Width, bottomMax = MinSize.Height;

            foreach (var shape in ShapeList)
            {
                var area = shape.GetLogicalArea();
                if (area.Right + Padding > rightMax)
                    rightMax = area.Right + Padding;
                if (area.Bottom + Padding > bottomMax)
                    bottomMax = area.Bottom + Padding;
            }
            foreach (var connection in ConnectionList)
            {
                var area = connection.GetLogicalArea();
                if (area.Right + Padding > rightMax)
                    rightMax = area.Right + Padding;
                if (area.Bottom + Padding > bottomMax)
                    bottomMax = area.Bottom + Padding;
            }

            Size = new Size(rightMax, bottomMax);
        }

        public void AlignLeft()
        {
            if (SelectedShapeCount >= 2)
            {
                var left = Size.Width;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    left = Math.Min(left, shape.Left);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Left = left;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignRight()
        {
            if (SelectedShapeCount >= 2)
            {
                var right = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    right = Math.Max(right, shape.Right);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Right = right;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignTop()
        {
            if (SelectedShapeCount >= 2)
            {
                var top = Size.Height;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    top = Math.Min(top, shape.Top);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Top = top;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignBottom()
        {
            if (SelectedShapeCount >= 2)
            {
                var bottom = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    bottom = Math.Max(bottom, shape.Bottom);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Bottom = bottom;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignHorizontal()
        {
            if (SelectedShapeCount >= 2)
            {
                var center = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    center += (shape.Top + shape.Bottom)/2;
                }
                center /= SelectedShapeCount;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Top = center - shape.Height/2;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignVertical()
        {
            if (SelectedShapeCount >= 2)
            {
                var center = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    center += (shape.Left + shape.Right)/2;
                }
                center /= SelectedShapeCount;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Left = center - shape.Width/2;
                }

                RedrawSuspended = false;
            }
        }

        public void AdjustToSameWidth()
        {
            if (SelectedShapeCount >= 2)
            {
                var maxWidth = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    maxWidth = Math.Max(maxWidth, shape.Width);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Width = maxWidth;
                }
                RedrawSuspended = false;
            }
        }

        public void AdjustToSameHeight()
        {
            if (SelectedShapeCount >= 2)
            {
                var maxHeight = 0;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    maxHeight = Math.Max(maxHeight, shape.Height);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Height = maxHeight;
                }

                RedrawSuspended = false;
            }
        }

        public void AdjustToSameSize()
        {
            if (SelectedShapeCount >= 2)
            {
                var maxSize = Size.Empty;
                RedrawSuspended = true;

                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    maxSize.Width = Math.Max(maxSize.Width, shape.Width);
                    maxSize.Height = Math.Max(maxSize.Height, shape.Height);
                }
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Size = maxSize;
                }

                RedrawSuspended = false;
            }
        }

        public void AutoSizeOfSelectedShapes()
        {
            RedrawSuspended = true;
            foreach (var shape in ShapeList.GetSelectedElements())
            {
                shape.AutoWidth();
                shape.AutoHeight();
            }
            RedrawSuspended = false;
        }

        public void AutoWidthOfSelectedShapes()
        {
            RedrawSuspended = true;
            foreach (var shape in ShapeList.GetSelectedElements())
            {
                shape.AutoWidth();
            }
            RedrawSuspended = false;
        }

        public void AutoHeightOfSelectedShapes()
        {
            RedrawSuspended = true;
            foreach (var shape in ShapeList.GetSelectedElements())
            {
                shape.AutoHeight();
            }
            RedrawSuspended = false;
        }

        public void CollapseAll()
        {
            var selectedOnly = HasSelectedElement;
            CollapseAll(selectedOnly);
        }

        public void CollapseAll(bool selectedOnly)
        {
            RedrawSuspended = true;

            foreach (var shape in ShapeList)
            {
                if (shape.IsSelected || !selectedOnly)
                    shape.Collapse();
            }

            RedrawSuspended = false;
        }

        public void ExpandAll()
        {
            var selectedOnly = HasSelectedElement;
            ExpandAll(selectedOnly);
        }

        public void ExpandAll(bool selectedOnly)
        {
            RedrawSuspended = true;

            foreach (var shape in ShapeList)
            {
                if (shape.IsSelected || !selectedOnly)
                    shape.Expand();
            }

            RedrawSuspended = false;
        }

        private bool ConfirmDelete()
        {
            var result = MessageBox.Show(
                Strings.DeleteElementsConfirmation,
                Strings.Confirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        private void DeleteSelectedElements(bool showConfirmation)
        {
            if (HasSelectedElement && (!showConfirmation || ConfirmDelete()))
            {
                if (SelectedShapeCount > 0)
                {
                    foreach (var shape in ShapeList.GetModifiableList())
                    {
                        if (shape.IsSelected)
                            RemoveEntity(shape.Entity);
                    }
                }
                if (SelectedConnectionCount > 0)
                {
                    foreach (var connection in ConnectionList.GetModifiableList())
                    {
                        if (connection.IsSelected)
                            RemoveRelationship(connection.Relationship);
                    }
                }
                Redraw();
            }
        }

        private void RequestRedrawIfNeeded()
        {
            if (Loading)
                return;

            foreach (var shape in ShapeList)
            {
                if (shape.NeedsRedraw)
                {
                    OnNeedsRedraw(EventArgs.Empty);
                    return;
                }
            }
            foreach (var connection in ConnectionList)
            {
                if (connection.NeedsRedraw)
                {
                    OnNeedsRedraw(EventArgs.Empty);
                    return;
                }
            }
        }

        public void DeselectAll()
        {
            foreach (var shape in ShapeList)
            {
                shape.IsSelected = false;
                shape.IsActive = false;
            }
            foreach (var connection in ConnectionList)
            {
                connection.IsSelected = false;
                connection.IsActive = false;
            }
            ActiveElement = null;
        }

        private void DeselectAllOthers(DiagramElement onlySelected)
        {
            foreach (var shape in ShapeList)
            {
                if (shape != onlySelected)
                {
                    shape.IsSelected = false;
                    shape.IsActive = false;
                }
            }
            foreach (var connection in ConnectionList)
            {
                if (connection != onlySelected)
                {
                    connection.IsSelected = false;
                    connection.IsActive = false;
                }
            }
        }

        private void AddCreatedShape()
        {
            DeselectAll();
            var shape = AddShape(shapeType);
            shape.Location = shapeOutline.Location;
            RecalculateSize();
            state = State.Normal;

            shape.IsSelected = true;
            shape.IsActive = true;
            if (shape is TypeShape) //TODO: nem szép
                shape.ShowEditor();
        }

        private void SelectElements(AbsoluteMouseEventArgs e)
        {
            DiagramElement firstElement = null;
            var multiSelection = Control.ModifierKeys == Keys.Control;

            foreach (var element in GetElementsInDisplayOrder())
            {
                var isSelected = element.IsSelected;
                element.MousePressed(e);
                if (e.Handled && firstElement == null)
                {
                    firstElement = element;
                    if (isSelected)
                        multiSelection = true;
                }
            }

            if (firstElement != null && !multiSelection)
            {
                DeselectAllOthers(firstElement);
            }

            if (!e.Handled)
            {
                if (!multiSelection)
                    DeselectAll();

                if (e.Button == MouseButtons.Left)
                {
                    state = State.Multiselecting;
                    selectionFrame.Location = e.Location;
                    selectionFrame.Size = Size.Empty;
                }
            }
        }

        private void TrySelectElements()
        {
            selectionFrame = RectangleF.FromLTRB(
                Math.Min(selectionFrame.Left, selectionFrame.Right),
                Math.Min(selectionFrame.Top, selectionFrame.Bottom),
                Math.Max(selectionFrame.Left, selectionFrame.Right),
                Math.Max(selectionFrame.Top, selectionFrame.Bottom));
            selectioning = true;

            foreach (var shape in ShapeList)
            {
                if (shape.TrySelect(selectionFrame))
                    SelectedShapeCount++;
            }
            foreach (var connection in ConnectionList)
            {
                if (connection.TrySelect(selectionFrame))
                    SelectedConnectionCount++;
            }

            OnSelectionChanged(EventArgs.Empty);
            OnClipboardAvailabilityChanged(EventArgs.Empty);
            OnSatusChanged(EventArgs.Empty);
            Redraw();

            selectioning = false;
        }

        private void UpdateWindowPosition()
        {
            if (ActiveElement != null)
                ActiveElement.MoveWindow();
        }

        internal void ShowWindow(PopupWindow window)
        {
            Redraw();
            OnShowingWindow(new PopupWindowEventArgs(window));
        }

        internal void HideWindow(PopupWindow window)
        {
            window.Closing();
            OnHidingWindow(new PopupWindowEventArgs(window));
        }

        private void AddShape(Shape shape)
        {
            shape.Diagram = this;
            shape.Modified += element_Modified;
            shape.Activating += element_Activating;
            shape.Dragging += shape_Dragging;
            shape.Resizing += shape_Resizing;
            shape.SelectionChanged += shape_SelectionChanged;
            ShapeList.AddFirst(shape);
            RecalculateSize();
        }

        private void element_Modified(object sender, EventArgs e)
        {
            if (!RedrawSuspended)
                RequestRedrawIfNeeded();
            OnModified(EventArgs.Empty);
        }

        private void element_Activating(object sender, EventArgs e)
        {
            foreach (var shape in ShapeList)
            {
                if (shape != sender)
                    shape.IsActive = false;
            }
            foreach (var connection in ConnectionList)
            {
                if (connection != sender)
                    connection.IsActive = false;
            }
            ActiveElement = (DiagramElement) sender;
        }

        private void shape_Dragging(object sender, MoveEventArgs e)
        {
            var offset = e.Offset;

            // Align to other shapes
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                var shape = (Shape) sender;

                foreach (var otherShape in ShapeList.GetUnselectedElements())
                {
                    var xDist = otherShape.X - (shape.X + offset.Width);
                    var yDist = otherShape.Y - (shape.Y + offset.Height);

                    if (Math.Abs(xDist) <= PrecisionSize)
                    {
                        var distance1 = Math.Abs(shape.Top - otherShape.Bottom);
                        var distance2 = Math.Abs(otherShape.Top - shape.Bottom);
                        var distance = Math.Min(distance1, distance2);

                        if (distance <= MaximalPrecisionDistance)
                            offset.Width += xDist;
                    }
                    if (Math.Abs(yDist) <= PrecisionSize)
                    {
                        var distance1 = Math.Abs(shape.Left - otherShape.Right);
                        var distance2 = Math.Abs(otherShape.Left - shape.Right);
                        var distance = Math.Min(distance1, distance2);

                        if (distance <= MaximalPrecisionDistance)
                            offset.Height += yDist;
                    }
                }
            }

            // Get maxmimal avaiable offset for the selected elements
            foreach (var shape in ShapeList)
            {
                offset = shape.GetMaximalOffset(offset, DiagramPadding);
            }
            foreach (var connection in ConnectionList)
            {
                offset = connection.GetMaximalOffset(offset, DiagramPadding);
            }
            if (!offset.IsEmpty)
            {
                foreach (var shape in ShapeList.GetSelectedElements())
                {
                    shape.Offset(offset);
                }
                foreach (var connection in ConnectionList.GetSelectedElements())
                {
                    connection.Offset(offset);
                }
            }
            RecalculateSize();
        }

        private void shape_Resizing(object sender, ResizeEventArgs e)
        {
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                var shape = (Shape) sender;
                var change = e.Change;

                // Horizontal resizing
                if (change.Width != 0)
                {
                    foreach (var otherShape in ShapeList.GetUnselectedElements())
                    {
                        if (otherShape != shape)
                        {
                            var xDist = otherShape.Right - (shape.Right + change.Width);
                            if (Math.Abs(xDist) <= PrecisionSize)
                            {
                                var distance1 = Math.Abs(shape.Top - otherShape.Bottom);
                                var distance2 = Math.Abs(otherShape.Top - shape.Bottom);
                                var distance = Math.Min(distance1, distance2);

                                if (distance <= MaximalPrecisionDistance)
                                {
                                    change.Width += xDist;
                                    break;
                                }
                            }
                        }
                    }
                }

                // Vertical resizing
                if (change.Height != 0)
                {
                    foreach (var otherShape in ShapeList.GetUnselectedElements())
                    {
                        if (otherShape != shape)
                        {
                            var yDist = otherShape.Bottom - (shape.Bottom + change.Height);
                            if (Math.Abs(yDist) <= PrecisionSize)
                            {
                                var distance1 = Math.Abs(shape.Left - otherShape.Right);
                                var distance2 = Math.Abs(otherShape.Left - shape.Right);
                                var distance = Math.Min(distance1, distance2);

                                if (distance <= MaximalPrecisionDistance)
                                {
                                    change.Height += yDist;
                                    break;
                                }
                            }
                        }
                    }
                }

                e.Change = change;
            }
        }

        private void RemoveShape(Shape shape)
        {
            if (shape.IsActive)
            {
                ActiveElement = null;
            }
            if (shape.IsSelected)
            {
                SelectedShapeCount--;
                OnSelectionChanged(EventArgs.Empty);
                OnClipboardAvailabilityChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
            shape.Diagram = null;
            shape.Modified -= element_Modified;
            shape.Activating -= element_Activating;
            shape.Dragging -= shape_Dragging;
            shape.Resizing -= shape_Resizing;
            shape.SelectionChanged -= shape_SelectionChanged;
            ShapeList.Remove(shape);
            RecalculateSize();
        }

        //TODO: legyenek inkább hivatkozások a shape-ekhez
        private Shape GetShape(IEntity entity)
        {
            foreach (var shape in ShapeList)
            {
                if (shape.Entity == entity)
                    return shape;
            }
            return null;
        }

        private Connection GetConnection(Relationship relationship)
        {
            foreach (var connection in ConnectionList)
            {
                if (connection.Relationship == relationship)
                    return connection;
            }
            return null;
        }

        private void AddConnection(Connection connection)
        {
            connection.Diagram = this;
            connection.Modified += element_Modified;
            connection.Activating += element_Activating;
            connection.SelectionChanged += connection_SelectionChanged;
            connection.RouteChanged += connection_RouteChanged;
            connection.BendPointMove += connection_BendPointMove;
            ConnectionList.AddFirst(connection);
            RecalculateSize();
        }

        private void RemoveConnection(Connection connection)
        {
            if (connection.IsSelected)
            {
                SelectedConnectionCount--;
                OnSelectionChanged(EventArgs.Empty);
                OnClipboardAvailabilityChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
            connection.Diagram = null;
            connection.Modified -= element_Modified;
            connection.Activating += element_Activating;
            connection.SelectionChanged -= connection_SelectionChanged;
            connection.RouteChanged -= connection_RouteChanged;
            connection.BendPointMove -= connection_BendPointMove;
            ConnectionList.Remove(connection);
            RecalculateSize();
        }

        private void shape_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectioning)
            {
                var shape = (Shape) sender;

                if (shape.IsSelected)
                {
                    SelectedShapeCount++;
                    ShapeList.ShiftToFirstPlace(shape);
                }
                else
                {
                    SelectedShapeCount--;
                }

                OnSelectionChanged(EventArgs.Empty);
                OnClipboardAvailabilityChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
        }

        private void connection_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectioning)
            {
                var connection = (Connection) sender;

                if (connection.IsSelected)
                {
                    SelectedConnectionCount++;
                    ConnectionList.ShiftToFirstPlace(connection);
                }
                else
                {
                    SelectedConnectionCount--;
                }

                OnSelectionChanged(EventArgs.Empty);
                OnClipboardAvailabilityChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
        }

        private void connection_RouteChanged(object sender, EventArgs e)
        {
            var connection = (Connection) sender;
            connection.ValidatePosition(DiagramPadding);

            RecalculateSize();
        }

        private void connection_BendPointMove(object sender, BendPointEventArgs e)
        {
            if (e.BendPoint.X < DiagramPadding)
                e.BendPoint.X = DiagramPadding;
            if (e.BendPoint.Y < DiagramPadding)
                e.BendPoint.Y = DiagramPadding;

            // Snap bend points to others if possible
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                foreach (var connection in ConnectionList.GetSelectedElements())
                {
                    foreach (var point in connection.BendPoints)
                    {
                        if (point != e.BendPoint && !point.AutoPosition)
                        {
                            var xDist = Math.Abs(e.BendPoint.X - point.X);
                            var yDist = Math.Abs(e.BendPoint.Y - point.Y);

                            if (xDist <= Connection.PrecisionSize)
                            {
                                e.BendPoint.X = point.X;
                            }
                            if (yDist <= Connection.PrecisionSize)
                            {
                                e.BendPoint.Y = point.Y;
                            }
                        }
                    }
                }
            }
        }

        public void CreateShape()
        {
            CreateShape(newShapeType);
        }

        public void CreateShape(EntityType type)
        {
            state = State.CreatingShape;
            shapeType = type;
            newShapeType = type;

            switch (type)
            {
                case EntityType.Class:
                case EntityType.Delegate:
                case EntityType.Enum:
                case EntityType.Interface:
                case EntityType.Structure:
                    shapeOutline = TypeShape.GetOutline(Style.CurrentStyle);
                    break;

                case EntityType.Comment:
                    shapeOutline = CommentShape.GetOutline(Style.CurrentStyle);
                    break;
            }
            shapeOutline.Location = new Point((int) mouseLocation.X, (int) mouseLocation.Y);
            Redraw();
        }

        public Shape AddShape(EntityType type)
        {
            switch (type)
            {
                case EntityType.Class:
                    AddClass();
                    break;

                case EntityType.Comment:
                    AddComment();
                    break;

                case EntityType.Delegate:
                    AddDelegate();
                    break;

                case EntityType.Enum:
                    AddEnum();
                    break;

                case EntityType.Interface:
                    AddInterface();
                    break;

                case EntityType.Structure:
                    AddStructure();
                    break;

                default:
                    return null;
            }

            RecalculateSize();
            return ShapeList.FirstValue;
        }

        protected override void AddClass(ClassType newClass)
        {
            base.AddClass(newClass);
            AddShape(new ClassShape(newClass));
        }

        protected override void AddStructure(StructureType structure)
        {
            base.AddStructure(structure);
            AddShape(new StructureShape(structure));
        }

        protected override void AddInterface(InterfaceType newInterface)
        {
            base.AddInterface(newInterface);
            var test = new InterfaceShape(newInterface);
            AddShape(test);
        }

        protected override void AddEnum(EnumType newEnum)
        {
            base.AddEnum(newEnum);
            AddShape(new EnumShape(newEnum));
        }

        protected override void AddDelegate(DelegateType newDelegate)
        {
            base.AddDelegate(newDelegate);
            AddShape(new DelegateShape(newDelegate));
        }

        protected override void AddComment(Comment comment)
        {
            base.AddComment(comment);
            AddShape(new CommentShape(comment));
        }

        public void CreateConnection(RelationshipType type)
        {
            connectionCreator = new ConnectionCreator(this, type);
            state = State.CreatingConnection;
        }

        protected override void AddAssociation(AssociationRelationship association)
        {
            base.AddAssociation(association);

            var startShape = GetShape(association.First);
            var endShape = GetShape(association.Second);
            AddConnection(new Association(association, startShape, endShape));
        }

        protected override void AddGeneralization(GeneralizationRelationship generalization)
        {
            base.AddGeneralization(generalization);

            var startShape = GetShape(generalization.First);
            var endShape = GetShape(generalization.Second);
            AddConnection(new Generalization(generalization, startShape, endShape));
        }

        protected override void AddRealization(RealizationRelationship realization)
        {
            base.AddRealization(realization);

            var startShape = GetShape(realization.First);
            var endShape = GetShape(realization.Second);
            AddConnection(new Realization(realization, startShape, endShape));
        }

        protected override void AddDependency(DependencyRelationship dependency)
        {
            base.AddDependency(dependency);

            var startShape = GetShape(dependency.First);
            var endShape = GetShape(dependency.Second);
            AddConnection(new Dependency(dependency, startShape, endShape));
        }

        protected override void AddNesting(NestingRelationship nesting)
        {
            base.AddNesting(nesting);

            var startShape = GetShape(nesting.First);
            var endShape = GetShape(nesting.Second);
            AddConnection(new Nesting(nesting, startShape, endShape));
        }

        protected override void AddCommentRelationship(CommentRelationship commentRelationship)
        {
            base.AddCommentRelationship(commentRelationship);

            var startShape = GetShape(commentRelationship.First);
            var endShape = GetShape(commentRelationship.Second);
            AddConnection(new CommentConnection(commentRelationship, startShape, endShape));
        }

        protected override void OnEntityRemoved(EntityEventArgs e)
        {
            var shape = GetShape(e.Entity);
            RemoveShape(shape);

            base.OnEntityRemoved(e);
        }

        protected override void OnRelationRemoved(RelationshipEventArgs e)
        {
            var connection = GetConnection(e.Relationship);
            RemoveConnection(connection);

            base.OnRelationRemoved(e);
        }

        protected override void OnDeserializing(SerializeEventArgs e)
        {
            base.OnDeserializing(e);

            // Old file format
            {
                var positionsNode = e.Node["Positions"];
                if (positionsNode != null)
                {
                    var currentShapeNode = ShapeList.Last;
                    foreach (XmlElement shapeNode in positionsNode.SelectNodes("Shape"))
                    {
                        if (currentShapeNode == null)
                            break;
                        currentShapeNode.Value.Deserialize(shapeNode);
                        currentShapeNode = currentShapeNode.Previous;
                    }

                    var currentConnecitonNode = ConnectionList.Last;
                    foreach (XmlElement connectionNode in positionsNode.SelectNodes("Connection"))
                    {
                        if (currentConnecitonNode == null)
                            break;
                        currentConnecitonNode.Value.Deserialize(connectionNode);
                        currentConnecitonNode = currentConnecitonNode.Previous;
                    }
                }
            }
        }

        protected virtual void OnOffsetChanged(EventArgs e)
        {
            if (OffsetChanged != null)
                OffsetChanged(this, e);
            UpdateWindowPosition();
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, e);
        }

        protected virtual void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
            CloseWindows();
        }

        protected virtual void OnSatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
                StatusChanged(this, e);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        protected virtual void OnNeedsRedraw(EventArgs e)
        {
            if (NeedsRedraw != null)
                NeedsRedraw(this, e);
        }

        protected virtual void OnClipboardAvailabilityChanged(EventArgs e)
        {
            if (ClipboardAvailabilityChanged != null)
                ClipboardAvailabilityChanged(this, e);
        }

        protected virtual void OnShowingWindow(PopupWindowEventArgs e)
        {
            if (ShowingWindow != null)
                ShowingWindow(this, e);
        }

        protected virtual void OnHidingWindow(PopupWindowEventArgs e)
        {
            if (HidingWindow != null)
                HidingWindow(this, e);
        }

        private enum State
        {
            Normal,
            Multiselecting,
            CreatingShape,
            CreatingConnection,
            Dragging
        }
    }
}