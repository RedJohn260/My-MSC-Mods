using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GaragePitCovers
{
    public class CoverAttach : MonoBehaviour
    {
        private FsmBool assemble_gui;

        private FsmBool disassemble_gui;

        private FsmString interaction_gui;

        private GameObject raycast_parent;

        private FsmGameObject raycast_object;

        public bool isFitted;

        public Collider pivotCollider;

        public Collider partCollider;

        public AudioSource soundSource;

        public AudioClip attachSound;

        public AudioClip detachSound;

        // Use this for initialization
        void Start()
        {
            assemble_gui = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIassemble");
            disassemble_gui = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIdisassemble");
            interaction_gui = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
            raycast_parent = GameObject.Find("PLAYER").transform.FindChild("Pivot/AnimPivot/Camera/FPSCamera/1Hand_Assemble/Hand").gameObject;
            raycast_object = PlayMakerFSM.FindFsmOnGameObject(raycast_parent, "PickUp").FsmVariables.FindFsmGameObject("RaycastHitObject");
        }
        private void OnTriggerStay(Collider other)
        {
            if (!isFitted && other == pivotCollider && gameObject.layer == LayerMask.NameToLayer("Wheel"))
            {
                assemble_gui.Value = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Attach();
                    assemble_gui.Value = false;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other == pivotCollider)
            {
                assemble_gui.Value = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
            if (isFitted && raycast_parent.activeInHierarchy && raycast_object.Value != null && raycast_object.Value == gameObject && Input.GetMouseButtonDown(1))
            {
                Detach();
            }
        }
        private void PlaySound(string sound)
        {
            MasterAudio.PlaySound3DAndForget("CarBuilding", transform, attachToSource: false, 1f, null, 0f, sound);
        }
        private IEnumerator FixParent(Transform parent)
        {
            yield return new WaitForEndOfFrame();
            while (transform.parent != parent)
            {
                transform.parent = parent;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                yield return new WaitForEndOfFrame();
            }
        }
        public void Attach(bool playSound = true)
        {
            isFitted = true;
            if (playSound)
            {
                PlaySound("assemble");
            }
            pivotCollider.enabled = false;
            Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.tag = "Untagged";
            transform.parent = pivotCollider.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            StartCoroutine(FixParent(pivotCollider.transform));
        }
        public void Detach()
        {
            isFitted = false;
            PlaySound("disassemble");
            gameObject.tag = "PART";
            transform.parent = null;
            pivotCollider.enabled = true;
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.mass = 5.0f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

    }
}