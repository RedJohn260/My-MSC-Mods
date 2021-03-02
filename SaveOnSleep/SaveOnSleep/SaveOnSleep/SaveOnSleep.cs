using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace SaveOnSleep
{
    public class SaveOnSleep : Mod
    {
        public override string ID => "SaveOnSleep"; //Your mod ID (unique)
        public override string Name => "SaveOnSleep"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        private FsmBool IsPlayerSleeping;
        private SaveOnSleepBehavior saveBehavior;
        public bool SaveGame;
        private FsmBool DrunkGuyValue;
        private bool isDrunkGuyCalling;
        private GameObject phone;


        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            IsPlayerSleeping = FsmVariables.GlobalVariables.FindFsmBool("PlayerSleeps");
            phone = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Logic/PhoneLogic");
            DrunkGuyValue = phone.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Drunk");
            GameObject PLAYER = GameObject.Find("PLAYER");
            saveBehavior = PLAYER.AddComponent<SaveOnSleepBehavior>();
            ModConsole.Print("<color=yellow>[SOS]: </color><color=white>Loaded Successfully!</color>");
        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        public override void OnGUI()
        {

        }

        public override void Update()
        {
            isDrunkGuyCalling = DrunkGuyValue.Value;
            if (isDrunkGuyCalling == false)
            {
                if (IsPlayerSleeping.Value)
                {
                    SaveGame = true;
                    if (SaveGame)
                    {
                        saveBehavior.SaveSleep();
                        IsPlayerSleeping.Value = false;
                    }
                }
            }
        }
    }
}
