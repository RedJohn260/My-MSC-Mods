using UnityEngine;
using System.Collections;

namespace M67Granade
{
	public class the_dude_ragdoll : MonoBehaviour
	{

		private GameObject the_dude_obj;

		private GameObject the_dude_ragdoll_obj;

		public GameObject PLAYER;

		private Vector3 charTransform;

		private Vector3 playerTransform;

		private float distance;

		private float minDistance = 2f;

		private float maxDistance = 500f;

		private GameObject rag_skeleton;

		private Vector3 HipsPos;
		private Quaternion HipsRot;
		private Vector3 LeftLegPos;
		private Quaternion LeftLegRot;
		private Vector3 LeftKneePos;
		private Quaternion LeftKneeRot;
		private Vector3 RightLegPos;
		private Quaternion RightLegRot;
		private Vector3 RightKneePos;
		private Quaternion RightKneeRot;
		private Vector3 SpinePos;
		private Quaternion SpineRot;
		private Vector3 LeftArmPos;
		private Quaternion LeftArmRot;
		private Vector3 LeftElbowPos;
		private Quaternion LeftElbowRot;
		private Vector3 RightArmPos;
		private Quaternion RightArmRot;
		private Vector3 RightElbowPos;
		private Quaternion RightElbowRot;
		private Vector3 HeadPos;
		private Quaternion HeadRot;
		public AmmoCrate ammoCrate;
		private AudioSource ouch;
		void Start()
		{
			the_dude_obj = gameObject.transform.FindChild("the_dude").gameObject;
			the_dude_ragdoll_obj = gameObject.transform.FindChild("the_dude_ragdoll").gameObject;
			ouch = gameObject.transform.GetComponent<AudioSource>();
			GetRagBonesTransform();
		}

		public bool isRagdollActive = false;
		public bool isCharActive = false;
		void Update()
		{
			charTransform = gameObject.transform.position;
			playerTransform = PLAYER.transform.position;
			distance = Vector3.Distance(playerTransform, charTransform);
			if (distance <= minDistance && Input.GetKeyDown(KeyCode.H) && !isRagdollActive)
			{
				the_dude_obj.SetActive(false);
				the_dude_ragdoll_obj.SetActive(true);
				isRagdollActive = true;
				isCharActive = false;
				ouch.Play();
			}
			if (distance >= maxDistance && !isCharActive)
			{
				the_dude_obj.SetActive(true);
				the_dude_ragdoll_obj.SetActive(false);
				isCharActive = true;
				isRagdollActive = false;
				SetRagBonesTranform();
			}
			if (the_dude_ragdoll_obj.activeInHierarchy)
			{
				ammoCrate.ragdollActive = true;
			}
			else
			{
				ammoCrate.ragdollActive = false;
			}
		}

		private void GetRagBonesTransform()
		{
			rag_skeleton = the_dude_ragdoll_obj.transform.FindChild("mixamorig:Hips").gameObject;
			HipsPos = rag_skeleton.transform.position;
			HipsRot = rag_skeleton.transform.rotation;
			LeftLegPos = rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.position;
			LeftLegRot = rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.rotation;
			LeftKneePos = rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.FindChild("mixamorig:LeftLeg").transform.position;
			LeftKneeRot = rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.FindChild("mixamorig:LeftLeg").transform.rotation;
			RightLegPos = rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.position;
			RightLegRot = rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.rotation;
			RightKneePos = rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.FindChild("mixamorig:RightLeg").transform.position;
			RightKneeRot = rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.FindChild("mixamorig:RightLeg").transform.rotation;
			LeftArmPos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.position;
			LeftArmRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.rotation;
			LeftElbowPos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.FindChild("mixamorig:LeftForeArm").transform.position;
			LeftElbowRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.FindChild("mixamorig:LeftForeArm").transform.rotation;
			RightArmPos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.position;
			RightArmRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.rotation;
			RightElbowPos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.FindChild("mixamorig:RightForeArm").transform.position;
			RightElbowRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.FindChild("mixamorig:RightForeArm").transform.rotation;
			HeadPos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:Neck").transform.FindChild("mixamorig:Head").transform.position;
			HeadRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:Neck").transform.FindChild("mixamorig:Head").transform.rotation;
			SpinePos = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.position;
			SpineRot = rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.rotation;
		}

		private void SetRagBonesTranform()
		{
			rag_skeleton.transform.position = HipsPos;
			rag_skeleton.transform.rotation = HipsRot;
			rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.position = LeftLegPos;
			rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.rotation = LeftLegRot;
			rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.FindChild("mixamorig:LeftLeg").transform.position = LeftKneePos;
			rag_skeleton.transform.FindChild("mixamorig:LeftUpLeg").transform.FindChild("mixamorig:LeftLeg").transform.rotation = LeftKneeRot;
			rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.position = RightLegPos;
			rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.rotation = RightLegRot;
			rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.FindChild("mixamorig:RightLeg").transform.position = RightKneePos;
			rag_skeleton.transform.FindChild("mixamorig:RightUpLeg").transform.FindChild("mixamorig:RightLeg").transform.rotation = RightKneeRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.position = LeftArmPos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.rotation = LeftArmRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.FindChild("mixamorig:LeftForeArm").transform.position = LeftElbowPos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:LeftShoulder").transform.FindChild("mixamorig:LeftArm").transform.FindChild("mixamorig:LeftForeArm").transform.rotation = LeftElbowRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.position = RightArmPos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.rotation = RightArmRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.FindChild("mixamorig:RightForeArm").transform.position = RightElbowPos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:RightShoulder").transform.FindChild("mixamorig:RightArm").transform.FindChild("mixamorig:RightForeArm").transform.rotation = RightElbowRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:Neck").transform.FindChild("mixamorig:Head").transform.position = HeadPos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.FindChild("mixamorig:Spine2").transform.FindChild("mixamorig:Neck").transform.FindChild("mixamorig:Head").transform.rotation = HeadRot;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.position = SpinePos;
			rag_skeleton.transform.FindChild("mixamorig:Spine").transform.FindChild("mixamorig:Spine1").transform.rotation = SpineRot;
		}
	}
}
