using System;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReflectionProject.Models;

namespace ReflectionProject.Demos
{

    public class MemberReflectionDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Member Reflection Demo ===");
            Console.WriteLine("This demo shows how to reflect on members of a type (properties, methods, fields, etc.).\n");
            
            Type personType = typeof(Person);
            
            // 1. Discovering members
            Console.WriteLine("1. Discovering members:");
            
            // Get all members
            Console.WriteLine("\nAll Members (first 10):");
            MemberInfo[] allMembers = personType.GetMembers();
            for (int i = 0; i < Math.Min(10, allMembers.Length); i++)
            {
                Console.WriteLine($"  {allMembers[i].MemberType}: {allMembers[i].Name}");
            }
            Console.WriteLine($"  ... and {Math.Max(0, allMembers.Length - 10)} more members");
            
            // Get specific member types
            Console.WriteLine("\nFields:");
            FieldInfo[] fields = personType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | 
                                                     BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"  {field.Name} ({field.FieldType.Name})");
            }
            
            Console.WriteLine("\nProperties:");
            PropertyInfo[] properties = personType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"  {property.Name} ({property.PropertyType.Name})");
            }
            
            Console.WriteLine("\nMethods (first 10):");
            MethodInfo[] methods = personType.GetMethods();
            for (int i = 0; i < Math.Min(10, methods.Length); i++)
            {
                Console.WriteLine($"  {methods[i].Name}");
            }
            Console.WriteLine($"  ... and {Math.Max(0, methods.Length - 10)} more methods");
            
            // 2. Examining member details
            Console.WriteLine("\n2. Examining member details:");
            
            // Examine a field
            FieldInfo ageField = personType.GetField("_age", BindingFlags.NonPublic | BindingFlags.Instance);
            if (ageField != null)
            {
                Console.WriteLine("\nField Details:");
                Console.WriteLine($"  Name: {ageField.Name}");
                Console.WriteLine($"  Type: {ageField.FieldType.Name}");
                Console.WriteLine($"  Is Private: {ageField.IsPrivate}");
                Console.WriteLine($"  Is Static: {ageField.IsStatic}");
                Console.WriteLine($"  Is Readonly: {ageField.IsInitOnly}");
            }
            
            // Examine a property
            PropertyInfo nameProperty = personType.GetProperty("FirstName");
            if (nameProperty != null)
            {
                Console.WriteLine("\nProperty Details:");
                Console.WriteLine($"  Name: {nameProperty.Name}");
                Console.WriteLine($"  Type: {nameProperty.PropertyType.Name}");
                Console.WriteLine($"  Can Read: {nameProperty.CanRead}");
                Console.WriteLine($"  Can Write: {nameProperty.CanWrite}");
                Console.WriteLine($"  Get Method: {nameProperty.GetMethod?.Name ?? "None"}");
                Console.WriteLine($"  Set Method: {nameProperty.SetMethod?.Name ?? "None"}");
            }
            
            // Examine a method
            MethodInfo method = personType.GetMethod("CalculateAgeInMonths");
            if (method != null)
            {
                Console.WriteLine("\nMethod Details:");
                Console.WriteLine($"  Name: {method.Name}");
                Console.WriteLine($"  Return Type: {method.ReturnType.Name}");
                Console.WriteLine($"  Is Public: {method.IsPublic}");
                Console.WriteLine($"  Is Static: {method.IsStatic}");
                Console.WriteLine($"  Is Virtual: {method.IsVirtual}");
                
                ParameterInfo[] parameters = method.GetParameters();
                Console.WriteLine($"  Parameters: {parameters.Length}");
                foreach (ParameterInfo param in parameters)
                {
                    Console.WriteLine($"    {param.ParameterType.Name} {param.Name}");
                }
            }
            
            // 3. Working with attributes
            Console.WriteLine("\n3. Working with attributes:");
            
            // Get class attributes
            object[] typeAttributes = personType.GetCustomAttributes(false);
            Console.WriteLine("Class Attributes:");
            foreach (var attr in typeAttributes)
            {
                if (attr is DescriptionAttribute desc)
                {
                    Console.WriteLine($"  Description: {desc.Description}");
                }
                else
                {
                    Console.WriteLine($"  {attr.GetType().Name}");
                }
            }
            
            // Get property attributes
            PropertyInfo firstNameProperty = personType.GetProperty("FirstName");
            object[] propertyAttributes = firstNameProperty.GetCustomAttributes(false);
            Console.WriteLine("\nProperty Attributes for FirstName:");
            foreach (var attr in propertyAttributes)
            {
                if (attr is RequiredAttribute)
                {
                    Console.WriteLine($"  Required");
                }
                else if (attr is MaxLengthAttribute maxLength)
                {
                    Console.WriteLine($"  MaxLength: {maxLength.Length}");
                }
                else
                {
                    Console.WriteLine($"  {attr.GetType().Name}");
                }
            }
            
            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's explore members of types!");
            
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Explore members of Person class");
                Console.WriteLine("2. Explore members of a .NET framework class");
                Console.WriteLine("3. Explore members of a custom type name");
                Console.WriteLine("4. Return to main menu");
                
                Console.Write("\nYour choice (1-4): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ExploreMembersOf(typeof(Person));
                        break;
                    case "2":
                        ExploreFrameworkType();
                        break;
                    case "3":
                        ExploreCustomType();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void ExploreMembersOf(Type type)
        {
            Console.WriteLine($"\n--- Exploring Members of {type.Name} ---");
            
            while (true)
            {
                Console.WriteLine("\nWhat would you like to explore?");
                Console.WriteLine("1. Fields");
                Console.WriteLine("2. Properties");
                Console.WriteLine("3. Methods");
                Console.WriteLine("4. Constructors");
                Console.WriteLine("5. Events");
                Console.WriteLine("6. Attributes");
                Console.WriteLine("7. Back to previous menu");
                
                Console.Write("\nYour choice (1-7): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ExploreFields(type);
                        break;
                    case "2":
                        ExploreProperties(type);
                        break;
                    case "3":
                        ExploreMethods(type);
                        break;
                    case "4":
                        ExploreConstructors(type);
                        break;
                    case "5":
                        ExploreEvents(type);
                        break;
                    case "6":
                        ExploreAttributes(type);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void ExploreFrameworkType()
        {
            Console.WriteLine("\n--- Explore .NET Framework Type ---");
            Console.WriteLine("Choose a type to explore:");
            Console.WriteLine("1. System.String");
            Console.WriteLine("2. System.DateTime");
            Console.WriteLine("3. System.Collections.Generic.List<T>");
            Console.WriteLine("4. System.IO.FileInfo");
            
            Console.Write("\nYour choice (1-4): ");
            string choice = Console.ReadLine();
            
            Type type = null;
            switch (choice)
            {
                case "1":
                    type = typeof(string);
                    break;
                case "2":
                    type = typeof(DateTime);
                    break;
                case "3":
                    type = typeof(System.Collections.Generic.List<>);
                    break;
                case "4":
                    type = typeof(System.IO.FileInfo);
                    break;
                default:
                    Console.WriteLine("Invalid choice, using String.");
                    type = typeof(string);
                    break;
            }
            
            ExploreMembersOf(type);
        }
        
        private static void ExploreCustomType()
        {
            Console.WriteLine("\n--- Explore Custom Type ---");
            Console.Write("Enter a type name (e.g., System.Net.HttpClient): ");
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
                    ExploreMembersOf(type);
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
        
        private static void ExploreFields(Type type)
        {
            Console.WriteLine($"\nFields of {type.Name}:");
            
            Console.WriteLine("\nChoose binding flags:");
            Console.WriteLine("1. Public only");
            Console.WriteLine("2. Public and Non-Public");
            Console.WriteLine("3. Instance only");
            Console.WriteLine("4. Static only");
            Console.WriteLine("5. All (Public, Non-Public, Instance, Static)");
            
            Console.Write("\nYour choice (1-5): ");
            string choice = Console.ReadLine();
            
            BindingFlags flags;
            switch (choice)
            {
                case "1":
                    flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "2":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "3":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    break;
                case "4":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                    break;
                case "5":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                default:
                    flags = BindingFlags.Public | BindingFlags.Instance;
                    break;
            }
            
            FieldInfo[] fields = type.GetFields(flags);
            if (fields.Length > 0)
            {
                Console.WriteLine($"\nFound {fields.Length} fields:");
                foreach (FieldInfo field in fields)
                {
                    string access = field.IsPublic ? "public" : field.IsPrivate ? "private" : field.IsFamily ? "protected" : "internal";
                    string modifier = field.IsStatic ? "static" : "instance";
                    Console.WriteLine($"  {access} {modifier} {field.FieldType.Name} {field.Name}");
                }
                
                // Examine a specific field
                Console.Write("\nEnter a field name to examine in detail (or press Enter to skip): ");
                string fieldName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(fieldName))
                {
                    FieldInfo field = Array.Find(fields, f => f.Name == fieldName);
                    if (field != null)
                    {
                        Console.WriteLine($"\nField Details for {field.Name}:");
                        Console.WriteLine($"  Declaring Type: {field.DeclaringType.Name}");
                        Console.WriteLine($"  Field Type: {field.FieldType.Name}");
                        Console.WriteLine($"  Is Public: {field.IsPublic}");
                        Console.WriteLine($"  Is Private: {field.IsPrivate}");
                        Console.WriteLine($"  Is Static: {field.IsStatic}");
                        Console.WriteLine($"  Is Readonly: {field.IsInitOnly}");
                        Console.WriteLine($"  Is Literal: {field.IsLiteral}");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{fieldName}' not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No fields found with the specified binding flags.");
            }
        }
        
        private static void ExploreProperties(Type type)
        {
            Console.WriteLine($"\nProperties of {type.Name}:");
            
            Console.WriteLine("\nChoose binding flags:");
            Console.WriteLine("1. Public only");
            Console.WriteLine("2. Public and Non-Public");
            Console.WriteLine("3. Instance only");
            Console.WriteLine("4. Static only");
            Console.WriteLine("5. All (Public, Non-Public, Instance, Static)");
            
            Console.Write("\nYour choice (1-5): ");
            string choice = Console.ReadLine();
            
            BindingFlags flags;
            switch (choice)
            {
                case "1":
                    flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "2":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "3":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    break;
                case "4":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                    break;
                case "5":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                default:
                    flags = BindingFlags.Public | BindingFlags.Instance;
                    break;
            }
            
            PropertyInfo[] properties = type.GetProperties(flags);
            if (properties.Length > 0)
            {
                Console.WriteLine($"\nFound {properties.Length} properties:");
                foreach (PropertyInfo property in properties)
                {
                    string access = "public"; 
                    string modifier = property.GetMethod?.IsStatic == true ? "static" : "instance";
                    Console.WriteLine($"  {access} {modifier} {property.PropertyType.Name} {property.Name}");
                }
                
                Console.Write("\nEnter a property name to examine in detail (or press Enter to skip): ");
                string propertyName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(propertyName))
                {
                    PropertyInfo property = Array.Find(properties, p => p.Name == propertyName);
                    if (property != null)
                    {
                        Console.WriteLine($"\nProperty Details for {property.Name}:");
                        Console.WriteLine($"  Declaring Type: {property.DeclaringType.Name}");
                        Console.WriteLine($"  Property Type: {property.PropertyType.Name}");
                        Console.WriteLine($"  Can Read: {property.CanRead}");
                        Console.WriteLine($"  Can Write: {property.CanWrite}");
                        Console.WriteLine($"  Get Method: {property.GetMethod?.Name ?? "None"}");
                        Console.WriteLine($"  Set Method: {property.SetMethod?.Name ?? "None"}");
                        
                        object[] attributes = property.GetCustomAttributes(false);
                        if (attributes.Length > 0)
                        {
                            Console.WriteLine("  Attributes:");
                            foreach (var attr in attributes)
                            {
                                Console.WriteLine($"    {attr.GetType().Name}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Property '{propertyName}' not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No properties found with the specified binding flags.");
            }
        }
        
        private static void ExploreMethods(Type type)
        {
            Console.WriteLine($"\nMethods of {type.Name}:");
            
            Console.WriteLine("\nChoose binding flags:");
            Console.WriteLine("1. Public only");
            Console.WriteLine("2. Public and Non-Public");
            Console.WriteLine("3. Instance only");
            Console.WriteLine("4. Static only");
            Console.WriteLine("5. All (Public, Non-Public, Instance, Static)");
            
            Console.Write("\nYour choice (1-5): ");
            string choice = Console.ReadLine();
            
            BindingFlags flags;
            switch (choice)
            {
                case "1":
                    flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "2":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                case "3":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    break;
                case "4":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                    break;
                case "5":
                    flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    break;
                default:
                    flags = BindingFlags.Public | BindingFlags.Instance;
                    break;
            }
            
            MethodInfo[] methods = type.GetMethods(flags);
            if (methods.Length > 0)
            {
                Console.WriteLine($"\nFound {methods.Length} methods. Showing first 20:");
                for (int i = 0; i < Math.Min(20, methods.Length); i++)
                {
                    MethodInfo method = methods[i];
                    string access = method.IsPublic ? "public" : method.IsPrivate ? "private" : method.IsFamily ? "protected" : "internal";
                    string modifier = method.IsStatic ? "static" : "instance";
                    Console.WriteLine($"  {access} {modifier} {method.ReturnType.Name} {method.Name}");
                }
                
                if (methods.Length > 20)
                {
                    Console.WriteLine($"  ... and {methods.Length - 20} more methods");
                }
                
                // Examine a specific method
                Console.Write("\nEnter a method name to examine in detail (or press Enter to skip): ");
                string methodName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(methodName))
                {
                    // Find methods with the given name
                    MethodInfo[] namedMethods = Array.FindAll(methods, m => m.Name == methodName);
                    
                    if (namedMethods.Length > 0)
                    {
                        if (namedMethods.Length > 1)
                        {
                            Console.WriteLine($"\nFound {namedMethods.Length} overloads of {methodName}:");
                            for (int i = 0; i < namedMethods.Length; i++)
                            {
                                ParameterInfo[] parameters = namedMethods[i].GetParameters();
                                Console.WriteLine($"{i + 1}. {methodName}({string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"))})");
                            }
                            
                            Console.Write("\nChoose an overload (number): ");
                            if (int.TryParse(Console.ReadLine(), out int overloadIndex) && 
                                overloadIndex >= 1 && overloadIndex <= namedMethods.Length)
                            {
                                ExamineMethod(namedMethods[overloadIndex - 1]);
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice, examining first overload.");
                                ExamineMethod(namedMethods[0]);
                            }
                        }
                        else
                        {
                            ExamineMethod(namedMethods[0]);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Method '{methodName}' not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No methods found with the specified binding flags.");
            }
        }
        
        private static void ExamineMethod(MethodInfo method)
        {
            Console.WriteLine($"\nMethod Details for {method.Name}:");
            Console.WriteLine($"  Declaring Type: {method.DeclaringType.Name}");
            Console.WriteLine($"  Return Type: {method.ReturnType.Name}");
            Console.WriteLine($"  Is Public: {method.IsPublic}");
            Console.WriteLine($"  Is Private: {method.IsPrivate}");
            Console.WriteLine($"  Is Static: {method.IsStatic}");
            Console.WriteLine($"  Is Virtual: {method.IsVirtual}");
            Console.WriteLine($"  Is Abstract: {method.IsAbstract}");
            Console.WriteLine($"  Is Generic: {method.IsGenericMethod}");
            
            // Parameters
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                Console.WriteLine("  Parameters:");
                foreach (ParameterInfo param in parameters)
                {
                    string modifier = param.IsOut ? "out " : param.ParameterType.IsByRef ? "ref " : "";
                    Console.WriteLine($"    {modifier}{param.ParameterType.Name} {param.Name}");
                }
            }
            else
            {
                Console.WriteLine("  No parameters");
            }
            
            // Generic parameters if applicable
            if (method.IsGenericMethod)
            {
                Type[] genericArgs = method.GetGenericArguments();
                Console.WriteLine("  Generic Type Parameters:");
                foreach (Type arg in genericArgs)
                {
                    Console.WriteLine($"    {arg.Name}");
                }
            }
            
            // Attributes
            object[] attributes = method.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                Console.WriteLine("  Attributes:");
                foreach (var attr in attributes)
                {
                    Console.WriteLine($"    {attr.GetType().Name}");
                }
            }
        }
        
        private static void ExploreConstructors(Type type)
        {
            Console.WriteLine($"\nConstructors of {type.Name}:");
            
            ConstructorInfo[] constructors = type.GetConstructors(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (constructors.Length > 0)
            {
                Console.WriteLine($"\nFound {constructors.Length} constructors:");
                for (int i = 0; i < constructors.Length; i++)
                {
                    ConstructorInfo ctor = constructors[i];
                    ParameterInfo[] parameters = ctor.GetParameters();
                    string access = ctor.IsPublic ? "public" : ctor.IsPrivate ? "private" : ctor.IsFamily ? "protected" : "internal";
                    
                    Console.WriteLine($"{i + 1}. {access} {type.Name}({string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"))})");
                }
                
                // Examine a specific constructor
                Console.Write("\nEnter a constructor number to examine in detail (or press Enter to skip): ");
                if (int.TryParse(Console.ReadLine(), out int ctorIndex) && 
                    ctorIndex >= 1 && ctorIndex <= constructors.Length)
                {
                    ConstructorInfo ctor = constructors[ctorIndex - 1];
                    
                    Console.WriteLine($"\nConstructor Details:");
                    Console.WriteLine($"  Declaring Type: {ctor.DeclaringType.Name}");
                    Console.WriteLine($"  Is Public: {ctor.IsPublic}");
                    Console.WriteLine($"  Is Private: {ctor.IsPrivate}");
                    Console.WriteLine($"  Is Protected: {ctor.IsFamily}");
                    
                    // Parameters
                    ParameterInfo[] parameters = ctor.GetParameters();
                    if (parameters.Length > 0)
                    {
                        Console.WriteLine("  Parameters:");
                        foreach (ParameterInfo param in parameters)
                        {
                            Console.WriteLine($"    {param.ParameterType.Name} {param.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No parameters (default constructor)");
                    }
                    
                    // Attributes
                    object[] attributes = ctor.GetCustomAttributes(false);
                    if (attributes.Length > 0)
                    {
                        Console.WriteLine("  Attributes:");
                        foreach (var attr in attributes)
                        {
                            Console.WriteLine($"    {attr.GetType().Name}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No constructors found.");
            }
        }
        
        private static void ExploreEvents(Type type)
        {
            Console.WriteLine($"\nEvents of {type.Name}:");
            
            EventInfo[] events = type.GetEvents(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            
            if (events.Length > 0)
            {
                Console.WriteLine($"\nFound {events.Length} events:");
                foreach (EventInfo eventInfo in events)
                {
                    Console.WriteLine($"  {eventInfo.Name} ({eventInfo.EventHandlerType.Name})");
                }
                
                Console.Write("\nEnter an event name to examine in detail (or press Enter to skip): ");
                string eventName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(eventName))
                {
                    EventInfo eventInfo = Array.Find(events, e => e.Name == eventName);
                    if (eventInfo != null)
                    {
                        Console.WriteLine($"\nEvent Details for {eventInfo.Name}:");
                        Console.WriteLine($"  Declaring Type: {eventInfo.DeclaringType.Name}");
                        Console.WriteLine($"  Event Handler Type: {eventInfo.EventHandlerType.Name}");
                        Console.WriteLine($"  Add Method: {eventInfo.AddMethod.Name}");
                        Console.WriteLine($"  Remove Method: {eventInfo.RemoveMethod.Name}");
                        Console.WriteLine($"  Is Multicast: {eventInfo.IsMulticast}");
                        
                        // Attributes
                        object[] attributes = eventInfo.GetCustomAttributes(false);
                        if (attributes.Length > 0)
                        {
                            Console.WriteLine("  Attributes:");
                            foreach (var attr in attributes)
                            {
                                Console.WriteLine($"    {attr.GetType().Name}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Event '{eventName}' not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No events found.");
            }
        }
        
        private static void ExploreAttributes(Type type)
        {
            Console.WriteLine($"\nAttributes of {type.Name}:");
            
            // Class attributes
            object[] typeAttributes = type.GetCustomAttributes(false);
            if (typeAttributes.Length > 0)
            {
                Console.WriteLine("\nClass Attributes:");
                foreach (var attr in typeAttributes)
                {
                    Console.WriteLine($"  {attr.GetType().Name}");
                    
                    // Display attribute properties
                    Type attrType = attr.GetType();
                    PropertyInfo[] attrProps = attrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in attrProps)
                    {
                        if (prop.Name != "TypeId") 
                        {
                            try
                            {
                                object value = prop.GetValue(attr);
                                Console.WriteLine($"    {prop.Name}: {value}");
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNo class attributes found.");
            }
            
            // Member attributes
            Console.WriteLine("\nChoose member type to check for attributes:");
            Console.WriteLine("1. Fields");
            Console.WriteLine("2. Properties");
            Console.WriteLine("3. Methods");
            
            Console.Write("\nYour choice (1-3): ");
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ExploreFieldAttributes(type);
                    break;
                case "2":
                    ExplorePropertyAttributes(type);
                    break;
                case "3":
                    ExploreMethodAttributes(type);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
        private static void ExploreFieldAttributes(Type type)
        {
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            
            if (fields.Length > 0)
            {
                Console.WriteLine("\nFields with attributes:");
                bool foundAny = false;
                
                foreach (FieldInfo field in fields)
                {
                    object[] attributes = field.GetCustomAttributes(false);
                    if (attributes.Length > 0)
                    {
                        foundAny = true;
                        Console.WriteLine($"  {field.Name}:");
                        foreach (var attr in attributes)
                        {
                            Console.WriteLine($"    {attr.GetType().Name}");
                        }
                    }
                }
                
                if (!foundAny)
                {
                    Console.WriteLine("  No fields with attributes found.");
                }
            }
            else
            {
                Console.WriteLine("No fields found.");
            }
        }
        
        private static void ExplorePropertyAttributes(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            
            if (properties.Length > 0)
            {
                Console.WriteLine("\nProperties with attributes:");
                bool foundAny = false;
                
                foreach (PropertyInfo property in properties)
                {
                    object[] attributes = property.GetCustomAttributes(false);
                    if (attributes.Length > 0)
                    {
                        foundAny = true;
                        Console.WriteLine($"  {property.Name}:");
                        foreach (var attr in attributes)
                        {
                            Console.WriteLine($"    {attr.GetType().Name}");
                            
                            // Display attribute properties
                            Type attrType = attr.GetType();
                            PropertyInfo[] attrProps = attrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (PropertyInfo prop in attrProps)
                            {
                                if (prop.Name != "TypeId") 
                                {
                                    try
                                    {
                                        object value = prop.GetValue(attr);
                                        Console.WriteLine($"      {prop.Name}: {value}");
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                
                if (!foundAny)
                {
                    Console.WriteLine("  No properties with attributes found.");
                }
            }
            else
            {
                Console.WriteLine("No properties found.");
            }
        }
        
        private static void ExploreMethodAttributes(Type type)
        {
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            
            if (methods.Length > 0)
            {
                Console.WriteLine("\nMethods with attributes:");
                bool foundAny = false;
                
                foreach (MethodInfo method in methods)
                {
                    object[] attributes = method.GetCustomAttributes(false);
                    if (attributes.Length > 0)
                    {
                        foundAny = true;
                        Console.WriteLine($"  {method.Name}:");
                        foreach (var attr in attributes)
                        {
                            Console.WriteLine($"    {attr.GetType().Name}");
                            
                            if (attr is ObsoleteAttribute obsolete)
                            {
                                Console.WriteLine($"      Message: {obsolete.Message}");
                                Console.WriteLine($"      IsError: {obsolete.IsError}");
                            }
                        }
                    }
                }
                
                if (!foundAny)
                {
                    Console.WriteLine("  No methods with attributes found.");
                }
            }
            else
            {
                Console.WriteLine("No methods found.");
            }
        }
    }
}
