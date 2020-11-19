using System;
using UnityEngine;

namespace Minimap
{
	public class CameraZoom : MonoBehaviour
	{
		private float zoomMax = 2044.3f;

		private float zoomMin = 400f;

		private float currentZoom;

		public Camera BigCam;

		private float camPosX = 225f;

		private float camPosY = 46012f;

		private float camPosZ = 143f;

		private float speed = 800f;

		public GameObject map;

		private void Start()
		{
			BigCam.orthographicSize = zoomMax;
		}

		private void Update()
		{
			currentZoom = BigCam.orthographicSize;
			currentZoom = BigCam.orthographicSize;
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				if (currentZoom > zoomMin)
				{
					currentZoom -= 50f;
					BigCam.orthographicSize = currentZoom;
				}
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				if (currentZoom < zoomMax)
				{
					currentZoom += 50f;
					BigCam.orthographicSize = currentZoom;
				}
				else if (currentZoom == zoomMax)
				{
					ResetPos();
				}
			}
			if (!Input.GetMouseButton(0))
			{
				return;
			}
			if (Input.GetAxis("Mouse X") < 0f)
			{
				if (currentZoom < zoomMax)
				{
					BigCam.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
				}
			}
			else if (Input.GetAxis("Mouse X") > 0f && currentZoom < zoomMax)
			{
				BigCam.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
			}
		}

		public void ResetPos()
		{
			BigCam.orthographicSize = zoomMax;
			BigCam.transform.position = new Vector3(camPosX, camPosY, camPosZ);
		}
	}
}
