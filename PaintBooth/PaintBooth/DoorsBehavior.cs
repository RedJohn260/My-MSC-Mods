using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
namespace PaintBooth
{
    public class DoorsBehavior : MonoBehaviour
    {

        public GameObject booth;

        private GameObject doorl;

        private GameObject doorr;

        private HingeJoint hingeJointDR;

        private HingeJoint hingeJointDL;

        private JointMotor JointMotorDL;

        private JointMotor JointMotorDR;

        private Ray ray3;

        private RaycastHit hit;

        private RaycastHit hit1;

        private GameObject player;

        private FsmBool GUIuse;

        private Rigidbody doorRigidL;

        private Rigidbody doorRigidR;

        private AudioSource door_audioL;

        private AudioSource door_audioR;

        private bool sound_isPlayingLO = false;

        private bool sound_isPlayingLC = false;

        private bool sound_isPlayingRO = false;

        private bool sound_isPlayingRC = false;

        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("PLAYER");
            GUIuse = PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse");
            booth = base.transform.parent.gameObject;
            doorl = booth.transform.FindChild("BoothDoorL").gameObject;
            doorRigidL = doorl.GetComponent<Rigidbody>();
            hingeJointDL = doorl.GetComponent<HingeJoint>();
            hingeJointDL.useMotor = true;

            doorr = booth.transform.FindChild("BoothDoorR").gameObject;
            doorRigidR = doorr.GetComponent<Rigidbody>();
            hingeJointDR = doorr.GetComponent<HingeJoint>();
            hingeJointDR.useMotor = true;

            door_audioL = doorl.GetComponent<AudioSource>();
            door_audioR = doorr.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray3, out hit) && hit.collider.name == "door_l_handle" || hit.collider.name == "door_l_handle_i")
            {
                if (Vector3.Distance(hit.collider.transform.position, player.transform.position) < 3.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!sound_isPlayingLO)
                        {
                            door_audioL.Play();
                            sound_isPlayingLO = true;
                            sound_isPlayingLC = false;
                        }
                        JointMotorDL.force = 50f;
                        JointMotorDL.targetVelocity = -40f;
                        doorRigidL.isKinematic = true;
                        doorRigidL.isKinematic = false;
                        hingeJointDL.motor = JointMotorDL;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {

                        if (!sound_isPlayingLC)
                        {
                            door_audioL.Play();
                            sound_isPlayingLC = true;
                            sound_isPlayingLO = false;
                        }
                        JointMotorDL.force = 50f;
                        JointMotorDL.targetVelocity = 40f;
                        doorRigidL.isKinematic = true;
                        doorRigidL.isKinematic = false;
                        hingeJointDL.motor = JointMotorDL;
                    }
                }
                else
                {
                    GUIuse.Value = false;
                   
                }
            }
            if (Physics.Raycast(ray3, out hit1) && hit.collider.name == "door_r_handle" || hit.collider.name == "door_r_handle_i")
            {
                if (Vector3.Distance(hit.collider.transform.position, player.transform.position) < 3.0f)
                {
                    GUIuse.Value = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!sound_isPlayingRO)
                        {
                            door_audioR.Play();
                            sound_isPlayingRO = true;
                            sound_isPlayingRC = false;
                        }
                        JointMotorDR.force = 50f;
                        JointMotorDR.targetVelocity = 40f;
                        doorRigidR.isKinematic = true;
                        doorRigidR.isKinematic = false;
                        hingeJointDR.motor = JointMotorDR;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (!sound_isPlayingRC)
                        {
                            door_audioR.Play();
                            sound_isPlayingRC = true;
                            sound_isPlayingRO = false;
                        }
                        JointMotorDR.force = 50f;
                        JointMotorDR.targetVelocity = -40f;
                        doorRigidR.isKinematic = true;
                        doorRigidR.isKinematic = false;
                        hingeJointDR.motor = JointMotorDR;
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