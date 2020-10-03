using System.Collections.Generic;
using System.IO;
using System.Linq;
using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

namespace BadLookHood
{
    public class BadLookHood : Mod
    {
        public override string ID => "BadLookHood"; //Your mod ID (unique)
        public override string Name => "BadLookHood"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        public override bool SecondPass => true;

        private AssetBundle ab;
        private GameObject SATSUMA;
        private GameObject NewHood;
        private GameObject OldHood;
        private GameObject badhood_pivot;
        private HoodAttach hood_attach;
        private static string modName = typeof(BadLookHood).Namespace;
        private static string path = Path.Combine(Application.persistentDataPath, modName + ".xml");
        private Settings resetButton = new Settings("ResetHood", "Reset", ResetHood);
        public override void OnNewGame()
        {
            ModConsole.Print("BAD LOOK HOOD: NEW GAME STARTED, RESETTING MOD.");
            SaveUtility.Remove();
        }

        public override void OnLoad()
        {
            SaveData saveData = SaveUtility.Load<SaveData>();
            ab = LoadAssets.LoadBundle(this, "hood.unity3d");
            GameObject gameObject = ab.LoadAsset("BadLookHood.prefab") as GameObject;
            SATSUMA = GameObject.Find("SATSUMA(557kg, 248)/Body");
            AudioClip attachSound = ab.LoadAsset<AudioClip>("assemble");
            AudioClip detachSound = ab.LoadAsset<AudioClip>("disassemble");
            FsmBool IsHoodPurchased = GameObject.Find("Database/DatabaseOrders/Fiberglass Hood").GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Purchased").Value;
            FsmBool Installed = GameObject.Find("Database/DatabaseOrders/Fiberglass Hood").GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Installed").Value;
            if (IsHoodPurchased.Value == true && Installed.Value == true)
            {
                GameObject hood = GameObject.Find("SATSUMA(557kg, 248)/Body/pivot_hood/fiberglass hood(Clone)");
                OldHood = hood;
                ModConsole.Print("Hood found on Satsuma");
            }
            else if (IsHoodPurchased.Value == false && Installed.Value == false)
            {
                OldHood = GameObject.Find("STORE").transform.FindChild("Boxes").transform.FindChild("fiberglass hood(Clone)").transform.FindChild("fiberglass hood(Clone)").gameObject;
                ModConsole.Print("Hood found in store");
            }
            else if (IsHoodPurchased.Value == true && Installed.Value == false)
            {
                OldHood = GameObject.Find("fiberglass hood(Clone)");
                ModConsole.Print("Hood found in world");
            }
            NewHood = Object.Instantiate(OldHood);
            NewHood.SetActive(false);
            MeshFilter oldmeshrend = NewHood.GetComponent<MeshFilter>();
            MeshFilter newmeshrend = gameObject.GetComponent<MeshFilter>();
            oldmeshrend.mesh = newmeshrend.mesh;
            Object.Destroy(gameObject);
            Object.Destroy(NewHood.GetComponents<PlayMakerFSM>()[0]);
            Object.Destroy(NewHood.GetComponents<PlayMakerFSM>()[1]);
            Object.Destroy(NewHood.transform.FindChild("hoodpins").gameObject);
            Object.Destroy(NewHood.transform.FindChild("Bolts").gameObject);
            NewHood.name = "Bad Look Hood(Clone)";
            NewHood.layer = LayerMask.NameToLayer("Parts");
            NewHood.tag = "PART";
            badhood_pivot = new GameObject("BadHood_Trigger");
            badhood_pivot.transform.SetParent(SATSUMA.transform, false);
            badhood_pivot.transform.localPosition = new Vector3(0, 0.7812822f, 1.680859f);
            badhood_pivot.transform.localEulerAngles = new Vector3(270f, 179.9915f, 0); ;
            SphereCollider sphereCollider = badhood_pivot.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.5f;
            hood_attach = NewHood.AddComponent<HoodAttach>();
            hood_attach.pivotCollider = sphereCollider;
            hood_attach.partCollider = NewHood.GetComponent<Collider>();
            hood_attach.soundSource = NewHood.GetComponent<AudioSource>();
            hood_attach.attachSound = attachSound;
            hood_attach.detachSound = detachSound;
            if (saveData.Attached)
            {
                hood_attach.Attach(playSound: false);
            }
            NewHood.transform.localPosition = saveData.BadLookHoodPos;
            NewHood.transform.localEulerAngles = saveData.BadLookHoodRot;
            ab.Unload(unloadAllLoadedObjects: false);
        }
        public override void SecondPassOnLoad()
        {
            NewHood.SetActive(true);
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
            SaveUtility.Save(new SaveData
            {
                Attached = hood_attach.isFitted,
                BadLookHoodPos = (hood_attach.isFitted ? Vector3.zero : NewHood.transform.localPosition),
                BadLookHoodRot = (hood_attach.isFitted ? Vector3.zero : NewHood.transform.localEulerAngles),
            });
        }
        public static void ResetHood()
        {
            File.Delete(path);
        }
    }
}
