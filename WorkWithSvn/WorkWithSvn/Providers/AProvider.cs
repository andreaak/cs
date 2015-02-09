using System;
using System.Collections.Generic;
using System.Text;
using Display;
using Utils;
using WorkWithSvn.DiskHierarchy;
using WorkWithSvn.Utils;

namespace WorkWithSvn.Providers
{
    public interface AProvider
    {
        void GetDirectories();
        WorkWithTree Tree { get; set;}
        WorkWithListView ListView { get; set; }
        RepoDirectoryData WorkingCopy { get; }
        Exception Error { get; }

        void FillTree();
        void FillListView(ControlsData ctrlData);
        RepoFileTypes GetFilesTypes(ControlsData ctrlData);
        void ClearWorkingCopy();
        void SetChangedEntitysData(string fullPath, ControlsData ctrlData);
        void RefreshFileStatus(ControlsData ctrlData);
        bool IsNotVersioned(RepoEntityData entity);
        void AddFile(string fullPath);
        void ShowDiff(ControlsData controlsData);
        bool IsDeletedEntity(RepoEntityData entity);
        void SetEntityData(string fullPath, ControlsData ctrlData);
        void Resolved(ControlsData ctrlData);
        void Switch(ControlsData ctrlData, bool backup, bool restore, string targetLocation);
        void Update(ControlsData ctrlData);
        void Commit(List<string> files, string message);
        void Revert();
        StringBuilder CreatePatch();
        bool MoveToChangeList(string changeList);
        void RemoveFromChangeList();
        CommitData GetLogs(string author);
        RepoEntityData GetEntity(string fullPath);
        RepoEntityData GetDeletedEntity(string fullPath);
        bool IsUnchanged(RepoEntityData entity);
        List<RepoEntityData> GetSelectedItems();
        void Add(ControlsData ctrlData);
        void Delete(ControlsData ctrlData);
        string GetLocation(string path);
        Uri GetRepository(string path);
        event Action ProcessEnded;
        event Action Increment;
        void UnsubscribeEvents();
    }
}
