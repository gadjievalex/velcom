using Jsonintegrity.ServiceBlock.JsonComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Настройки задаются через App.config
 * -путь к папке с исходными данными json
 * -путь по которому сохраняется созданный файл Json
 * -регулярки для выборки хешЗначения , порядкового номера файла, разделителя метаДанных от основной информации
 * 
 * - планируемые доработки 
 *      -- не учитывается блокировка, переход например на Threading 
 *      --SortedDictionary довольно медленный подход - можно написать что то вроде - class ComparableRawJson<K, V> : IDictionary<K, V> where K: IComparable<K>
 *      --отсутствует отвязка от конкретных реализация - как вариант интерфейсы и контенер(например ninject https://github.com/ninject/ninject)
 *      --для файла конфигурации создать свой класс 
 */

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
