using MSCLoader;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace GaragePitCovers
{
    public class SaveUtility
    {
        private static string modName = typeof(SaveUtility).Namespace;

        private static string path = Path.Combine(Application.persistentDataPath, modName + ".xml");

        public static void WriteFile<T>(T value)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                StreamWriter output = new StreamWriter(path);
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "    ",
                    NewLineOnAttributes = false,
                    OmitXmlDeclaration = true
                };
                XmlWriter xmlWriter = XmlWriter.Create(output, settings);
                xmlSerializer.Serialize(xmlWriter, value, xmlSerializerNamespaces);
                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                ModConsole.Error(modName + ": " + ex.ToString());
            }
        }

        public static SaveData ReadFile<T>()
        {
            try
            {
                if (File.Exists(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    XmlReader xmlReader = XmlReader.Create(new StreamReader(path));
                    return xmlSerializer.Deserialize(xmlReader) as SaveData;
                }
                return new SaveData();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                ModConsole.Error(modName + ": " + ex.ToString());
                return new SaveData();
            }
        }
    }
}