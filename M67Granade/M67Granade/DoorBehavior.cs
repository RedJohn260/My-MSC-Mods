using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace M67Granade
{
	public class DoorBehavior : MonoBehaviour
	{

		private Animation doorAnimation;
		private Collider doorTriggerCol;
		private bool doorOpened = false;
		private bool doorClosed = false;
		private AudioSource doorOpenAudio;
		private AudioSource doorCloseAudio;
		private AudioSource doorLockedAudio;
		private GameObject PLAYER;
		public bool isDoorLocked;
		void Start()
		{
			doorAnimation = gameObject.transform.GetComponent<Animation>();
			doorTriggerCol = gameObject.transform.GetComponent<Collider>();
			doorOpenAudio = gameObject.transform.FindChild("doorOpen").GetComponent<AudioSource>();
			doorCloseAudio = gameObject.transform.FindChild("doorClose").GetComponent<AudioSource>();
			doorLockedAudio = gameObject.transform.FindChild("doorLocked").GetComponent<AudioSource>();
			PLAYER = GameObject.Find("PLAYER");
		}

		// Update is called once per frame
		void Update()
		{
			RAY2();
		}
		private void RAY2()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.name == doorTriggerCol.name)
			{
				if (Vector3.Distance(hit.collider.transform.position, PLAYER.transform.position) < 2.0f)
				{
					PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
					if (Input.GetMouseButtonDown(0) && !doorOpened)
					{
						if (!doorAnimation.IsPlaying("doorOpenAnim") && !doorAnimation.IsPlaying("doorCloseAnim"))
						{
							doorAnimation.Play("doorOpenAnim");
							if (!doorOpenAudio.isPlaying)
							{
								doorOpenAudio.Play();
							}
						}
						doorOpened = true;
						doorClosed = false;
					}
					else if (Input.GetMouseButtonDown(0) && !doorClosed)
					{
						if (!doorAnimation.IsPlaying("doorCloseAnim") && !doorAnimation.IsPlaying("doorOpenAnim"))
						{
							doorAnimation.Play("doorCloseAnim");
							if (!doorCloseAudio.isPlaying)
							{
								doorCloseAudio.Play();
							}
						}
						doorClosed = true;
						doorOpened = false;
					}
				}
				else
				{
					PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = false;
				}
			}
		}
	}
}
