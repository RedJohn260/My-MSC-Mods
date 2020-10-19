using UnityEngine;
using System.Collections;
using System;
using MSCLoader;
using System.Timers;
using System.Linq;
using System.Collections.Generic;

namespace M67Granade
{
	public class Granade : MonoBehaviour
	{


		public float _delay = 5f;

		public float countdown;

		public float radius = 15f;

		public GameObject explosionEffect;

		public bool hasExploded = false;

		public float explosionForce = 100000f;

		public AudioSource bounceSound;

		public AudioSource explosionSound;

		public AudioSource pinSound;

		public AudioSource handleSound;

		public Animation removePinAnim;

		public Animation removeHandleAnim;

		public bool explode = false;

		private CapsuleCollider trigger;

		private GameObject pin;

		private GameObject handle;

		private bool pinAnimHasFinished = false;

		private float _animDelay = 0.7f;

		private float _animcount;

		private bool handleAnimPlayed = false;

		private GameObject PLAYER;



		void Start()
		{
			countdown = _delay;
			_animcount = _animDelay;
			pin = gameObject.transform.FindChild("pin").gameObject;
			handle = gameObject.transform.FindChild("handle").gameObject;
			explosionSound = gameObject.transform.GetComponent<AudioSource>();
			bounceSound = gameObject.transform.FindChild("bounce").GetComponent<AudioSource>();
			pinSound = gameObject.transform.FindChild("pin").GetComponent<AudioSource>();
			handleSound = gameObject.transform.FindChild("handle").GetComponent<AudioSource>();
			trigger = gameObject.transform.GetComponent<CapsuleCollider>();
			removePinAnim = gameObject.transform.FindChild("pin").GetComponent<Animation>();
			removeHandleAnim = gameObject.transform.FindChild("handle").GetComponent<Animation>();
			PLAYER = GameObject.Find("PLAYER");
		}

		// Update is called once per frame
		public void Update()
		{
			if (pinAnimHasFinished)
			{
				_animcount -= Time.deltaTime;
			}
			if (explode)
			{
				countdown -= Time.deltaTime;
			}
			if (countdown <= 0 && !hasExploded)
			{
				Explode();
				hasExploded = true;
			}
			RAY();
			RemoveHandle();
		}

		public void OnCollisionEnter(Collision col)
		{
			if (!bounceSound.isPlaying)
			{
				bounceSound.volume = col.relativeVelocity.magnitude / 50;
				bounceSound.Play();
			}
		}

		public void Explode()
		{
			GameObject exef = UnityEngine.Object.Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
			foreach (Collider nearbyObject in colliders)
			{
				Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
				bool playerdeath = Vector3.Distance(PLAYER.transform.position, base.transform.position) < 5f;
				if (rb != null)
				{
					rb.AddExplosionForce(explosionForce, gameObject.transform.position, radius);
				}

				if (playerdeath)
				{
					PlayMakerFSM.BroadcastEvent("DEATH");
				}

			}

			if (!explosionSound.isPlaying)
			{
				explosionSound.Play();
				Destroy(gameObject, 5f);
				Destroy(exef, 5f);
			}
		}
		private void RAY()
		{
			if (Camera.main != null)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
				foreach (RaycastHit hit in hits)
				{
					if (hit.collider.name == trigger.name)
					{
						if (Input.GetKeyDown(KeyCode.F))
						{
							RemovePin();
						}
						PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
						PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "";
						break;
					}
				}
			}
		}

		private void RemovePin()
		{
			if (!removePinAnim.isPlaying)
			{
				removePinAnim.Play();
				if (!pinSound.isPlaying)
				{
					pinSound.Play();
					UnityEngine.Object.Destroy(pin, 0.5f);
					pinAnimHasFinished = true;
				}
			}
		}
		private void RemoveHandle()
		{
			if (!removeHandleAnim.isPlaying && !handleAnimPlayed && _animcount <= 0)
			{
				Rigidbody rb = handle.AddComponent<Rigidbody>();
				rb.AddExplosionForce(500f, transform.position, 50f);
				if (!handleSound.isPlaying)
				{
					handleSound.Play();
					UnityEngine.Object.Destroy(handle, 0.5f);
					explode = true;
					handleAnimPlayed = true;
				}
			}
		}
	}
}
