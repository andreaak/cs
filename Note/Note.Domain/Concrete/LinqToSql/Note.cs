using System;
using System.ComponentModel;
using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
using DbLinq.Data.Linq;
using DbLinq.Vendor;
#endif  // MONO_STRICT
using System.Data.Linq.Mapping;
using System.Diagnostics;


namespace Note.Domain.Concrete.LinqToSql
{

[Table(Name = "main.Entity")]
    public partial class Entity : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {

        private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");

        private string _description;

        private long _id;

        private System.Nullable<long> _orderPosition;

        private System.Nullable<long> _parentID;

        private System.Nullable<int> _type;

        private string _modDate;

        #region Extensibility Method Declarations
        partial void OnCreated();

        partial void OnDescriptionChanged();

        partial void OnDescriptionChanging(string value);

        partial void OnIDChanged();

        partial void OnIDChanging(long value);

        partial void OnOrderPositionChanged();

        partial void OnOrderPositionChanging(System.Nullable<long> value);

        partial void OnParentIDChanged();

        partial void OnParentIDChanging(System.Nullable<long> value);

        partial void OnTypeChanged();

        partial void OnTypeChanging(System.Nullable<int> value);

        partial void OnModDateChanged();

        partial void OnModDateChanging(string value);

        #endregion


        public Entity()
        {
            this.OnCreated();
        }

        [Column(Storage = "_description", Name = "Description", DbType = "TEXT", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                if (((_description == value)
                            == false))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [Column(Storage = "_id", Name = "ID", DbType = "INTEGER", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
        public long ID
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((_id != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this._id = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [Column(Storage = "_orderPosition", Name = "OrderPosition", DbType = "INTEGER", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public System.Nullable<long> OrderPosition
        {
            get
            {
                return this._orderPosition;
            }
            set
            {
                if ((_orderPosition != value))
                {
                    this.OnOrderPositionChanging(value);
                    this.SendPropertyChanging();
                    this._orderPosition = value;
                    this.SendPropertyChanged("OrderPosition");
                    this.OnOrderPositionChanged();
                }
            }
        }

        [Column(Storage = "_parentID", Name = "ParentID", DbType = "INTEGER", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public System.Nullable<long> ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                if ((_parentID != value))
                {
                    this.OnParentIDChanging(value);
                    this.SendPropertyChanging();
                    this._parentID = value;
                    this.SendPropertyChanged("ParentID");
                    this.OnParentIDChanged();
                }
            }
        }

        [Column(Storage = "_type", Name = "Type", DbType = "INTEGER", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public System.Nullable<int> Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if ((_type != value))
                {
                    this.OnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._type = value;
                    this.SendPropertyChanged("Type");
                    this.OnTypeChanged();
                }
            }
        }

        [Column(Storage = "_modDate", Name = "ModDate", DbType = "TEXT", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public string ModDate
        {
            get
            {
                return this._modDate;
            }
            set
            {
                if (((_modDate == value)
                            == false))
                {
                    this.OnModDateChanging(value);
                    this.SendPropertyChanging();
                    this._modDate = value;
                    this.SendPropertyChanged("ModDate");
                    this.OnModDateChanged();
                }
            }
        }

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
            if ((h != null))
            {
                h(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
            if ((h != null))
            {
                h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", ID, Description) ;
        }
    }

[Table(Name = "main.EntityData")]
    public partial class EntityData : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {

        private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");

        private string _data;

        private long _id;

        private string _textData;

        private string _modDate;

        #region Extensibility Method Declarations
        partial void OnCreated();

        partial void OnDataChanged();

        partial void OnDataChanging(string value);

        partial void OnIDChanged();

        partial void OnIDChanging(long value);

        partial void OnTextDataChanged();

        partial void OnTextDataChanging(string value);

        partial void OnModDateChanged();

        partial void OnModDateChanging(string value);
        #endregion


        public EntityData()
        {
            this.OnCreated();
        }

        [Column(Storage = "_data", Name = "Data", DbType = "TEXT", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public string Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if (((_data == value)
                            == false))
                {
                    this.OnDataChanging(value);
                    this.SendPropertyChanging();
                    this._data = value;
                    this.SendPropertyChanged("Data");
                    this.OnDataChanged();
                }
            }
        }

        [Column(Storage = "_id", Name = "ID", DbType = "INTEGER", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
        public long ID
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((_id != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this._id = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [Column(Storage = "_textData", Name = "TextData", DbType = "TEXT", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public string TextData
        {
            get
            {
                return this._textData;
            }
            set
            {
                if (((_textData == value)
                            == false))
                {
                    this.OnTextDataChanging(value);
                    this.SendPropertyChanging();
                    this._textData = value;
                    this.SendPropertyChanged("TextData");
                    this.OnTextDataChanged();
                }
            }
        }

        [Column(Storage = "_modDate", Name = "ModDate", DbType = "TEXT", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public string ModDate
        {
            get
            {
                return this._modDate;
            }
            set
            {
                if (((_modDate == value)
                            == false))
                {
                    this.OnModDateChanging(value);
                    this.SendPropertyChanging();
                    this._modDate = value;
                    this.SendPropertyChanged("ModDate");
                    this.OnModDateChanged();
                }
            }
        }

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
            if ((h != null))
            {
                h(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
            if ((h != null))
            {
                h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    public partial class NoteDataContext : DataContext
    {

        #region Extensibility Method Declarations
        partial void OnCreated();
        #endregion


        public NoteDataContext(string connectionString) :
            base(connectionString)
        {
            this.OnCreated();
        }

        public NoteDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public NoteDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public Table<Entity> Entity
        {
            get
            {
                return this.GetTable<Entity>();
            }
        }

        public Table<EntityData> EntityData
        {
            get
            {
                return this.GetTable<EntityData>();
            }
        }

        public NoteDataContext(IDbConnection connection) :
            base(connection, new DbLinq.Sqlite.SqliteVendor())
        {
            this.OnCreated();
        }

        public NoteDataContext(IDbConnection connection, IVendor sqlDialect) :
            base(connection, sqlDialect)
        {
            this.OnCreated();
        }

        public NoteDataContext(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) :
            base(connection, mappingSource, sqlDialect)
        {
            this.OnCreated();
        }
    }
}




//#region Start MONO_STRICT
//#if MONO_STRICT

//public partial class Main
//{
	
//    public Main(IDbConnection connection) : 
//            base(connection)
//    {
//        this.OnCreated();
//    }
//}
//#region End MONO_STRICT
//#endregion
//#else     // MONO_STRICT

//public partial class NoteDataContext
//{

//    public NoteDataContext(IDbConnection connection) :
//        base(connection, new DbLinq.Sqlite.SqliteVendor())
//    {
//        this.OnCreated();
//    }

//    public NoteDataContext(IDbConnection connection, IVendor sqlDialect) :
//        base(connection, sqlDialect)
//    {
//        this.OnCreated();
//    }

//    public NoteDataContext(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) :
//        base(connection, mappingSource, sqlDialect)
//    {
//        this.OnCreated();
//    }
//}
//#region End Not MONO_STRICT
//#endregion
//#endif     // MONO_STRICT
//#endregion



