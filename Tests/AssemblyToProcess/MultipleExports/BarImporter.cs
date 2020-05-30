using System.Collections.Generic;

namespace AssemblyToProcess.MultipleExports
{
    public class BarImporter
    {
        public IReadOnlyList<IBarExporter> Imports { get; } =
            Vandelay.Import.Many<IBarExporter>(
                "AssemblyToProcess.MultipleExports.dll");
    }
}
