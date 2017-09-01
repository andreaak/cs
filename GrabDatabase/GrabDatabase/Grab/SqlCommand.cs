
using System.Data;
using System;
using Utils;
using System.IO;
using DevExpress.XtraEditors;
using System.Text;
using Utils.ActionWindow;
using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using Utils.WorkWithDB;
using DevExpress.XtraGrid.Views.Base;

namespace GrabDatabase.Grab
{
    public partial class SqlCommand : DevExpress.XtraEditors.XtraForm
    {
        private const string SEPARATOR = ";;";
        private const int DEFAULT_ROW_COUNT = 100;
        private DBService dbService;
        private HelpProvider help;
        private const string SQL_QUERY = "Sql Query ";
        private const string ID_COLUMN = "ID";
        private DbStructureDataset dbStructure;

        public bool IsTableSelect
        {
            get
            {
                return (DBObjects)toolStripComboBox1.SelectedItem == DBObjects.Tables;
            }
        }

        public SqlCommand(DBService dbService, DbStructureDataset dbStructure)
        {
            InitializeComponent();
            this.dbService = dbService;
            help = new HelpProvider();
            toolStripTextBoxMaxItemsCount.Text = 100.ToString();
            this.toolStripComboBox1.SelectedIndexChanged -= new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            toolStripComboBox1.Items.Add(DBObjects.Tables);
            toolStripComboBox1.Items.Add(DBObjects.Views);
            toolStripComboBox1.Items.Add(DBObjects.Triggers);
            toolStripComboBox1.SelectedItem = DBObjects.Tables;
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.dbStructure = dbStructure;
            toolStripComboBox1_SelectedIndexChanged(null, null);
            Text = SQL_QUERY + dbService.GetConnectionDescription();
        }

        #region HANDLERS

        private void toolStripTextBoxMaxItemsCount_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBoxMaxItemsCount.Text))
            {
                return;
            }
            int res;
            if (!int.TryParse(toolStripTextBoxMaxItemsCount.Text, out res))
            {
                StringBuilder sb = new StringBuilder();
                foreach (char ch in toolStripTextBoxMaxItemsCount.Text)
                {
                    if (char.IsDigit(ch))
                    {
                        sb.Append(ch);
                    }
                }
                toolStripTextBoxMaxItemsCount.Text = sb.ToString();
            }
        }

        private void memoEdit1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                string query = ParseQueryFromMemo((MemoEdit)sender);
                ExecuteQuery(query);
                e.Handled = true;
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dbStructure == null)
            {
                return;
            }
            gridViewDbEntity.Columns.Clear();
            gridControlDbEntity.DataSource = dbStructure;
            DBObjects selItem = (DBObjects)toolStripComboBox1.SelectedItem;
            switch (selItem)
            {
                case DBObjects.Tables:
                    TuneView(selItem, dbStructure.Table.TableName);
                    break;
                case DBObjects.Views:
                    TuneView(selItem, dbStructure.Views.TableName);
                    break;
                case DBObjects.Triggers:
                    TuneView(selItem, dbStructure.Triggers.TableName);
                    break;
            }
        }

        private void gridControl1_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            GridView view = e.View as GridView;

            if (view != null && view.GetViewCaption() == "Table_Column")
            {
                HideColumns(view, dbService.GetVisibleColumns(DBObjects.Columns));
                view.OptionsSelection.MultiSelect = true;
                view.OptionsBehavior.Editable = false;
                view.OptionsBehavior.ReadOnly = true;
                view.PopupMenuShowing += view_PopupMenuShowing;
                GridColumn column = view.Columns.ColumnByFieldName(ID_COLUMN);
                if (column != null)
                {
                    column.SortMode = ColumnSortMode.Custom;
                    view.CustomColumnSort -= gridView_CustomColumnSort;
                    view.CustomColumnSort += gridView_CustomColumnSort;
                }

            }
        }

        private void view_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row && IsTableSelect)
            {
                int rowHandle = e.HitInfo.RowHandle;
                e.Menu.Items.Clear();
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Insert 'Select' Query", InsertQuery, DBConstants.DbCommand.select));
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Insert 'Insert' Query", InsertQuery, DBConstants.DbCommand.insert));
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Insert 'Update' Query", InsertQuery, DBConstants.DbCommand.update));
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Insert 'Delete' Query", InsertQuery, DBConstants.DbCommand.delete));
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Insert 'Drop' Query", InsertQuery, DBConstants.DbCommand.drop));
                DXMenuItem item = CreateRowMenuItem(view, rowHandle, "Select", ExecuteQuery, DBConstants.DbCommand.select);
                item.BeginGroup = true;
                e.Menu.Items.Add(item);
                e.Menu.Items.Add(CreateRowMenuItem(view, rowHandle, "Drop", ExecuteQuery, DBConstants.DbCommand.drop));
            }
        }

        private void InsertQuery(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            RowInfo info = item.Tag as RowInfo;
            string query = GetQuery(info);
            if (!string.IsNullOrEmpty(query))
            {
                InsertQueryInMemo(query);
            }
        }

        private void ExecuteQuery(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            RowInfo info = item.Tag as RowInfo;
            string query = GetQuery(info);
            if (!string.IsNullOrEmpty(query))
            {
                ExecuteQuery(query);
            }
        }

        private void gridView_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != null && e.Column.FieldName == ID_COLUMN)
            {
                DataRowView dr1 = (view.DataSource as DataView)[e.ListSourceRowIndex1];
                DataRowView dr2 = (view.DataSource as DataView)[e.ListSourceRowIndex2];
                e.Handled = true;
                int a, b;
                if (int.TryParse(dr1.Row.Field<string>(ID_COLUMN), out a) && int.TryParse(dr2.Row.Field<string>(ID_COLUMN), out b))
                {
                    e.Result = a.CompareTo(b);
                }
            }
        }

        private void memoEdit1_BeforeShowMenu(object sender, DevExpress.XtraEditors.Controls.BeforeShowMenuEventArgs e)
        {
            String caption = "Select template";
            foreach (DevExpress.Utils.Menu.DXMenuItem item in e.Menu.Items)
            {
                if (item.Caption == caption)
                {
                    return;
                }
            }
            DXMenuItem menuItem = CreateRowMenuItem(null, 0, "Select template", InsertTemplate, template: DBConstants.SELECT_TEMPLATE);
            menuItem.BeginGroup = true;
            e.Menu.Items.Add(menuItem);
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Inner Join template", InsertTemplate, template: DBConstants.INNER_JOIN_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Left/Right Join template", InsertTemplate, template: DBConstants.LEFT_RIGHT_JOIN_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Insert template", InsertTemplate, template: DBConstants.INSERT_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Update template", InsertTemplate, template: DBConstants.UPDATE_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Delete template", InsertTemplate, template: DBConstants.DELETE_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Add Column template", InsertTemplate, template: DBConstants.ADD_COLUMN_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Drop Column template", InsertTemplate, template: DBConstants.DROP_COLUMN_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Modify Column template", InsertTemplate, template: DBConstants.MODIFY_COLUMN_TEMPLATE));
            e.Menu.Items.Add(CreateRowMenuItem(null, 0, "Create Table template", InsertTemplate, template: DBConstants.CREATE_TABLE_TEMPLATE));
        }

        private void InsertTemplate(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            RowInfo info = item.Tag as RowInfo;
            string query = info.Template;
            if (!string.IsNullOrEmpty(query))
            {
                InsertQueryInMemo(query);
            }
        }

        #endregion

        #region BUTTON HANDLERS

        private void toolStripButtonOpenSql_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
                "SQL Files (*.sql)|*.sql|",
                "All Files (*.*)|*.*"
            };
            string title = "Load Data";
            string filePath = string.Empty;
            if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                StreamReader reader = File.OpenText(filePath);
                memoEdit1.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void toolStripButtonSaveSql_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
                "SQL Files (*.sql)|*.sql|",
                "All Files (*.*)|*.*"
            };
            string title = "Save Data";
            string filePath = string.Empty;
            if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                StreamWriter writer = File.CreateText(filePath);
                writer.Write(memoEdit1.Text);
                writer.Flush();
                writer.Close();
            }
        }

        private void toolStripButtonSelectQueryHelp_Click(object sender, EventArgs e)
        {
            RtfForm form = new RtfForm(dbService.DatabaseType, DBConstants.DbCommand.select);
            form.Show();
        }

        private void toolStripButtonInsertQueryHelp_Click(object sender, EventArgs e)
        {
            RtfForm form = new RtfForm(dbService.DatabaseType, DBConstants.DbCommand.insert);
            form.Show();
        }

        private void toolStripButtonUpdateQueryHelp_Click(object sender, EventArgs e)
        {
            RtfForm form = new RtfForm(dbService.DatabaseType, DBConstants.DbCommand.update);
            form.Show();
        }

        private void toolStripButtonDeleteQueryHelp_Click(object sender, EventArgs e)
        {
            RtfForm form = new RtfForm(dbService.DatabaseType, DBConstants.DbCommand.delete);
            form.Show();
        }

        private void toolStripButtonUpdateDbData_Click(object sender, EventArgs e)
        {
            DbStructureDataset ds = UTILS.ReadDbStructure(dbService);
            if (ds != null)
            {
                dbStructure = ds;
                toolStripComboBox1_SelectedIndexChanged(null, null);
            }
        }

        private void toolStripButtonExecute_Click(object sender, EventArgs e)
        {
            string query;
            if (memoEdit1.SelectionLength > 0 )
            {
                query = memoEdit1.SelectedText;
            }
            else
            {
                query = ParseQueryFromMemo(memoEdit1);
            }
            ExecuteQuery(query);
        }

        #endregion

        private static string ParseQueryFromMemo(MemoEdit edit)
        {
            int endPos = edit.Text.IndexOf(SEPARATOR, edit.SelectionStart);
            int startPos;
            if (endPos == -1)
            {
                startPos = edit.Text.LastIndexOf(SEPARATOR) + 1;
                endPos = edit.Text.Length - 1;
            }
            else
            {
                startPos = edit.Text.Remove(endPos).LastIndexOf(SEPARATOR) + 1;
                --endPos; 
            }

            if(startPos != 0)
            {
                ++startPos;
            }
            return edit.Text.Substring(startPos, endPos - startPos + 1).Trim();
        }

        private void InsertQueryInMemo(string query)
        {
            int index = memoEdit1.SelectionStart;
            query = query + "; ";
            memoEdit1.Text = memoEdit1.Text.Insert(index, query);
            memoEdit1.SelectionStart = index + query.Length;
        }

        private void TuneView(DBObjects selItem, string dataMember)
        {
            gridControlDbEntity.DataMember = dataMember;
            HideColumns(gridViewDbEntity, dbService.GetVisibleColumns(selItem));
        }

        private void HideColumns(GridView view, IEnumerable<string> visibleColumns)
        {
            view.PopulateColumns();
            foreach (GridColumn column in view.Columns)
            {
                if (!visibleColumns.Contains(column.FieldName))
                {
                    column.Visible = false;
                }
            }
        }

        DXMenuItem CreateRowMenuItem(GridView view, int rowHandle, string caption, EventHandler handler
            , DBConstants.DbCommand command = DBConstants.DbCommand.select, string template = null)
        {
            DXMenuItem menuItem = new DXMenuItem(caption, handler);
            menuItem.Tag = new RowInfo(view, rowHandle, command, template);
            return menuItem;
        }

        #region QUERY

        private void ExecuteQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return;
            }
            string typeStr = query.Substring(0, query.IndexOf(' '));
            try
            {
                DBConstants.DbCommand type = (DBConstants.DbCommand)Enum.Parse(typeof(DBConstants.DbCommand), typeStr, true);
                switch (type)
                {
                    case DBConstants.DbCommand.select:
                        DataSet dataset = dbService.SelectQuery(GetRowNumberRestrictionQuery(query));
                        UTILS.BindDataSet(dataset, gridControlResult, gridViewResult);
                        break;
                    case DBConstants.DbCommand.drop:
                        if (DisplayMessage.ShowWarningYesNO(DisplayMessage.DROP_TABLE_CONST) == System.Windows.Forms.DialogResult.Yes)
                        {
                            dbService.CRUDQuery(query);
                            DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
                        }
                        break;
                    case DBConstants.DbCommand.insert:
                    case DBConstants.DbCommand.update:
                    case DBConstants.DbCommand.delete:
                    default:
                        dbService.CRUDQuery(query);
                        DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }

        }

        private string GetRowNumberRestrictionQuery(string query)
        {
            if (string.IsNullOrEmpty(toolStripTextBoxMaxItemsCount.Text))
            {
                return query;
            }

            string template = dbService.RowRestrictionQuery;

            return string.Format(template, query, toolStripTextBoxMaxItemsCount.Text);
        }

        private string GetQuery(RowInfo info)
        {
            switch (info.Command)
            {
                case DBConstants.DbCommand.select:
                    return GetSelectQuery(info);
                case DBConstants.DbCommand.delete:
                    return GetDeleteQuery(info);
                case DBConstants.DbCommand.drop:
                    return GetDropQuery(info);
                case DBConstants.DbCommand.insert:
                    return GetInsertQuery(info);
                case DBConstants.DbCommand.update:
                    return GetUpdateQuery(info);
                case DBConstants.DbCommand.alter:
                case DBConstants.DbCommand.create:
                default:
                    return GetSelectQuery(info);
            }
        }

        private string GetSelectQuery(RowInfo info)
        {
            string query = null;
            if (IsTableSelect)
            {
                DataRow tableRow = info.View.IsDetailView ? (info.View.SourceRow as DataRowView).Row : info.View.GetDataRow(info.RowHandle);
                string tableName = tableRow.Field<string>("TABLE_NAME");
                List<string> columnRows = new List<string>();
                if (info.View.IsDetailView)
                {
                    foreach (int rowHandle in info.View.GetSelectedRows())
                    {
                        columnRows.Add(string.Format("[{0}]", info.View.GetDataRow(rowHandle).Field<string>("COLUMN_NAME")));
                    }
                }
                string columns = columnRows.Count == 0 ? "*" : string.Join(", ", columnRows.ToArray());
                query = string.Format("SELECT {0} FROM [{1}]", columns, tableName);
            }
            return query;
        }  

        private string GetDeleteQuery(RowInfo info)
        {
            string query = null;
            if (IsTableSelect)
            {
                DataRow tableRow = info.View.IsDetailView ? (info.View.SourceRow as DataRowView).Row : info.View.GetDataRow(info.RowHandle);
                string tableName = tableRow.Field<string>("TABLE_NAME");
                query = string.Format("DELETE FROM [{0}] WHERE ?", tableName);
            }
            return query;
        }  
    
        private string GetDropQuery(RowInfo info)
        {
            string query = null;
            if (IsTableSelect)
            {
                DataRow tableRow = info.View.IsDetailView ? (info.View.SourceRow as DataRowView).Row : info.View.GetDataRow(info.RowHandle);
                string tableName = tableRow.Field<string>("TABLE_NAME");
                query = string.Format("DROP TABLE [{0}]", tableName);
            }
            return query;
        }

        private string GetUpdateQuery(RowInfo info)
        {
            string query = null;
            if (IsTableSelect)
            {
                DataRow tableRow = info.View.IsDetailView ? (info.View.SourceRow as DataRowView).Row : info.View.GetDataRow(info.RowHandle);
                string tableName = tableRow.Field<string>("TABLE_NAME");
                List<string> columnRows = new List<string>();
                foreach (string column in Columns(info.View, info))
                {
                    columnRows.Add(string.Format("[{0}] = ?", column));
                }
                string columns = string.Join(", ", columnRows.ToArray());
                query = string.Format("UPDATE [{0}] SET {1} WHERE ?", tableName, columns);
            }
            return query;
        }

        private string GetInsertQuery(RowInfo info)
        {
            string query = null;
            if (IsTableSelect)
            {
                DataRow tableRow = info.View.IsDetailView ? (info.View.SourceRow as DataRowView).Row : info.View.GetDataRow(info.RowHandle);
                string tableName = tableRow.Field<string>("TABLE_NAME");
                List<string> columnRows = new List<string>();
                
                foreach (string column in Columns(info.View, info))
	            {
		            columnRows.Add(string.Format("[{0}]", column));
	            }
                IEnumerable<string> valuesRows = Enumerable.Repeat<string>("?", columnRows.Count);
                string columns = string.Join(", ", columnRows.ToArray());
                string values = string.Join(", ", valuesRows);
                query = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", tableName, columns, values);
            }
            return query;
        }

        private IEnumerable<string> Columns(GridView view, RowInfo info)
        {
            int sourceRowHandle = info.View.IsDetailView ? info.View.SourceRowHandle : info.RowHandle;
            if (view.IsDetailView)
            {
                foreach (int rowHandle in view.GetSelectedRows())
                {
                    yield return view.GetDataRow(rowHandle).Field<string>("COLUMN_NAME");
                }
            }
            else
            {
                view.SetMasterRowExpanded(sourceRowHandle, true);
                BaseView detailView = view.GetDetailView(sourceRowHandle, 0);
                for (int row = 0; row < detailView.RowCount; row++)
                {
                    yield return (detailView.GetRow(row) as DataRowView).Row.Field<string>("COLUMN_NAME");
                }
            }
        }
        
        #endregion

        private class RowInfo
        {
            public RowInfo(GridView view, int rowHandle, DBConstants.DbCommand command, string template)
            {
                this.RowHandle = rowHandle;
                this.View = view;
                this.Command = command;
                this.Template = template;
            }
            public GridView View
            {
                get;
                set;
            }
            public int RowHandle
            {
                get;
                set;
            }
            public DBConstants.DbCommand Command
            {
                get;
                set;
            }

            public string Template
            {
                get;
                set;
            }
        }


    }
}