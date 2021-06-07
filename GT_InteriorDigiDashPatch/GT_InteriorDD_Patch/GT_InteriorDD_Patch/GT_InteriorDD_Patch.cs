using MSCLoader;
using System;
using UnityEngine;

namespace GT_InteriorDD_Patch
{
    public class GT_InteriorDD_Patch : Mod
    {
        public override string ID => "GT_InteriorDD_Patch"; //Your mod ID (unique)
        public override string Name => "GT_InteriorDD_Patch"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;
        public override bool LoadInMenu => true;

        private bool IsGTIntInstalled;
        private bool IsDDInstalled;
        public override void OnMenuLoad()
        {
            IsDDInstalled = ModLoader.IsModPresent("SatsumaDigidash");
            IsGTIntInstalled = ModLoader.IsModPresent("GT_Interior");
            if (IsGTIntInstalled && IsDDInstalled)
            {
                ModConsole.Print("[GT_INTERIOR_DDPATCH]: <color=green>SatsumaDigidash Mod Found!</color>");
                ModConsole.Print("[GT_INTERIOR_DDPATCH]: <color=green>GT_Interior Mod Found!</color>");
            }
            else
            {
                ModConsole.Print("[GT_INTERIOR_DDPATCH]: <color=orange>SatsumaDigidash Mod Not Found!</color>");
                ModConsole.Print("[GT_INTERIOR_DDPATCH]: <color=green>Continuing...</color>");
            }
        }
        public override void OnLoad()
        {
            try
            {
                if (IsGTIntInstalled && IsDDInstalled)
                {
                    UnityEngine.Object.Destroy(GameObject.Find("gtmeters(Clone)"));
                }
                else
                {
                    ModConsole.Print("[GT_INTERIOR_DDPATCH]: <color=orange>This patch only works if GT_Interior and SatsumaDigidash Mods are installed... Otherwise it does nothing.</color>");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message);
                Debug.LogError(e.Message);
            }
        }
    }
}
