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
        public override string Version => "1.0.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;


        private AssetBundle ab;
        private GameObject lightOff;
        private GameObject lightOn;
        private GameObject r_spotlight;
        private GameObject lightBrake;
        private GameObject JONNEZ;
        private GameObject JONNEZ1;
        private Light spotlight;
        private Drivetrain drivetrain;
        private AxisCarController AxisCarController;
        private float intensity_normal = 2.0f;
        private float intensity_high = 5.0f;
        private GameObject lights;
        private PlayMakerFSM PlayMakerFSM;
        private FsmBool lightsON;
        private GameObject plate;


        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "rlight.unity3d");
            lightOff = GameObject.Instantiate( ab.LoadAsset("light_off.prefab")) as GameObject;
            lightOn = GameObject.Instantiate( ab.LoadAsset("light_on.prefab")) as GameObject;
            lightBrake = GameObject.Instantiate( ab.LoadAsset("light_break.prefab")) as GameObject;
            r_spotlight = GameObject.Instantiate( ab.LoadAsset("r_spotlight.prefab")) as GameObject;
            plate = GameObject.Instantiate(ab.LoadAsset("plate.prefab")) as GameObject;
            ab.Unload(false);


            JONNEZ = GameObject.Find("JONNEZ ES(Clone)/MESH/frame");

            lightOff.transform.SetParent(JONNEZ.transform, false);
            lightOn.transform.SetParent(JONNEZ.transform, false);
            lightBrake.transform.SetParent(JONNEZ.transform, false);
            r_spotlight.transform.SetParent(JONNEZ.transform, false);
            plate.transform.SetParent(JONNEZ.transform, false);
            lightOn.SetActive(false);
            lightBrake.SetActive(false);


            spotlight = r_spotlight.GetComponent<Light>();
            spotlight.intensity = intensity_normal;
            r_spotlight.SetActive(false);

            lights = GameObject.Find("JONNEZ ES(Clone)").transform.Find("LOD/Dashboard/Lights").gameObject;
            PlayMakerFSM = lights.GetComponent<PlayMakerFSM>();
            lightsON = PlayMakerFSM.FsmVariables.FindFsmBool("LightsOn");

            JONNEZ1 = GameObject.Find("JONNEZ ES(Clone)");
            drivetrain = JONNEZ1.GetComponent<Drivetrain>();
            AxisCarController = drivetrain.GetComponent<AxisCarController>();

        }

        public void LightCheck()
        {
            if (lightsON.Value == true)
            {
                lightOn.SetActive(true);
                r_spotlight.SetActive(true);
            }
            else
            {
                lightOn.SetActive(false);
                r_spotlight.SetActive(false);
            }
        }
        public void BrakeCheck()
        {
            if (AxisCarController.brakeKey == true) 
            { 
                lightBrake.SetActive(true);
                spotlight.intensity = intensity_high;
                r_spotlight.SetActive(true);
            }
            else
            {
                lightBrake.SetActive(false);
                spotlight.intensity = intensity_normal;
            }
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
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            LightCheck();
            BrakeCheck();
        }
    }
}
