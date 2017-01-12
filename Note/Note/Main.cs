using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraSpellChecker;
using Note.ControlWrapper;
using Note.ControlWrapper.DevExpressWrappers;
using Note.Domain.Common;
using Note.Domain.Concrete;
using Note.ExportData;
using Note.Properties;
using Utils;
using Utils.ActionWindow;

namespace Note
{
    public partial class Main : Form, IMainView
    {
        private const string TITLE = "Note";

        private readonly ITreeWrapper treeWrapper;
        private readonly IRtfWrapper rtfWrapper;
        private readonly ExportHelper exportHelper;
        private readonly MainPresenter presenter;

        Form IMainView.Form
        {
            get
            {
                return this;
            }
        }

        public Main()
        {
            InitializeComponent();
            presenter = new MainPresenter(this);

            InitSpellChecker();

            rtfWrapper = new RtfWrapper(myRichEditControl);
            treeWrapper = new TreeWrapper(treeList1, rtfWrapper, presenter);
            exportHelper = new ExportHelper(treeWrapper, presenter);
            Init(false);
        }

        #region BUTTON_HANDLERS

        private void barButtonConnect_ItemClick(object sender, EventArgs e)
        {
            OptionsUtils.ClearDbData();
            bool res = presenter.Connect();
            Init(res);
        }

        private void barButtonItemAddDir_ItemClick(object sender, EventArgs e)
        {
            Insert(DataTypes.DIR);
        }

        private void barButtonItemAddNote_ItemClick(object sender, EventArgs e)
        {
            if (treeWrapper.IsNodeSelect())
            {
                Insert(DataTypes.NOTE);
            }
        }

        private void barButtonItemDelete_ItemClick(object sender, EventArgs e)
        {
            Delete();
        }

        private void barButtonItemUp_ItemClick(object sender, EventArgs e)
        {
            treeWrapper.ChangeNodeLocation(presenter.IsCanMove, presenter.Move, Direction.UP);
        }

        private void barButtonItemDown_ItemClick(object sender, EventArgs e)
        {
            treeWrapper.ChangeNodeLocation(presenter.IsCanMove, presenter.Move, Direction.DOWN);
        }

        private void barButtonItemSaveAll_ItemClick(object sender, EventArgs e)
        {
            exportHelper.Export();
        }

        private void barButtonItemDefinitionFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetDefinitionFormat();
        }

        private void barButtonItemMethodFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetMethodFormat();
        }

        private void barButtonItemClassFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetClassFormat();
        }

        private void barButtonItemHeaderFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetHeaderFormat();
        }

        private void barButtonItemInfoFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetInfoFormat();
        }

        private void barButtonItemCodeFormat_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.SetCodeFormat();
        }

        private void barButtonItemRemoveWhiteSpace_ItemClick(object sender, ItemClickEventArgs e)
        {
            rtfWrapper.RemoveWhiteSpace();
        }

        private void barButtonItemRemoveDoubleWhiteSpace_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.RemoveDoubleWhiteSpace();
        }

        private void barButtonItemRemoveLineBreak_ItemClick(object sender, EventArgs e)
        {
            rtfWrapper.RemoveLineBreak();
        }

        private void barButtonItemConvertDb_ItemClick(object sender, EventArgs e)
        {
            string headerText = "Normalize database";
            Action action = () => presenter.Normalize();
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        private void barButtonItemVacuum_ItemClick(object sender, EventArgs e)
        {
            string headerText = "Vacuum database";
            Action action = () => presenter.VacuumDb();
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        private void barButtonItemLevelUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            treeWrapper.ChangeNodeLocation(presenter.IsCanChangeLevel, presenter.ChangeLevel, Direction.UP);
        }

        private void barButtonItemLevelDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            treeWrapper.ChangeNodeLocation(presenter.IsCanChangeLevel, presenter.ChangeLevel, Direction.DOWN);
        }

        private void barButtonItemImport_ItemClick(object sender, EventArgs e)
        {
            if (treeWrapper.IsNoteSelect())
            {
                return;
            }

            string[] extensions = new string[]
            {
                "Rtf Files (*.rtf)|*.rtf|",
                "All Files (*.*)|*.*"
            };
            string title = Resources.FilesImportCaption;
            string[] fileNames;
            if (SelectFile.OpenFiles(title, string.Empty, out fileNames, extensions))
            {
                treeWrapper.DisableFocusing();
                foreach (string file in fileNames.Where(File.Exists))
                {
                    string rtf = File.ReadAllText(file);
                    int parentId = treeWrapper.GetParentId(true);
                    string description = Path.GetFileNameWithoutExtension(file);
                    int id = presenter.Insert(parentId, description, DataTypes.NOTE);
                    if (id != DBConstants.BASE_LEVEL)
                    {
                        treeWrapper.Insert(id, parentId, description, DataTypes.NOTE);
                        rtfWrapper.EditValue = rtf;
                        presenter.UpdateTextData(id, rtfWrapper.EditValue, rtfWrapper.PlainText, rtfWrapper.HtmlText);
                    }
                    treeWrapper.FocusParentNode();
                }
                treeWrapper.EnableFocusing();
            }
        }

        private void barButtonItemCheckNewestEntity_ItemClick(object sender, ItemClickEventArgs e)
        {
            presenter.CheckChangedItems();
        }

        void barButtonItemSpelling_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.spellChecker1.SpellCheckMode = barButtonItemSpelling.Checked ?
                DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType
                : DevExpress.XtraSpellChecker.SpellCheckMode.OnDemand;
        }

        private void barButtonItemFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            Find form = new Find(presenter.DataManager);
            form.Show();
        }

        private void barButtonItemUpload_ItemClick(object sender, ItemClickEventArgs e)
        {
            string headerText = "Upload database";
            Action action = () => presenter.Upload();
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        private void barButtonItemDownload_ItemClick(object sender, ItemClickEventArgs e)
        {
            string headerText = "Download database";
            Action action = () => presenter.Download();
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        #endregion

        private void InitSpellChecker()
        {
            SpellCheckerCustomDictionary spellCheckerCustomDictionary = new SpellCheckerCustomDictionary();
            spellCheckerCustomDictionary.CacheKey = null;
            spellCheckerCustomDictionary.Culture = new System.Globalization.CultureInfo("ru-RU");
            spellCheckerCustomDictionary.Encoding = ((System.Text.UTF8Encoding)(System.Text.Encoding.GetEncoding(65001)));
            spellCheckerCustomDictionary.AlphabetPath = Options.AlphabetPath;
            spellCheckerCustomDictionary.DictionaryPath = Options.DictionaryPath;
            this.spellChecker1.Dictionaries.Add(spellCheckerCustomDictionary);
        }

        private void Init(bool isConnect)
        {
            EnableControls(isConnect);
            if (isConnect)
            {
                presenter.Init();
                rtfWrapper.Clear();
                treeWrapper.SetDataSource();
                Text = string.Format("{0} {1}", TITLE, presenter.GetConnectionDescription());
                notifyIcon.Text = presenter.GetDBFileName();
            }
        }

        private void EnableControls(bool isEnable)
        {
            foreach (BarItem item in GetBarButtons())
            {
                item.Enabled = isEnable;
            }
            treeWrapper.Enabled = isEnable;
            rtfWrapper.Enabled = isEnable;
        }

        private IEnumerable<BarItem> GetBarButtons()
        {
            return new BarItem[] {
                barButtonItemAddDir, barButtonItemAddNote, barButtonItemDelete, barButtonItemDown, barButtonItemUp
            , barButtonItemClassFormat, barButtonItemCodeFormat, barButtonItemDefinitionFormat
            , barButtonItemHeaderFormat, barButtonItemImport, barButtonItemInfoFormat, barButtonItemMethodFormat
            , barButtonItemRemoveLineBreak, barButtonItemRemoveWhiteSpace, barButtonItemRemoveDoubleWhiteSpace
            , barButtonItemSaveAll, barButtonItemVacuum , barButtonItemLevelUp, barButtonItemLevelDown, barButtonItemConvertDb
            , barButtonItemCheckNewestEntity, barButtonItemSpelling, barButtonItemFind, barButtonItemUpload, barButtonItemDownload
            };
        }

        private void Insert(DataTypes type)
        {
            if (treeWrapper.IsNoteSelect())
            {
                return;
            }
            AddDescription form = new AddDescription();
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            int parentId = treeWrapper.GetParentId(form.IsChildNode);
            int id = presenter.Insert(parentId, form.Description, type);
            if (id != DBConstants.BASE_LEVEL)
            {
                treeWrapper.Insert(id, parentId, form.Description, type);
            }
        }

        private void Delete()
        {
            if (!treeWrapper.IsNodeSelect())
            {
                return;
            }
            if (DisplayMessage.ShowWarningYesNO(Resources.DeleteQuestion) == DialogResult.Yes)
            {
                try
                {
                    int id = treeWrapper.SelectedNodeId;
                    presenter.Delete(id);
                    treeWrapper.Delete(id);
                }
                catch (Exception ex)
                {
                    DisplayMessage.ShowError(ex.Message);
                }
            }
        }

        #region DISPLAY

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Visible = true;
            notifyIcon.Visible = false;
            this.SizeChanged -= new EventHandler(this.Main_SizeChanged);
            this.LocationChanged -= Main_LocationChanged;
            this.WindowState = FormWindowState.Normal;
            this.Bounds = Settings.Default.WindowPosition;
            this.SizeChanged += new EventHandler(this.Main_SizeChanged);
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
                notifyIcon.Visible = true;
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

    }
}
