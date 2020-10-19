using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MSCLoader;
using UnityEngine;

namespace GaragePitCovers
{
    public class GaragePitCovers : Mod
    {
        public override string ID => "GaragePitCovers"; //Your mod ID (unique)
        public override string Name => "GaragePitCovers"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        public override bool LoadInMenu => false;


        private AssetBundle ab;
        private GameObject YARD;
        private GameObject PitColliders;
        private GameObject PitCover1;
        private GameObject PitCover2;
        private GameObject PitCover3;
        private GameObject Collider1;
        private GameObject Collider2;
        private GameObject Collider3;
        private CoverAttach coverAttach1;
        private CoverAttach coverAttach2;
        private CoverAttach coverAttach3;
        private static string modName = typeof(GaragePitCovers).Namespace;
        private static string path = Path.Combine(Application.persistentDataPath, modName + ".xml");
        private Settings resetButton = new Settings("Pit Covers Reset", "Reset", ResetCovers);
        private GameObject Collisions;
        private GameObject col1;
        private GameObject col2;
        private GameObject col3;
        public override void OnLoad()
        {
            SaveData saveData = SaveUtility.ReadFile<SaveData>();
            ab = LoadAssets.LoadBundle(this, "pitcovers.unity3d");
            YARD = GameObject.Find("YARD").transform.Find("LOD").gameObject;
            GameObject gameObject = ab.LoadAsset("GaragePitCoversColliders.prefab") as GameObject;
            GameObject gameObject1 = ab.LoadAsset("PitCover.prefab") as GameObject;
            GameObject gameObject2 = ab.LoadAsset("Collisions.prefab") as GameObject;
            AudioClip attachSound = ab.LoadAsset<AudioClip>("assemble");
            AudioClip detachSound = ab.LoadAsset<AudioClip>("disassemble");
            PitColliders = UnityEngine.Object.Instantiate(gameObject);
            PitCover1 = UnityEngine.Object.Instantiate(gameObject1);
            Collisions = UnityEngine.Object.Instantiate(gameObject2);
            UnityEngine.Object.Destroy(gameObject);
            UnityEngine.Object.Destroy(gameObject1);
            UnityEngine.Object.Destroy(gameObject2);
            Collider1 = PitColliders.transform.FindChild("Collider1").gameObject;
            Collider2 = PitColliders.transform.FindChild("Collider2").gameObject;
            Collider3 = PitColliders.transform.FindChild("Collider3").gameObject;

            col1 = Collisions.transform.FindChild("col1").gameObject;
            col2 = Collisions.transform.FindChild("col2").gameObject;
            col3 = Collisions.transform.FindChild("col3").gameObject;

            PitCover1.name = "Pit Cover (COVER)";
            PitCover1.layer = LayerMask.NameToLayer("Parts");
            PitCover1.tag = "PART";
            PitCover2 = UnityEngine.Object.Instantiate(PitCover1);
            PitCover3 = UnityEngine.Object.Instantiate(PitCover1);
            PitCover2.name = "Pit Cover (COVER)";
            PitCover3.name = "Pit Cover (COVER)";

            PitCover1.transform.localPosition = saveData.Cover1position;
            PitCover1.transform.localEulerAngles = saveData.Cover1rotation;

            PitCover2.transform.localPosition = saveData.Cover2position;
            PitCover2.transform.localEulerAngles = saveData.Cover2rotation;

            PitCover3.transform.localPosition = saveData.Cover3position;
            PitCover3.transform.localEulerAngles = saveData.Cover3rotation;


            PitColliders.transform.SetParent(YARD.transform, false);
            Collisions.transform.SetParent(YARD.transform, false);
            PitColliders.transform.localPosition = new Vector3(-16.54244f, -0.6847799f, 3.039505f);
            PitColliders.transform.localEulerAngles = new Vector3(0f, 0, 0);
            Collisions.transform.localPosition = new Vector3(-16.54244f, -0.6847799f, 3.039505f);
            Collisions.transform.localEulerAngles = new Vector3(0f, 0, 0);

            coverAttach1 = PitCover1.AddComponent<CoverAttach>();
            coverAttach1.pivotCollider = Collider1.GetComponent<Collider>();
            coverAttach1.partCollider = PitCover1.GetComponent<Collider>();
            coverAttach1.soundSource = PitCover1.GetComponent<AudioSource>();
            coverAttach1.attachSound = attachSound;
            coverAttach1.detachSound = detachSound;

            coverAttach2 = PitCover2.AddComponent<CoverAttach>();
            coverAttach2.pivotCollider = Collider2.GetComponent<Collider>();
            coverAttach2.partCollider = PitCover2.GetComponent<Collider>();
            coverAttach2.soundSource = PitCover2.GetComponent<AudioSource>();
            coverAttach2.attachSound = attachSound;
            coverAttach2.detachSound = detachSound;

            coverAttach3 = PitCover3.AddComponent<CoverAttach>();
            coverAttach3.pivotCollider = Collider3.GetComponent<Collider>();
            coverAttach3.partCollider = PitCover3.GetComponent<Collider>();
            coverAttach3.soundSource = PitCover3.GetComponent<AudioSource>();
            coverAttach3.attachSound = attachSound;
            coverAttach3.detachSound = detachSound;

            if (saveData.Cover1Attached)
            {
                coverAttach1.Attach(playSound: false);
            }
            if (saveData.Cover2Attached)
            {
                coverAttach2.Attach(playSound: false);
            }
            if (saveData.Cover3Attached)
            {
                coverAttach3.Attach(playSound: false);
            }
            ab.Unload(unloadAllLoadedObjects: false);
        }
        public override void OnNewGame()
        {
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
                Cover1Attached = coverAttach1.isFitted,
                Cover2Attached = coverAttach2.isFitted,
                Cover3Attached = coverAttach2.isFitted,

                Cover1position = (coverAttach1.isFitted ? Vector3.zero : PitCover1.transform.localPosition),
                Cover1rotation = (coverAttach1.isFitted ? Vector3.zero : PitCover1.transform.localEulerAngles),

                Cover2position = (coverAttach2.isFitted ? Vector3.zero : PitCover2.transform.localPosition),
                Cover2rotation = (coverAttach2.isFitted ? Vector3.zero : PitCover2.transform.localEulerAngles),

                Cover3position = (coverAttach3.isFitted ? Vector3.zero : PitCover3.transform.localPosition),
                Cover3rotation = (coverAttach3.isFitted ? Vector3.zero : PitCover3.transform.localEulerAngles),
            });
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            if (coverAttach1.isFitted)
            {
                col1.SetActive(true);
            }
            else
            {
                col1.SetActive(false);
            }

            if (coverAttach2.isFitted)
            {
                col2.SetActive(true);
            }
            else
            {
                col2.SetActive(false);
            }

            if (coverAttach3.isFitted)
            {
                col3.SetActive(true);
            }
            else
            {
                col3.SetActive(false);
            }
        }
        private static void ResetCovers()
        {
            File.Delete(path);
        }
    }
}
