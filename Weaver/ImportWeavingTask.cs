using PostSharp.Sdk.Extensibility;

namespace Vandelay.PostSharp
{
    [ExportTask(Phase = TaskPhase.CustomTransform, TaskName = nameof(ImportWeavingTask))]
    public class ImportWeavingTask : Task
    {

    }
}
