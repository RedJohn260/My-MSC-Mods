using System.Collections;
using UnityEngine;
using HutongGames.PlayMaker;
using System;
using MSCLoader;

namespace RJMicrowave
{
    public class BoxOpenBehavior : MonoBehaviour
    {
        public GameObject Microwave;
        private MeshRenderer boxMeshRenderer;
        private bool isBoxDestroyed;
        private FsmBool GuiUse;
        private FsmString GuiInteract;
        private bool mouseTrigger = false;
        private BoxCollider boxCollider;
        private BoxCollider boxCollider1;
        public AudioSource boxSound;
        public GameObject boxChild;

        // Use this for initialization
        void Start()
        {
            GuiUse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            GuiInteract = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
            boxCollider = GetComponents<BoxCollider>()[1];
            boxCollider1 = GetComponents<BoxCollider>()[0];
            boxChild = transform.FindChild("Box").gameObject;
            boxMeshRenderer = boxChild.GetComponent<MeshRenderer>();
            boxSound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if (Camera.main != null && !isBoxDestroyed && boxCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _, 2f))
                {
                    mouseTrigger = true;
                    GuiUse.Value = true;
                    GuiInteract.Value = "Open";
                    if (cInput.GetButtonDown("Use") || Input.GetKeyDown(KeyCode.F))
                    {
                        if (!boxSound.isPlaying)
                        {
                            boxSound.PlayOneShot(boxSound.clip, 1f);
                            StartCoroutine(DestroyBox());
                            GuiUse.Value = false;
                            GuiInteract.Value = "";
                        }
                    }
                }
                else if (mouseTrigger)
                {
                    mouseTrigger = false;
                    GuiUse.Value = false;
                    GuiInteract.Value = "";
                }
            }
            catch(Exception e)
            {
                ModConsole.Error("RJMicrovawe: " + e.Message);
                Debug.LogError("RJMicrovawe: " + e.Message);
            }
        }
        private IEnumerator DestroyBox()
        {
            try
            {
                Microwave.transform.position = gameObject.transform.position;
                Microwave.transform.eulerAngles = gameObject.transform.eulerAngles;
                boxMeshRenderer.enabled = false;
                Microwave.SetActive(true);
                isBoxDestroyed = true;
                gameObject.tag = "Untagged";
                gameObject.layer = LayerMask.NameToLayer("Default");
                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(boxCollider);
                UnityEngine.Object.Destroy(boxCollider1);
                UnityEngine.Object.Destroy(gameObject.GetComponent<BoxOpenBehavior>());
                UnityEngine.Object.Destroy(gameObject);
            }
            catch (Exception e)
            {
                ModConsole.Error("RJMicrovawe: " + e.Message);
                Debug.LogError("RJMicrovawe: " + e.Message);
            }
            yield return new WaitForSeconds(1);
            StopCoroutine(DestroyBox());
        }
    }
}