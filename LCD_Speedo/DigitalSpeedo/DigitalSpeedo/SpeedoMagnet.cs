using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalSpeedo
{
    public class SpeedoMagnet : MonoBehaviour
    {
        private FsmBool guiAssemble;

        private FsmBool guiDisassemble;

        private FsmString guiInteraction;

        private GameObject raycastParent;

        private FsmGameObject raycastObject;

        public bool isFitted;

        public Collider pivotCollider;

        public Collider partCollider;

        public AudioSource soundSource;

        public AudioClip attachSound;

        public AudioClip detachSound;

        // Use this for initialization
        void Start()
        {
            guiAssemble = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIassemble");
            guiDisassemble = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIdisassemble");
            guiInteraction = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
            raycastParent = GameObject.Find("PLAYER").transform.FindChild("Pivot/AnimPivot/Camera/FPSCamera/1Hand_Assemble/Hand").gameObject;
            raycastObject = PlayMakerFSM.FindFsmOnGameObject(raycastParent, "PickUp").FsmVariables.FindFsmGameObject("RaycastHitObject");
        }
        private void OnTriggerStay(Collider other)
        {
            if (!isFitted && other == pivotCollider && base.gameObject.layer == LayerMask.NameToLayer("Wheel"))
            {
                guiAssemble.Value = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Attach();
                    guiAssemble.Value = false;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other == pivotCollider)
            {
                guiAssemble.Value = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isFitted && raycastParent.activeInHierarchy && raycastObject.Value != null && raycastObject.Value == base.gameObject && Input.GetMouseButtonDown(1))
            {
                Detach();
            }
        }
        private void PlaySound(string sound)
        {
            MasterAudio.PlaySound3DAndForget("CarBuilding", base.transform, attachToSource: false, 1f, null, 0f, sound);
        }
        private IEnumerator FixParent(Transform parent)
        {
            yield return new WaitForEndOfFrame();
            while (base.transform.parent != parent)
            {
                base.transform.parent = parent;
                base.transform.localPosition = Vector3.zero;
                base.transform.localEulerAngles = Vector3.zero;
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
            Object.Destroy(base.gameObject.GetComponent<Rigidbody>());
            base.gameObject.tag = "Untagged";
            base.transform.parent = pivotCollider.transform;
            base.transform.localPosition = Vector3.zero;
            base.transform.localEulerAngles = Vector3.zero;
            StartCoroutine(FixParent(pivotCollider.transform));
        }
        public void Detach()
        {
            isFitted = false;
            PlaySound("disassemble");
            base.gameObject.tag = "PART";
            base.transform.parent = null;
            pivotCollider.enabled = true;
            Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
            rigidbody.mass = 0.75f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        
    }
}