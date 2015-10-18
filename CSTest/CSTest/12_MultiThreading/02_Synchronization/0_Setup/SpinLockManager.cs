using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._12_MultiThreading._02_Synchronization._0_Setup
{
    public class SpinLockManager : IDisposable
    {
        private readonly SpinLock_ block;

        public SpinLockManager(SpinLock_ block)
        {
            this.block = block;
            block.Enter();
        }

        public void Dispose()
        {
            block.Exit();
        }
    }
}
