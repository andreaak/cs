using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _05_Is
    {
#if CS7
        [Test]
        public void TestIs_1()
        {
            Performer p = new Performer();
            Actor a = new Actor("Eddie Murphy", 54, "male",
                                "Coming to America", 2017);
            Musician m = new Musician("Jen", 25, "female", "singing",
                                      "Pop");

            // Is expressions pattern

            if (a is Performer p1)
                Debug.WriteLine($"This actor {p1.Name} is a performer");
            else
                Debug.WriteLine("This actor is a performer");

            if (a is Musician)
                Debug.WriteLine("This actor is a musician");
            else
                Debug.WriteLine("This actor is not a musician");

            /*
            This actor Eddie Murphy is a performer
            This actor is not a musician
            */
        }


#endif
    }

    #region Classes
    // Performer class      
    class Performer
    {
        public string Name { get; set; }
        public short Age { get; set; }
        public string Gender { get; set; }
    }

    // Actor class      
    class Actor : Performer
    {
        public string BestMovie { get; set; }
        public short Year { get; set; }

        public Actor(string name, short age, string gender,
                     string bestMovie, short year)
        {
            Name = name;
            Age = age;
            Gender = gender;
            BestMovie = bestMovie;
            Year = year;
        }
    }
    // Musician class      
    class Musician : Performer
    {
        private string Interest;
        private string Format;
        public Musician(string name, short age, string gender,
                        string interest, string format)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Interest = interest;
            Format = format;
        }
    }
    #endregion
}
