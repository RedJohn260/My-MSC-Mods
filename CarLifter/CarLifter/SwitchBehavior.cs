using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace CarLifter
{
    public class SwitchBehavior : MonoBehaviour
    {
        private GameObject lift_switch;

        private GameObject lift;

        private Ray ray;

        private RaycastHit hit;

        private GameObject player;

        private FsmBool GUIuse;

        private bool played1 = false;

        private bool played2 = false;

        private AudioSource switch_audio;

        private Animation lift_anim;

        public AudioSource AudioSource;


        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("PLAYER");
            GUIuse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            lift = base.gameObject;
            lift_switch = lift.transform.FindChild("switch_mesh").gameObject;
            lift_anim = lift.GetComponent<Animation>();
            AudioSource = base.gameObject.GetComponent<AudioSource>();
          
        }

        // Update is called once per frame
        void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.name == "switch_mesh")
            {
                if (Vector3.Distance(hit.collider.transform.position, player.transform.position) < 2.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!played1)
                        {
                            if (!lift_anim.IsPlaying("LiftAnimationR"))
                            {
                                AudioSource.Play();
                                lift_switch.transform.localEulerAngles = new Vector3(65f, 0, 0);
                                lift_anim.Play("LiftAnimation");
                                played1 = true;
                                played2 = false;
                            }
                        }
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (!played2)
                        {
                            if (!lift_anim.IsPlaying("LiftAnimation"))
                            {
                                AudioSource.Play();
                                lift_switch.transform.localEulerAngles = new Vector3(0f, 0, 0);
                                lift_anim.Play("LiftAnimationR");
                                played2 = true;
                                played1 = false;
                            }
                        }
                    }
                }
                else
                {
                    GUIuse.Value = false;

                }
            }
        }
        private void PlaySound(string sound)
        {
            MasterAudio.PlaySound3DAndForget("CarBuilding", base.transform, attachToSource: false, 1f, null, 0f, sound);
        }
    }
}