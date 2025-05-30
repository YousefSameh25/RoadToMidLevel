using AssembliesDemo;
using ISharedDSL;


namespace Assemblies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPrintDSL printDSL = TypeResolver.Resolve<IPrintDSL>();

            printDSL.PrintFullName("Yousef Loves Eman");
        }
    }
}
