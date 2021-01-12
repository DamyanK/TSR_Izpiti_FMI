using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzpitPU_2017_2
{
    struct Employee
    {
        public string Name { get; set; }
        public string PIN { get; set; }
        public string NameLat { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public Employee(string name, string pin, string nameLat,
            string country, string postCode, string city)
        {
            this.Name = Program.Limiter(name, 50);
            this.PIN = Program.Limiter(pin, 15);
            this.NameLat = Program.Limiter(nameLat, 50);
            this.Country = Program.Limiter(country, 30);
            this.PostCode = Program.Limiter(postCode, 30);
            this.City = Program.Limiter(city, 30);
        }
    }

    class Program
    {
        public static string Limiter(string input, int limit)
        {
            if (input.Length < limit)
                return input;
            return input.Substring(0, limit);
        }

        static int NumOfEmployees()
        {
            int N;
            do
            {
                Console.Clear();
                Console.Write("Enter the number of employees: ");
                N = Convert.ToInt32(Console.ReadLine());
            } while (N < 1 || N > 50);

            return N;
        }

        static void Print(List<Employee> employees)
        {
            foreach (Employee person in employees)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                    person.Name, person.PIN, person.NameLat, person.Country,
                    person.PostCode, person.City);
            }
        }

        static void SortByPIN(List<Employee> wrongFields)
        {
            for (int i = 0; i < wrongFields.Count - 1; i++)
            {
                for (int j = 1; j < wrongFields.Count; j++)
                {
                    if (String.Compare(wrongFields[i].PIN, wrongFields[j].PIN) == 1)
                    {
                        Employee tmp = wrongFields[i];
                        wrongFields[i] = wrongFields[j];
                        wrongFields[j] = tmp;
                    }
                }
            }
        }

        static List<Employee> AddEmployees()
        {
            var employees = new List<Employee>();

            int N = NumOfEmployees();

            for (int i = 0; i < N; i++)
            {
                Console.Clear();

                Console.Write("*Name: ");
                string name = Console.ReadLine();

                Console.Write("*PIN: ");
                string pin = Console.ReadLine();

                Console.Write("*Name(Lat.): ");
                string nameLat = Console.ReadLine();

                Console.Write("*Country: ");
                string country = Console.ReadLine();

                Console.Write("Post Code: ");
                string postCode = Console.ReadLine();

                Console.Write("City: ");
                string city = Console.ReadLine();

                Employee person = new Employee(name, pin, nameLat,
                    country, postCode, city);
                employees.Add(person);
            }

            return employees;
        }

        static void Email(List<Employee> employees)
        {
            foreach (Employee person in employees)
            {
                string[] names = person.NameLat.Split(' ');

                if (person.NameLat == "")
                {
                    Console.WriteLine(person.Name + " - No information given for that task!");
                    break;
                }

                if (names.Length == 3)
                {
                    string email = names[2] + "_" + names[0] + "_" + names[1].Substring(0, 1) + "@nncomputers.com";
                    Console.WriteLine(person.Name + " - " + email);
                }
                if (names.Length == 2)
                {
                    string email = names[1] + "_" + names[0] + "@nncomputers.com";
                    Console.WriteLine(person.Name + " - " + email);
                }
                if (names.Length == 1)
                {
                    string email = names[0] + "@nncomputers.com";
                    Console.WriteLine(person.Name + " - " + email);
                }                
            }
        }

        static void Main(string[] args)
        {
            var employees = AddEmployees();

            Console.Clear();
            Print(employees);

            var wrongFields = new List<Employee>();

            foreach (Employee person in employees)
            {
                if (person.Name == ""
                    || person.PIN == ""
                    || person.NameLat == ""
                    || person.Country == "")
                {
                    wrongFields.Add(person);
                }
            }

            Console.WriteLine("\nEmployees with wrong filled fields: \n");

            if (wrongFields.Count == 0)
            {
                Console.WriteLine("\t-=NONE=-");
            }
            else
            {
                SortByPIN(wrongFields);
                Print(wrongFields);
            }

            Console.WriteLine("\nEmails of the employees: \n");
            Email(employees);

            Console.ReadKey(true);
        }
    }
}