﻿
namespace Note.Domain.Repository
{
    public class DLRepository /*: IDataRepository*/
    {
        //private DBServiceDataset ds;
        //private ADBWrapper dbWrapper = null;

        //public DLRepository(ADBWrapper dbWrapper)
        //{
        //    this.dbWrapper = dbWrapper;
        //    ds = new DBServiceDataset();
        //}

        //#region IDataRepository Members

        //public IList<Description> Descriptions
        //{
        //    get
        //    {
        //        return Convert(db.Entity);
        //    }
        //}

        //public IList<TextData> Texts
        //{
        //    get
        //    {
        //        return Convert(db.EntityData);
        //    }
        //}

        //public IList<object> Updates
        //{
        //    get
        //    {
        //        return db.GetChangeSet().Updates;
        //    }
        //}

        //public bool IsProperDB
        //{
        //    get
        //    {
        //        try
        //        {
        //            dbWrapper.SelectMethodDataset(DBConstants.GetEntityExistQuery(), DBConstants.ENTITY_TABLE, null);
        //            dbWrapper.SelectMethodDataset(DBConstants.GetEntityDataExistQuery(), DBConstants.ENTITY_DATA_TABLE, null);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //}

        //public long InsertNode(long parentId, string description, DataTypes type)
        //{
        //    long id = GetId(DBConstants.ENTITY_TABLE);
        //    ;
        //    long position = GetPosition(parentId);

        //    Entity.AddEntityRow(id, parentId, description, (byte)type, position, DateTime.Now.ToString());
        //    if (type == DataTypes.NOTE)
        //    {
        //        EntityData.AddEntityDataRow(id, "", "", DateTime.Now.ToString());
        //    }
        //    SaveDataToDB();
        //    return id;
        //}

        //public void DeleteNode(long id)
        //{
        //    DeleteNodeRecursive(id);
        //    SaveDataToDB();
        //}

        //public bool IsCanMoveNode(long position, long parentId, Direction direction)
        //{
        //    if (direction == Direction.UP)
        //    {
        //        return !IsMinPosition(position, parentId);
        //    }
        //    else
        //    {
        //        return !IsMaxPosition(position, parentId);
        //    }
        //}

        //public void MoveNode(long currentPosition, long parentId, long id, Direction direction)
        //{
        //    DBServiceDataset.EntityRow selDataRow =
        //    Description.FirstOrDefault<DBServiceDataset.EntityRow>(row => UTILS.IsActiveRow(row) && row.ID == id);

        //    long destPosition;
        //    if (direction == Direction.UP)
        //    {
        //        destPosition = Description.Where(row => UTILS.IsActiveRow(row)
        //            && row.ParentID == parentId && row.OrderPosition < currentPosition).Max(row => row.OrderPosition);
        //    }
        //    else
        //    {
        //        destPosition = Description.Where(row => UTILS.IsActiveRow(row)
        //            && row.ParentID == parentId && row.OrderPosition > currentPosition).Min(row => row.OrderPosition);
        //    }

        //    DBServiceDataset.EntityRow changeDataRow =
        //    Description.FirstOrDefault<DBServiceDataset.EntityRow>(row => UTILS.IsActiveRow(row)
        //        && row.ParentID == parentId && row.OrderPosition == destPosition);
        //    if (changeDataRow != null)
        //    {
        //        selDataRow.OrderPosition = destPosition;
        //        changeDataRow.OrderPosition = currentPosition;
        //    }
        //}

        //public void InitEntity()
        //{
        //    string table = DBConstants.ENTITY_TABLE;
        //    string sql = string.Format("SELECT * FROM {0}", DBConstants.ENTITY_TABLE);
        //    ClearData();
        //    FillData(sql, table, null);
        //}

        //public string GetEntityData(long id)
        //{
        //    DBServiceDataset.EntityDataRow selRow =
        //    EntityData.SingleOrDefault<DBServiceDataset.EntityDataRow>(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    if (selRow == null)
        //    {
        //        selRow = ReadEntityDataFromDB(id);
        //    }
        //    return selRow.Data;
        //}

        //public void UpdateNode(long id, bool isNoteNode, string data, string textData)
        //{
        //    DBServiceDataset.EntityDataRow selDataRow =
        //        EntityData.SingleOrDefault<DBServiceDataset.EntityDataRow>(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    DBServiceDataset.EntityRow selRow =
        //        Description.SingleOrDefault<DBServiceDataset.EntityRow>(row => UTILS.IsActiveRow(row) && row.ID == id);

        //    if (isNoteNode && selDataRow != null
        //        && data != selDataRow.Data)
        //    {
        //        selDataRow.Data = data;
        //        selDataRow.TextData = textData;
        //    }
        //    if ((selDataRow != null && selRow.RowState == DataRowState.Modified)
        //       || (selDataRow != null && selDataRow.RowState == DataRowState.Modified))
        //    {
        //        SaveDataToDB();
        //    }
        //}

        //public bool UpdateNodeDescription(long id)
        //{
        //    DBServiceDataset.EntityDataRow selDataRow =
        //        EntityData.FirstOrDefault<DBServiceDataset.EntityDataRow>(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    DBServiceDataset.EntityRow selRow =
        //        Description.FirstOrDefault<DBServiceDataset.EntityRow>(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    if ((selDataRow != null && selRow.RowState == DataRowState.Modified)
        //        || (selDataRow != null && selDataRow.RowState == DataRowState.Modified))
        //    {
        //        SaveDataToDB();
        //    }
        //    return false;
        //}

        //public bool UpdateNodeData(long id, string data, string textData)
        //{
        //    DBServiceDataset.EntityDataRow selDataRow =
        //        EntityData.FirstOrDefault<DBServiceDataset.EntityDataRow>(row => UTILS.IsActiveRow(row) && row.ID == id);

        //    if (selDataRow != null && data != selDataRow.Data)
        //    {
        //        selDataRow.Data = data;
        //        selDataRow.TextData = textData;
        //        SaveDataToDB();
        //        return true;
        //    }
        //    return false;
        //}

        //public void ConvertData(ControlWrapper.IWrapper controlWrapper)
        //{
        //    string table = DBConstants.ENTITY_DATA_TABLE;
        //    string sql = string.Format("SELECT * FROM {0}",
        //        DBConstants.ENTITY_DATA_TABLE);
        //    FillData(sql, table, null);

        //    foreach (var item in EntityData)
        //    {
        //        item.Data = controlWrapper.GetConvertedData(item.Data, Options.InType, Options.OutType);
        //        item.TextData = controlWrapper.TextData;
        //    }
        //    SaveDataToDB();
        //}

        //#endregion

        //private void ClearData()
        //{
        //    ds.Clear();
        //}

        //private void FillData(string sql, string table, List<Parameter> parameters)
        //{
        //    string xml = dbWrapper.SelectMethodToString(sql, table, parameters);
        //    DBUtils.GetDatasetFromXML(xml, ds);
        //    ds.AcceptChanges();
        //}

        //private void SaveDataToDB()
        //{
        //    try
        //    {
        //        dbWrapper.UpdateDataSet(ds.GetChanges());
        //        ds.AcceptChanges();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        DisplayMessage.ShowError("Update Failed: " + ex.Message);
        //    }
        //}

        //private long GetId(string tableName)
        //{
        //    string idColumnName = "ID";
        //    return dbWrapper.GetNextId(tableName, idColumnName);
        //}

        //private long GetPosition(long parentId)
        //{
        //    string sql = string.Format("SELECT MAX({0}) FROM {1} WHERE {2} = {3}",
        //        DBConstants.ENTITY_TABLE_POSITION, DBConstants.ENTITY_TABLE, DBConstants.ENTITY_TABLE_PARENTID, parentId);
        //    object ret = dbWrapper.ScalarMethod(sql);
        //    if (ret != DBNull.Value)
        //        return Convert.ToInt64(ret) + 1;
        //    else
        //        return 0;
        //}

        //private DBServiceDataset.EntityDataRow ReadEntityDataFromDB(long id)
        //{

        //    string table = DBConstants.ENTITY_DATA_TABLE;
        //    string sql = string.Format("SELECT * FROM {0} WHERE {1} = @Id",
        //        table, DBConstants.ENTITY_DATA_TABLE_ID, id);
        //    Parameter param = new Parameter("Id", DbType.Int64, id);
        //    FillData(sql, table, new List<Parameter> { param });
        //    DBServiceDataset.EntityDataRow selRow =
        //        EntityData.SingleOrDefault<DBServiceDataset.EntityDataRow>(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    if (selRow == null)
        //    {
        //        throw new Exception("Note not Found");
        //    }
        //    selRow.AcceptChanges();

        //    return selRow;
        //}

        //private void DeleteNodeRecursive(long id)
        //{
        //    IEnumerable<long> selEntityRows = Description.AsEnumerable().
        //        Where(row => UTILS.IsActiveRow(row) && row.ParentID == id).
        //        Select<DBServiceDataset.EntityRow, long>(row => row.ID);
        //    foreach (long itemId in selEntityRows)
        //    {
        //        DeleteNodeRecursive(itemId);
        //    }
        //    DBServiceDataset.EntityRow selEntityRow =
        //        Description.SingleOrDefault(row => UTILS.IsActiveRow(row) && row.ID == id);
        //    if (selEntityRow.Type == (long)DataTypes.NOTE)
        //    {
        //        DBServiceDataset.EntityDataRow selRow =
        //            EntityData.SingleOrDefault(row => UTILS.IsActiveRow(row) && row.ID == id);
        //        if (selRow == null)
        //        {
        //            selRow = ReadEntityDataFromDB(id);
        //        }
        //        selRow.Delete();
        //    }
        //    selEntityRow.Delete();
        //}

        //private bool IsMinPosition(long position, long parentId)
        //{
        //    return !Description.Any(row => UTILS.IsActiveRow(row)
        //        && row.ParentID == parentId && row.OrderPosition < position);
        //}

        //private bool IsMaxPosition(long position, long parentId)
        //{
        //    return !Description.Any(row => UTILS.IsActiveRow(row)
        //        && row.ParentID == parentId && row.OrderPosition > position);
        //}


        //public bool IsCanChangeNodeLevel(long position, long parentId, Direction direction)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ChangeNodeLevel(long position, long parentId, long id, Direction direction)
        //{
        //    throw new NotImplementedException();
        //}

        //#region IDataRepository Members


        //List<Entity> IDataRepository.Description
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //List<EntityData> IDataRepository.EntityData
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public IEnumerable<Tuple<Entity, DataStatus>> GetModifiedEntities(IDataRepository dataService)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

    }
}
