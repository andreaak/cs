using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._004_StubsAndMocks._003_Mocks_Rhino
{
    public class Warehouse : IWarehouse
    {
        private DataAccess db;

        public Warehouse()
        {
            db = new DataAccess();
        }

        public virtual bool HasInventory(string productName)
        {
            return db.HasInventory(productName);
        }

        public virtual void Remove(string productName)
        {
            db.Remove(productName);
        }
    }
}
