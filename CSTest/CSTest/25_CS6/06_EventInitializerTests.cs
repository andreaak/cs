﻿using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _06_EventInitializerTests
    {
        public class User
        {
            public string Name { get; set;}
            public event EventHandler<EventArgs> Speaking;

            public void Speak()
            {
                if(Speaking != null)
                {
                    Speaking(this, EventArgs.Empty);
                }
            }

        }


#if CS6
        [Test]
        public void Test1()
        {
            EventHandler<EventArgs> log = (o, e) => Debug.WriteLine("hit");
            var user = new User
            {
                Name = "test",
                //Speaking += log,
            };
            user.Speaking += log;
        }
#endif
    }
}