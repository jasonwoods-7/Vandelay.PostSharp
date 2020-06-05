using System;
using PostSharp.Extensibility;

namespace Vandelay
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [RequirePostSharp("Vandelay.PostSharp", "ExportWeavingTask")]
    public class ExporterAttribute : Attribute
    {
        public ExporterAttribute(Type exportType)
        {
        }
    }
}
