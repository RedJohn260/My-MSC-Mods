using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
	public class MiniMapController : MonoBehaviour
	{
		public Transform playerPos;

		public Camera mapCamera;

		public static List<MapObject> mapObjects = new List<MapObject>();

		public static void RegisterMapObject(GameObject o, Image i)
		{
			Image icon = UnityEngine.Object.Instantiate(i);
			mapObjects.Add(new MapObject
			{
				owner = o,
				icon = icon
			});
		}

		public static void RemoveMapObject(GameObject o)
		{
			List<MapObject> list = new List<MapObject>();
			for (int i = 0; i < mapObjects.Count; i++)
			{
				if (mapObjects[i].owner == o)
				{
					UnityEngine.Object.Destroy(mapObjects[i].icon);
				}
				else
				{
					list.Add(mapObjects[i]);
				}
			}
			mapObjects.RemoveRange(0, mapObjects.Count);
			mapObjects.AddRange(list);
		}

		private void DrawMapIcons()
		{
			foreach (MapObject mapObject in mapObjects)
			{
				Vector3 vector = mapCamera.WorldToViewportPoint(mapObject.owner.transform.position);
				mapObject.icon.transform.SetParent(base.transform);
				RectTransform component = GetComponent<RectTransform>();
				Vector3[] array = new Vector3[4];
				component.GetWorldCorners(array);
				Rect rect = RectTransformToScreenSpace(component);
				vector.x = Mathf.Clamp(vector.x * rect.width + array[0].x, array[0].x, array[2].x);
				vector.y = Mathf.Clamp(vector.y * rect.height + array[0].y, array[0].y, array[1].y);
				vector.z = 0f;
				vector.z = 0f;
				mapObject.icon.transform.position = vector;
			}
		}

		public static Rect RectTransformToScreenSpace(RectTransform transform)
		{
			Vector2 vector = Vector2.Scale(transform.rect.size, transform.lossyScale);
			float left = transform.position.x + transform.anchoredPosition.x;
			float top = (float)Screen.height - transform.position.y - transform.anchoredPosition.y;
			return new Rect(left, top, vector.x, vector.y);
		}

		private void Update()
		{
			DrawMapIcons();
		}
	}
}
