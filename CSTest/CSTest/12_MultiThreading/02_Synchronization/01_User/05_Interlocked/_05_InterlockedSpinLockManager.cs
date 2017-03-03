using System;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User._05_Interlocked
{
    public class _05_InterlockedSpinLockManager : IDisposable
    {
        private readonly _05_InterlockedExchange block;

        public _05_InterlockedSpinLockManager(_05_InterlockedExchange block)
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
