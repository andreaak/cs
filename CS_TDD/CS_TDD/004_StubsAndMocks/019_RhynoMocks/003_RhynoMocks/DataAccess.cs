using System.Collections.Generic;

namespace CS_TDD._004_StubsAndMocks._003_RhynoMocks
{
    class DataAccess
    {
        private List<string> db;

        public DataAccess()
        {
            db = new List<string>();
        }

        public bool HasInventory(string productName)
        {
            return db.Contains(productName);
        }

        public void Remove(string productName)
        {
            db.Remove(productName);
        }
    }
}
