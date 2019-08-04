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
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject vanGauges;
        private Material vanGaugesMat;
        private Color EmissiveGreen;
        private Color EmissiveRed;
        public float rslider = 0f;
        public float gslider = 1f;
        public float bslider = 0f;
        public float rnslider = 1f;
        public float gnslider = 0f;
        public float bnslider = 0f;
        public Rect guiBox1 = new Rect((float)(Screen.width - 2500 / 2), 70f, 600f, 300f);
        public Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.F6);
        private string path;
        private bool GUI1;
        private PlayMakerFSM component1;
        private FsmInt fsmLight;
        private int LightValue;
        private GameObject needleTemp;
        private GameObject needleSpeedo;
        private GameObject needleFuel;
        private Material needle1;
        private Material needle2;
        private Material needle3;


        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gauges.unity3d");
            vanGauges = GameObject.Instantiate(ab.LoadAsset("vanGauges.prefab")) as GameObject;
            needleSpeedo = GameObject.Instantiate(ab.LoadAsset("needle1.prefab")) as GameObject;
            needleFuel = GameObject.Instantiate(ab.LoadAsset("needle1.prefab")) as GameObject;
            needleTemp = GameObject.Instantiate(ab.LoadAsset("needle1.prefab")) as GameObject;
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
            vanGaugesMat = vanGauges.GetComponent<MeshRenderer>().material;
            vanGaugesMat.DisableKeyword("_EMISSION");

            //Temp
            var n31 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleTemp/Needle/needle").gameObject;
            n31.GetComponent<MeshRenderer>().enabled = false;
            needleTemp.transform.SetParent(n31.transform, false);
            needle1 = needleTemp.GetComponent<MeshRenderer>().material;
            //Speedo
            var n41 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleSpeedometer/Needle/needle").gameObject;
            n41.GetComponent<MeshRenderer>().enabled = false;
            needleSpeedo.transform.SetParent(n41.transform, false);
            needle2 = needleSpeedo.GetComponent<MeshRenderer>().material;
            //Fuel
            var n71 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Gauges/NeedleFuel/Needle/needle").gameObject;
            n71.GetComponent<MeshRenderer>().enabled = false;
            needleFuel.transform.SetParent(n71.transform, false);
            needle3 = needleFuel.GetComponent<MeshRenderer>().material;

            needle1.DisableKeyword("_EMISSION");
            needle2.DisableKeyword("_EMISSION");
            needle3.DisableKeyword("_EMISSION");



            var pr = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/dashboard_glass").gameObject;
            pr.SetActive(true);


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
                GUI.ModalWindow(1, this.guiBox1, new GUI.WindowFunction(this.Window1), "Hayosiko Gauges");
            }
        }

        public override void Update()
        {
            if (this.openGUI.IsDown())
            {
                this.GuiShow();
            }
            component1 = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD/Dashboard/Knobs/Lights").GetComponent<PlayMakerFSM>();
            fsmLight = component1.FsmVariables.GetFsmInt("Selection");
            LightValue = fsmLight.Value;

            EmissiveGreen = new Color(rslider, gslider, bslider);
            EmissiveRed = new Color(rnslider, gnslider, bnslider);

            if (LightValue == 1 || LightValue == 2)
            {

                vanGaugesMat.EnableKeyword("_EMISSION");
                needle1.EnableKeyword("_EMISSION");
                needle2.EnableKeyword("_EMISSION");
                needle3.EnableKeyword("_EMISSION");
                



                vanGaugesMat.SetColor("_EmissionColor", EmissiveGreen);

                needle1.SetColor("_EmissionColor", EmissiveRed);
                needle2.SetColor("_EmissionColor", EmissiveRed);
                needle3.SetColor("_EmissionColor", EmissiveRed);
                

            }
            else 
            {
                vanGaugesMat.DisableKeyword("_EMISSION");
                needle1.DisableKeyword("_EMISSION");
                needle2.DisableKeyword("_EMISSION");
                needle3.DisableKeyword("_EMISSION");
                

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
