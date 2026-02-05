using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._03_CS6
{
    [TestFixture]
    public class _06_EventInitializerTests
    {
#if CS6
        [Test]
        public void Test1()
        {
            EventHandler<EventArgs> log = (o, e) => Debug.WriteLine("hit");
            var user = new User2
            {
                Name = "test",
                //Speaking += log,
            };
            user.Speaking += log;
        }
#endif
    }

    public class User2
    {
        public string Name { get; set; }
        public event EventHandler<EventArgs> Speaking;

        public void Speak()
        {
            if (Speaking != null)
            {
                Speaking(this, EventArgs.Empty);
            }
        }
    }
}
