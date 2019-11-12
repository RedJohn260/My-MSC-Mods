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

        public override string ID => "PaintBooth";

        public override string Name => "PaintBooth";

        public override string Author => "RedJohn260, Zamp";

        public override string Version => "0.1.3";

        public override bool UseAssetsFolder => true;

        /*public class SaveData
        {
            public int waitTimeLeft;
        }*/


        private AssetBundle ab;
        private GameObject paint_booth_new;
        private GameObject repairshop_mesh;
        private Painter painter;

        public static int time;
        







        public override void OnLoad()
        {
            
            ab = LoadAssets.LoadBundle(this, "paintbooth.unity3d");

            paint_booth_new = GameObject.Instantiate(ab.LoadAsset("paint_booth.prefab")) as GameObject;
            ab.Unload(false);

            repairshop_mesh = GameObject.Find("REPAIRSHOP/Building");
            GameObject.Find("REPAIRSHOP/Building/repair_shop_paint_booth").SetActive(false);
            paint_booth_new.transform.SetParent(repairshop_mesh.transform, false);
            paint_booth_new.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            assetPath = ModLoader.GetModAssetsFolder(this);
            GameObject.Find("REPAIRSHOP").transform.FindChild("LOD/garage_door").gameObject.SetActive(false);

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
            //SaveUtility.WriteFile(new SaveData());
            ModConsole.Print("PAINT BOOTH: NEW GAME STARTED, MOD RESETTING.");
        }
    }
}
