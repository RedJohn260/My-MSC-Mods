using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace JonnezRlight
{
    public class JonnezRlight : Mod
    {
        public override string ID => "JonnezRlight"; //Your mod ID (unique)
        public override string Name => "JonnezRlight"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.2"; //Version

        public override bool UseAssetsFolder => true;


        private AssetBundle ab;
        private GameObject JonnezRearLight;
        private GameObject LightOff;
        private GameObject LightOn;
        private GameObject LightBrakeOn;
        private Light SpotLight;
        private GameObject JONNEZ;
        private PlayMakerFSM LightPlayMaker;
        private PlayMakerFSM RpmPlayMaker;
        private FsmBool IsLightOn;
        private FsmBool IsEngineOn;
        private FsmFloat CurrentRpm;
        private AxisCarController JonnezAxisController;


        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "rlight.unity3d");
            GameObject gameObject = ab.LoadAsset("JonnezRLight.prefab") as GameObject;
            JonnezRearLight = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            JONNEZ = GameObject.Find("JONNEZ ES(Clone)");
            JonnezRearLight.transform.SetParent(JONNEZ.transform, false);
            JonnezRearLight.transform.localPosition = new Vector3(0f, 0.241f, -0.582f);
            LightOff = JonnezRearLight.transform.FindChild("light_off").gameObject;
            LightOn = JonnezRearLight.transform.FindChild("light_on").gameObject;
            LightOn.SetActive(false);
            LightBrakeOn = JonnezRearLight.transform.FindChild("light_brake").gameObject;
            LightBrakeOn.SetActive(false);
            SpotLight = JonnezRearLight.transform.FindChild("light_spot").gameObject.GetComponent<Light>();
            SpotLight.enabled = false;
            ab.Unload(false);
            LightPlayMaker = JONNEZ.transform.FindChild("LOD/Dashboard/Lights").GetComponent<PlayMakerFSM>();
            RpmPlayMaker = JONNEZ.transform.FindChild("Starter").GetComponent<PlayMakerFSM>();
            JonnezAxisController = JONNEZ.GetComponent<Drivetrain>().GetComponent<AxisCarController>();
            MeshRenderer meshRenderer = JonnezRearLight.transform.FindChild("plate").GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_MainTex", LoadAssets.LoadTexture(this, "plate_diff.jpg"));
            meshRenderer.material.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "plate_norm.jpg"));
            meshRenderer.material.EnableKeyword("_NORMALMAP");
        }

        private void DayLightOn()
        {
            IsLightOn = LightPlayMaker.FsmVariables.FindFsmBool("LightsOn").Value;
            CurrentRpm = RpmPlayMaker.FsmVariables.FindFsmFloat("RPM").Value;
            IsEngineOn = RpmPlayMaker.FsmVariables.FindFsmBool("Starting").Value;
            if (IsLightOn.Value == true && IsEngineOn.Value == true)
            {
                LightOff.SetActive(false);
                LightOn.SetActive(true);
                SpotLight.enabled = true;
               if (CurrentRpm.Value > 3000.603)
               {
                    SpotLight.intensity = CurrentRpm.Value / 1000;
               }
            }
            else
            {
                LightOff.SetActive(true);
                LightOn.SetActive(false);
                SpotLight.enabled = false;
            }
        }

        private void BrakeLightOn()
        {
            if (JonnezAxisController.brakeKey == true)
            {
                LightBrakeOn.SetActive(true);
                LightOff.SetActive(false);
                LightOn.SetActive(false);
                if (IsLightOn.Value == false)
                {
                    SpotLight.enabled = true;
                    SpotLight.intensity = 8.0f;
                }
                if (IsLightOn.Value == true)
                {
                    SpotLight.intensity = CurrentRpm.Value / 500;
                }
            }
            else
            {
                LightBrakeOn.SetActive(false);
                if (IsLightOn.Value == false)
                {
                    SpotLight.enabled = false;
                    LightOff.SetActive(true);
                    LightOn.SetActive(false);
                    SpotLight.intensity = CurrentRpm.Value / 1000;
                }
            }
        }

        public override void Update()
        {
            DayLightOn();
            BrakeLightOn();
        }
    }
}
