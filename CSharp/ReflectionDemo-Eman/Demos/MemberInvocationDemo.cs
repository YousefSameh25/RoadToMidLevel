using System;
using System.ComponentModel;
using System.Reflection;
using ReflectionProject.Models;

namespace ReflectionProject.Demos
{
    public class MemberInvocationDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Member Invocation Demo ===");
            Console.WriteLine("This demo shows how to invoke methods, access properties, and work with fields using reflection.\n");
            
            // Create a Person instance for demonstration
            Person person = new Person
            {
                FirstName = "John",
                LastName = "smith",
                Age = 30
            };
            
            Type personType = typeof(Person);
            
            Console.WriteLine("1. Invoking Methods:");
            
            Console.WriteLine("\nInvoking instance method with no parameters:");
            MethodInfo introduceMethod = personType.GetMethod("Introduce");
            introduceMethod.Invoke(person, null);  // Output: Hello, my name is John smith and I am 30 years old.
            
            Console.WriteLine("\nInvoking instance method with parameters:");
            MethodInfo calculateMethod = personType.GetMethod("CalculateRetirementAge");
            object result = calculateMethod.Invoke(person, new object[] { 65 });
            Console.WriteLine($"Years to retirement: {result}");  // Output: Years to retirement: 35
            
            // Invoke static method
            Console.WriteLine("\nInvoking static method:");
            MethodInfo createMethod = personType.GetMethod("CreateAdult");
            object adultPerson = createMethod.Invoke(null, new object[] { "Aymona", "Shehta" });
            Console.WriteLine($"Created adult: {((Person)adultPerson).FullName}, Age: {((Person)adultPerson).Age}");
            
            // Invoke generic method
            Console.WriteLine("\nInvoking generic method:");
            MethodInfo convertMethod = personType.GetMethod("ConvertAge");
            MethodInfo convertToStringMethod = convertMethod.MakeGenericMethod(typeof(string));
            object stringAge = convertToStringMethod.Invoke(person, null);
            Console.WriteLine($"Age as string: {stringAge}");
            
            // 2. Accessing Properties
            Console.WriteLine("\n2. Accessing Properties:");
            
            // Get property values
            PropertyInfo idProperty = personType.GetProperty("Id");
            object idValue = idProperty.GetValue(person);
            Console.WriteLine($"Id: {idValue}");
            
            PropertyInfo nameProperty = personType.GetProperty("FirstName");
            object nameValue = nameProperty.GetValue(person);
            Console.WriteLine($"First Name: {nameValue}");
            
            // Set property values
            Console.WriteLine("\nSetting property values:");
            nameProperty.SetValue(person, "esraa");
            
            PropertyInfo ageProperty = personType.GetProperty("Age");
            ageProperty.SetValue(person, 25);
            
            Console.WriteLine($"Updated person: {person.FullName}, Age: {person.Age}");
            
            // Access static property
            PropertyInfo countProperty = personType.GetProperty("InstanceCount");
            object countValue = countProperty.GetValue(null);
            Console.WriteLine($"Instance count: {countValue}");
            
            // 3. Accessing Fields
            Console.WriteLine("\n3. Accessing Fields:");
            
            // Access private instance field
            FieldInfo firstNameField = personType.GetField("_firstName", BindingFlags.NonPublic | BindingFlags.Instance);
            object firstNameValue = firstNameField.GetValue(person);
            Console.WriteLine($"_firstName field value: {firstNameValue}");
            
            // Modify private field
            firstNameField.SetValue(person, "salama");
            Console.WriteLine($"After modifying _firstName field: {person.FirstName}");
            
            // Access private static field
            FieldInfo countField = personType.GetField("_instanceCount", BindingFlags.NonPublic | BindingFlags.Static);
            object instanceCountValue = countField.GetValue(null);
            Console.WriteLine($"_instanceCount field value: {instanceCountValue}");
            
            Console.WriteLine("\n4. Working with Events:");
            
            PropertyChangedEventHandler handler = (sender, e) => 
            {
                Console.WriteLine($"Property changed: {e.PropertyName}");
            };
            
            EventInfo propertyChangedEvent = personType.GetEvent("PropertyChanged");
            
            Console.WriteLine("\nAdding event handler and changing properties:");
            propertyChangedEvent.AddEventHandler(person, handler);
            
            person.FirstName = "eman shehta"; 
            person.Age = 23;              
            
            Console.WriteLine("\nRemoving event handler and changing properties:");
            propertyChangedEvent.RemoveEventHandler(person, handler);
            
            person.FirstName = "sameh";
            person.Age = 24;
            Console.WriteLine("Properties changed but no event handler output");
            
            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's invoke members dynamically!");
            
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Invoke a method");
                Console.WriteLine("2. Get/Set a property");
                Console.WriteLine("3. Get/Set a field");
                Console.WriteLine("4. Work with events");
                Console.WriteLine("5. Return to main menu");
                
                Console.Write("\nYour choice (1-5): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        InvokeMethodDemo();
                        break;
                    case "2":
                        AccessPropertyDemo();
                        break;
                    case "3":
                        AccessFieldDemo();
                        break;
                    case "4":
                        WorkWithEventsDemo();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void InvokeMethodDemo()
        {
            Console.WriteLine("\n--- Invoke Method Demo ---");
            
            Person person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                Age = 30
            };
            
            Type personType = typeof(Person);
            
            // Get available methods
            Console.WriteLine("Available methods:");
            MethodInfo[] methods = personType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                ParameterInfo[] parameters = method.GetParameters();
                Console.WriteLine($"{i + 1}. {method.Name}({string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"))})");
            }
            
            // Also show static methods
            MethodInfo[] staticMethods = personType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            for (int i = 0; i < staticMethods.Length; i++)
            {
                MethodInfo method = staticMethods[i];
                ParameterInfo[] parameters = method.GetParameters();
                Console.WriteLine($"{methods.Length + i + 1}. [static] {method.Name}({string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"))})");
            }
            
            // Choose a method to invoke
            Console.Write("\nEnter the number of the method to invoke: ");
            if (int.TryParse(Console.ReadLine(), out int methodIndex) && 
                methodIndex >= 1 && methodIndex <= methods.Length + staticMethods.Length)
            {
                MethodInfo selectedMethod;
                bool isStatic = false;
                
                if (methodIndex <= methods.Length)
                {
                    selectedMethod = methods[methodIndex - 1];
                }
                else
                {
                    selectedMethod = staticMethods[methodIndex - methods.Length - 1];
                    isStatic = true;
                }
                
                // Get parameters for the method
                ParameterInfo[] parameters = selectedMethod.GetParameters();
                object[] paramValues = new object[parameters.Length];
                
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write($"Enter value for {parameters[i].Name} ({parameters[i].ParameterType.Name}): ");
                    string input = Console.ReadLine();
                    paramValues[i] = ConvertToType(input, parameters[i].ParameterType);
                }
                
                try
                {
                    // Invoke the method
                    object result = selectedMethod.Invoke(isStatic ? null : person, paramValues);
                    
                    if (selectedMethod.ReturnType != typeof(void))
                    {
                        Console.WriteLine($"\nMethod returned: {result}");
                    }
                    else
                    {
                        Console.WriteLine("\nMethod executed successfully (void return type)");
                    }
                    
                    Console.WriteLine($"\nCurrent person state: {person.FullName}, Age: {person.Age}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError invoking method: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid method number.");
            }
        }
        
        private static void AccessPropertyDemo()
        {
            Console.WriteLine("\n--- Access Property Demo ---");
            
            Person person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                Age = 30
            };
            
            Type personType = typeof(Person);
            
            // Get available properties
            Console.WriteLine("Available properties:");
            PropertyInfo[] properties = personType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                string access = property.CanWrite ? "read/write" : "read-only";
                Console.WriteLine($"{i + 1}. {property.Name} ({property.PropertyType.Name}) - {access}");
            }
            
            // Also show static properties
            PropertyInfo[] staticProperties = personType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < staticProperties.Length; i++)
            {
                PropertyInfo property = staticProperties[i];
                string access = property.CanWrite ? "read/write" : "read-only";
                Console.WriteLine($"{properties.Length + i + 1}. [static] {property.Name} ({property.PropertyType.Name}) - {access}");
            }
            
            // Choose a property to access
            Console.Write("\nEnter the number of the property to access: ");
            if (int.TryParse(Console.ReadLine(), out int propertyIndex) && 
                propertyIndex >= 1 && propertyIndex <= properties.Length + staticProperties.Length)
            {
                PropertyInfo selectedProperty;
                bool isStatic = false;
                
                if (propertyIndex <= properties.Length)
                {
                    selectedProperty = properties[propertyIndex - 1];
                }
                else
                {
                    selectedProperty = staticProperties[propertyIndex - properties.Length - 1];
                    isStatic = true;
                }
                
                try
                {
                    object value = selectedProperty.GetValue(isStatic ? null : person);
                    Console.WriteLine($"\nCurrent value of {selectedProperty.Name}: {value}");
                    
                    if (selectedProperty.CanWrite)
                    {
                        Console.Write($"\nEnter a new value for {selectedProperty.Name} ({selectedProperty.PropertyType.Name}): ");
                        string input = Console.ReadLine();
                        object newValue = ConvertToType(input, selectedProperty.PropertyType);
                        
                        selectedProperty.SetValue(isStatic ? null : person, newValue);
                        Console.WriteLine($"Property {selectedProperty.Name} set to: {newValue}");
                        
                        Console.WriteLine($"\nCurrent person state: {person.FullName}, Age: {person.Age}");
                    }
                    else
                    {
                        Console.WriteLine($"\nProperty {selectedProperty.Name} is read-only and cannot be modified.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError accessing property: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid property number.");
            }
        }
        
        private static void AccessFieldDemo()
        {
            Console.WriteLine("\n--- Access Field Demo ---");
            
            Person person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                Age = 30
            };
            
            Type personType = typeof(Person);
            
            Console.WriteLine("Available fields:");
            FieldInfo[] fields = personType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];
                string access = field.IsPublic ? "public" : field.IsPrivate ? "private" : "protected";
                Console.WriteLine($"{i + 1}. {access} {field.FieldType.Name} {field.Name}");
            }
            
            FieldInfo[] staticFields = personType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            for (int i = 0; i < staticFields.Length; i++)
            {
                FieldInfo field = staticFields[i];
                string access = field.IsPublic ? "public" : field.IsPrivate ? "private" : "protected";
                Console.WriteLine($"{fields.Length + i + 1}. [static] {access} {field.FieldType.Name} {field.Name}");
            }
            
            Console.Write("\nEnter the number of the field to access: ");
            if (int.TryParse(Console.ReadLine(), out int fieldIndex) && 
                fieldIndex >= 1 && fieldIndex <= fields.Length + staticFields.Length)
            {
                FieldInfo selectedField;
                bool isStatic = false;
                
                if (fieldIndex <= fields.Length)
                {
                    selectedField = fields[fieldIndex - 1];
                }
                else
                {
                    selectedField = staticFields[fieldIndex - fields.Length - 1];
                    isStatic = true;
                }
                
                // Get the current value
                try
                {
                    object value = selectedField.GetValue(isStatic ? null : person);
                    Console.WriteLine($"\nCurrent value of {selectedField.Name}: {value}");
                    
                    // If the field is not readonly, allow setting a new value
                    if (!selectedField.IsInitOnly && !selectedField.IsLiteral)
                    {
                        Console.Write($"\nEnter a new value for {selectedField.Name} ({selectedField.FieldType.Name}): ");
                        string input = Console.ReadLine();
                        object newValue = ConvertToType(input, selectedField.FieldType);
                        
                        selectedField.SetValue(isStatic ? null : person, newValue);
                        Console.WriteLine($"Field {selectedField.Name} set to: {newValue}");
                        
                        // Show the current state of the person
                        Console.WriteLine($"\nCurrent person state: {person.FullName}, Age: {person.Age}");
                    }
                    else
                    {
                        Console.WriteLine($"\nField {selectedField.Name} is readonly and cannot be modified.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError accessing field: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid field number.");
            }
        }
        
        private static void WorkWithEventsDemo()
        {
            Console.WriteLine("\n--- Work with Events Demo ---");
            
            Person person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                Age = 30
            };
            
            Type personType = typeof(Person);
            
            Console.WriteLine("Available events:");
            EventInfo[] events = personType.GetEvents();
            for (int i = 0; i < events.Length; i++)
            {
                EventInfo eventInfo = events[i];
                Console.WriteLine($"{i + 1}. {eventInfo.Name} ({eventInfo.EventHandlerType.Name})");
            }
            
            if (events.Length == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }
                        Console.Write("\nEnter the number of the event to work with: ");
            if (int.TryParse(Console.ReadLine(), out int eventIndex) && 
                eventIndex >= 1 && eventIndex <= events.Length)
            {
                EventInfo selectedEvent = events[eventIndex - 1];
                
                Console.WriteLine($"\nSelected event: {selectedEvent.Name}");
                Console.WriteLine("1. Add event handler");
                Console.WriteLine("2. Remove event handler");
                
                Console.Write("\nYour choice (1-2): ");
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    PropertyChangedEventHandler handler = (sender, e) => 
                    {
                        Console.WriteLine($"Event fired: {selectedEvent.Name}, Property: {e.PropertyName}");
                    };
                    
                    EventHandlers.CurrentHandler = handler;
                    
                    selectedEvent.AddEventHandler(person, handler);
                    Console.WriteLine($"Event handler added to {selectedEvent.Name}");
                    
                    Console.WriteLine("\nChanging properties to trigger the event:");
                    person.FirstName = "Event";
                    person.LastName = "Test";
                    person.Age = 25;
                }
                else if (choice == "2")
                {
                    if (EventHandlers.CurrentHandler != null)
                    {
                        selectedEvent.RemoveEventHandler(person, EventHandlers.CurrentHandler);
                        Console.WriteLine($"Event handler removed from {selectedEvent.Name}");
                        
                        Console.WriteLine("\nChanging properties (event should not fire):");
                        person.FirstName = "No";
                        person.LastName = "Event";
                        person.Age = 35;
                        
                        EventHandlers.CurrentHandler = null;
                    }
                    else
                    {
                        Console.WriteLine("No event handler has been added yet.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("Invalid event number.");
            }
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
                else if (targetType == typeof(Guid))
                    return Guid.Parse(input);
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
    
    public static class EventHandlers
    {
        public static PropertyChangedEventHandler CurrentHandler { get; set; }
    }
}
