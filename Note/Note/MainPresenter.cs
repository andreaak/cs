using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DataManager;
using DataManager.Repository;
using Note.ControlWrapper.Binding;
using Utils.ActionWindow;
using Utils.WorkWithDB.Wrappers;

namespace Note
{
    public class MainPresenter
    {
        private const string ConnectionErrorMessage = "Connection is absent!";

        private IMainView view;

        public DatabaseManager DataManager
        {
            set;
            get;
        }

        public IEnumerable<Description> Descriptions
        {
            get
            {
                return DataManager.Descriptions;
            }
        }

        public IEnumerable<TextData> Texts
        {
            get
            {
                return DataManager.Texts;
            }
        }

        public IList<object> Updates
        {
            get
            {
                return DataManager.Updates;
            }
        }

        public MainPresenter(IMainView view)
        {
            this.view = view;
        }

        public void Init()
        {
            DataManager.Init();
        }

        public bool Connect()
        {
            bool dbConnectionOk = false;
            bool showMessage = true;
            string errorMessage = null;
            try
            {
                DialogResult res = new DBDataForm().ShowDialog(view.Form);
                if (res != DialogResult.OK)
                {
                    return false;
                }
                DataManager = new DatabaseManager();
                if (DataManager.IsDBOnline())
                {
                    dbConnectionOk = DataManager.IsProperDB || DataManager.CreateDb();
                }
            }
            catch (DbFileNotExistException)
            {
                dbConnectionOk = DataManager.CreateDb();
            }
            catch (OperationCanceledException)
            {
                if (DataManager == null)
                {
                    showMessage = false;
                }
                else
                {
                    dbConnectionOk = true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            if (!dbConnectionOk)
            {
                HandleError(showMessage, errorMessage);
            }
            return dbConnectionOk;
        }

        public string GetDBFileName()
        {
            return DataManager.GetDBFileName();
        }

        public string GetConnectionDescription()
        {
            return DataManager.GetConnectionDescription();
        }

        public long Insert(long parentId, string description, DataTypes type)
        {
            return DataManager.Insert(parentId, description, type);
        }

        public void Delete(long id)
        {
            DataManager.Delete(id);
        }

        public string GetTextData(long id)
        {
            return DataManager.GetTextData(id);
        }

        public bool UpdateTextData(long id, string editValue, string plainText)
        {
            return DataManager.UpdateTextData(id, editValue, plainText);
        }

        public bool UpdateDescription(long id, string description)
        {
            return DataManager.UpdateDescription(id, description);
        }

        public void VacuumDb(bool isSilent)
        {
            try
            {
                DataManager.VacuumDb();
                DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        public BindingDataset.DescriptionDataTable GetBindingTable()
        {
            BindingDataset.DescriptionDataTable table = new BindingDataset.DescriptionDataTable();
            IEnumerable<long> entityIds = Descriptions.Select(item => item.ID).ToList();
            foreach (var item in Descriptions.Where(item => entityIds.Contains(item.ID)))
            {
                table.AddDescriptionRow(
                    item.ID,
                    item.ParentID,
                    item.EditValue,
                    (byte)item.Type,
                    item.OrderPosition
                    );
            }
            table.AcceptChanges();
            return table;
        }

        public bool IsCanChangeLevel(int position, long parentId, Direction direction)
        {
            return DataManager.IsCanChangeLevel(position, parentId, direction);
        }

        public bool ChangeLevel(int position, long parentId, long id, Direction direction)
        {
            return DataManager.ChangeLevel(position, parentId, id, direction);
        }

        public bool IsCanMove(int position, long parentId, Direction direction)
        {
            return DataManager.IsCanMove(position, parentId, direction);
        }

        public bool Move(int position, long parentId, long id, Direction direction)
        {
            return DataManager.Move(position, parentId, id, direction);
        }

        public void CheckChangedItems()
        {
            DatabaseManager dataManagerLocal;
            if (TryGetDataManager(out dataManagerLocal))
            {
                IEnumerable<Tuple<Description, DataStatus>> descriptions = GetModifiedDescriptions(dataManagerLocal);
                ItemsList form = new ItemsList();
                form.LoadData(descriptions);
                form.Show();
            }
        }

        private void HandleError(bool showMessage, string errorMessage)
        {
            if (showMessage)
            {
                DisplayMessage.ShowError(errorMessage ?? ConnectionErrorMessage);
            }
        }

        private bool TryGetDataManager(out DatabaseManager dataManagerLocal)
        {
            bool dbConnectionOk = false;
            bool showMessage = true;
            string errorMessage = null;
            dataManagerLocal = null;
            try
            {
                DialogResult form = new DBDataForm().ShowDialog(view.Form);
                if (form != DialogResult.OK)
                {
                    return false;
                }
                dataManagerLocal = new DatabaseManager();
                if (dataManagerLocal.IsDBOnline())
                {
                    dbConnectionOk = true;
                }
            }
            catch (OperationCanceledException)
            {
                showMessage = false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            if (!dbConnectionOk)
            {
                HandleError(showMessage, errorMessage);
            }
            return dbConnectionOk;
        }

        private IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(DatabaseManager dataManagerLocal)
        {
            return DataManager.GetModifiedDescriptions(dataManagerLocal);
        }

    }
}
