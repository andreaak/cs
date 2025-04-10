using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections
{
    [TestFixture]
    public partial class _10_ReadOnlyCollection
    {
        [Test]
        public void TestReadOnlyCollection1()
        {
            List<string> dinosaurs = new List<string>();

            dinosaurs.Add("Tyrannosaurus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Compsognathus");

            ReadOnlyCollection<string> readOnly = new ReadOnlyCollection<string>(dinosaurs);

            Debug.WriteLine(string.Empty);
            foreach (string dinosaur in readOnly)
            {
                Debug.WriteLine(dinosaur);
            }

            Debug.WriteLine(string.Empty);
            Debug.WriteLine(string.Format("Base Count: {0}", dinosaurs.Count));
            Debug.WriteLine(string.Format("ReadOnly Count: {0}", readOnly.Count));

            Debug.WriteLine(string.Format("ReadOnly Contains(\"Deinonychus\"): {0}",
                readOnly.Contains("Deinonychus")));

            Debug.WriteLine(string.Format("ReadOnly[3]: {0}",
                readOnly[3]));

            Debug.WriteLine(string.Format("ReadOnly IndexOf(\"Compsognathus\"): {0}",
                readOnly.IndexOf("Compsognathus")));

            Debug.WriteLine(string.Empty);
            Debug.WriteLine(string.Format("Insert into the wrapped List:"));
            Debug.WriteLine(string.Format("Insert(2, \"Oviraptor\")"));
            dinosaurs.Insert(2, "Oviraptor");
            Debug.WriteLine(string.Format("ReadOnly Count: {0}", readOnly.Count));
            foreach (string dinosaur in readOnly)
            {
                Debug.WriteLine(dinosaur);
            }

            Debug.WriteLine(string.Empty);
            string[] dinoArray = new string[readOnly.Count + 2];
            readOnly.CopyTo(dinoArray, 1);

            Debug.WriteLine(string.Format("Copied array has {0} elements:", dinoArray.Length));
            foreach (string dinosaur in dinoArray)
            {
                Debug.WriteLine(string.Format("\"{0}\"", dinosaur));
            }

            /*
            Tyrannosaurus
            Amargasaurus
            Deinonychus
            Compsognathus

            Base Count: 4
            ReadOnly Count: 4
            ReadOnly Contains("Deinonychus"): True
            ReadOnly[3]: Compsognathus
            ReadOnly IndexOf("Compsognathus"): 3

            Insert into the wrapped List:
            Insert(2, "Oviraptor")
            ReadOnly Count: 5
            Tyrannosaurus
            Amargasaurus
            Oviraptor
            Deinonychus
            Compsognathus

            Copied array has 7 elements:
            ""
            "Tyrannosaurus"
            "Amargasaurus"
            "Oviraptor"
            "Deinonychus"
            "Compsognathus"
            ""
             */
        }

        [Test]
        public void TestReadOnlyCollection2()
        {
            List<string> dinosaurs = new List<string>();

            dinosaurs.Add("Tyrannosaurus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Compsognathus");

            ReadOnlyCollection<string> readOnly = new ReadOnlyCollection<string>(dinosaurs);

            Debug.WriteLine("ReadOnly as ICollection: " + (ICollection<string>)readOnly);
            Debug.WriteLine("ReadOnly as List: " + (IList<string>)readOnly);

            var list = readOnly as IList<string>;
            //list.Add("Test");//NotSupportedException Additional information: Collection is read-only.


            list = readOnly.ToList();
            list.Add("Test2");
            foreach (string dinosaur in readOnly)
            {
                Debug.WriteLine(dinosaur);
            }

            /*
            ReadOnly as ICollection: System.Collections.ObjectModel.ReadOnlyCollection`1[System.String]
            ReadOnly as List: System.Collections.ObjectModel.ReadOnlyCollection`1[System.String]
            Tyrannosaurus
            Amargasaurus
            Deinonychus
            Compsognathus
            */
        }
    }
}
