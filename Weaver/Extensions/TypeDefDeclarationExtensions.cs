using System.Linq;
using PostSharp.Sdk.CodeModel;

namespace Vandelay.PostSharp.Extensions
{
    public static class TypeDefDeclarationExtensions
    {
        public static bool IsClass(this TypeDefDeclaration declaration) =>
            declaration.BaseType != null &&
            !declaration.IsEnum() &&
            !declaration.IsInterface();

        public static bool ImplementsInterface(this TypeDefDeclaration declaration,
            TypeDefDeclaration interfaceType)
        {
            if (!interfaceType.IsInterface())
            {
                return false;
            }

            if (declaration?.BaseType == null)
            {
                return false;
            }

            if (declaration.InterfaceImplementations.Any(i =>
                i.ImplementedInterface.Name == interfaceType.Name))
            {
                return true;
            }

            return declaration.BaseTypeDef.ImplementsInterface(interfaceType);
        }
    }
}
