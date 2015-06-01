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
        //TreeProvider Tree { get; set;}
        //ListViewProvider ListView { get; set; }
        RepositoryDirectory WorkingCopy { get; }
        Exception Error { get; }
        IEnumerable<RepositoryItem> GetActiveItems(string fullName);
        void Clear();
        void SetChangedEntitysData(string fullPath, ControlsData ctrlData);
        void RefreshFileStatus(ControlsData ctrlData, List<string> filesPath);
        bool IsNotVersioned(RepositoryItem entity);
        void AddFile(string fullPath);
        void ShowDiff(ControlsData controlsData, List<string> filesPath);
        void SetEntityData(string fullPath, ControlsData ctrlData);
        void Resolved(ControlsData ctrlData, List<string> filesPath);
        void Switch(ControlsData ctrlData, bool backup, bool restore, string targetLocation, List<string> filesPath);
        void Update(ControlsData ctrlData, List<string> filesPath);
        void Commit(List<string> files, string message);
        void Revert(List<string> filesPath);
        StringBuilder CreatePatch(List<string> filesPath);
        void MoveToChangeList(string changeList, List<string> filesPath);
        void RemoveFromChangeList(List<string> filesPath);
        CommitData GetLogs(string author);
        RepositoryItem GetEntity(string fullPath);
        RepositoryItem GetDeletedEntity(string fullPath);
        bool IsUnchanged(RepositoryItem entity);
        List<RepositoryItem> GetSelectedItems(List<string> filesPath);
        void Add(ControlsData ctrlData, List<string> filesPath);
        void Delete(ControlsData ctrlData, List<string> filesPath);
        string GetLocation(string path);
        Uri GetRepository(string path);
        event Action ProcessEnded;
        event Action Increment;
        void UnsubscribeEvents();

        IMainView View { get; set; }
    }
}
