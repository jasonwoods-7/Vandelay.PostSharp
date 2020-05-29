using System;
using System.Collections.Generic;

namespace Vandelay
{
    public static class Import
    {
        public static IReadOnlyList<T> Many<T>(
            string searchPatterns, params object[] exports)
        {
            throw new NotSupportedException();
        }
    }
}
