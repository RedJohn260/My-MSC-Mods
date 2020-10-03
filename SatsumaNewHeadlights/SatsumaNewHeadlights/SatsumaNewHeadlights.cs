using MSCLoader;
using UnityEngine;

namespace SatsumaNewHeadlights
{
    public class SatsumaNewHeadlights : Mod
    {
        public override string ID => "SatsumaNewHeadlights"; //Your mod ID (unique)
        public override string Name => "SatsumaNewHeadlights"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject Hcase;
        private GameObject Hglass;
        private GameObject HbulbCoverL;
        private GameObject HbulbCoverR;
        private GameObject headlightL;
        private GameObject headlightR;
        private GameObject bulb_trigerL;
        private GameObject bulb_trigerR;
        private GameObject NewHeadlights;

        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "headlights.unity3d");
            GameObject gameObject = ab.LoadAsset("NewHeadlights.prefab") as GameObject;
            NewHeadlights = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            Hcase = NewHeadlights.transform.FindChild("case").gameObject;
            Hglass = NewHeadlights.transform.FindChild("glass").gameObject;
            HbulbCoverL = NewHeadlights.transform.FindChild("bulb_coverL").gameObject;
            HbulbCoverR = NewHeadlights.transform.FindChild("bulb_coverR").gameObject;
            Object.Destroy(NewHeadlights);
            headlightL = GameObject.Find("headlight left(Clone)");
            headlightR = GameObject.Find("headlight right(Clone)");
            headlightL.GetComponent<MeshFilter>().mesh = Hcase.GetComponent<MeshFilter>().mesh;
            headlightL.GetComponent<MeshRenderer>().materials = Hcase.GetComponent<MeshRenderer>().materials;
            headlightR.GetComponent<MeshFilter>().mesh = Hcase.GetComponent<MeshFilter>().mesh;
            headlightR.GetComponent<MeshRenderer>().materials = Hcase.GetComponent<MeshRenderer>().materials;
            headlightL.GetComponent<MeshRenderer>().useLightProbes = true;
            headlightL.GetComponent<MeshRenderer>().reflectionProbeUsage = Hcase.GetComponent<MeshRenderer>().reflectionProbeUsage;
            headlightR.GetComponent<MeshFilter>().mesh = Hcase.GetComponent<MeshFilter>().mesh;
            headlightR.GetComponent<MeshRenderer>().materials = Hcase.GetComponent<MeshRenderer>().materials;
            headlightR.GetComponent<MeshRenderer>().useLightProbes = true;
            headlightR.GetComponent<MeshRenderer>().reflectionProbeUsage = Hcase.GetComponent<MeshRenderer>().reflectionProbeUsage;
            var hl_glass = headlightL.transform.FindChild("light_front_drive_l").gameObject;
            hl_glass.GetComponent<MeshFilter>().mesh = Hglass.GetComponent<MeshFilter>().mesh;
            hl_glass.GetComponent<MeshRenderer>().materials = Hglass.GetComponent<MeshRenderer>().materials;
            hl_glass.GetComponent<MeshRenderer>().useLightProbes = true;
            hl_glass.GetComponent<MeshRenderer>().reflectionProbeUsage = Hglass.GetComponent<MeshRenderer>().reflectionProbeUsage;
            hl_glass.transform.localPosition = new Vector3(-0.002f, 0.053f, 0.002f);
            hl_glass.transform.localEulerAngles = new Vector3(90f, -2.286024E-15f, 0f);
            var hr_glass = headlightR.transform.FindChild("light_front_drive_r").gameObject;
            hr_glass.GetComponent<MeshFilter>().mesh = Hglass.GetComponent<MeshFilter>().mesh;
            hr_glass.GetComponent<MeshRenderer>().materials = Hglass.GetComponent<MeshRenderer>().materials;
            hr_glass.GetComponent<MeshRenderer>().useLightProbes = true;
            hr_glass.GetComponent<MeshRenderer>().reflectionProbeUsage = Hglass.GetComponent<MeshRenderer>().reflectionProbeUsage;
            hr_glass.transform.localPosition = new Vector3(-0.002f, 0.05f, 0.002f);
            hr_glass.transform.localEulerAngles = new Vector3(90f, -2.286024E-15f, 0f);
            HbulbCoverL.transform.SetParent(headlightL.transform, false);
            HbulbCoverR.transform.SetParent(headlightR.transform, false);
            HbulbCoverL.SetActive(false);
            HbulbCoverR.SetActive(false);
            var b_pivotL = headlightL.transform.FindChild("pivot_bulb1").gameObject;
            b_pivotL.transform.localPosition = new Vector3(-0.0001003542f, -0.003399999f, 0.0003996312f);
            var b_pivotR = headlightR.transform.FindChild("pivot_bulb2").gameObject;
            b_pivotR.transform.localPosition = new Vector3(-0.0001003542f, -0.003399999f, 0.0003996312f);
            bulb_trigerL = headlightL.transform.FindChild("trigger_bulb1").gameObject;
            bulb_trigerR = headlightR.transform.FindChild("trigger_bulb2").gameObject;
            var shortL = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON/BeamsShort/BeamShortLeft/light_front_drive_l");
            var shortR = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON/BeamsShort/BeamShortRight/light_front_drive_r");
            var LongL = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON/BeamsLong/BeamLongLeft/light_front_drive_l");
            var LongR = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON/BeamsLong/BeamLongRight/light_front_drive_r");
            shortL.transform.localPosition = new Vector3(0.003f, -0.009000001f, -0.06419998f);
            shortR.transform.localPosition = new Vector3(0.003f, -0.009000001f, -0.06419998f);
            LongL.transform.localPosition = new Vector3(0.003f, -0.009000001f, -0.06419998f);
            LongR.transform.localPosition = new Vector3(0.003f, -0.009000001f, -0.06419998f);
            ab.Unload(false);
        }

        public override void Update()
        {
           if (bulb_trigerL.activeSelf == false)
           {
               HbulbCoverL.SetActive(true);
           }
           else
           {
               HbulbCoverL.SetActive(false);
           }
           if(bulb_trigerR.activeSelf == false)
           {
               HbulbCoverR.SetActive(true);
           }
           else
           {
               HbulbCoverR.SetActive(false);
           }
            
        }
    }
}
