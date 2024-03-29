﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithSvn.DiskHierarchy.Base
{
    public interface IRepositoryHelper
    {
        object LocalStatus { get; set; }

        string LocalStatusDesc { get; }

        object RemoteStatus { get; set; }

        string RemoteStatusDesc { get; }

        bool IsModified { get; }

        bool IsAdded { get; }

        bool IsDeleted { get; }

        bool IsConflicted { get; }

        bool IsUnchanged { get; }

        bool IsIgnoredItem(RepositoryFileStatuses list);

        bool IsNotVersioned { get; }

        bool IsDeletedLocal { get; }
    }
}
