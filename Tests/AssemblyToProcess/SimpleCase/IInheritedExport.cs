using System.ComponentModel.Composition;

[assembly: Vandelay.Exporter(typeof(AssemblyToProcess.SimpleCase.IInheritedExport))]

namespace AssemblyToProcess.SimpleCase
{
    [InheritedExport]
    public interface IInheritedExport
    {
    }
}
