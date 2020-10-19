using UnityEngine;
using System.Collections;

namespace M67Granade
{
	public class DudeBehavior : MonoBehaviour
	{

		public GameObject character;

		private Animator animator;

		private string anim_walking = "isWalking";

		private string anim_button = "isPushingButton";

		private bool _isWalking = false;

		private bool _isPushingButton = false;

		private string anim_walkingBack = "isWalkingBack";

		private string anim_isDancing = "isDancing";

		private bool _isWalkingBack = false;

		private bool _isDancing = false;

		public GameObject secondObject;

		private float countdown;

		private bool setWalkBackTrigger;

		private float delay = 3f;

		public bool isRadioPaused;

		private bool radioBtnPressed = false;

		public RadioBehavior radio;

		private AudioSource swearAudio;


		void Start()
		{
			animator = character.GetComponent<Animator>();
			_isWalking = false;
			_isPushingButton = false;
			swearAudio = character.transform.FindChild("swearing").GetComponent<AudioSource>();
		}

		void Update()
		{
			if (isRadioPaused)
			{
				DudeResetRadio();
				isRadioPaused = false;
				radioBtnPressed = false;
				if (!swearAudio.isPlaying)
				{
					swearAudio.Play();
				}
			}

			countdown -= Time.deltaTime;
			Sequence1();
			if (setWalkBackTrigger && countdown <= 0)
			{
				if (!_isDancing)
				{
					animator.SetTrigger(anim_walkingBack);
					_isWalkingBack = true;
				}
			}
			Sequence2();
		}

		public void DudeResetRadio()
		{
			if (!_isPushingButton)
			{
				animator.SetTrigger(anim_walking);
				_isWalking = true;
				_isWalkingBack = false;
				_isDancing = false;
			}
		}

		private void Sequence1()
		{
			if (_isWalking)
			{
				Vector3 targetDir;
				targetDir = new Vector3(gameObject.transform.position.x - character.transform.position.x, 0f, gameObject.transform.position.z - character.transform.position.z);
				Quaternion rot = Quaternion.LookRotation(targetDir);
				character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
				character.transform.Translate(Vector3.forward * 0.01f);

				if (Vector3.Distance(character.transform.position, gameObject.transform.position) < 1.35)
				{
					animator.SetTrigger(anim_button);
					//character.transform.rotation = gameObject.transform.rotation;
					_isWalking = false;
					_isPushingButton = true;
					countdown = delay;
					radio.audio.UnPause();
					setWalkBackTrigger = !setWalkBackTrigger;
				}
			}
		}

		private void Sequence2()
		{
			if (_isWalkingBack)
			{
				if (!radioBtnPressed)
				{
					radio.RadioPowerState = !radio.RadioPowerState;
					radioBtnPressed = true;
				}

				Vector3 targetDir;
				targetDir = new Vector3(secondObject.transform.position.x - character.transform.position.x, 0f, secondObject.transform.position.z - character.transform.position.z);
				Quaternion rot = Quaternion.LookRotation(targetDir);
				character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
				character.transform.Translate(Vector3.forward * 0.01f);
				_isWalking = false;
				_isPushingButton = false;
				if (Vector3.Distance(character.transform.position, secondObject.transform.position) < 1.0)
				{
					animator.SetTrigger(anim_isDancing);
					character.transform.rotation = secondObject.transform.rotation;
					_isDancing = true;
					if (_isDancing)
					{
						_isWalkingBack = false;
						_isWalking = false;
						_isPushingButton = false;
						animator.ResetTrigger(anim_walkingBack);
						setWalkBackTrigger = !setWalkBackTrigger;
					}
				}
			}
		}
	}
}
