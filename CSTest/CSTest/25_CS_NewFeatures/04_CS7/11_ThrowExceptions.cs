using NUnit.Framework;
using System;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _11_ThrowExceptions
    {
#if CS7
        [Test]
        public void TestThrowExceptions_1()
        {
            /*

            */
        }

    }

    public class Person2
    {
        public string Name { get; }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => _surname = value ??
                throw new ArgumentNullException(paramName: nameof(Surname),
                    message: "New Surname must not be null");
        }

        // Null Coalescing Expression.
        public Person2(string name) => Name = name ??
                throw new ArgumentException(name);

        // Conditional Expressions
        public string GetFirstName()
        {
            var parts = Name.Split(new string[] { " " }, StringSplitOptions.None);
            return (parts.Length > 0) ? parts[0] :
                throw new InvalidOperationException("No name");
        }

        // Lambda Expression    
        public string GetLastName() =>
            throw new NotImplementedException();
    }
#endif
}
