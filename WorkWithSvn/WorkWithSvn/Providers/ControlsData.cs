﻿using System.Collections.Generic;

namespace WorkWithSvn.Providers
{
    public class ControlsData
    {
        public string ChangeList { get; set; }

        public List<string> SelectedExtensions { get; set; }

        public bool ShowModified { get; set; }

        public bool ShowModifiedOnServer { get; set; }

        public bool ShowUnversioned { get; set; }

        public bool ShowConflicted { get; set; }

        public bool ShowSwitched { get; set; }

        public bool ShowUnchanged { get; set; }

        public bool UseServerData { get; set; }

        public bool FastScan { get; set; }

        public bool IsSvnDif { get; set; }
    }
}