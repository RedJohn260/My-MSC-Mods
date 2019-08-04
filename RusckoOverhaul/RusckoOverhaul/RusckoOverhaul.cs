using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace RusckoOverhaul
{
    public class RusckoOverhaul : Mod
    {
        public override string ID => "RusckoOverhaul"; //Your mod ID (unique)
        public override string Name => "RusckoOverhaul"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "0.3"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject body;
        private GameObject doorL;
        private GameObject doorR;
        private GameObject interior;
        private Material bodyMat;
        private GameObject RUSCKOB;
        private Material notS;
        private GameObject doorRear;
        private Material reardoor;
        private GameObject rimFL;
        private GameObject rimFR;
        private GameObject rimRL;
        private GameObject rimRR;
        private GameObject tireFL;
        private GameObject tireFR;
        private GameObject tireRL;
        private GameObject tireRR;
        private GameObject dashboard;
        private GameObject parts1;
        private GameObject parts2;
        private Material Chrome;
        private Material Chrome1;
        private GameObject Wo;
        private GameObject wheel;
        private Material pedals;
        private Material wipers;
        private Material wood;
        private Material bdd;
        private Material chassis;
        private GameObject plate;
        private GameObject gauges;
        private Material gauge;
        private FsmInt lightState;
        private GameObject Hive;
        private FsmBool fsmHiveKilled;
        private bool HiveKilled;
        private GameObject ggg;
        private float gear_ratio = 5.6f;
        private string ratiog;
        private float[] gear = new float[7];
        private float power = 1.3f;
        private float shiftRPMmax = 5000f;
        private GameObject RUSCKO;
        private Drivetrain drivetrain;
        private float TCS_SLIP = 0.1f;
        private AxisCarController AxisCarController;
        Settings ABS = new Settings("ABS", "ABS ON/OFF", true);
        Settings ESP = new Settings("ESP", "ESP ON/OFF", true);
        Settings TCS = new Settings("TCS", "TCS ON/OFF", true);
        Settings AWD = new Settings("AWD", "AWD ON/OFF", true);
        Settings Ratio = new Settings("slider1", "Gear Ratio",6.2f);


        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "ruscko.unity3d");
            body = GameObject.Instantiate(ab.LoadAsset("rusckoBody.prefab")) as GameObject;
            doorL= GameObject.Instantiate(ab.LoadAsset("doorLeft.prefab")) as GameObject;
            doorR = GameObject.Instantiate(ab.LoadAsset("doorRight.prefab")) as GameObject;
            interior = GameObject.Instantiate(ab.LoadAsset("rusckoInterior.prefab")) as GameObject;
            doorRear = GameObject.Instantiate(ab.LoadAsset("doorRear.prefab")) as GameObject;
            rimFL = GameObject.Instantiate(ab.LoadAsset("rim.prefab")) as GameObject;
            rimFR = GameObject.Instantiate(ab.LoadAsset("rim.prefab")) as GameObject;
            rimRL = GameObject.Instantiate(ab.LoadAsset("rim.prefab")) as GameObject;
            rimRR = GameObject.Instantiate(ab.LoadAsset("rim.prefab")) as GameObject;
            tireFL = GameObject.Instantiate(ab.LoadAsset("tire.prefab")) as GameObject;
            tireFR = GameObject.Instantiate(ab.LoadAsset("tire.prefab")) as GameObject;
            tireRL = GameObject.Instantiate(ab.LoadAsset("tire.prefab")) as GameObject;
            tireRR = GameObject.Instantiate(ab.LoadAsset("tire.prefab")) as GameObject;
            dashboard = GameObject.Instantiate(ab.LoadAsset("dashboard.prefab")) as GameObject;
            parts1 = GameObject.Instantiate(ab.LoadAsset("outside_parts.prefab")) as GameObject;
            parts2 = GameObject.Instantiate(ab.LoadAsset("interior_parts.prefab")) as GameObject;
            Wo = GameObject.Instantiate(ab.LoadAsset("window_opener.prefab")) as GameObject;
            wheel = GameObject.Instantiate(ab.LoadAsset("wheel.prefab")) as GameObject;
            plate = GameObject.Instantiate(ab.LoadAsset("licencePlate.prefab")) as GameObject;
            bodyMat = ab.LoadAsset("body.mat") as Material;
            notS = ab.LoadAsset("notShiny.mat") as Material;
            Chrome = ab.LoadAsset("ccc.mat") as Material;
            Chrome1 = ab.LoadAsset("ch1.mat") as Material;
            pedals = ab.LoadAsset("pedals.mat") as Material;
            wipers = ab.LoadAsset("wipers.mat") as Material;
            wood = ab.LoadAsset("wood.mat") as Material;
            bdd = ab.LoadAsset("BdoorPart.mat") as Material;
            chassis = ab.LoadAsset("chassis.mat") as Material;
            reardoor = ab.LoadAsset("reardoor.mat") as Material;
            gauges = GameObject.Instantiate(ab.LoadAsset("gauges.prefab")) as GameObject;
            ab.Unload(false);

            //bee hive
            Hive = GameObject.Find("RCO_RUSCKO12(270)/WaspHive");
            fsmHiveKilled = Hive.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmBool("WaspHive");
            var mat = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/ruscko_matress").gameObject;
            mat.SetActive(false);


            //body
            var piv = GameObject.Find("RCO_RUSCKO12(270)/PIVOT");
            RUSCKOB = GameObject.Find("RCO_RUSCKO12(270)/PIVOT/ruscko_body");
            RUSCKOB.SetActive(false);
            body.transform.SetParent(piv.transform, false);
            body.SetActive(true);
            //door left
            var dl = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorl/door");
            dl.GetComponent<MeshFilter>().sharedMesh = doorL.GetComponent<MeshFilter>().sharedMesh;
            dl.GetComponent<MeshRenderer>().sharedMaterial = bodyMat;
            //door right
            var dr = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorr/door");
            dr.GetComponent<MeshFilter>().sharedMesh = doorR.GetComponent<MeshFilter>().sharedMesh;
            dr.GetComponent<MeshRenderer>().sharedMaterial = bodyMat;
            //interior
            var LOD = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD");
            var Gint = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/ruscko_interior");
            Gint.GetComponent<MeshRenderer>().enabled = false;
            interior.transform.SetParent(LOD.transform, false);
            interior.transform.localPosition = new Vector3(0f, 0.31f, 0f);
            interior.transform.localEulerAngles = new Vector3(270f, 180f, 0f);
            interior.SetActive(true);
            //dashboard
            var dash = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/ruscko_dash").gameObject;
            dash.GetComponent<MeshRenderer>().enabled = false;
            dashboard.transform.SetParent(dash.transform, false);
            //door panels
            var dlp = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorl/door/panel");
            dlp.GetComponent<MeshRenderer>().sharedMaterial = notS;
            var drp = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorr/door/panel");
            drp.GetComponent<MeshRenderer>().sharedMaterial = notS;
            //rear door
            var rd = GameObject.Find("RCO_RUSCKO12(270)/RearDoor/doorear/door").gameObject;
            rd.GetComponent<MeshRenderer>().enabled = false;
            doorRear.transform.SetParent(rd.transform, false);
            //tires and rims
            var fl = GameObject.Find("RCO_RUSCKO12(270)/wheelFL/tire/rim").gameObject;
            fl.GetComponent<MeshRenderer>().enabled = false;
            rimFL.transform.SetParent(fl.transform, false);

            var fr = GameObject.Find("RCO_RUSCKO12(270)/wheelFR/tire/rim").gameObject;
            fr.GetComponent<MeshRenderer>().enabled = false;
            rimFR.transform.SetParent(fr.transform, false);

            var rl = GameObject.Find("RCO_RUSCKO12(270)/wheelRL/tire/rim").gameObject;
            rl.GetComponent<MeshRenderer>().enabled = false;
            rimRL.transform.SetParent(rl.transform, false);

            var rr = GameObject.Find("RCO_RUSCKO12(270)/wheelRR/tire/rim").gameObject;
            rr.GetComponent<MeshRenderer>().enabled = false;
            rimRR.transform.SetParent(rr.transform, false);

            var flt = GameObject.Find("RCO_RUSCKO12(270)/wheelFL/tire/tire").gameObject;
            flt.GetComponent<MeshRenderer>().enabled = false;
            tireFL.transform.SetParent(flt.transform, false);

            var frt = GameObject.Find("RCO_RUSCKO12(270)/wheelFR/tire/tire").gameObject;
            frt.GetComponent<MeshRenderer>().enabled = false;
            tireFR.transform.SetParent(frt.transform, false);

            var rlt = GameObject.Find("RCO_RUSCKO12(270)/wheelRL/tire/tire").gameObject;
            rlt.GetComponent<MeshRenderer>().enabled = false;
            tireRL.transform.SetParent(rlt.transform, false);

            var rrt = GameObject.Find("RCO_RUSCKO12(270)/wheelRR/tire/tire").gameObject;
            rrt.GetComponent<MeshRenderer>().enabled = false;
            tireRR.transform.SetParent(rrt.transform, false);

            GameObject.Find("RCO_RUSCKO12(270)/wheelFL/tire/hubcap").SetActive(false);
            GameObject.Find("RCO_RUSCKO12(270)/wheelRL/tire/hubcap 1").SetActive(false);
            GameObject.Find("RCO_RUSCKO12(270)/wheelRR/tire/hubcap 2").SetActive(false);

            //parts
            var par = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/ruscko_parts").gameObject;
            par.GetComponent<MeshRenderer>().enabled = false;
            parts1.transform.SetParent(par.transform, false);

            var intpar = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/ruscko_interior_parts").gameObject;
            intpar.GetComponent<MeshRenderer>().enabled = false;
            parts2.transform.SetParent(intpar.transform, false);

            var par1 = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorl/door/parts").gameObject;
            par1.GetComponent<MeshRenderer>().sharedMaterial = Chrome;
            var par2 = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorr/door/parts").gameObject;
            par2.GetComponent<MeshRenderer>().sharedMaterial = Chrome;
            var par3 = GameObject.Find("RCO_RUSCKO12(270)/DriverDoors/doorl/door/opener/mesh").gameObject;
            par3.GetComponent<MeshRenderer>().enabled = false;
            Wo.transform.SetParent(par3.transform, false);
            var par4 = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/Dashboard/LightSwitch/mesh").gameObject;
            par4.GetComponent<MeshRenderer>().sharedMaterial = Chrome;
            var par5 = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/Dashboard/WiperSwitch/mesh").gameObject;
            par5.GetComponent<MeshRenderer>().sharedMaterial = Chrome;
            //wheel
            var wh = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/Steering/RusckoSteeringPivot/steering").gameObject;
            wh.GetComponent<MeshRenderer>().enabled = false;
            wheel.transform.SetParent(wh.transform, false);
            //pedals leavers
            var p1 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/Pedals/clutch/mesh").gameObject;
            var p2 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/Pedals/brake/mesh").gameObject;
            var p3 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/Pedals/throttle/mesh").gameObject;
            var p4 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/GearLever/Vibration/Pivot/lever").gameObject;
            var p5 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Dashboard/ParkingBrake/LeverPivot/mesh").gameObject;
            p1.GetComponent<MeshRenderer>().sharedMaterial = pedals;
            p2.GetComponent<MeshRenderer>().sharedMaterial = pedals;
            p3.GetComponent<MeshRenderer>().sharedMaterial = pedals;
            p4.GetComponent<MeshRenderer>().sharedMaterial = pedals;
            p5.GetComponent<MeshRenderer>().sharedMaterial = pedals;
            //wipers
            var w1 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Wipers/WiperLeftPivot/wipers_tap").gameObject;
            var w2 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Wipers/WiperLeftPivot/wipers_tap/wipers_rod").gameObject;
            var w3 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Wipers/WiperRightPivot/wipers_tap").gameObject;
            var w4 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/Wipers/WiperRightPivot/wipers_tap/wipers_rod").gameObject;
            w1.GetComponent<MeshRenderer>().sharedMaterial = wipers;
            w2.GetComponent<MeshRenderer>().sharedMaterial = wipers;
            w3.GetComponent<MeshRenderer>().sharedMaterial = wipers;
            w4.GetComponent<MeshRenderer>().sharedMaterial = wipers;
            //roofrack
            var r1 = GameObject.Find("RCO_RUSCKO12(270)/PIVOT/ruscko_roof_shelf").gameObject;
            r1.GetComponent<MeshRenderer>().sharedMaterial = Chrome1;
            var r2 = GameObject.Find("RCO_RUSCKO12(270)/PIVOT/ruscko_roof_Shelf").gameObject;
            r2.GetComponent<MeshRenderer>().sharedMaterial = wood;
            //b door part
            var bd = GameObject.Find("RCO_RUSCKO12(270)/RearDoor/doorear/door/parts").gameObject;
            bd.GetComponent<MeshRenderer>().sharedMaterial = bdd;
            //chassis
            var ch = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/ruscko_chassis").gameObject;
            ch.GetComponent<MeshRenderer>().sharedMaterial = chassis;
            //axles
            var a1 = GameObject.Find("RCO_RUSCKO12(270)/Suspension/LeafSprings 1/Front").gameObject;
            var a2 = GameObject.Find("RCO_RUSCKO12(270)/Suspension/LeafSprings 1/Rear").gameObject;
            var a3 = GameObject.Find("RCO_RUSCKO12(270)/Suspension/SuspFR/ruscko_axle_front").gameObject;
            var a4 = GameObject.Find("RCO_RUSCKO12(270)/Suspension/SuspFL/ruscko_axle_front").gameObject;
            var a5 = GameObject.Find("RCO_RUSCKO12(270)/wheelRL/wheel_spindle_rl/axle").gameObject;
            var a6 = GameObject.Find("RCO_RUSCKO12(270)/wheelRR/wheel_spindle_rr/axle 1").gameObject;
            a1.GetComponent<SkinnedMeshRenderer>().sharedMaterial = chassis;
            a2.GetComponent<SkinnedMeshRenderer>().sharedMaterial = chassis;
            a3.GetComponent<SkinnedMeshRenderer>().sharedMaterial = chassis;
            a4.GetComponent<SkinnedMeshRenderer>().sharedMaterial = chassis;
            a5.GetComponent<MeshRenderer>().sharedMaterial = chassis;
            a6.GetComponent<MeshRenderer>().sharedMaterial = chassis;
            // licence Plate 
            plate.transform.SetParent(piv.transform, false);
            //gauges
            var g1 = GameObject.Find("RCO_RUSCKO12(270)").transform.Find("LOD/ruscko_gauges").gameObject;
            g1.GetComponent<MeshRenderer>().enabled = false;
            gauges.transform.SetParent(g1.transform, false);
            gauge = gauges.GetComponent<MeshRenderer>().material;
            gauge.DisableKeyword("_EMISSION");

            ggg = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD/Dashboard/Knobs/Lights").gameObject;
            lightState = ggg.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("Selection");

            //Gear Array
            gear[0] = -4.1f;
            gear[1] = 0;
            gear[2] = 3.5f;
            gear[3] = 2.8f;
            gear[4] = 2.1f;
            gear[5] = 1.5f;
            gear[6] = 1f;
            //Ruscko Tuning
            RUSCKO = GameObject.Find("RCO_RUSCKO12(270)");
            drivetrain = RUSCKO.GetComponent<Drivetrain>();
            
            
            AxisCarController = drivetrain.GetComponent<AxisCarController>();           
            AxisCarController.TCSAllowedSlip = TCS_SLIP;
            drivetrain.finalDriveRatio = gear_ratio;
            drivetrain.gearRatios = gear;
            drivetrain.powerMultiplier = power;
            drivetrain.shiftUpRPM = shiftRPMmax;
            Ratio.Name = "Ruscko Overhaul Settings";
            ratiog = string.Format("{0:G}", gear_ratio);
            ModConsole.Print("<b><color=green>Ruscko Overhaul Mod Loaded</color></b>");
            ModConsole.Print("<color=orange>RUSCKO ESP ON:</color>" + ABS.GetValue());
            ModConsole.Print("<color=orange>RUSCKO ABS ON:</color>" + ESP.GetValue());
            ModConsole.Print("<color=orange>RUSCKO TCS ON:</color>" + TCS.GetValue());
            ModConsole.Print("<color=orange>RUSCKO AWD ON:</color>" + AWD.GetValue());
            ModConsole.Print("<color=orange>RUSCKO Gear Ratio:</color>" + ratiog);
            ModConsole.Print("<color=orange>RUSCKO Power Multiplier:</color>" + power);
        }

        public override void ModSettings()
        {
            Settings.AddCheckBox(this, ABS);
            Settings.AddCheckBox(this, ESP);
            Settings.AddCheckBox(this, TCS);
            Settings.AddCheckBox(this, AWD);
        }
        
        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            if (lightState.Value > 0)
            {
                gauge.EnableKeyword("_EMISSION");
                
            }
            else
            {
                gauge.DisableKeyword("_EMISSION");
                
            }
            if (!HiveKilled && fsmHiveKilled.Value)
            {
                Hive.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                HiveKilled = true;
            }
            if ((bool)ABS.GetValue())
            {
                AxisCarController.ABS = true;

            }
            else
            {
                AxisCarController.ABS = false;

            }
            if ((bool)ESP.GetValue())
            {
                AxisCarController.ESP = true;

            }
            else
            {
                AxisCarController.ESP = false;

            }
            if ((bool)TCS.GetValue())
            {
                AxisCarController.TCS = true;

            }
            else
            {
                AxisCarController.TCS = false;

            }
            if ((bool)AWD.GetValue())
            {
                drivetrain.SetTransmission(Drivetrain.Transmissions.AWD);
                drivetrain.transmission = Drivetrain.Transmissions.AWD;
            }
            else
            {
                drivetrain.SetTransmission(Drivetrain.Transmissions.RWD);
                drivetrain.transmission = Drivetrain.Transmissions.RWD;

            }
        }
    }
}
