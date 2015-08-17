using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._004_StubsAndMocks._003_Mocks_Rhino
{
    public class Order
    {
        public string ProductName { get; private set; }
        public bool IsFilled { get; private set; }

        public Order(string productName)
        {
            ProductName = productName;
        }

        public void Fill(IWarehouse warehouse)
        {
            if (warehouse.HasInventory(ProductName))
            {
                warehouse.Remove(ProductName);
                IsFilled = true;
            }
        }
    }
}
