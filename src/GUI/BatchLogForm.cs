using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NClass.GUI
{
    public partial class BatchLogForm : Form
    {
        public BatchLogForm()
        {
            InitializeComponent();
        }

        private void BatchLogForm_Load(object sender, EventArgs e)
        {
            TextBoxAppender.ConfigureTextBoxAppender(richTextBoxLog);
        }
    }
}
