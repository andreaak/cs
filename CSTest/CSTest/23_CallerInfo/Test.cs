using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CSTest._23_CallerInfo
{

    [TestFixture]
    public class Test
    {
        #if CS5
        
        [Test]
        public void TestCallerInfo()
        {
            TraceMessage("Test message");
            /*
            message: Test message
            member name: TestCallerInfo
            source file path: E:\Prog\Projects\CS\CSTest\CSTest\23_CallerInfo\Test.cs
            source line number: 16
            ------------------------- 
            */
        }


        static void TraceMessage(string message,
                                [CallerMemberName] string memberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Debug.WriteLine("message: " + message);
            Debug.WriteLine("member name: " + memberName);
            Debug.WriteLine("source file path: " + sourceFilePath);
            Debug.WriteLine("source line number: " + sourceLineNumber);
            Debug.WriteLine(new string('-', 25));
        }
        
        #endif
    }
}
