using MSCLoader;
using UnityEngine;

namespace RPS
{
    public class LegsBehavior : MonoBehaviour
    {
        //private float move_speed = 3f;
        public GameObject leg1;
        public GameObject leg2;
        public GameObject leg3;
        public GameObject leg4;
        public bool leg1anim_played = false;
        private Animation leg1_anim;
        private bool leg2anim_played = false;
        private Animation leg2_anim;
        private bool leg3anim_played = false;
        private Animation leg3_anim;
        private bool leg4anim_played = false;
        private Animation leg4_anim;
        private AudioSource legs_audio;
        // Use this for initialization
        void Start()
        {
            //leg1 = transform.FindChild("leg_1").gameObject;
            //leg2 = transform.FindChild("leg_2").gameObject;
            //leg3 = transform.FindChild("leg_3").gameObject;
            //leg4 = transform.FindChild("leg_4").gameObject;
            legs_audio = this.transform.GetComponent<AudioSource>();
            leg1_anim = leg1.GetComponent<Animation>();
            leg2_anim = leg2.GetComponent<Animation>();
            leg3_anim = leg3.GetComponent<Animation>();
            leg4_anim = leg4.GetComponent<Animation>();
        }

        // Update is called once per frame
        void Update()
        {
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
                    if (hit.collider.name == leg1.name)
                    {
                        if (Input.GetMouseButtonDown(0) && !leg1anim_played)
                        {
                            if (!leg1_anim.IsPlaying("leg1_animB"))
                            {
                                 leg1_anim.Play("leg1_animF");
                                leg1anim_played = !leg1anim_played;
                                legs_audio.Play();
                            }
                        }
                        else if (Input.GetMouseButtonDown(0) && leg1anim_played)
                        {
                            if (!leg1_anim.IsPlaying("leg1_animF"))
                            {
                                leg1_anim.Play("leg1_animB");
                                leg1anim_played = !leg1anim_played;
                                legs_audio.Play();
                            }
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Move";
                        break;
                    }
                    if (hit.collider.name == leg2.name)
                    {
                        if (Input.GetMouseButtonDown(0) && !leg2anim_played)
                        {
                            if (!leg2_anim.IsPlaying("leg2_animB"))
                            {
                                leg2_anim.Play("leg2_animF");
                                leg2anim_played = !leg2anim_played;
                                legs_audio.Play();
                            }
                        }
                        else if (Input.GetMouseButtonDown(0) && leg2anim_played)
                        {
                            if (!leg2_anim.IsPlaying("leg2_animF"))
                            {
                                leg2_anim.Play("leg2_animB");
                                leg2anim_played = !leg2anim_played;
                                legs_audio.Play();
                            }
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Move";
                        break;
                    }
                    if (hit.collider.name == leg3.name)
                    {
                        if (Input.GetMouseButtonDown(0) && !leg3anim_played)
                        {
                            if (!leg3_anim.IsPlaying("leg3_animB"))
                            {
                                leg3_anim.Play("leg3_animF");
                                leg3anim_played = !leg3anim_played;
                                legs_audio.Play();
                            }
                        }
                        else if (Input.GetMouseButtonDown(0) && leg3anim_played)
                        {
                            if (!leg3_anim.IsPlaying("leg3_animF"))
                            {
                                leg3_anim.Play("leg3_animB");
                                leg3anim_played = !leg3anim_played;
                                legs_audio.Play();
                            }
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Move";
                        break;
                    }
                    if (hit.collider.name == leg4.name)
                    {
                        if (Input.GetMouseButtonDown(0) && !leg4anim_played)
                        {
                            if (!leg4_anim.IsPlaying("leg4_animB"))
                            {
                                leg4_anim.Play("leg4_animF");
                                leg4anim_played = !leg4anim_played;
                                legs_audio.Play();
                            }
                        }
                        else if (Input.GetMouseButtonDown(0) && leg4anim_played)
                        {
                            if (!leg4_anim.IsPlaying("leg4_animF"))
                            {
                                leg4_anim.Play("leg4_animB");
                                leg4anim_played = !leg4anim_played;
                                legs_audio.Play();
                            }
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Move";
                        break;
                    }
                }
            }
        }
    }
}