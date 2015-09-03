using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Utils.WorkWithDB;
using Utils.WorkWithDB.Connections;

namespace GrabDatabase
{
    public partial class RtfForm : DevExpress.XtraEditors.XtraForm
    {
        HelpProvider help;
        public RtfForm()
        {
            InitializeComponent();
            help = new HelpProvider();
            comboBoxEditDatabase.Properties.Items.AddRange(Enum.GetValues(typeof(DataBaseType)));
            comboBoxEditCommand.Properties.Items.AddRange(Enum.GetValues(typeof(DBConstants.DbCommand)));
        }

        public RtfForm(DataBaseType databaseType, DBConstants.DbCommand commandType)
            :this()
        {
            comboBoxEditDatabase.EditValue = databaseType;
            comboBoxEditCommand.EditValue = commandType;
            simpleButtonSave.Enabled =  comboBoxEditCommand.Enabled = false;
        }

        private void comboBoxEditDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditDatabase.EditValue == null
                || comboBoxEditCommand.EditValue == null)
            {
                return;
            }

            richEditControl1.RtfText = help.GetHelp((DataBaseType)comboBoxEditDatabase.EditValue, (DBConstants.DbCommand)comboBoxEditCommand.EditValue);
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            help.SetHelp((DataBaseType)comboBoxEditDatabase.EditValue, (DBConstants.DbCommand)comboBoxEditCommand.EditValue, richEditControl1.RtfText);
        }
    }
}