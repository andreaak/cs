using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User
{
    /*
    Применение к методу атрибута [MethodImplAttribute(MethodImplOptions.Synchronized)] 
    заставляет JIТ-компилятор окружить машинный код метода вызовами Monitor.Enter и Monitor.Exit. 
    Если мы имеем дело с экземплярным методом, он передается указанным методам, запирая неявно открытое запирание. 
    В случае статического метода этим двум методам передается ссылка на объект-тип, 
    потенциально запирая нейтральный по отношению к домену тип. 
    Поэтому использовать данный атрибут не рекомендуется. 
    */

    [TestFixture]
    public class _03_MethodImplAttributeTest
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetCalendarName()
        {
            return "";
        }
    }
}
