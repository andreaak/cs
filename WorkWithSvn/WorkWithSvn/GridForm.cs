using System.Windows.Forms;
using WorkWithSvn.Utils;

namespace WorkWithSvn
{
    public partial class GridForm : Form
    {
        private const string ASC_SORT = "ASC";
        private const string DESC_SORT = "DESC";
        private const string SORT_TEMPLATE = "{0} {1}";
        
        public GridForm()
        {
            InitializeComponent();

        }

        public GridForm(CommitData logs)
        {
            InitializeComponent();
            bindingSource1 = new BindingSource();
            bindingSource1.DataSource = logs.COMMIT;
            dataGridView1.DataSource = bindingSource1;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (string.IsNullOrEmpty(bindingSource1.Sort)
                || bindingSource1.Sort.Contains(ASC_SORT))
            {
                bindingSource1.Sort = string.Format(SORT_TEMPLATE, dataGridView1.Columns[e.ColumnIndex].Name, DESC_SORT);
            }
            else
            {
                bindingSource1.Sort = string.Format(SORT_TEMPLATE, dataGridView1.Columns[e.ColumnIndex].Name, ASC_SORT); ;
            }
        }
    }
}
