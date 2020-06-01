using System.ComponentModel.Composition;
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

        public static bool ExportsType(this TypeDefDeclaration declaration,
            TypeDefDeclaration exportedType) =>
            declaration.CustomAttributes.Any(a =>
                a.Constructor.Parent.GetTypeDefinition().Name == typeof(ExportAttribute).FullName &&
                a.ConstructorArguments.Count == 1 &&
                a.ConstructorArguments[0].Value.Value.GetTypeDefinition().Name == exportedType.Name);

        public static bool ImplementsInterface(this TypeDefDeclaration? declaration,
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

        public static bool InheritsBase(this TypeDefDeclaration? declaration,
            TypeDefDeclaration baseType)
        {
            if (!baseType.IsClass())
            {
                return false;
            }

            if (declaration?.BaseType == null)
            {
                return false;
            }

            if (declaration.BaseType.Name == baseType.Name)
            {
                return true;
            }

            return declaration.BaseTypeDef.InheritsBase(baseType);
        }
    }
}
