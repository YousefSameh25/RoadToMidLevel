using System;
using ReflectionProject.Demos;

namespace ReflectionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("       REFLECTION METHODOLOGY DEMO PROJECT       ");
                Console.WriteLine("=================================================");
                Console.WriteLine();
                Console.WriteLine("Choose a demo to run:");
                Console.WriteLine();
                Console.WriteLine("1. What, Why and How of Reflection");
                Console.WriteLine("2. Obtaining Types ");
                Console.WriteLine("3. Instantiating Types ");
                Console.WriteLine("4. Reflecting Members ");
                Console.WriteLine("5. Invoking Members ");
                Console.WriteLine("6. Reflecting Assemblies ");
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice (0-6): ");
                
                string choice = Console.ReadLine();
                Console.Clear();
                
                switch (choice)
                {
                    case "1":
                        BasicReflectionDemo.Run();
                        break;
                    case "2":
                        TypeDiscoveryDemo.Run();
                        break;
                    case "3":
                        DynamicInstantiationDemo.Run();
                        break;
                    case "4":
                        MemberReflectionDemo.Run();
                        break;
                    case "5":
                        MemberInvocationDemo.Run();
                        break;
                    case "6":
                        AssemblyReflectionDemo.Run();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Thank you for exploring reflection! Goodbye.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
                
                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
