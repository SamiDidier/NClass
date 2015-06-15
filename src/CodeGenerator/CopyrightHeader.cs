using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            if (String.IsNullOrWhiteSpace(rtbCopyrightHeader.Text) == false)
                Settings.Default.CopyrightHeader = rtbCopyrightHeader.Text;
            else
                Settings.Default.CopyrightHeader = string.Empty;

            Settings.Default.CompagnyName = tbCompagnyName.Text;
            Settings.Default.Author = tbAuthor.Text;

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
