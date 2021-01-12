using System;
using System.Collections.Generic;
using System.Linq;

namespace IzpitPU_2019_1
{
    public enum StarType : short
    {
        HyperGiant = 1,
        SuperGiant,
        BrightGiant,
        Giant,
        SubGiant,
        Dwarf,
        SubDwarf,
        RedDwarf,
        BrownDward
    }

    public class Star
    {
        //private string _Name;
        //public string GetName {return _Name;}
        //public void SetName(string a) {_Name = a;}
        public string Name { get; set; }
        public double Distance { get; set; }
        public StarType Classification { get; set; }
        public double Mass { get; set; }
        public string Constellation { get; set; }

        public Star(string name, double distance, StarType classification, double mass,
            string constellation)
        {
            this.Name = StringLimiter(name, 20);
            this.Distance = distance;
            this.Classification = classification;
            this.Mass = mass;
            this.Constellation = StringLimiter(constellation, 30);
        }

        public string StringLimiter(string input, int index)
        {
            if (input.Length < index)
                return input;
            return input.Substring(0, index);
        }
    }

    class Program
    {
        private static int StarsCount()
        {
            int n;
            do
            {
                Console.Write("Enter the amount of stars: ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            } while (n < 1 || n > 2000);

            return n;
        }

        private static List<Star> EnterStar()
        {
            int n = StarsCount();

            var stars = new List<Star>();

            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                double distance;
                do
                {
                    Console.Write("Enter the distance: ");
                    distance = Convert.ToDouble(Console.ReadLine());
                } while (distance <= 0);

                int classification;
                do
                {
                    Console.Write("Enter the classification: \n1 - HyperGiant" +
                        "\n2 - SuperGiant \n3 - BrightGiant \n4 - Giant" +
                        "\n5 - SubGiant \n6 - Dwarf \n7 - SubDwarf" +
                        "\n8 - RedDwarf \n9 - BrownDwarf \n> ");
                    classification = Convert.ToInt32(Console.ReadLine());
                } while (classification < 1 || classification > 9);

                double mass;
                do
                {
                    Console.Write("Enter the mass: ");
                    mass = Convert.ToDouble(Console.ReadLine());
                } while (mass <= 0);

                Console.Write("Enter the constellation: ");
                string constellation = Console.ReadLine();

                Star star =
                    new Star(name, distance, (StarType)classification, mass, constellation);
                stars.Add(star);
                Console.Clear();
            }

            return stars;
        }

        static List<Star> SortByDistance(List<Star> stars)
        {
            for (int i = 0; i < stars.Count - 1; i++)
            {
                for (int j = i + 1; j < stars.Count; j++)
                {
                    if (stars[i].Distance > stars[j].Distance)
                    {
                        Star tmp = stars[i];
                        stars[i] = stars[j];
                        stars[j] = tmp;
                    }
                }
            }
            return stars;
        }

        static List<Star> SortByConstThenByMass(List<Star> stars)
        {
            List<Star> ordered = stars
               .OrderBy(x => x.Constellation)
               .ThenByDescending(x => x.Mass)
               .ToList();
            return ordered;
        }

        static void AverageConstMass(List<Star> stars)
        {
            var grouped = stars.GroupBy(x => x.Constellation);
            //[Кентавър: <Проксима К., Алфа К.>]
            //[Лъв: <Звездичка, Небула>]
            //[Крава: <MyStar>]

            Console.WriteLine();
            foreach (var star in grouped)
            {
                Console.WriteLine("Constellation: {0} -> {1} average mass.", star.Key,
                    star.Average(x => x.Mass));
            }
        }

        static void Main(string[] args)
        {
            var stars = EnterStar();
            Print(SortByDistance(stars));
            Console.WriteLine();
            Print(SortByConstThenByMass(stars));
            AverageConstMass(stars);
            Console.ReadKey(true);
        }

        public static void Print(List<Star> stars)
        {
            foreach (Star el in stars)
            {
                Console.WriteLine("{0}, {1}св.г., {2}, {3}сл.м., {4}",
                    el.Name, el.Distance, el.Classification, el.Mass, el.Constellation);
            }
        }
    }
}
