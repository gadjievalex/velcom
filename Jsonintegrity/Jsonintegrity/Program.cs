using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonintegrity
{
    class Program
    {
        private static string dirName = @ConfigurationManager.AppSettings["dirName"];
        private static string outputFileName = @ConfigurationManager.AppSettings["outputFile"];

        private static SortedDictionary<int, string> rawJsonCollection = new SortedDictionary<int, string>();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] files = Directory.GetFiles(dirName);

            foreach (string s in files)
            {
                JsonRawDataTile jsonRaw = new JsonRawDataTile(s);
                if (jsonRaw.MessageTileNumber != 0)
                {
                    rawJsonCollection.Add(jsonRaw.MessageTileNumber, jsonRaw.JsonTextTile);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Метаданные файла {0} не валидны Код:0002.", s);
                }

            }

            using (StreamWriter outputFile = new StreamWriter(outputFileName))
            {
                foreach (KeyValuePair<int, string> entry in rawJsonCollection)
                {
                    outputFile.WriteLine(entry.Value.Trim());
                }
            }

            //string[] all = Directory.GetFiles(dirName, "*.txt")
            //      .Select(x => Path.GetFileNameWithoutExtension(x))
            //      .Distinct().ToArray();
            Console.ReadLine();
        }
    }
}
