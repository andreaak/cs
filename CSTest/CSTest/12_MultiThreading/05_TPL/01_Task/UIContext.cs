using System.Threading;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    public class UIContext
    {
        private static TaskScheduler current;

        /// <summary>
        /// Should be used instead of TaskScheduler.FromCurrentSynchronizationContext()
        /// </summary>
        public static TaskScheduler Current
        {
            get
            {
                if (current == null)
                {
                    Initialize();
                }
                return current;
            }
            private set
            {
                current = value;
            }
        }

        /// <summary>
        /// Should be initialized on application startup
        /// </summary>
        public static void Initialize()
        {
            if (current != null)
            {
                return;
            }
            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            }

            Current = TaskScheduler.FromCurrentSynchronizationContext();
        }
    }
}
