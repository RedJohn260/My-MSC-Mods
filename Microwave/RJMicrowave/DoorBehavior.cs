using System.Collections;
using UnityEngine;
using HutongGames.PlayMaker;
using System;
using MSCLoader;

namespace RJMicrowave
{
    public class DoorBehavior : MonoBehaviour
    {
        private Collider handleCol;
        private FsmBool GuiUse;
        private FsmString GuiInteract;
        private Animation DoorAnimation;
        private bool IsDoorAnimPlayed;
        private AudioSource DoorOpenSound;
        private AudioSource DoorCloseSound;
        public bool IsDoorOpened;
        private bool mouseTrigger;
        // Use this for initialization
        void Start()
        {
            handleCol = transform.FindChild("handle").GetComponent<Collider>();
            GuiUse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            GuiInteract = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
            DoorAnimation = GetComponent<Animation>();
            DoorOpenSound = GetComponents<AudioSource>()[0];
            DoorCloseSound = GetComponents<AudioSource>()[1];
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if (Camera.main != null && handleCol.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _, 1f))
                {
                    mouseTrigger = true;
                    GuiUse.Value = true;
                    GuiInteract.Value = "Interact";
                    if (cInput.GetButtonDown("Use") && !IsDoorAnimPlayed)
                    {
                        if (!DoorAnimation.IsPlaying("microwaveDoorClose"))
                        {
                            DoorAnimation.Play("microwaveDoorOpen");
                            IsDoorAnimPlayed = !IsDoorAnimPlayed;
                            IsDoorOpened = true;
                            ModConsole.Print("Door Opened: " + IsDoorOpened.ToString());
                            DoorOpenSound.Play();
                        }
                    }
                    else if (cInput.GetButtonDown("Use") && IsDoorAnimPlayed)
                    {
                        if (!DoorAnimation.IsPlaying("microwaveDoorOpen"))
                        {
                            DoorAnimation.Play("microwaveDoorClose");
                            IsDoorAnimPlayed = !IsDoorAnimPlayed;
                            IsDoorOpened = false;
                            ModConsole.Print("Door Opened: " + IsDoorOpened.ToString());
                            DoorCloseSound.Play();
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
    }
}