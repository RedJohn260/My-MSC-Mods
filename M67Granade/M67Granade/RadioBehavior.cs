using UnityEngine;
using System.Collections;

namespace M67Granade
{
	public class RadioBehavior : MonoBehaviour
	{

		public AudioSource audio;
		private Collider powerTrigger;

		public bool RadioPowerState;

		public DudeBehavior dude;

		private AudioSource powerBtnSound;

		void Start()
		{
			audio = gameObject.transform.FindChild("musicTrack").GetComponent<AudioSource>();
			powerTrigger = gameObject.transform.FindChild("powerTrigger").GetComponent<Collider>();
			powerBtnSound = gameObject.transform.FindChild("pwBtnClick").GetComponent<AudioSource>();
			audio.Play();
		}

		void Update()
		{
			if (RadioPowerState)
			{
				if (audio.isPlaying)
				{
					audio.Pause();
				}
			}
			else
			{
				if (!audio.isPlaying)
				{
					audio.UnPause();
				}
			}
			RAY();
		}

		private void RAY()
		{
			if (Camera.main != null)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
				foreach (RaycastHit hit in hits)
				{
					if (hit.collider.name == powerTrigger.name)
					{
						if (Input.GetKeyDown(KeyCode.F))
						{
							RadioPowerState = !RadioPowerState;
							dude.isRadioPaused = !dude.isRadioPaused;
							if (!powerBtnSound.isPlaying)
							{
								powerBtnSound.Play();
							}
						}
						PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
						PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Power";
						break;
					}
				}
			}
		}
	}

}