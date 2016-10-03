using System.Diagnostics;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _06_TestFromResult
    {
        [Test]
        public async void TestAsyncAwait15_WithResult()
        {
            var test = new _06_TestFromResult();
            string res = await test.TestReturnTaskFromResult();
            Debug.WriteLine(res);//res == "AsyncTest"
        }

        [Test]
        public async void TestAsyncAwait18_ReturnTask_EmptyOperation()
        {
            var test = new _06_TestFromResult();

            await test.TestReturnEmptyTaskFromResult();
        }

        private Task<string> TestReturnTaskFromResult()
        {
            return Task.FromResult("AsyncTest");
        }

        private Task TestReturnEmptyTaskFromResult()
        {
            return Task.FromResult(new object());
        }
    }

#endif
}