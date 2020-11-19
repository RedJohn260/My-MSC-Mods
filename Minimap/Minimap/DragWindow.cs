using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minimap
{
	public class DragWindow : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		public RectTransform dragTransform;

		public Canvas canvas;

		public Image backgroundImage;

		private Color backgroundColor;

		public void OnBeginDrag(PointerEventData eventData)
		{
			backgroundColor = backgroundImage.color;
		}

		public void OnDrag(PointerEventData eventData)
		{
			dragTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			backgroundColor.a = 0.4f;
			backgroundImage.color = backgroundColor;
		}
	}
}
