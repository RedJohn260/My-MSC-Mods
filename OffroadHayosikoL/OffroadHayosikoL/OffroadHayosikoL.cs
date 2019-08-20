using MSCLoader;
using UnityEngine;
using System.IO;
using HutongGames.PlayMaker;

namespace OffroadHayosikoL
{
    public class OffroadHayosikoL : Mod
    {
        public override string ID => "OffroadHayosikoL"; //Your mod ID (unique)
        public override string Name => "OffroadHayosikoL"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.2"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject felgaFL;
        private GameObject felgaFR;
        private GameObject felgaRL;
        private GameObject felgaRR;
        private Material felgaMat;
        private float r = 1f;
        private float g = 1f;
        private float b = 1f;
        private bool showgui;
        private string path;
        private Rect guiBox = new Rect((float)(Screen.width - 2500 / 2), 70f, 600f, 600f);
        private readonly Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.Keypad3);
        private Color matColor;
        private Color vanColor;
        public float vanr = 1f;
        public float vang = 1f;
        public float vanb = 1f;
        private GameObject VanRearDoor;
        public float hSliderValue = 0.0F;
        private GameObject switchBoxLightON;
        private GameObject SideDoorORG;
        private GameObject svijetlaOFF;
        private GameObject svijetlaON;
        private GameObject bullbarFront;
        private GameObject bullbarBack;
        private GameObject roofRack;
        private GameObject switchBox;
        private GameObject light1;
        private GameObject light2;
        private GameObject light3;
        private GameObject light4;
        private GameObject light5;
        private GameObject LightTrigger;
        private GameObject switchBoxON;
        private GameObject ljestve;
        private GameObject doorL;
        private GameObject doorR;
        private GameObject doorSide;
        private GameObject FuelHatch;
        private GameObject vanBody;
        private Material VanNewMaterial;
        private Material backDoorMat;
        private GameObject OffroadTireFL;
        private GameObject OffroadTireFR;
        private GameObject OffroadTireRL;
        private GameObject OffroadTireRR;
        private GameObject svijetlaCh;
        private AudioSource switchAudio;
        private Material vanInt;
        private Material vanVolan;
        private GameObject interior;
        private GameObject volan;
        private GameObject doorPanel1;
        private GameObject doorPanel2;
        private GameObject doorOpener1;
        private GameObject doorOpener2;
        private Material interiorMat;
        private Material volanMat;
        private Material doorPanel1Mat;
        private Material doorPanel2Mat;
        private Material doorOpener1Mat;
        private Material doorOpener2Mat;
        private bool SwitchControll;
        public float hight = 0.1f;
        private Transform whFL;
        private Transform whFR;
        private Transform whRL;
        private Transform whRR;
        private Transform w1;
        private Transform w2;
        private Transform w3;
        private Transform w4;
        private float width = 1.3f;

        public override void OnLoad()
        {
            AssetBundleLoad();
            NewGameObjects();
            Keybind.Add(this, this.openGUI);
            this.path = ModLoader.GetModAssetsFolder(this);
            Loadsettings();
           


            GameObject switchAudioR = new GameObject
            {
                name = "Rswitch_Audio"
            };
            this.switchAudio = switchAudioR.AddComponent<AudioSource>();
            this.switchAudio.transform.SetParent(switchBoxLightON.transform, false);
            this.switchAudio.transform.localPosition = Vector3.zero;
            WWW www = new WWW(string.Concat("file:///", Path.Combine(ModLoader.GetModAssetsFolder(this), "light_buttonON.wav")));
            while (!www.isDone)
            {
            }
            this.switchAudio.clip = www.GetAudioClip(true);
            this.switchAudio.loop = false;
            this.switchAudio.spatialBlend = 1f;
            this.switchAudio.volume = 1f;
            this.switchAudio.maxDistance = 1000f;
            this.switchAudio.minDistance = 12f;
            //interior
            interior = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/generalPivot/van_interior").gameObject;
            interiorMat = interior.GetComponent<MeshRenderer>().sharedMaterial;


            volan = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/Dashboard/Steering/VanSteeringPivot/steering").gameObject;
            volanMat = volan.GetComponent<MeshRenderer>().material;

            doorPanel1 = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorl/panel");
            doorPanel1Mat = doorPanel1.GetComponent<MeshRenderer>().material;

            doorOpener1 = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorl/opener/mesh");
            doorOpener1Mat = doorOpener1.GetComponent<MeshRenderer>().material;

            doorPanel2 = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorr/panel");
            doorPanel2Mat = doorPanel2.GetComponent<MeshRenderer>().material;

            doorOpener2 = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorr/opener 1");
            doorOpener2Mat = doorOpener2.GetComponent<MeshRenderer>().material;

            switchBox.transform.localPosition = new Vector3(0.145f, 0.7543734f, 1.605471f);
            switchBoxON.transform.localPosition = new Vector3(0.145f, 0.7543734f, 1.605471f);
            LightTrigger.transform.localPosition = new Vector3(0.01301997f, 0.7614903f, 1.573369f);


            vanBody.SetActive(true);
            doorL.SetActive(true);
            doorR.SetActive(true);
            doorSide.SetActive(true);
            VanRearDoor.SetActive(true);
            FuelHatch.SetActive(true);
            GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorl/door").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorr/door").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/SideDoor/door").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/RearDoor/doorear/door").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/body").GetComponent<MeshRenderer>().enabled = false;
            var fh = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/FuelFiller/Pivot/body_fuel_hatch").gameObject;
            fh.GetComponent<MeshRenderer>().enabled = false;

            interior.GetComponent<MeshRenderer>().sharedMaterial = vanInt;
            doorPanel1.GetComponent<MeshRenderer>().sharedMaterial = vanInt;
            doorPanel2.GetComponent<MeshRenderer>().sharedMaterial = vanInt;
            doorOpener1.GetComponent<MeshRenderer>().sharedMaterial = vanInt;
            doorOpener2.GetComponent<MeshRenderer>().sharedMaterial = vanInt;
            volan.GetComponent<MeshRenderer>().sharedMaterial = vanVolan;

            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(false);
            light4.SetActive(false);
            light5.SetActive(false);
            svijetlaON.SetActive(false);
            switchBoxON.SetActive(false);
            switchBoxLightON.SetActive(false);
            svijetlaOFF.SetActive(true);
            felgaFL.SetActive(true);
            felgaFR.SetActive(true);
            felgaRL.SetActive(true);
            felgaRR.SetActive(true);
            OffroadTireFL.SetActive(true);
            OffroadTireFR.SetActive(true);
            OffroadTireRL.SetActive(true);
            OffroadTireRR.SetActive(true);
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL/tire/rim").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR/tire/rim").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL/tire/rim").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR/tire/rim").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL/tire/tire").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR/tire/tire").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL/tire/tire").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR/tire/tire").GetComponent<MeshRenderer>().enabled = false;

            whFL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL").transform;
            whFR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR").transform;
            whRL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL").transform;
            whRR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR").transform;

            w1 = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL/tire").transform;
            w2 = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR/tire").transform;
            w3 = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL/tire").transform;
            w4 = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR/tire").transform;

           
            ModConsole.Print("<b><color=green>OffRoad Hayosiko Mod Loaded</color></b>");

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


        public override void Update()
        {

            if (this.openGUI.IsDown())
            {
                this.GuiShow();
            }
            NewMaterialsAndColorsUpdate();
            EnableDisableButtons();
            RayCast();
        }
        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "felge.unity3d");
            felgaFL = GameObject.Instantiate(ab.LoadAsset("zeljezna_felga.prefab")) as GameObject;
            felgaFR = GameObject.Instantiate(ab.LoadAsset("zeljezna_felga.prefab")) as GameObject;
            felgaRL = GameObject.Instantiate(ab.LoadAsset("zeljezna_felga.prefab")) as GameObject;
            felgaRR = GameObject.Instantiate(ab.LoadAsset("zeljezna_felga.prefab")) as GameObject;
            vanInt = ab.LoadAsset("vanInt.mat") as Material;
            vanVolan = ab.LoadAsset("vanVolan.mat") as Material;



            felgaMat = ab.LoadAsset("zeljezna_felgaMat.mat") as Material;

            svijetlaOFF = GameObject.Instantiate(ab.LoadAsset("svijetlaOFF.prefab")) as GameObject;
            svijetlaON = GameObject.Instantiate(ab.LoadAsset("svijetlaEmmit.prefab")) as GameObject;

            bullbarFront = GameObject.Instantiate(ab.LoadAsset("bar_front.prefab")) as GameObject;

            bullbarBack = GameObject.Instantiate(ab.LoadAsset("bar_back.prefab")) as GameObject;

            roofRack = GameObject.Instantiate(ab.LoadAsset("roof_rack.prefab")) as GameObject;

            switchBox = GameObject.Instantiate(ab.LoadAsset("switch_box.prefab")) as GameObject;

            switchBoxON = GameObject.Instantiate(ab.LoadAsset("switch_boxON.prefab")) as GameObject;
            VanRearDoor = GameObject.Instantiate(ab.LoadAsset("van_rear_door.prefab")) as GameObject;
            switchBoxLightON = GameObject.Instantiate(ab.LoadAsset("2dLight.prefab")) as GameObject;
            ljestve = GameObject.Instantiate(ab.LoadAsset("ljestve.prefab")) as GameObject;

            doorL = GameObject.Instantiate(ab.LoadAsset("door_left_new.prefab")) as GameObject;
            doorR = GameObject.Instantiate(ab.LoadAsset("door_left_new.prefab")) as GameObject;
            doorSide = GameObject.Instantiate(ab.LoadAsset("door_side_new.prefab")) as GameObject;
            vanBody = GameObject.Instantiate(ab.LoadAsset("body_new.prefab")) as GameObject;
            FuelHatch = GameObject.Instantiate(ab.LoadAsset("fuel_hatch_new.prefab")) as GameObject;
            VanNewMaterial = ab.LoadAsset("vanBody.mat") as Material;
            backDoorMat = ab.LoadAsset("backDoor.mat") as Material;
            OffroadTireFL = GameObject.Instantiate(ab.LoadAsset("tire_offroad.prefab")) as GameObject;
            OffroadTireFR = GameObject.Instantiate(ab.LoadAsset("tire_offroad.prefab")) as GameObject;
            OffroadTireRL = GameObject.Instantiate(ab.LoadAsset("tire_offroad.prefab")) as GameObject;
            OffroadTireRR = GameObject.Instantiate(ab.LoadAsset("tire_offroad.prefab")) as GameObject;
            svijetlaCh = GameObject.Instantiate(ab.LoadAsset("svijetla_OFFCh.prefab")) as GameObject;
            ab.Unload(false);
        }
        private void NewGameObjects()
        {
            //VanBody
            var Van = GameObject.Find("HAYOSIKO(1500kg, 250)");
            vanBody.transform.SetParent(Van.transform, false);
            vanBody.transform.localPosition = new Vector3(0f, 0.54f, 0f);
            vanBody.transform.localEulerAngles = new Vector3(270f, 270.0001f, 0f);
            vanBody.transform.localScale = new Vector3(1f, 1f, 1f);
            //LeftDoor
            var Ldor = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorl");
            doorL.transform.SetParent(Ldor.transform, false);
            doorL.transform.localPosition = new Vector3(0, 0, 0);
            doorL.transform.localEulerAngles = new Vector3(0, 0, 0);
            doorL.transform.localScale = new Vector3(1f, 1f, 1f);
            //RightDoor
            var Rdor = GameObject.Find("HAYOSIKO(1500kg, 250)/DriverDoors/doorr");
            doorR.transform.SetParent(Rdor.transform, false);
            doorR.transform.localPosition = new Vector3(4.768363E-07f, -7.74861E-07f, -2.235183E-08f);
            doorR.transform.localEulerAngles = new Vector3(1.350914E-06f, 180f, 7.519543E-06f);
            doorR.transform.localScale = new Vector3(-1f, -1f, -1f);
            //SideDoor
            var Sdor = SideDoorORG = GameObject.Find("HAYOSIKO(1500kg, 250)/SideDoor");
            doorSide.transform.SetParent(Sdor.transform, false);
            doorSide.transform.localPosition = new Vector3(0, 0, 0);
            doorSide.transform.localEulerAngles = new Vector3(0, 0, 0);
            doorSide.transform.localScale = new Vector3(1f, 1f, 1f);
            //Wheel FL
            var WFL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL/tire");
            felgaFL.transform.SetParent(WFL.transform, false);
            felgaFL.transform.localPosition = new Vector3(0f, 0f, 0f);
            felgaFL.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
            felgaFL.transform.localScale = new Vector3(1f, 1f, 1f);
            //Wheel FR
            var WFR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR/tire");
            felgaFR.transform.SetParent(WFR.transform, false);
            felgaFR.transform.localPosition = new Vector3(0f, 0f, 0f);
            felgaFR.transform.localEulerAngles = new Vector3(0f, 0f, 90.00001f);
            felgaFR.transform.localScale = new Vector3(1f, 1f, 1f);
            //Wheel RL
            var WRL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL/tire");
            felgaRL.transform.SetParent(WRL.transform, false);
            felgaRL.transform.localPosition = new Vector3(0f, 0f, 0f);
            felgaRL.transform.localEulerAngles = new Vector3(90f, 90f, 0f);
            felgaRL.transform.localScale = new Vector3(1f, 1f, 1f);
            //Wheel RR
            var WRR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR/tire");
            felgaRR.transform.SetParent(WRR.transform, false);
            felgaRR.transform.localPosition = new Vector3(0f, 0f, 0f);
            felgaRR.transform.localEulerAngles = new Vector3(270f, 90f, 0f);
            felgaRR.transform.localScale = new Vector3(1f, 1f, 1f);


            // OFFroadTireFL
            var OFL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFL/tire");
            OffroadTireFL.transform.SetParent(OFL.transform, false);
            OffroadTireFL.transform.localPosition = new Vector3(0f, 0f, 0f);
            OffroadTireFL.transform.localEulerAngles = new Vector3(2.85793E-06f, 180f, 89.99999f);
            OffroadTireFL.transform.localScale = new Vector3(1f, 1f, 1f);

            //OFFroadTireFR
            var OFR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelFR/tire");
            OffroadTireFR.transform.SetParent(OFR.transform, false);
            OffroadTireFR.transform.localPosition = new Vector3(0f, 0f, 0f);
            OffroadTireFR.transform.localEulerAngles = new Vector3(0f, 0f, 90.00001f);
            OffroadTireFR.transform.localScale = new Vector3(1f, 1f, 1f);

            //OFFroadTireRL
            var ORL = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRL/tire");
            OffroadTireRL.transform.SetParent(ORL.transform, false);
            OffroadTireRL.transform.localPosition = new Vector3(0f, 0f, 0f);
            OffroadTireRL.transform.localEulerAngles = new Vector3(90f, 90f, 0f);
            OffroadTireRL.transform.localScale = new Vector3(1f, 1f, 1f);

            //OFFroadTireRR
            var ORR = GameObject.Find("HAYOSIKO(1500kg, 250)/wheelRR/tire");
            OffroadTireRR.transform.SetParent(ORR.transform, false);
            OffroadTireRR.transform.localPosition = new Vector3(0f, 0f, 0f);
            OffroadTireRR.transform.localEulerAngles = new Vector3(270f, 270.0002f, 180.00001f);
            OffroadTireRR.transform.localScale = new Vector3(1f, 1f, 1f);
            //Van Rear Door
            var dorR = GameObject.Find("HAYOSIKO(1500kg, 250)/RearDoor/doorear");
            VanRearDoor.transform.SetParent(dorR.transform, false);
            VanRearDoor.transform.localPosition = new Vector3(0f, 0f, 0f);
            VanRearDoor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            VanRearDoor.transform.localScale = new Vector3(1f, 1f, 1f);
            //Svijetla OFF Bar
            var HAYOSIKO = GameObject.Find("HAYOSIKO(1500kg, 250)");
            svijetlaOFF.transform.SetParent(HAYOSIKO.transform, false);
            svijetlaOFF.transform.localScale = new Vector3(1, 1, 1);
            svijetlaOFF.transform.localPosition = new Vector3(0, 1.65f, 1.23f);
            svijetlaOFF.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Svijetla OFF svijetla
            svijetlaCh.transform.SetParent(svijetlaOFF.transform, false);
            svijetlaCh.transform.localScale = new Vector3(1, 1, 1);
            svijetlaCh.transform.localPosition = new Vector3(0, 0, 0);
            svijetlaCh.transform.localEulerAngles = new Vector3(0, 0, 0);
            //FuelHatch
            var fp = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/FuelFiller/Pivot").gameObject;

            //var ft = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.Find("LOD/FuelFiller/Pivot/body_fuel_hatch/Trigger").gameObject;
            FuelHatch.transform.SetParent(fp.transform, false);
            //ft.transform.SetParent(FuelHatch.transform, false);
            FuelHatch.transform.localPosition = new Vector3(-0.09740014f, 0.00799945f, 0f);
            FuelHatch.transform.localEulerAngles = new Vector3(0, 0, 0);
            FuelHatch.transform.localScale = new Vector3(1, 1, 1);
            //Svijetla ON State
            svijetlaON.transform.SetParent(HAYOSIKO.transform, false);
            svijetlaON.transform.localScale = new Vector3(1, 1, 1);
            svijetlaON.transform.localPosition = new Vector3(0, 1.65f, 1.23f);
            svijetlaON.transform.localEulerAngles = new Vector3(0, 0, 0);
            //FrontBullbar
            bullbarFront.transform.SetParent(HAYOSIKO.transform, false);
            bullbarFront.transform.localScale = new Vector3(1, 1, 1);
            bullbarFront.transform.localPosition = new Vector3(0, 0.425095f, 1.99366f);
            bullbarFront.transform.localEulerAngles = new Vector3(0, 0, 0);
            //BackBullbar
            bullbarBack.transform.SetParent(HAYOSIKO.transform, false);
            bullbarBack.transform.localScale = new Vector3(1, 1, 1);
            bullbarBack.transform.localPosition = new Vector3(0, 0.222748f, -2.31215f);
            bullbarBack.transform.localEulerAngles = new Vector3(0, 0, 0);
            //RoofRack
            roofRack.transform.SetParent(HAYOSIKO.transform, false);
            roofRack.transform.localScale = new Vector3(1, 1, 1);
            roofRack.transform.localPosition = new Vector3(0, 1.71754f, -0.285868f);
            roofRack.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Ladders
            ljestve.transform.SetParent(HAYOSIKO.transform, false);
            ljestve.transform.localScale = new Vector3(1, 1, 1);
            ljestve.transform.localPosition = new Vector3(0f, 0f, 0f);
            ljestve.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Switchbox OFF State
            switchBox.transform.SetParent(HAYOSIKO.transform, false);
            switchBox.transform.localScale = new Vector3(1, 1, 1);
            switchBox.transform.localPosition = new Vector3(0, 0.996373f, 1.60747f);
            switchBox.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Switchbox ON State
            switchBoxON.transform.SetParent(HAYOSIKO.transform, false);
            switchBoxON.transform.localScale = new Vector3(1, 1, 1);
            switchBoxON.transform.localPosition = new Vector3(0, 0.996373f, 1.60747f);
            switchBoxON.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Switchbox 2D Light ON State
            switchBoxLightON.transform.SetParent(switchBoxON.transform, false);
            switchBoxLightON.transform.localScale = new Vector3(1, 1, 1);
            switchBoxLightON.transform.localPosition = new Vector3(0f, 0f, 0f);
            switchBoxLightON.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Not Rendered LightTrigger Cube
            LightTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
            LightTrigger.transform.parent = HAYOSIKO.transform;
            LightTrigger.transform.localScale = new Vector3(0.030f, 0.030f, 0.030f);
            LightTrigger.transform.localPosition = new Vector3(-0.13298f, 1.00449f, 1.57737f);
            LightTrigger.transform.localEulerAngles = new Vector3(0, 0, 0);
            LightTrigger.name = "RJ260";
            var collision1 = LightTrigger.GetComponent<Collider>();
            collision1.isTrigger = true;
            LightTrigger.GetComponent<MeshRenderer>().enabled = false;
            //Lights
            light1 = new GameObject("NewLight1");
            Light lightComp1 = light1.AddComponent<Light>();
            lightComp1.color = Color.white;
            lightComp1.type = LightType.Spot;
            lightComp1.spotAngle = 50;
            lightComp1.intensity = 60;
            lightComp1.cullingMask = 3;
            lightComp1.range = 200;
            light1.transform.parent = HAYOSIKO.transform;
            light1.transform.localPosition = new Vector3(0.001126f, 1.70592f, 1.28036f);
            light1.transform.localEulerAngles = new Vector3(0, 0, 0);
            light2 = new GameObject("NewLight2");
            Light lightComp2 = light2.AddComponent<Light>();
            lightComp2.color = Color.white;
            lightComp2.type = LightType.Point;
            lightComp2.intensity = 10;
            lightComp2.range = 0.5f;
            light2.transform.parent = HAYOSIKO.transform;
            light2.transform.localPosition = new Vector3(0.440265f, 1.70187f, 1.34423f);
            light2.transform.localEulerAngles = new Vector3(0, 0, 0);
            light3 = new GameObject("NewLight3");
            Light lightComp3 = light3.AddComponent<Light>();
            lightComp3.color = Color.white;
            lightComp3.type = LightType.Point;
            lightComp3.intensity = 10;
            lightComp3.range = 0.5f;
            light3.transform.parent = HAYOSIKO.transform;
            light3.transform.localPosition = new Vector3(0.150798f, 1.70187f, 1.34423f);
            light3.transform.localEulerAngles = new Vector3(0, 0, 0);
            light4 = new GameObject("NewLight4");
            Light lightComp4 = light4.AddComponent<Light>();
            lightComp4.color = Color.white;
            lightComp4.type = LightType.Point;
            lightComp4.intensity = 10;
            lightComp4.range = 0.5f;
            light4.transform.parent = HAYOSIKO.transform;
            light4.transform.localPosition = new Vector3(-0.146389f, 1.70187f, 1.34423f);
            light4.transform.localEulerAngles = new Vector3(0, 0, 0);
            light5 = new GameObject("NewLight5");
            Light lightComp5 = light5.AddComponent<Light>();
            lightComp5.color = Color.white;
            lightComp5.type = LightType.Point;
            lightComp5.intensity = 10;
            lightComp5.range = 0.5f;
            light5.transform.parent = HAYOSIKO.transform;
            light5.transform.localPosition = new Vector3(-0.431996f, 1.70187f, 1.34423f);
            light5.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        private void GuiShow()
        {
            this.showgui = !this.showgui;
            if (this.showgui)
            {
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = true;
                return;
            }
            FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
        }
        public override void OnGUI()
        {
            if (this.showgui)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, this.guiBox, new GUI.WindowFunction(this.Window), "Offroad Hayosiko by RedJohn260");
            }
        }
        private void Window(int windowid)
        {
            //Wheel Color
            //-----------------------------------------------------------------------------------
            // R With Slider
            GUI.Label(new Rect(75f, 30f, 150f, 20f), "Wheel Color");
            GUI.Label(new Rect(10f, 50f, 10f, 20f), "R");
            r = GUI.HorizontalSlider(new Rect(30f, 55f, 220f, 20f), r, 0, 1);
            //G With Slider
            GUI.Label(new Rect(10f, 80f, 10f, 20f), "G");
            g = GUI.HorizontalSlider(new Rect(30f, 85f, 220f, 20f), g, 0, 1);
            //B With Slider
            GUI.Label(new Rect(10f, 110f, 10f, 20f), "B");
            b = GUI.HorizontalSlider(new Rect(30f, 115f, 220f, 20f), b, 0, 1);

            //-----------------------------------------------------------------------------------

            //           Van Color

            // R With Slider
            GUI.Label(new Rect(375f, 30f, 150f, 20f), "Van Color");
            GUI.Label(new Rect(310f, 50f, 10f, 20f), "R");
            vanr = GUI.HorizontalSlider(new Rect(330f, 55f, 250f, 20f), vanr, 0, 1);
            //G With Slider
            GUI.Label(new Rect(310f, 80f, 10f, 20f), "G");
            vang = GUI.HorizontalSlider(new Rect(330f, 85f, 250f, 20f), vang, 0, 1);
            //B With Slider
            GUI.Label(new Rect(310f, 110f, 10f, 20f), "B");
            vanb = GUI.HorizontalSlider(new Rect(330f, 115f, 250f, 20f), vanb, 0, 1);
            //--------------------------------------------------------------------------------------

            // VAn Hight
            GUI.Label(new Rect(230f, 440f, 350f, 25f), "Van Wheel Hight");
            hight = GUI.HorizontalSlider(new Rect(150f, 470f, 250f, 20f), hight, 0.1f, -0.1f);
            GUI.Label(new Rect(230f, 490f, 380f, 25f), "Van Wheel Width");
            width = GUI.HorizontalSlider(new Rect(150f, 520f, 250f, 20f), width, 1f, 1.5f);



            if (GUI.Button(new Rect(355f, 390f, 150f, 40f), "Close"))
            {
                this.showgui = !this.showgui;
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }
            if (GUI.Button(new Rect(230f, 240f, 100f, 40f), "Save"))
            {
                Savesettings();
               
                ModConsole.Print("<b><color=green>OffRoad Hayosiko Mod Settings Saved</color></b>");
            }
            if (GUI.Button(new Rect(230f, 290f, 100f, 40f), "Load"))
            {
                Loadsettings();
               
                ModConsole.Print("<b><color=green>OffRoad Hayosiko Mod Settings Loaded</color></b>");
            }

            GUI.DragWindow();
        }
        private void RayCast()
        {

           
            
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.collider.name == "RJ260")
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                SwitchControll = !SwitchControll;
                                switchAudio.Play();
                            }
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Roof Lights";
                            break;
                        }
                    }
                }
            
        }
        private void Savesettings()
        {
            string[] str = new string[8];
            int num = 0;
            float StoreRedB = this.r;
            str[num] = StoreRedB.ToString();
            int num1 = 1;
            float StoreGreenB = this.g;
            str[num1] = StoreGreenB.ToString();
            int num2 = 2;
            float StoreBlueB = this.b;
            str[num2] = StoreBlueB.ToString();
            int num3 = 3;
            float StoreRedN = this.vanr;
            str[num3] = StoreRedN.ToString();
            int num4 = 4;
            float StoreGreenN = this.vang;
            str[num4] = StoreGreenN.ToString();
            int num5 = 5;
            float StoreBlueN = this.vanb;
            str[num5] = StoreBlueN.ToString();
            int num6 = 6;
            float StoreWheelH = this.hight;
            str[num6] = StoreWheelH.ToString();
            int num7 = 7;
            float StoreWheelW = this.width;
            str[num7] = StoreWheelW.ToString();

            File.WriteAllLines(string.Concat(this.path, "/HayosikoOffroadColor.txt"), str);
        }
        private void Loadsettings()
        {
            string[] strArrays = new string[8];

            strArrays = File.ReadAllLines(string.Concat(this.path, "/HayosikoOffroadColor.txt"));
            this.r = float.Parse(strArrays[0]);
            this.g = float.Parse(strArrays[1]);
            this.b = float.Parse(strArrays[2]);
            this.vanr = float.Parse(strArrays[3]);
            this.vang = float.Parse(strArrays[4]);
            this.vanb = float.Parse(strArrays[5]);
            this.hight = float.Parse(strArrays[6]);
            this.width = float.Parse(strArrays[7]);
        }
        private void EnableDisableButtons()
        {
            if (SwitchControll)
            {
                svijetlaON.SetActive(true);
                svijetlaOFF.SetActive(true);
                switchBox.SetActive(false);
                switchBoxON.SetActive(true);
                switchBoxLightON.SetActive(true);
                light1.SetActive(true);
                light2.SetActive(true);
                light3.SetActive(true);
                light4.SetActive(true);
                light5.SetActive(true);

            }
            else 
            {
                svijetlaON.SetActive(false);
                svijetlaOFF.SetActive(true);
                switchBox.SetActive(true);
                switchBoxON.SetActive(false);
                switchBoxLightON.SetActive(false);

                light1.SetActive(false);
                light2.SetActive(false);
                light3.SetActive(false);
                light4.SetActive(false);
                light5.SetActive(false);

            }
        }
        private void NewMaterialsAndColorsUpdate()
        {
            VanNewMaterial.SetColor("_Color", vanColor);
            felgaMat.SetColor("_Color", matColor);
            backDoorMat.SetColor("_Color", vanColor);
            matColor = new Color(r, g, b);
            vanColor = new Color(vanr, vang, vanb);

            Vector3 localPosition = whFL.localPosition;
            localPosition.y = hight;
            whFL.localPosition = localPosition;

            Vector3 localPosition1 = whFR.localPosition;
            localPosition1.y = hight;
            whFR.localPosition = localPosition1;

            Vector3 localPosition2 = whRL.localPosition;
            localPosition2.y = hight;
            whRL.localPosition = localPosition2;

            Vector3 localPosition3 = whRR.localPosition;
            localPosition3.y = hight;
            whRR.localPosition = localPosition3;
            Vector3 localScale1 = w1.localScale;
            localScale1.x = width;
            w1.localScale = localScale1;
            Vector3 localScale2 = w2.localScale;
            localScale2.x = width;
            w2.localScale = localScale2;
            Vector3 localScale3 = w3.localScale;
            localScale3.x = width;
            w3.localScale = localScale3;
            Vector3 localScale4 = w4.localScale;
            localScale4.x = width;
            w4.localScale = localScale4;
        }
    }
}
