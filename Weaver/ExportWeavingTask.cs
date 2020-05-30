using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using PostSharp.Sdk.CodeModel;
using PostSharp.Sdk.Extensibility;
using PostSharp.Sdk.Extensibility.Tasks;
using Vandelay.PostSharp.Extensions;

namespace Vandelay.PostSharp
{
    [ExportTask(Phase = TaskPhase.CustomTransform, TaskName = nameof(ExportWeavingTask))]
    public class ExportWeavingTask : Task
    {
        [ImportService]
        IAnnotationRepositoryService _annotationRepositoryService;

        public override bool Execute()
        {
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }

            var enumerator = _annotationRepositoryService.GetAnnotationsOfType(typeof(ExporterAttribute), false, false);

            while (enumerator.MoveNext())
            {
                var serializedType = enumerator.Current!.Value.ConstructorArguments[0].Value;
                //TODO: skip when InheritedExportAttribute is on type

                var exportedType = serializedType.Value switch
                {
                    TypeRefDeclaration r => r.GetTypeDefinition(),
                    TypeDefDeclaration d => d,
                    _ => throw new InvalidOperationException()
                };

                var module = Project.Module;

                var exportAttributeType = (INamedType)module.FindType(typeof(ExportAttribute));

                var exportAttributeCtor = module.FindMethod(exportAttributeType, ".ctor", c =>
                    c.Parameters.Count == 1 &&
                    c.Parameters[0].ParameterType.GetReflectionName() == "System.Type");

                var export = new CustomAttributeDeclaration(exportAttributeCtor);
                export.ConstructorArguments.Add(new MemberValuePair(
                    MemberKind.Parameter, 0, "contractType", serializedType));

                var types = module.Types
                    .OfType<TypeDefDeclaration>()
                    .Where(t => t.IsClass() &&
                        t.ImplementsInterface(exportedType));

                foreach (var type in types)
                {
                    type.CustomAttributes.Add(export);
                }
            }

            return true;
        }
    }
}
