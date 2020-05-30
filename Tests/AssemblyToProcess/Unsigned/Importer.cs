using System.Collections.Generic;
using AssemblyToProcess.Core;

namespace AssemblyToProcess.Unsigned
{
    public class Importer
    {
        public IReadOnlyList<IExportable> Imports { get; } =
            Vandelay.Import.Many<IExportable>(
                "AssemblyToProcess.Signed.dll|" +
                "AssemblyToProcess.Unsigned.dll");
    }
}
