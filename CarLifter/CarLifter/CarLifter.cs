using MSCLoader;
using UnityEngine;

namespace CarLifter
{
    public class CarLifter : Mod
    {
        public override string ID => "CarLifter"; //Your mod ID (unique)
        public override string Name => "CarLifter"; //You mod name
        public override string Author => "Kubix, RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;

        private GameObject lifter;

        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "carlifter.unity3d");
            GameObject gameObject = ab.LoadAsset("car_lifter.prefab") as GameObject;
            lifter = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            GameObject.Find("REPAIRSHOP").transform.FindChild("LOD/Vehicle").gameObject.SetActive(value: false);
            lifter.transform.localPosition = new Vector3(1559.66f, 4.592002f, 732.856f);
            lifter.transform.localEulerAngles = new Vector3(0f, 65.00009f, 0f);
            lifter.AddComponent<SwitchBehavior>().enabled = true;

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
            // Update is called once per frame
        }
    }
}
