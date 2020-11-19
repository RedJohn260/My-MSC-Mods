using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MSCLoader;
using UnityEngine;

namespace Minimap
{
	public class SaveUtility
	{
		private static string modName = typeof(SaveUtility).Namespace;

		private static string path = Path.Combine(Application.persistentDataPath, SaveUtility.modName + ".xml");
		public static void Save<T>(T saveData)
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
				xmlSerializerNamespaces.Add("", "");
				StreamWriter output = new StreamWriter(SaveUtility.path);
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "    ",
					NewLineOnAttributes = false,
					OmitXmlDeclaration = true
				};
				XmlWriter xmlWriter = XmlWriter.Create(output, settings);
				xmlSerializer.Serialize(xmlWriter, saveData, xmlSerializerNamespaces);
				xmlWriter.Close();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				ModConsole.Error(SaveUtility.modName + ": " + ex.ToString());
			}
		}

		public static SaveData Load<T>()
		{
			SaveData result;
			try
			{
				bool flag = File.Exists(SaveUtility.path);
				if (flag)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					StreamReader input = new StreamReader(SaveUtility.path);
					XmlReader xmlReader = XmlReader.Create(input);
					result = (xmlSerializer.Deserialize(xmlReader) as SaveData);
				}
				else
				{
					result = new SaveData();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				ModConsole.Error(SaveUtility.modName + ": " + ex.ToString());
				result = new SaveData();
			}
			return result;
		}

		public static void Remove()
		{
			bool flag = File.Exists(SaveUtility.path);
			if (flag)
			{
				File.Delete(SaveUtility.path);
				ModConsole.Print(SaveUtility.modName + ": Savefile found and deleted, mod is reset.");
			}
			else
			{
				ModConsole.Print(SaveUtility.modName + ": Savefile not found, mod is already reset.");
			}
		}

	}
}
