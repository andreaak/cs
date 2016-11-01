using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events.Events
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
            Form_FormClickDelegat
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

    public class TestButton
    {
        public event Action Click;

        public void OnClick()
        {
            Click?.Invoke();
        }
    }

    public class TestForm
    {
        public event Action FormClickFault;
        //Decompiled
        //private Action FormClickFaultAction;
        //public event Action FormClickFault
        //{
        //    add
        //    {
        //        Action action = this.FormClickFaultAction;
        //        Action comparand;
        //        do
        //        {
        //            comparand = action;
        //            action = Interlocked.CompareExchange<Action>(ref this.FormClickFaultAction, (Action)Delegate.Combine((Delegate)comparand, (Delegate)value), comparand);
        //            /*
        //             public static T CompareExchange<T>(ref T location1, T value, T comparand)

        //             location1 - The destination, whose value is compared with comparand and possibly replaced. 
        //             value - The value that replaces the destination value if the comparison results in equality.
        //             comparand - The value that is compared to the value at location1.
        //             Return Value - The original value in location1.
        //            */
        //        }
        //        while (action != comparand);
        //    }
        //    remove
        //    {
        //        Action action = this.FormClickFaultAction;
        //        Action comparand;
        //        do
        //        {
        //            comparand = action;
        //            action = Interlocked.CompareExchange<Action>(ref this.FormClickFaultAction, (Action)Delegate.Remove((Delegate)comparand, (Delegate)value), comparand);
        //        }
        //        while (action != comparand);
        //    }
        //}

        public event Action FormClickLambda;
        public event Action FormClickDelegate;

        TestButton button;

        public TestForm()
        {
            button = new TestButton();

            //button.Click += FormClickFault;
            FormClickFault += FormClickFaultAction_Click;
            button.Click += FormClickFault;
            button.Click += Button_Click;
            button.Click += Button_Click_Static;
            button.Click += () => FormClickLambda?.Invoke();
            button.Click += () => FormClickLambda();
            button.Click += delegate { FormClickDelegate?.Invoke(); };
        }

        public void InitiateForTests()
        {
            button.OnClick();
        }

        private void FormClickFaultAction_Click()
        {
            Debug.WriteLine("FormClickFaultAction Click Handler");
        }

        private void Button_Click()
        {
            Debug.WriteLine("Button Click Handler");
        }

        private static void Button_Click_Static()
        {
            Debug.WriteLine("Button Click Static Handler");
        }

        /*
        Decompiled

        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Action FormClickFault;
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Action FormClickLambda;
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Action FormClickDelegate;
        private TestButton button;

        public event Action FormClickFault
        {
            add
            {
                Action action = this.FormClickFault;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickFault, (Action)Delegate.Combine((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
            remove
            {
                Action action = this.FormClickFault;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickFault, (Action)Delegate.Remove((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
        }

        public event Action FormClickLambda
        {
          add
          {
            Action action = this.FormClickLambda;
            Action comparand;
            do
            {
              comparand = action;
              action = Interlocked.CompareExchange<Action>(ref this.FormClickLambda, (Action) Delegate.Combine((Delegate) comparand, (Delegate) value), comparand);
            }
            while (action != comparand);
          }
          remove
          {
            Action action = this.FormClickLambda;
            Action comparand;
            do
            {
              comparand = action;
              action = Interlocked.CompareExchange<Action>(ref this.FormClickLambda, (Action) Delegate.Remove((Delegate) comparand, (Delegate) value), comparand);
            }
            while (action != comparand);
          }
        }

        public event Action FormClickDelegate
        {
            add
            {
                Action action = this.FormClickDelegate;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickDelegate, (Action)Delegate.Combine((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
            remove
            {
                Action action = this.FormClickDelegate;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickDelegate, (Action)Delegate.Remove((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
        }

        public TestForm()
        {
            base.\u002Ector();
            this.button = new TestButton();
            // ISSUE: method pointer
            this.FormClickFault += new Action((object) this, __methodptr(FormClickFaultAction_Click));
            this.button.Click += this.FormClickFault;
            // ISSUE: method pointer
            this.button.Click += new Action((object) this, __methodptr(Button_Click));
            // ISSUE: method pointer
            this.button.Click += new Action((object) null, __methodptr(Button_Click_Static));
            // ISSUE: method pointer
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_0));
            // ISSUE: method pointer
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_1));
            // ISSUE: method pointer
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_2));
        }

        [CompilerGenerated]
        private void \u003C\u002Ector\u003Eb__10_0()
        {
          Action action = this.FormClickLambda;
          if (action == null)
            return;
          action();
        }

        [CompilerGenerated]
        private void \u003C\u002Ector\u003Eb__10_1()
        {
          this.FormClickLambda();
        }

        [CompilerGenerated]
        private void \u003C\u002Ector\u003Eb__10_2()
        {
          Action action = this.FormClickDelegate;
          if (action == null)
            return;
          action();
        }
        */
    }
}
