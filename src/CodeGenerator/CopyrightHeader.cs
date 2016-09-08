using System;
using System.Windows.Forms;
using NClass.Translations;

namespace NClass.CodeGenerator
{
    public partial class CopyrightHeader : Form
    {
        public CopyrightHeader()
        {
            InitializeComponent();
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            lblCompagnyName.Text = Strings.CompagnyName;
            lblAuthor.Text = Strings.AuthorName;
            lblCopyright.Text = Strings.CopyrightHeader;
            btnCancel.Text = Strings.ButtonCancel;
            btnOK.Text = Strings.ButtonOK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbCopyrightHeader.Text) == false)
                Settings.Default.CopyrightHeader = rtbCopyrightHeader.Text;
            else
                Settings.Default.CopyrightHeader = string.Empty;

            Settings.Default.CompagnyName = tbCompagnyName.Text;
            Settings.Default.Author = tbAuthor.Text;

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