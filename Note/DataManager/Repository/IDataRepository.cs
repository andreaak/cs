using System;
using System.Collections.Generic;
using DataManager.Domain;

namespace DataManager.Repository
{
    public interface IDataRepository
    {
        bool IsProperDB{get;}
        List<Entity> Descriptions{get;}
        List<EntityData> Data{get;}
        IList<object> Updates{get;}
        void Init();
        long InsertNode(long parentId, string description, DataTypes type);
        void DeleteNode(long id);
        bool IsCanChangeNodeLevel(long position, long parentId, Direction direction);
        bool ChangeNodeLevel(long position, long parentId, long id, Direction direction);
        bool IsCanMoveNode(long position, long parentId, Direction direction);
        bool MoveNode(long currentPosition, long parentId, long id, Direction direction);
        string GetData(long id);
        bool UpdateDescription(long id, string description);
        bool UpdateData(long id, string data, string textData);
        IEnumerable<Tuple<Entity, DataStatus>> GetModifiedDescriptions(IDataRepository dataRepository);
    }
}
