using System.Diagnostics;

namespace CSTest._09_Array._0_Setup
{
    public interface IAnimal
    {
        void Voice();
    }

    public class Dog : IAnimal
    {
        public void Voice()
        {
            Debug.WriteLine("Gav-Gav");
        }

        public void Jump()
        {
            Debug.WriteLine("Jump");
        }
    }
}
