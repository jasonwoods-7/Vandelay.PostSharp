using System.ComponentModel.Composition;
using System.Linq;
using JetBrains.Annotations;
using PostSharp.Sdk.CodeModel;
using PostSharp.Sdk.Extensibility;
using PostSharp.Sdk.Extensibility.Tasks;
using Vandelay.PostSharp.Extensions;

namespace Vandelay.PostSharp
{
    [ExportTask(Phase = TaskPhase.CustomTransform, TaskName = nameof(ExportWeavingTask))]
    [UsedImplicitly]
    public class ExportWeavingTask : Task
    {
        [ImportService]
        IAnnotationRepositoryService _annotationRepositoryService;

        public override bool Execute()
        {
            var enumerator = _annotationRepositoryService.GetAnnotationsOfType(typeof(ExporterAttribute), false, false);

            while (enumerator.MoveNext())
            {
                var serializedType = enumerator.Current!.Value.ConstructorArguments[0].Value;

                var exportedType = serializedType.Value.GetTypeDefinition();

                if (exportedType.CustomAttributes.Any(a =>
                    a.Constructor.Parent.GetTypeDefinition().Name == typeof(InheritedExportAttribute).FullName &&
                    a.ConstructorArguments.Count == 0))
                {
                    continue;
                }

                var module = Project.Module;

                var exportAttributeType = (INamedType)module.FindType(typeof(ExportAttribute));

                var exportAttributeCtor = module.FindMethod(exportAttributeType, ".ctor", c =>
                    c.Parameters.Count == 1 &&
                    c.Parameters[0].ParameterType.GetReflectionName() == "System.Type");

                var types = module.Types
                    .OfType<TypeDefDeclaration>()
                    .Where(t =>
                        t.IsClass() &&
                        !t.IsAbstract() &&
                        !t.ExportsType(exportedType) &&
                        (t.ImplementsInterface(exportedType) ||
                        t.InheritsBase(exportedType)));

                foreach (var type in types)
                {
                    var export = new CustomAttributeDeclaration(exportAttributeCtor);
                    export.ConstructorArguments.Add(new MemberValuePair(
                        MemberKind.Parameter, 0, "contractType", serializedType));

                    type.CustomAttributes.Add(export);
                }
            }

            return true;
        }
    }
}
