﻿using Jsonintegrity.DebugUtilities;
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
 *      --не устранено отображение переноса и перехода каретки
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
            Logger.LogMessage(ConsoleColor.Green, " Попытка получить список файлов в обрабатываемой директории ");
            string[] files = Directory.GetFiles(dirName);

            
            foreach (string s in files)
            {
                Logger.LogMessage(ConsoleColor.Green, string.Format(" Обрабатывается файл {0} ",s));
                JsonRawDataTile jsonRaw = new JsonRawDataTile(s);
                if (jsonRaw.MessageTileNumber != 0)
                {
                    rawJsonCollection.Add(jsonRaw.MessageTileNumber, jsonRaw.JsonTextTile);
                }
                else
                {
                    Logger.LogMessage(ConsoleColor.Red, string.Format("Метаданные файла {0} не валидны Код:0002.", s));
                }
                Logger.LogMessage(ConsoleColor.Blue, string.Format(" файл {0} успешно", s));
            }

            using (StreamWriter outputFile = new StreamWriter(outputFileName))
            {
                foreach (KeyValuePair<int, string> entry in rawJsonCollection)
                {
                    outputFile.WriteLine(entry.Value.Trim());
                }
                Logger.LogMessage(ConsoleColor.Cyan, string.Format(" Результирующий файл {0} создан", outputFileName));
            }

            //string[] all = Directory.GetFiles(dirName, "*.txt")
            //      .Select(x => Path.GetFileNameWithoutExtension(x))
            //      .Distinct().ToArray();
            Console.ReadLine();
        }
    }
}
