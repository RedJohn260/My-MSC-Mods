using MSCLoader;
using UnityEngine;
using System.Collections;
using UnityStandardAssets;

namespace RPS
{
    public class LiftingBehavior : MonoBehaviour
    {
        public GameObject lift_switch;
        public bool is_lift_lifted = false;
        public GameObject lifting_part;
        public Vector3 startPos;
        public Vector3 midPos;
        public Vector3 endPos;
        public float timeStartedLerping;
        public float lerpTime = 5.0f;
        private bool shouldLerpUp = false;
        private bool shouldLerpMid = false;
        private bool shouldLerpDown = false;
        private AudioSource switch_sound;
        private AudioSource lift_sound;

        // Use this for initialization
        void Start()
        {
            startPos = new Vector3(0, 0, 0);
            midPos = new Vector3(0, 0, 0.5f);
            endPos = new Vector3(0, 0, 1);
            switch_sound = transform.gameObject.GetComponents<AudioSource>()[0];
            lift_sound = transform.gameObject.GetComponents<AudioSource>()[1];
        }
        
        private void StartLerpingUp()
        {
            lift_sound.Play();
            timeStartedLerping = Time.time;
            shouldLerpUp = true;
            shouldLerpMid = false;
            shouldLerpDown = false;
        }
        private void StartLerpingDown()
        {
            lift_sound.Play();
            timeStartedLerping = Time.time;
            shouldLerpDown = true;
            shouldLerpMid = false;
            shouldLerpUp = false;
        }
        private void StartLerpingMiddle()
        {
            lift_sound.Play();
            timeStartedLerping = Time.time;
            shouldLerpMid = true;
            shouldLerpUp = false;
            shouldLerpDown = false;
        }
        public Vector3 Lerp (Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / lerpTime;
            var results = Vector3.Lerp(start, end, percentageComplete);
            return results;
        }
        // Update is called once per frame
        void Update()
        {
            if (shouldLerpMid)
            {
                lifting_part.transform.localPosition = Lerp(startPos, midPos, timeStartedLerping, lerpTime);
            }
            if (shouldLerpUp)
            {
               lifting_part.transform.localPosition = Lerp(startPos, endPos, timeStartedLerping, lerpTime);
            }
            if (shouldLerpDown)
            {
                lifting_part.transform.localPosition = Lerp(endPos, startPos, timeStartedLerping, lerpTime);
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
                    if (hit.collider.name == lift_switch.name)
                    {
                        if (Input.GetMouseButtonDown(0) && lifting_part.transform.localPosition.z == 0f)
                        {
                            lift_switch.transform.localEulerAngles = new Vector3(45f, 0f, 0f);
                            switch_sound.Play();
                            StartLerpingUp();
                            //is_lift_lifted = !is_lift_lifted;
                        }
                        if (Input.GetMouseButtonDown(0) && lifting_part.transform.localPosition.z > 0.9f)
                        {
                            lift_switch.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                            switch_sound.Play();
                            StartLerpingDown();
                            is_lift_lifted = !is_lift_lifted;
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Use";
                        break;
                    }
                }
            }
        }
    }
}