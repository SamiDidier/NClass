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

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NClass.DiagramEditor
{
    public abstract class DynamicMenu : IEnumerable<ToolStripMenuItem>
    {
        public DynamicMenu()
        {
            PreferredIndex = -1;
        }

        public DynamicMenu(int preferredIndex)
        {
            PreferredIndex = preferredIndex;
        }

        public int PreferredIndex { get; }

        public IEnumerator<ToolStripMenuItem> GetEnumerator()
        {
            return GetMenuItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerable<ToolStripMenuItem> GetMenuItems();

        public abstract ToolStrip GetToolStrip();

        public abstract void SetReference(IDocument document);
    }
}