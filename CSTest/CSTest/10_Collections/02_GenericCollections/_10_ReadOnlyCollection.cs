using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CSTest._10_Collections._02_GenericCollections
{
    [TestClass]
    public partial class _10_ReadOnlyCollection
    {
        [TestMethod]
        public void Test()
        {
            List<string> dinosaurs = new List<string>();

            dinosaurs.Add("Tyrannosaurus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Compsognathus");

            ReadOnlyCollection<string> readOnlyDinosaurs =
                new ReadOnlyCollection<string>(dinosaurs);

            Debug.WriteLine(string.Empty);
            foreach (string dinosaur in readOnlyDinosaurs)
            {
                Debug.WriteLine(dinosaur);
            }

            Debug.WriteLine(string.Format("\nReadOnlyDinosaurs Count: {0}", readOnlyDinosaurs.Count));
            Debug.WriteLine(string.Format("\nCount: {0}", readOnlyDinosaurs.Count));

            Debug.WriteLine(string.Format("\nContains(\"Deinonychus\"): {0}",
                readOnlyDinosaurs.Contains("Deinonychus")));

            Debug.WriteLine(string.Format("\nreadOnlyDinosaurs[3]: {0}",
                readOnlyDinosaurs[3]));

            Debug.WriteLine(string.Format("\nIndexOf(\"Compsognathus\"): {0}",
                readOnlyDinosaurs.IndexOf("Compsognathus")));

            Debug.WriteLine(string.Format("Insert into the wrapped List:"));
            Debug.WriteLine(string.Format("Insert(2, \"Oviraptor\")"));
            dinosaurs.Insert(2, "Oviraptor");

            Debug.WriteLine(string.Empty);
            Debug.WriteLine(string.Format("\nReadOnlyDinosaurs Count: {0}", readOnlyDinosaurs.Count));
            foreach (string dinosaur in readOnlyDinosaurs)
            {
                Debug.WriteLine(dinosaur);
            }

            string[] dinoArray = new string[readOnlyDinosaurs.Count + 2];
            readOnlyDinosaurs.CopyTo(dinoArray, 1);

            Debug.WriteLine(string.Format("\nCopied array has {0} elements:",
                dinoArray.Length));
            foreach (string dinosaur in dinoArray)
            {
                Debug.WriteLine(string.Format("\"{0}\"", dinosaur));
            }

            /*
            Tyrannosaurus
            Amargasaurus
            Deinonychus
            Compsognathus

            Count: 4

            Contains("Deinonychus"): True

            readOnlyDinosaurs[3]: Compsognathus

            IndexOf("Compsognathus"): 3
            Insert into the wrapped List:
            Insert(2, "Oviraptor")

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
    }
}
