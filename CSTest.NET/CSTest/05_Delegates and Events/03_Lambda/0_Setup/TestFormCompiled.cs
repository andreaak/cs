namespace CSTest._05_Delegates_and_Events._03_Lambda._0_Setup
{
    public class TestFormCompiled
    {
        /*
        //private Action FormClickFault;
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
        /*

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

        //FormClickLambda?.Invoke()
        private void \u003C\u002Ector\u003Eb__10_0()
        {
          Action action = this.FormClickLambda;
          if (action == null)
            return;
          action();
        }

        //FormClickLambda()
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

        //FormClickDelegate?.Invoke()
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