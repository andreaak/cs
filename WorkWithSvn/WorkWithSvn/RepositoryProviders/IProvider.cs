using System;
using System.Collections.Generic;
using System.Text;
using VisualProviders;
using Utils;
using WorkWithSvn.DiskHierarchy.Base;
using WorkWithSvn.Utils;

namespace WorkWithSvn.RepositoryProviders
{
    public interface IProvider
    {
        RepositoryDirectory WorkingCopy { get; }
        
        void Clear();
        void ReadDirectories(string fullName);
        void ScanDirectory(string fullName, ControlsData ctrlData);
        void RefreshFilesStatus(List<string> fullNames, ControlsData ctrlData);
        void ShowDiff(List<string> fullNames, ControlsData ctrlData);
        void Resolved(List<string> fullNames, ControlsData ctrlData);
        void AddItems(List<string> fullNames, ControlsData ctrlData);
        void DeleteItems(List<string> fullNames, ControlsData ctrlData);
        void Switch(List<string> fullNames, ControlsData ctrlData, bool backup, bool restore, string targetLocation);
        void Update(List<string> fullNames, ControlsData ctrlData);
        void Commit(List<string> fullNames, string message);
        List<RepositoryItem> GetItems(List<string> fullNames);
        bool IsNotVersioned(RepositoryItem entity);
        void AddFile(string fullPath);
        void Revert(List<string> fullNames);
        StringBuilder CreatePatch(List<string> fullNames);
        void MoveToChangeList(string changeListName, List<string> fullNames);
        void RemoveFromChangeList(List<string> fullNames);
        CommitData LogByAuthor(string author);
        void SetItemData(string fullName, ControlsData ctrlData);
        RepositoryItem GetDeletedItem(string fullPath);
        string GetLocation(string path);
        Uri GetRepository(string path);
        IEnumerable<RepositoryItem> GetActiveItems(string fullName, ControlsData ctrlData);
    }
}
