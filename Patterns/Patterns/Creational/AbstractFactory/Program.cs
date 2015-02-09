using System;
using FactorySample.Facilities;
using FactorySample.Factory;

namespace FactorySample
{
    class Program
    {
        static void Main(string[] args)
        {
            VolkswagenFacility facility = new RussianVolkswagenFacility();

            facility.GetCar("Golf");
            Console.WriteLine();

            facility.GetCar("Passat");
            Console.WriteLine();

            facility.GetCar("Tiguan");
            Console.WriteLine();

            facility.GetCar("Touareg");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
