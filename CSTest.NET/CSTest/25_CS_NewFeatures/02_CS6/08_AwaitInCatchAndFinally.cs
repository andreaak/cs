#if CS6
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._03_CS6
{
    [TestFixture]
    public class _08_AwaitInCatchAndFinally
    {

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

    }
}
#endif