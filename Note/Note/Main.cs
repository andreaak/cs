using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataManager;
using DataManager.Domain;
using DataManager.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using System.IO;
using System.Threading;
using System.Text;
using Utils;
using Utils.WorkWithDB;
using Utils.ActionWindow;
using Note.ControlWrapper;
using DevExpress.XtraBars;
using Utils.WorkWithDB.Wrappers;
using ControlWrapper;
using Utils.WorkWithFiles;
using Properties;
using System.Drawing;

namespace Note
{
    public partial class Main : Form
    {
        NoteDataManager dataManager;
        IWrapper controlWrapper;
        private const string TITLE = "Note";

        public Main()
        {
            InitializeComponent();
            spellCheckerCustomDictionary1.AlphabetPath = Options.AlphabetPath;
            spellCheckerCustomDictionary1.DictionaryPath = Options.DictionaryPath;
            this.spellChecker1.Dictionaries.Add(spellCheckerCustomDictionary1);
            
            controlWrapper = new DevExpressWrapper(treeList1, richEditControl1, GetControls());

            Init(false);
            controlWrapper.InitHandlers();

            //Rectangle rect = Settings.Default.WindowPosition;
            //if (rect.Width != 0 && rect.Height != 0)
            //{
            //    this.Bounds = rect;
            //}
        }

        private BarItem[] GetControls()
        {
            return new BarItem[] {
                barButtonItemAddDir, barButtonItemAddNote, barButtonItemDelete, barButtonItemDown, barButtonItemUp
            , barButtonItemClassFormat, barButtonItemCodeFormat, barButtonItemDefinitionFormat
            , barButtonItemHeaderFormat, barButtonItemImport, barButtonItemInfoFormat, barButtonItemMethodFormat
            , barButtonItemRemoveLineBreak, barButtonItemRemoveWhiteSpace, barButtonItemRemoveDoubleWhiteSpace
            , barButtonItemSaveAll, barButtonItemVacuum , barButtonItemLevelUp, barButtonItemLevelDown, barButtonItemConvertDb
            , barButtonItemCheckNewestEntity, barButtonItemSpelling
            };
        }

        private void Init(bool isConnect)
        {
            controlWrapper.EnableControls(isConnect);
            if (isConnect)
            {
                controlWrapper.DataManager = dataManager; 
                controlWrapper.LoadEntityFromDB();
                this.Text = string.Format("{0} {1}", TITLE, dataManager.GetConnectionDescription());
            }
        }

        #region BUTTON_HANDLERS

        private void barButtonConnect_ItemClick(object sender, EventArgs e)
        {
            OptionsUtils.ClearDbData();
            Options.DbFile = null;
            Init(Connect());
        }

        private void barButtonItemAddDir_ItemClick(object sender, EventArgs e)
        {
            AddNode(DataTypes.DIR);
        }

        private void barButtonItemAddNote_ItemClick(object sender, EventArgs e)
        {
            AddNode(DataTypes.NOTE);
        }

        private void barButtonItemDelete_ItemClick(object sender, EventArgs e)
        {
            if (!controlWrapper.IsNodeSelect())
            {
                return;
            }
            long id = controlWrapper.SelectedNodeId;
            if (DisplayMessage.ShowWarningYesNO("Delete Selected Node") == DialogResult.Yes)
            {
                try
                {
                    dataManager.DeleteNode(id);
                    controlWrapper.UpdateBinding();
                }
                catch (Exception ex)
                {
                    DisplayMessage.ShowError(ex.Message);
                }
            }
        }

        private void barButtonItemUp_ItemClick(object sender, EventArgs e)
        {
            ChangeNodeLocation(dataManager.IsCanMoveNode, dataManager.MoveNode, Direction.UP);
        }

        private void barButtonItemDown_ItemClick(object sender, EventArgs e)
        {
            ChangeNodeLocation(dataManager.IsCanMoveNode, dataManager.MoveNode, Direction.DOWN);
        }

        private void barButtonItemSaveAll_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SaveNodesDataToFile();
        }

        private void barButtonItemDefinitionFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetDefinitionFormat();
        }

        private void barButtonItemMethodFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetMethodFormat();
        }

        private void barButtonItemClassFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetClassFormat();
        }

        private void barButtonItemHeaderFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetHeaderFormat();
        }

        private void barButtonItemInfoFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetInfoFormat();
        }

        private void barButtonItemCodeFormat_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.SetCodeFormat();
        }

        private void barButtonItemRemoveWhiteSpace_ItemClick(object sender, ItemClickEventArgs e)
        {
            controlWrapper.RemoveWhiteSpace();
        }

        private void barButtonItemRemoveDoubleWhiteSpace_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.RemoveDoubleWhiteSpace();
        }

        private void barButtonItemRemoveLineBreak_ItemClick(object sender, EventArgs e)
        {
            controlWrapper.RemoveLineBreak();
        }

        private void barButtonItemConvertDb_ItemClick(object sender, EventArgs e)
        {
            //if (Options.InType == DocTypes.None || Options.OutType == DocTypes.None)
            //{
            //    return;
            //}
            //controlWrapper.BeginUpdateRtfControl();             
            //string backup = controlWrapper.RtfText;
            
            //DBService tempDbService = new DBService();
            //tempDbService.ConvertData(controlWrapper);

            //controlWrapper.RtfText = backup;
            //controlWrapper.EndUpdateRtfControl();
        }

        private void barButtonItemVacuum_ItemClick(object sender, EventArgs e)
        {
            VacuumDb(false);
        }

        private void barButtonItemLevelUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeNodeLocation(dataManager.IsCanChangeNodeLevel, dataManager.ChangeNodeLevel, Direction.UP);
        }

        private void barButtonItemLevelDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeNodeLocation(dataManager.IsCanChangeNodeLevel, dataManager.ChangeNodeLevel, Direction.DOWN);
        }

        private void barButtonItemImport_ItemClick(object sender, EventArgs e)
        {
            if (controlWrapper.IsNoteNode())
            {
                return;
            }
            
            string[] extensions = new string[]
            {
                "Rtf Files (*.rtf)|*.rtf|",
                "All Files (*.*)|*.*"
            };
            string title = "File's Import";
            string[] fileNames;
            if (SelectFile.OpenFiles(title, string.Empty, out fileNames, extensions))
            {
                controlWrapper.DisableFocusing();
                foreach (string file in fileNames.Where(file => File.Exists(file)))
	            {
		            string rtf = File.ReadAllText(file);
                    long id = dataManager.InsertNode(controlWrapper.GetParentId(true), Path.GetFileNameWithoutExtension(file), DataTypes.NOTE);
                    controlWrapper.UpdateBinding();
                    if (controlWrapper.IsNodeSelect())
                    {
                        controlWrapper.FocusSelectedNode();
                    }
                    controlWrapper.Data = rtf;
                    dataManager.UpdateData(id, controlWrapper.Data, controlWrapper.TextData);
                    controlWrapper.FocusParentNode();
	            }
                controlWrapper.EnableFocusing();
            }
        }


        private void barButtonItemCheckNewestEntity_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NoteDataManager dataManagerLocal;
            if(TryGetDbService(out dataManagerLocal))
            {
                IEnumerable<Tuple<Entity, DataStatus>> descriptions = dataManager.GetModifiedDescriptions(dataManagerLocal);
                ItemsList form = new ItemsList();
                form.LoadData(descriptions);
                form.Show();
            }
        }

        void barButtonItemSpelling_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.spellChecker1.SpellCheckMode = barButtonItemSpelling.Checked ? 
                DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType 
                : DevExpress.XtraSpellChecker.SpellCheckMode.OnDemand;
        }

        #endregion


        #region DISPLAY

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.SizeChanged -= new System.EventHandler(this.Main_SizeChanged);
            this.LocationChanged -= Main_LocationChanged;
            this.WindowState = FormWindowState.Normal;
            this.Bounds = Settings.Default.WindowPosition;
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.LocationChanged += Main_LocationChanged;
        }

        private void Main_Shown(object sender, System.EventArgs e)
        {
            this.LocationChanged += Main_LocationChanged;
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {

                this.Visible = false;
                notifyIcon1.Visible = true;
            }
            else
            {
                Main_LocationChanged(null, EventArgs.Empty);
            }
        }

        private void Main_LocationChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                Settings.Default.WindowPosition = this.Bounds;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Settings.Default.WindowPosition = this.Bounds;
            Settings.Default.Save();
        }

        #endregion

        private bool Connect()
        {

            bool dbConnectionOk = false;
            bool showMessage = true;
            string errorMessage = "Connection is absent!";
            try
            {
                DialogResult res = new DBDataForm().ShowDialog(this);
                if (res != DialogResult.OK)
                {
                    return false;
                }
                dataManager = new NoteDataManager();
                if (dataManager.IsDBOnline())
                {
                    if (!dataManager.IsProperDB)
                    {
                        dbConnectionOk = CreateDb();
                    }
                    else
                    {
                        dbConnectionOk = true;
                    }
                }
            }
            catch (DbFileNotExistException)
            {
                dbConnectionOk = CreateDb();
            }
            catch (OperationCanceledException)
            {
                if (dataManager == null)
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

            HandleConnect(dbConnectionOk, showMessage, errorMessage);
            return dbConnectionOk;
        }

        private void HandleConnect(bool dbConnectionOk, bool showMessage, string errorMessage)
        {
            if (!dbConnectionOk)
            {
                if (showMessage)
                {
                    DisplayMessage.ShowError(errorMessage);
                }
            }
            else
            {
                notifyIcon1.Text = dataManager.GetDBFile();
            }
        }

        private bool TryGetDbService(out NoteDataManager dataManagerLocal)
        {

            bool dbConnectionOk = false;
            bool showMessage = true;
            string errorMessage = "Connection is absent!";
            dataManagerLocal = null;
            try
            {
                if (true)
                {
                    DialogResult res = new DBDataForm().ShowDialog(this);
                    if (res != DialogResult.OK)
                    {
                        return false;
                    }
                }
                dataManagerLocal = new NoteDataManager();
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

            if (!dbConnectionOk && showMessage)
            {
                DisplayMessage.ShowError(errorMessage);
            }

            return dbConnectionOk;
        }

        private bool CreateDb()
        {
            string errorMessage = "Database fault!";
            return dataManager.CreateDb(errorMessage);
        }

        private void VacuumDb(bool isSilent)
        {
            try
            {
                if (dataManager != null)
                {
                    dataManager.VacuumDb();
                }
                if (!isSilent)
                {
                    DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
                }
            }
            catch (Exception ex)
            {
                if (!isSilent)
                {
                    DisplayMessage.ShowError(ex.Message);
                }
            }
        }

        private void AddNode(DataTypes type)
        {
            if (!controlWrapper.IsNodeSelect() && type == DataTypes.NOTE
                || controlWrapper.IsNodeSelect() && controlWrapper.IsNoteNode())
            {
                return;
            }
            AddDescription form = new AddDescription();
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataManager.InsertNode(controlWrapper.GetParentId(form.IsChildNode), form.Description, type);
            controlWrapper.UpdateBinding();
            if (controlWrapper.IsNodeSelect())
            {
                controlWrapper.FocusSelectedNode();
            }
        }

        private void ChangeNodeLocation(Func<long, long, Direction, bool> IsValidAction, Func<long, long, long, Direction, bool> PerformAction, Direction dir)
        {
            controlWrapper.DisableFocusing();
            if (!controlWrapper.IsNodeSelect())
            {
                controlWrapper.EnableFocusing();
                return;
            }

            long position = controlWrapper.Position;
            long parentId = controlWrapper.ParentId;
            if (!IsValidAction(position, parentId, dir))
            {
                controlWrapper.EnableFocusing();
                return;
            }

            long id = controlWrapper.SelectedNodeId;
            if (PerformAction(position, parentId, id, dir))
            {
                controlWrapper.UpdateBinding();
            }
            controlWrapper.EnableFocusing();
        }
    }
}
