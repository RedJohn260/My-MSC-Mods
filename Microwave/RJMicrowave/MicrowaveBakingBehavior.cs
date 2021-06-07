using System.Collections;
using UnityEngine;
using MSCLoader;

namespace RJMicrowave
{
    public class MicrowaveBakingBehavior : MonoBehaviour
    {
        public bool IsDoorOpened;
        public string MicrowaveCurrentPowerWatt;
        public int MicrowaveCurrentTime;
        public GameObject PizzaObject;
        private float PlateRotSpeed = 20f;
        public GameObject TimerKnob;
        private AudioSource MicrowaveSoundStart;
        private AudioSource MicrowaveSoundOnLoop;
        private AudioSource MicrowaveSoundEnd;
        private bool SoundStartPlaying;
        private bool SoundOnPlaying;
        private bool SoundEndPlaying;
        private bool MicrowaveON;
        private bool FoodIsDone;
        private bool IsFoodPizza;
        GameObject pizza;
        private float CurrentKnobRotation;

        // Use this for initialization
        void Start()
        {
            MicrowaveSoundStart = GetComponents<AudioSource>()[0];
            MicrowaveSoundOnLoop = GetComponents<AudioSource>()[1];
            MicrowaveSoundEnd = GetComponents<AudioSource>()[2];
        }
        private void Pizza()
        {
            if (IsFoodPizza)
            {
                
            }
        }
        private void MicrowaveSound()
        {
            if (MicrowaveON)
            {
                if (!SoundStartPlaying)
                {
                    StartCoroutine(PlaySoundStart());
                }
                if (!SoundOnPlaying)
                {
                    StartCoroutine(PlaySoundOn());
                }
                if (FoodIsDone)
                {
                    if (!SoundEndPlaying)
                    {
                        if (MicrowaveSoundOnLoop.isPlaying)
                        {
                            MicrowaveSoundOnLoop.Stop();
                            SoundOnPlaying = true;
                        }
                        StartCoroutine(PlaySoundEnd());
                        FoodIsDone = false;
                    }
                }
            }
            else
            {
                if (MicrowaveSoundOnLoop.isPlaying)
                {
                    MicrowaveSoundOnLoop.Stop();
                    //SoundOnPlaying = true;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            //CurrentTimer = MicrowaveCurrentTime * 60;
            CurrentKnobRotation = TimerKnob.transform.localEulerAngles.z;
            if (!IsDoorOpened)
            {
                MicrowaveSound();
                if (MicrowaveCurrentTime >= 1 && MicrowaveCurrentPowerWatt == "100W" || MicrowaveCurrentPowerWatt == "200W" || MicrowaveCurrentPowerWatt == "300W" ||
                    MicrowaveCurrentPowerWatt == "400W" || MicrowaveCurrentPowerWatt == "500W")
                {
                    MicrowaveON = true;
                }
                else
                {
                    MicrowaveON = false;
                    //FoodIsDone = true;
                }
                if (MicrowaveON)
                {
                    gameObject.transform.Rotate(0, PlateRotSpeed * Time.deltaTime, 0);

                    #region fucking knob
                    if (MicrowaveCurrentTime > 0 && MicrowaveCurrentTime < 35 && TimerKnob.transform.localEulerAngles.z < 180f && TimerKnob.transform.localEulerAngles.z > 0.1f && TimerKnob.transform.localEulerAngles.z < 359f && TimerKnob.transform.localEulerAngles.z != -0)
                    {
                        TimerKnob.transform.Rotate(0, 0, -0.3f * Time.deltaTime);
                        //ModConsole.Print(TimerKnob.transform.localEulerAngles.z.ToString());
                    }
                    else
                    {
                        TimerKnob.transform.localEulerAngles = new Vector3(0, 0, 0);
                        FoodIsDone = true;
                    }
                    #endregion
                }
                
            }
        }


        private void OnTriggerEnter(Collider col)
        {
            if (col.name == FoodItems.pizza && col.gameObject.layer == 19)
            {
                Object.Destroy(col.gameObject);
                pizza = Object.Instantiate(PizzaObject);
                pizza.transform.SetParent(gameObject.transform, false);
                pizza.SetActive(true);
                pizza.transform.localPosition = new Vector3(0f, 0.01f, 0f);
                pizza.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
                IsFoodPizza = true;
            }

        }
        private IEnumerator PlaySoundStart()
        {
            if (!MicrowaveSoundStart.isPlaying)
            {
                StopCoroutine(PlaySoundEnd());
                if (MicrowaveSoundEnd.isPlaying)
                {
                    MicrowaveSoundEnd.Stop();
                }
                MicrowaveSoundStart.Play();
                SoundStartPlaying = true;
                SoundOnPlaying = false;
                SoundEndPlaying = false;
            }
            yield return new WaitForSeconds(1);
        }
        private IEnumerator PlaySoundOn()
        {
            if (!MicrowaveSoundOnLoop.isPlaying)
            {
                StopCoroutine(PlaySoundStart());
                if (MicrowaveSoundStart.isPlaying)
                {
                    MicrowaveSoundStart.Stop();
                }
                MicrowaveSoundOnLoop.Play();
                SoundOnPlaying = true;
                SoundStartPlaying = false;
                SoundEndPlaying = false;
            }
            yield return new WaitForSeconds(1);
        }
        private IEnumerator PlaySoundEnd()
        {
            if (!MicrowaveSoundEnd.isPlaying)
            {
                MicrowaveSoundOnLoop.Stop();
                StopCoroutine(PlaySoundOn());
                MicrowaveSoundEnd.PlayOneShot(MicrowaveSoundEnd.clip, 1f);
                SoundEndPlaying = true;
                SoundStartPlaying = false;
                SoundOnPlaying = false;
            }
            yield return new WaitForSeconds(1);
        }
    }
}