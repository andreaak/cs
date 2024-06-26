﻿using System;
using System.Windows.Forms;
using Patterns._01_Creational.FactoryMethod._003_Application.Creator.ConcreteCreator;
using Patterns._01_Creational.FactoryMethod._003_Application.Product;
using Application = Patterns._01_Creational.FactoryMethod._003_Application.Creator.Application;

namespace Patterns._01_Creational.FactoryMethod._003_Application
{
    public partial class BaseForm : Form
    {
        Application application;
        Document document;
        int counter = 0;

        public BaseForm()
        {
            InitializeComponent();
        }

        private void MakeDocument(DocType docType, Action action)
        {
            if (docType == DocType.TextDoc)
                application = new TextApplication(this, ++counter, action);
            else
                application = new PngApplication(this, ++counter, action);

            if (action == Action.NewDoc)
                document = application.CreateDocument();
            else
                application.OpenDocument();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeDocument(DocType.TextDoc, Action.NewDoc);
        }

        private void pngDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeDocument(DocType.PngDoc, Action.NewDoc);
        }

        private void pngDocumentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MakeDocument(DocType.PngDoc, Action.OpenDoc);
        }

        private void textDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeDocument(DocType.TextDoc, Action.OpenDoc);
        }
    }
}
