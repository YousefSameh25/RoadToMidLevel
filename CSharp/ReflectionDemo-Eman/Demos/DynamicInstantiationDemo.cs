using System;
using System.Reflection;
using ReflectionProject.Models;

namespace ReflectionProject.Demos
{

    public class DynamicInstantiationDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Dynamic Instantiation Demo ===");
            Console.WriteLine("This demo shows how to create objects dynamically at runtime.\n");
            
            // Basic instantiation
            Console.WriteLine("1. Basic instantiation using Activator.CreateInstance:");
            Type dateType = typeof(DateTime);
            object emptyDate = Activator.CreateInstance(dateType);
            Console.WriteLine($"Created empty string: \"{emptyDate}\"");
            
            Type listType = typeof(System.Collections.Generic.List<int>);
            object emptyList = Activator.CreateInstance(listType);
            Console.WriteLine($"Created empty list: {emptyList}");
            
            // Instantiation with parameters
            Console.WriteLine("\n2. Instantiation with constructor parameters:");
            object Date = Activator.CreateInstance(dateType, new object[] { 2024, 5, 23 });
            Console.WriteLine($"Created Date with value: \"{Date}\"");
            
            Type personType = typeof(Person);
            object person = Activator.CreateInstance(personType, new object[] { "John", "Doe" });
            Console.WriteLine($"Created person: {((Person)person).FullName}");
            
            // Generic type instantiation
            Console.WriteLine("\n3. Generic type instantiation:");
            Type genericListType = typeof(System.Collections.Generic.List<>);
            Type stringListType = genericListType.MakeGenericType(typeof(string));
            object stringList = Activator.CreateInstance(stringListType);
            Console.WriteLine($"Created List<string>: {stringList}");
            
            Type genericDictType = typeof(System.Collections.Generic.Dictionary<,>);
            Type stringIntDictType = genericDictType.MakeGenericType(typeof(string), typeof(int));
            object dictionary = Activator.CreateInstance(stringIntDictType);
            Console.WriteLine($"Created Dictionary<string, int>: {dictionary}");
            
            // Using ConstructorInfo
            Console.WriteLine("\n4. Using ConstructorInfo directly:");
            ConstructorInfo constructor = personType.GetConstructor(
                new Type[] { typeof(string), typeof(string), typeof(int) });
            
            object person2 = constructor.Invoke(new object[] { "Jane", "Smith", 25 });
            Console.WriteLine($"Created person using ConstructorInfo: {((Person)person2).FullName}, Age: {((Person)person2).Age}");
            
            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's create objects dynamically!");
            
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Create a string with custom content");
                Console.WriteLine("2. Create a Person with custom values");
                Console.WriteLine("3. Create a generic collection");
                Console.WriteLine("4. Create an object from any available type");
                Console.WriteLine("5. Return to main menu");
                
                Console.Write("\nYour choice (1-5): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        CreateStringDemo();
                        break;
                    case "2":
                        CreatePersonDemo();
                        break;
                    case "3":
                        CreateGenericDemo();
                        break;
                    case "4":
                        CreateCustomTypeDemo();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void CreateStringDemo()
        {
            Console.WriteLine("\n--- Create String Demo ---");
            Console.Write("Enter the content for your string: ");
            string content = Console.ReadLine();
            char[] chars = content.ToCharArray();
            ConstructorInfo ctor = typeof(string).GetConstructor(new[] { typeof(char[]) });
            object customString = ctor.Invoke(new object[] { chars });
            Console.WriteLine($"Created string: \"{customString}\"");
            Console.WriteLine($"Length: {((string)customString).Length}");
            Console.WriteLine($"Type: {customString.GetType().FullName}");
        }


        private static void CreatePersonDemo()
        {
            Console.WriteLine("\n--- Create Person Demo ---");
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            
            Console.Write("Enter age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                age = 0;
                Console.WriteLine("Invalid age, using default value 0.");
            }
            
            Type personType = typeof(Person);
            
            // Choose constructor
            Console.WriteLine("\nChoose a constructor:");
            Console.WriteLine("1. Person()");
            Console.WriteLine("2. Person(string firstName, string lastName)");
            Console.WriteLine("3. Person(string firstName, string lastName, int age)");
            
            Console.Write("\nYour choice (1-3): ");
            string choice = Console.ReadLine();
            
            object person = null;
            switch (choice)
            {
                case "1":
                    person = Activator.CreateInstance(personType);
                    // Set properties manually
                    personType.GetProperty("FirstName").SetValue(person, firstName);
                    personType.GetProperty("LastName").SetValue(person, lastName);
                    personType.GetProperty("Age").SetValue(person, age);
                    break;
                case "2":
                    person = Activator.CreateInstance(personType, new object[] { firstName, lastName });
                    personType.GetProperty("Age").SetValue(person, age);
                    break;
                case "3":
                    person = Activator.CreateInstance(personType, new object[] { firstName, lastName, age });
                    break;
                default:
                    Console.WriteLine("Invalid choice, using default constructor.");
                    person = Activator.CreateInstance(personType);
                    break;
            }
            
            Console.WriteLine($"\nCreated person: {((Person)person).FullName}, Age: {((Person)person).Age}");
            Console.WriteLine($"ID: {((Person)person).Id}");
            
            // Invoke a method
            Console.WriteLine("\nInvoking Introduce() method:");
            MethodInfo introduceMethod = personType.GetMethod("Introduce");
            introduceMethod.Invoke(person, null);
        }
        
        private static void CreateGenericDemo()
        {
            Console.WriteLine("\n--- Create Generic Collection Demo ---");
            Console.WriteLine("Choose a generic collection type:");
            Console.WriteLine("1. List<T>");
            Console.WriteLine("2. Dictionary<TKey, TValue>");
            Console.WriteLine("3. HashSet<T>");
            
            Console.Write("\nYour choice (1-3): ");
            string collectionChoice = Console.ReadLine();
            
            Type genericType = null;
            switch (collectionChoice)
            {
                case "1":
                    genericType = typeof(System.Collections.Generic.List<>);
                    break;
                case "2":
                    genericType = typeof(System.Collections.Generic.Dictionary<,>);
                    break;
                case "3":
                    genericType = typeof(System.Collections.Generic.HashSet<>);
                    break;
                default:
                    Console.WriteLine("Invalid choice, using List<T>.");
                    genericType = typeof(System.Collections.Generic.List<>);
                    break;
            }
            
            Console.WriteLine("\nChoose element type(s):");
            Console.WriteLine("1. string");
            Console.WriteLine("2. int");
            Console.WriteLine("3. Person");
            Console.WriteLine("4. DateTime");
            
            Type[] typeArguments;
            if (genericType.GetGenericArguments().Length == 1)
            {
                Console.Write("\nYour choice (1-4): ");
                string typeChoice = Console.ReadLine();
                
                Type elementType = GetTypeFromChoice(typeChoice);
                typeArguments = new Type[] { elementType };
            }
            else
            {
                Console.Write("\nYour choice for key type (1-4): ");
                string keyTypeChoice = Console.ReadLine();
                
                Console.Write("Your choice for value type (1-4): ");
                string valueTypeChoice = Console.ReadLine();
                
                Type keyType = GetTypeFromChoice(keyTypeChoice);
                Type valueType = GetTypeFromChoice(valueTypeChoice);
                typeArguments = new Type[] { keyType, valueType };
            }
            
            // Create the constructed generic type
            Type constructedType = genericType.MakeGenericType(typeArguments);
            object instance = Activator.CreateInstance(constructedType);
            
            Console.WriteLine($"\nCreated instance: {instance}");
            Console.WriteLine($"Type: {instance.GetType().FullName}");
            
            // Add some items if it's a collection
            if (collectionChoice == "1" || collectionChoice == "3")
            {
                MethodInfo addMethod = constructedType.GetMethod("Add");
                if (addMethod != null)
                {
                    Console.WriteLine("\nAdding some items to the collection...");
                    
                    for (int i = 0; i < 3; i++)
                    {
                        object item = CreateSampleValue(typeArguments[0], i);
                        addMethod.Invoke(instance, new object[] { item });
                        Console.WriteLine($"Added: {item}");
                    }
                }
            }
            else if (collectionChoice == "2")
            {
                MethodInfo addMethod = constructedType.GetMethod("Add");
                if (addMethod != null)
                {
                    Console.WriteLine("\nAdding some items to the dictionary...");
                    
                    for (int i = 0; i < 3; i++)
                    {
                        object key = CreateSampleValue(typeArguments[0], i);
                        object value = CreateSampleValue(typeArguments[1], i);
                        addMethod.Invoke(instance, new object[] { key, value });
                        Console.WriteLine($"Added: {key} -> {value}");
                    }
                }
            }
            
            // Display count
            PropertyInfo countProperty = constructedType.GetProperty("Count");
            if (countProperty != null)
            {
                object count = countProperty.GetValue(instance);
                Console.WriteLine($"\nCollection count: {count}");
            }
        }
        
        private static void CreateCustomTypeDemo()
        {
            Console.WriteLine("\n--- Create Custom Type Demo ---");
            Console.WriteLine("Enter the full name of the type to create (e.g., System.DateTime):");
            string typeName = Console.ReadLine();
            
            try
            {
                Type type = Type.GetType(typeName);
                if (type == null)
                {
                    // Try with System namespace if not found
                    type = Type.GetType("System." + typeName);
                }
                
                if (type != null)
                {
                    Console.WriteLine($"Found type: {type.FullName}");
                    
                    // Get constructors
                    ConstructorInfo[] constructors = type.GetConstructors();
                    if (constructors.Length > 0)
                    {
                        Console.WriteLine("\nAvailable constructors:");
                        for (int i = 0; i < constructors.Length; i++)
                        {
                            ParameterInfo[] parameters = constructors[i].GetParameters();
                            Console.WriteLine($"{i + 1}. {type.Name}({string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"))})");
                        }
                        
                        Console.Write("\nChoose a constructor (number): ");
                        if (int.TryParse(Console.ReadLine(), out int constructorIndex) && 
                            constructorIndex >= 1 && constructorIndex <= constructors.Length)
                        {
                            ConstructorInfo selectedConstructor = constructors[constructorIndex - 1];
                            ParameterInfo[] parameters = selectedConstructor.GetParameters();
                            
                            object[] paramValues = new object[parameters.Length];
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                Console.Write($"Enter value for {parameters[i].Name} ({parameters[i].ParameterType.Name}): ");
                                string input = Console.ReadLine();
                                paramValues[i] = ConvertToType(input, parameters[i].ParameterType);
                            }
                            
                            object instance = selectedConstructor.Invoke(paramValues);
                            Console.WriteLine($"\nCreated instance: {instance}");
                            Console.WriteLine($"Type: {instance.GetType().FullName}");
                            
                            // Show some properties
                            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            if (properties.Length > 0)
                            {
                                Console.WriteLine("\nProperties:");
                                foreach (PropertyInfo property in properties.Take(5))
                                {
                                    try
                                    {
                                        object value = property.GetValue(instance);
                                        Console.WriteLine($"  {property.Name}: {value}");
                                    }
                                    catch
                                    {
                                        Console.WriteLine($"  {property.Name}: <unable to read>");
                                    }
                                }
                                
                                if (properties.Length > 5)
                                {
                                    Console.WriteLine($"  ... and {properties.Length - 5} more properties");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid constructor choice.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No public constructors found for this type.");
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
        }
        
        private static Type GetTypeFromChoice(string choice)
        {
            switch (choice)
            {
                case "1": return typeof(string);
                case "2": return typeof(int);
                case "3": return typeof(Person);
                case "4": return typeof(DateTime);
                default: return typeof(string);
            }
        }
        
        private static object CreateSampleValue(Type type, int index)
        {
            if (type == typeof(string))
                return $"Item {index}";
            else if (type == typeof(int))
                return index;
            else if (type == typeof(Person))
                return new Person($"Person{index}", "Sample", 20 + index);
            else if (type == typeof(DateTime))
                return DateTime.Now.AddDays(index);
            else
                return null;
        }
        
        private static object ConvertToType(string input, Type targetType)
        {
            try
            {
                if (targetType == typeof(string))
                    return input;
                else if (targetType == typeof(int) || targetType == typeof(Int32))
                    return int.Parse(input);
                else if (targetType == typeof(double))
                    return double.Parse(input);
                else if (targetType == typeof(bool))
                    return bool.Parse(input);
                else if (targetType == typeof(DateTime))
                    return DateTime.Parse(input);
                else
                    return Convert.ChangeType(input, targetType);
            }
            catch
            {
                Console.WriteLine($"Could not convert '{input}' to {targetType.Name}, using default value.");
                return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            }
        }
    }
}
