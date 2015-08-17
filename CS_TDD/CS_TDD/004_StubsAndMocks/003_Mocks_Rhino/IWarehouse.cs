using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._004_StubsAndMocks._003_Mocks_Rhino
{
    public interface IWarehouse
    {
        bool HasInventory(string productName);
        void Remove(string productName);
    }
}
