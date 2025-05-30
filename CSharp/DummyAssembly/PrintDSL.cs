using ISharedDSL;

namespace DummyAssembly
{
    public class PrintDSL : IPrintDSL
    {
        public void PrintFullName(string FullName)
        {
            Console.WriteLine(FullName);
        }
    }
}
