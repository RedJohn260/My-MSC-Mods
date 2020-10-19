//Original script made by Fredrik. Resource from My Summer Car Modding discord server.
using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalSpeedo
{
    public class SpeedoAttach : MonoBehaviour
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
            if (!isFitted && other == pivotCollider && base.gameObject.layer == LayerMask.NameToLayer("Wheel"))
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
            if (isFitted && raycast_parent.activeInHierarchy && raycast_object.Value != null && raycast_object.Value == base.gameObject && Input.GetMouseButtonDown(1))
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