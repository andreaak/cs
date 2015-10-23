using System;

namespace CSTest._12_MultiThreading._02_Synchronization._0_Setup
{
    public class _05_InterlockedSpinLockManager : IDisposable
    {
        private readonly _05_InterlockedSpinLock block;

        public _05_InterlockedSpinLockManager(_05_InterlockedSpinLock block)
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
