using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NClass.AssemblyCSharpImport.Controls;
using NClass.AssemblyCSharpImport.Lang;
using NClass.AssemblyCSharpImport.Properties;
using NReflect.Filter;
using Resources = NClass.DiagramEditor.Properties.Resources;

namespace NClass.AssemblyCSharpImport
{
    /// <summary>
    ///     A form to set up the ImportSettings.
    /// </summary>
    public partial class ImportSettingsForm : Form
    {
        #region === Construction

        /// <summary>
        ///     Initializes a new ImportSettingsForm2.
        /// </summary>
        /// <param name="settings">The <see cref="ImportSettings" /> which will be used for import. </param>
        public ImportSettingsForm(ImportSettings settings)
        {
            InitializeComponent();

            // Add the icons to the realisation check boxes.
            chkCreateRealizations.Image = Resources.Realization;
            chkCreateNesting.Image = Resources.Nesting;
            chkCreateGeneralizations.Image = Resources.Generalization;
            chkCreateAssociations.Image = Resources.Association;

            //Localization goes here...
            colFilterElement.Items.Clear();
            colFilterElement.ImageSize = new Size(16, 16);
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Elements, null, FilterElements.AllElements));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Class,
                                                             Resources.Class,
                                                             FilterElements.Class));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Struct,
                                                             Resources.Structure,
                                                             FilterElements.Struct));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Interface,
                                                             Resources.Interface32,
                                                             FilterElements.Interface));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Delegate,
                                                             Resources.Delegate,
                                                             FilterElements.Delegate));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Enum, Resources.Enum, FilterElements.Enum));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_EnumValue,
                                                             Resources.EnumItem,
                                                             FilterElements.EnumValue));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Constructor,
                                                             Resources.Constructor,
                                                             FilterElements.Constructor));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Field,
                                                             Resources.Field,
                                                             FilterElements.Field));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Constant,
                                                             Resources.PublicConst,
                                                             FilterElements.Constant));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Property,
                                                             Resources.Property,
                                                             FilterElements.Property));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Method,
                                                             Resources.Method,
                                                             FilterElements.Method));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Operator,
                                                             Resources.PublicOperator,
                                                             FilterElements.Operator));
            colFilterElement.Items.Add(new ImageComboBoxItem(Strings.Element_Event,
                                                             Resources.Event,
                                                             FilterElements.Event));

            colFilterModifier.Items.Clear();
            colFilterModifier.ImageSize = new Size(16, 16);
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_All, null, FilterModifiers.AllModifiers));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Instance, null, FilterModifiers.Instance));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Static,
                                                              Resources.Static,
                                                              FilterModifiers.Static));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Default,
                                                              Properties.Resources.ModifierDefault,
                                                              FilterModifiers.Default));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Public, null, FilterModifiers.Public));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Private,
                                                              Properties.Resources.ModifierPrivate,
                                                              FilterModifiers.Private));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Internal,
                                                              Properties.Resources.ModifierInternal,
                                                              FilterModifiers.Internal));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_Protected,
                                                              Properties.Resources.ModifierProtected,
                                                              FilterModifiers.Protected));
            colFilterModifier.Items.Add(new ImageComboBoxItem(Strings.Modifier_ProtectedInternal,
                                                              Properties.Resources.ModifierProtectedInternal,
                                                              FilterModifiers.ProtectedInternal));

            //Build reverse maps for easy access while loading a template.
            reverseElementNameMap.Clear();
            foreach (var comboBoxItem in colFilterElement.Items)
            {
                reverseElementNameMap.Add((FilterElements) comboBoxItem.Tag, comboBoxItem);
            }
            reverseModifierNameMap.Clear();
            foreach (var comboBoxItem in colFilterModifier.Items)
            {
                reverseModifierNameMap.Add((FilterModifiers) comboBoxItem.Tag, comboBoxItem);
            }

            importSettings = settings;

            //Templates
            cboTemplate.Items.Clear();
            if (Settings.Default.ImportSettingsTemplates == null)
            {
                Settings.Default.ImportSettingsTemplates = new TemplateList();
                var newSettings = new ImportSettings
                {
                    Name = Strings.Settings_Template_LastUsed,
                    CreateAssociations = true,
                    CreateGeneralizations = true,
                    CreateNestings = true,
                    CreateRealizations = true,
                    CreateRelationships = true,
                    LabelAggregations = true
                };
                Settings.Default.ImportSettingsTemplates.Add(newSettings);
            }
            foreach (var xTemplate in Settings.Default.ImportSettingsTemplates)
            {
                cboTemplate.Items.Add(xTemplate);
            }
            cboTemplate.SelectedItem = cboTemplate.Items[0];
            DisplaySettings((ImportSettings) cboTemplate.Items[0]);

            LocalizeComponents();
        }

        /// <summary>
        ///     Displays the text for the current culture.
        /// </summary>
        private void LocalizeComponents()
        {
            Text = Strings.Settings_Title;
            grpTemplate.Text = Strings.Settings_Template;
            cmdLoadTemplate.Text = Strings.Settings_Template_LoadButton;
            cmdStoreTemplate.Text = Strings.Settings_Template_StoreButton;
            cmdDeleteTemplate.Text = Strings.Settings_Template_DeleteButton;

            grpFilter.Text = Strings.Settings_Filter_GroupTitle;
            rdoWhiteList.Text = Strings.Settings_Filter_WhiteList;
            rdoBlackList.Text = Strings.Settings_Filter_BlackList;
            colFilterElement.HeaderText = Strings.Settings_Filter_ElementColumnTitle;
            colFilterModifier.HeaderText = Strings.Settings_Filter_ModifierColumnTitle;

            chkCreateRelationships.Text = Strings.Settings_CreateRelationships;
            chkCreateAssociations.Text = Strings.Settings_CreateAssociations;
            chkLabelAssociations.Text = Strings.Settings_CreateLabel;
            chkRemoveFields.Text = Strings.Settings_RemoveFields;
            chkCreateGeneralizations.Text = Strings.Settings_CreateGeneralizations;
            chkCreateRealizations.Text = Strings.Settings_CreateRealizations;
            chkCreateNesting.Text = Strings.Settings_CreateNesting;

            cmdOK.Text = Strings.Settings_OKButton;
            cmdCancel.Text = Strings.Settings_CancelButton;
        }

        #endregion

        #region === Fields

        /// <summary>
        ///     The settings which are used for the import
        /// </summary>
        private readonly ImportSettings importSettings;

        ///// <summary>
        ///// A map from element names to the element enum.
        ///// </summary>
        //readonly Dictionary<string, Elements> elementNameMap = new Dictionary<string, Elements>();
        /// <summary>
        ///     A map from element enum to the element names.
        /// </summary>
        private readonly Dictionary<FilterElements, ImageComboBoxItem> reverseElementNameMap =
            new Dictionary<FilterElements, ImageComboBoxItem>();

        ///// <summary>
        ///// A map from the modifier names to the modifier enum.
        ///// </summary>
        //readonly Dictionary<string, Modifiers> modifierNameMap = new Dictionary<string, Modifiers>();
        /// <summary>
        ///     A map from the modifier enum to the modifier names.
        /// </summary>
        private readonly Dictionary<FilterModifiers, ImageComboBoxItem> reverseModifierNameMap =
            new Dictionary<FilterModifiers, ImageComboBoxItem>();

        #endregion

        #region === Event-Methods

        /// <summary>
        ///     Gets called when the OK-button is clicked. Closes the dialog.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            // Files or folders should be available
            if (lbEntries.Items == null)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(Strings.Settings_Error_NoEntries,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (lbEntries.Items.Count == 0)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(Strings.Settings_Error_NoEntries,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        /// <summary>
        ///     Gets called when the dialog is closed. Stores all settings.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void ImportSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StoreSettings(importSettings);
            StoreSettings((ImportSettings) Settings.Default.ImportSettingsTemplates[0]); //<last used>
            Settings.Default.Save();
        }

        /// <summary>
        ///     Gets called when a key is pressed while the dialog is opened.
        ///     Closes the dialog if the key is escape.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void ImportSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Gets called when the LoadTemplate-button is clicked. Displays the
        ///     settings belonging to the actual template.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdLoadTemplate_Click(object sender, EventArgs e)
        {
            if (cboTemplate.SelectedItem == null)
            {
                MessageBox.Show(Strings.Settings_Error_NoTemplateSelected,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            DisplaySettings((ImportSettings) cboTemplate.SelectedItem);
        }

        /// <summary>
        ///     Gets called when the StoreTemplate-button is clicked. Stores the settings
        ///     to a template.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdStoreTemplate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboTemplate.Text))
            {
                MessageBox.Show(Strings.Settings_Error_NoTemplateName,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (cboTemplate.Text.Contains("<"))
            {
                MessageBox.Show(Strings.Settings_Error_AngleBracketNotAllowed,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            var settings = (ImportSettings) cboTemplate.SelectedItem ?? new ImportSettings();
            StoreSettings(settings);
            settings.Name = cboTemplate.Text;
            if (cboTemplate.SelectedItem == null)
            {
                //New entry
                cboTemplate.Items.Add(settings);
                cboTemplate.SelectedItem = settings;
                Settings.Default.ImportSettingsTemplates.Add(settings);
            }
        }

        /// <summary>
        ///     Gets called when the DeleteTemplate-button is clicked. Deletes the
        ///     actual template.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdDeleteTemplate_Click(object sender, EventArgs e)
        {
            if (cboTemplate.SelectedItem == null)
            {
                MessageBox.Show(Strings.Settings_Error_NoTemplateSelected,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (cboTemplate.Text.Contains("<"))
            {
                MessageBox.Show(Strings.Settings_Error_TemplateCantBeDeleted,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            //Settings.Default.ImportSettingsTemplates.Remove(cboTemplate.SelectedItem);
            cboTemplate.Items.Remove(cboTemplate.SelectedItem);
        }

        /// <summary>
        ///     Gets called when the CreateRelationships-checkbox is (un)checked.
        ///     Updates the user interface.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void chkCreateRelationships_CheckedChanged(object sender, EventArgs e)
        {
            chkCreateAssociations.Enabled = chkCreateRelationships.Checked;
            chkLabelAssociations.Enabled = chkCreateRelationships.Checked && chkCreateAssociations.Checked;
            chkRemoveFields.Enabled = chkCreateRelationships.Checked && chkCreateAssociations.Checked;
            chkCreateGeneralizations.Enabled = chkCreateRelationships.Checked;
            chkCreateRealizations.Enabled = chkCreateRelationships.Checked;
            chkCreateNesting.Enabled = chkCreateRelationships.Checked;
        }

        /// <summary>
        ///     Gets called when the CreateAggregations-checkbox is (un)checked.
        ///     Updates the user interface.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void chkCreateAssociations_CheckedChanged(object sender, EventArgs e)
        {
            chkLabelAssociations.Enabled = chkCreateRelationships.Checked && chkCreateAssociations.Checked;
            chkRemoveFields.Enabled = chkCreateRelationships.Checked && chkCreateAssociations.Checked;
        }

        /// <summary>
        ///     Gets called when the data displayed at the filter list changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void dgvFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var row = dgvFilter.Rows[e.RowIndex];
            var modifier = row.Cells[0].Value as ImageComboBoxItem;
            var element = row.Cells[1].Value as ImageComboBoxItem;
            if (modifier != null && element != null)
            {
                if (element.Tag == null)
                {
                    // A modifier was just selected...
                    row.Cells[1].Value = colFilterElement.Items[0];
                }
                if (modifier.Tag == null)
                {
                    // An element was just selected...
                    row.Cells[0].Value = colFilterModifier.Items[0];
                }
            }
        }

        /// <summary>
        ///     Gets called when the dirty state of a cell changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void dgvFilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgvFilter.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        ///     Gets called when the Add files-button is clicked. Display a dialogbox to select files by user.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdAddFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialogAssemblies.ShowDialog() == DialogResult.OK)
            {
                // Add entries into the list
                foreach (var file in openFileDialogAssemblies.FileNames)
                    AddFile(file);
            }
        }

        /// <summary>
        ///     Gets called when the Add folder-button is clicked. Display a dialogbox to select a folder by user.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdAddFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                AddFolder(folderBrowserDialog.SelectedPath);
        }

        /// <summary>
        ///     Gets called when the Delete-button is clicked. Deletes the items selected in the list.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void cmdDeleteFileFolder_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex == -1)
                return;

            var selectedItems = new ListBox.SelectedObjectCollection(lbEntries);
            selectedItems = lbEntries.SelectedItems;

            // Delete all items selected
            for (var i = selectedItems.Count - 1; i >= 0; i--)
                lbEntries.Items.Remove(selectedItems[i]);
        }

        #endregion

        #region === Methods

        /// <summary>
        ///     Displays the given Settings.
        /// </summary>
        /// <param name="settings">The Settings which shall be displayed.</param>
        private void DisplaySettings(ImportSettings settings)
        {
            lbEntries.Items.Clear();

            if (settings.Items != null)
            {
                if (settings.Items.Count != 0)
                {
                    foreach (var item in settings.Items)
                    {
                        // Check entries
                        AddFile(item);
                        AddFolder(item);
                    }

                    CopyItemsFromListToSettings(ref settings);
                }
            }

            chkNewDiagram.Checked = settings.NewDiagram;

            dgvFilter.Rows.Clear();

            rdoWhiteList.Checked = settings.UseAsWhiteList;
            rdoBlackList.Checked = !settings.UseAsWhiteList;
            if (settings.FilterRules != null)
            {
                foreach (var filterRule in settings.FilterRules)
                {
                    dgvFilter.Rows.Add(reverseModifierNameMap[filterRule.Modifier],
                                       reverseElementNameMap[filterRule.Element]);
                }
            }

            chkCreateRelationships.Checked = settings.CreateRelationships;
            chkCreateAssociations.Checked = settings.CreateAssociations;
            chkLabelAssociations.Checked = settings.LabelAggregations;
            chkRemoveFields.Checked = settings.RemoveFields;
            chkCreateGeneralizations.Checked = settings.CreateGeneralizations;
            chkCreateRealizations.Checked = settings.CreateRealizations;
            chkCreateNesting.Checked = settings.CreateNestings;
        }

        /// <summary>
        ///     Stores the displayed settings to <paramref name="settings" />
        /// </summary>
        /// <param name="settings">The destination of the store operation.</param>
        private void StoreSettings(ImportSettings settings)
        {
            CopyItemsFromListToSettings(ref settings);

            settings.NewDiagram = chkNewDiagram.Checked;

            settings.UseAsWhiteList = rdoWhiteList.Checked;
            var filterRules = new List<FilterRule>(dgvFilter.Rows.Count);
            foreach (DataGridViewRow row in dgvFilter.Rows)
            {
                var modifier = row.Cells[0].Value as ImageComboBoxItem;
                var element = row.Cells[1].Value as ImageComboBoxItem;
                if (modifier != null && element != null)
                {
                    if (modifier.Tag is FilterModifiers && element.Tag is FilterElements)
                    {
                        filterRules.Add(new FilterRule((FilterModifiers) modifier.Tag, (FilterElements) element.Tag));
                    }
                }
            }
            settings.FilterRules = filterRules;

            settings.CreateRelationships = chkCreateRelationships.Checked;
            settings.CreateAssociations = chkCreateAssociations.Checked;
            settings.LabelAggregations = chkLabelAssociations.Checked;
            settings.RemoveFields = chkRemoveFields.Checked;
            settings.CreateGeneralizations = chkCreateGeneralizations.Checked;
            settings.CreateRealizations = chkCreateRealizations.Checked;
            settings.CreateNestings = chkCreateNesting.Checked;
        }

        /// <summary>
        ///     Check if a parent directory in the pathName is already present in the list.
        /// </summary>
        /// <param name="pathName">pathName to check.</param>
        /// <param name="isFile">if pathName is a file to check.</param>
        /// <returns>A parent folder who is already present in the list or an empty string.</returns>
        private string IsParentFolderAlreadyExits(string pathName, bool isFile)
        {
            if (string.IsNullOrEmpty(pathName))
                return string.Empty;

            var pathRoot = Path.GetPathRoot(pathName);

            if (string.IsNullOrEmpty(pathRoot))
                return string.Empty;

            // Check all parent folders
            var parentFolder = string.Empty;
            var path = pathName;
            do
            {
                parentFolder = Path.GetDirectoryName(path);

                // Check List
                foreach (string item in lbEntries.Items)
                {
                    // When checking File
                    if (isFile)
                    {
                        // if it is a file == false
                        // Not checking file vs file
                        if (string.IsNullOrEmpty(Path.GetExtension(item)) == false)
                            continue;
                    }
                    // When checking folder
                    else
                    {
                        // If it is a file == true
                        // Not Checking a folder with a file
                        if (string.IsNullOrEmpty(Path.GetExtension(item)))
                            continue;
                    }

                    if (parentFolder == item)
                        return item;
                }

                path = parentFolder;
            } while (pathRoot != parentFolder);

            return string.Empty;
        }

        /// <summary>
        ///     Add a C# source code file or an .NET assembly (.exe and .dll) into the listbox.
        /// </summary>
        /// <param name="fileName">fileName to add.</param>
        private void AddFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var extension = Path.GetExtension(fileName);
            // Only for C# file
            if (extension == ".cs")
            {
                // Don't add the same files multiple times
                if (lbEntries.Items.Contains(fileName) == false)
                {
                    // Don't include a C# source code file if a parent folder is alreday present
                    var parentFolder = IsParentFolderAlreadyExits(fileName, true);
                    if (string.IsNullOrEmpty(parentFolder))
                        lbEntries.Items.Add(fileName);
                    else
                        MessageBox.Show(string.Format(Strings.Settings_Error_AddCSharpFile, fileName, parentFolder),
                                        Strings.Error_MessageBoxTitle,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
            }
            // For .exe and .dll assemblies and Visual Studio files
            else if (extension == ".exe" || extension == ".dll" || extension == ".sln" || extension == ".csproj")
            {
                // Don't add the same files multiple times
                if (lbEntries.Items.Contains(fileName) == false)
                    lbEntries.Items.Add(fileName);
            }
        }

        /// <summary>
        ///     Add a folder into the listbox.
        /// </summary>
        /// <param name="folderName">folderName to add.</param>
        private void AddFolder(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return;

            // Only for folder
            if (string.IsNullOrEmpty(Path.GetExtension(folderName)) == false)
                return;

            // Don't add the same folder multiple times
            if (lbEntries.Items.Contains(folderName) == false)
            {
                var parentFolder = IsParentFolderAlreadyExits(folderName, false);

                // Don't include a subfolder if parent folder is already present
                if (string.IsNullOrEmpty(parentFolder))
                {
                    // Remove all subfolders and C# source code items if it is a parent folder included
                    for (var i = 0; i < lbEntries.Items.Count; i++)
                    {
                        var item = lbEntries.Items[i].ToString();
                        var extension = Path.GetExtension(item);
                        var path = Path.GetDirectoryName(item);

                        if (string.IsNullOrWhiteSpace(path))
                            continue;

                        // A C# code source file or a directory
                        if (extension != ".cs" && string.IsNullOrEmpty(extension) == false)
                            continue;

                        // Is it a file in a parent folder added?
                        if (path.StartsWith(folderName) == false)
                            continue;

                        // This file is under the folderName added
                        lbEntries.Items.RemoveAt(i);
                        i--;
                    }
                    lbEntries.Items.Add(folderName);
                }
                else
                    MessageBox.Show(string.Format(Strings.Settings_Error_AddSubFolder, folderName, parentFolder),
                                    Strings.Error_MessageBoxTitle,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }


        /// <summary>
        ///     Copy items froml ListBox to Settigns.
        /// </summary>
        /// <param name="settings">settings to memorize.</param>
        private void CopyItemsFromListToSettings(ref ImportSettings settings)
        {
            if (lbEntries.Items == null)
                return;

            if (lbEntries.Items.Count == 0)
                return;

            var items = new List<string>(lbEntries.Items.Count);

            foreach (string itemName in lbEntries.Items)
                items.Add(itemName);

            settings.Items = items;
        }

        #endregion
    }
}