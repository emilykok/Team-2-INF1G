using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Buffer
{
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
