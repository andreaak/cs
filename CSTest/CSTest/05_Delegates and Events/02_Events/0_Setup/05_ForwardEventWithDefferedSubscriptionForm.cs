using System;
using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    public class TestButton
    {
        public event Action Click;

        public void OnClick()
        {
            Click?.Invoke();
        }
    }

    public class TestObj
    {
        public void Run()
        {

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
                        /*
                         public static T CompareExchange<T>(ref T location1, T value, T comparand)

                         location1 - The destination, whose value is compared with comparand and possibly replaced. 
                         value - The value that replaces the destination value if the comparison results in equality.
                         comparand - The value that is compared to the value at location1.
                         Return Value - The original value in location1.
                        */
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

        private TestButton button;
        private TestObj obj = new TestObj();
        private int i = 5;


        public TestForm()
        {
            button = new TestButton();

            //FormClickFault += FormClickFaultAction_Click;
            button.Click += FormClickFault;
            button.Click += Button_Click;
            button.Click += Button_Click_Static;
            button.Click += () => FormClickLambda?.Invoke();
            button.Click += () => FormClickLambda();
            button.Click += () =>
            {
                obj.Run();
                i ++;
                Debug.WriteLine(i);

            };
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
        private Action FormClickFault;
        private Action FormClickLambda;
        private Action FormClickDelegate;
        private TestButton button;

        public TestForm()
        {
            base.\u002Ector();
            this.button = new TestButton();

            // FormClickFault += FormClickFaultAction_Click;
            this.FormClickFault += new Action((object) this, __methodptr(FormClickFaultAction_Click));
            this.button.Click += this.FormClickFault;

            // button.Click += Button_Click;
            this.button.Click += new Action((object) this, __methodptr(Button_Click));

            // button.Click += Button_Click_Static;
            this.button.Click += new Action((object) null, __methodptr(Button_Click_Static));

            // button.Click += () => FormClickLambda?.Invoke();
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_0));

            // button.Click += () => FormClickLambda();
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_1));
            
            //button.Click += () =>
            //{
            //    obj.Run();
            //    i ++;
            //    Debug.WriteLine(i);
            //};
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__12_2));

            // button.Click += delegate { FormClickDelegate?.Invoke(); };
            this.button.Click += new Action((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_2));
        }

        private void \u003C\u002Ector\u003Eb__10_0()
        {
          Action action = this.FormClickLambda;
          if (action == null)
            return;
          action();
        }

        private void \u003C\u002Ector\u003Eb__10_1()
        {
          this.FormClickLambda();
        }

        private void \u003C\u002Ector\u003Eb__12_2()
        {
            this.obj.Run();
            this.i = this.i + 1;
            Debug.WriteLine((object) this.i);
        }

        private void \u003C\u002Ector\u003Eb__10_2()
        {
          Action action = this.FormClickDelegate;
          if (action == null)
            return;
          action();
        }

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

        */
    }
}