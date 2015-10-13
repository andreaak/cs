using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CSTest._04_Class._06_Indexers
{
    class TestClass
    {
        List<int> items = new List<int>();

        private int myProperty;

        public int MyProperty
        {
            get { return myProperty; }
            private set { myProperty = value; }
        }
        
        public int Item { get; set; } 
        
        //already contains a definition for 'Item'
        //in CIL indexer write like set_Item and get_Item
        //can't define default indexer
        //public int this[int index]
        //{
        //    get { return items[index]; }
        //    set { items[index] = value; }
        //}  
        //параметры не дают возможности перегрузки со свойством
        //public int this[string index, int i]
        //{
        //    get { return 0; }
        //    set { }
        //} 

        [IndexerName("Itm")]
        public int this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }
        //нельзя перегружать индексаторы с одинаковыми параметрами по имени индексатора
        //[IndexerName("Itm2")]
        //public int this[int index]
        //{
        //    get { return items[index]; }
        //    set { items[index] = value; }
        //}

        [IndexerName("Itm")]
        public int this[int index, string indexStr]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

    }
}
