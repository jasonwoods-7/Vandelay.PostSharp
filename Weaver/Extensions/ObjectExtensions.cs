using System;
using PostSharp.Sdk.CodeModel;

namespace Vandelay.PostSharp.Extensions
{
    public static class ObjectExtensions
    {
        public static TypeDefDeclaration GetTypeDefinition(
            this object source) => source switch
        {
            TypeRefDeclaration r => r.GetTypeDefinition(),
            TypeDefDeclaration d => d,
            _ => throw new InvalidOperationException()
        };
    }
}
