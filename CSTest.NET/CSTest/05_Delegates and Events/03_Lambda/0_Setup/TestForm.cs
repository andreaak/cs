using System;
using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._03_Lambda._0_Setup
{
    public class TestForm
    {
        public event Action FormClickFault;
        public event Action FormClickLambda;
        public event Action FormClickDelegate;

        private TestButton button;
        private TestObj obj = new TestObj();
        private int i = 5;


        public TestForm()
        {
            button = new TestButton();

            FormClickFault += FormClickFaultAction_Click;
            button.Click += FormClickFault;
            button.Click += Button_Click;
            button.Click += Button_Click_Static;
            button.Click += () => FormClickLambda?.Invoke();
            button.Click += () => FormClickLambda();
            button.Click += () =>
            {
                obj.Run();
                i++;
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
    }
}