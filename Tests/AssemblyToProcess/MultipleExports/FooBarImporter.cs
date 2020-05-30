using System.Linq;

namespace AssemblyToProcess.MultipleExports
{
    public class FooBarImporter
    {
        public int IterateFooBars() =>
            Vandelay.Import.Many<IFooExporter>("AssemblyToProcess.MultipleExports.dll").Count() +
            Vandelay.Import.Many<IBarExporter>("AssemblyToProcess.MultipleExports.dll").Count();
    }
}
