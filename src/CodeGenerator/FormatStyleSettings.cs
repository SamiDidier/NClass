using System;
using System.Windows.Forms;

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
            var formatStyle = FormattingOptionsFactoryUI.CreateEmpty();
            propertyGridFormatStyle.SelectedObject = formatStyle;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}