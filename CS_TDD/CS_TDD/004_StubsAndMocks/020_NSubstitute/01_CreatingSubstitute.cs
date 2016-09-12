using System;
using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    public class _01_CreatingSubstitute
    {
        [Test]
        public void TestCreatingSubstituteForInterface()
        {
            var substitute = Substitute.For<ICommand>();

            /*
            There are times when you want to substitute for multiple types. 
            The best example of this is when you have code that works with a type, 
            then checks whether it implements IDisposable and disposes of it if it doesn’t. 
             */
            var command = Substitute.For<ICommand, IDisposable>();
            var runner = new CommandRunner(command);

            runner.DoSomething();

            command.Received().Execute();
            //((IDisposable)command).Received().Dispose();
        }

        [Test]
        public void TestCreatingSubstituteForClasses()
        {
            /*
            Substituting for classes can have some nasty side-effects. 
            For starters, NSubstitute can only work with virtual members of the class, 
            so any non-virtual code in the class will actually execute! 
            If you try to substitute for your class that formats your hard drive in the constructor 
            or in a non-virtual property setter then you’re asking for trouble. 
            If possible, stick to substituting interfaces.
             */
            var someClass = Substitute.For<Controller>(5, "hello world");
            var someClass2 = Substitute.For<Controller>(new object[] { new string[] { "A", "B" } });
        }

        [Test]
        public void TestCreatingSubstituteForDelegate()
        {
            var func = Substitute.For<Func<string>>();

            func().Returns("hello");
            Assert.AreEqual("hello", func());
        }
    }
}
