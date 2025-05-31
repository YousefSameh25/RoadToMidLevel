using AssembliesDemo;
using ISharedDSL;


namespace Assemblies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPrintDSL printDSL = TypeResolver.Resolve<IPrintDSL>();
            IPrintDSL printDSL2 = TypeResolver.Resolve<IPrintDSL>();

            printDSL.PrintFullName("Yousef loves eman once");
            printDSL2.PrintFullName("Yousef loves eman twice");
        }
    }
}
