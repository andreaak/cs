using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    /*
    Применение к методу атрибута [MethodImplAttribute(MethodImplOptions.Synchronized)] 
    заставляет JIТ-компилятор окружить машинный код метода вызовами Monitor.Enter и Monitor.Exit. 
    Если мы имеем дело с экземплярным методом, он передается указанным методам, запирая неявно открытое запирание. 
    В случае статического метода этим двум методам передается ссылка на объект-тип, 
    потенциально запирая нейтральный по отношению к домену тип. 
    Поэтому использовать данный атрибут не рекомендуется. 
    */

    [TestClass]
    public class _03_MethodImplAttributeTest
    {
        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public static string GetCalendarName()
        {
            return "";
        }
    }
}
