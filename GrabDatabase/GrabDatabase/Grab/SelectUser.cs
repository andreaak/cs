using System.Data;

namespace GrabDatabase.Grab
{
    public partial class SelectUser : DevExpress.XtraEditors.XtraForm
    {
        public string User
        {
            get { return comboBoxEdit1.Text; }
        }
        
        public SelectUser(DataSet ds)
        {
            InitializeComponent();
            foreach (DataRow user in ds.Tables["Users"].Rows)
	        {
                if (ds.Tables["Users"].Columns.Contains("NAME"))
                {
                    comboBoxEdit1.Properties.Items.Add(user["NAME"]);
                }
                else if (ds.Tables["Users"].Columns.Contains("user_name"))
                {
                    comboBoxEdit1.Properties.Items.Add(user["user_name"]);
                }
	        }
        }
    }
}