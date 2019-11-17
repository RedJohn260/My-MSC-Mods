using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PaintBooth
{
    public class GarageDoorsBehavior : MonoBehaviour
    {
        public GameObject booth;

        private GameObject doorl;

        private GameObject doorr;

        private Ray ray;

        private RaycastHit hit;

        private RaycastHit hit1;

        private GameObject player;

        private FsmBool GUIuse;

        private bool played_left = false;

        private bool played_reverse_left = false;

        private bool played_right = false;

        private bool played_reverse_right = false;

        private Animation a_clipL;

        private Animation a_clipR;

        private AudioSource g_door_audioL;

        private AudioSource g_door_audioR;

        private GameObject FLEETARI_WEEKENDS;

        private GameObject FLEETARI_SHOP;

        private PlayMakerFSM fleetari_pmc;

        private FsmBool shop_opened;

        private PlayMakerFSM fletarii_weekends_pm;

        private bool fleetari_active;

        private bool left_door_opened = false;

        private bool right_door_opened = false;

        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("PLAYER");
            GUIuse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            booth = base.transform.parent.gameObject;
            doorl = booth.transform.FindChild("door_big_l").gameObject;
            doorr = booth.transform.FindChild("door_big_r").gameObject;
            a_clipL = doorl.GetComponent<Animation>();
            a_clipR = doorr.GetComponent<Animation>();
            g_door_audioL = doorl.GetComponent<AudioSource>();
            g_door_audioR = doorr.GetComponent<AudioSource>();
            FLEETARI_WEEKENDS = GameObject.Find("REPAIRSHOP/LOD/Office").transform.FindChild("Fleetari").gameObject;
            fletarii_weekends_pm = FLEETARI_WEEKENDS.GetComponent<PlayMakerFSM>();
            FLEETARI_SHOP = GameObject.Find("REPAIRSHOP");
            fleetari_pmc = FLEETARI_SHOP.GetComponents<PlayMakerFSM>()[1];
            shop_opened = fleetari_pmc.FsmVariables.FindFsmBool("OpenRepairShop");
        }

        // Update is called once per frame
        void Update()
        {
            if (fletarii_weekends_pm.Active && shop_opened.Value)
            {
                fleetari_active = true;
            }
            else
            {
                fleetari_active = false;
                if(left_door_opened || right_door_opened)
                {
                    a_clipL.Play("garage_door_l_reverse");
                    g_door_audioL.Play();
                    a_clipR.Play("garage_door_r_reverse");
                    g_door_audioR.Play();
                    left_door_opened = false;
                    right_door_opened = false;
                    played_reverse_left = true;
                    played_left = false;
                    played_reverse_right = true;
                    played_right = false;
                }
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.name == "ColiderL")
            {
                if (Vector3.Distance(hit.collider.transform.position, player.transform.position) < 3.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (fleetari_active)
                        {
                            if (!played_left)
                            {
                                if (!a_clipL.IsPlaying("garage_door_l_reverse"))
                                {
                                    a_clipL.Play("garage_door_l");
                                    g_door_audioL.Play();
                                    played_left = true;
                                    played_reverse_left = false;
                                    left_door_opened = true;
                                }
                                else
                                {
                                    a_clipL.Stop("garage_door_l");
                                }
                            }
                        }
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (fleetari_active)
                        {
                            if (!played_reverse_left)
                            {
                                if (!a_clipL.IsPlaying("garage_door_l"))
                                {
                                    a_clipL.Play("garage_door_l_reverse");
                                    g_door_audioL.Play();
                                    played_reverse_left = true;
                                    played_left = false;
                                }
                                else
                                {
                                    a_clipL.Stop("garage_door_l_reverse");
                                }
                            }
                        }
                    }
                }
                else
                {
                    GUIuse.Value = false;

                }
            }
            if (Physics.Raycast(ray, out hit1) && hit1.collider.name == "ColiderR")
            {
                if (Vector3.Distance(hit1.collider.transform.position, player.transform.position) < 3.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (fleetari_active)
                        {
                            if (!played_right)
                            {
                                if (!a_clipR.IsPlaying("garage_door_r_reverse"))
                                {
                                    a_clipR.Play("garage_door_r");
                                    g_door_audioR.Play();
                                    played_right = true;
                                    played_reverse_right = false;
                                    right_door_opened = true;
                                }
                                else
                                {
                                    a_clipR.Stop("garage_door_r");
                                }
                            }
                        }
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (fleetari_active)
                        {
                            if (!played_reverse_right)
                            {
                                if (!a_clipR.IsPlaying("garage_door_r"))
                                {
                                    a_clipR.Play("garage_door_r_reverse");
                                    g_door_audioR.Play();
                                    played_reverse_right = true;
                                    played_right = false;
                                }
                                else
                                {
                                    a_clipR.Stop("garage_door_r_reverse");
                                }
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
    }
}