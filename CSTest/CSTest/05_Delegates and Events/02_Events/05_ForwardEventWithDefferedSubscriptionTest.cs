using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    [TestFixture]
    public class _05_ForwardEventWithDefferedSubscriptionTest
    {
        [Test]
        public void TestEvents()
        {
            TestForm form = new TestForm();
            form.FormClickFault += Form_FormClickFault;
            form.FormClickLambda += Form_FormLambda;
            form.FormClickDelegate += Form_FormClickDelegate;
            form.InitiateForTests();
            /*
            FormClickFaultAction Click Handler
            Button Click Handler
            Button Click Static Handler
            Form_FormLambda
            Form_FormLambda
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
              testForm.FormClickLambda += new Action((object) this, __methodptr(Form_FormLambda));
              // ISSUE: method pointer
              testForm.FormClickDelegate += new Action((object) this, __methodptr(Form_FormClickDelegate));
              testForm.InitiateForTests();
        } 
        */

        private void Form_FormClickFault()
        {
            Debug.WriteLine("Form_FormClickFault");
        }

        private void Form_FormLambda()
        {
            Debug.WriteLine("Form_FormLambda");
        }

        private void Form_FormClickDelegate()
        {
            Debug.WriteLine("Form_FormClickDelegate");
        }
    }
}
