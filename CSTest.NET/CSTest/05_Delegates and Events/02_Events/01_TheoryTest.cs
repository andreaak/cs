using CSTest._05_Delegates_and_Events._02_Events._01_Theory;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    [TestFixture]
    public class _02_TheoryTest
    {
        [Test]
        public void TestEvents1()
        {
            MailManager mm = new MailManager();
            Fax fax = new Fax(mm);

            mm.SimulateNewMail("FromStr", "ToStr", "SubjectStr");
            fax.Unregister(mm);
            mm.SimulateNewMail("FromStr2", "ToStr2", "SubjectStr2");

            /*
            Faxing mail message:
            From=FromStr, To=ToStr, Subject=SubjectStr
             */
        }
    }
}
