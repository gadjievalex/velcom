using Jsonintegrity.ServiceBlock.JsonMetaDataParser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonintegrity.ServiceBlock.JsonComponent
{
    class JsonRawDataTile
    {
        private static string delimiter = @ConfigurationManager.AppSettings["delimiter"];

        public JsonRawDataTile() { }
        public JsonRawDataTile(string serviceInfo)
        {
            StreamReader readingFile = new StreamReader(serviceInfo);
            string readingLine = readingFile.ReadLine();
            int metaDataAreaIndex = readingLine.IndexOf(delimiter);

            string serviceMetaData = readingLine.Substring(0, metaDataAreaIndex);


            jsonRequestHash = MetaDataParser.GetJsonHash(serviceMetaData);
            messageTileNumber = MetaDataParser.GetJsonRawNumber(serviceMetaData);

            int jsonAreaIndex = metaDataAreaIndex + delimiter.Length;
            jsonTile = readingLine.Substring(jsonAreaIndex);
        }

        private string jsonRequestHash;
        private int messageTileNumber;
        private string jsonTileFileName;
        private string jsonTile;

        public string RequestHash { get => jsonRequestHash; set => jsonRequestHash = value; }
        public int MessageTileNumber { get => messageTileNumber; set => messageTileNumber = value; }
        public string JsonTileFileName { get => jsonTileFileName; set => jsonTileFileName = value; }
        public string JsonTextTile { get => jsonTile; set => jsonTile = value; }
    }
}
