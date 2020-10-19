using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System;
using System.IO;

namespace GifuGauges
{
    public class GifuGauges : Mod
    {
        public override string ID => "GifuGauges"; //Your mod ID (unique)
        public override string Name => "GifuGauges  "; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.3"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject gifuGauges;
        private PlayMakerFSM component1;
        private FsmInt fsmLight;
        private PlayMakerFSM component2;
        private FsmBool fsmKey;
        private GameObject needleH;
        private GameObject needleM;
        private GameObject needleTemp;
        private GameObject needleSpeedo;
        private GameObject needleRPM;
        private GameObject needleOil;
        private GameObject needleFuel;
        private GameObject needleAir;
        private GameObject needleVolt;

        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gifu.unity3d");

            GameObject gameObject = ab.LoadAsset("truck_gauges") as GameObject;
            GameObject gameObject1 = ab.LoadAsset("gifu_needle_m") as GameObject;
            GameObject gameObject2 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject3 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject4 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject5 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject6 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject7 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject8 = ab.LoadAsset("gifu_needle_h") as GameObject;
            GameObject gameObject9 = ab.LoadAsset("gifu_needle_h") as GameObject;
            gifuGauges = UnityEngine.Object.Instantiate(gameObject);
            needleM = UnityEngine.Object.Instantiate(gameObject1);
            needleH = UnityEngine.Object.Instantiate(gameObject2);
            needleRPM = UnityEngine.Object.Instantiate(gameObject3);
            needleTemp = UnityEngine.Object.Instantiate(gameObject4);
            needleSpeedo = UnityEngine.Object.Instantiate(gameObject5);
            needleOil = UnityEngine.Object.Instantiate(gameObject6);
            needleFuel = UnityEngine.Object.Instantiate(gameObject7);
            needleAir = UnityEngine.Object.Instantiate(gameObject8);
            needleVolt = UnityEngine.Object.Instantiate(gameObject9);
            UnityEngine.Object.Destroy(gameObject);
            UnityEngine.Object.Destroy(gameObject1);
            UnityEngine.Object.Destroy(gameObject2);
            UnityEngine.Object.Destroy(gameObject3);
            UnityEngine.Object.Destroy(gameObject4);
            UnityEngine.Object.Destroy(gameObject5);
            UnityEngine.Object.Destroy(gameObject6);
            UnityEngine.Object.Destroy(gameObject7);
            UnityEngine.Object.Destroy(gameObject8);
            UnityEngine.Object.Destroy(gameObject9);
            ab.Unload(false);
        }
        private void NewGameObjects()
        {
            //Gauges
            var pp = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD").gameObject;
            var gaug = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/truck_dash_gauges").gameObject;
            gaug.SetActive(false);
            gifuGauges.transform.SetParent(pp.transform, false);
            gifuGauges.transform.localPosition = new Vector3(0f, 0.05799997f, 0f);
            gifuGauges.transform.localEulerAngles = new Vector3(270f, -4.829672E-06f, 0f);
            gifuGauges.transform.localScale = new Vector3(1f, 1f, 1f);
            ObjectVars.gifuGaugesMat = gifuGauges.GetComponent<MeshRenderer>().material;
            ObjectVars.gifuGaugesMat.DisableKeyword("_EMISSION");
            // Clock
            var n11 = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD/Dashboard/Gauges/Clock/hour/needle_hour").gameObject;
            n11.GetComponent<MeshRenderer>().enabled = false;
            needleH.transform.SetParent(n11.transform, false);
            ObjectVars.needle1 = needleH.GetComponent<MeshRenderer>().material;

            var n21 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Clock/minute/needle_minute").gameObject;
            n21.GetComponent<MeshRenderer>().enabled = false;
            needleM.transform.SetParent(n21.transform, false);
            ObjectVars.needle2 = needleM.GetComponent<MeshRenderer>().material;
            //Temp
            var n31 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Temp/needle 1").gameObject;
            n31.GetComponent<MeshRenderer>().enabled = false;
            needleTemp.transform.SetParent(n31.transform, false);
            ObjectVars.needle3 = needleTemp.GetComponent<MeshRenderer>().material;
            //Speedo
            var n41 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Speedometer/needle 1").gameObject;
            n41.GetComponent<MeshRenderer>().enabled = false;
            needleSpeedo.transform.SetParent(n41.transform, false);
            ObjectVars.needle4 = needleSpeedo.GetComponent<MeshRenderer>().material;
            //Rpm
            var n51 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Tachometer/needle 1").gameObject;
            n51.GetComponent<MeshRenderer>().enabled = false;
            needleRPM.transform.SetParent(n51.transform, false);
            needleRPM.name = "RpmNeedle";
            ObjectVars.needle5 = needleRPM.GetComponent<MeshRenderer>().material;
            //oil
            var n61 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Oil Pressure/needle 1").gameObject;
            n61.GetComponent<MeshRenderer>().enabled = false;
            needleOil.transform.SetParent(n61.transform, false);
            ObjectVars.needle6 = needleOil.GetComponent<MeshRenderer>().material;
            //Fuel
            var n71 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Fuel/needle").gameObject;
            n71.GetComponent<MeshRenderer>().enabled = false;
            needleFuel.transform.SetParent(n71.transform, false);
            ObjectVars.needle7 = needleFuel.GetComponent<MeshRenderer>().material;
            //Air
            var n81 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Air Pressure/needle 1").gameObject;
            n81.GetComponent<MeshRenderer>().enabled = false;
            needleAir.transform.SetParent(n81.transform, false);
            ObjectVars.needle8 = needleAir.GetComponent<MeshRenderer>().material;
            //Volt
            var n91 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Volts/needle 1").gameObject;
            n91.GetComponent<MeshRenderer>().enabled = false;
            needleVolt.transform.SetParent(n91.transform, false);
            ObjectVars.needle9 = needleVolt.GetComponent<MeshRenderer>().material;

            component1 = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD/Dashboard/ButtonLights").GetComponent<PlayMakerFSM>();
            component2 = GameObject.Find("GIFU(750/450psi)/Simulation/Starter").GetComponent<PlayMakerFSM>();
            ModConsole.Print("<b><color=green>Gifu Colorful Gauges Laoded</color></b>");


        }
        public override void OnLoad()
        {
            AssetBundleLoad();
            NewGameObjects();

        }

        public override void ModSettings()
        {
            Settings.AddHeader(this, "Gifu Gauges Settings", new Color32(101, 34, 18, byte.MaxValue), new Color32(254, 254, 0, byte.MaxValue));
            Settings.AddHeader(this, "Background Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.BGcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorB, 0f, 2f);
            Settings.AddHeader(this, "Needle Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.NcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorB, 0f, 2f);
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        public static void BackgroundColors()
        {
            if (ModLoader.GetCurrentScene() == CurrentScene.Game)
            {
                float bgcolorR = float.Parse(SettingsVars.BGcolorR.GetValue().ToString());
                float bgcolorG = float.Parse(SettingsVars.BGcolorG.GetValue().ToString());
                float bgcolorB = float.Parse(SettingsVars.BGcolorB.GetValue().ToString());
                Color EmissiveGreen = new Color(bgcolorR, bgcolorG, bgcolorB);
                if (ObjectVars.KeyValue == true)
                {
                    if (ObjectVars.LightValue == 1 || ObjectVars.LightValue == 2)
                    {
                        ObjectVars.gifuGaugesMat.EnableKeyword("_EMISSION");
                        ObjectVars.gifuGaugesMat.SetColor("_EmissionColor", EmissiveGreen);
                    }
                    else if (ObjectVars.LightValue == 0)
                    {
                        ObjectVars.gifuGaugesMat.DisableKeyword("_EMISSION");
                    }
                }
                else
                {
                    ObjectVars.gifuGaugesMat.DisableKeyword("_EMISSION");
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
                if (ObjectVars.KeyValue == true)
                {
                    if (ObjectVars.LightValue == 1 || ObjectVars.LightValue == 2)
                    {
                        ObjectVars.needle1.EnableKeyword("_EMISSION");
                        ObjectVars.needle2.EnableKeyword("_EMISSION");
                        ObjectVars.needle3.EnableKeyword("_EMISSION");
                        ObjectVars.needle4.EnableKeyword("_EMISSION");
                        ObjectVars.needle5.EnableKeyword("_EMISSION");
                        ObjectVars.needle6.EnableKeyword("_EMISSION");
                        ObjectVars.needle7.EnableKeyword("_EMISSION");
                        ObjectVars.needle8.EnableKeyword("_EMISSION");
                        ObjectVars.needle9.EnableKeyword("_EMISSION");
                        ObjectVars.needle1.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle2.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle3.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle4.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle5.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle6.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle7.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle8.SetColor("_EmissionColor", EmissiveRed);
                        ObjectVars.needle9.SetColor("_EmissionColor", EmissiveRed);
                    }
                    else if (ObjectVars.LightValue == 0)
                    {
                        ObjectVars.needle1.DisableKeyword("_EMISSION");
                        ObjectVars.needle2.DisableKeyword("_EMISSION");
                        ObjectVars.needle3.DisableKeyword("_EMISSION");
                        ObjectVars.needle4.DisableKeyword("_EMISSION");
                        ObjectVars.needle5.DisableKeyword("_EMISSION");
                        ObjectVars.needle6.DisableKeyword("_EMISSION");
                        ObjectVars.needle7.DisableKeyword("_EMISSION");
                        ObjectVars.needle8.DisableKeyword("_EMISSION");
                        ObjectVars.needle9.DisableKeyword("_EMISSION");
                    }
                }
                else
                {
                    ObjectVars.needle1.DisableKeyword("_EMISSION");
                    ObjectVars.needle2.DisableKeyword("_EMISSION");
                    ObjectVars.needle3.DisableKeyword("_EMISSION");
                    ObjectVars.needle4.DisableKeyword("_EMISSION");
                    ObjectVars.needle5.DisableKeyword("_EMISSION");
                    ObjectVars.needle6.DisableKeyword("_EMISSION");
                    ObjectVars.needle7.DisableKeyword("_EMISSION");
                    ObjectVars.needle8.DisableKeyword("_EMISSION");
                    ObjectVars.needle9.DisableKeyword("_EMISSION");
                }
            }
        }
        public override void Update()
        {
            fsmLight = component1.FsmVariables.GetFsmInt("Selection");
            ObjectVars.LightValue = fsmLight.Value;
            fsmKey = component2.FsmVariables.GetFsmBool("ACC");
            ObjectVars.KeyValue = fsmKey.Value;
            BackgroundColors();
            NeedleColors();
        }
    }
}
