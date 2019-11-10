using MSCLoader;
using UnityEngine;

namespace MSCPaintMagazine
{
    public class PaintMagazine : Mod
    {
        private bool m_isLoaded;

        public static string assetPath;

        public override string ID => "SatsumaPaintM";

        public override string Name => "SatsumaPaintM";

        public override string Author => "RedJohn260(original by: Zamp)";

        public override string Version => "0.1.3";

        public override bool UseAssetsFolder => true;

        public override void OnLoad()
        {
            assetPath = ModLoader.GetModAssetsFolder(this);
            GameObject.Find("REPAIRSHOP").transform.FindChild("LOD/garage_door").gameObject.SetActive(false);
            
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
    }
}
