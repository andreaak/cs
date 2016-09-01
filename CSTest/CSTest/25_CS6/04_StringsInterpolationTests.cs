using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _04_StringsInterpolationTests
    {
#if CS6
        [Test]
        public void Test1()
        {
            string name = "Test";
            int id = 23;
            string temp = string.Format("{0} {1}", name, id);
            Debug.WriteLine(temp);

            temp = $"{name} {id}";//StringsInterpolation
            Debug.WriteLine(temp);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"{name} {id}");
            Debug.WriteLine(sb.ToString());

            /*
            Test 23
            Test 23
            Test 23
            */
        }
#endif
    }
}
