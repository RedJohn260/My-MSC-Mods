//Original script made by Fredrik. Resource from My Summer Car Modding discord server.
using MSCLoader;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace M67Granade
{
	public class SaveUtility
	{
		static string modName = typeof(SaveUtility).Namespace;
		static string path = Path.Combine(Application.persistentDataPath, modName + ".xml");

		public static void Save<T>(T saveData)
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
				xmlNamespace.Add("", "");
				StreamWriter output = new StreamWriter(path);
				XmlWriterSettings xmlSettings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "    ",
					NewLineOnAttributes = false,
					OmitXmlDeclaration = true
				};
				XmlWriter xmlWriter = XmlWriter.Create(output, xmlSettings);
				xmlSerializer.Serialize(xmlWriter, saveData, xmlNamespace);
				xmlWriter.Close();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				ModConsole.Error(modName + ": " + ex.ToString());
			}
		}

		public static SaveData Load<T>()
		{
			try
			{
				if (File.Exists(path))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					StreamReader input = new StreamReader(path);
					XmlReader xmlReader = XmlReader.Create(input);
					return xmlSerializer.Deserialize(xmlReader) as SaveData;
				}
				else return new SaveData();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				ModConsole.Error(modName + ": " + ex.ToString());
				return new SaveData();
			}
		}

		public static void Remove()
		{
			if (File.Exists(path))
			{
				File.Delete(path);
				ModConsole.Print(modName + ": Savefile found and deleted, mod is reset.");
			}
			else ModConsole.Print(modName + ": Savefile not found, mod is already reset.");
		}
	}
}