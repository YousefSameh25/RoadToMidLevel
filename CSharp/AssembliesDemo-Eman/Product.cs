using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private double price;

        public Product() { }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name} - {Id}");
        }


    }

}
