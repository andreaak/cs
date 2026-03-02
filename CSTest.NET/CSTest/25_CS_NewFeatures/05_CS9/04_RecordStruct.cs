using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
#if CS9

    readonly record struct Angle(int Degrees, int Minutes, int Seconds, string? Name = null)
    {
        public Angle(int degrees) : this(degrees, 0, 0)//this обязательный
        {
            
        }
    };


    [TestFixture]
    public class _04_RecordStruct
    {



        [Test]
        public void TestRecordStruct1()
        {
            (int degrees, int minutes, int seconds) = (90, 0, 0);

            // The constructor is generated using positional parameters
            Angle angle = new(degrees, minutes, seconds);


            // Records include a ToString() implementation
            // that returns:
            //   "Angle { Degrees = 90, Minutes = 0, Seconds = 0, Name =  }"
            Debug.WriteLine(angle.ToString());


            // Records have a deconstructor using the 
            // positional parameters.
            if (angle is (int, int, int, string) angleData)
            {
                Debug.WriteLine(angle.Degrees == angleData.Degrees);
                Debug.WriteLine(angle.Minutes == angleData.Minutes);
                Debug.WriteLine(angle.Seconds == angleData.Seconds);
            }

            Angle copy = new(degrees, minutes, seconds);

            // Records provide a custom equality operator.
            Debug.WriteLine(angle == copy);


            // The with operator is the equivalent of
            // Angle copy = new(degrees, minutes, seconds);
            copy = angle with { };
            Debug.WriteLine(angle == copy);


            // The with operator has object initializer type
            // syntax for instantiating a modified copy.
            Angle modifiedCopy = angle with { Degrees = 180 };
            Debug.WriteLine(angle != modifiedCopy);
        }

        [Test]
        public void TestRecordStruct2Copy()
        {
            Angle p1 = new Angle(2, 3, 4);
            Angle p2 = p1 with { Degrees = 4 };

            Debug.WriteLine(p1);         // Point { X = 3, Y = 3 }
            Debug.WriteLine(p2);         // Point { X = 3, Y = 4 }
        }

        [Test]
        public void TestRecordStruct3Equality()
        {
            var point1 = new Angle(2, 3, 4);
            var point2 = new Angle(2, 3, 4);

            Debug.WriteLine(point1.Equals(point2));//true
            Debug.WriteLine(point1 == point2);//true
            Debug.WriteLine(Equals(point1, point2));//true

            var point3 = new Angle(2, 3, 4);
            var point4 = new Angle(2, 3, 4);

            Debug.WriteLine(point3.Equals(point4));//False
            Debug.WriteLine(point3 == point4);//False
            Debug.WriteLine(Equals(point3, point4));//False
        }



    }
#endif
}