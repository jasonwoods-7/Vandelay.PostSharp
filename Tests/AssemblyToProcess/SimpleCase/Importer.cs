using System.Collections.Generic;
using Vandelay;

namespace AssemblyToProcess.SimpleCase
{
    public class ImporterEmptySearchPattern
    {
        public IReadOnlyList<IExportable> Imports { get; } =
            Import.Many<IExportable>("");
    }

    public class ImporterSingleSearchPattern
    {
        public IReadOnlyList<IExportable> Imports { get; } =
            Import.Many<IExportable>(
                "AssemblyToProcess.SimpleCase.dll");
    }

    public class ImporterSingleSearchPatternWithImport
    {
        public ImporterSingleSearchPatternWithImport()
        {
            var greetingExport = "Hello, World";

            Imports = Import.Many<IExportable>(
                "AssemblyToProcess.SimpleCase.dll",
                greetingExport);
        }

        public IReadOnlyList<IExportable> Imports { get; }
    }

    public class ImporterMultipleSearchPatterns
    {
        public IReadOnlyList<IExportable> Imports { get; } =
            Import.Many<IExportable>(
                "AssemblyToProcess.SimpleCase.dll|" +
                "AssemblyToProcess.SimpleCase.exe");
    }

    public class ImporterInheritsBase
    {
        public IReadOnlyList<ExportBase> Imports { get; } =
            Import.Many<ExportBase>(
                "AssemblyToProcess.SimpleCase.dll");
    }

    public class ImporterInheritedExport
    {
        public IReadOnlyList<IInheritedExport> Imports { get; } =
            Import.Many<IInheritedExport>(
                "AssemblyToProcess.SimpleCase.dll");
    }
}
