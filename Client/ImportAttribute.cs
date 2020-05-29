using PostSharp.Extensibility;

namespace Vandelay
{
    [MulticastAttributeUsage(MulticastTargets.Assembly, AllowMultiple = false)]
    [RequirePostSharp("Vandelay.PostSharp", "ImportWeavingTask")]
    public class ImportAttribute : MulticastAttribute
    {
    }
}
