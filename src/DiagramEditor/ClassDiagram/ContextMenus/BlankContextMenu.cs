// NClass - Free class diagram editor
// Copyright (C) 2006-2007 Balazs Tihanyi
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
using System.Windows.Forms;
using NClass.Core;
using NClass.DiagramEditor.Properties;
using NClass.Translations;

namespace NClass.DiagramEditor.ClassDiagram.ContextMenus
{
    public sealed class BlankContextMenu : DiagramContextMenu
    {
        private BlankContextMenu()
        {
            InitMenuItems();
        }

        public static BlankContextMenu Default { get; } = new BlankContextMenu();

        public override void ValidateMenuItems(Diagram diagram)
        {
            base.ValidateMenuItems(diagram);
            mnuPaste.Enabled = diagram.CanPasteFromClipboard;

            mnuNewStructure.Visible = diagram.Language.SupportsStructures;
            mnuNewDelegate.Visible = diagram.Language.SupportsDelegates;

            mnuShowType.Checked = Settings.Default.ShowType;
            mnuShowParameters.Checked = Settings.Default.ShowParameters;
            mnuShowParameterNames.Checked = Settings.Default.ShowParameterNames;
            mnuShowInitialValue.Checked = Settings.Default.ShowInitialValue;

            mnuSaveAsImage.Enabled = !diagram.IsEmpty;
        }

        private void InitMenuItems()
        {
            mnuAddNewElement = new ToolStripMenuItem(Strings.MenuNew, Resources.NewEntity);
            mnuNewClass = new ToolStripMenuItem(Strings.MenuClass, Resources.Class, mnuNewClass_Click);
            mnuNewStructure = new ToolStripMenuItem(Strings.MenuStruct, Resources.Structure, mnuNewStructure_Click);
            mnuNewInterface = new ToolStripMenuItem(Strings.MenuInterface, Resources.Interface32, mnuNewInterface_Click);
            mnuNewEnum = new ToolStripMenuItem(Strings.MenuEnum, Resources.Enum, mnuNewEnum_Click);
            mnuNewDelegate = new ToolStripMenuItem(Strings.MenuDelegate, Resources.Delegate, mnuNewDelegate_Click);
            mnuNewComment = new ToolStripMenuItem(Strings.MenuComment, Resources.Comment, mnuNewComment_Click);
            mnuNewAssociation = new ToolStripMenuItem(Strings.MenuAssociation,
                                                      Resources.Association,
                                                      mnuNewAssociation_Click);
            mnuNewComposition = new ToolStripMenuItem(Strings.MenuComposition,
                                                      Resources.Composition,
                                                      mnuNewComposition_Click);
            mnuNewAggregation = new ToolStripMenuItem(Strings.MenuAggregation,
                                                      Resources.Aggregation,
                                                      mnuNewAggregation_Click);
            mnuNewGeneralization = new ToolStripMenuItem(Strings.MenuGeneralization,
                                                         Resources.Generalization,
                                                         mnuNewGeneralization_Click);
            mnuNewRealization = new ToolStripMenuItem(Strings.MenuRealization,
                                                      Resources.Realization,
                                                      mnuNewRealization_Click);
            mnuNewDependency = new ToolStripMenuItem(Strings.MenuDependency,
                                                     Resources.Dependency,
                                                     mnuNewDependency_Click);
            mnuNewNesting = new ToolStripMenuItem(Strings.MenuNesting, Resources.Nesting, mnuNewNesting_Click);
            mnuNewCommentRelationship = new ToolStripMenuItem(Strings.MenuCommentRelationship,
                                                              Resources.CommentRel,
                                                              mnuNewCommentRelationship_Click);

            mnuMembersFormat = new ToolStripMenuItem(Strings.MenuMembersFormat, null);
            mnuShowType = new ToolStripMenuItem(Strings.MenuType, null);
            mnuShowType.CheckedChanged += mnuShowType_CheckedChanged;
            mnuShowType.CheckOnClick = true;
            mnuShowParameters = new ToolStripMenuItem(Strings.MenuParameters, null);
            mnuShowParameters.CheckedChanged += mnuShowParameters_CheckedChanged;
            mnuShowParameters.CheckOnClick = true;
            mnuShowParameterNames = new ToolStripMenuItem(Strings.MenuParameterNames, null);
            mnuShowParameterNames.CheckedChanged += mnuShowParameterNames_CheckedChanged;
            mnuShowParameterNames.CheckOnClick = true;
            mnuShowInitialValue = new ToolStripMenuItem(Strings.MenuInitialValue, null);
            mnuShowInitialValue.CheckedChanged += mnuShowInitialValue_CheckedChanged;
            mnuShowInitialValue.CheckOnClick = true;

            mnuPaste = new ToolStripMenuItem(Strings.MenuPaste, Resources.Paste, mnuPaste_Click);
            mnuSaveAsImage = new ToolStripMenuItem(Strings.MenuSaveAsImage, Resources.Image, mnuSaveAsImage_Click);
            mnuSelectAll = new ToolStripMenuItem(Strings.MenuSelectAll, null, mnuSelectAll_Click);

            mnuAddNewElement.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuNewClass,
                mnuNewStructure,
                mnuNewInterface,
                mnuNewEnum,
                mnuNewDelegate,
                mnuNewComment,
                new ToolStripSeparator(),
                mnuNewAssociation,
                mnuNewComposition,
                mnuNewAggregation,
                mnuNewGeneralization,
                mnuNewRealization,
                mnuNewDependency,
                mnuNewNesting,
                mnuNewCommentRelationship
            });
            mnuMembersFormat.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuShowType,
                mnuShowParameters,
                mnuShowParameterNames,
                mnuShowInitialValue
            });
            MenuList.AddRange(new ToolStripItem[]
            {
                mnuAddNewElement,
                mnuMembersFormat,
                new ToolStripSeparator(),
                mnuPaste,
                mnuSaveAsImage,
                mnuSelectAll
            });
        }

        private void mnuNewClass_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Class);
        }

        private void mnuNewStructure_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Structure);
        }

        private void mnuNewInterface_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Interface);
        }

        private void mnuNewEnum_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Enum);
        }

        private void mnuNewDelegate_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Delegate);
        }

        private void mnuNewComment_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateShape(EntityType.Comment);
        }

        private void mnuNewAssociation_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Association);
        }

        private void mnuNewComposition_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Composition);
        }

        private void mnuNewAggregation_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Aggregation);
        }

        private void mnuNewGeneralization_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Generalization);
        }

        private void mnuNewRealization_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Realization);
        }

        private void mnuNewDependency_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Dependency);
        }

        private void mnuNewNesting_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Nesting);
        }

        private void mnuNewCommentRelationship_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CreateConnection(RelationshipType.Comment);
        }

        private void mnuShowType_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowType = ((ToolStripMenuItem) sender).Checked;
            if (Diagram != null)
                Diagram.Redraw();
        }

        private void mnuShowParameters_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowParameters = ((ToolStripMenuItem) sender).Checked;
            if (Diagram != null)
                Diagram.Redraw();
        }

        private void mnuShowParameterNames_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowParameterNames = ((ToolStripMenuItem) sender).Checked;
            if (Diagram != null)
                Diagram.Redraw();
        }

        private void mnuShowInitialValue_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowInitialValue = ((ToolStripMenuItem) sender).Checked;
            if (Diagram != null)
                Diagram.Redraw();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.Paste();
        }

        private void mnuSaveAsImage_Click(object sender, EventArgs e)
        {
            if (Diagram != null && !Diagram.IsEmpty)
                Diagram.SaveAsImage();
        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.SelectAll();
        }

        #region MenuItem fields

        private ToolStripMenuItem mnuAddNewElement;
        private ToolStripMenuItem mnuNewClass;
        private ToolStripMenuItem mnuNewStructure;
        private ToolStripMenuItem mnuNewInterface;
        private ToolStripMenuItem mnuNewEnum;
        private ToolStripMenuItem mnuNewDelegate;
        private ToolStripMenuItem mnuNewComment;
        private ToolStripMenuItem mnuNewAssociation;
        private ToolStripMenuItem mnuNewComposition;
        private ToolStripMenuItem mnuNewAggregation;
        private ToolStripMenuItem mnuNewGeneralization;
        private ToolStripMenuItem mnuNewRealization;
        private ToolStripMenuItem mnuNewDependency;
        private ToolStripMenuItem mnuNewNesting;
        private ToolStripMenuItem mnuNewCommentRelationship;

        private ToolStripMenuItem mnuMembersFormat;
        private ToolStripMenuItem mnuShowType;
        private ToolStripMenuItem mnuShowParameters;
        private ToolStripMenuItem mnuShowParameterNames;
        private ToolStripMenuItem mnuShowInitialValue;

        private ToolStripMenuItem mnuPaste;
        private ToolStripMenuItem mnuSaveAsImage;
        private ToolStripMenuItem mnuSelectAll;

        #endregion
    }
}