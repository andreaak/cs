using System.Diagnostics;
using CSTest._05_Delegates_and_Events._03_Lambda._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._03_Lambda
{
    [TestFixture]
    public class _02_ForwardEventWithDefferedSubscriptionTest
    {
        [Test]
        public void TestEvents()
        {
            TestForm form = new TestForm();
            form.FormClickFault += Form_FormClickFault;
            form.FormClickLambda += Form_FormClickLambda;
            form.FormClickDelegate += Form_FormClickDelegate;
            form.InitiateForTests();
            /*
            FormClickFaultAction Click Handler
            Button Click Handler
            Button Click Static Handler
            Form_FormClickLambda
            Form_FormClickLambda
            6
            Form_FormClickDelegate
            */
        }

        /*
        public _05_ForwardEventWithDefferedSubscriptionTest()
        {
            base.\u002Ector();
        }

        [Test]
        public void TestEvents()
        {
              TestForm testForm = new TestForm();
              // ISSUE: method pointer
              testForm.FormClickFault += new Action((object) this, __methodptr(Form_FormClickFault));
              // ISSUE: method pointer
              testForm.FormClickLambda += new Action((object) this, __methodptr(Form_FormClickLambda));
              // ISSUE: method pointer
              testForm.FormClickDelegate += new Action((object) this, __methodptr(Form_FormClickDelegate));
              testForm.InitiateForTests();
        } 
        */

        private void Form_FormClickFault()
        {
            Debug.WriteLine("Form_FormClickFault");
        }

        private void Form_FormClickLambda()
        {
            Debug.WriteLine("Form_FormClickLambda");
        }

        private void Form_FormClickDelegate()
        {
            Debug.WriteLine("Form_FormClickDelegate");
        }
    }
}
