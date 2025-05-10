using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    class DynamicAssemblyExample
    {
        public static void CreateAndInvokeDynamicAssembly()
        {
            // 1. Define the Assembly name
            AssemblyName asmName = new AssemblyName("MyDynamicAssembly");

            // 2. Create a dynamic Assembly (in memory only)
            AssemblyBuilder asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);

            // 3. Create a Module within the Assembly
            ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule("MainModule");

            // 4. Create a Class within the Module
            TypeBuilder typeBuilder = moduleBuilder.DefineType("MyDynamicType", TypeAttributes.Public);

            // 5. Create a Method within the class
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("SayHello", MethodAttributes.Public | MethodAttributes.Static, typeof(void), Type.EmptyTypes);
            ILGenerator il = methodBuilder.GetILGenerator();
            il.EmitWriteLine("Hello from Dynamic Assembly!");
            il.Emit(OpCodes.Ret);

            // 6. Create the type
            Type dynamicType = typeBuilder.CreateType();

            // 7. Print whether the Assembly is dynamic
            Console.WriteLine("Is Dynamic: " + dynamicType.Assembly.IsDynamic);

            // 8. Invoke the method
            MethodInfo sayHelloMethod = dynamicType.GetMethod("SayHello");
            sayHelloMethod.Invoke(null, null);
        }
    }


}
