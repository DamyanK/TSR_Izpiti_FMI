using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izpit_PU_2019_2
{
    public class Customer
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int Groceries { get; set; }
        public double Total { get; set; }
        public int Rate { get; set; }

        public Customer(string name, string date, int groceries, double total, int rate)
        {
            this.Name = StringLimiter(name, 40);
            this.Date = date;
            this.Groceries = groceries;
            this.Total = total;
            this.Rate = rate;
        }

        public string StringLimiter(string input, int limit)
        {
            if (input.Length <= limit)
            {
                return input;
            }
            return input.Substring(0, limit);
        }
    }
    class Program
    {
        static int NumberOfCustomers()
        {
            int N;
            do
            {
                Console.Write("Enter N: ");
                N = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            } while (N < 1 || N > 5000);

            return N;
        }

        static Customer CreateCustomer()
        {
            Console.Write("Name and Surname: ");
            string name = Console.ReadLine();

            Console.Write("Date(DD.MM.YYYY): ");
            string date = Console.ReadLine();

            int groceries;
            do
            {
                Console.Write("Enter the count of groceries: ");
                groceries = Convert.ToInt32(Console.ReadLine());
            } while (groceries < 1 || groceries > 9999);

            Console.Write("Enter total: ");
            double total = Convert.ToDouble(Console.ReadLine());

            int rate = 1;

            if (groceries >= 100 && groceries <= 299)
            {
                rate = 2;
            }
            if (groceries >= 300 && groceries <= 499)
            {
                rate = 3;
            }
            if (groceries >= 500 && groceries <= 999)
            {
                rate = 4;
            }
            if (groceries >= 1000)
            {
                rate = 5;
            }

            return new Customer(name, date, groceries, total, rate);
        }

        static Customer[] SortByName(Customer[] custs)
        {
            for (int i = 0; i < custs.Length - 1; i++)
            {
                for (int j = i + 1; j < custs.Length; j++)
                {
                    if (String.Compare(custs[i].Name, custs[j].Name) > 0)
                    {
                        Customer temp = custs[i];
                        custs[i] = custs[j];
                        custs[j] = temp;
                    }
                }
            }
            return custs;
        }

        static void PrintSortedByName(Customer[] custs)
        {
            foreach (var item in custs)
            {
                string stars = "";

                for (int i = 1; i <= item.Rate; i++)
                {
                    stars += "*";
                }

                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", item.Name, item.Groceries, item.Total,
                    item.Date, stars);
            }
        }

        static void Main(string[] args)
        {
            int N = NumberOfCustomers();

            Customer[] custs = new Customer[N];

            for (int i = 0; i < custs.Length; i++)
            {
                custs[i] = CreateCustomer();
                Console.Clear();
            }

            custs = SortByName(custs);
            PrintSortedByName(custs);
            Console.ReadKey(true);

            Console.WriteLine("Customers with **: ");
            var filtered = custs.Where(x => x.Rate == 2).ToArray();
            filtered.OrderByDescending(x => x.Total).ThenBy(x => x.Name);
            PrintSortedByName(filtered);

            Console.ReadKey(true);
            Console.Write("Check a rate: \n1 - * \n2 - ** \n3 - *** \n4 - **** \n5 - ***** \n> ");
            int controlRate = Convert.ToInt32(Console.ReadLine());

            var yearCount = new Dictionary<int, int>();

            foreach (var item in custs)
            {
                if (controlRate == item.Rate)
                {
                    if (yearCount.ContainsKey(Convert.ToInt32(item.Date.Substring(6, 4))))
                    {
                        yearCount[Convert.ToInt32(item.Date.Substring(6, 4))]++;
                    }
                    else
                    {
                        yearCount[Convert.ToInt32(item.Date.Substring(6, 4))] = 1;
                    }
                }
            }

            yearCount.OrderBy(x => x.Key);
            foreach (KeyValuePair<int, int> kv in yearCount)
            {
                Console.WriteLine("{0} - {1}", kv.Key, kv.Value);
            }

            Console.ReadKey(true);
        }
    }
}
