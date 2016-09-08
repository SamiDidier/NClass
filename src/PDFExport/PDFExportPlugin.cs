using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using NClass.GUI;
using PdfSharp.Drawing;
using PDFExport.Lang;
using PDFExport.Properties;
using Settings = NClass.GUI.Settings;

namespace PDFExport
{
    /// <summary>
    ///     A plugin to export a diagram to a pdf.
    /// </summary>
    public class PDFExportPlugin : Plugin
    {
        // ========================================================================
        // Attributes

        #region === Attributes

        /// <summary>
        ///     The menu item used to start the export.
        /// </summary>
        private readonly ToolStripMenuItem menuItem;

        #endregion

        // ========================================================================
        // Event handling

        #region === Event handling

        /// <summary>
        ///     Starts the export.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Additional information.</param>
        private void menuItem_Click(object sender, EventArgs e)
        {
            Launch();
        }

        #endregion

        // ========================================================================
        // Methods

        #region === Methods

        /// <summary>
        ///     Starts the functionality of the plugin.
        /// </summary>
        protected void Launch()
        {
            if (!DocumentManager.HasDocument)
            {
                return;
            }

            string fileName;
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = Strings.SaveDialogFilter;
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                fileName = dialog.FileName;
            }

            var optionsForm = new PDFExportOptions();
            if (optionsForm.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            var padding = new Padding((int) new XUnit(optionsForm.PDFPadding.Left, optionsForm.Unit).Point,
                                      (int) new XUnit(optionsForm.PDFPadding.Top, optionsForm.Unit).Point,
                                      (int) new XUnit(optionsForm.PDFPadding.Right, optionsForm.Unit).Point,
                                      (int) new XUnit(optionsForm.PDFPadding.Bottom, optionsForm.Unit).Point);

            var mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();

            PDFExportProgress.ShowAsync(mainForm);

            var exporter = new PDFExporter(fileName, DocumentManager.ActiveDocument, optionsForm.SelectedOnly, padding);
            Application.DoEvents();
            exporter.Export();

            // Running the exporter within an extra thread isn't a good idea since
            // the exporter uses GDI+. NClass uses GDI+ also if it has to redraw the
            // diagram. GDI+ can't be used by two threads at the same time.
            //Thread exportThread = new Thread(exporter.Export)
            //                        {
            //                          Name = "PDFExporterThread",
            //                          IsBackground = true
            //                        };
            //exportThread.Start();
            //while (exportThread.IsAlive)
            //{
            //  Application.DoEvents();
            //  exportThread.Join(5);
            //}
            //exportThread.Join();

            PDFExportProgress.CloseAsync();

            if (exporter.Successful)
            {
                if (new PDFExportFinished().ShowDialog(mainForm) == DialogResult.OK)
                {
                    Process.Start(fileName);
                }
            }
        }

        #endregion

        // ========================================================================
        // Con- / Destruction

        #region === Con- / Destruction

        /// <summary>
        ///     Set up the current culture for the strings.
        /// </summary>
        static PDFExportPlugin()
        {
            try
            {
                Strings.Culture = CultureInfo.GetCultureInfo(Settings.Default.UILanguage);
            }
            catch (ArgumentException)
            {
                //Culture is not supported, maybe the setting is "default".
            }
        }

        /// <summary>
        ///     Constructs a new instance of PDFExportPlugin.
        /// </summary>
        /// <param name="environment">An instance of NClassEnvironment.</param>
        public PDFExportPlugin(NClassEnvironment environment)
            : base(environment)
        {
            menuItem = new ToolStripMenuItem
            {
                Text = Strings.Menu_Title,
                Image = Resources.Document_pdf_16,
                ToolTipText = Strings.Menu_ToolTip
            };
            menuItem.Click += menuItem_Click;
        }

        #endregion

        // ========================================================================
        // Properties

        #region === Properties

        /// <summary>
        ///     Gets a value indicating whether the plugin can be executed at the moment.
        /// </summary>
        public override bool IsAvailable { get { return DocumentManager.HasDocument; } }

        /// <summary>
        ///     Gets the menu item used to start the plugin.
        /// </summary>
        public override ToolStripItem MenuItem { get { return menuItem; } }

        #endregion
    }
}