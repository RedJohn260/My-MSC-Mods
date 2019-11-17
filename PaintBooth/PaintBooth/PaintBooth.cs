using MSCLoader;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;


namespace PaintBooth
{
    public class PaintBooth : Mod
    {
        private bool m_isLoaded;

        public static string assetPath;

        public static string imageFilePath;

        public override string ID => "PaintBooth";

        public override string Name => "PaintBooth";

        public override string Author => "RedJohn260";

        public override string Version => "0.1.3";

        public override bool UseAssetsFolder => true;
        private AssetBundle ab;

        private GameObject paint_booth_new;

        private GameObject repairshop_mesh;

        public GameObject doorl;

        public GameObject doorr;

        public GameObject g_doorl;

        public GameObject g_doorr;

        public GameObject salter;

        public static int time;

        public GameObject garage_doors;

        public GameObject SatusmaTrigger;

        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "paintbooth.unity3d");
            paint_booth_new = GameObject.Instantiate(ab.LoadAsset("paint_booth.prefab")) as GameObject;
            garage_doors = GameObject.Instantiate(ab.LoadAsset("garage_doors.prefab")) as GameObject;
            ab.Unload(false);
            repairshop_mesh = GameObject.Find("REPAIRSHOP/Building");
            GameObject.Find("REPAIRSHOP/Building/repair_shop_paint_booth").SetActive(false);
            paint_booth_new.transform.SetParent(repairshop_mesh.transform, false);
            paint_booth_new.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            garage_doors.transform.SetParent(repairshop_mesh.transform, false);
            garage_doors.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            assetPath = ModLoader.GetModAssetsFolder(this);
            string imagePath = Path.GetFullPath(".")  + "/Images/paintjobs.txt";
            imageFilePath = imagePath;
            ModConsole.Print("Loading Paintjobs From : " + imageFilePath.ToString());
            GameObject.Find("REPAIRSHOP").transform.FindChild("LOD/garage_door").gameObject.SetActive(false);
            GameObject.Find("REPAIRSHOP").transform.FindChild("LOD/garage_door 1").gameObject.SetActive(false);
            doorl = paint_booth_new.transform.Find("BoothDoorL").gameObject;
            doorr = paint_booth_new.transform.Find("BoothDoorR").gameObject;
            g_doorl = garage_doors.transform.Find("door_big_l").gameObject;
            g_doorr = garage_doors.transform.Find("door_big_r").gameObject;
            salter = paint_booth_new.transform.Find("Salter").gameObject;
            doorl.AddComponent<DoorsBehavior>().enabled = true;
            doorr.AddComponent<DoorsBehavior>().enabled = true;
            g_doorl.AddComponent<GarageDoorsBehavior>().enabled = true;
            g_doorr.AddComponent<GarageDoorsBehavior>().enabled = true;
            salter.AddComponent<SwitchBehavior>().enabled = true;
            SatusmaTrigger = paint_booth_new.transform.Find("SatsumaTrigger").gameObject;
            SatusmaTrigger.AddComponent<DatsunTrigger>().enabled = true;
            Painter.LoadData();
        }
        public override void Update()
        {
            if (Application.loadedLevelName == "GAME")
            {
                if (!m_isLoaded)
                {
                    new GameObject("Skinner").AddComponent<Painter>();
                    m_isLoaded = true;
                }
            }
            else if (Application.loadedLevelName != "GAME" && m_isLoaded)
            {
                m_isLoaded = false;
            }
        }
        public override void OnSave()
        {
            time = Painter.StaticSeconds;
            var path = Path.Combine(Application.persistentDataPath, "PaintBooth.xml");
            SaveUtility.SerializeWriteFile(new SaveData
            {
               timeLeft = time
            }, path);
        }
        public override void OnNewGame()
        {
            var path2 = Path.Combine(Application.persistentDataPath, "PaintBooth.xml");
            SaveUtility.SerializeWriteFile(new SaveData
            {
                timeLeft = time
            }, path2);
            ModConsole.Print("PAINT BOOTH NOW RESETTING.");
        }
    }
}
