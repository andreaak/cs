using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._01_Thread
{
    /*
    Существуют две разновидности потоков: приоритетный Foreground и фоновый Background. 
    Отличие между ними заключается в том, что процесс не завершится до тех пор, 
    пока не окончится приоритетный поток, тогда как фоновые потоки завершаются автоматически после окончания всех приоритетных потоков. 
    Foreground - Будет работать после завершения работы первичного потока.
    Background - Завершает работу вместе с первичным потоком.
    По умолчанию создаваемый поток становится приоритетным. 
    Для того чтобы сделать поток фоновым, достаточно присвоить логическое значение true свойству IsBackground. 
    А логическое значение false указывает на то, что поток является приоритетным.
    */

    [TestFixture]
    public class _05_BackgroundTest
    {
        [Test]
        public void TestThreadIsBackground()
        {
            Thread thread = new Thread(Function);

            // IsBackground - устанавливает поток как фоновый. Не ждем завершения вторичного потока в данном случае.
            //По умолчанию свойство IsBackground равно false.
            thread.IsBackground = true;
            thread.Start();

            Thread.Sleep(500);

            Debug.WriteLine("\nЗавершение главного потока.");
            /*
                      .
                      .

            Завершение главного потока.
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
            */
        }

        private static void Function()
        {
            string temp = new string(' ', 10);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Debug.WriteLine(temp + ".");
            }
            Debug.WriteLine(temp + "Завершение вторичного потока.");
        }
    }
}
