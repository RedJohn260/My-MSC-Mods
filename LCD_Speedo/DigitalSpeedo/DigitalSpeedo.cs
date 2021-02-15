using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;

namespace DigitalSpeedo
{
    public class DigitalSpeedo : Mod
    {
        public override string ID => "DigitalSpeedo"; //Your mod ID (unique)
        public override string Name => "DigitalSpeedo"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.3"; //Version

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

        private GameObject bk_text;

        private ChangeColor ChangeColor;

        private GameObject e_buttont;

        private GameObject e_buttonbg;

        private static string modName = typeof(DigitalSpeedo).Namespace;

        private static string path = Path.Combine(Application.persistentDataPath, modName + ".xml");

        private Settings resetButton = new Settings("LCD Display Reset", "Reset", ResetLCDSpeedo);
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
            AudioClip button_push = ab.LoadAsset<AudioClip>("car_dash_button");
            lcd_display = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            lcd_display.name = "LCD Display(Clone)";
            lcd_display.layer = LayerMask.NameToLayer("Parts");
            lcd_display.tag = "PART";
            lcd_display.transform.localPosition = saveData.position;
            lcd_display.transform.localEulerAngles = saveData.rotation;
            lcd_display.transform.FindChild("glass_front").gameObject.GetComponent<MeshRenderer>().reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            lcd_display.transform.FindChild("lcd").gameObject.GetComponent<MeshRenderer>().reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            speedo_pivotLCD = new GameObject("LCD_Trigger");
            speedo_pivotLCD.transform.SetParent(SATSUMA.transform, false);
            //speedo_pivotLCD.transform.localPosition = new Vector3(0f, 0.451f, 0.538f);
            //speedo_pivotLCD.transform.localEulerAngles = new Vector3(270f, 203.64f, 0f);
            speedo_pivotLCD.transform.localPosition = new Vector3(0f, 0.451f, 0.528f);
            speedo_pivotLCD.transform.localEulerAngles = new Vector3(270f, 184.6401f, 0f);
            SphereCollider sphereCollider = speedo_pivotLCD.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.05f;
            speedo_attach = lcd_display.AddComponent<SpeedoAttach>();
            speedo_attach.pivotCollider = sphereCollider;
            speedo_attach.partCollider = lcd_display.GetComponent<Collider>();
            speedo_attach.soundSource = lcd_display.GetComponent<AudioSource>();
            speedo_attach.attachSound = attachSound;
            speedo_attach.detachSound = detachSound;

            ChangeColor = lcd_display.AddComponent<ChangeColor>();
            ChangeColor.bg_button = lcd_display.transform.FindChild("bg_button").gameObject;
            ChangeColor.t_button = lcd_display.transform.FindChild("t_button").gameObject;
            ChangeColor.bgmat = lcd_display.transform.FindChild("glass_middle").gameObject.GetComponent<MeshRenderer>().material;
            ChangeColor.textmat = lcd_display.transform.FindChild("speed_text").gameObject.GetComponent<MeshRenderer>().material;
            ChangeColor.button_push = button_push;
            ChangeColor.audioSource = lcd_display.GetComponent<AudioSource>();

            if (saveData.Attached)
            {
                speedo_attach.Attach(playSound: false);
            }
            drivetrain = SATSUMA.GetComponent<Drivetrain>();
            IgnitionCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON").gameObject;
            speed_text = lcd_display.transform.FindChild("speed_text").gameObject;
            speed_text_mesh = speed_text.GetComponent<TextMesh>();
            glass_middle = lcd_display.transform.FindChild("glass_middle").gameObject;
            bk_text = lcd_display.transform.FindChild("bk_1").gameObject;
            e_buttont = lcd_display.transform.FindChild("t_button_e").gameObject;
            e_buttonbg = lcd_display.transform.FindChild("bg_button_e").gameObject;
            ChangeColor.e_buttonbg = e_buttonbg.GetComponent<MeshRenderer>().material;
            ChangeColor.e_buttontx = e_buttont.GetComponent<MeshRenderer>().material;
            glass_middle.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", saveData.bgColor);
            speed_text.GetComponent<MeshRenderer>().material.color = saveData.textccolor;
            speed_text.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", saveData.textccolor);
            e_buttonbg.GetComponent<MeshRenderer>().material.color = saveData.bgColor;
            e_buttonbg.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", saveData.bgColor);
            e_buttont.GetComponent<MeshRenderer>().material.color = saveData.textccolor;
            e_buttont.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", saveData.textccolor);
            ab.Unload(unloadAllLoadedObjects: false);
        }

        public override void ModSettings()
        {
            Settings.AddText(this, "This button deletes mod save file.");
            Settings.AddText(this, "Warrnig: Mod save file can't be recovered.");
            Settings.AddText(this, "Use it to reset the mod if you lost it.");
            Settings.AddButton(this, resetButton);
            
        }

        public override void OnSave()
        {
            SaveUtility.WriteFile(new SaveData
            {
                Attached = speedo_attach.isFitted,
                position = (speedo_attach.isFitted ? Vector3.zero : lcd_display.transform.localPosition),
                rotation = (speedo_attach.isFitted ? Vector3.zero : lcd_display.transform.localEulerAngles),
                bgColor = glass_middle.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor"),
                textccolor = speed_text.GetComponent<MeshRenderer>().material.color,
            });
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public static void ResetLCDSpeedo()
        {
            File.Delete(path);
        }
        public override void Update()
        {
            if (IgnitionCheck.activeSelf == true && speedo_attach.isFitted == true)
            {
                ChangeColor._isAttached = true;
                glass_middle.SetActive(true);
                speed_text.SetActive(true);
                bk_text.SetActive(false);
                e_buttont.SetActive(true);
                e_buttonbg.SetActive(true);
                dif_speed = Mathf.Abs(drivetrain.differentialSpeed);
                string text = Mathf.Round(dif_speed) + "---------------------------" + "KM/H";
                speed_text_mesh.text = text;
            }
            else
            {
                ChangeColor._isAttached = false;
                glass_middle.SetActive(false);
                speed_text.SetActive(false);
                bk_text.SetActive(true);
                e_buttont.SetActive(false);
                e_buttonbg.SetActive(false);
            }
        }
    }
}
