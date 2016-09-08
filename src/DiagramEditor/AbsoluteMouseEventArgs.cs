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

using System.Drawing;
using System.Windows.Forms;

namespace NClass.DiagramEditor
{
    public delegate void AbsoluteMouseEventHandler(object sender, AbsoluteMouseEventArgs e);

    public class AbsoluteMouseEventArgs
    {
        public AbsoluteMouseEventArgs(MouseButtons button, float x, float y, float zoom)
        {
            Button = button;
            X = x;
            Y = y;
            Zoom = zoom;
        }

        public AbsoluteMouseEventArgs(MouseButtons button, PointF location, float zoom)
        {
            Button = button;
            X = location.X;
            Y = location.Y;
            Zoom = zoom;
        }

        public AbsoluteMouseEventArgs(MouseEventArgs e, Point offset, float zoom)
        {
            Button = e.Button;
            X = (e.X + offset.X)/zoom;
            Y = (e.Y + offset.Y)/zoom;
            Zoom = zoom;
        }

        public AbsoluteMouseEventArgs(MouseEventArgs e, IDocument document)
            : this(e, document.Offset, document.Zoom)
        {
        }

        public MouseButtons Button { get; }

        public float X { get; }

        public float Y { get; }

        public PointF Location { get { return new PointF(X, Y); } }

        public bool Handled { get; set; } = false;

        public float Zoom { get; set; }
    }
}