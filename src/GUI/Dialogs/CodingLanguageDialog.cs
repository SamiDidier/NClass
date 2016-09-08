using System;
using System.Windows.Forms;
using NClass.Core;
using NClass.Translations;

namespace NClass.GUI.Dialogs
{
    public partial class CodingLanguageDialog : Form
    {
        private Language languageSelected;
        public Language LanguageSelected
        {
            get
            {
                return languageSelected;
            }
        }


        public CodingLanguageDialog()
        {
            InitializeComponent();

            // Display all coding languages available
            // listBoxCodingLanguages.Items.AddRange();

            // Select by default the first one if we have items
            if (listBoxCodingLanguages.Items.Count != 0)
            {
                btnOK.Enabled = false;
                listBoxCodingLanguages.Enabled = false;
                listBoxCodingLanguages.SelectedIndex = 0;
            }
            else
            {
                btnOK.Enabled = false;
                listBoxCodingLanguages.Enabled = false;
            }
        }

        private void UpdateTexts()
        {
            this.Text = Strings.ProgramingLanguage;
            btnOK.Text = Strings.ButtonOK;
            btnCancel.Text = Strings.ButtonCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBoxCodingLanguages.SelectedIndex == -1)
                return;

            //LanguageSelected = GetLanguageInstance();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateTexts();
        }
    }
}
