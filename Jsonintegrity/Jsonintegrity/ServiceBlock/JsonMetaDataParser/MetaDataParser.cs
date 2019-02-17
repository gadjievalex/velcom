using Jsonintegrity.DebugUtilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jsonintegrity.ServiceBlock.JsonMetaDataParser
{
    class MetaDataParser
    {
        private static Regex jsonhashreg = new Regex(@ConfigurationManager.AppSettings["jsonhashreg"], RegexOptions.IgnoreCase);
        private static Regex jsonNumberreg = new Regex(@ConfigurationManager.AppSettings["jsonNumberreg"], RegexOptions.IgnoreCase);

        public static string GetJsonHash(string rawMetaData)
        {
            Match match = jsonhashreg.Match(rawMetaData);
            string hash = match.Groups[1].Value;
            return hash;
        }

        public static int GetJsonRawNumber(string rawMetaData)
        {
            int value = 0;
            Match match = jsonNumberreg.Match(rawMetaData);
            string number = match.Groups[1].Value;
            try
            {
                value = int.Parse(number);
            }
            catch (FormatException e)
            {
                Logger.LogMessage(ConsoleColor.Red, string.Format("Для строки {0} не удалось высчитать порядковый номер Код:0001.", rawMetaData));
            }
            return value;
        }
    }
}
