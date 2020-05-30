using System;
using PostSharp.Extensibility;

namespace Vandelay
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [MulticastAttributeUsage(MulticastTargets.Assembly, AllowMultiple = true)]
    [RequirePostSharp("Vandelay.PostSharp", "ExportWeavingTask")]
    public class ExporterAttribute : MulticastAttribute
    {
        public ExporterAttribute(Type exportType)
        {
        }
    }
}
