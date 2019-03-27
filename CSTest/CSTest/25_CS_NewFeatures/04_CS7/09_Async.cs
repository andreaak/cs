using NUnit.Framework;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _09_Async
    {
#if CS7
        [Test]
        public async Task TestAsync_1()
        {
            int count = await ProcessFile();
            Debug.WriteLine(count.ToString() + " characters in file.");

            /*
            68 characters in file.
            */
        }

        private int GetCharacterCount()
        {
            int count = 0;
            count = 68;
            Thread.Sleep(5000);
            return count;
        }

        private async ValueTask<int> ProcessFile()
        {
            Task<int> task = new Task<int>(GetCharacterCount);
            task.Start();
            return await task;
        }

#endif
    }

}
