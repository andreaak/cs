using System;
using System.Collections.Generic;
using Note.Domain.Common;
using Note.Domain.Entities;

namespace Note.Domain.Repository
{
    public interface IDataRepository
    {
        bool IsProperDB{get;}
        IList<Description> Descriptions{get;}
        IList<TextData> Texts{get;}
        void Init();
        long Insert(long parentId, string description, DataTypes type);
        string GetTextData(long id);
        bool UpdateDescription(long id, string description);
        bool UpdateTextData(long id, string editValue, string textData);
        void Delete(long id);
        bool IsCanChangeLevel(int position, long parentId, Direction direction);
        bool ChangeLevel(int position, long parentId, long id, Direction direction);
        bool IsCanMove(int position, long parentId, Direction direction);
        bool Move(int position, long parentId, long id, Direction direction);
        IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(IDataRepository dataRepository);
    }
}
