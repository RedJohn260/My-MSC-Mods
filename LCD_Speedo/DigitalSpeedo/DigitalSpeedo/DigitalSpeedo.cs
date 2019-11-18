using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace DigitalSpeedo
{
    public class DigitalSpeedo : Mod
    {
        public override string ID => "DigitalSpeedo"; //Your mod ID (unique)
        public override string Name => "DigitalSpeedo"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private GameObject SATSUMA;

        private Drivetrain drivetrain;

        private float dif_speed;

        private AssetBundle ab;

        private GameObject lcd_display;

        private GameObject speed_text;

        private GameObject glass_middle;

        private TextMesh speed_text_mesh;

        private GameObject IgnitionCheck;

        private SpeedoAttach speedo_attach;

        private GameObject speedo_pivotLCD;

        public override void OnNewGame()
        {
            SaveUtility.WriteFile(new SaveData());
            ModConsole.Print("LCD Digital Speedometer Mod Is Resetting");
        }

        public override void OnLoad()
        {
            SaveData saveData = SaveUtility.ReadFile<SaveData>();
            ab = LoadAssets.LoadBundle(this, "speedo.unity3d");
            GameObject gameObject = ab.LoadAsset("lcd_display.prefab") as GameObject;
            SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");
            AudioClip attachSound = ab.LoadAsset<AudioClip>("assemble");
            AudioClip detachSound = ab.LoadAsset<AudioClip>("disassemble");
            lcd_display = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            lcd_display.name = "LCD Display(Clone)";
            lcd_display.layer = LayerMask.NameToLayer("Parts");
            lcd_display.tag = "PART";
            lcd_display.transform.localPosition = saveData.position;
            lcd_display.transform.localEulerAngles = saveData.rotation;
            speedo_pivotLCD = new GameObject("LCD_Trigger");
            speedo_pivotLCD.transform.SetParent(SATSUMA.transform, false);
            speedo_pivotLCD.transform.localPosition = new Vector3(0f, 0.451f, 0.538f);
            speedo_pivotLCD.transform.localEulerAngles = new Vector3(270f, 203.64f, 0f);
            SphereCollider sphereCollider = speedo_pivotLCD.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.05f;
            speedo_attach = lcd_display.AddComponent<SpeedoAttach>();
            speedo_attach.pivotCollider = sphereCollider;
            speedo_attach.partCollider = lcd_display.GetComponent<Collider>();
            speedo_attach.soundSource = lcd_display.GetComponent<AudioSource>();
            speedo_attach.attachSound = attachSound;
            speedo_attach.detachSound = detachSound;
            if (saveData.Attached)
            {
                speedo_attach.Attach(playSound: false);
            }
            drivetrain = SATSUMA.GetComponent<Drivetrain>();
            IgnitionCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON").gameObject;
            speed_text = lcd_display.transform.FindChild("speed_text").gameObject;
            speed_text_mesh = speed_text.GetComponent<TextMesh>();
            glass_middle = lcd_display.transform.FindChild("glass_middle").gameObject;
            ab.Unload(unloadAllLoadedObjects: false);
        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            SaveUtility.WriteFile(new SaveData
            {
                Attached = speedo_attach.isFitted,
                position = (speedo_attach.isFitted ? Vector3.zero : lcd_display.transform.localPosition),
                rotation = (speedo_attach.isFitted ? Vector3.zero : lcd_display.transform.localEulerAngles),
            });
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            if (IgnitionCheck.activeSelf == true && speedo_attach.isFitted == true)
            {
                glass_middle.SetActive(true);
                speed_text.SetActive(true);
                dif_speed = Mathf.Abs(drivetrain.differentialSpeed);
                string text = Mathf.Round(dif_speed) + "." + "km/h";
                speed_text_mesh.text = text;
            }
            else
            {
                glass_middle.SetActive(false);
                speed_text.SetActive(false);
            }
        }
    }
}
