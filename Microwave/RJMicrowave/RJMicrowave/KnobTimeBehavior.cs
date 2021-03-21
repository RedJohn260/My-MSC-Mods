using HutongGames.PlayMaker;
using System.Collections;
using UnityEngine;
using MSCLoader;
using System;

namespace RJMicrowave
{
    public class KnobTimeBehavior : MonoBehaviour
    {
        private Collider knobTimeCol;
        private AudioSource knobTimeSound;
        private FsmBool GuiUse;
        private FsmString GuiInteract;
        private Vector3 knobTimeCurrentRot;
        private float KnobTimeRotZ;
        private bool mouseTrigger;
        public int CurrentTime;
        // Use this for initialization
        void Start()
        {
            knobTimeCol = transform.GetComponent<Collider>();
            knobTimeSound = transform.GetComponent<AudioSource>();
            GuiUse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            GuiInteract = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
        }

        // Update is called once per frame
        void Update()
        {
            knobTimeCurrentRot = transform.localEulerAngles;
            decimal tempDec = (decimal)knobTimeCurrentRot.z;
            KnobTimeRotZ = (float)Math.Round(tempDec, 2);
            if (Camera.main != null && knobTimeCol.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _, 0.5f))
            {
                mouseTrigger = true;
                GuiUse.Value = true;
                GuiInteract.Value = "Set Time";
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (KnobTimeRotZ >= 0f && KnobTimeRotZ < 270f)
                    {
                        KnobTimeRotZ = KnobTimeRotZ += 18f;
                        transform.localEulerAngles = new Vector3(knobTimeCurrentRot.x, knobTimeCurrentRot.y, KnobTimeRotZ);
                        knobTimeSound.Play();
                    }
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (KnobTimeRotZ < 271f && KnobTimeRotZ > 0.1f)
                    {
                        KnobTimeRotZ = KnobTimeRotZ -= 18f;
                        transform.localEulerAngles = new Vector3(knobTimeCurrentRot.x, knobTimeCurrentRot.y, KnobTimeRotZ);
                        knobTimeSound.Play();
                    }
                }
            }
            else if (mouseTrigger)
            {
                mouseTrigger = false;
                GuiUse.Value = false;
                GuiInteract.Value = "";
            }
            //ModConsole.Print("KnobTimeRotZ: " + KnobTimeRotZ.ToString());
            GetKnobPositions();
            //ModConsole.Print("Current Microwave Time Is: " + CurrentTime.ToString());
        }

        private void GetKnobPositions()
        {
            if (KnobTimeRotZ == 0f)
            {
                CurrentTime = 0;
            }
            else if (KnobTimeRotZ == 18f)
            {
                CurrentTime = 1;
            }
            else if (KnobTimeRotZ == 36f)
            {
                CurrentTime = 2;
            }
            else if (KnobTimeRotZ == 54f)
            {
                CurrentTime = 3;
            }
            else if (KnobTimeRotZ == 72f)
            {
                CurrentTime = 4;
            }
            else if (KnobTimeRotZ == 90f)
            {
                CurrentTime = 5;
            }
            else if (KnobTimeRotZ == 108f)
            {
                CurrentTime = 6;
            }
            else if (KnobTimeRotZ == 126f)
            {
                CurrentTime = 7;
            }
            else if (KnobTimeRotZ == 144f)
            {
                CurrentTime = 8;
            }
            else if (KnobTimeRotZ == 162f)
            {
                CurrentTime = 9;
            }
            else if (KnobTimeRotZ == 180f)
            {
                CurrentTime = 10;
            }
            else if (KnobTimeRotZ == 198f)
            {
                CurrentTime = 15;
            }
            else if (KnobTimeRotZ == 216f)
            {
                CurrentTime = 20;
            }
            else if (KnobTimeRotZ == 234f)
            {
                CurrentTime = 25;
            }
            else if (KnobTimeRotZ == 252f)
            {
                CurrentTime = 30;
            }
            else if (KnobTimeRotZ == 270f)
            {
                CurrentTime = 35;
            }
        }
    }
}