﻿using System.ComponentModel;
using System.Windows.Forms;
using NClass.DiagramEditor;

namespace NClass.GUI
{
    public partial class TabbedWindow : UserControl
    {
        private DocumentManager docManager;

        public TabbedWindow()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public DocumentManager DocumentManager
        {
            get { return docManager; }
            set
            {
                if (docManager != value)
                {
                    docManager = value;

                    if (docManager != null)
                        docManager.ActiveDocumentChanged -= docManager_ActiveDocumentChanged;
                    docManager = value;

                    if (docManager != null)
                    {
                        docManager.ActiveDocumentChanged += docManager_ActiveDocumentChanged;
                        Canvas.Document = docManager.ActiveDocument;
                    }
                    else
                    {
                        Canvas.Document = null;
                    }
                    TabBar.DocumentManager = value;
                }
            }
        }

        [Browsable(false)]
        public TabBar TabBar { get; private set; }

        [Browsable(false)]
        public Canvas Canvas { get; private set; }

        private void docManager_ActiveDocumentChanged(object sender, DocumentEventArgs e)
        {
            Canvas.Document = docManager.ActiveDocument;
        }
    }
}