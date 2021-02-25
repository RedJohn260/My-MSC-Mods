using MSCLoader;
using UnityEngine;

namespace RPS
{
    public class RPS : Mod
    {
        public override string ID => "RPS"; //Your mod ID (unique)
        public override string Name => "RPS"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject RPS_BASE;
        private Vector3 rps_base_pos = new Vector3(-1372.8f, 3.109999f, 1272.5f);
        private Vector3 rps_base_rot = new Vector3(270f, 353.1037f, 0f);
        private Vector3 rps_base2_pos = new Vector3(-1332.4f, -2.004f, -651.3997f);
        private Vector3 rps_base2_rot = new Vector3(270f, 130.0015f, 0f);
        private GameObject lifter_moving_parts;
        private GameObject lifter_base;
        private LegsBehavior lifter_legs;
        private LiftingBehavior lifter_control;
        private BuyBehavior buy_it;
        private BuyBehavior buy_it2;
        private GameObject RPS_BASE2;
        private Settings resetButton = new Settings("RPS Reset buy state", "Reset", ResetBuy);


        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            SaveData saveData = SaveUtility.Load<SaveData>();
            ab = LoadAssets.LoadBundle(this, "rps.unity3d");
            GameObject gameObject = ab.LoadAsset("RPS.prefab") as GameObject;
            RPS_BASE = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            RPS_BASE.transform.position = rps_base_pos;
            RPS_BASE.transform.localEulerAngles = rps_base_rot;
            lifter_moving_parts = RPS_BASE.transform.FindChild("CarLifter").transform.FindChild("moving_parts").gameObject;
            lifter_base = RPS_BASE.transform.FindChild("CarLifter").gameObject;

            lifter_legs = lifter_moving_parts.AddComponent<LegsBehavior>();
            lifter_legs.leg1 = lifter_moving_parts.transform.Find("leg_1").gameObject;
            lifter_legs.leg2 = lifter_moving_parts.transform.Find("leg_2").gameObject;
            lifter_legs.leg3 = lifter_moving_parts.transform.Find("leg_3").gameObject;
            lifter_legs.leg4 = lifter_moving_parts.transform.Find("leg_4").gameObject;

            lifter_control = lifter_base.AddComponent<LiftingBehavior>();
            lifter_control.lift_switch = lifter_base.transform.FindChild("switch_mesh").gameObject;
            lifter_control.lifting_part = lifter_base.transform.FindChild("moving_parts").gameObject;

            buy_it = RPS_BASE.AddComponent<BuyBehavior>();
            buy_it.container = RPS_BASE.transform.FindChild("Container").gameObject;
            buy_it.buy_sign = RPS_BASE.transform.FindChild("sign").gameObject;


            RPS_BASE2 = Object.Instantiate(RPS_BASE);
            RPS_BASE2.name = "RPS2(Clone)";
            RPS_BASE2.transform.position = rps_base2_pos;
            RPS_BASE2.transform.localEulerAngles = rps_base2_rot;
            buy_it2 = RPS_BASE2.GetComponent<BuyBehavior>();
            GameObject con2 = RPS_BASE2.transform.FindChild("Container").gameObject;
            GameObject sin2 = RPS_BASE2.transform.FindChild("sign").gameObject;
            con2.name = "con2";
            sin2.name = "sin2";
            buy_it2.container = con2;
            buy_it2.buy_sign = sin2;
            buy_it2.sign_collider_name = sin2.name;

            GameObject mov_parts = RPS_BASE2.transform.FindChild("CarLifter").transform.FindChild("moving_parts").gameObject;
            GameObject lift_b = RPS_BASE2.transform.FindChild("CarLifter").gameObject;
            LegsBehavior leg_B = mov_parts.GetComponent<LegsBehavior>();
            leg_B.leg1 = mov_parts.transform.FindChild("leg_1").gameObject;
            leg_B.leg2 = mov_parts.transform.FindChild("leg_2").gameObject;
            leg_B.leg3 = mov_parts.transform.FindChild("leg_3").gameObject;
            leg_B.leg4 = mov_parts.transform.FindChild("leg_4").gameObject;
            leg_B.leg1.name = "leg1b";
            leg_B.leg2.name = "leg2b";
            leg_B.leg3.name = "leg3b";
            leg_B.leg4.name = "leg4b";

            LiftingBehavior lift_beh = lift_b.GetComponent<LiftingBehavior>();
            lift_beh.lift_switch = lift_b.transform.FindChild("switch_mesh").gameObject;
            lift_beh.lifting_part = lift_b.transform.FindChild("moving_parts").gameObject;
            lift_beh.lift_switch.name = "switch_mesh2";
            lift_beh.lifting_part.name = "moving_parts2";

            if (saveData.RPS_Bought1)
            {
                Object.Destroy(buy_it.container);
                Object.Destroy(buy_it.buy_sign);
                buy_it.IsBought = true;
            }
            if (saveData.RPS_Bought2)
            {
                Object.Destroy(buy_it2.container);
                Object.Destroy(buy_it2.buy_sign);
                buy_it2.IsBought = true;
            }
            ab.Unload(false);
        }

        public override void ModSettings()
        {
            Settings.AddText(this, "This button deletes mod save file.");
            Settings.AddText(this, "Warrnig: Mod save file can't be recovered.");
            Settings.AddText(this, "Use it to reset restet the bought state of RPS.");
            Settings.AddButton(this, resetButton);
        }

        public override void OnSave()
        {
            SaveUtility.Save(new SaveData
            {
                RPS_Bought1 = buy_it.IsBought,
                RPS_Bought2 = buy_it2.IsBought
            });
        }
        private static void ResetBuy()
        {
            SaveUtility.Remove();
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {

        }
    }
}
