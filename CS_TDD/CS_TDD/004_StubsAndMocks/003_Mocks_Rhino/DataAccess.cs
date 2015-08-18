using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._004_StubsAndMocks._003_Mocks_Rhino
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
