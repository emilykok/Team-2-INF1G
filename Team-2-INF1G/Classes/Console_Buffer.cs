using System;
using System.Diagnostics.CodeAnalysis;

namespace Console_Buffer
{
    [ExcludeFromCodeCoverage]
    class Console_Reset
    {
        public static void clear()
        {
            try
            {
                Console.Clear();
            }
            catch { }
        }
    }
}
