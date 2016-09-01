using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _08_AwaitInCatchAndFinally
    {
#if CS6
        [Test]
        public async void Test1()
        {
            await TestMethod();
            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            In catch
            In finally
            */
        }

        private static async Task TestMethod()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                await Task.FromResult(new object());
                Debug.WriteLine("In catch");
            }
            finally
            {
                await Task.FromResult(new object());
                Debug.WriteLine("In finally");
            }
        }
#endif
    }
}
