using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
	internal class IconAdding : MonoBehaviour
	{
		//public Text text;
		public static void AddIcon(GameObject gameObject, Image Icon)
		{
			MakeMapObjects makeMapObjects = gameObject.AddComponent<MakeMapObjects>();
			makeMapObjects.image = Icon;
			//Blink blink = gameObject.AddComponent<Blink>();
		}

		public static void DestroyIcon(GameObject gameObject)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
