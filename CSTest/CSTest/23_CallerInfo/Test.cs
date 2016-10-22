using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CSTest._23_CallerInfo
{
#if CS5

    [TestFixture]
    public class Test
    {


        [Test]
        public void TestCallerInfo()
        {
            TraceMessage("Test message");
            /*
            message: Test message
            caller member name: TestCallerInfo
            caller file path: D:\My\cs\CSTest\CSTest\23_CallerInfo\Test.cs
            caller line number: 16
            -------------------------
            */
        }

        [Test]
        public void TestCallerInfo_Nested()
        {
            TraceMessage1("Test message");
            /*
            message: Test message
            caller member name: TestCallerInfo_Nested
            caller file path: D:\My\cs\CSTest\CSTest\23_CallerInfo\Test.cs
            caller line number: 29
            -------------------------
            */
        }

        [Test]
        public void TestCallerInfo_Attribute()
        {
            //new TeastableCallerAttribute().TraceMessageAttribute("Test message");
            /*
            message: Test message
            caller member name: TestCallerInfo_Nested
            caller file path: D:\My\cs\CSTest\CSTest\23_CallerInfo\Test.cs
            caller line number: 29
            -------------------------
            */
        }

        private static void TraceMessage(string message,
                                [CallerMemberName] string memberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Debug.WriteLine("message: " + message);
            Debug.WriteLine("caller member name: " + memberName);
            Debug.WriteLine("caller file path: " + sourceFilePath);
            Debug.WriteLine("caller line number: " + sourceLineNumber);
            Debug.WriteLine(new string('-', 25));
        }

        private static void TraceMessage1(string message,
                        [CallerMemberName] string memberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            TraceMessage2(message, memberName, sourceFilePath, sourceLineNumber);
        }

        private static void TraceMessage2(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            TraceMessage(message, memberName, sourceFilePath, sourceLineNumber);
        }
    }

    class CallerAttribute : Attribute
    {
        public CallerAttribute([CallerMemberName] string memberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Debug.WriteLine("caller member name: " + memberName);
            Debug.WriteLine("caller file path: " + sourceFilePath);
            Debug.WriteLine("caller line number: " + sourceLineNumber);
        }
    }

    //class TeastableCallerAttribute
    //{
    //    [Caller]
    //    public void TraceMessageAttribute(string message)
    //    {
    //        Debug.WriteLine("message: " + message);
    //    }
    //}

#endif
}
