using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;
using System;


namespace ColorfulGauges3
{
    public class ColorfulGauges3 : Mod
    {
        public override string ID => "ColorfulGauges3"; //Your mod ID (unique)
        public override string Name => "ColorfulGauges3"; //You mod name
        public override string Author => "RedJonh260"; //Your Username
        public override string Version => "3.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;


        private AssetBundle ab;
        private GameObject SpeedoGauge;
        private GameObject ClockGauge;
        private GameObject RpmGauge;
        private GameObject LightCheck;
        private GameObject IgnitionCheck;
        private Material SpeedoGaugeMat;
        private Material ClockGaugeMat;
        private Material RpmGaugeMat;
        private Material TachoNeedle;
        private Material Tachometer;
        private Material SmallNeedle;
        private Material Odometer;
        private Material GreenNeedle;
        private Material ExtraGauges;
        private Color EmissiveGreen;
        private Color EmissiveRed;
        public Rect guiBox1 = new Rect((float)(Screen.width - 2500 / 2), 70f, 600f, 300f);
        public Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.F4);
        private string path;
        private bool GUI1;
        public float rslider = 0f;
        public float gslider = 1f;
        public float bslider = 0f;
        public float rnslider = 1f;
        public float gnslider = 0f;
        public float bnslider = 0f;
        private bool EnableDisableLights;
        private GameObject svijetlo1;
        private GameObject svijetlo2;
        private GameObject svijetlo3;
        private GameObject svijetlo4;
        private GameObject svijetlo5;
        private GameObject svijetlo6;
        private GameObject svijetlo7;
        private GameObject svijetlo8;
        private GameObject svijetlo9;
        private Light svijetloComp1;
        private Light svijetloComp2;
        private Light svijetloComp3;
        private Light svijetloComp4;
        private Light svijetloComp5;
        private Light svijetloComp6;
        private Light svijetloComp7;
        private Light svijetloComp8;
        private Light svijetloComp9;
        private GameObject SATSUMA;
        private GameObject Tacho2;
        private FsmBool fsmTacho2Installed;
        private GameObject Tacho1;
        private FsmBool fsmTacho1Installed;
        private GameObject Clock;
        private FsmBool fsmClockInstalled;
        private GameObject Dashboard;
        private FsmBool fsmDashboardInstalled;
        private GameObject Meters;
        private FsmBool fsmMetersInstalled;
        private GameObject Rpm;
        private FsmBool fsmRpmInstalled;
        private GameObject ExtraG;
        private FsmBool fsmExtraGInstalled;
        private GameObject AFG;
        private FsmBool fsmAFGInstalled;
        private bool EgaugesSvijetla = false;
        private bool MixGaugeSvijetla = false;
        private bool SpeedoSvijetla = false;
        private bool Tacho1Svijetla = false;
        private bool Tacho2Svijetla = false;
       //private Texture2D extra_gauges;
       //private Texture2D rpm_gauge;
       //private Texture2D satsuma_dash_gauges_flipped;
       //private Texture2D tachometer_gauge;


        private void AssetBundle()
        {
            ab = LoadAssets.LoadBundle(this, "gauges.unity3d");
            SpeedoGauge = GameObject.Instantiate(ab.LoadAsset("speedo_gauges.prefab")) as GameObject;
            ClockGauge = GameObject.Instantiate(ab.LoadAsset("clock_gauge.prefab")) as GameObject;
            RpmGauge = GameObject.Instantiate(ab.LoadAsset("rpm_gauge.prefab")) as GameObject;
            TachoNeedle = ab.LoadAsset("TachoNeedle.mat") as Material;
            Tachometer = ab.LoadAsset("Tachometer.mat") as Material;
            SmallNeedle = ab.LoadAsset("SmallNeedle.mat") as Material;
            Odometer = ab.LoadAsset("Odometer.mat") as Material;
            GreenNeedle = ab.LoadAsset("GreenNeedle.mat") as Material;
            ExtraGauges = ab.LoadAsset("ExtraGauges.mat") as Material;
           //extra_gauges = ab.LoadAsset("extra_gauges.png") as Texture2D;
           //rpm_gauge = ab.LoadAsset("rpm_gauge.png") as Texture2D;
           //satsuma_dash_gauges_flipped = ab.LoadAsset("satsuma_dash_gauges_flipped.png") as Texture2D;
           //tachometer_gauge = ab.LoadAsset("tachometer_gauge.png") as Texture2D;
            ab.Unload(false);
        }
        public override void OnLoad()
        {
            AssetBundle();
            SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");
            Keybind.Add(this, this.openGUI);
            this.path = ModLoader.GetModAssetsFolder(this);
            
            //Dashboard
            Dashboard = GameObject.Find("SATSUMA(557kg, 248)/Dashboard").transform.Find("trigger_dashboard").gameObject;
            fsmDashboardInstalled = Dashboard.GetComponent<PlayMakerFSM>().Active;
            if (fsmDashboardInstalled.Value == false)
            {
                //Dashboard Meters
                ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard:</color><b><color=green> Installed</color></b>");
                Meters = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.FindChild("trigger_meters").gameObject;
                fsmMetersInstalled = Meters.GetComponent<PlayMakerFSM>().Active;
                if (fsmMetersInstalled.Value == false)
                {
                    ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard Meters:</color><b><color=green> Installed</color></b>");
                    Svijetla();
                    Loadsettings();
                    LightLoad();
                    try
                    {
                        //Speedo
                        var dashMeters = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)");
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugesMesh").SetActive(false);
                        SpeedoGauge.transform.SetParent(dashMeters.transform, false);
                        SpeedoGauge.transform.localPosition = new Vector3(-0.2799262f, 0.04781772f, 0.02104893f);
                        SpeedoGauge.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        SpeedoGauge.transform.localScale = new Vector3(1f, 1f, 1f);
                        SpeedoGaugeMat = SpeedoGauge.GetComponent<MeshRenderer>().material;
                        
                        SpeedoGaugeMat.DisableKeyword("_EMISSION");
                        ClockGaugeMat = ClockGauge.GetComponent<MeshRenderer>().material;
                        ClockGaugeMat.DisableKeyword("_EMISSION");
                        RpmGaugeMat = RpmGauge.GetComponent<MeshRenderer>().material;
                        RpmGaugeMat.DisableKeyword("_EMISSION");
                        Tachometer.DisableKeyword("_EMISSION");
                        TachoNeedle.DisableKeyword("_EMISSION");
                        ExtraGauges.DisableKeyword("_EMISSION");
                        GreenNeedle.DisableKeyword("_EMISSION");
                        /*try
                        {
                            SpeedoGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/speedo.png");
                            ClockGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/speedo.png");
                            RpmGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/rpm.png");
                            Tachometer.mainTexture = LoadAssets.LoadTexture(this, "textures/tacho.png");
                            ExtraGauges.mainTexture = LoadAssets.LoadTexture(this, "textures/extra.png");
                            SmallNeedle.mainTexture = LoadAssets.LoadTexture(this, "textures/speedo.png");
                        }
                        catch (Exception)
                        {
                            SpeedoGaugeMat.mainTexture = satsuma_dash_gauges_flipped;
                            ClockGaugeMat.mainTexture = satsuma_dash_gauges_flipped;
                            RpmGaugeMat.mainTexture = rpm_gauge;
                            Tachometer.mainTexture = tachometer_gauge;
                            ExtraGauges.mainTexture = extra_gauges;
                            SmallNeedle.mainTexture = satsuma_dash_gauges_flipped;
                        }*/

                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Speedometer/needle_large").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Temp/needle_small").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Fuel/needle_small").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                        SmallNeedle.DisableKeyword("_EMISSION");
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000001").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000010").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000100").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/001000").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/010000").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/100000").GetComponent<MeshRenderer>().sharedMaterial = Odometer;
                        Odometer.DisableKeyword("_EMISSION");
                        SpeedoSvijetla = true;

                        IgnitionCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON").gameObject;
                        LightCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON/RearLights").gameObject;
                        //Dashboard Tacho
                        Tacho2 = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.FindChild("tachometer(xxxx2)").gameObject;
                        fsmTacho2Installed = Tacho2.GetComponent<PlayMakerFSM>().Active;
                        if (fsmTacho2Installed.Value)
                        {
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/tachometer(xxxx2)/Pivot/GFX/tachometer_panel").GetComponent<MeshRenderer>().sharedMaterial = Tachometer;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/tachometer(xxxx2)/Pivot/GFX/NeedlePivot/tachometer_needle").GetComponent<MeshRenderer>().sharedMaterial = TachoNeedle;
                            Tacho2Svijetla = true;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard Tacho:</color><b><color=green> Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard Tacho:</color><b><color=red> Not Installed</color></b>");
                        }
                        //Column Tacho
                        Tacho1 = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2").transform.FindChild("tachometer(xxxx1)").gameObject;
                        fsmTacho1Installed = Tacho1.GetComponent<PlayMakerFSM>().Active;
                        if (fsmTacho1Installed.Value)
                        {
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2/tachometer(xxxx1)/Pivot/GFX/tachometer_panel").GetComponent<MeshRenderer>().sharedMaterial = Tachometer;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2/tachometer(xxxx1)/Pivot/GFX/NeedlePivot/tachometer_needle").GetComponent<MeshRenderer>().sharedMaterial = TachoNeedle;
                            Tacho1Svijetla = true;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Column Tacho:</color><b><color=green>Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Column Tacho:</color><b><color=red> Not Installed</color></b>");
                        }
                        //Clock Gauge
                        Clock = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)").transform.Find("trigger_clock").gameObject;
                        fsmClockInstalled = Clock.GetComponent<PlayMakerFSM>().Active;
                        if (fsmClockInstalled.Value == false)
                        {
                            var dashClock = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/clock gauge(Clone)");
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/clock gauge(Clone)/GaugeMeshClock").SetActive(false);
                            ClockGauge.transform.SetParent(dashClock.transform, false);
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/clock gauge(Clone)/ClockCar/hour/needle_hour").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/clock gauge(Clone)/ClockCar/minute/needle_minute").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Clock Gauge:</color><b><color=green> Installed</color></b>");

                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Clock Gauge:</color><b><color=red> Not Installed</color></b>");
                        }
                        //RPM Gauge
                        Rpm = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)").transform.Find("trigger_rpm").gameObject;
                        fsmRpmInstalled = Rpm.GetComponent<PlayMakerFSM>().Active;
                        if (fsmRpmInstalled.Value == false)
                        {
                            var dashRpm = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/rpm gauge(Clone)");
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/rpm gauge(Clone)/GaugeMeshTach").SetActive(false);
                            RpmGauge.transform.SetParent(dashRpm.transform, false);
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugePivot/rpm gauge(Clone)/Pivot/needle").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Rpm Gauge:</color><b><color=green> Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Rpm Gauge:</color><b><color=red> Not Installed</color></b>");
                        }
                        //Extra Gauges
                        ExtraG = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.Find("trigger_extra_gauges").gameObject;
                        fsmExtraGInstalled = ExtraG.GetComponent<PlayMakerFSM>().Active;
                        if (fsmExtraGInstalled.Value == false)
                        {
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/bg").GetComponent<MeshRenderer>().sharedMaterial = ExtraGauges;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotVolt/needle").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotWaterPress/needle").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotOilPress/needle").GetComponent<MeshRenderer>().sharedMaterial = SmallNeedle;
                            EgaugesSvijetla = true;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Extra Gauges:</color><b><color=green> Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Extra Gauges:</color><b><color=red> Not Installed</color></b>");
                        }
                        //AF Gauge
                        AFG = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.Find("trigger_fuel_mix_gauge").gameObject;
                        fsmAFGInstalled= AFG.GetComponent<PlayMakerFSM>().Active;
                        if (fsmAFGInstalled.Value == false)
                        {
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/fuel mixture gauge(xxxxx)/table").GetComponent<MeshRenderer>().sharedMaterial = ExtraGauges;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/fuel mixture gauge(xxxxx)/mesh/AirFuel/Pivot/needle").GetComponent<MeshRenderer>().sharedMaterial = GreenNeedle;
                            MixGaugeSvijetla = true;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> AF Gauge:</color><b><color=green> Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> AF Gauge:</color><b><color=red> Not Installed</color></b>");
                        }

                        ModConsole.Print("<b><color=green>Colorful Gauges Successfully Loaded</color></b>");
                    }
                    catch (Exception)
                    {
                        ModConsole.Error("<b><color=red>Colorful Gauges: Problem accured in the Dashboard Meters section. Please contact the Developer of the mod for potential fix. Or submit a bug report on: 'https://www.nexusmods.com/mysummercar/mods/30'</color></b>");
                    }
                }
                else
                {
                    ModConsole.Error("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard Meters:</color><b><color=red> Not Installed</color></b>");
                    ModConsole.Error("<b><color=red>ColourfulGauges Disabled.</color></b>");
                    ModConsole.Error("<b><color=red>Please install the Dashboard Meters and Reload a Game.</color></b>");
                }
            }
            else
            {
                ModConsole.Error("<b><color=red>ColourfulGauges:</color></b><color=yellow> Dashboard:</color><b><color=red> Not Installed</color></b>");
                ModConsole.Error("<b><color=red>ColourfulGauges Disabled.</color></b>");
                ModConsole.Error("<b><color=red>Please install the dashboard and Reload a Game.</color></b>");
            }
            
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
                GUI.ModalWindow(1, this.guiBox1, new GUI.WindowFunction(this.Window1), "ColorfulGauges 3.0");
            }
        }

        public override void Update()
        {
            EmissiveGreen = new Color(rslider, gslider, bslider);
            EmissiveRed = new Color(rnslider, gnslider, bnslider);
            try
            {
                if (LightCheck.activeSelf == true && IgnitionCheck.activeSelf == true)
                {


                    SpeedoGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    ClockGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    RpmGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    Tachometer.SetColor("_EmissionColor", EmissiveGreen);
                    Odometer.SetColor("_EmissionColor", EmissiveGreen);
                    ExtraGauges.SetColor("_EmissionColor", EmissiveGreen);
                    SmallNeedle.SetColor("_EmissionColor", EmissiveRed);
                    TachoNeedle.SetColor("_EmissionColor", EmissiveRed);
                    GreenNeedle.SetColor("_EmissionColor", EmissiveRed);
                    SpeedoGaugeMat.EnableKeyword("_EMISSION");
                    SmallNeedle.EnableKeyword("_EMISSION");
                    ClockGaugeMat.EnableKeyword("_EMISSION");
                    RpmGaugeMat.EnableKeyword("_EMISSION");
                    Tachometer.EnableKeyword("_EMISSION");
                    TachoNeedle.EnableKeyword("_EMISSION");
                    ExtraGauges.EnableKeyword("_EMISSION");
                    GreenNeedle.EnableKeyword("_EMISSION");
                    Odometer.EnableKeyword("_EMISSION");
                }
                else
                {
                    SpeedoGaugeMat.DisableKeyword("_EMISSION");
                    SmallNeedle.DisableKeyword("_EMISSION");
                    ClockGaugeMat.DisableKeyword("_EMISSION");
                    RpmGaugeMat.DisableKeyword("_EMISSION");
                    Tachometer.DisableKeyword("_EMISSION");
                    TachoNeedle.DisableKeyword("_EMISSION");
                    ExtraGauges.DisableKeyword("_EMISSION");
                    GreenNeedle.DisableKeyword("_EMISSION");
                    Odometer.DisableKeyword("_EMISSION");
                }
                SvijetlaColor();
                SvijetlaEnableDisable();
                if (this.openGUI.IsDown())
                {
                    this.GuiShow();
                }
            }
            catch (Exception)
            {

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
                LightSave();
                ModConsole.Print("Gauge Settings Saved");
            }
            //Load Button
            if (GUI.Button(new Rect(355f, 140f, 150f, 40f), "Load Settings"))
            {
               this.Loadsettings();
               LightLoad();
                ModConsole.Print("Gauge Settings Loaded");
            }
            // Gauges
            if (GUI.Button(new Rect(205f, 250f, 150f, 40f), "Close"))
            {
                GUI1 = false;
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }
            if (GUI.Button(new Rect(205f, 200f, 150f, 40f), "Enable/Disable Lights"))
            {
                EnableDisableLights = !EnableDisableLights;
            }
            GUI.DragWindow();
        }
        private void Svijetla()
        {

            svijetlo1 = new GameObject("NovoSvijetlo1");
            svijetloComp1 = svijetlo1.AddComponent<Light>();
            svijetloComp1.type = LightType.Point;
            svijetloComp1.intensity = 3.5f;
            svijetloComp1.range = 0.1f;
            svijetloComp1.shadows = LightShadows.Soft;
            svijetlo1.transform.parent = SATSUMA.transform;
            svijetlo1.transform.localPosition = new Vector3(-0.267977f, 0.3120232f, 0.578438f);
            svijetlo1.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo5 = new GameObject("NovoSvijetlo5");
            svijetloComp5 = svijetlo5.AddComponent<Light>();
            svijetloComp5.type = LightType.Point;
            svijetloComp5.intensity = 3.5f;
            svijetloComp5.range = 0.1f;
            svijetloComp5.shadows = LightShadows.Soft;
            svijetlo5.transform.parent = SATSUMA.transform;
            svijetlo5.transform.localPosition = new Vector3(-0.3579769f, 0.3020232f, 0.528438f);
            svijetlo5.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo6 = new GameObject("NovoSvijetlo6");
            svijetloComp6 = svijetlo6.AddComponent<Light>();
            svijetloComp6.type = LightType.Point;
            svijetloComp6.intensity = 3.5f;
            svijetloComp6.range = 0.1f;
            svijetloComp6.shadows = LightShadows.Soft;
            svijetlo6.transform.parent = SATSUMA.transform;
            svijetlo6.transform.localPosition = new Vector3(-0.177977f, 0.3120232f, 0.518438f);
            svijetlo6.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo2 = new GameObject("NovoSvijetlo2");
            svijetloComp2 = svijetlo2.AddComponent<Light>();
            svijetloComp2.type = LightType.Point;
            svijetloComp2.intensity = 3.5f;
            svijetloComp2.range = 0.180f;
            svijetloComp2.shadows = LightShadows.Soft;
            svijetlo2.transform.parent = SATSUMA.transform;
            svijetlo2.transform.localPosition = new Vector3(-0.158463f, 0.5022882f, 0.528438f);
            svijetlo2.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo3 = new GameObject("NovoSvijetlo3");
            svijetloComp3 = svijetlo3.AddComponent<Light>();
            svijetloComp3.type = LightType.Point;
            svijetloComp3.intensity = 3.5f;
            svijetloComp3.range = 0.1f;
            svijetloComp3.shadows = LightShadows.Soft;
            svijetlo3.transform.parent = SATSUMA.transform;
            svijetlo3.transform.localPosition = new Vector3(0.005298f, 0.4834832f, 0.578438f);
            svijetlo3.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo7 = new GameObject("NovoSvijetlo7");
            svijetloComp7 = svijetlo7.AddComponent<Light>();
            svijetloComp7.type = LightType.Point;
            svijetloComp7.intensity = 3.5f;
            svijetloComp7.range = 0.1f;
            svijetloComp7.shadows = LightShadows.Soft;
            svijetlo7.transform.parent = SATSUMA.transform;
            svijetlo7.transform.localPosition = new Vector3(-0.05797695f, 0.4820231f, 0.578438f);
            svijetlo7.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo8 = new GameObject("NovoSvijetlo8");
            svijetloComp8 = svijetlo8.AddComponent<Light>();
            svijetloComp8.type = LightType.Point;
            svijetloComp8.intensity = 3.5f;
            svijetloComp8.range = 0.1f;
            svijetloComp8.shadows = LightShadows.Soft;
            svijetlo8.transform.parent = SATSUMA.transform;
            svijetlo8.transform.localPosition = new Vector3(0.06202304f, 0.4820231f, 0.578438f);
            svijetlo8.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo4 = new GameObject("NovoSvijetlo4");
            svijetloComp4 = svijetlo4.AddComponent<Light>();
            svijetloComp4.type = LightType.Point;
            svijetloComp4.intensity = 3.5f;
            svijetloComp4.range = 0.1f;
            svijetloComp4.shadows = LightShadows.Soft;
            svijetlo4.transform.parent = SATSUMA.transform;
            svijetlo4.transform.localPosition = new Vector3(-0.554525f, 0.4624652f, 0.528438f);
            svijetlo4.transform.localEulerAngles = new Vector3(0, 0, 0);

            svijetlo9 = new GameObject("NovoSvijetlo9");
            svijetloComp9 = svijetlo9.AddComponent<Light>();
            svijetloComp9.type = LightType.Point;
            svijetloComp9.intensity = 3.7f;
            svijetloComp9.range = 0.180f;
            svijetloComp9.shadows = LightShadows.Soft;
            svijetlo9.transform.parent = SATSUMA.transform;
            svijetlo9.transform.localPosition = new Vector3(-0.179977f, 0.354023f, 0.4564387f);
            svijetlo9.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        private void LightLoad()
        {
            string[] strArrays1 = new string[1];

            strArrays1 = File.ReadAllLines(string.Concat(this.path, "/Light.txt"));
            EnableDisableLights = bool.Parse(strArrays1[0]);

        }
        private void LightSave()
        {
            string[] str1 = new string[1];
            int num = 0;
            bool StoreLight = EnableDisableLights;
            str1[num] = StoreLight.ToString();
            File.WriteAllLines(string.Concat(this.path, "/Light.txt"), str1);
        }
        private void SvijetlaColor()
        {
            svijetloComp1.color = EmissiveGreen;
            svijetloComp2.color = EmissiveGreen;
            svijetloComp3.color = EmissiveGreen;
            svijetloComp4.color = EmissiveGreen;
            svijetloComp5.color = EmissiveGreen;
            svijetloComp6.color = EmissiveGreen;
            svijetloComp7.color = EmissiveGreen;
            svijetloComp8.color = EmissiveGreen;
            svijetloComp9.color = EmissiveGreen;
        }
        private void SvijetlaEnableDisable()
        {

            if (EnableDisableLights == true && LightCheck.activeSelf == true && IgnitionCheck.activeSelf == true)
            {
                if (Tacho2Svijetla == true)
                {
                    svijetlo2.SetActive(true);


                }

                if (EgaugesSvijetla == true)
                {
                    svijetlo3.SetActive(true);
                    svijetlo7.SetActive(true);
                    svijetlo8.SetActive(true);
                }
                if (MixGaugeSvijetla == true)
                {
                    svijetlo4.SetActive(true);
                }
                if (SpeedoSvijetla == true)
                {
                    svijetlo5.SetActive(true);
                    svijetlo6.SetActive(true);
                    svijetlo1.SetActive(true);
                }
                if (Tacho1Svijetla == true)
                {
                    svijetlo9.SetActive(true);


                }

            }
            else
            {
                svijetlo1.SetActive(false);
                svijetlo2.SetActive(false);
                svijetlo3.SetActive(false);
                svijetlo4.SetActive(false);
                svijetlo5.SetActive(false);
                svijetlo6.SetActive(false);
                svijetlo7.SetActive(false);
                svijetlo8.SetActive(false);
                svijetlo9.SetActive(false);
            }

        }
    }
}
