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
        IList<LogData> Logs { get;}
        void Init();
        int Insert(int parentId, string description, DataTypes type);
        string GetTextData(int id);
        bool UpdateDescription(int id, string description);
        bool UpdateTextData(int id, string editValue, string textData, string htmlText);
        void Delete(int id);
        bool IsCanChangeLevel(int position, int parentId, Direction direction);
        bool ChangeLevel(int position, int parentId, int id, Direction direction);
        bool IsCanMove(int position, int parentId, Direction direction);
        bool Move(int position, int parentId, int id, Direction direction);
        IEnumerable<DescriptionWithStatus> GetModifiedDescriptions(IDataRepository dataRepository);
        IEnumerable<DescriptionWithText> Find(string text);
    }
}
