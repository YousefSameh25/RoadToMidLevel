using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ReflectionProject.Models;

namespace ReflectionProject.Demos
{
    public class TypeDiscoveryDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Type Discovery Demo ===");
            Console.WriteLine("This demo shows different ways to obtain type information.\n");
            
            Console.WriteLine("1. Using typeof operator:");
            Type intType = typeof(int);
            Type stringType = typeof(string);
            Type personType = typeof(Person);
            
            Console.WriteLine($"int type: {intType.FullName}");
            Console.WriteLine($"string type: {stringType.FullName}");
            Console.WriteLine($"Person type: {personType.FullName}");
            
            Console.WriteLine("\n2. Using Object.GetType():");
            object obj = "Hello, Reflection!";
            Type objType = obj.GetType();
            Console.WriteLine($"Object declared as 'object' but actually contains: {objType.Name}");
            
            object person = new Person { FirstName = "John", LastName = "Doe" };
            Type actualType = person.GetType();
            Console.WriteLine($"Object declared as 'object' but actually is: {actualType.Name}");
            
            Console.WriteLine("\n3. Using Type.GetType(string):");
            Type dateTimeType = Type.GetType("System.DateTime");
            Console.WriteLine($"System.DateTime: {dateTimeType?.Name ?? "Not found"}");
            
            Type repoType = Type.GetType("ReflectionProject.Models.Repository`1");
            Console.WriteLine($"Repository<T>: {repoType?.Name ?? "Not found"}");
            #region   ليه '1
             //✅ ليه في `1 بعد اسم الكلاس في الريفلكشن:
            //`1 معناها إن الكلاس generic ويأخذ 1 generic parameter(زي T).
           //دي صيغة مستخدمة داخل CLR(وقت التشغيل) علشان تفرق بين الكلاسات العادية والـ generic.
          //📌 أمثلة على أسماء Generic Types في CLR:
         //  Repository < T > → Repository1`
        //Dictionary < TKey, TValue > → Dictionary2`
       //Tuple < T1, T2, T3 > → Tuple3`
            #endregion

            Console.WriteLine("\n4. Using Assembly methods:");
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Current Assembly: {currentAssembly.GetName().Name}");
            
            Type[] allTypes = currentAssembly.GetTypes();
            Console.WriteLine($"Total types in assembly: {allTypes.Length}");
            
            var modelTypes = Array.FindAll(allTypes, t => t.Namespace?.Contains("Models") == true);
            Console.WriteLine("\nModel types in the project:");
            foreach (var type in modelTypes)
            {
                Console.WriteLine($"  {type.Name}");
            }
            
            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's explore types in different ways!");
            
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Get type using typeof");
                Console.WriteLine("2. Get type using GetType() on an object");
                Console.WriteLine("3. Get type using Type.GetType(string)");
                Console.WriteLine("4. List types in an assembly");
                Console.WriteLine("5. Return to main menu");
                
                Console.Write("\nYour choice (1-5): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        TypeofDemo();
                        break;
                    case "2":
                        GetTypeDemo();
                        break;
                    case "3":
                        TypeGetTypeDemo();
                        break;
                    case "4":
                        AssemblyTypesDemo();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void TypeofDemo()
        {
            Console.WriteLine("\n--- typeof Demo ---");
            Console.WriteLine("Choose a type to examine:");
            Console.WriteLine("1. int (System.Int32)");
            Console.WriteLine("2. string (System.String)");
            Console.WriteLine("3. Person (ReflectionProject.Models.Person)");
            Console.WriteLine("4. List<int> (System.Collections.Generic.List<int>)");
            
            Console.Write("\nYour choice (1-4): ");
            string choice = Console.ReadLine();
            
            Type type = null;
            switch (choice)
            {
                case "1":
                    type = typeof(int);
                    break;
                case "2":
                    type = typeof(string);
                    break;
                case "3":
                    type = typeof(Person);
                    break;
                case "4":
                    type = typeof(System.Collections.Generic.List<int>);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            
            DisplayTypeInfo(type);
        }
        
        private static void GetTypeDemo()
        {
            Console.WriteLine("\n--- GetType() Demo ---");
            Console.WriteLine("Choose an object to get its type:");
            Console.WriteLine("1. String object");
            Console.WriteLine("2. Person object");
            Console.WriteLine("3. Integer object (boxed)");
            Console.WriteLine("4. Anonymous object");
            
            Console.Write("\nYour choice (1-4): ");
            string choice = Console.ReadLine();
            
            object obj = null;
            switch (choice)
            {
                case "1":
                    obj = "This is a string";
                    break;
                case "2":
                    obj = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
                    break;
                case "3":
                    obj = 42;  // Boxing
                    break;
                case "4":
                    obj = new { Name = "Anonymous", Value = 123 };
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            
            Type type = obj.GetType();
            Console.WriteLine($"\nObject value: {obj}");
            DisplayTypeInfo(type);
        }
        
        private static void TypeGetTypeDemo()
        {
            Console.WriteLine("\n--- Type.GetType() Demo ---");
            Console.Write("Enter a type name (e.g., System.DateTime, System.Collections.ArrayList): ");
            string typeName = Console.ReadLine();
            
            try
            {
                Type type = Type.GetType(typeName);
                if (type == null)
                {
                    type = Type.GetType("System." + typeName);
                }
                
                if (type != null)
                {
                    DisplayTypeInfo(type);
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
        }
        
        private static void AssemblyTypesDemo()
        {
            Console.WriteLine("\n--- Assembly Types Demo ---");
            Console.WriteLine("Choose an assembly to examine:");
            Console.WriteLine("1. Current assembly");
            Console.WriteLine("2. mscorlib assembly (core .NET types)");
            
            Console.Write("\nYour choice (1-2): ");
            string choice = Console.ReadLine();
            
            Assembly assembly = null;
            switch (choice)
            {
                case "1":
                    assembly = Assembly.GetExecutingAssembly();
                    break;
                case "2":
                    assembly = typeof(object).Assembly;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            
            Console.WriteLine($"\nAssembly: {assembly.GetName().Name}");
            
            Type[] types = assembly.GetTypes();
            Console.WriteLine($"Total types: {types.Length}");
            
            Console.WriteLine("\nDo you want to filter the types? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("Choose a filter:");
                Console.WriteLine("1. Classes only");
                Console.WriteLine("2. Interfaces only");
                Console.WriteLine("3. Public types only");
                Console.WriteLine("4. Types with a specific namespace");
                
                Console.Write("\nYour choice (1-4): ");
                string filterChoice = Console.ReadLine();
                
                Type[] filteredTypes = null;
                switch (filterChoice)
                {
                    case "1":
                        filteredTypes = Array.FindAll(types, t => t.IsClass);
                        break;
                    case "2":
                        filteredTypes = Array.FindAll(types, t => t.IsInterface);
                        break;
                    case "3":
                        filteredTypes = Array.FindAll(types, t => t.IsPublic);
                        break;
                    case "4":
                        Console.Write("Enter namespace prefix: ");
                        string ns = Console.ReadLine();
                        filteredTypes = Array.FindAll(types, t => t.Namespace?.StartsWith(ns) == true);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }
                
                Console.WriteLine($"\nFiltered types: {filteredTypes.Length}");
                DisplayTypes(filteredTypes, 10);
            }
            else
            {
                DisplayTypes(types, 10);
            }
        }
        
        private static void DisplayTypeInfo(Type type)
        {
            Console.WriteLine($"\nType information for {type.FullName}:");
            Console.WriteLine($"Type Name: {type.Name}");
            Console.WriteLine($"Full Name: {type.FullName}");
            Console.WriteLine($"Namespace: {type.Namespace}");
            Console.WriteLine($"Assembly: {type.Assembly.GetName().Name}");
            Console.WriteLine($"Is Class: {type.IsClass}");
            Console.WriteLine($"Is Interface: {type.IsInterface}");
            Console.WriteLine($"Is Value Type: {type.IsValueType}");
            Console.WriteLine($"Is Abstract: {type.IsAbstract}");
            Console.WriteLine($"Is Sealed: {type.IsSealed}");
            Console.WriteLine($"Is Generic: {type.IsGenericType}");
            Console.WriteLine($"Base Type: {type.BaseType?.Name ?? "None"}");
            
            Console.WriteLine("\nInterfaces:");
            Type[] interfaces = type.GetInterfaces();
            foreach (Type interfaceType in interfaces.Length > 0 ? interfaces : new Type[] { null })
            {
                Console.WriteLine($"  {interfaceType?.Name ?? "None"}");
            }
            
            if (type.IsGenericType)
            {
                Console.WriteLine("\nGeneric Type Parameters:");
                foreach (Type genericArg in type.GetGenericArguments())
                {
                    Console.WriteLine($"  {genericArg.Name}");
                }
            }
        }
        
        private static void DisplayTypes(Type[] types, int maxCount)
        {
            Console.WriteLine("\nTypes (showing first few):");
            for (int i = 0; i < Math.Min(maxCount, types.Length); i++)
            {
                Console.WriteLine($"  {types[i].FullName}");
            }
            
            if (types.Length > maxCount)
            {
                Console.WriteLine($"  ... and {types.Length - maxCount} more");
            }
        }
    }
}
