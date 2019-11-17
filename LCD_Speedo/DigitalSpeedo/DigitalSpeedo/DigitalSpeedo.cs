﻿using MSCLoader;
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

        private SpeedoMagnet speedo_magnet;

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
            speed_text = gameObject.transform.FindChild("speed_text").gameObject;
            glass_middle = gameObject.transform.FindChild("glass_middle").gameObject;
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
            speedo_pivotLCD.transform.localPosition = new Vector3(-0.13f, 0.461f, 0.558f);
            speedo_pivotLCD.transform.localEulerAngles = new Vector3(270f, 201f, 0f);
            SphereCollider sphereCollider = speedo_pivotLCD.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 1f;
            speedo_magnet = lcd_display.AddComponent<SpeedoMagnet>();
            speedo_magnet.pivotCollider = sphereCollider;
            speedo_magnet.partCollider = lcd_display.GetComponent<Collider>();
            speedo_magnet.soundSource = lcd_display.GetComponent<AudioSource>();
            speedo_magnet.attachSound = attachSound;
            speedo_magnet.detachSound = detachSound;
            if (saveData.Attached)
            {
                speedo_magnet.Attach(playSound: false);
            }
            glass_middle.SetActive(false);
            speed_text.SetActive(false);
            drivetrain = SATSUMA.GetComponent<Drivetrain>();
            IgnitionCheck = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.Find("PowerON").gameObject;
           
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
                Attached = speedo_magnet.isFitted,
                position = (speedo_magnet.isFitted ? Vector3.zero : lcd_display.transform.localPosition),
                rotation = (speedo_magnet.isFitted ? Vector3.zero : lcd_display.transform.localEulerAngles),
            });
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            if (IgnitionCheck.activeSelf == true && speedo_magnet.isFitted == true)
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
