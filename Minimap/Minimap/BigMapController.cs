using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
	public class BigMapController : MonoBehaviour
	{
		public Transform playerPos;

		public Camera mapCamera;

		public static List<MapObjectB> mapObjects = new List<MapObjectB>();

		public static void RegisterMapObject(GameObject o, Image i)
		{
			Image iconB = Object.Instantiate(i);
			mapObjects.Add(new MapObjectB
			{
				ownerB = o,
				iconB = iconB
			});
		}

		public static void RemoveMapObject(GameObject o)
		{
			List<MapObjectB> list = new List<MapObjectB>();
			for (int i = 0; i < mapObjects.Count; i++)
			{
				if (mapObjects[i].ownerB == o)
				{
					Object.Destroy(mapObjects[i].iconB);
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
			foreach (MapObjectB mapObject in mapObjects)
			{
				Vector3 vector = mapCamera.WorldToViewportPoint(mapObject.ownerB.transform.position);
				mapObject.iconB.transform.SetParent(base.transform);
				RectTransform component = GetComponent<RectTransform>();
				Vector3[] array = new Vector3[4];
				component.GetWorldCorners(array);
				Rect rect = RectTransformToScreenSpace(component);
				vector.x = Mathf.Clamp(vector.x * rect.width + array[0].x, array[0].x, array[2].x);
				vector.y = Mathf.Clamp(vector.y * rect.height + array[0].y, array[0].y, array[1].y);
				vector.z = 0f;
				vector.z = 0f;
				mapObject.iconB.transform.position = vector;
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
