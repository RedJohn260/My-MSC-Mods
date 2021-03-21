using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HutongGames.PlayMaker;
using MSCLoader;

namespace RJMicrowave
{
    public class BoxShopBehavior : MonoBehaviour
    {
        private class StoreBox : FsmStateAction
        {
            public Action action;
            public override void OnEnter()
            {
                action();
                Finish();
            }
        }

        public GameObject box1;
        private PlayMakerFSM register;
        private FsmBool gui_buy;
        private FsmString gui_interact;
        private StoreBox storeBox;
        private string buy_text = "Seemens 500W, 1199 mk";
        private float box_price = 1199f;
        private MeshRenderer boxMeshRenderer;
        private BoxCollider boxCollider;
        private Vector3 boxShelfPos = new Vector3(-1551.363f, 5.97678f, 1180.895f);
        private Vector3 boxShelfRot = new Vector3(5.485752E-05f, 237.5056f, -4.247359E-05f);
        private bool isOrdered = false;
        private bool mouseTrigger;
        public bool isPurchased;
        // Use this for initialization
        void Start()
        {
            try
            {
                //transform.SetParent(GameObject.Find("STORE").transform.Find("LOD/ActivateStore"));
                transform.position = boxShelfPos;
                transform.eulerAngles = boxShelfRot;
                boxCollider = GetComponents<BoxCollider>()[1];
                boxMeshRenderer = transform.FindChild("Box").GetComponent<MeshRenderer>();
                register = GameObject.Find("STORE/StoreCashRegister/Register").GetComponent<PlayMakerFSM>();
                gui_buy = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIbuy");
                gui_interact = PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction");
                FsmState boxFsmState = register.FsmStates.FirstOrDefault((FsmState state) => state.Name == "Purchase");
                storeBox = new StoreBox
                {
                    action = BoxPurchase
                };
                List<FsmStateAction> list = boxFsmState.Actions.ToList();
                list.Insert(0, storeBox);
                boxFsmState.Actions = list.ToArray();
            }
            catch(Exception e)
            {
                ModConsole.Error("RJMicrovawe: " + e.Message);
                Debug.LogError("RJMicrovawe: " + e.Message);
            }
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if (Camera.main != null && !isOrdered && boxCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _, 2f))
                {
                    mouseTrigger = true;
                    gui_buy.Value = true;
                    gui_interact.Value = buy_text;
                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseTrigger = false;
                        gui_buy.Value = false;
                        gui_interact.Value = "";
                        isOrdered = true;
                        boxMeshRenderer.enabled = false;
                        register.FsmVariables.GetFsmFloat("PriceTotal").Value += box_price;
                        register.SendEvent("PURCHASE");
                    }
                }
                else if (mouseTrigger)
                {
                    mouseTrigger = false;
                    gui_buy.Value = false;
                    gui_interact.Value = "";
                }
            }
            catch(Exception e)
            {
                ModConsole.Error("RJMicrovawe: " + e.Message);
                Debug.LogError("RJMicrovawe: " + e.Message);
            }
        }

        private void BoxPurchase()
        {
            try
            {
                if (isOrdered)
                {
                    isOrdered = false;
                    isPurchased = true;
                    box1.SetActive(true);
                    FsmState fsmState = register.FsmStates.FirstOrDefault((FsmState state) => state.Name == "Purchase");
                    List<FsmStateAction> list = fsmState.Actions.ToList();
                    list.Remove(storeBox);
                    fsmState.Actions = list.ToArray();
                    UnityEngine.Object.Destroy(gameObject);
                }
            }
            catch (Exception e)
            {
                ModConsole.Error("RJMicrovawe: " + e.Message);
                Debug.LogError("RJMicrovawe: " + e.Message);
            }
        }
    }
}