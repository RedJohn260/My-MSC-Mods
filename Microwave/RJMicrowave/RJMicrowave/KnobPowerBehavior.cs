using HutongGames.PlayMaker;
using MSCLoader;
using System;
using UnityEngine;

namespace RJMicrowave
{
    public class KnobPowerBehavior : MonoBehaviour
    {
        private Collider knobPowerCol;
        private AudioSource knobPowerSound;
        private FsmBool GuiUse;
        private FsmString GuiInteract;
        public GameObject KnobPower;
        private Vector3 knobPowerCurrentRot;
        private bool mouseTrigger;
        private float KnobPowerRotZ;
        public string MicrowavePowerWatt;

        void Start()
        {
            knobPowerCol = KnobPower.transform.GetComponent<Collider>();
            knobPowerSound = KnobPower.transform.GetComponent<AudioSource>();
            GuiUse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            GuiInteract = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
        }

        void Update()
        {
            knobPowerCurrentRot = transform.localEulerAngles;
            decimal tempDec = (decimal)knobPowerCurrentRot.z; // converting float to decimal
            KnobPowerRotZ = (float)Math.Round(tempDec, 2); // converting back to float
            if (Camera.main != null && knobPowerCol.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _, 0.5f))
            {
                mouseTrigger = true;
                GuiUse.Value = true;
                GuiInteract.Value = "Set Power";
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (KnobPowerRotZ >= 0f && KnobPowerRotZ < 180f)
                    {
                        KnobPowerRotZ = KnobPowerRotZ += 22.5f;
                        //ModConsole.Print("KnobPowerRotZ: " + KnobPowerRotZ.ToString() + " knobPowerPassedTwo: " + knobPowerPassedTwo.ToString());
                        transform.localEulerAngles = new Vector3(knobPowerCurrentRot.x, knobPowerCurrentRot.y, KnobPowerRotZ);
                        knobPowerSound.Play();
                    }
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (KnobPowerRotZ < 181f && KnobPowerRotZ > 0.1f)
                    {
                        KnobPowerRotZ = KnobPowerRotZ -= 22.5f;
                        //ModConsole.Print("KnobPowerRotZ: " + KnobPowerRotZ.ToString() + " knobPowerPassedTwo: " + knobPowerPassedTwo.ToString());
                        transform.localEulerAngles = new Vector3(knobPowerCurrentRot.x, knobPowerCurrentRot.y, KnobPowerRotZ);
                        knobPowerSound.Play();
                    }
                }
            }
            else if (mouseTrigger)
            {
                mouseTrigger = false;
                GuiUse.Value = false;
                GuiInteract.Value = "";
            }
            //ModConsole.Print("KnobPowerRotZ: " + KnobPowerRotZ.ToString());
            CurrentMicrowavePower();
            //ModConsole.Print("Current Microwave Power Is : " + MicrowavePowerWatt);
        }
        private void CurrentMicrowavePower()
        {
            if (KnobPowerRotZ == 0f)
            {
                MicrowavePowerWatt = "0W";
            }
            else if (KnobPowerRotZ == 90f)
            {
                MicrowavePowerWatt = "100W";
            }
            else if (KnobPowerRotZ == 112.5f)
            {
                MicrowavePowerWatt = "200W";
            }
            else if (KnobPowerRotZ == 135f)
            {
                MicrowavePowerWatt = "300W";
            }
            else if (KnobPowerRotZ == 157.5f)
            {
                MicrowavePowerWatt = "400W";
            }
            else if (KnobPowerRotZ == 180f)
            {
                MicrowavePowerWatt = "500W";
            }
        }
    }
}