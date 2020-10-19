using System.Collections.Generic;
using MSCLoader;
using UnityEngine;

namespace M67Granade
{
    public class M67Granade : Mod
    {
        public override string ID => "M67Granade"; //Your mod ID (unique)
        public override string Name => "M67Granade"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;

        private GameObject _ammoCrate;

        private AmmoCrate crate_script;

        private GameObject the_dude;

        private the_dude_ragdoll the_Dude_Ragdoll;

        private GameObject PLAYER;

        private GameObject furnature;

        private GameObject radio;
        private AudioSource radioTrack;

        private RadioBehavior radioBehavior;

        private DudeBehavior dudeBehavior;

        private GameObject SUN;
        private PlayMakerFSM Clock;
        private bool isNight;

        private GameObject StrobeLights;
        private Animation StrobeLightsAnim;
        private GameObject StrobeLightR;
        private GameObject StrobeLightG;
        private GameObject StrobeLightB;

        private GameObject door;

        private DoorBehavior doorBehavior;

        public override void OnNewGame()
        {
            ModConsole.Print("M67 GRANADE: NEW GAME STARTED, RESETTING MOD.");
            SaveUtility.Remove();
        }

        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "m67granade.unity3d");
            GameObject gameObject = ab.LoadAsset("ammocrate.prefab") as GameObject;
            GameObject gameObject1 = ab.LoadAsset("Explosion.prefab") as GameObject;
            GameObject gameObject2 = ab.LoadAsset("m67_granade.prefab") as GameObject;
            GameObject gameObject3 = ab.LoadAsset("the_dude_base.prefab") as GameObject;
            GameObject gameObject4 = ab.LoadAsset("furnature.prefab") as GameObject;
            GameObject gameObject5 = ab.LoadAsset("radio.prefab") as GameObject;
            GameObject gameObject6 = ab.LoadAsset("dude_door.prefab") as GameObject;
            _ammoCrate = UnityEngine.Object.Instantiate(gameObject);
            _ammoCrate.name = "Ammo Crate (Clone)";
            crate_script = _ammoCrate.AddComponent<AmmoCrate>();
            the_dude = UnityEngine.Object.Instantiate(gameObject3);
            crate_script.granade = UnityEngine.Object.Instantiate(gameObject2);
            crate_script.exploEffect = UnityEngine.Object.Instantiate(gameObject1);
            furnature = UnityEngine.Object.Instantiate(gameObject4);
            radio = UnityEngine.Object.Instantiate(gameObject5);
            door = UnityEngine.Object.Instantiate(gameObject6);

            UnityEngine.Object.Destroy(gameObject);
            UnityEngine.Object.Destroy(gameObject1);
            UnityEngine.Object.Destroy(gameObject2);
            UnityEngine.Object.Destroy(gameObject3);
            UnityEngine.Object.Destroy(gameObject4);
            UnityEngine.Object.Destroy(gameObject5);
            UnityEngine.Object.Destroy(gameObject6);

            ab.Unload(false);

            PLAYER = GameObject.Find("PLAYER");
            the_Dude_Ragdoll = the_dude.AddComponent<the_dude_ragdoll>();
            the_Dude_Ragdoll.PLAYER = PLAYER;

            radioTrack = radio.transform.FindChild("musicTrack").GetComponent<AudioSource>();
            radioBehavior = radio.AddComponent<RadioBehavior>();
            dudeBehavior = radio.AddComponent<DudeBehavior>();
            dudeBehavior.character = the_dude.transform.FindChild("the_dude").gameObject;
            dudeBehavior.secondObject = radio.transform.FindChild("charStartPos").gameObject;

            radioBehavior.dude = dudeBehavior;
            dudeBehavior.radio = radioBehavior;

            GameObject doorParent = GameObject.Find("PERAJARVI/TerraceHouse/Apartments/LOD2/DoorsApartment2/DoorFront");
            GameObject oldDoor = doorParent.transform.FindChild("house_door1").gameObject;
            UnityEngine.Object.Destroy(oldDoor);
            door.transform.SetParent(doorParent.transform, false);
            door.transform.localPosition = new Vector3(0f, 0.05f, 0f);
            door.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            doorBehavior = door.AddComponent<DoorBehavior>();

            SUN = GameObject.Find("MAP/SUN/Pivot/SUN");
            Clock = SUN.GetComponent<PlayMakerFSM>();

            StrobeLights = furnature.transform.FindChild("StrobeLights").gameObject;
            StrobeLightsAnim = StrobeLights.GetComponent<Animation>();
            StrobeLightR = StrobeLights.transform.FindChild("spotlightR").transform.FindChild("Red").gameObject;
            StrobeLightG = StrobeLights.transform.FindChild("spotlightG").transform.FindChild("Green").gameObject;
            StrobeLightB = StrobeLights.transform.FindChild("spotlightB").transform.FindChild("Blue").gameObject;

            crate_script.ragdoll = the_Dude_Ragdoll;
            the_Dude_Ragdoll.ammoCrate = crate_script;
            LoadData();
        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            SaveUtility.Save(new SaveData
            {
                granadePrice = crate_script.ammountTaken

            });
        }

        private void LoadData()
        {
            SaveData saveData = SaveUtility.Load<SaveData>();
            crate_script.ammountTaken = saveData.granadePrice;
        }
        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        private bool isRadioPlaying = false;
        public override void Update()
        {
           
            isNight = Clock.FsmVariables.GetFsmBool("Night").Value;
            if (isNight)
            {
                StrobeLightR.SetActive(true);
                StrobeLightG.SetActive(true);
                StrobeLightB.SetActive(true);
                the_dude.SetActive(true);
                _ammoCrate.SetActive(true);
                radio.SetActive(true);
                if (!isRadioPlaying)
                {
                    if (!radioTrack.isPlaying)
                    {
                        radioTrack.Play();
                        isRadioPlaying = true;
                    }
                }
                if (!StrobeLightsAnim.isPlaying)
                {
                    StrobeLightsAnim.Play();
                }
            }
            else
            {
                StrobeLightR.SetActive(false);
                StrobeLightG.SetActive(false);
                StrobeLightB.SetActive(false);
                the_dude.SetActive(false);
                _ammoCrate.SetActive(false);
                radio.SetActive(false);
                isRadioPlaying = false;
                if (StrobeLightsAnim.isPlaying)
                {
                    StrobeLightsAnim.Stop();
                }
            }
        }
    }
}
