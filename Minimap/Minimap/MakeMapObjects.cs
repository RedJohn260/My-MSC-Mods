using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
	public class MakeMapObjects : MonoBehaviour
	{
		public Image image;

		private void Start()
		{
			AddMapObject();
		}

		public void AddMapObject()
		{
			MiniMapController.RegisterMapObject(base.gameObject, image);
			BigMapController.RegisterMapObject(base.gameObject, image);
		}

		public void DestroyMapObject()
		{
		}

		public void OnDestroy()
		{
			MiniMapController.RemoveMapObject(base.gameObject);
			BigMapController.RemoveMapObject(base.gameObject);
		}
	}
}
