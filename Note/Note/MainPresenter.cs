using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Note.ControlWrapper.Binding;
using Note.Domain;
using Note.Domain.Common;
using Note.Domain.Concrete;
using Note.Domain.Entities;
using Note.Properties;
using Utils.ActionWindow;
using Utils.WorkWithDB;

namespace Note
{
    public class MainPresenter
    {
        private readonly IMainView view;

        private DatabaseManager DataManager
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

        public MainPresenter(IMainView view)
        {
            this.view = view;
        }

        public void Init()
        {
            try
            {
                DataManager.Init();
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
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
                DataManager = ObjectsFactory.Instance.GetService<DatabaseManager>();
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
            try
            {
                return DataManager.GetDBFileName();
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return string.Empty;
            }
        }

        public string GetConnectionDescription()
        {
            try
            {
                return DataManager.GetConnectionDescription();
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return string.Empty;
            }
        }

        public long Insert(long parentId, string description, DataTypes type)
        {
            try
            {
                return DataManager.Insert(parentId, description, type);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return DBConstants.BASE_LEVEL;
            }
        }

        public void Delete(long id)
        {
            try
            {
                DataManager.Delete(id);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        public string GetTextData(long id)
        {
            try
            {
                return DataManager.GetTextData(id);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return string.Empty;
            }
        }

        public bool UpdateTextData(long id, string editValue, string plainText)
        {
            try
            {
                return DataManager.UpdateTextData(id, editValue, plainText);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public bool UpdateDescription(long id, string description)
        {
            try
            {
                return DataManager.UpdateDescription(id, description);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
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
            try
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
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return null;
            }
        }

        public bool IsCanChangeLevel(int position, long parentId, Direction direction)
        {
            try
            {
                return DataManager.IsCanChangeLevel(position, parentId, direction);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public bool ChangeLevel(int position, long parentId, long id, Direction direction)
        {
            try
            {
                return DataManager.ChangeLevel(position, parentId, id, direction);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public bool IsCanMove(int position, long parentId, Direction direction)
        {
            try
            {
                return DataManager.IsCanMove(position, parentId, direction);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public bool Move(int position, long parentId, long id, Direction direction)
        {
            try
            {
                return DataManager.Move(position, parentId, id, direction);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public void CheckChangedItems()
        {
            DatabaseManager dataManagerLocal;
            if (TryGetDataManager(out dataManagerLocal))
            {
                try
                {
                    IEnumerable<Tuple<Description, DataStatus>> descriptions = GetModifiedDescriptions(dataManagerLocal);
                    ItemsList form = new ItemsList();
                    form.LoadData(descriptions);
                    form.Show();
                }
                catch (Exception ex)
                {
                    DisplayMessage.ShowError(ex.Message);
                }
            }
        }

        private void HandleError(bool showMessage, string errorMessage)
        {
            if (showMessage)
            {
                DisplayMessage.ShowError(errorMessage ?? Resources.ConnectionErrorMessage);
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
                dataManagerLocal = ObjectsFactory.Instance.GetService<DatabaseManager>();
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
