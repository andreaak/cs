using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _008_MockRepository
    {
        /*
        Класс MockRepository предоставляет еще один синтаксис для создания стабов и, что самое главное, 
        позволяет хранить несколько мок-объектов и проверять более комплексное поведение путем вызова одного метода.
        */

        [Test]
        /*
            Использование MockRepository.Of для создания стабов. 
            Данный синтаксис аналогичен использованию Mock.Of, 
            однако позволяет задавать поведение разных методов не через оператор &&, 
            а путем использования нескольких методов Where:     
        */
        public void MoqTestRepository1()
        {
            var repository = new MockRepository(MockBehavior.Default);
            IFoo mock = repository.Of<IFoo>()
                .Where(ld => ld.Name == "DefaultLogger")
                .Where(ld => ld.DoSomething() == "D:\\Temp")
                .Where(ld => ld.DoSomething2(It.IsAny<string>()) == "C:\\Temp")
                .First();

            Assert.That(mock.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(mock.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(mock.DoSomething2("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }

        [Test]
        /*
            Использование MockRepository для задания поведения нескольких мок-объектов. 
            Предположим, у нас есть более сложный класс SmartLogger, которому требуется две зависимости: ILogWriter и ILogMailer. 
            Наш тестируемый класс при вызове его метода Write должен вызвать методы двух зависимостей:
        */
        public void MoqTestRepository2()
        {
            //var repo = new MockRepository(MockBehavior.Default);
            //var logWriterMock = repo.Create<ILogWriter>();
            //logWriterMock.Setup(lw => lw.Write(It.IsAny<string>()));

            //var logMailerMock = repo.Create<ILogMailer>();
            //logMailerMock.Setup(lm => lm.Send(It.IsAny<MailMessage>()));

            //var smartLogger = new SmartLogger(logWriterMock.Object, logMailerMock.Object);

            //smartLogger.WriteLine("Hello, Logger");

            //repo.Verify();
        }
    }
}
