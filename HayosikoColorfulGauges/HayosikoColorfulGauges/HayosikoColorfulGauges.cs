using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System;
using System.IO;

namespace HayosikoColorfulGauges
{
    public class HayosikoColorfulGauges : Mod
    {
        public override string ID => "HayosikoColorfulGauges"; //Your mod ID (unique)
        public override string Name => "HayosikoColorfulGauges"; //You mod name
        public override string Author => "Your Username"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject vanGauges;
        private PlayMakerFSM component1;
        private FsmInt fsmLight;
        private GameObject needleTemp;
        private GameObject needleSpeedo;
        private GameObject needleFuel;

        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gauges.unity3d");
            GameObject gameObject1 = ab.LoadAsset("vanGauges.prefab") as GameObject;
            GameObject gameObject2 = ab.LoadAsset("needle1.prefab") as GameObject;
            GameObject gameObject3 = ab.LoadAsset("needle1.prefab") as GameObject;
            GameObject gameObject4 = ab.LoadAsset("needle1.prefab") as GameObject;
            vanGauges = UnityEngine.Object.Instantiate(gameObject1);
            needleSpeedo = UnityEngine.Object.Instantiate(gameObject2);
            needleFuel = UnityEngine.Object.Instantiate(gameObject3);
            needleTemp = UnityEngine.Object.Instantiate(gameObject4);
            UnityEngine.Object.Destroy(gameObject1);
            UnityEngine.Object.Destroy(gameObject2);
            UnityEngine.Object.Destroy(gameObject3);
            UnityEngine.Object.Destroy(gameObject4);
            ab.Unload(false);
        }

        private void NewGameObjects()
        {
            //Gauges
            var pp = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges").gameObject;
            var gaug = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/GaugesMesh").gameObject;
            gaug.SetActive(false);
            vanGauges.transform.SetParent(pp.transform, false);
            vanGauges.transform.localPosition = new Vector3(-0.06374384f, -1.053064f, 0.4513461f);
            vanGauges.transform.localEulerAngles = new Vector3(-4.061388E-05f, 1.976469E-06f, 90.00008f);
            vanGauges.transform.localScale = new Vector3(1f, 1f, 1f);
            ObjectVars.vanGaugesMat = vanGauges.GetComponent<MeshRenderer>().material;
            ObjectVars.vanGaugesMat.DisableKeyword("_EMISSION");
            //Temp
            var n31 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleTemp/Needle/needle").gameObject;
            n31.GetComponent<MeshRenderer>().enabled = false;
            needleTemp.transform.SetParent(n31.transform, false);
            ObjectVars.needle1 = needleTemp.GetComponent<MeshRenderer>().material;
            //Speedo
            var n41 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleSpeedometer/Needle/needle").gameObject;
            n41.GetComponent<MeshRenderer>().enabled = false;
            needleSpeedo.transform.SetParent(n41.transform, false);
            ObjectVars.needle2 = needleSpeedo.GetComponent<MeshRenderer>().material;
            //Fuel
            var n71 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleFuel/Needle/needle").gameObject;
            n71.GetComponent<MeshRenderer>().enabled = false;
            needleFuel.transform.SetParent(n71.transform, false);
            ObjectVars.needle3 = needleFuel.GetComponent<MeshRenderer>().material;
            var pr = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/dashboard_glass").gameObject;
            pr.SetActive(true);
            component1 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD/Dashboard/Knobs/Lights").GetComponent<PlayMakerFSM>();
        }

        public override void OnLoad()
        {
            AssetBundleLoad();
            NewGameObjects();

        }

        public override void ModSettings()
        {
            Settings.AddHeader(this, "Hayosiko Gauges Settings", new Color32(101, 34, 18, byte.MaxValue), new Color32(254, 254, 0, byte.MaxValue));
            Settings.AddHeader(this, "Background Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.BGcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorB, 0f, 2f);
            Settings.AddHeader(this, "Needle Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.NcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorB, 0f, 2f);
        }

        public static void BackgroundColors()
        {
            if (ModLoader.GetCurrentScene() == CurrentScene.Game)
            {
                float bgcolorR = float.Parse(SettingsVars.BGcolorR.GetValue().ToString());
                float bgcolorG = float.Parse(SettingsVars.BGcolorG.GetValue().ToString());
                float bgcolorB = float.Parse(SettingsVars.BGcolorB.GetValue().ToString());
                Color EmissiveGreen = new Color(bgcolorR, bgcolorG, bgcolorB);
                if (ObjectVars.LightValue == 1 || ObjectVars.LightValue == 2)
                {
                    ObjectVars.vanGaugesMat.EnableKeyword("_EMISSION");
                    ObjectVars.vanGaugesMat.SetColor("_EmissionColor", EmissiveGreen);
                }
                else
                {
                    ObjectVars.vanGaugesMat.DisableKeyword("_EMISSION");
                }
            }
        }

        public static void NeedleColors()
        {
            if (ModLoader.GetCurrentScene() == CurrentScene.Game)
            {
                float ncolorR = float.Parse(SettingsVars.NcolorR.GetValue().ToString());
                float ncolorG = float.Parse(SettingsVars.NcolorG.GetValue().ToString());
                float ncolorB = float.Parse(SettingsVars.NcolorB.GetValue().ToString());
                Color EmissiveRed = new Color(ncolorR, ncolorG, ncolorB);
                if (ObjectVars.LightValue == 1 || ObjectVars.LightValue == 2)
                {
                    ObjectVars.needle1.EnableKeyword("_EMISSION");
                    ObjectVars.needle2.EnableKeyword("_EMISSION");
                    ObjectVars.needle3.EnableKeyword("_EMISSION");
                    ObjectVars.needle1.SetColor("_EmissionColor", EmissiveRed);
                    ObjectVars.needle2.SetColor("_EmissionColor", EmissiveRed);
                    ObjectVars.needle3.SetColor("_EmissionColor", EmissiveRed);
                }
                else
                {
                    ObjectVars.needle1.DisableKeyword("_EMISSION");
                    ObjectVars.needle2.DisableKeyword("_EMISSION");
                    ObjectVars.needle3.DisableKeyword("_EMISSION");
                }
            }
        }
        public override void Update()
        {
           
            fsmLight = component1.FsmVariables.GetFsmInt("Selection");
            ObjectVars.LightValue = fsmLight.Value;
            BackgroundColors();
            NeedleColors();
        }
    }
}
