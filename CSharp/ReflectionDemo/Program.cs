using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionDemo
{
    internal class Program
    {
        #region Helper Methods
        static int GetCurrentLine([CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }
        static void PrintLine(int lineLength = 100)
        {
            Console.WriteLine(new string('=', lineLength));
        }
        static void Process(Action reflectionAction, string functionName, int line)
        {
            Console.WriteLine($"Function Name: {functionName} at Line {line}");
            Console.Write("Output: "); reflectionAction();
            PrintLine();
        }
        #endregion

        static void Main(string[] args)
        {
            #region Reflection Definition
            /*
                = What is reflection?
                    - Reflection is the process of getting an assembly's meta data at [Runtime].
                    - Also it give the power of creating new properties at runtime!

                = What kind of meta data?
                    - Data type of any programming component (Class, Struct, etc)
                    - Properties inside type.
             */
            #endregion

            Type sampleType = typeof(Sample);

            #region Reflection Functions
            Process(() => { Console.WriteLine(DateTime.Now.GetType()); }, "DateTime.Now.GetType()", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.Namespace); }, "sampleType.Namespace", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.Name); }, "sampleType.Name", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.FullName); }, "sampleType.FullName", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.BaseType); }, "sampleType.BaseType", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.IsAbstract); }, "sampleType.IsAbstract", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.IsPublic); }, "sampleType.IsPublic", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.IsNotPublic); }, "sampleType.IsNotPublic", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.IsPrimitive); }, "sampleType.IsPrimitive", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.IsInterface); }, "sampleType.IsInterface", GetCurrentLine());
            Process(() => { Console.WriteLine(sampleType.Assembly); }, "sampleType.Assembly", GetCurrentLine());
            Process(() => { foreach (var prop in sampleType.GetProperties()) { Console.WriteLine(prop.Name); } }, "sampleType.GetProperties()", GetCurrentLine());
            Process(() => { foreach (var method in sampleType.GetMethods(BindingFlags.DeclaredOnly)) { Console.WriteLine(method.Name); } }, "sampleType.GetMethods(BindingFlags.DeclaredOnly)", GetCurrentLine());
            #endregion
        }
    }
}
