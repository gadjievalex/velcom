using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonintegrity.DebugUtilities
{
    class Logger
    {
        

        public static void LogMessage(ConsoleColor color, string message)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
