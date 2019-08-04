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
        public override string Name => "GifuGauges"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject gifuGauges;
        private Material gifuGaugesMat;
        private Color EmissiveGreen;
        private Color EmissiveRed;
        public float rslider = 0f;
        public float gslider = 1f;
        public float bslider = 0f;
        public float rnslider = 1f;
        public float gnslider = 0f;
        public float bnslider = 0f;
        public Rect guiBox1 = new Rect((float)(Screen.width - 2500 / 2), 70f, 600f, 300f);
        public Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.F5);
        private string path;
        private bool GUI1;
        private PlayMakerFSM component1;
        private FsmInt fsmLight;
        private int LightValue;
        private PlayMakerFSM component2;
        private FsmBool fsmKey;
        private bool KeyValue;
        private GameObject needleH;
        private GameObject needleM;
        private GameObject needleTemp;
        private GameObject needleSpeedo;
        private GameObject needleRPM;
        private GameObject needleOil;
        private GameObject needleFuel;
        private GameObject needleAir;
        private GameObject needleVolt;
        private Material needle1;
        private Material needle2;
        private Material needle3;
        private Material needle4;
        private Material needle5;
        private Material needle6;
        private Material needle7;
        private Material needle8;
        private Material needle9;

        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gifu.unity3d");
            gifuGauges = GameObject.Instantiate(ab.LoadAsset("truck_gauges.prefab")) as GameObject;
            needleH = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleM = GameObject.Instantiate(ab.LoadAsset("gifu_needle_m.prefab")) as GameObject;
            needleRPM = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleTemp = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleSpeedo = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleOil = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleFuel = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleAir = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
            needleVolt = GameObject.Instantiate(ab.LoadAsset("gifu_needle_h.prefab")) as GameObject;
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
            gifuGaugesMat = gifuGauges.GetComponent<MeshRenderer>().material;
            gifuGaugesMat.DisableKeyword("_EMISSION");
            // Clock
            var n11 = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD/Dashboard/Gauges/Clock/hour/needle_hour").gameObject;
            n11.GetComponent<MeshRenderer>().enabled = false;
            needleH.transform.SetParent(n11.transform, false);
            needle1 = needleH.GetComponent<MeshRenderer>().material;

            var n21 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Clock/minute/needle_minute").gameObject;
            n21.GetComponent<MeshRenderer>().enabled = false;
            needleM.transform.SetParent(n21.transform, false);
            needle2 = needleM.GetComponent<MeshRenderer>().material;
            //Temp
            var n31 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Temp/needle 1").gameObject;
            n31.GetComponent<MeshRenderer>().enabled = false;
            needleTemp.transform.SetParent(n31.transform, false);
            needle3 = needleTemp.GetComponent<MeshRenderer>().material;
            //Speedo
            var n41 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Speedometer/needle 1").gameObject;
            n41.GetComponent<MeshRenderer>().enabled = false;
            needleSpeedo.transform.SetParent(n41.transform, false);
            needle4 = needleSpeedo.GetComponent<MeshRenderer>().material;
            //Rpm
            var n51 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Tachometer/needle 1").gameObject;
            n51.GetComponent<MeshRenderer>().enabled = false;
            needleRPM.transform.SetParent(n51.transform, false);
            needleRPM.name = "RpmNeedle";
            needle5 = needleRPM.GetComponent<MeshRenderer>().material;
            //oil
            var n61 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Oil Pressure/needle 1").gameObject;
            n61.GetComponent<MeshRenderer>().enabled = false;
            needleOil.transform.SetParent(n61.transform, false);
            needle6 = needleOil.GetComponent<MeshRenderer>().material;
            //Fuel
            var n71 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Fuel/needle").gameObject;
            n71.GetComponent<MeshRenderer>().enabled = false;
            needleFuel.transform.SetParent(n71.transform, false);
            needle7 = needleFuel.GetComponent<MeshRenderer>().material;
            //Air
            var n81 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Air Pressure/needle 1").gameObject;
            n81.GetComponent<MeshRenderer>().enabled = false;
            needleAir.transform.SetParent(n81.transform, false);
            needle8 = needleAir.GetComponent<MeshRenderer>().material;
            //Volt
            var n91 = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Volts/needle 1").gameObject;
            n91.GetComponent<MeshRenderer>().enabled = false;
            needleVolt.transform.SetParent(n91.transform, false);
            needle9 = needleVolt.GetComponent<MeshRenderer>().material;

            needle1.DisableKeyword("_EMISSION");
            needle2.DisableKeyword("_EMISSION");
            needle3.DisableKeyword("_EMISSION");
            needle4.DisableKeyword("_EMISSION");
            needle5.DisableKeyword("_EMISSION");
            needle6.DisableKeyword("_EMISSION");
            needle7.DisableKeyword("_EMISSION");
            needle8.DisableKeyword("_EMISSION");
            needle9.DisableKeyword("_EMISSION");
            ModConsole.Print("<b><color=green>Gifu Colorful Gauges Laoded</color></b>");


        }
        public override void OnLoad()
        {
            AssetBundleLoad();
            NewGameObjects();
            
            Keybind.Add(this, this.openGUI);
            this.path = ModLoader.GetModAssetsFolder(this);
            Loadsettings();

           

        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        public override void OnGUI()
        {
            if (this.GUI1)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, this.guiBox1, new GUI.WindowFunction(this.Window1), "Gifu Gauges");
            }
        }

        public override void Update()
        {
            if (this.openGUI.IsDown())
            {
                this.GuiShow();
            }

            component1 = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD/Dashboard/ButtonLights").GetComponent<PlayMakerFSM>();
            fsmLight = component1.FsmVariables.GetFsmInt("Selection");
            LightValue = fsmLight.Value;
            component2 = GameObject.Find("GIFU(750/450psi)/Simulation/Starter").GetComponent<PlayMakerFSM>();
            fsmKey = component2.FsmVariables.GetFsmBool("ACC");
            KeyValue = fsmKey.Value;


            EmissiveGreen = new Color(rslider, gslider, bslider);
            EmissiveRed = new Color(rnslider, gnslider, bnslider);

           if (KeyValue == true)
            {
                if (LightValue == 1 || LightValue == 2)
                {

                    gifuGaugesMat.EnableKeyword("_EMISSION");
                    needle1.EnableKeyword("_EMISSION");
                    needle2.EnableKeyword("_EMISSION");
                    needle3.EnableKeyword("_EMISSION");
                    needle4.EnableKeyword("_EMISSION");
                    needle5.EnableKeyword("_EMISSION");
                    needle6.EnableKeyword("_EMISSION");
                    needle7.EnableKeyword("_EMISSION");
                    needle8.EnableKeyword("_EMISSION");
                    needle9.EnableKeyword("_EMISSION");



                    gifuGaugesMat.SetColor("_EmissionColor", EmissiveGreen);

                    needle1.SetColor("_EmissionColor", EmissiveRed);
                    needle2.SetColor("_EmissionColor", EmissiveRed);
                    needle3.SetColor("_EmissionColor", EmissiveRed);
                    needle4.SetColor("_EmissionColor", EmissiveRed);
                    needle5.SetColor("_EmissionColor", EmissiveRed);
                    needle6.SetColor("_EmissionColor", EmissiveRed);
                    needle7.SetColor("_EmissionColor", EmissiveRed);
                    needle8.SetColor("_EmissionColor", EmissiveRed);
                    needle9.SetColor("_EmissionColor", EmissiveRed);

                }
                else if (LightValue == 0)
                {
                    gifuGaugesMat.DisableKeyword("_EMISSION");
                    needle1.DisableKeyword("_EMISSION");
                    needle2.DisableKeyword("_EMISSION");
                    needle3.DisableKeyword("_EMISSION");
                    needle4.DisableKeyword("_EMISSION");
                    needle5.DisableKeyword("_EMISSION");
                    needle6.DisableKeyword("_EMISSION");
                    needle7.DisableKeyword("_EMISSION");
                    needle8.DisableKeyword("_EMISSION");
                    needle9.DisableKeyword("_EMISSION");

                }
            }
            else
            {
                gifuGaugesMat.DisableKeyword("_EMISSION");
                needle1.DisableKeyword("_EMISSION");
                needle2.DisableKeyword("_EMISSION");
                needle3.DisableKeyword("_EMISSION");
                needle4.DisableKeyword("_EMISSION");
                needle5.DisableKeyword("_EMISSION");
                needle6.DisableKeyword("_EMISSION");
                needle7.DisableKeyword("_EMISSION");
                needle8.DisableKeyword("_EMISSION");
                needle9.DisableKeyword("_EMISSION");
            }
        }
        public void GuiShow()
        {
            this.GUI1 = !GUI1;
            if (this.GUI1)
            {
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = true;
                return;
            }
            else
            {
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }
            FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
        }
        public void Window1(int windowid)
        {
            //Backround
            //-----------------------------------------------------------------------------------
            // R With Slider
            GUI.Label(new Rect(75f, 30f, 150f, 20f), "Backround Color");
            GUI.Label(new Rect(10f, 50f, 10f, 20f), "R");
            rslider = GUI.HorizontalSlider(new Rect(30f, 55f, 220f, 20f), rslider, 0, 1);
            //G With Slider
            GUI.Label(new Rect(10f, 80f, 10f, 20f), "G");
            gslider = GUI.HorizontalSlider(new Rect(30f, 85f, 220f, 20f), gslider, 0, 1);
            //B With Slider
            GUI.Label(new Rect(10f, 110f, 10f, 20f), "B");
            bslider = GUI.HorizontalSlider(new Rect(30f, 115f, 220f, 20f), bslider, 0, 1);

            //-----------------------------------------------------------------------------------

            //Needles
            //------------------------------------------------------------------------------------
            // R With Slider
            GUI.Label(new Rect(375f, 30f, 150f, 20f), "Needle Color");
            GUI.Label(new Rect(310f, 50f, 10f, 20f), "R");
            rnslider = GUI.HorizontalSlider(new Rect(330f, 55f, 250f, 20f), rnslider, 0, 1);
            //G With Slider
            GUI.Label(new Rect(310f, 80f, 10f, 20f), "G");
            gnslider = GUI.HorizontalSlider(new Rect(330f, 85f, 250f, 20f), gnslider, 0, 1);
            //B With Slider
            GUI.Label(new Rect(310f, 110f, 10f, 20f), "B");
            bnslider = GUI.HorizontalSlider(new Rect(330f, 115f, 250f, 20f), bnslider, 0, 1);



            //Save Button
            if (GUI.Button(new Rect(55f, 140f, 150f, 40f), "Save Settings"))
            {
                this.Savesettings();
                ModConsole.Print("Gauge Settings Saved");
            }
            //Load Button
            if (GUI.Button(new Rect(355f, 140f, 150f, 40f), "Load Settings"))
            {
                this.Loadsettings();
                ModConsole.Print("Gauge Settings Loaded");
            }
            // Gauges

            if (GUI.Button(new Rect(205f, 250f, 150f, 40f), "Close"))
            {
                GUI1 = false;
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }


            GUI.DragWindow();
        }
        private void Loadsettings()
        {
            string[] strArrays = new string[6];

            strArrays = File.ReadAllLines(string.Concat(this.path, "/Gauges.txt"));
            this.rslider = float.Parse(strArrays[0]);
            this.gslider = float.Parse(strArrays[1]);
            this.bslider = float.Parse(strArrays[2]);
            this.rnslider = float.Parse(strArrays[3]);
            this.gnslider = float.Parse(strArrays[4]);
            this.bnslider = float.Parse(strArrays[5]);
        }
        private void Savesettings()
        {
            string[] str = new string[6];
            int num = 0;
            float StoreRedB = this.rslider;
            str[num] = StoreRedB.ToString();
            int num1 = 1;
            float StoreGreenB = this.gslider;
            str[num1] = StoreGreenB.ToString();
            int num2 = 2;
            float StoreBlueB = this.bslider;
            str[num2] = StoreBlueB.ToString();
            int num3 = 3;
            float StoreRedN = this.rnslider;
            str[num3] = StoreRedN.ToString();
            int num4 = 4;
            float StoreGreenN = this.gnslider;
            str[num4] = StoreGreenN.ToString();
            int num5 = 5;
            float StoreBlueN = this.bnslider;
            str[num5] = StoreBlueN.ToString();
            File.WriteAllLines(string.Concat(this.path, "/Gauges.txt"), str);
        }

    }
}
