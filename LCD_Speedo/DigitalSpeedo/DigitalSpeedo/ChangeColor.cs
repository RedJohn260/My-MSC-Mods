using MSCLoader;
using UnityEngine;

namespace DigitalSpeedo
{
    public class ChangeColor : MonoBehaviour
    {
        public Collider glas_frontCollider;
        public GameObject bg_button;
        public GameObject t_button;
        public Material e_buttonbg;
        public Material e_buttontx;
        public Material bgmat;
        public Material textmat;
        public bool _isAttached;
        public Color[] bgcolors;
        public Color[] textcolors;
        private int indexbg;
        private int indextx;
        public AudioClip button_push;
        public AudioSource audioSource;

        // Use this for initialization
        void Start()
        {
            bgcolors = new Color[3];
            bgcolors[0] = new Color(0.15f, 0.3f, 0.1f);
            bgcolors[1] = new Color(0.1f, 0.15f, 0.4f);
            bgcolors[2] = new Color(0.3f, 0.15f, 0.0f);

            textcolors = new Color[3];
            textcolors[0] = new Color(0.54f, 0, 0);
            textcolors[1] = new Color(0, 0.54f, 0);
            textcolors[2] = new Color(0f, 0.27f, 0.8f);

        }
        void Update()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.name == bg_button.name)
                    {
                        if (_isAttached)
                        {
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Change Background Color";
                            if (Input.GetMouseButtonDown(0) && bgcolors.Length > 0)
                            {
                                indexbg++;
                                indexbg %= bgcolors.Length;
                                bgmat.SetColor("_EmissionColor", bgcolors[indexbg]);
                                e_buttonbg.color = bgcolors[indexbg];
                                e_buttonbg.SetColor("_EmissionColor", bgcolors[indexbg]);
                                audioSource.PlayOneShot(button_push);
                            }
                            break;
                        }
                    }
                    if (hit.collider.name == t_button.name)
                    {
                        if (_isAttached)
                        {
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Change Text Color";
                            if (Input.GetMouseButtonDown(0) && textcolors.Length > 0)
                            {
                                indextx++;
                                indextx %= textcolors.Length;
                                textmat.color = textcolors[indextx];
                                textmat.SetColor("_EmissionColor", textcolors[indextx]);
                                e_buttontx.color = textcolors[indextx];
                                e_buttontx.SetColor("_EmissionColor", textcolors[indextx]);
                                audioSource.PlayOneShot(button_push);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}