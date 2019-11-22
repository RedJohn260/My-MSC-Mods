using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;


namespace KekmetAddons
{
    public class KekmetAddons : Mod
    {
        public override string ID => "KekmetAddons"; //Your mod ID (unique)
        public override string Name => "KekmetAddons"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.3"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        //Deffinition
        public AssetBundle ab;
        private GameObject nosaci;
        private GameObject kontrolkaOFF;
        private GameObject kontrolkaON;
        private GameObject prekidacOFF;
        private GameObject prekidacON;
        private GameObject svijetlaFront;
        private GameObject svijetlaBack;
        private GameObject pozicija;
        private GameObject workLight;
        private GameObject reflektor;
        private GameObject zice;
        public GameObject Bl;
        public GameObject Br;
        private GameObject kocnica;
        private GameObject lightzFL;
        private GameObject lightzRL;
        private GameObject lightzFR;
        private GameObject lightzRR;
        private GameObject lightBLR;
        private GameObject lightBRR;
        private GameObject lightDRL;
        private GameObject lightDFL;
        private GameObject lightDFR;
        private GameObject lightDRR;
        private GameObject lightW1;
        private GameObject lightWP;
        private GameObject LightTrigger;
        private readonly float lightPower = 2.0f;
        private readonly float lightDomet = 1.0f;
        private Color red = Color.red;
        private Color yellow = new Color(1f, 0.705f, 0.301f);
        private Color white = Color.white;
        private PlayMakerFSM component1;
        private FsmInt fsmLightKnob;
        private int fsmValue;
        private PlayMakerFSM component2;
        private FsmFloat fsmBrakes;
        private float fsmBrakesValue;
        private PlayMakerFSM component3;
        private FsmBool fsmIgnition;
        private PlayMakerFSM component4;
        private FsmInt reverseTrigger;
        private int FsmReverseValue;
        private bool fsmIgnitionValue;
        private bool switchControll;
        private GameObject worklightEmmit;
        private GameObject BswitchLight;
        private GameObject BswitchOFF;
        private GameObject BswitchONleft;
        private GameObject BswitchONright;
        private GameObject BlinkerTrigger;
        private GameObject BlinkerAudio1;
        private GameObject BlinkerAudio2;
        private GameObject BlinkerSwitchAudioOFF;
        private GameObject BlinkerSwitchAudioON;
        private AudioSource BS1;
        private AudioSource BS2;
        private AudioClip bs1;
        private AudioClip bs2;
        private AudioSource BSL;
        private readonly Keybind Hazzard = new Keybind("HazzardLights", "HazzardLights", KeyCode.Keypad5);
        private readonly Keybind TurnSignalLeft = new Keybind("TurnSignalLeft", "TurnSignalLeft", KeyCode.Keypad4);
        private readonly Keybind TurnSignalRight = new Keybind("TurnSignalRight ", "TurnSignalRight ", KeyCode.Keypad6);
        private bool TslPressed;
        private bool TsrPressed;
        private Rect guiBox = new Rect((float)(Screen.width - 2150 / 2), 70f, 600f,300f);
        private readonly Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.Keypad8);
        private bool showgui;
        private bool FrontLoader;
        private GameObject tractorFL;
        private string path;
        private GameObject zice2;
        private GameObject intLight;
        private GameObject intLightEmmit;
        private GameObject intPrekidacOFF;
        private GameObject intPrekidacON;
        private GameObject zvucnik;
        private bool IntSwitchEnable;
        private GameObject intLightTrigger;
        private GameObject intLightLight;
        private static Drivetrain drivetrain;
        private static GameObject FL;


        public override void OnLoad()
        {
            AssetBundleLoad();
            GameObjectNew();
            lightzFL.SetActive(false);
            lightzRL.SetActive(false);
            lightzFR.SetActive(false);
            lightzRR.SetActive(false);
            lightBLR.SetActive(false);
            lightBRR.SetActive(false);
            lightDRL.SetActive(false);
            lightDFL.SetActive(false);
            lightDFR.SetActive(false);
            lightDRR.SetActive(false);
            lightW1.SetActive(false);
            lightWP.SetActive(false);
            BswitchOFF.SetActive(true);
            BswitchONleft.SetActive(false);
            BswitchONright.SetActive(false);
            Keybind.Add(this, Hazzard);
            Keybind.Add(this, TurnSignalLeft);
            Keybind.Add(this, TurnSignalRight);
            Keybind.Add(this, openGUI);
            tractorFL = GameObject.Find("KEKMET(350-400psi)/Frontloader");
            FL = GameObject.Find("KEKMET(350-400psi)/Frontloader");
            path = ModLoader.GetModAssetsFolder(this);
            drivetrain = GameObject.Find("KEKMET(350-400psi)").GetComponent<Drivetrain>();
            GearsLoad();
            FLLoadSettings();
        }
        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "kekaddons.unity3d");
            nosaci = GameObject.Instantiate(ab.LoadAsset("drzaci.prefab")) as GameObject;
            kontrolkaOFF = GameObject.Instantiate(ab.LoadAsset("kontrolka.prefab")) as GameObject;
            kontrolkaON = GameObject.Instantiate(ab.LoadAsset("kontrolkaEmmit.prefab")) as GameObject;
            prekidacOFF = GameObject.Instantiate(ab.LoadAsset("prekidacOFF.prefab")) as GameObject;
            prekidacON = GameObject.Instantiate(ab.LoadAsset("prekidacON.prefab")) as GameObject;
            svijetlaBack = GameObject.Instantiate(ab.LoadAsset("svijetlaB.prefab")) as GameObject;
            svijetlaFront = GameObject.Instantiate(ab.LoadAsset("svijetlaF.prefab")) as GameObject;
            kocnica = GameObject.Instantiate(ab.LoadAsset("svijetlaKocnica.prefab")) as GameObject;
            pozicija = GameObject.Instantiate(ab.LoadAsset("svijetlaPozicija.prefab")) as GameObject;
            workLight = GameObject.Instantiate(ab.LoadAsset("workLight.prefab")) as GameObject;
            reflektor = GameObject.Instantiate(ab.LoadAsset("workLightReflektor.prefab")) as GameObject;
            zice = GameObject.Instantiate(ab.LoadAsset("zice.prefab")) as GameObject;
            Bl = GameObject.Instantiate(ab.LoadAsset("Bl.prefab")) as GameObject;
            Br = GameObject.Instantiate(ab.LoadAsset("Br.prefab")) as GameObject;
            worklightEmmit = GameObject.Instantiate(ab.LoadAsset("worklightEmmit.prefab")) as GameObject;
            BswitchLight = GameObject.Instantiate(ab.LoadAsset("SblinkerKontrollLight.prefab")) as GameObject;
            BswitchOFF = GameObject.Instantiate(ab.LoadAsset("SblinkerOFF.prefab")) as GameObject;
            BswitchONleft = GameObject.Instantiate(ab.LoadAsset("SblinkerONLeft.prefab")) as GameObject;
            BswitchONright = GameObject.Instantiate(ab.LoadAsset("SblinkerONRight.prefab")) as GameObject;
            BlinkerAudio1 = GameObject.Instantiate(ab.LoadAsset("BlinkerSound1.prefab")) as GameObject;
            BlinkerAudio2 = GameObject.Instantiate(ab.LoadAsset("BlinkerSound2.prefab")) as GameObject;
            BlinkerSwitchAudioOFF = GameObject.Instantiate(ab.LoadAsset("BlinkerSwitchSoundOFF.prefab")) as GameObject;
            BlinkerSwitchAudioON = GameObject.Instantiate(ab.LoadAsset("BlinkerSwitchSoundON.prefab")) as GameObject;
            zice2 = GameObject.Instantiate(ab.LoadAsset("zice2.prefab")) as GameObject;
            intLight = GameObject.Instantiate(ab.LoadAsset("intLight.prefab")) as GameObject;
            intLightEmmit = GameObject.Instantiate(ab.LoadAsset("intLightEmmit.prefab")) as GameObject;
            intPrekidacOFF = GameObject.Instantiate(ab.LoadAsset("intPrekidacOFF.prefab")) as GameObject;
            intPrekidacON = GameObject.Instantiate(ab.LoadAsset("intPrekidacON.prefab")) as GameObject;
            zvucnik= GameObject.Instantiate(ab.LoadAsset("zvucnik.prefab")) as GameObject;
            ab.Unload(false);
        }

        private void GameObjectNew()
        {
            //Nosaci
            var kk = GameObject.Find("KEKMET(350-400psi)");

            Drivetrain drivetrain = kk.GetComponent<Drivetrain>();
            drivetrain.SetTransmission(Drivetrain.Transmissions.AWD);
            drivetrain.transmission = Drivetrain.Transmissions.AWD;

            nosaci.transform.SetParent(kk.transform, false);
            nosaci.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            nosaci.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            nosaci.transform.localScale = new Vector3(1f, 1f, 1f);
            //KontrolkaOFF
            kontrolkaOFF.transform.SetParent(kk.transform, false);
            kontrolkaOFF.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            kontrolkaOFF.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            kontrolkaOFF.transform.localScale = new Vector3(1f, 1f, 1f);
            //KontrolkaON
            kontrolkaON.transform.SetParent(kk.transform, false);
            kontrolkaON.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            kontrolkaON.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            kontrolkaON.transform.localScale = new Vector3(1f, 1f, 1f);
            kontrolkaON.SetActive(false);
            //prekidacOFF
            prekidacOFF.transform.SetParent(kk.transform, false);
            prekidacOFF.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            prekidacOFF.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            prekidacOFF.transform.localScale = new Vector3(1f, 1f, 1f);
            //prekidacON
            prekidacON.transform.SetParent(kk.transform, false);
            prekidacON.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            prekidacON.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            prekidacON.transform.localScale = new Vector3(1f, 1f, 1f);
            prekidacON.SetActive(false);
            //SvijetlaFront
            svijetlaFront.transform.SetParent(kk.transform, false);
            svijetlaFront.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            svijetlaFront.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            svijetlaFront.transform.localScale = new Vector3(1f, 1f, 1f);
            //svijetlaBack
            svijetlaBack.transform.SetParent(kk.transform, false);
            svijetlaBack.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            svijetlaBack.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            svijetlaBack.transform.localScale = new Vector3(1f, 1f, 1f);
            //Kocnica
            kocnica.transform.SetParent(kk.transform, false);
            kocnica.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            kocnica.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            kocnica.transform.localScale = new Vector3(1f, 1f, 1f);
            kocnica.SetActive(false);
            //pozicija
            pozicija.transform.SetParent(kk.transform, false);
            pozicija.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            pozicija.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            pozicija.transform.localScale = new Vector3(1f, 1f, 1f);
            pozicija.SetActive(false);
            //WorkLight
            workLight.transform.SetParent(kk.transform, false);
            workLight.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            workLight.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            workLight.transform.localScale = new Vector3(1f, 1f, 1f);
            //WorkLight Emmit
            worklightEmmit.transform.SetParent(kk.transform, false);
            worklightEmmit.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            worklightEmmit.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            worklightEmmit.transform.localScale = new Vector3(1f, 1f, 1f);
            worklightEmmit.SetActive(false);
            //WorkLightReflektor
            reflektor.transform.SetParent(kk.transform, false);
            reflektor.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            reflektor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            reflektor.transform.localScale = new Vector3(1f, 1f, 1f);
            //zice
            zice.transform.SetParent(kk.transform, false);
            zice.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            zice.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            zice.transform.localScale = new Vector3(1f, 1f, 1f);
            //Zmigavci Left
            Bl.transform.SetParent(kk.transform, false);
            Bl.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            Bl.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            Bl.transform.localScale = new Vector3(1f, 1f, 1f);
            Bl.SetActive(false);
            //Zmigavci Right
            Br.transform.SetParent(kk.transform, false);
            Br.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            Br.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            Br.transform.localScale = new Vector3(1f, 1f, 1f);
            Br.SetActive(false);
            //lightTrigger
            LightTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
            LightTrigger.transform.SetParent(kk.transform, false);
            LightTrigger.transform.localScale = new Vector3(0.030f, 0.030f, 0.030f);
            LightTrigger.transform.localPosition = new Vector3(0.03199993f, 1.895f, -0.1f);
            LightTrigger.transform.localEulerAngles = new Vector3(0, 0, 0);
            LightTrigger.name = "WorkLightTrigger";
            var collision1 = LightTrigger.GetComponent<Collider>();
            collision1.isTrigger = true;
            LightTrigger.GetComponent<MeshRenderer>().enabled = false;
            //LightZmigavac FL
            lightzFL = new GameObject("zmLightFL");
            Light lightComp1 = lightzFL.AddComponent<Light>();
            lightComp1.type = LightType.Point;
            lightComp1.intensity = lightPower;
            lightComp1.range = lightDomet;
            lightComp1.color = yellow;
            lightzFL.transform.SetParent(kk.transform, false);
            lightzFL.transform.localPosition = new Vector3(-0.8499999f, 1.12f, -0.7200001f);
            //Light Zmigavac RL
            lightzRL = new GameObject("zmLightRL");
            Light lightComp2 = lightzRL.AddComponent<Light>();
            lightComp2.type = LightType.Point;
            lightComp2.intensity = lightPower;
            lightComp2.range = lightDomet;
            lightComp2.color = yellow;
            lightzRL.transform.SetParent(kk.transform, false);
            lightzRL.transform.localPosition = new Vector3(-0.8499999f, 1.12f, -1.54f);
            //Light Zmigavac FR
            lightzFR = new GameObject("zmLightFR");
            Light lightComp3 = lightzFR.AddComponent<Light>();
            lightComp3.type = LightType.Point;
            lightComp3.intensity = lightPower;
            lightComp3.range = lightDomet;
            lightComp3.color = yellow;
            lightzFR.transform.SetParent(kk.transform, false);
            lightzFR.transform.localPosition = new Vector3(0.8499998f, 1.12f, -0.72f);
            //Light Zmigavac RR
            lightzRR = new GameObject("zmLightRR");
            Light lightComp4 = lightzRR.AddComponent<Light>();
            lightComp4.type = LightType.Point;
            lightComp4.intensity = lightPower;
            lightComp4.range = lightDomet;
            lightComp4.color = yellow;
            lightzRR.transform.SetParent(kk.transform, false);
            lightzRR.transform.localPosition = new Vector3(0.8499998f, 1.12f, -1.519f);
            //Light Brake Light RL
            lightBLR = new GameObject("zmLightBLR");
            Light lightComp5 = lightBLR.AddComponent<Light>();
            lightComp5.type = LightType.Point;
            lightComp5.intensity = lightPower;
            lightComp5.range = lightDomet;
            lightComp5.color = red;
            lightBLR.transform.SetParent(kk.transform, false);
            lightBLR.transform.localPosition = new Vector3(-0.77f, 1.12f, -1.52f);
            //Light Brake Light RR
            lightBRR = new GameObject("zmLightBRR");
            Light lightComp6 = lightBRR.AddComponent<Light>();
            lightComp6.type = LightType.Point;
            lightComp6.intensity = lightPower;
            lightComp6.range = lightDomet;
            lightComp6.color = red;
            lightBRR.transform.SetParent(kk.transform, false);
            lightBRR.transform.localPosition = new Vector3(0.7630009f, 1.12f, -1.519f);
            //Light Pozicija FL
            lightDFL= new GameObject("zmLightDFL");
            Light lightComp7 = lightDFL.AddComponent<Light>();
            lightComp7.type = LightType.Point;
            lightComp7.intensity = lightPower;
            lightComp7.range = lightDomet;
            lightComp7.color = white;
            lightDFL.transform.SetParent(kk.transform, false);
            lightDFL.transform.localPosition = new Vector3(-0.77f, 1.12f, -0.72f);
            //Light Pozicija FR
            lightDFR = new GameObject("zmLightDFR");
            Light lightComp8 = lightDFR.AddComponent<Light>();
            lightComp8.type = LightType.Point;
            lightComp8.intensity = lightPower;
            lightComp8.range = lightDomet;
            lightComp8.color = white;
            lightDFR.transform.SetParent(kk.transform, false);
            lightDFR.transform.localPosition = new Vector3(0.7599999f, 1.12f, -0.72f);
            //Light Pozicija RL
            lightDRL = new GameObject("zmLightDRL");
            Light lightComp9 = lightDRL.AddComponent<Light>();
            lightComp9.type = LightType.Point;
            lightComp9.intensity = lightPower;
            lightComp9.color = red;
            lightComp9.range = lightDomet;
            lightDRL.transform.SetParent(kk.transform, false);
            lightDRL.transform.localPosition = new Vector3(-0.77f, 1.12f, -1.52f);
            //Light Pozicija RR
            lightDRR = new GameObject("zmLightDRR");
            Light lightComp10 = lightDRR.AddComponent<Light>();
            lightComp10.type = LightType.Point;
            lightComp10.intensity = lightPower;
            lightComp10.color = red;
            lightComp10.range = lightDomet;
            lightDRR.transform.SetParent(kk.transform, false);
            lightDRR.transform.localPosition = new Vector3(0.7630009f, 1.12f, -1.519f);
            //Spot Light WorkLight
            lightW1 = new GameObject("zmLightW1");
            Light lightComp11 = lightW1.AddComponent<Light>();
            lightComp11.color = Color.white;
            lightComp11.type = LightType.Spot;
            lightComp11.spotAngle = 80;
            lightComp11.intensity = 25;
            lightComp11.range = 15;
            lightComp11.cullingMask = 3;
            lightW1.transform.SetParent(kk.transform, false);
            lightW1.transform.localPosition = new Vector3(0.6500002f, 0.8900003f, -1.45f);
            lightW1.transform.localEulerAngles = new Vector3(350, 190, 0);
            //Point light Worklight
            lightWP = new GameObject("pLightW0");
            Light lightComp12 = lightWP.AddComponent<Light>();
            lightComp12.color = Color.white;
            lightComp12.type = LightType.Point;
            lightComp12.intensity = 3.0f;
            lightComp12.range = 2.0f;
            lightWP.transform.SetParent(kk.transform, false);
            lightWP.transform.localPosition = new Vector3(0.6100001f, 0.8356657f, -1.99f);
            //Blinker Trigger
            BlinkerTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BlinkerTrigger.transform.SetParent(kk.transform, false);
            BlinkerTrigger.transform.localScale = new Vector3(0.030f, 0.030f, 0.030f);
            BlinkerTrigger.transform.localPosition = new Vector3(-0.06800008f, 1.185f, -0.144f);
            BlinkerTrigger.transform.localEulerAngles = new Vector3(324.0007f, 0f, 0f);
            BlinkerTrigger.name = "BlinkerTrigger";
            var collision2 = BlinkerTrigger.GetComponent<Collider>();
            collision2.isTrigger = true;
            BlinkerTrigger.GetComponent<MeshRenderer>().enabled = false;
            //Blinker Switch light
            BswitchLight.transform.SetParent(kk.transform, false);
            BswitchLight.transform.localScale = new Vector3(0, 0, 0);
            BswitchLight.transform.localPosition = new Vector3(0, 0, 0);
            BswitchLight.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Blinker switch OFF
            BswitchOFF.transform.SetParent(kk.transform, false);
            BswitchOFF.transform.localScale = new Vector3(1, 1, 1);
            BswitchOFF.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            BswitchOFF.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Blinker Switch ON Left
            BswitchONleft.transform.SetParent(kk.transform, false);
            BswitchONleft.transform.localScale = new Vector3(1, 1, 1);
            BswitchONleft.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            BswitchONleft.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Blinker Switch ON Right
            BswitchONright.transform.SetParent(kk.transform, false);
            BswitchONright.transform.localScale = new Vector3(1, 1, 1);
            BswitchONright.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            BswitchONright.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Audio
            BlinkerAudio1.transform.SetParent(kk.transform, false);
            BlinkerAudio2.transform.SetParent(kk.transform, false);
            BlinkerSwitchAudioOFF.transform.SetParent(kk.transform, false);
            BlinkerSwitchAudioON.transform.SetParent(kk.transform, false);
            BS1 = BlinkerAudio1.GetComponent<AudioSource>();
            BS2 = BlinkerAudio2.GetComponent<AudioSource>();
            bs1 = BlinkerAudio1.GetComponent<AudioSource>().clip;
            bs2 = BlinkerAudio2.GetComponent<AudioSource>().clip;
            BS1.loop = false;
            BSL = BlinkerSwitchAudioOFF.GetComponent<AudioSource>();
            //zice 2
            zice2.transform.SetParent(kk.transform, false);
            zice2.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            zice2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            zice2.transform.localScale = new Vector3(1f, 1f, 1f);
            //interior light
            intLight.transform.SetParent(kk.transform, false);
            intLight.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            intLight.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            intLight.transform.localScale = new Vector3(1f, 1f, 1f);
            //interior light ON
            intLightEmmit.transform.SetParent(kk.transform, false);
            intLightEmmit.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            intLightEmmit.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            intLightEmmit.transform.localScale = new Vector3(1f, 1f, 1f);
            intLightEmmit.SetActive(false);
            //interior light Light
            intLightLight = new GameObject("IntLightLight");
            Light lightComp13 = intLightLight.AddComponent<Light>(); 
            lightComp13.type = LightType.Point;
            lightComp13.intensity = 3.0f;
            lightComp13.range = 2.0f;
            lightComp13.color = Color.white;
            intLightLight.transform.SetParent(intLightEmmit.transform, false);
            intLightLight.transform.localPosition = new Vector3(-0.4f, 0.9756658f, -0.8000001f);
            //interior light switch OFF
            intPrekidacOFF.transform.SetParent(kk.transform, false);
            intPrekidacOFF.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            intPrekidacOFF.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            intPrekidacOFF.transform.localScale = new Vector3(1f, 1f, 1f);
            //interior Light Switch ON
            intPrekidacON.transform.SetParent(kk.transform, false);
            intPrekidacON.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            intPrekidacON.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            intPrekidacON.transform.localScale = new Vector3(1f, 1f, 1f);
            intPrekidacON.SetActive(false);
            //interior Light Switc Trigger 
            intLightTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
            intLightTrigger.transform.SetParent(kk.transform, false);
            intLightTrigger.transform.localScale = new Vector3(0.030f, 0.030f, 0.030f);
            intLightTrigger.transform.localPosition = new Vector3(- 0.54f, 1.870001f, -1.007f);
            intLightTrigger.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            intLightTrigger.name = "IntLightTrigger";
            var collision3 = BlinkerTrigger.GetComponent<Collider>();
            collision3.isTrigger = true;
            intLightTrigger.GetComponent<MeshRenderer>().enabled = false;
            //Speaker
            zvucnik.transform.SetParent(kk.transform, false);
            zvucnik.transform.localPosition = new Vector3(0f, 0.3656657f, 0f);
            zvucnik.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            zvucnik.transform.localScale = new Vector3(1f, 1f, 1f);

            IntSwitchEnable = false;
            switchControll = false;
        }
        private void LightsOperation()
        {
            component1 = GameObject.Find("KEKMET(350-400psi)").transform.FindChild("LOD/Dashboard/Lights").GetComponent<PlayMakerFSM>();
            fsmLightKnob = component1.FsmVariables.GetFsmInt("Selection");
            fsmValue = fsmLightKnob.Value;
            if (fsmValue == 1 || fsmValue == 2)
            {
                lightDRL.SetActive(true);
                lightDFL.SetActive(true);
                lightDFR.SetActive(true);
                lightDRR.SetActive(true);
                pozicija.SetActive(true);
            }
            else
            {
                lightDRL.SetActive(false);
                lightDFL.SetActive(false);
                lightDFR.SetActive(false);
                lightDRR.SetActive(false);
                pozicija.SetActive(false);
            }
            component2 = GameObject.Find("KEKMET(350-400psi)").transform.FindChild("LOD/Dashboard/Brake/Pivot").GetComponent<PlayMakerFSM>();
            fsmBrakes = component2.FsmVariables.GetFsmFloat("Data");
            fsmBrakesValue = fsmBrakes.Value;
            component3 = GameObject.Find("KEKMET(350-400psi)").transform.FindChild("LOD/Dashboard/Ignition").GetComponent<PlayMakerFSM>();
            fsmIgnition = component3.FsmVariables.GetFsmBool("MotorOn");
            fsmIgnitionValue = fsmIgnition.Value;
            if (fsmBrakesValue == 25 && fsmIgnitionValue == true)
            {
                lightBLR.SetActive(true);
                lightBRR.SetActive(true);
                kocnica.SetActive(true);
            }
            else
            {
                lightBLR.SetActive(false);
                lightBRR.SetActive(false);
                kocnica.SetActive(false);
            }
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.name == "WorkLightTrigger")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            switchControll = !switchControll;
                            BSL.Play();
                            
                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Back Worklight";
                        break;
                    }
                    if (hit.collider.name == "BlinkerTrigger")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            BswitchONleft.SetActive(true);
                            BswitchONright.SetActive(false);
                            BlinkerSwitchAudioON.GetComponent<AudioSource>().Play();
                            if (BswitchONleft.activeSelf)
                            {
                                BswitchOFF.SetActive(false);
                            }
                            else 
                            {
                                BswitchOFF.SetActive(true);
                            }
                            if(BswitchONleft.activeSelf == true && BswitchONright.activeSelf == true)
                            {
                                BswitchONleft.SetActive(false);
                                BswitchONright.SetActive(false);
                                BswitchOFF.SetActive(true);
                            }
                            else if(BswitchONleft.activeSelf == false && BswitchONright.activeSelf == true)
                            {
                                BswitchONright.SetActive(true);
                                BswitchONleft.SetActive(false);
                                BswitchOFF.SetActive(false);
                            }

                        }
                        if (Input.GetMouseButtonDown(1))
                        {

                            BswitchONright.SetActive(true);
                            BswitchONleft.SetActive(false);
                            BlinkerSwitchAudioON.GetComponent<AudioSource>().Play();
                            if (BswitchONright.activeSelf)
                            {
                                BswitchOFF.SetActive(false);
                            }
                            else
                            {
                                BswitchOFF.SetActive(true);
                            }
                            if (BswitchONright.activeSelf == true && BswitchONleft.activeSelf == true)
                            {
                                BswitchONright.SetActive(false);
                                BswitchONleft.SetActive(false);
                                BswitchOFF.SetActive(true);
                            }
                            else if (BswitchONright.activeSelf == false && BswitchONleft.activeSelf == true)
                            {
                                BswitchONleft.SetActive(true);
                                BswitchONright.SetActive(false);
                                BswitchOFF.SetActive(false);
                            }

                        }
                        if (Input.GetMouseButtonDown(2))
                        {
                            BswitchOFF.SetActive(true);
                            BswitchONleft.SetActive(false);
                            BswitchONright.SetActive(false);
                            BlinkerSwitchAudioOFF.GetComponent<AudioSource>().Play();
                            
                        }


                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Turn Signals";
                        break;
                    }
                    if (hit.collider.name == "IntLightTrigger")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            IntSwitchEnable = !IntSwitchEnable;
                            BSL.Play();

                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Interior Light";
                        break;
                    }

                }
            }

            component4 = GameObject.Find("KEKMET(350-400psi)").transform.FindChild("LOD/Dashboard/Gear/Lever").GetComponent<PlayMakerFSM>();
            reverseTrigger = component4.FsmVariables.GetFsmInt("Gear");
            FsmReverseValue = reverseTrigger.Value;

            if (switchControll || FsmReverseValue == 0)
            {
                worklightEmmit.SetActive(true);
                lightW1.SetActive(true);
                prekidacON.SetActive(true);
                prekidacOFF.SetActive(false);
                kontrolkaON.SetActive(true);
                lightWP.SetActive(true);
            }
            else
            {
                worklightEmmit.SetActive(false);
                lightW1.SetActive(false);
                prekidacON.SetActive(false);
                prekidacOFF.SetActive(true);
                kontrolkaON.SetActive(false);
                lightWP.SetActive(false);
            }
            if (BswitchONleft.activeSelf == true)
            {
                BlinkerLeft();
            }
            else if(Bl.activeSelf == true)
            {
                Bl.SetActive(false);
                lightzFL.SetActive(false);
                lightzRL.SetActive(false);
                
            }

            if (BswitchONright.activeSelf == true)
            {
                BlinkerRight();
            }
            else if (Br.activeSelf == true)
            {
                Br.SetActive(false);
                lightzFR.SetActive(false);
                lightzRR.SetActive(false);

            }
           
            if (FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle").Value == "Kekmet")
            {
                Tooggle();
            }
            if (openGUI.IsDown())
            {
                GuiShow();
            }
            if (FrontLoader)
            {
                tractorFL.SetActive(false);
            }
            else
            {
                tractorFL.SetActive(true);
            }

        }
        private void Tooggle()
        {
            if (Hazzard.IsDown())
            {
                TslPressed = !TslPressed;
                TsrPressed = !TsrPressed;
            }
            if (TurnSignalLeft.IsDown())
            {
                TslPressed = !TslPressed;
            }
            if (TslPressed)
            {
                BlinkerLeft();
            }
            else if (Bl.activeSelf == true)
            {
                Bl.SetActive(false);
                lightzFL.SetActive(false);
                lightzRL.SetActive(false);
            }
            if (TurnSignalRight.IsDown())
            {
                TsrPressed = !TsrPressed;
            }
            if (TsrPressed)
            {
                BlinkerRight();
            }
            else if (Br.activeSelf == true)
            {
                Br.SetActive(false);
                lightzFR.SetActive(false);
                lightzRR.SetActive(false);

            }
            
        }
        private void GuiShow()
        {
            showgui = !showgui;
            if (showgui)
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
        private float gear_r = 0f;
        private float gear_n = 0f;
        private float gear_1 = 0f;
        private float gear_2 = 0f;
        private float gear_3 = 0f;
        private float gear_4 = 0f;
        private float gear_5 = 0f;
        private float gear_6 = 0f;
        private void Window(int windowid)
        {
            GUI.Label(new Rect(150f, 30f, 150f, 20f), "Gear Ratios");
            GUI.Label(new Rect(10f, 50f, 10f, 30f), "R");
            gear_r = GUI.HorizontalSlider(new Rect(30f, 55f, 300f, 30f), gear_r, -15f, 15f);
            GUI.Label(new Rect(340f, 50f, 30f, 20f), gear_r.ToString("F1"));
            GUI.Label(new Rect(10f, 80f, 10f, 20f), "1");
            gear_1 = GUI.HorizontalSlider(new Rect(30f, 85f, 300f, 30f), gear_1, -15f, 15f);
            GUI.Label(new Rect(340f, 80f, 30f, 20f), gear_1.ToString("F1"));
            GUI.Label(new Rect(10f, 110f, 10f, 20f), "2");
            gear_2 = GUI.HorizontalSlider(new Rect(30f, 115f, 300f, 30f), gear_2, -15f, 15f);
            GUI.Label(new Rect(340f, 110f, 30f, 20f), gear_2.ToString("F1"));
            GUI.Label(new Rect(10f, 140f, 10f, 20f), "3");
            gear_3 = GUI.HorizontalSlider(new Rect(30f, 145f, 300f, 30f), gear_3, -15f, 15f);
            GUI.Label(new Rect(340f, 140f, 30f, 20f), gear_3.ToString("F1"));
            GUI.Label(new Rect(10f, 170f, 10f, 20f), "4");
            gear_4 = GUI.HorizontalSlider(new Rect(30f, 175f, 300f, 30f), gear_4, -15f, 15f);
            GUI.Label(new Rect(340f, 170f, 30f, 20f), gear_4.ToString("F1"));
            GUI.Label(new Rect(10f, 200f, 10f, 20f), "5");
            gear_5 = GUI.HorizontalSlider(new Rect(30f, 205f, 300f, 30f), gear_5, -15f, 15f);
            GUI.Label(new Rect(340f, 200f, 30f, 20f), gear_5.ToString("F1"));
            GUI.Label(new Rect(10f, 230f, 10f, 20f), "6");
            gear_6 = GUI.HorizontalSlider(new Rect(30f, 235f, 300f, 30f), gear_6, -15f, 15f);
            GUI.Label(new Rect(340f, 230f, 30f, 20f), gear_6.ToString("F1"));
            if (GUI.Button(new Rect(400f, 20f, 150f, 40f), "Front Loader ON/OFF"))
            {
                FrontLoader = !FrontLoader;
            }
            if (GUI.Button(new Rect(400f, 70f, 150f, 40f), "AWD ON/OFF"))
            {
                awdIsSet = !awdIsSet;
            }
            if (GUI.Button(new Rect(400f, 120f, 150f, 40f), "Save Settings"))
            {
                FLSaveSettings();
                GearsSave();
                ModConsole.Print("<b><color=green> Kekmet Addons: Settings Saved</color></b>");
            }
            if (GUI.Button(new Rect(400f, 170f, 150f, 40f), "Load Settings"))
            {
                FLLoadSettings();
                GearsLoad();
                ModConsole.Print("<b><color=green> Kekmet Addons: Settings Loaded</color></b>");
            }
            if (GUI.Button(new Rect(400f, 220f, 150f, 40f), "Reset Gear Ratio"))
            {
                GearsReset();
                ModConsole.Print("<b><color=green> Kekmet Addons: Gear Ratio Resetted</color></b>");
            }
            GUI.DragWindow();
        }
        private void FLSaveSettings()
        {
            string[] saveString = new string[1];
            int num = 0;
            bool StoreFL = FrontLoader;
            saveString[num] = StoreFL.ToString();
            File.WriteAllLines(string.Concat(path, "/FrontLoaderState.txt"), saveString);
        }
        private void GearsSave()
        {
            string[] str = new string[9];
            int num = 0;
            float GearR = this.gear_r;
            str[num] = GearR.ToString();
            int num1 = 1;
            float Gear1 = this.gear_1;
            str[num1] = Gear1.ToString();
            int num2 = 2;
            float Gear2 = this.gear_2;
            str[num2] = Gear2.ToString();
            int num3 = 3;
            float Gear3 = this.gear_3;
            str[num3] = Gear3.ToString();
            int num4 = 4;
            float Gear4 = this.gear_4;
            str[num4] = Gear4.ToString();
            int num5 = 5;
            float Gear5 = this.gear_5;
            str[num5] = Gear5.ToString();
            int num6 = 6;
            float Gear6 = this.gear_6;
            str[num6] = Gear6.ToString();
            int num7 = 7;
            float GearN = this.gear_n;
            str[num7] = GearN.ToString();
            int num8 = 8;
            bool awdState = this.awdIsSet;
            str[num8] = awdState.ToString();
            File.WriteAllLines(string.Concat(this.path, "/Gears.txt"), str);
        }
        private void GearsLoad()
        {
            string[] strArrays = new string[9];

            strArrays = File.ReadAllLines(string.Concat(this.path, "/Gears.txt"));
            this.gear_r = float.Parse(strArrays[0]);
            this.gear_1 = float.Parse(strArrays[1]);
            this.gear_2 = float.Parse(strArrays[2]);
            this.gear_3 = float.Parse(strArrays[3]);
            this.gear_4 = float.Parse(strArrays[4]);
            this.gear_5 = float.Parse(strArrays[5]);
            this.gear_6 = float.Parse(strArrays[6]);
            this.gear_n = float.Parse(strArrays[7]);
            this.awdIsSet = bool.Parse(strArrays[8]);
        }
        private void FLLoadSettings()
        {
            string[] saveStringArrays = new string[1];
            saveStringArrays = File.ReadAllLines(string.Concat(path, "/FrontLoaderState.txt"));
            this.FrontLoader = bool.Parse(saveStringArrays[0]);
        }

        public override void ModSettings()
        {
        }
        private float[] gear = new float [8];
        private void Gears()
        {
            if (drivetrain != null)
            {
                gear[0] = gear_r;
                gear[1] = gear_n;
                gear[2] = gear_1;
                gear[3] = gear_2;
                gear[4] = gear_3;
                gear[5] = gear_4;
                gear[6] = gear_5;
                gear[7] = gear_6;
                drivetrain.gearRatios = gear;
            }
        }
        private void GearsReset()
        {
            gear_r = -11f;
            gear_n = 0f;
            gear_1 = 11f;
            gear_2 = 7f;
            gear_3 = 5f;
            gear_4 = 3.8f;
            gear_5 = 2.6f;
            gear_6 = 2f;
        }
        private bool awdIsSet = false;
        private void AllWheel()
        {
            if(drivetrain.transmission == Drivetrain.Transmissions.RWD && awdIsSet)
            {
                drivetrain.SetTransmission(Drivetrain.Transmissions.AWD);
                drivetrain.transmission = Drivetrain.Transmissions.AWD;
                ModConsole.Print("<b><color=green>Kekmet Addons: All Wheel Drive Turned ON</color></b>");
            }
            else if(drivetrain.transmission == Drivetrain.Transmissions.AWD && !awdIsSet)
            {
                drivetrain.SetTransmission(Drivetrain.Transmissions.RWD);
                drivetrain.transmission = Drivetrain.Transmissions.RWD;
                ModConsole.Print("<b><color=green> Kekmet Addons: All Wheel Drive Turned OFF</color></b>");
            }
        }

        public override void OnSave()
        {
            
        }

        public override void OnGUI()
        {
            if (showgui)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, guiBox, new GUI.WindowFunction(this.Window), "Kekmet Rules!!! :-)");
            }
        }

        public override void Update()
        {

            Gears();
            AllWheel();
            LightsOperation();

            if (IntSwitchEnable)
            {
                intPrekidacOFF.SetActive(false);
                intPrekidacON.SetActive(true);
                intLightLight.SetActive(true);
                intLightEmmit.SetActive(true);
            }
            else
            {
                intPrekidacOFF.SetActive(true);
                intPrekidacON.SetActive(false);
                intLightLight.SetActive(false);
                intLightEmmit.SetActive(false);
            }
        }

        private readonly float waitTime = 0.5f;
        private float timer = 0.0f;
        private readonly float waitTime2 = 1.0f;
        private float timer2 = 0.0f;
        private readonly float waitTime3 = 0.5f;
        private float timer3 = 0.0f;
        private readonly float waitTime4 = 1.0f;
        private float timer4 = 0.0f;

        private void BlinkerLeft()
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                Bl.SetActive(true);
                lightzFL.SetActive(true);
                lightzRL.SetActive(true);
                if (!BS1.isPlaying)
                {
                    BS1.loop = false;
                    BS1.volume = 0.7f;
                    BS1.maxDistance = 3f;
                    BS1.minDistance = 1f;
                    BS1.spatialBlend = 1f;
                    BS1.Play();
                    
                }
            }
            timer2 += Time.deltaTime;
            if (timer2 > waitTime2)
            {
                Bl.SetActive(false);
                lightzFL.SetActive(false);
                lightzRL.SetActive(false);
                timer = 0f;
                timer2 = 0f;
            }

        }
        private void BlinkerRight()
        {
            timer3 += Time.deltaTime;
            if (timer3 > waitTime3)
            {
                Br.SetActive(true);
                lightzFR.SetActive(true);
                lightzRR.SetActive(true);
                if (!BS1.isPlaying)
                {
                    BS1.loop = false;
                    BS1.volume = 0.7f;
                    BS1.maxDistance = 3f;
                    BS1.minDistance = 1f;
                    BS1.spatialBlend = 1f;
                    BS1.Play();

                }

            }
            timer4 += Time.deltaTime;
            if (timer4 > waitTime4)
            {
                Br.SetActive(false);
                lightzFR.SetActive(false);
                lightzRR.SetActive(false);
                timer3 = 0f;
                timer4 = 0f;
            }
        }
    }
}
