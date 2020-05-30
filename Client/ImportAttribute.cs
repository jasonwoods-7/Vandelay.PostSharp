using System;
using PostSharp.Extensibility;

namespace Vandelay
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    [MulticastAttributeUsage(MulticastTargets.Assembly, AllowMultiple = false)]
    [RequirePostSharp("Vandelay.PostSharp", "ImportWeavingTask")]
    public class ImportAttribute : MulticastAttribute
    {
    }
}
