using ReflectionProject.Models;
using System;
using System.Reflection;

namespace ReflectionProject.Demos
{

    public class BasicReflectionDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Basic Reflection Demo ===");
            Console.WriteLine("This demo shows the fundamental concepts of reflection.\n");

            // Get type information for DateTime
            Type dateTimeType = typeof(DateTime);

            // Display information about the type
            Console.WriteLine("Type information for System.DateTime:");
            Console.WriteLine($"Type Name: {dateTimeType.Name}");
            Console.WriteLine($"Full Name: {dateTimeType.FullName}");
            Console.WriteLine($"Namespace: {dateTimeType.Namespace}");
            Console.WriteLine($"Is Class: {dateTimeType.IsClass}");
            Console.WriteLine($"Base Type: {dateTimeType.BaseType.Name}");

            Console.WriteLine("\nPublic Methods (first 5):");
            MethodInfo[] methods = dateTimeType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < Math.Min(5, methods.Length); i++)
            {
                Console.WriteLine($"  {methods[i].ReturnType.Name} {methods[i].Name}");
            }
            Console.WriteLine("\nPublic Properties:");

            PropertyInfo[] propertiesd = dateTimeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in propertiesd)
            {
                Console.WriteLine($"  {property.PropertyType.Name} {property.Name}");
            }
            // Create an instance of DateTime using reflection
            Console.WriteLine("\nCreating a DateTime instance using reflection:");
            object date = Activator.CreateInstance(dateTimeType, new object[] { 2025, 5, 20 });
            Console.WriteLine($"Created Date: {date}");

            // Get a method (e.g., AddDays) and invoke it
            Console.WriteLine("Invoking AddDays(10) method through reflection:");
            MethodInfo addDaysMethod = dateTimeType.GetMethod("AddDays", new Type[] { typeof(double) });
            object newDate = addDaysMethod.Invoke(date, new object[] { 10.0 });

            Console.WriteLine($"Original Date: {date}");
            Console.WriteLine($"Date after AddDays(10): {newDate}");

            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's examine a type of your choice!");
            Console.Write("Enter a type name (e.g., System.DateTime, System.Collections.ArrayList): ");
            string typeName = Console.ReadLine();

            try
            {
                Type userType = Type.GetType(typeName);
                if (userType == null)
                {
                    // Try with System namespace if not found
                    userType = Type.GetType("System." + typeName);
                }

                if (userType != null)
                {
                    Console.WriteLine($"\nType information for {userType.FullName}:");
                    Console.WriteLine($"Type Name: {userType.Name}");
                    Console.WriteLine($"Full Name: {userType.FullName}");
                    Console.WriteLine($"Namespace: {userType.Namespace}");
                    Console.WriteLine($"Is Class: {userType.IsClass}");
                    Console.WriteLine($"Base Type: {userType.BaseType?.Name ?? "None"}");

                    Console.WriteLine("\nPublic Properties:");
                    PropertyInfo[] properties = userType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo property in properties)
                    {
                        Console.WriteLine($"  {property.PropertyType.Name} {property.Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"Type '{typeName}' not found. Try using the full type name including namespace.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
