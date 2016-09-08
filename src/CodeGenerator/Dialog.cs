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
using System.Collections.Specialized;
using System.Windows.Forms;
using NClass.Core;
using NClass.CSharp;
using NClass.Java;
using NClass.Translations;

namespace NClass.CodeGenerator
{
    public partial class Dialog : Form
    {
        private Project project;

        public Dialog()
        {
            InitializeComponent();
            importToolStrip.Renderer = ToolStripSimplifiedRenderer.Default;

            /*
            this.cboLanguage.Items.AddRange(new object[] {
            "C#",
            "Java",
            "C# extended"});
            */
        }

        private void UpdateTexts()
        {
            Text = Strings.CodeGeneration;
            lblDestination.Text = Strings.Destination;
            btnAddItem.Text = Strings.ButtonAddItem;
            btnBrowse.Text = Strings.ButtonBrowse;
            btnGenerate.Text = Strings.ButtonGenerate;
            btnCancel.Text = Strings.ButtonCancel;
            toolImportList.Text = Strings.ImportList;
            toolMoveUp.Text = Strings.MoveUp;
            toolMoveDown.Text = Strings.MoveDown;
            toolDelete.Text = Strings.Delete;
            lblSolutionType.Text = Strings.SolutionType;
            chkUseTabs.Text = Strings.UseTabs;
            lblIndentSize.Text = Strings.IndentSize;
            chkNotImplemented.Text = Strings.UseNotImplementedExceptions;
            chkXMLDocFood.Text = Strings.GenerateXMLDocFood;
            chkGenerateNunitTests.Text = Strings.GenerateNunitTests;
            lblCSharpFormatStyle.Text = Strings.CSharpFormatStyle;
            chkSortUsing.Text = Strings.SortUsing;
            lblTemplate.Text = Strings.TemplateFile;
            btnCopyrightHeader.Text = Strings.CopyrightHeader;
            btnCustomFormatStyle.Text = Strings.CustomFormatStyle;
        }

        private void UpdateValues()
        {
            lstImportList.Items.Clear();
            UpdateImportList();
            EnableCustomFormatStyleButton();

            txtDestination.Text = Settings.Default.DestinationPath;
            chkUseTabs.Checked = Settings.Default.UseTabsForIndents;
            updIndentSize.Value = Settings.Default.IndentSize;
            cboSolutionType.SelectedIndex = (int) Settings.Default.SolutionType;
            chkNotImplemented.Checked = Settings.Default.UseNotImplementedExceptions;
            cboLanguage.SelectedIndex = 0;

            cboCSharpFormat.SelectedIndex = 0;
            cboTemplateFile.SelectedIndex = 0;
        }

        private void UpdateImportList()
        {
            Language language = null;

            // TO DO: include that in Language Manager
            if (Equals(cboLanguage.SelectedItem, "C#"))
                language = CSharpLanguage.Instance;
            else if (Equals(cboLanguage.SelectedItem, "Java"))
                language = JavaLanguage.Instance;

            if (language != null)
            {
                Settings.Default.Upgrade();
                lstImportList.Items.Clear();
                foreach (var importString in Settings.Default.ImportList[language])
                    lstImportList.Items.Add(importString);
            }

            // Hide uneeded UI
            if (Equals(cboLanguage.SelectedItem, "C# extended"))
            {
                tabPageCSharpExt.Enabled = true;
                tabPageCSharpJava.Enabled = false;
            }
            else
            {
                tabPageCSharpExt.Enabled = false;
                tabPageCSharpJava.Enabled = true;
            }
        }

        private void SaveImportList()
        {
            var importList = new StringCollection();
            foreach (var import in lstImportList.Items)
                importList.Add(import.ToString());

            //TODO: ezt is másképp kéne
            if (Equals(cboLanguage.SelectedItem, "C#"))
                Settings.Default.CSharpImportList = importList;
            else if (Equals(cboLanguage.SelectedItem, "Java"))
                Settings.Default.JavaImportList = importList;
            else if (Equals(cboLanguage.SelectedItem, "C# extented"))
                Settings.Default.CSharpImportList = importList;
        }

        public void ShowDialog(Project project)
        {
            this.project = project;

            UpdateTexts();
            UpdateValues();
            ShowDialog();

            if (DialogResult == DialogResult.OK)
                Settings.Default.Save();
            else
                Settings.Default.Reload();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = Strings.GeneratorTargetDir;
                dialog.SelectedPath = txtDestination.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                    txtDestination.Text = dialog.SelectedPath;
            }
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImportList();
        }

        private void toolMoveUp_Click(object sender, EventArgs e)
        {
            var index = lstImportList.SelectedIndex;
            if (index > 0)
            {
                var temp = lstImportList.Items[index];
                lstImportList.Items[index] = lstImportList.Items[index - 1];
                lstImportList.Items[index - 1] = temp;
                lstImportList.SelectedIndex--;
                SaveImportList();
            }
        }

        private void toolMoveDown_Click(object sender, EventArgs e)
        {
            var index = lstImportList.SelectedIndex;
            if (index < lstImportList.Items.Count - 1)
            {
                var temp = lstImportList.Items[index];
                lstImportList.Items[index] = lstImportList.Items[index + 1];
                lstImportList.Items[index + 1] = temp;
                lstImportList.SelectedIndex++;
                SaveImportList();
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (lstImportList.SelectedItem != null)
            {
                var selectedIndex = lstImportList.SelectedIndex;
                lstImportList.Items.RemoveAt(selectedIndex);
                if (lstImportList.Items.Count > selectedIndex)
                    lstImportList.SelectedIndex = selectedIndex;
                else
                    lstImportList.SelectedIndex = lstImportList.Items.Count - 1;
                SaveImportList();
            }
        }

        private void lstImportList_SelectedValueChanged(object sender, EventArgs e)
        {
            var isSelected = lstImportList.SelectedItem != null;

            toolMoveUp.Enabled = isSelected && (lstImportList.SelectedIndex > 0);
            toolMoveDown.Enabled = isSelected &&
                                   (lstImportList.SelectedIndex < lstImportList.Items.Count - 1);
            toolDelete.Enabled = isSelected;
        }

        private void lstImportList_Leave(object sender, EventArgs e)
        {
            lstImportList.SelectedItem = null;
        }

        private void txtNewImport_TextChanged(object sender, EventArgs e)
        {
            btnAddItem.Enabled = txtNewImport.Text.Length > 0;
        }

        private void txtNewImport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtNewImport.Text.Length > 0)
            {
                lstImportList.Items.Add(txtNewImport.Text);
                txtNewImport.Text = string.Empty;
                SaveImportList();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            lstImportList.Items.Add(txtNewImport.Text);
            txtNewImport.Text = string.Empty;
            txtNewImport.Focus();
            SaveImportList();
        }

        private void chkUseTabs_CheckedChanged(object sender, EventArgs e)
        {
            var useTabs = chkUseTabs.Checked;

            lblIndentSize.Enabled = !useTabs;
            updIndentSize.Enabled = !useTabs;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                ValidateSettings();

                try
                {
                    var solutionType = (SolutionType) cboSolutionType.SelectedIndex;
                    var generator = new Generator(project, solutionType);
                    var destination = txtDestination.Text;

                    var result = generator.Generate(destination,
                                                    chkSortUsing.Checked,
                                                    chkXMLDocFood.Checked,
                                                    Settings.Default.CompagnyName,
                                                    Settings.Default.CopyrightHeader,
                                                    Settings.Default.Author);
                    if (result == GenerationResult.Success)
                    {
                        if (Equals(cboLanguage.SelectedItem, "C# extented"))
                        {
                            // TO DO : ICSharpCode.NRefactory.CSharp. FormattingOptionsFactory

                            // TO DO : Merge old and new files!
                        }

                        MessageBox.Show(Strings.CodeGenerationCompleted,
                                        Strings.CodeGeneration,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else if (result == GenerationResult.Error)
                    {
                        MessageBox.Show(Strings.CodeGenerationFailed,
                                        Strings.Error,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else // Cancelled
                    {
                        DialogResult = DialogResult.None;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                    Strings.UnknownError,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void ValidateSettings()
        {
            Settings.Default.DestinationPath = txtDestination.Text;
            Settings.Default.UseTabsForIndents = chkUseTabs.Checked;
            Settings.Default.IndentSize = (int) updIndentSize.Value;
            Settings.Default.SolutionType = (SolutionType) cboSolutionType.SelectedIndex;
            Settings.Default.UseNotImplementedExceptions = chkNotImplemented.Checked;
            // TO DO - Add other settings
        }

        private void cboCSharpFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableCustomFormatStyleButton();
        }

        private void EnableCustomFormatStyleButton()
        {
            if (Equals(cboCSharpFormat.SelectedItem, "Custom"))
                btnCustomFormatStyle.Enabled = true;
            else
                btnCustomFormatStyle.Enabled = false;
        }

        private void btnCustomFormatStyle_Click(object sender, EventArgs e)
        {
            var dialogBox = new FormatStyleSettings();

            dialogBox.ShowDialog();
        }

        private void btnCopyrightHeader_Click(object sender, EventArgs e)
        {
            var dialogBox = new CopyrightHeader();

            dialogBox.ShowDialog();
        }
    }
}