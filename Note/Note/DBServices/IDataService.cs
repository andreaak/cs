using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBServices
{
    public interface IDataService
    {
        object BindEntity{get;}
        bool IsProperDB{get;}
        List<Entity> Entity{get;}
        List<EntityData> EntityData{get;}
        long InsertNode(long parentId, string description, DataTypes type);
        void DeleteNode(long id);
        bool IsCanMoveNode(long position, long parentId, Direction direction);
        void MoveNode(long currentPosition, long parentId, long id, Direction direction);
        void InitEntity();
        string GetEntityData(long id);
        void UpdateNode(long id, bool isNoteNode, string data, string textData);
        bool UpdateNodeDescription(long id);
        bool UpdateNodeData(long id, string data, string textData);
        void ConvertData(ControlWrapper.IWrapper controlWrapper);
        bool IsCanChangeNodeLevel(long position, long parentId, Direction direction);
        void ChangeNodeLevel(long position, long parentId, long id, Direction direction);
        IEnumerable<Entity> GetNewestEntity(IDataService dataService);
    }
}
