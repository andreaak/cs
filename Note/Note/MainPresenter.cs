using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Note.ControlWrapper.Binding;
using Note.Domain;
using Note.Domain.Common;
using Note.Domain.Concrete;
using Note.Domain.Entities;
using Note.Properties;
using Utils;
using Utils.ActionWindow;
using Utils.WorkWithDB;
using Utils.Google;
using System.IO;

namespace Note
{
    public class MainPresenter
    {
        private readonly IMainView view;

        public DatabaseManager DataManager
        {
            get;
            private set;
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
                if (OptionsUtils.ConnectionName == OptionsUtils.Other)
                {
                    DialogResult res = new DBDataForm().ShowDialog(view.Form);
                    if (res != DialogResult.OK)
                    {
                        return false;
                    }
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

        public int Insert(int parentId, string description, DataTypes type)
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

        public void Delete(int id)
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

        public string GetTextData(int id)
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

        public void Normalize()
        {
            new Normalizer().Normalize(DataManager);
        }

        public bool UpdateTextData(int id, string editValue, string plainText, string htmlText)
        {
            try
            {
                return DataManager.UpdateTextData(id, editValue, plainText, htmlText);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public bool UpdateDescription(int id, string description)
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

        public void VacuumDb()
        {
            DataManager.VacuumDb();
        }

        public object GetDataSource()
        {

            string headerText = "Read Data";
            Func<object> func = () =>
            {
                BindingDataset.DescriptionDataTable table = new BindingDataset.DescriptionDataTable();
                foreach (var item in Descriptions)
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
            };
            return CancelFormEx.ShowProgressWindow(func, headerText);
        }

        public bool IsCanChangeLevel(int position, int parentId, Direction direction)
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

        public bool ChangeLevel(int position, int parentId, int id, Direction direction)
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

        public bool IsCanMove(int position, int parentId, Direction direction)
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

        public bool Move(int position, int parentId, int id, Direction direction)
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
                string headerText = "Find changed items";
                Func<IEnumerable<DescriptionWithStatus>> func = () => GetModifiedDescriptions(dataManagerLocal);
                IEnumerable<DescriptionWithStatus> res = CancelFormEx.ShowProgressWindow(func, headerText);
                if (res != null)
                {
                    ItemsList form = new ItemsList();
                    form.LoadData(res);
                    form.Show();
                }
            }
        }

        public void UploadToGoogleDrive()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            helper.UploadOrUpdateFile(OptionsUtils.DbPath, Options.DbDirectory);
        }


        public void DownloadFromGoogleDrive()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            helper.DownloadFile(OptionsUtils.DbPath, Options.DbDirectory, OptionsUtils.DbPath);
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

        private IEnumerable<DescriptionWithStatus> GetModifiedDescriptions(DatabaseManager dataManagerLocal)
        {
            return DataManager.GetModifiedDescriptions(dataManagerLocal);
        }
    }
}
