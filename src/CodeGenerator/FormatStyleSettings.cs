using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.NRefactory.CSharp;

namespace NClass.CodeGenerator
{
    public partial class FormatStyleSettings : Form
    {
        public FormatStyleSettings()
        {
            InitializeComponent();

            // TO DO : Add categries
            // http://msdn.microsoft.com/en-us/library/aa302326.aspx

            // Create the AppSettings class and display it in the PropertyGrid.
            CSharpFormattingOptionsUI formatStyle = FormattingOptionsFactoryUI.CreateEmpty();
            propertyGridFormatStyle.SelectedObject = formatStyle;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
