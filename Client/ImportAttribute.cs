using System;
using PostSharp.Extensibility;

namespace Vandelay
{
    [AttributeUsage(AttributeTargets.Assembly)]
    [RequirePostSharp("Vandelay.PostSharp", "ImportWeavingTask")]
    public class ImportAttribute : Attribute
    {
    }
}
