using System.Security;
using System.Threading;

namespace CSTest._30_NET_Code
{
    public interface IThreadPoolWorkItem
    {
        [SecurityCritical]
        void ExecuteWorkItem();

        [SecurityCritical]
        void MarkAborted(ThreadAbortException tae);
    }
}
