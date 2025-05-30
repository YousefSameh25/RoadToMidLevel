using System;
using System.Reflection;
using System.IO;
using System.Linq;
using ReflectionProject.Models;

namespace ReflectionProject.Demos
{

    public class AssemblyReflectionDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Assembly Reflection Demo ===");
            Console.WriteLine("This demo shows how to work with assemblies using reflection.\n");
            
            // 1. Getting assemblies
            Console.WriteLine("1. Getting assemblies:");
            
            // Get the current executing assembly
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Console.WriteLine($"\nCurrent Assembly: {currentAssembly.GetName().Name}");
            Console.WriteLine($"Full Name: {currentAssembly.FullName}");
            Console.WriteLine($"Location: {currentAssembly.Location}");
            Console.WriteLine($"Version: {currentAssembly.GetName().Version}");
            
            // Get the assembly that contains a specific type
            Assembly systemAssembly = typeof(Console).Assembly;
            Console.WriteLine($"\nSystem.Console Assembly: {systemAssembly.GetName().Name}");
            Console.WriteLine($"Full Name: {systemAssembly.FullName}");
            
            // Get loaded assemblies
            Console.WriteLine("\nLoaded Assemblies (first 5):");
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < Math.Min(5, loadedAssemblies.Length); i++)
            {
                Console.WriteLine($"  {loadedAssemblies[i].GetName().Name}");
            }
            Console.WriteLine($"  ... and {Math.Max(0, loadedAssemblies.Length - 5)} more assemblies");
            
            // 2. Examining assembly contents
            Console.WriteLine("\n2. Examining assembly contents:");
            
            // Get all types in the current assembly
            Type[] types = currentAssembly.GetTypes();
            Console.WriteLine($"\nTypes in current assembly: {types.Length}");
            
            // Group types by namespace
            var typesByNamespace = types.GroupBy(t => t.Namespace ?? "No Namespace")
                                        .OrderBy(g => g.Key);
            
            foreach (var group in typesByNamespace)
            {
                Console.WriteLine($"\nNamespace: {group.Key}");
                foreach (Type type in group.Take(3))
                {
                    Console.WriteLine($"  {type.Name}");
                }
                
                if (group.Count() > 3)
                {
                    Console.WriteLine($"  ... and {group.Count() - 3} more types");
                }
            }
            
            // 3. Loading assemblies dynamically
            Console.WriteLine("\n3. Loading assemblies dynamically:");
            
            try
            {
                // Load assembly from file
                string systemRuntimePath = typeof(Uri).Assembly.Location;
                Assembly loadedAssembly = Assembly.LoadFrom(systemRuntimePath);
                
                Console.WriteLine($"\nLoaded assembly from file: {loadedAssembly.GetName().Name}");
                Console.WriteLine($"Full Name: {loadedAssembly.FullName}");
                
                // Get public types from the loaded assembly
                Type[] publicTypes = loadedAssembly.GetExportedTypes();
                Console.WriteLine($"Public types: {publicTypes.Length}");
                
                // Show some example types
                Console.WriteLine("\nSample types from loaded assembly (first 5):");
                for (int i = 0; i < Math.Min(5, publicTypes.Length); i++)
                {
                    Console.WriteLine($"  {publicTypes[i].FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError loading assembly: {ex.Message}");
            }
            
            // 4. Assembly metadata
            Console.WriteLine("\n4. Assembly metadata:");
            
            // Get assembly attributes
            object[] attributes = currentAssembly.GetCustomAttributes(false);
            Console.WriteLine("\nAssembly attributes:");
            foreach (var attr in attributes)
            {
                Console.WriteLine($"  {attr.GetType().Name}");
            }
            
            // Get referenced assemblies
            AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            Console.WriteLine($"\nReferenced assemblies: {referencedAssemblies.Length}");
            
            Console.WriteLine("\nSample referenced assemblies (first 5):");
            for (int i = 0; i < Math.Min(5, referencedAssemblies.Length); i++)
            {
                Console.WriteLine($"  {referencedAssemblies[i].Name}, Version={referencedAssemblies[i].Version}");
            }
            
            // Interactive part
            Console.WriteLine("\n=== Interactive Part ===");
            Console.WriteLine("Let's explore assemblies!");
            
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Explore current assembly");
                Console.WriteLine("2. Explore a .NET framework assembly");
                Console.WriteLine("3. Load an assembly from file");
                Console.WriteLine("4. Return to main menu");
                
                Console.Write("\nYour choice (1-4): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ExploreAssembly(Assembly.GetExecutingAssembly());
                        break;
                    case "2":
                        ExploreFrameworkAssembly();
                        break;
                    case "3":
                        LoadAssemblyFromFile();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void ExploreAssembly(Assembly assembly)
        {
            Console.WriteLine($"\n--- Exploring Assembly: {assembly.GetName().Name} ---");
            
            while (true)
            {
                Console.WriteLine("\nWhat would you like to explore?");
                Console.WriteLine("1. Assembly information");
                Console.WriteLine("2. Types in assembly");
                Console.WriteLine("3. Referenced assemblies");
                Console.WriteLine("4. Assembly attributes");
                Console.WriteLine("5. Back to previous menu");
                
                Console.Write("\nYour choice (1-5): ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        DisplayAssemblyInfo(assembly);
                        break;
                    case "2":
                        ExploreAssemblyTypes(assembly);
                        break;
                    case "3":
                        ExploreReferencedAssemblies(assembly);
                        break;
                    case "4":
                        ExploreAssemblyAttributes(assembly);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
        private static void ExploreFrameworkAssembly()
        {
            Console.WriteLine("\n--- Explore .NET Framework Assembly ---");
            Console.WriteLine("Choose an assembly to explore:");
            Console.WriteLine("1. System.Runtime (contains basic types)");
            Console.WriteLine("2. System.Console (contains console functionality)");
            Console.WriteLine("3. System.Collections (contains collection types)");
            Console.WriteLine("4. System.Linq (contains LINQ functionality)");
            
            Console.Write("\nYour choice (1-4): ");
            string choice = Console.ReadLine();
            
            Assembly assembly = null;
            switch (choice)
            {
                case "1":
                    assembly = typeof(Object).Assembly;
                    break;
                case "2":
                    assembly = typeof(Console).Assembly;
                    break;
                case "3":
                    assembly = typeof(System.Collections.ArrayList).Assembly;
                    break;
                case "4":
                    assembly = typeof(Enumerable).Assembly;
                    break;
                default:
                    Console.WriteLine("Invalid choice, using System.Runtime.");
                    assembly = typeof(Object).Assembly;
                    break;
            }
            
            ExploreAssembly(assembly);
        }
        
        private static void LoadAssemblyFromFile()
        {
            Console.WriteLine("\n--- Load Assembly from File ---");
            Console.WriteLine("Enter the path to an assembly file (.dll or .exe):");
            Console.WriteLine("(Example: Use the path of a .NET assembly like System.dll)");
            
            Console.Write("\nPath: ");
            string path = Console.ReadLine();
            
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    // Use a known assembly as an example
                    path = typeof(Uri).Assembly.Location;
                    Console.WriteLine($"Using example assembly: {path}");
                }
                
                if (!File.Exists(path))
                {
                    Console.WriteLine($"File not found: {path}");
                    return;
                }
                
                Assembly assembly = Assembly.LoadFrom(path);
                Console.WriteLine($"\nSuccessfully loaded assembly: {assembly.GetName().Name}");
                
                ExploreAssembly(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError loading assembly: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
        
        private static void DisplayAssemblyInfo(Assembly assembly)
        {
            Console.WriteLine($"\nAssembly Information for {assembly.GetName().Name}:");
            Console.WriteLine($"Full Name: {assembly.FullName}");
            Console.WriteLine($"Location: {assembly.Location}");
            Console.WriteLine($"Version: {assembly.GetName().Version}");
            Console.WriteLine($"Culture: {assembly.GetName().CultureName ?? "neutral"}");
            Console.WriteLine($"Public Key Token: {BitConverter.ToString(assembly.GetName().GetPublicKeyToken() ?? new byte[0]).Replace("-", "").ToLowerInvariant()}");
            Console.WriteLine($"Global Assembly Cache: {assembly.GlobalAssemblyCache}");
            Console.WriteLine($"Is Dynamic: {assembly.IsDynamic}");
            Console.WriteLine($"Host Context: {assembly.HostContext}");
            Console.WriteLine($"Image Runtime Version: {assembly.ImageRuntimeVersion}");
        }
        
        private static void ExploreAssemblyTypes(Assembly assembly)
        {
            Console.WriteLine($"\nTypes in {assembly.GetName().Name}:");
            
            Console.WriteLine("\nChoose type filter:");
            Console.WriteLine("1. All types");
            Console.WriteLine("2. Public types only");
            Console.WriteLine("3. Classes only");
            Console.WriteLine("4. Interfaces only");
            Console.WriteLine("5. Enums only");
            Console.WriteLine("6. Filter by namespace");
            
            Console.Write("\nYour choice (1-6): ");
            string choice = Console.ReadLine();
            
            Type[] types;
            try
            {
                switch (choice)
                {
                    case "2":
                        types = assembly.GetExportedTypes();
                        Console.WriteLine($"\nPublic types: {types.Length}");
                        break;
                    case "3":
                        types = assembly.GetTypes().Where(t => t.IsClass).ToArray();
                        Console.WriteLine($"\nClasses: {types.Length}");
                        break;
                    case "4":
                        types = assembly.GetTypes().Where(t => t.IsInterface).ToArray();
                        Console.WriteLine($"\nInterfaces: {types.Length}");
                        break;
                    case "5":
                        types = assembly.GetTypes().Where(t => t.IsEnum).ToArray();
                        Console.WriteLine($"\nEnums: {types.Length}");
                        break;
                    case "6":
                        Console.Write("\nEnter namespace prefix: ");
                        string ns = Console.ReadLine();
                        types = assembly.GetTypes().Where(t => t.Namespace?.StartsWith(ns) == true).ToArray();
                        Console.WriteLine($"\nTypes in namespace '{ns}': {types.Length}");
                        break;
                    default:
                        types = assembly.GetTypes();
                        Console.WriteLine($"\nAll types: {types.Length}");
                        break;
                }
                
                // Group types by namespace
                var typesByNamespace = types.GroupBy(t => t.Namespace ?? "No Namespace")
                                            .OrderBy(g => g.Key);
                
                foreach (var group in typesByNamespace)
                {
                    Console.WriteLine($"\nNamespace: {group.Key}");
                    
                    // Sort types by name
                    var sortedTypes = group.OrderBy(t => t.Name).ToArray();
                    
                    // Display first 10 types in each namespace
                    for (int i = 0; i < Math.Min(10, sortedTypes.Length); i++)
                    {
                        Type type = sortedTypes[i];
                        string kind = type.IsClass ? "class" : type.IsInterface ? "interface" : 
                                     type.IsEnum ? "enum" : type.IsValueType ? "struct" : "type";
                        Console.WriteLine($"  {kind} {type.Name}");
                    }
                    
                    if (sortedTypes.Length > 10)
                    {
                        Console.WriteLine($"  ... and {sortedTypes.Length - 10} more types");
                    }
                }
                
                // Examine a specific type
                Console.Write("\nEnter a type name to examine in detail (or press Enter to skip): ");
                string typeName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(typeName))
                {
                    Type type = types.FirstOrDefault(t => t.Name == typeName);
                    if (type == null)
                    {
                        // Try with case-insensitive search
                        type = types.FirstOrDefault(t => t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
                    }
                    
                    if (type != null)
                    {
                        Console.WriteLine($"\nType Details for {type.FullName}:");
                        Console.WriteLine($"  Assembly: {type.Assembly.GetName().Name}");
                        Console.WriteLine($"  Namespace: {type.Namespace ?? "None"}");
                        Console.WriteLine($"  Base Type: {type.BaseType?.Name ?? "None"}");
                        Console.WriteLine($"  Is Public: {type.IsPublic}");
                        Console.WriteLine($"  Is Class: {type.IsClass}");
                        Console.WriteLine($"  Is Interface: {type.IsInterface}");
                        Console.WriteLine($"  Is Enum: {type.IsEnum}");
                        Console.WriteLine($"  Is Value Type: {type.IsValueType}");
                        Console.WriteLine($"  Is Abstract: {type.IsAbstract}");
                        Console.WriteLine($"  Is Sealed: {type.IsSealed}");
                        Console.WriteLine($"  Is Generic: {type.IsGenericType}");
                        
                        // Show interfaces
                        Type[] interfaces = type.GetInterfaces();
                        if (interfaces.Length > 0)
                        {
                            Console.WriteLine("\n  Interfaces:");
                            foreach (Type iface in interfaces.Take(5))
                            {
                                Console.WriteLine($"    {iface.Name}");
                            }
                            
                            if (interfaces.Length > 5)
                            {
                                Console.WriteLine($"    ... and {interfaces.Length - 5} more interfaces");
                            }
                        }
                        
                        // Show members
                        MemberInfo[] members = type.GetMembers();
                        Console.WriteLine($"\n  Members: {members.Length} total");
                        
                        // Count by member type
                        int methods = members.Count(m => m.MemberType == MemberTypes.Method);
                        int properties = members.Count(m => m.MemberType == MemberTypes.Property);
                        int fields = members.Count(m => m.MemberType == MemberTypes.Field);
                        int events = members.Count(m => m.MemberType == MemberTypes.Event);
                        
                        Console.WriteLine($"    Methods: {methods}");
                        Console.WriteLine($"    Properties: {properties}");
                        Console.WriteLine($"    Fields: {fields}");
                        Console.WriteLine($"    Events: {events}");
                    }
                    else
                    {
                        Console.WriteLine($"Type '{typeName}' not found.");
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine($"\nError loading types: {ex.Message}");
                if (ex.LoaderExceptions != null && ex.LoaderExceptions.Length > 0)
                {
                    Console.WriteLine("Loader exceptions:");
                    foreach (Exception loaderEx in ex.LoaderExceptions.Take(3))
                    {
                        Console.WriteLine($"  {loaderEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
        
        private static void ExploreReferencedAssemblies(Assembly assembly)
        {
            Console.WriteLine($"\nReferenced Assemblies for {assembly.GetName().Name}:");
            
            AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
            Console.WriteLine($"Total referenced assemblies: {referencedAssemblies.Length}");
            
            // Sort by name
            var sortedAssemblies = referencedAssemblies.OrderBy(a => a.Name).ToArray();
            
            for (int i = 0; i < sortedAssemblies.Length; i++)
            {
                AssemblyName asmName = sortedAssemblies[i];
                Console.WriteLine($"{i + 1}. {asmName.Name}, Version={asmName.Version}");
            }
            
            // Load a referenced assembly
            Console.Write("\nEnter a number to load and explore a referenced assembly (or press Enter to skip): ");
            if (int.TryParse(Console.ReadLine(), out int asmIndex) && 
                asmIndex >= 1 && asmIndex <= sortedAssemblies.Length)
            {
                try
                {
                    AssemblyName asmName = sortedAssemblies[asmIndex - 1];
                    Assembly referencedAssembly = Assembly.Load(asmName);
                    
                    Console.WriteLine($"\nLoaded assembly: {referencedAssembly.GetName().Name}");
                    ExploreAssembly(referencedAssembly);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError loading assembly: {ex.Message}");
                }
            }
        }
        
        private static void ExploreAssemblyAttributes(Assembly assembly)
        {
            Console.WriteLine($"\nAttributes for {assembly.GetName().Name}:");
            
            object[] attributes = assembly.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                Console.WriteLine($"Found {attributes.Length} attributes:");
                foreach (var attr in attributes)
                {
                    Console.WriteLine($"\n  {attr.GetType().Name}");
                    
                    // Display attribute properties
                    Type attrType = attr.GetType();
                    PropertyInfo[] properties = attrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in properties)
                    {
                        if (prop.Name != "TypeId") // Skip common TypeId property
                        {
                            try
                            {
                                object value = prop.GetValue(attr);
                                Console.WriteLine($"    {prop.Name}: {value}");
                            }
                            catch
                            {
                                // Ignore errors reading properties
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No custom attributes found.");
            }
        }
    }
}
