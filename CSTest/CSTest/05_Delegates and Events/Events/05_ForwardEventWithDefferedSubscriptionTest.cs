using System;
using System.Diagnostics;
using System.Threading;
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
            form.FormClickFault += Form_FormClickFault;
            form.FormClick += Form_FormClick;
            form.FormClickDelegate += Form_FormClickDelegate;
            form.InitiateForTests();
            /*
            Button Click Handler
            Form_FormClick
            Form_FormClick
            Form_FormClickDelegate
            */
        }
        private void Form_FormClickFault()
        {
            Debug.WriteLine("Form_FormClickFault");
        }

        private void Form_FormClick()
        {
            Debug.WriteLine("Form_FormClick");
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
        //public event Action FormClickFault;
        //Decompiled
        private Action FormClickFaultAction;
        public event Action FormClickFault
        {
            add
            {
                Action action = this.FormClickFaultAction;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickFaultAction, (Action)Delegate.Combine((Delegate)comparand, (Delegate)value), comparand);
                    /*
                     location1 - The destination, whose value is compared with comparand and possibly replaced. 
                     value - The value that replaces the destination value if the comparison results in equality.
                     comparand - The value that is compared to the value at location1.
                     Return Value - The original value in location1.
                    */
                }
                while (action != comparand);
            }
            remove
            {
                Action action = this.FormClickFaultAction;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClickFaultAction, (Action)Delegate.Remove((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
        }

        public event Action FormClick;
        public event Action FormClickDelegate;

        TestButton button;

        public TestForm()
        {
            button = new TestButton();

            //button.Click += FormClickFault;
            button.Click += this.FormClickFaultAction;
            button.Click += Button_Click;
            button.Click += () => FormClick?.Invoke();
            button.Click += () => FormClick();
            button.Click += delegate { FormClickDelegate?.Invoke(); };
        }

        public void InitiateForTests()
        {
            button.OnClick();
        }

        private void Button_Click()
        {
            Debug.WriteLine("Button Click Handler");
        }

        /*
        Decompiled

        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Action FormClickFault;
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Action FormClick;
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

        public event Action FormClick
        {
            add
            {
                Action action = this.FormClick;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClick, (Action)Delegate.Combine((Delegate)comparand, (Delegate)value), comparand);
                }
                while (action != comparand);
            }
            remove
            {
                Action action = this.FormClick;
                Action comparand;
                do
                {
                    comparand = action;
                    action = Interlocked.CompareExchange<Action>(ref this.FormClick, (Action)Delegate.Remove((Delegate)comparand, (Delegate)value), comparand);
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
            this.button.Click += this.FormClickFault;
            // ISSUE: method pointer
            this.button.Click += new Action((object)this, __methodptr(Button_Click));
            // ISSUE: method pointer
            this.button.Click += new Action((object)this, __methodptr(\u003C\u002Ector\u003Eb__10_0));
            // ISSUE: method pointer
            this.button.Click += new Action((object)this, __methodptr(\u003C\u002Ector\u003Eb__10_1));
            // ISSUE: method pointer
            this.button.Click += new Action((object)this, __methodptr(\u003C\u002Ector\u003Eb__10_2));
        }

        [CompilerGenerated]
        private void \u003C\u002Ector\u003Eb__10_0()
        {
            Action action = this.FormClick;
            if (action == null)
                return;
            action();
        }

        [CompilerGenerated]
        private void \u003C\u002Ector\u003Eb__10_1()
        {
            this.FormClick();
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
