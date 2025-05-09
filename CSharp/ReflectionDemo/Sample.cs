using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
    public class Sample
    {
        // Private field
        private string _secret = "hidden";

        // Public field
        public string PublicNote = "visible";

        // Property
        public int Number { get; set; }

        // Constructor
        public Sample(int number)
        {
            Number = number;
        }

        // Instance method
        public string Describe()
        {
            return $"Number is {Number}, secret is {_secret}, note is {PublicNote}.";
        }

        // Static method
        public static string GetClassInfo()
        {
            return "This is the Sample class.";
        }
    }

}
