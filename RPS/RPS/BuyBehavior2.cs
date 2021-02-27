using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace RPS
{
    public class BuyBehavior2 : MonoBehaviour
    {
        public GameObject container;
        public GameObject buy_sign;
        public FsmFloat player_money;
        public float buy_price = 10000f;
        private AudioSource buy_sound;
        public bool IsBought;

        void Start()
        {
            container = this.gameObject.transform.FindChild("Container2").gameObject;
            buy_sign = this.gameObject.transform.FindChild("sign2").gameObject;
            player_money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
            buy_sound = transform.gameObject.GetComponent<AudioSource>();

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
                    if (hit.collider.name == "sign")
                    {
                        if (Input.GetMouseButtonDown(0) && player_money.Value > buy_price)
                        {
                            buy_sound.Play();
                            player_money.Value -= buy_price;
                            Object.Destroy(container);
                            Object.Destroy(buy_sign);
                            IsBought = true;
                        }
                        else if (Input.GetMouseButtonDown(0) && player_money.Value < buy_price)
                        {
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Not enough money";
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
