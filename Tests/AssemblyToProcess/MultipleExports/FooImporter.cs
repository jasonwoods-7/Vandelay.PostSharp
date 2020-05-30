using System.Collections.Generic;

namespace AssemblyToProcess.MultipleExports
{
    public class FooImporter
    {
        public IReadOnlyList<IFooExporter> Imports { get; } =
            Vandelay.Import.Many<IFooExporter>(
                "AssemblyToProcess.MultipleExports.dll");
    }
}
