using System;
using System.Reflection;
using System.Resources;
using System.IO;
using Assemblies;

class AssemblyInfoExplorer
{
    static void Main(string[] args)
    {
        // Get the Type of the current class
        var type = typeof(AssemblyInfoExplorer);

        // Get the Assembly the class is part of
        var assembly = type.Assembly;

        // Print the full name of the Assembly
        Console.WriteLine("Full Name: " + assembly.FullName);

        // Print the detailed information of the Assembly
        DisplayAssemblyDetails(assembly);

        // Print the actual file location of the Assembly
        DisplayAssemblyLocation(assembly);

        // Check and print if the Assembly is dynamic (generated at runtime)
        Console.WriteLine("Is Dynamic: " + assembly.IsDynamic);

        // Display all the types defined within the Assembly
        DisplayTypesInAssembly(assembly);

        // Display any custom attributes of the Assembly
        DisplayCustomAttributes(assembly);

        // Display any embedded resources within the Assembly
        DisplayEmbeddedResources(assembly);

        // Call the function to create and invoke the dynamic assembly
        DynamicAssemblyExample.CreateAndInvokeDynamicAssembly();

    Console.ReadKey();
    }

    // Display the assembly details such as name, version, culture, and public key
    private static void DisplayAssemblyDetails(Assembly assembly)
    {
        var assemblyName = assembly.GetName();
        Console.WriteLine("\nName: " + assemblyName.Name);
        Console.WriteLine("Version: " + assemblyName.Version);
        Console.WriteLine("Culture Info: " + assemblyName.CultureInfo.DisplayName);
        Console.WriteLine("Public Key: " + BitConverter.ToString(assemblyName.GetPublicKey()));
        Console.WriteLine("Public Key Token: " + BitConverter.ToString(assemblyName.GetPublicKeyToken()));
    }

    // Display the file location of the Assembly
    private static void DisplayAssemblyLocation(Assembly assembly)
    {
        Console.WriteLine("\nCodeBase (URL): " + assembly.CodeBase);
        Console.WriteLine("Location (path on disk): " + assembly.Location);
    }

    // Display the types (classes, interfaces, etc.) defined within the Assembly
    private static void DisplayTypesInAssembly(Assembly assembly)
    {
        Console.WriteLine("\nTypes defined in the assembly:");
        foreach (Type t in assembly.GetTypes())
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Type: " + t.FullName);

            // Display Constructors of the type
            DisplayConstructors(t);

            // Display Methods of the type
            DisplayMethods(t);

            // Display Properties of the type
            DisplayProperties(t);

            // Display Fields of the type
            DisplayFields(t);

            Console.WriteLine("====================================\n");
        }
    }

    // Display Constructors for a given type
    private static void DisplayConstructors(Type type)
    {
        Console.WriteLine("\nConstructors:");
        foreach (var ctor in type.GetConstructors())
        {
            Console.WriteLine("- " + ctor);
        }
    }

    // Display Methods for a given type
    private static void DisplayMethods(Type type)
    {
        Console.WriteLine("\nMethods:");
        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            Console.WriteLine("- " + method.Name);
        }
    }

    // Display Properties for a given type
    private static void DisplayProperties(Type type)
    {
        Console.WriteLine("\nProperties:");
        foreach (var prop in type.GetProperties())
        {
            Console.WriteLine("- " + prop.Name + " (" + prop.PropertyType.Name + ")");
        }
    }

    // Display Fields for a given type
    private static void DisplayFields(Type type)
    {
        Console.WriteLine("\nFields:");
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
        {
            Console.WriteLine("- " + field.Name + " (" + field.FieldType.Name + ")");
        }
    }

    // Display the custom attributes of the Assembly
    private static void DisplayCustomAttributes(Assembly assembly)
    {
        Console.WriteLine("\nCustom Attributes:");
        foreach (var attr in assembly.GetCustomAttributes())
        {
            Console.WriteLine("- " + attr.GetType().Name);
        }
    }

    // Display any embedded resources in the Assembly
    private static void DisplayEmbeddedResources(Assembly assembly)
    {
        Console.WriteLine("\n== Embedded Resources ==");
        var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        foreach (var name in names)
        {
            Console.WriteLine("- " + name);
        }

        // Show the content of a specific embedded resource
        var resourceName = "Assemblies.Resources.countries.json";
        var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream != null)
        {
            var data = new BinaryReader(stream).ReadBytes((int)stream.Length);
            foreach (var b in data)
            {
                Console.Write((char)b);
                System.Threading.Thread.Sleep(300); // Adding delay to simulate processing
            }
            stream.Close();
        }
        else
        {
            Console.WriteLine("Resource not found: " + resourceName);
        }
    }


}

class BaseClass
{
    public void BaseMethod() { }
}

class MyClass : BaseClass
{
    private void MyPrivateMethod() { }
    public static void MyStaticMethod() { }
}
