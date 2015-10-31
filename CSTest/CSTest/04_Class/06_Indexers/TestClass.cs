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
        // Виртуальный индексатор.
        public virtual int this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }
        [IndexerName("Itm")]
        public int this[string index]
        {
            get { return items[int.Parse(index)]; }
            set { items[int.Parse(index)] = value; }
        }


        //нельзя перегружать индексаторы с одинаковыми параметрами по имени индексатора
        //[IndexerName("Itm2")]
        //public int this[int index]
        //{
        //    get { return items[index]; }
        //    set { items[index] = value; }
        //}

        [IndexerName("Itm")]
        public int this[int index1, string index2]
        {
            get { return items[index1]; }
            set { items[index1] = value; }
        }

    }

    class DerivedClass : TestClass
    {
        private string[] derivedArray = null;

        // Конструктор.
        public DerivedClass()
        {
            derivedArray = new string[3];
            derivedArray[0] = "Zero!";
            derivedArray[1] = "One!";
            derivedArray[2] = "Two!";
        }

        // Переопределенный индексатор.
        [IndexerName("Itm")]
        public override string this[int index]
        {
            get { return base[index] + " - " + derivedArray[index]; }
        }
    }
}
