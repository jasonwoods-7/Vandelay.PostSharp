using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using PostSharp.Sdk.CodeModel;
using PostSharp.Sdk.Extensibility;
using Vandelay.PostSharp.Extensions;

namespace Vandelay.PostSharp
{
    [ExportTask(Phase = TaskPhase.CustomTransform, TaskName = nameof(ImportWeavingTask))]
    [UsedImplicitly]
    public class ImportWeavingTask : Task
    {
        IGenericMethodDefinition _manyMethod;

        public override bool Execute()
        {
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }

            var module = Project.Module;

            var importTypeDef = module.Cache.GetType(typeof(Import)).GetTypeDefinition();
            _manyMethod = module.FindMethod(importTypeDef, "Many");

            foreach (var method in module.Types
                .OfType<TypeDefDeclaration>()
                .Where(t => t.IsClass())
                .SelectMany(t => t.Methods.Cast<MethodDefDeclaration>())
                .Where(m => m.HasBody))
            {
                method.MethodBody.ForEachInstruction(ProcessInstruction);
            }

            return true;
        }

        bool ProcessInstruction(InstructionReader reader)
        {
            var instruction = reader.CurrentInstruction;

            if (instruction.OpCodeNumber != OpCodeNumber.Call ||
                !instruction.MethodOperand.GetMethodDefinition().Equals(_manyMethod))
            {
                return true;
            }

            ProcessImportMany(instruction);

            return true;
        }

        void ProcessImportMany(Instruction instruction)
        {
            var searchPatternInstruction = instruction;
        }
    }
}
