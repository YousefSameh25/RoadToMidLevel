using System.Reflection;

namespace AssembliesDemo
{
    internal static class TypeResolver
    {
        public static TInterface Resolve<TInterface>()
        {

            Assembly assembly = Assembly.LoadFile("C:\\Users\\youse\\source\\YousefSameh25\\RoadToMidLevel\\CSharp\\DummyAssembly\\bin\\Debug\\net8.0\\DummyAssembly.dll");

            var interfaceType = typeof(TInterface);


            // Find first type that implements the interface and is not abstract
            var implementationType = assembly.GetTypes()
                .FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            if (implementationType == null)
                throw new InvalidOperationException($"No implementation of {interfaceType.Name} found in assembly.");

            // Create instance
            return (TInterface)Activator.CreateInstance(implementationType)!;
        }

    }
}
