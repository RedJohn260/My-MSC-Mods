using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PaintBooth
{
    public class SwitchBehavior : MonoBehaviour
    {
        public GameObject prekidac;

        public GameObject booth;

        public GameObject light_child;

        public Vector3 eular;

        public Ray ray;

        public RaycastHit hit;

        public GameObject player;

        public FsmBool GUIuse;

        public Material sign_mat;

        public GameObject sign_light;

        public bool played1 = false;

        public bool played2 = false;

        public AudioSource switch_audio;

        public GameObject wall_radiator;

        public AudioSource radiator_audio;

        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("PLAYER");
            GUIuse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            booth = base.transform.parent.gameObject;
            light_child = booth.transform.FindChild("booth_lights_emmit").gameObject;
            prekidac = booth.transform.FindChild("Salter").transform.FindChild("SalterPrekidac").gameObject;
            sign_mat = booth.transform.FindChild("booth_sign").GetComponent<MeshRenderer>().materials[1];
            sign_light = booth.transform.FindChild("booth_sign").transform.FindChild("sign_light").gameObject;
            eular = prekidac.transform.localEulerAngles;
            eular = new Vector3(-80, 90, -90);
            light_child.SetActive(false);
            sign_light.SetActive(false);
            sign_mat.DisableKeyword("_EMISSION");
            switch_audio = prekidac.GetComponent<AudioSource>();
            wall_radiator = booth.transform.FindChild("booth_radiator_wall").gameObject;
            radiator_audio = wall_radiator.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.name == "SalterPrekidac")
            {
                if (Vector3.Distance(hit.collider.transform.position, player.transform.position) < 2.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!played1)
                        {
                            switch_audio.Play();
                            if (!radiator_audio.isPlaying)
                            {
                                radiator_audio.Play();
                            }
                            else
                            {
                                radiator_audio.Stop();
                            }
                            radiator_audio.Play();
                            played1 = true;
                            played2 = false;
                        }
                        prekidac.transform.localEulerAngles = new Vector3(-100, 90, -90);
                        light_child.SetActive(true);
                        sign_light.SetActive(true);
                        sign_mat.EnableKeyword("_EMISSION");
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (!played2)
                        {
                            switch_audio.Play();
                            if (radiator_audio.isPlaying)
                            {
                                radiator_audio.Stop();
                            }
                            else
                            {
                                radiator_audio.Play();
                            }
                            played2 = true;
                            played1 = false;
                        }
                        prekidac.transform.localEulerAngles = new Vector3(-80, 90, -90);
                        light_child.SetActive(false);
                        sign_light.SetActive(false);
                        sign_mat.DisableKeyword("_EMISSION");
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