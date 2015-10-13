using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSTest._04_Class._02_Methods
{
    class _02_Parameters
    {
        public void DefaultParameters(int i
            , int j = 5
            , TestStruct testStruct = new TestStruct()
            , TestStruct testStruct2 = default(TestStruct)
            , string testString = "GGGG"
            , object obj = default(object)//null
            //, object obj = new object()//Default parameter value for 'obj' must be a compile-time constant
            //, TestClass testClass = new TestClass()//Default parameter value for 'testClass' must be a compile-time constant
            , TestClass testClass = null
            , TestClass testClass2 = default(TestClass)//null
            //, int k //Optional parameters must appear after all required parameters
            , params double[] prm//must be in the last position
            )
        {
            
            
            Debug.WriteLine("i = " + i);
            Debug.WriteLine("j = " + j);
            Debug.WriteLine("testStruct = " + testStruct);
            Debug.WriteLine("testStruct2 = " + testStruct2);
            Debug.WriteLine("testString = " + testString);
            Debug.WriteLine("obj = " + obj);
            Debug.WriteLine("testClass = " + testClass);
            Debug.WriteLine("testClass2 = " + testClass2);

            if (prm == null)
            {
                Debug.WriteLine("params = null");
            }
            else if (prm.Length == 0)
            {
                Debug.WriteLine("params = empty array");
            }
            else
            {
                foreach (var item in prm)
                {
                    Debug.WriteLine("item = " + item);
                }
            }
            Debug.WriteLine("-----------------");

            List<int> tts = new List<int>();
            tts.Add(10);
            if(tts.Count != 0)
            {
                Debug.WriteLine(tts[0].ToString());
            }
            if (tts.Any())
            {
                Debug.WriteLine(tts[0].ToString());
            }
        }

    }

    struct TestStruct
    {
    
    }

    class TestClass
    {
    
    }
}
