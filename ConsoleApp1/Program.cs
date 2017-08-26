using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Struct.Customer(1, "asd", "Das");
            Struct.Customer c2 = new Struct.Customer(1, "asd", "3");

            Console.WriteLine(c1.CompareBySelector(c2, c => c.Name));
        

            Console.ReadKey();
        }
    }
}
