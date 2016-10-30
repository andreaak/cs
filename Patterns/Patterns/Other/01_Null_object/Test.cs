using System;
using NUnit.Framework;

namespace Patterns.Other._01_Null_object
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestNull()
        {
            Employee e = DB.GetEmployee("Bob");
            if (e.IsTimeToPay(new DateTime()))
                Assert.Fail();
            Assert.AreSame(Employee.Null, e);
        }
    }

    public class DB
    {
        public static Employee GetEmployee(string s)
        {
            return Employee.Null;
        }
    }

    public abstract class Employee
    {
        public abstract bool IsTimeToPay(DateTime time);
        public abstract void Pay();
        public static readonly Employee Null = new NullEmployee();

        private class NullEmployee : Employee
        {
            public override bool IsTimeToPay(DateTime time)
            {
                return false;
            }

            public override void Pay()
            {
            }
        }
    }
}
