// Program 1B
// CIS 200-01
// Fall 2017
// Due: 8/4/17
// By: C2518

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy based off Dr. Wright's 1A solution.
// It creates several different Parcels and prints them in an original list. Then using LINQ sorts the parcels into various lists.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address a1 = new Address("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ",
                "  Louisville   ", "  KY   ", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 04101); // Test Address 4
            Address a5 = new Address("Bobby Lee", "8888 Ooga Booga Ally", "Memphis", "TN", 98963); //Test Address 5
            Address a6 = new Address("Jamie Oliver", "12 Healthy Cook Drive", "New York", "NY", 87455); //Test Address 6
            Address a7 = new Address("Gordon Ramsey", "2022 Raw Chicken Road", "Apt. 22 ", "Los Angeles", "CA", 94049);//Test Address 7
            Address a8 = new Address("Jon Snow", "1711 Winterfell Drive", "Winterfell", "WA", 45888); //Test Address 8

            Letter letter1 = new Letter(a1, a8, 3.95M);                            // Letter test object
            Letter letter2 = new Letter(a2, a4, 1.20M);                            // Test Letter 2
            GroundPackage gp1 = new GroundPackage(a3, a5, 15, 10, 5, 12.5);        // Ground test object
            GroundPackage gp2 = new GroundPackage(a5, a6, 15, 16, 80, 10);         //Test groundpackage 2
            NextDayAirPackage ndap1 = new NextDayAirPackage(a1, a3, 25, 15, 15,    // Next Day test object
                85, 20.50M);
            NextDayAirPackage ndap2 = new NextDayAirPackage(a1, a8, 100, 12, 30,
                10, 10.0M);                                                         //Test nextdayair 2
            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a3, a6, 10, 12, 14,
                76, TwoDayAirPackage.Delivery.Saver);                               //test twodayair 2
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a4, a7, 46.5, 39.5, 28.0, // Two Day test object
                80.5, TwoDayAirPackage.Delivery.Saver);

            List<Parcel> parcels;      // List of test parcels

            parcels = new List<Parcel>();

            parcels.Add(letter1); // Populate list
            parcels.Add(letter2);
            parcels.Add(gp1);
            parcels.Add(gp2);
            parcels.Add(ndap1);
            parcels.Add(ndap2);
            parcels.Add(tdap1);
            parcels.Add(tdap2);

            Console.WriteLine("Original List:");
            Console.WriteLine("====================");
            foreach (Parcel p in parcels)
            {
                Console.WriteLine(p.ToString());
                Console.WriteLine("====================");
            }
            Pause();
            //Holds LINQ results for sorting parcels list by destination zip in descending order.
            var zipSort =
                from p in parcels
                orderby p.DestinationAddress.Zip descending
                select p;

            Console.WriteLine("Sorted by DestZip Descending List:"); //Displays the results
            Console.WriteLine("====================");
            if (zipSort.Any())
                foreach (var element in zipSort)
                {
                    Console.WriteLine(element);
                    Console.WriteLine("====================");
                }
            else
            {
                Console.WriteLine("No zip found");
            }
            Pause();
            //Holds LINQ results for sorting parcels list by cost in ascending order.
            var costSort =
                from p in parcels
                orderby p.CalcCost()
                select p;

            Console.WriteLine("Sorted by Cost List:"); //Displays results
            Console.WriteLine("====================");
            if (costSort.Any())
                foreach (var element in costSort)
                {
                    Console.WriteLine(element);
                    Console.WriteLine("====================");
                }
            else
            {
                Console.WriteLine("No price found");
            }
            Pause();
            //Holds LINQ results for sorting parcels list by parcel type (ascending) and then by cost in descending order.
            var typeCostSort =
                from p in parcels
                orderby p.GetType().ToString(), p.CalcCost() descending
                select p;

            Console.WriteLine("Sorted by Type, Cost List:"); //Displays results
            Console.WriteLine("====================");
            if (typeCostSort.Any())
                foreach (var element in typeCostSort)
                {
                    Console.WriteLine(element);
                    Console.WriteLine("====================");
                }
            else
            {
                Console.WriteLine("No type/cost found");
            }
            Pause();
            //Holds LINQ results for all airpackages that are heavy and sorts by weight(descending).
            var airHeavySort =
                from p in parcels
                where p is AirPackage
                let heavyPack = (AirPackage)p
                where heavyPack.IsHeavy()
                orderby heavyPack.Weight descending
                select heavyPack;

            Console.WriteLine("Heavy Air Package List:"); //Displays results
            Console.WriteLine("====================");
            if (airHeavySort.Any())
                foreach (var element in airHeavySort)
                {
                    Console.WriteLine(element);
                    Console.WriteLine("====================");
                }
            else
            {
                Console.WriteLine("No heavy air package found");
            }
            Pause();

        }


        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();

            Console.Clear(); // Clear screen
        }
    }
}
