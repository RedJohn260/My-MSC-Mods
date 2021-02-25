using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
namespace AddMoney
{
    public class MoneyMod : Mod
    {
        public override string ID => "MoneyMod"; //Your mod ID (unique)
        public override string Name => "MoneyMod"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        public FsmFloat money;

        public float ammount = 1000f;

        private Keybind money_add = new Keybind("Add1000mk", "Adds 1000mk", KeyCode.Keypad5, KeyCode.LeftControl);
        private Keybind money_remove= new Keybind("Remove1000mk", "Removes 1000mk", KeyCode.Keypad2, KeyCode.LeftControl);

        public override void OnLoad()
        {
            money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
            Keybind.Add(this,money_add);
            Keybind.Add(this, money_remove);
        }
        public override void Update()
        {
            if (money_add.GetKeybindDown())
            {
                money.Value += ammount;
                ModConsole.Print("<color=orange>MoneyMod : Added 1000 marks.</color>");

            }
            else if (money_remove.GetKeybindDown())
            {
                money.Value -= ammount;
                ModConsole.Print("<color=orange>MoneyMod : Removed 1000 marks.</color>");
            }
        }
    }
}
