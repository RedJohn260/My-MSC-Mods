using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace RPS
{
    public class BuyBehavior : MonoBehaviour
    {
        public GameObject container;
        public GameObject buy_sign;
        public FsmFloat player_money;
        public float buy_price = 10000f;
        private AudioSource buy_sound;
        private AudioSource error_sound;
        public string sign_collider_name = "sign";
        public bool IsBought;

        // Use this for initialization
        void Start()
        {
            //container = gameObject.transform.FindChild("Container").gameObject;
            //buy_sign = gameObject.transform.FindChild("sign").gameObject;
            player_money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
            buy_sound = transform.gameObject.GetComponents<AudioSource>()[0];
            error_sound = transform.gameObject.GetComponents<AudioSource>()[1];

        }

        // Update is called once per frame
        void Update()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.name == sign_collider_name)
                    {
                        if (Input.GetMouseButtonDown(0) && player_money.Value >= buy_price)
                        {
                            buy_sound.Play();
                            player_money.Value -= buy_price;
                            Object.Destroy(container);
                            Object.Destroy(buy_sign);
                            IsBought = true;
                        }
                        else if (Input.GetMouseButtonDown(0) && player_money.Value < buy_price)
                        {
                            error_sound.Play();
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "buy for " + buy_price + " mk";
                        break;
                    }
                }
            }
        }
    }
}