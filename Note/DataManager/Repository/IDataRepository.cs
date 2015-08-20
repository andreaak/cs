using System;
using System.Collections.Generic;

namespace DataManager.Repository
{
    public interface IDataRepository
    {
        bool IsProperDB{get;}
        IList<Description> Descriptions{get;}
        IList<TextData> Texts{get;}
        IList<object> Updates{get;}
        void Init();
        long InsertNode(long parentId, string description, DataTypes type);
        void DeleteNode(long id);
        bool IsCanChangeNodeLevel(int position, long parentId, Direction direction);
        bool ChangeNodeLevel(int position, long parentId, long id, Direction direction);
        bool IsCanMoveNode(int position, long parentId, Direction direction);
        bool MoveNode(int position, long parentId, long id, Direction direction);
        string GetTextData(long id);
        bool UpdateDescription(long id, string description);
        bool UpdateTextData(long id, string editValue, string textData);
        IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(IDataRepository dataRepository);
    }
}
