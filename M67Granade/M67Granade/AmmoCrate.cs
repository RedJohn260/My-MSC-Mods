using System.Collections.Generic;
using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

namespace M67Granade
{
    public class AmmoCrate : MonoBehaviour
    {
        public Collider crateCollider;

        public Vector3 cratePos;

        public Vector3 granadeSpawnPos;

        private Quaternion rot;

        public GameObject granade;

        public GameObject exploEffect;

        private Granade granade_script;

        private int gname = 0;

        private string _granadeName = "m67 granade (Clone)";

        private GameObject newg;

        private FsmFloat money;

        public float ammountTaken = 100f;

        public the_dude_ragdoll ragdoll;

        // Use this for initialization
        void Start()
        {
            crateCollider = gameObject.transform.FindChild("trigger").GetComponent<Collider>();
            rot = new Quaternion();
            granade.transform.SetParent(gameObject.transform, false);
            granade.transform.localPosition = new Vector3(0f, -50000f, 0f);
            UnityEngine.Object.Destroy(granade.GetComponent<Rigidbody>());
            granade_script = granade.AddComponent<Granade>();
            granade_script.explosionEffect = exploEffect;
        }

        public bool ragdollActive;
        void Update()
        {
            money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
            cratePos = gameObject.transform.GetComponent<Transform>().localPosition;
            granadeSpawnPos = new Vector3(cratePos.x, cratePos.y + 0.2f, cratePos.z);
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
                    if (hit.collider.name == crateCollider.name)
                    {
                        if (!ragdollActive)
                        {
                            if (Input.GetKeyDown(KeyCode.F) && money.Value > 99)
                            {
                                Spawngranade();
                                money.Value -= ammountTaken;
                            }
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Buy Granade " + ammountTaken + "MK";
                            break;
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.F))
                            {
                                Spawngranade();
                                ammountTaken += 100;
                            }
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Steal Granade";
                            break;
                        }
                    }
                }
            }
        }

        public void Spawngranade()
        {
            newg = UnityEngine.Object.Instantiate(granade, granadeSpawnPos, rot) as GameObject;
            newg.transform.parent = null;
            newg.name = _granadeName + " " + gname++;
            newg.layer = LayerMask.NameToLayer("Parts");
            newg.tag = "PART";
            CapsuleCollider col = newg.AddComponent<CapsuleCollider>();
            col.direction = (int)transform.position.z;
            col.radius = 0.05299032f;
            col.height = 0.11f;
            col.center = new Vector3(5.097687e-05f, -0.0006789676f, 0.016f);
            Rigidbody rigidbody = newg.AddComponent<Rigidbody>();
            rigidbody.drag = 0.5f;
            rigidbody.angularDrag = 0.5f;
            rigidbody.mass = 0.3f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}