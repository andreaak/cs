using System;
using Note.Domain.Properties;
using Utils.WorkWithDB.Connections;

namespace Note.Domain.Concrete
{
    public static class DBConstants
    {
        public const int BASE_LEVEL = -1;
        public const int START_POSITION = 0;
        public const string ENTITY_TABLE = "Entity";

        public const string ENTITY_TABLE_ID = "ID";
        public const string ENTITY_TABLE_PARENTID = "ParentID";
        public const string ENTITY_TABLE_DESC = "Description";
        public const string ENTITY_TABLE_TYPE = "Type";
        public const string ENTITY_TABLE_POSITION = "OrderPosition";

        public const string ENTITY_DATA_TABLE = "EntityData";

        public const string ENTITY_DATA_TABLE_ID = "ID";
        public const string ENTITY_DATA_TABLE_DATA = "Data";
        public const string ENTITY_DATA_TABLE_TEXT = "TextData";

        const string SQLiteScript = @"
            CREATE TABLE Entity(
                ID INTEGER  PRIMARY KEY NOT NULL,
                ParentID INTEGER,
                Description TEXT,
                Type INTEGER,
                OrderPosition INTEGER,
                ModDate TEXT          
            );

            CREATE TABLE EntityData(
                ID INTEGER  PRIMARY KEY NOT NULL,
                Data TEXT,
                TextData TEXT,
                HtmlData TEXT,
                ModDate TEXT         
            );

            CREATE TABLE DATA_LOG(         
                EntityId INTEGER, 
                Operation TEXT,
                EntityDescription TEXT,
                ModDate Text
            );

            CREATE TRIGGER Entity_AI AFTER INSERT ON Entity  
            BEGIN  
            INSERT INTO DATA_LOG(EntityId,
					            Operation,
					            EntityDescription,
					            ModDate)  
                     VALUES(NEW.ID,
		             'Insert ' ||  (CASE NEW.Type WHEN 0 THEN 'Folder' ELSE 'Note' END) ,
		             NEW.Description,
		             current_timestamp);  
            END;  

            CREATE TRIGGER Entity_AU AFTER UPDATE ON Entity  
            BEGIN  
            INSERT INTO DATA_LOG(EntityId,
					            Operation,
					            EntityDescription,
					            ModDate)  
                     VALUES(NEW.ID,
		             'Update ' ||  (CASE NEW.Type WHEN 0 THEN 'Folder' ELSE 'Note' END) ,
		             NEW.Description,
		             current_timestamp);  
            END;

            CREATE TRIGGER Entity_AD AFTER DELETE ON Entity  
            BEGIN  
            INSERT INTO DATA_LOG(EntityId,
					            Operation,
					            EntityDescription,
					            ModDate)  
                     VALUES(OLD.ID,
		             'Delete ' ||  (CASE OLD.Type WHEN 0 THEN 'Folder' ELSE 'Note' END) ,
		             OLD.Description,
		             current_timestamp);  
            END;    

            CREATE TRIGGER EntityData_AU AFTER UPDATE ON EntityData  
            BEGIN  
            INSERT INTO DATA_LOG(EntityId,
					            Operation,
					            EntityDescription,
					            ModDate)  
                     VALUES(NEW.ID,
		             'Update Note',
		             (SELECT e.Description FROM Entity e WHERE e.ID = OLD.ID),
		             current_timestamp);  
            END;

            CREATE TRIGGER EntityData_AD AFTER DELETE ON EntityData  
            BEGIN  
            INSERT INTO DATA_LOG(EntityId,
					            Operation,
					            EntityDescription,
					            ModDate)  
                     VALUES(OLD.ID,
		             'Delete Note' ,
		             (SELECT e.Description FROM Entity e WHERE e.ID = OLD.ID),
		             current_timestamp);  
            END;
        ";

        const string SQServerScript = @"
            CREATE TABLE Entity(
                ID int  PRIMARY KEY NOT NULL,
                ParentID int,
                Description nvarchar(max),
                Type int,
                OrderPosition int,
                ModDate nvarchar(max)                     
            );

            CREATE TABLE EntityData(
                ID int  PRIMARY KEY NOT NULL,
                Data nvarchar(max),
                TextData nvarchar(max),
                ModDate nvarchar(max)
            );
        ";

        public static string GetEntityExistQuery()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4} FROM {5} WHERE {0} = -1",
            ENTITY_TABLE_ID, ENTITY_TABLE_PARENTID, ENTITY_TABLE_DESC, ENTITY_TABLE_TYPE,
            ENTITY_TABLE_POSITION, ENTITY_TABLE);
        }

        public static string GetEntityDataExistQuery()
        {
            return string.Format("SELECT {0},{1},{2} FROM {3} WHERE {0} = -1",
            ENTITY_DATA_TABLE_ID, ENTITY_DATA_TABLE_DATA, ENTITY_DATA_TABLE_TEXT, ENTITY_DATA_TABLE);
        }

        public static string GetScript(DataBaseType dbType)
        {
            string outStr = null;
            switch (dbType)
            {
                case DataBaseType.SQLite:
                    outStr = SQLiteScript;
                    break;
                case DataBaseType.SQLServer:
                case DataBaseType.SQLExpress:
                    outStr = SQLiteScript;
                    break;
                default:
                    throw new Exception(Resources.ScriptErrorMessage);
            }
            return outStr.Replace(Environment.NewLine, " ").Trim();
        }
    }
}
