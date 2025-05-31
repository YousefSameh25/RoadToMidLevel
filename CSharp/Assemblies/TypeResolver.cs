using System.Collections.Concurrent;
using System.Reflection;

namespace AssembliesDemo
{
    internal static class TypeResolver
    {
        // Thread-safe cache for resolved instances
        private static readonly ConcurrentDictionary<Type, object> _instanceCache = new();

        public static TInterface Resolve<TInterface>()
        {
            var interfaceType = typeof(TInterface);

            if (_instanceCache.TryGetValue(interfaceType, out var cachedInstance))
            {
                return (TInterface)cachedInstance;
            }

            Assembly assembly = Assembly.LoadFile("C:\\Users\\youse\\source\\YousefSameh25\\RoadToMidLevel\\CSharp\\DummyAssembly\\bin\\Debug\\net8.0\\DummyAssembly.dll");

            var implementationType = assembly.GetTypes()
                .FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            if (implementationType == null)
            {
                throw new InvalidOperationException($"No implementation of {interfaceType.Name} found in assembly.");
            }

            object instance = Activator.CreateInstance(implementationType)!;

            TInterface typedInstance = (TInterface)instance!;

            _instanceCache.TryAdd(interfaceType, typedInstance);

            return typedInstance;
        }

    }
}
