using System;
using UnityEngine;

namespace Minimap
{
	public class FollowPlayerCam : MonoBehaviour
	{
		public Transform player;

		private void Update()
		{
			base.transform.position = new Vector3(player.position.x, base.transform.position.y, player.position.z);
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, player.eulerAngles.y, base.transform.eulerAngles.z);
		}
	}
}
