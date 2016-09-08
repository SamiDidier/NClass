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
using System.Windows.Forms;
using NClass.DiagramEditor;
using NClass.Translations;

namespace NClass.GUI
{
    public partial class TabBar : Control
    {
        private const int LeftMargin = 3;
        private const int TopMargin = 3;
        private const int ClosingSignSize = 8;
        private bool activeCloseButton;
        private Tab activeTab;
        private Font activeTabFont;


        private DocumentManager docManager;
        private Tab grabbedTab;
        private int maxTabWidth = 200;
        private int originalPosition;

        private readonly StringFormat stringFormat;
        private readonly List<Tab> tabs = new List<Tab>();

        public TabBar()
        {
            InitializeComponent();
            UpdateTexts();
            BackColor = SystemColors.Control;
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.ResizeRedraw, true);

            stringFormat = new StringFormat(StringFormat.GenericTypographic);
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            stringFormat.FormatFlags |= StringFormatFlags.NoWrap;

            activeTabFont = new Font(Font, FontStyle.Bold);
        }

        [Browsable(false)]
        public DocumentManager DocumentManager
        {
            get { return docManager; }
            set
            {
                if (docManager != value)
                {
                    if (docManager != null)
                    {
                        docManager.ActiveDocumentChanged -= docManager_ActiveDocumentChanged;
                        docManager.DocumentAdded -= docManager_DocumentAdded;
                        docManager.DocumentRemoved -= docManager_DocumentRemoved;
                        docManager.DocumentMoved -= docManager_DocumentMoved;
                        ClearTabs();
                    }
                    docManager = value;
                    if (docManager != null)
                    {
                        docManager.ActiveDocumentChanged += docManager_ActiveDocumentChanged;
                        docManager.DocumentAdded += docManager_DocumentAdded;
                        docManager.DocumentRemoved += docManager_DocumentRemoved;
                        docManager.DocumentMoved += docManager_DocumentMoved;
                        CreateTabs();
                    }
                }
            }
        }

        [DefaultValue(typeof (Color), "Control")]
        public override Color BackColor
        {
            get
            {
                if (Parent != null && !DesignMode &&
                    (docManager == null || !docManager.HasDocument))
                {
                    return Parent.BackColor;
                }
                return base.BackColor;
            }
            set { base.BackColor = value; }
        }

        [DefaultValue(typeof (Color), "ControlDark")]
        public Color BorderColor { get; set; } = SystemColors.ControlDark;

        [DefaultValue(typeof (Color), "White")]
        public Color ActiveTabColor { get; set; } = Color.White;

        [DefaultValue(typeof (Color), "ControlLight")]
        public Color InactiveTabColor { get; set; } = SystemColors.ControlLight;

        [DefaultValue(200)]
        public int MaxTabWidth
        {
            get { return maxTabWidth; }
            set
            {
                maxTabWidth = value;
                if (maxTabWidth < 50)
                    maxTabWidth = 50;
            }
        }

        protected override Size DefaultSize { get { return new Size(100, 25); } }

        private void UpdateTexts()
        {
            mnuClose.Text = Strings.MenuCloseTab;
            mnuCloseAll.Text = Strings.MenuCloseAllTabs;
            mnuCloseAllButThis.Text = Strings.MenuCloseAllTabsButThis;
        }

        private void CreateTabs()
        {
            foreach (var doc in docManager.Documents)
            {
                var tab = new Tab(doc, this);
                tabs.Add(tab);
                if (doc == docManager.ActiveDocument)
                    activeTab = tab;
            }
        }

        private void ClearTabs()
        {
            foreach (var tab in tabs)
                tab.Detached();
            tabs.Clear();
            activeTab = null;
        }

        private void docManager_ActiveDocumentChanged(object sender, DocumentEventArgs e)
        {
            foreach (var tab in tabs)
            {
                if (tab.Document == docManager.ActiveDocument)
                {
                    activeTab = tab;
                    break;
                }
            }
            Invalidate();
        }

        private void docManager_DocumentAdded(object sender, DocumentEventArgs e)
        {
            var tab = new Tab(e.Document, this);
            tabs.Add(tab);
        }

        private void docManager_DocumentRemoved(object sender, DocumentEventArgs e)
        {
            for (var index = 0; index < tabs.Count; index++)
            {
                if (tabs[index].Document == e.Document)
                {
                    tabs.RemoveAt(index);
                    Invalidate();
                    return;
                }
            }
        }

        private void docManager_DocumentMoved(object sender, DocumentMovedEventArgs e)
        {
            var tab = tabs[e.OldPostion];

            if (e.NewPosition > e.OldPostion)
            {
                for (var i = e.OldPostion; i < e.NewPosition; i++)
                    tabs[i] = tabs[i + 1];
            }
            else // e.NewPosition < e.OldPostion
            {
                for (var i = e.OldPostion; i > e.NewPosition; i--)
                    tabs[i] = tabs[i - 1];
            }
            tabs[e.NewPosition] = tab;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            var selectedTab = PickTab(e.Location);
            if (selectedTab != null)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    docManager.Close(selectedTab.Document);
                }
                else
                {
                    docManager.ActiveDocument = selectedTab.Document;
                    grabbedTab = selectedTab;
                    originalPosition = e.X;
                }
            }
            else if (docManager.HasDocument && IsOverClosingSign(e.Location))
            {
                docManager.Close(docManager.ActiveDocument);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            var selectedTab = PickTab(e.Location);
            if (selectedTab != null && e.Button == MouseButtons.Left)
            {
                docManager.Close(selectedTab.Document);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left && grabbedTab != null)
            {
                MoveTab(grabbedTab, e.Location);
            }

            var overClosingSign = IsOverClosingSign(e.Location);
            if (activeCloseButton != overClosingSign)
            {
                activeCloseButton = overClosingSign;
                Invalidate();
            }
        }

        private bool IsOverClosingSign(Point location)
        {
            var margin = (Height - ClosingSignSize)/2;
            var left = Width - margin - ClosingSignSize;

            return location.X >= left && location.X <= left + ClosingSignSize &&
                   location.Y >= margin && location.Y <= margin + ClosingSignSize;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            grabbedTab = null;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (activeCloseButton)
            {
                activeCloseButton = false;
                Invalidate();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            activeTabFont.Dispose();
            activeTabFont = new Font(Font, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (tabs.Count > 0)
            {
                DrawTabs(e.Graphics);
                DrawCloseIcon(e.Graphics);
            }
        }

        private Tab PickTab(Point point)
        {
            var x = LeftMargin;

            foreach (var tab in tabs)
            {
                if (point.X >= x && point.X < x + tab.Width)
                    return tab;
                x += tab.Width;
            }
            return null;
        }

        private void MoveTab(Tab grabbedTab, Point destination)
        {
            var newNeighbourTab = PickTab(destination);

            if (newNeighbourTab == grabbedTab)
            {
                originalPosition = destination.X;
            }
            else if (newNeighbourTab != null)
            {
                var oldIndex = tabs.IndexOf(grabbedTab);
                var newIndex = tabs.IndexOf(newNeighbourTab);

                if (newIndex > oldIndex) // Moving right
                {
                    if (destination.X >= originalPosition)
                        docManager.MoveDocument(grabbedTab.Document, newIndex - oldIndex);
                }
                else if (newIndex < oldIndex) // Moving left
                {
                    if (destination.X <= originalPosition)
                        docManager.MoveDocument(grabbedTab.Document, newIndex - oldIndex);
                }
            }
        }

        private void DrawTabs(Graphics g)
        {
            var borderPen = new Pen(BorderColor);
            Brush activeTabBrush = new SolidBrush(ActiveTabColor);
            Brush inactiveTabBrush = new LinearGradientBrush(
                new Rectangle(0, 5, Width, Height - 1),
                ActiveTabColor,
                SystemColors.ControlLight,
                LinearGradientMode.Vertical);
            Brush textBrush;
            var left = LeftMargin;

            if (ForeColor.IsKnownColor)
                textBrush = SystemBrushes.FromSystemColor(ForeColor);
            else
                textBrush = new SolidBrush(ForeColor);

            g.DrawLine(borderPen, 0, Height - 1, left, Height - 1);
            foreach (var tab in tabs)
            {
                //TODO: szépíteni
                var isActiveTab = tab == activeTab;
                var top = isActiveTab ? TopMargin : TopMargin + 2;
                var tabBrush = isActiveTab ? activeTabBrush : inactiveTabBrush;
                var tabRectangle = new Rectangle(left, top, tab.Width, Height - top);

                // To display bottom line for inactive tabs
                if (!isActiveTab)
                    tabRectangle.Height--;

                g.FillRectangle(tabBrush, tabRectangle); // Draw background
                g.DrawRectangle(borderPen, tabRectangle); // Draw border
                var font = isActiveTab ? activeTabFont : Font;
                g.DrawString(tab.Text, font, textBrush, tabRectangle, stringFormat);

                left += tab.Width;
            }
            g.DrawLine(borderPen, left, Height - 1, Width - 1, Height - 1);

            borderPen.Dispose();
            if (!ForeColor.IsKnownColor)
                textBrush.Dispose();
            activeTabBrush.Dispose();
            inactiveTabBrush.Dispose();
        }

        private void DrawCloseIcon(Graphics g)
        {
            var lineColor = activeCloseButton ? SystemColors.ControlText : SystemColors.ControlDark;
            var margin = (Height - ClosingSignSize)/2;
            var left = Width - margin - ClosingSignSize;
            var linePen = new Pen(lineColor, 2);

            g.DrawLine(linePen, left, margin, left + ClosingSignSize, margin + ClosingSignSize);
            g.DrawLine(linePen, left, margin + ClosingSignSize, left + ClosingSignSize, margin);
            linePen.Dispose();
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (docManager == null || !docManager.HasDocument)
            {
                e.Cancel = true;
            }
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            if (docManager != null && activeTab != null)
            {
                docManager.Close(activeTab.Document);
            }
        }

        private void mnuCloseAll_Click(object sender, EventArgs e)
        {
            if (docManager != null)
                docManager.CloseAll();
        }

        private void mnuCloseAllButThis_Click(object sender, EventArgs e)
        {
            if (docManager != null && activeTab != null)
            {
                docManager.CloseAllOthers(activeTab.Document);
            }
        }

        private class Tab
        {
            private const int MinWidth = 60;
            private const int TextMargin = 20;

            private readonly TabBar parent;
            private string text;

            public Tab(IDocument document, TabBar parent)
            {
                Document = document;
                this.parent = parent;
                document.Renamed += document_Renamed;
                text = document.Name;
                TextWidth = MeasureWidth(text);
            }

            public IDocument Document { get; }

            public string Text
            {
                get { return text; }
                private set
                {
                    if (text != value)
                    {
                        text = value;
                        TextWidth = MeasureWidth(text);
                        parent.Invalidate();
                    }
                }
            }

            public float TextWidth { get; private set; }

            public int Width { get { return Math.Max(MinWidth, (int) TextWidth + TextMargin); } }

            public bool IsActive { get { return Document == parent.docManager.ActiveDocument; } }

            private void document_Renamed(object sender, EventArgs e)
            {
                Text = Document.Name;
            }

            public void Detached()
            {
                Document.Renamed -= document_Renamed;
            }

            private float MeasureWidth(string text)
            {
                var g = parent.CreateGraphics();

                var textSize = g.MeasureString(text,
                                               parent.activeTabFont,
                                               parent.MaxTabWidth,
                                               parent.stringFormat);
                g.Dispose();

                return textSize.Width;
            }

            public override string ToString()
            {
                return text;
            }
        }
    }
}