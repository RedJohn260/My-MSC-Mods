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
        public override string Version => "3.2.2"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;


        private AssetBundle ab;
        private GameObject SpeedoGauge;
        private GameObject ClockGauge;
        private GameObject RpmGauge;
        private GameObject SATSUMA;
        private FsmBool fsmTacho2Installed;
        private GameObject Tacho1;
        private FsmBool fsmTacho1Installed;
        private GameObject Clock;
        private FsmBool fsmClockBolted;
        private GameObject Dashboard;
        private FsmBool fsmDashboardInstalled;
        private GameObject Meters;
        private FsmBool fsmMetersInstalled;
        private GameObject Rpm;
        private FsmBool fsmRpmBolted;
        private GameObject ExtraG;
        private FsmBool fsmExtraGInstalled;
        private GameObject AFG;
        private FsmBool fsmAFGInstalled;
        private Texture2D extra_gauges;
        private Texture2D rpm_gauge;
        private Texture2D satsuma_dash_gauges_flipped;
        private Texture2D tachometer_gauge;
        private Texture2D speedoE;
        private Texture2D extraE;
        private Texture2D smallNeedleE;
        private Texture2D rpmE;
        private Texture2D tachoE;
        private Texture2D greenE;
        private Texture2D odoE;
        private Texture2D tachoNeedleE;

        private void AssetBundle()
        {
            ab = LoadAssets.LoadBundle(this, "gauges.unity3d");
            SpeedoGauge = GameObject.Instantiate(ab.LoadAsset("speedo_gauges.prefab")) as GameObject;
            ClockGauge = GameObject.Instantiate(ab.LoadAsset("clock_gauge.prefab")) as GameObject;
            RpmGauge = GameObject.Instantiate(ab.LoadAsset("rpm_gauge.prefab")) as GameObject;
            ObjectVars.TachoNeedle = ab.LoadAsset("TachoNeedle.mat") as Material;
            ObjectVars.Tachometer = ab.LoadAsset("Tachometer.mat") as Material;
            ObjectVars.SmallNeedle = ab.LoadAsset("SmallNeedle.mat") as Material;
            ObjectVars.Odometer = ab.LoadAsset("Odometer.mat") as Material;
            ObjectVars.GreenNeedle = ab.LoadAsset("GreenNeedle.mat") as Material;
            ObjectVars.ExtraGauges = ab.LoadAsset("ExtraGauges.mat") as Material;
           extra_gauges = ab.LoadAsset("extra_gauges.png") as Texture2D;
           rpm_gauge = ab.LoadAsset("rpm_gauge.png") as Texture2D;
           satsuma_dash_gauges_flipped = ab.LoadAsset("satsuma_dash_gauges_flipped.png") as Texture2D;
           tachometer_gauge = ab.LoadAsset("tachometer_gauge.png") as Texture2D;
            ab.Unload(false);
        }
        public override void OnLoad()
        {
            AssetBundle();
            SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");
            Svijetla();
            //LoadData();
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
                    try
                    {
                        //Speedo
                        var dashMeters = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)");
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/GaugesMesh").SetActive(false);
                        SpeedoGauge.transform.SetParent(dashMeters.transform, false);
                        SpeedoGauge.transform.localPosition = new Vector3(-0.2799262f, 0.04781772f, 0.02104893f);
                        SpeedoGauge.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        SpeedoGauge.transform.localScale = new Vector3(1f, 1f, 1f);
                        ObjectVars.SpeedoGaugeMat = SpeedoGauge.GetComponent<MeshRenderer>().material;

                        ObjectVars.SpeedoGaugeMat.DisableKeyword("_EMISSION");
                        ObjectVars.ClockGaugeMat = ClockGauge.GetComponent<MeshRenderer>().material;
                        ObjectVars.ClockGaugeMat.DisableKeyword("_EMISSION");
                        ObjectVars.RpmGaugeMat = RpmGauge.GetComponent<MeshRenderer>().material;
                        ObjectVars.RpmGaugeMat.DisableKeyword("_EMISSION");
                        ObjectVars.Tachometer.DisableKeyword("_EMISSION");
                        ObjectVars.TachoNeedle.DisableKeyword("_EMISSION");
                        ObjectVars.ExtraGauges.DisableKeyword("_EMISSION");
                        ObjectVars.GreenNeedle.DisableKeyword("_EMISSION");
                        
                        try
                        {
                            //Difusse Png Textures
                            ObjectVars.SpeedoGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_speedo.png");
                            ObjectVars.ClockGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_speedo.png");
                            ObjectVars.RpmGaugeMat.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_rpm.png");
                            ObjectVars.Tachometer.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_tacho.png");
                            ObjectVars.ExtraGauges.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_extra.png");
                            ObjectVars.SmallNeedle.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_speedo.png");
                            ObjectVars.GreenNeedle.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_extra.png");
                            ObjectVars.Odometer.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_odo.png");
                            ObjectVars.Tachometer.mainTexture = LoadAssets.LoadTexture(this, "textures/diffuse_tacho.png");
                            // Emmit png Textures
                            speedoE = LoadAssets.LoadTexture(this, "textures/emmit_speedo.png");
                            extraE = LoadAssets.LoadTexture(this, "textures/emmit_extra.png");
                            smallNeedleE = LoadAssets.LoadTexture(this, "textures/emmit_smallNeedle.png");
                            rpmE = LoadAssets.LoadTexture(this, "textures/emmit_rpm.png");
                            tachoE = LoadAssets.LoadTexture(this, "textures/emmit_tacho.png");
                            greenE = LoadAssets.LoadTexture(this, "textures/emmit_greenNeedle.png");
                            odoE = LoadAssets.LoadTexture(this, "textures/emmit_odo.png");
                            tachoNeedleE = LoadAssets.LoadTexture(this, "textures/emmit_tachoNeedle.png");

                            ObjectVars.ClockGaugeMat.SetTexture("_EmissionMap", speedoE);
                            ObjectVars.SpeedoGaugeMat.SetTexture("_EmissionMap", speedoE);
                            ObjectVars.SmallNeedle.SetTexture("_EmissionMap", smallNeedleE);
                            ObjectVars.ExtraGauges.SetTexture("_EmissionMap", extraE);
                            ObjectVars.RpmGaugeMat.SetTexture("_EmissionMap", rpmE);
                            ObjectVars.Tachometer.SetTexture("_EmissionMap", tachoE);
                            ObjectVars.GreenNeedle.SetTexture("_EmissionMap", greenE);
                            ObjectVars.Odometer.SetTexture("_EmissionMap", odoE);
                            ObjectVars.TachoNeedle.SetTexture("_EmissionMap", tachoNeedleE);
                        }
                        catch (Exception)
                        {
                            ModConsole.Error("Missing Textures --- My Summer Car/Mods/Assets/ColorfulGauges3/textures ---");
                        }
                        
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Speedometer/needle_large").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Temp/needle_small").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Fuel/needle_small").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                        ObjectVars.SmallNeedle.DisableKeyword("_EMISSION");
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000001").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000010").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/000100").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/001000").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/010000").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Gauges/Odometer/100000").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Odometer;
                        ObjectVars.Odometer.DisableKeyword("_EMISSION");

                        ObjectVars.IgnitionCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON").gameObject;
                        ObjectVars.LightCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON/RearLights").gameObject;
                        //Dashboard Tacho
                        ObjectVars.Tacho2 = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.FindChild("tachometer(xxxx2)").gameObject;
                        fsmTacho2Installed = ObjectVars.Tacho2.GetComponent<PlayMakerFSM>().Active;
                        if (fsmTacho2Installed.Value)
                        {
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/tachometer(xxxx2)/Pivot/GFX/tachometer_panel").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Tachometer;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/tachometer(xxxx2)/Pivot/GFX/NeedlePivot/tachometer_needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.TachoNeedle;
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
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2/tachometer(xxxx1)/Pivot/GFX/tachometer_panel").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.Tachometer;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2/tachometer(xxxx1)/Pivot/GFX/NeedlePivot/tachometer_needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.TachoNeedle;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Column Tacho:</color><b><color=green>Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Column Tacho:</color><b><color=red> Not Installed</color></b>");
                        }


                        


                        //Clock Gauge
                        Clock = GameObject.Find("Database/DatabaseMechanics/GaugeClock");
                        fsmClockBolted = Clock.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Bolted");
                        if (fsmClockBolted.Value == true)
                        {
                            var dashClock = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/clock gauge(Clone)");
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/clock gauge(Clone)/GaugeMeshClock").SetActive(false);
                            ClockGauge.transform.SetParent(dashClock.transform, false);
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/clock gauge(Clone)/ClockCar/hour/needle_hour").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/clock gauge(Clone)/ClockCar/minute/needle_minute").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Clock Gauge:</color><b><color=green> Installed</color></b>");

                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> Clock Gauge:</color><b><color=red> Not Installed</color></b>");
                        }
                        //RPM Gauge
                        Rpm = GameObject.Find("Database/DatabaseMechanics/GaugeRPM");
                        fsmRpmBolted = Rpm.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Bolted");
                        if (fsmRpmBolted.Value == true)
                        {
                            var dashRpm = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/rpm gauge(Clone)");
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/rpm gauge(Clone)/GaugeMeshTach").SetActive(false);
                            RpmGauge.transform.SetParent(dashRpm.transform, false);
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/pivot_gauge/rpm gauge(Clone)/Pivot/needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
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
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/bg").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.ExtraGauges;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotVolt/needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotWaterPress/needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/extra gauges(xxxxx)/PivotOilPress/needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.SmallNeedle;
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
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/fuel mixture gauge(xxxxx)/table").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.ExtraGauges;
                            GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/fuel mixture gauge(xxxxx)/mesh/AirFuel/Pivot/needle").GetComponent<MeshRenderer>().sharedMaterial = ObjectVars.GreenNeedle;
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> AF Gauge:</color><b><color=green> Installed</color></b>");
                        }
                        else
                        {
                            ModConsole.Print("<b><color=red>ColourfulGauges:</color></b><color=yellow> AF Gauge:</color><b><color=red> Not Installed</color></b>");
                        }

                        ModConsole.Print("<b><color=green>Colorful Gauges Successfully Loaded</color></b>");
                    }
                    catch (Exception ex)
                    {
                        ModConsole.Error("<b><color=red>Colorful Gauges: Problem accured in the Dashboard Meters section. Please contact the Developer of the mod for potential fix. Or submit a bug report on: 'https://www.nexusmods.com/mysummercar/mods/30'</color></b>");
                        ModConsole.Error(ex.Message.ToString());
                        UnityEngine.Debug.LogError(ex);
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

        public override void ModSettings()
        {
            Settings.AddHeader(this, "Satsuma Gauges Settings", new Color32(101, 34, 18, byte.MaxValue), new Color32(254, 254, 0, byte.MaxValue));
            Settings.AddHeader(this, "Background Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.BGcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.BGcolorB, 0f, 2f);
            Settings.AddHeader(this, "Needle Color", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddSlider(this, SettingsVars.NcolorR, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorG, 0f, 2f);
            Settings.AddSlider(this, SettingsVars.NcolorB, 0f, 2f);
            Settings.AddHeader(this, "Ambient Lights", new Color32(0, 128, 0, byte.MaxValue));
            Settings.AddCheckBox(this, SettingsVars.ambient);
        }

        public static void BackgroundColors()
        {
            if (ModLoader.GetCurrentScene() == CurrentScene.Game)
            {
                float bgcolorR = float.Parse(SettingsVars.BGcolorR.GetValue().ToString());
                float bgcolorG = float.Parse(SettingsVars.BGcolorG.GetValue().ToString());
                float bgcolorB = float.Parse(SettingsVars.BGcolorB.GetValue().ToString());

                Color EmissiveGreen = new Color(bgcolorR, bgcolorG, bgcolorB);

                if (ObjectVars.LightCheck.activeSelf == true && ObjectVars.IgnitionCheck.activeSelf == true)
                {
                    ObjectVars.SpeedoGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.SpeedoGaugeMat.EnableKeyword("_EMISSION");
                    ObjectVars.ClockGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.ClockGaugeMat.EnableKeyword("_EMISSION");
                    ObjectVars.RpmGaugeMat.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.RpmGaugeMat.EnableKeyword("_EMISSION");
                    ObjectVars.Tachometer.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.Tachometer.EnableKeyword("_EMISSION");
                    ObjectVars.Odometer.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.Odometer.EnableKeyword("_EMISSION");
                    ObjectVars.ExtraGauges.SetColor("_EmissionColor", EmissiveGreen);
                    ObjectVars.ExtraGauges.EnableKeyword("_EMISSION");
                }
                else
                {
                    ObjectVars.SpeedoGaugeMat.DisableKeyword("_EMISSION");
                    ObjectVars.ClockGaugeMat.DisableKeyword("_EMISSION");
                    ObjectVars.RpmGaugeMat.DisableKeyword("_EMISSION");
                    ObjectVars.Tachometer.DisableKeyword("_EMISSION");
                    ObjectVars.Odometer.DisableKeyword("_EMISSION");
                    ObjectVars.ExtraGauges.DisableKeyword("_EMISSION");
                }
                ObjectVars.svijetloComp1.color = EmissiveGreen;
                ObjectVars.svijetloComp2.color = EmissiveGreen;
                ObjectVars.svijetloComp3.color = EmissiveGreen;
                ObjectVars.svijetloComp4.color = EmissiveGreen;
                ObjectVars.svijetloComp5.color = EmissiveGreen;
                ObjectVars.svijetloComp6.color = EmissiveGreen;
                ObjectVars.svijetloComp7.color = EmissiveGreen;
                ObjectVars.svijetloComp8.color = EmissiveGreen;
                ObjectVars.svijetloComp9.color = EmissiveGreen;
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

                if (ObjectVars.LightCheck.activeSelf == true && ObjectVars.IgnitionCheck.activeSelf == true)
                {
                    ObjectVars.SmallNeedle.SetColor("_EmissionColor", EmissiveRed);
                    ObjectVars.TachoNeedle.SetColor("_EmissionColor", EmissiveRed);
                    ObjectVars.GreenNeedle.SetColor("_EmissionColor", EmissiveRed);
                    ObjectVars.SmallNeedle.EnableKeyword("_EMISSION");
                    ObjectVars.TachoNeedle.EnableKeyword("_EMISSION");
                    ObjectVars.GreenNeedle.EnableKeyword("_EMISSION");
                }
                else
                {
                    ObjectVars.SmallNeedle.DisableKeyword("_EMISSION");
                    ObjectVars.TachoNeedle.DisableKeyword("_EMISSION");
                    ObjectVars.GreenNeedle.DisableKeyword("_EMISSION");
                }
            }
        }

        public static  void AmbientLights()
        {
            if (ModLoader.GetCurrentScene() == CurrentScene.Game)
            {
                bool active = (bool)SettingsVars.ambient.GetValue();
                if (ObjectVars.LightCheck.activeSelf == true && ObjectVars.IgnitionCheck.activeSelf == true && active == true)
                {
                    ObjectVars.svijetlo2.SetActive(true);
                    ObjectVars.svijetlo3.SetActive(true);
                    ObjectVars.svijetlo7.SetActive(true);
                    ObjectVars.svijetlo8.SetActive(true);
                    ObjectVars.svijetlo4.SetActive(true);
                    ObjectVars.svijetlo5.SetActive(true);
                    ObjectVars.svijetlo6.SetActive(true);
                    ObjectVars.svijetlo1.SetActive(true);
                    ObjectVars.svijetlo9.SetActive(true);
                }
                else
                {
                    ObjectVars.svijetlo2.SetActive(false);
                    ObjectVars.svijetlo3.SetActive(false);
                    ObjectVars.svijetlo7.SetActive(false);
                    ObjectVars.svijetlo8.SetActive(false);
                    ObjectVars.svijetlo4.SetActive(false);
                    ObjectVars.svijetlo5.SetActive(false);
                    ObjectVars.svijetlo6.SetActive(false);
                    ObjectVars.svijetlo1.SetActive(false);
                    ObjectVars.svijetlo9.SetActive(false);
                }
            }
        }
        private void LoadSettings()
        {
            BackgroundColors();
            NeedleColors();
            AmbientLights();
        }
        public override void ModSettingsLoaded()
        {
            this.LoadSettings();
        }

        private void Svijetla()
        {
            ObjectVars.svijetlo1 = new GameObject("NovoSvijetlo1");
            ObjectVars.svijetloComp1 = ObjectVars.svijetlo1.AddComponent<Light>();
            ObjectVars.svijetloComp1.type = LightType.Point;
            ObjectVars.svijetloComp1.intensity = 3.5f;
            ObjectVars.svijetloComp1.range = 0.1f;
            ObjectVars.svijetloComp1.shadows = LightShadows.Soft;
            ObjectVars.svijetlo1.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo1.transform.localPosition = new Vector3(-0.267977f, 0.3120232f, 0.578438f);
            ObjectVars.svijetlo1.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo5 = new GameObject("NovoSvijetlo5");
            ObjectVars.svijetloComp5 = ObjectVars.svijetlo5.AddComponent<Light>();
            ObjectVars.svijetloComp5.type = LightType.Point;
            ObjectVars.svijetloComp5.intensity = 3.5f;
            ObjectVars.svijetloComp5.range = 0.1f;
            ObjectVars.svijetloComp5.shadows = LightShadows.Soft;
            ObjectVars.svijetlo5.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo5.transform.localPosition = new Vector3(-0.3579769f, 0.3020232f, 0.528438f);
            ObjectVars.svijetlo5.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo6 = new GameObject("NovoSvijetlo6");
            ObjectVars.svijetloComp6 = ObjectVars.svijetlo6.AddComponent<Light>();
            ObjectVars.svijetloComp6.type = LightType.Point;
            ObjectVars.svijetloComp6.intensity = 3.5f;
            ObjectVars.svijetloComp6.range = 0.1f;
            ObjectVars.svijetloComp6.shadows = LightShadows.Soft;
            ObjectVars.svijetlo6.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo6.transform.localPosition = new Vector3(-0.177977f, 0.3120232f, 0.518438f);
            ObjectVars.svijetlo6.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo2 = new GameObject("NovoSvijetlo2");
            ObjectVars.svijetloComp2 = ObjectVars.svijetlo2.AddComponent<Light>();
            ObjectVars.svijetloComp2.type = LightType.Point;
            ObjectVars.svijetloComp2.intensity = 3.5f;
            ObjectVars.svijetloComp2.range = 0.180f;
            ObjectVars.svijetloComp2.shadows = LightShadows.Soft;
            ObjectVars.svijetlo2.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo2.transform.localPosition = new Vector3(-0.158463f, 0.5022882f, 0.528438f);
            ObjectVars.svijetlo2.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo3 = new GameObject("NovoSvijetlo3");
            ObjectVars.svijetloComp3 = ObjectVars.svijetlo3.AddComponent<Light>();
            ObjectVars.svijetloComp3.type = LightType.Point;
            ObjectVars.svijetloComp3.intensity = 3.5f;
            ObjectVars.svijetloComp3.range = 0.1f;
            ObjectVars.svijetloComp3.shadows = LightShadows.Soft;
            ObjectVars.svijetlo3.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo3.transform.localPosition = new Vector3(0.005298f, 0.4834832f, 0.578438f);
            ObjectVars.svijetlo3.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo7 = new GameObject("NovoSvijetlo7");
            ObjectVars.svijetloComp7 = ObjectVars.svijetlo7.AddComponent<Light>();
            ObjectVars.svijetloComp7.type = LightType.Point;
            ObjectVars.svijetloComp7.intensity = 3.5f;
            ObjectVars.svijetloComp7.range = 0.1f;
            ObjectVars.svijetloComp7.shadows = LightShadows.Soft;
            ObjectVars.svijetlo7.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo7.transform.localPosition = new Vector3(-0.05797695f, 0.4820231f, 0.578438f);
            ObjectVars.svijetlo7.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo8 = new GameObject("NovoSvijetlo8");
            ObjectVars.svijetloComp8 = ObjectVars.svijetlo8.AddComponent<Light>();
            ObjectVars.svijetloComp8.type = LightType.Point;
            ObjectVars.svijetloComp8.intensity = 3.5f;
            ObjectVars.svijetloComp8.range = 0.1f;
            ObjectVars.svijetloComp8.shadows = LightShadows.Soft;
            ObjectVars.svijetlo8.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo8.transform.localPosition = new Vector3(0.06202304f, 0.4820231f, 0.578438f);
            ObjectVars.svijetlo8.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo4 = new GameObject("NovoSvijetlo4");
            ObjectVars.svijetloComp4 = ObjectVars.svijetlo4.AddComponent<Light>();
            ObjectVars.svijetloComp4.type = LightType.Point;
            ObjectVars.svijetloComp4.intensity = 3.5f;
            ObjectVars.svijetloComp4.range = 0.1f;
            ObjectVars.svijetloComp4.shadows = LightShadows.Soft;
            ObjectVars.svijetlo4.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo4.transform.localPosition = new Vector3(-0.554525f, 0.4624652f, 0.528438f);
            ObjectVars.svijetlo4.transform.localEulerAngles = new Vector3(0, 0, 0);

            ObjectVars.svijetlo9 = new GameObject("NovoSvijetlo9");
            ObjectVars.svijetloComp9 = ObjectVars.svijetlo9.AddComponent<Light>();
            ObjectVars.svijetloComp9.type = LightType.Point;
            ObjectVars.svijetloComp9.intensity = 3.7f;
            ObjectVars.svijetloComp9.range = 0.180f;
            ObjectVars.svijetloComp9.shadows = LightShadows.Soft;
            ObjectVars.svijetlo9.transform.parent = SATSUMA.transform;
            ObjectVars.svijetlo9.transform.localPosition = new Vector3(-0.179977f, 0.354023f, 0.4564387f);
            ObjectVars.svijetlo9.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        public override void Update()
        {
            BackgroundColors();
            NeedleColors();
            AmbientLights();
        }
    }
}
