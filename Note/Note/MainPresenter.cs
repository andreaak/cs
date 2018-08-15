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
        private string backupedFile;

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

        public void Connect()
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
                        view.Refresh(false);
                        return;
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
            view.Refresh(dbConnectionOk);
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
                var res = DataManager.Insert(parentId, description, type);
                view.OnChanged();
                return res;
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
                view.OnChanged();
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
                var res = DataManager.UpdateTextData(id, editValue, plainText, htmlText);
                view.OnChanged();
                return res;
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
                var res = DataManager.UpdateDescription(id, description);
                view.OnChanged();
                return res;
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
                var res = DataManager.ChangeLevel(position, parentId, id, direction);
                view.OnChanged();
                return res;
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
                var res = DataManager.Move(position, parentId, id, direction);
                view.OnChanged();
                return res;
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
                return false;
            }
        }

        public void CheckChangedItems(string path = null)
        {
            DatabaseManager dataManagerLocal;
            if (TryGetDataManager(out dataManagerLocal, path))
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

        public bool IsNewForUpload()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            DateTime remoteDate;
            if (!helper.TryGetModificationDate(OptionsUtils.DbPath, Options.DbDirectory, out remoteDate))
            {
                return false;
            }

            var fileDate = File.GetLastWriteTime(OptionsUtils.DbPath);
            return Compare(remoteDate, fileDate) < 0;
        }

        public bool IsNewForDownload()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            DateTime remoteDate;
            if (!helper.TryGetModificationDate(OptionsUtils.DbPath, Options.DbDirectory, out remoteDate))
            {
                return false;
            }
            var fileDate = File.GetLastWriteTime(OptionsUtils.DbPath);
            return Compare(remoteDate, fileDate) > 0;
        }

        private int Compare(DateTime date1, DateTime date2)
        {
            var dateNorm1 = new DateTime(date1.Year, date1.Month, date1.Day, date1.Hour, date1.Minute, 0);
            var dateNorm2 = new DateTime(date2.Year, date2.Month, date2.Day, date2.Hour, date2.Minute, 0);
            return dateNorm1.CompareTo(dateNorm2);
        }

        public void UploadToGoogleDrive()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            helper.UploadOrUpdateFile(OptionsUtils.DbPath, Options.DbDirectory);
            view.OnChanged();
        }

        public void DownloadFromGoogleDrive()
        {
            GoogleDriveHelper helper = new GoogleDriveHelper();
            helper.Init();
            if (helper.DownloadFile(OptionsUtils.DbPath, Options.DbDirectory, OptionsUtils.DbPath, BackupFile))
            {
                Connect();
            }
        }

        public void CheckBackupedFile()
        {
            CheckChangedItems(backupedFile);
        }

        private void BackupFile(string sourceFile)
        {
            string backupDirectory = System.IO.Path.GetDirectoryName(sourceFile) +
                System.IO.Path.DirectorySeparatorChar + Options.BackupDirectory;

            string fileName = System.IO.Path.GetFileName(sourceFile);
            if (!System.IO.Directory.Exists(backupDirectory))
            {
                System.IO.Directory.CreateDirectory(backupDirectory);
            }
            if (System.IO.File.Exists(sourceFile))
            {
                backupedFile = backupDirectory +
                               System.IO.Path.DirectorySeparatorChar +
                               fileName +
                               DateTime.Now.ToString("yyyyMMdd-HHmmss");
                System.IO.File.Copy(sourceFile, backupedFile);
            }
        }

        public void ShowLogs()
        {
            try
            {
                Logs form = new Logs();
                form.LoadData(DataManager.Logs);
                form.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        private void HandleError(bool showMessage, string errorMessage)
        {
            if (showMessage)
            {
                DisplayMessage.ShowError(errorMessage ?? Resources.ConnectionErrorMessage);
            }
        }

        private bool TryGetDataManager(out DatabaseManager dataManagerLocal, string dataSource)
        {
            bool dbConnectionOk = false;
            bool showMessage = true;
            string errorMessage = null;
            dataManagerLocal = null;
            try
            {
                if (string.IsNullOrEmpty(dataSource))
                {
                    DialogResult form = new DBDataForm().ShowDialog(view.Form);
                    if (form != DialogResult.OK)
                    {
                        return false;
                    }
                    dataManagerLocal = ObjectsFactory.Instance.GetService<DatabaseManager>();
                }
                else
                {
                    dataManagerLocal = GetDatabaseManager(dataSource);
                }
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

        private DatabaseManager GetDatabaseManager(string dataSource)
        {
            string connectionString = OptionsUtils.GetConnectionString(dataSource);
            return ObjectsFactory.Instance.GetService<DatabaseManager>(
                new KeyValuePair<string, object>(OptionsUtils.ProviderName, OptionsUtils.Provider),
                new KeyValuePair<string, object>(OptionsUtils.ConnectionStringName, connectionString));
        }
    }
}
