using MSCLoader;
using UnityEngine;

namespace RJMicrowave
{
    public class RJMicrowave : Mod
    {
        public override string ID => "RJMicrowave"; //Your mod ID (unique)
        public override string Name => "RJMicrowave"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject RJ_Microwave;
        private GameObject boxStore;
        private GameObject boxWorld;
        SaveData saveData;
        private BoxOpenBehavior boxOpen;
        private DoorBehavior doorBehavior;
        private GameObject MicrowaveDoor;
        private GameObject KnobPower;
        private GameObject KnobTime;
        private KnobPowerBehavior powerBehavior;
        private KnobTimeBehavior timeBehavior;
        private GameObject Plate;
        private MicrowaveBakingBehavior bakingBehavior;
        private Vector3 tempBoxWorldPos = new Vector3(0, 0, 0);
        private Vector3 tempBoxWorldRot = new Vector3(0, 0, 0);
        private GameObject MicrowaveFood;
        private GameObject Pizza;
        private GameObject Macaroni;
        private GameObject Sausages;
        private Material PizzaRawMat;
        private Material PizzaBakedMat;
        private Material MacaroniRawMat;
        private Material MacaroniBakedMat;
        private Material SausagesRawMat;
        private Material SausagesBakedMat;
        private Settings resetButton = new Settings("Microwave Reset state", "Reset", MicrowaveReset);

        public override void OnNewGame()
        {
            SaveUtility.Save(new SaveData());
        }

        public override void OnLoad()
        {
            saveData = SaveUtility.Load<SaveData>();
            ab = LoadAssets.LoadBundle(this, "microwave.unity3d");
            MicrowaveFoodLoad();
            MicrowaveLoad();
            BoxWorldLoad();
            BoxStoreLoad();
            ab.Unload(false);
        }

        public override void ModSettings()
        {
            Settings.AddText(this, "This button deletes mod save file.");
            Settings.AddText(this, "Warrnig: Mod save file can't be recovered.");
            Settings.AddText(this, "Use it to reset restet state of RJMicrowave.");
            Settings.AddButton(this, resetButton);
        }

        public override void OnSave()
        {
            if (boxWorld != null)
            {
                tempBoxWorldPos = boxWorld.transform.position;
                tempBoxWorldRot = boxWorld.transform.eulerAngles;
            }
            else
            {
                tempBoxWorldPos = new Vector3(0, 0, 0);
                tempBoxWorldRot = new Vector3(0, 0, 0);
            }
            SaveUtility.Save(new SaveData
            {
                MicrowavePurchased = boxStore == null,
                MicrowaveBoxWorldUnboxed = boxOpen == null,
                MicrowaveBoxWorldPosition = tempBoxWorldPos,
                MicrowaveBoxWorldRotation = tempBoxWorldRot,
                MicrowavePosition = RJ_Microwave.gameObject.transform.localPosition,
                MicrowaveRotation = RJ_Microwave.gameObject.transform.localEulerAngles,
            });
        }

        public override void Update()
        {
            bakingBehavior.IsDoorOpened = doorBehavior.IsDoorOpened;
            bakingBehavior.MicrowaveCurrentPowerWatt = powerBehavior.MicrowavePowerWatt;
            bakingBehavior.MicrowaveCurrentTime = timeBehavior.CurrentTime;
        }

        private void MicrowaveFoodLoad()
        {
            GameObject gameObject = ab.LoadAsset("MicrowaveFood.prefab") as GameObject;
            MicrowaveFood = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            Pizza = MicrowaveFood.transform.FindChild("pizza").gameObject;
            Pizza.SetActive(false);
            Macaroni = MicrowaveFood.transform.FindChild("macaroni").gameObject;
            Macaroni.SetActive(false);
            Sausages = MicrowaveFood.transform.FindChild("sausages").gameObject;
            Sausages.SetActive(false);
            PizzaRawMat = ab.LoadAsset("pizzaRaw.mat") as Material;
            PizzaBakedMat = ab.LoadAsset("pizzaBaked.mat") as Material;
            MacaroniRawMat = ab.LoadAsset("macaroniRaw.mat") as Material;
            MacaroniBakedMat = ab.LoadAsset("macaroniBaked.mat") as Material;
            SausagesRawMat = ab.LoadAsset("sausagesRaw.mat") as Material;
            SausagesBakedMat = ab.LoadAsset("sausagesBaked.mat") as Material;

        }
        private void MicrowaveLoad()
        {
            GameObject gameObject = ab.LoadAsset("microwave.prefab") as GameObject;
            RJ_Microwave = Object.Instantiate(gameObject);
            Object.Destroy(gameObject);
            if (saveData.MicrowaveBoxWorldUnboxed)
            {
                RJ_Microwave.SetActive(true);
            }
            else
            {
                RJ_Microwave.SetActive(false);
            }
            RJ_Microwave.transform.localPosition = saveData.MicrowavePosition;
            RJ_Microwave.transform.localEulerAngles = saveData.MicrowaveRotation;
            RJ_Microwave.tag = "PART";
            RJ_Microwave.layer = LayerMask.NameToLayer("Parts");

            MicrowaveDoor = RJ_Microwave.transform.FindChild("door").gameObject;
            doorBehavior = MicrowaveDoor.AddComponent<DoorBehavior>();

            KnobPower = RJ_Microwave.transform.FindChild("knob1").gameObject;
            powerBehavior = KnobPower.AddComponent<KnobPowerBehavior>();
            powerBehavior.KnobPower = KnobPower;

            KnobTime = RJ_Microwave.transform.FindChild("knob2").gameObject;
            timeBehavior = KnobTime.AddComponent<KnobTimeBehavior>();

            Plate = RJ_Microwave.transform.FindChild("plate").gameObject;
            bakingBehavior = Plate.AddComponent<MicrowaveBakingBehavior>();
            bakingBehavior.TimerKnob = KnobTime;
            bakingBehavior.PizzaObject = Pizza;
        }
        private void BoxWorldLoad()
        {
            if (!saveData.MicrowaveBoxWorldUnboxed)
            {
                GameObject gameObject1 = ab.LoadAsset("MicrowaveBox.prefab") as GameObject;
                boxWorld = Object.Instantiate(gameObject1);
                Object.Destroy(gameObject1);
                if (saveData.MicrowavePurchased)
                {
                    boxWorld.SetActive(true);
                }
                else
                {
                    boxWorld.SetActive(false);
                }
                tempBoxWorldPos = boxWorld.transform.position;
                tempBoxWorldRot = boxWorld.transform.eulerAngles;
                boxWorld.transform.position = saveData.MicrowaveBoxWorldPosition;
                boxWorld.transform.eulerAngles = saveData.MicrowaveBoxWorldRotation;
                boxWorld.tag = "PART";
                boxWorld.layer = LayerMask.NameToLayer("Parts");
                boxOpen = boxWorld.AddComponent<BoxOpenBehavior>();
                boxOpen.Microwave = RJ_Microwave;
            }
        }
        private void BoxStoreLoad()
        {
            if (!saveData.MicrowavePurchased)
            {
                GameObject gameObject2 = ab.LoadAsset("MicrowaveBox.prefab") as GameObject;
                boxStore = Object.Instantiate(gameObject2);
                Object.Destroy(gameObject2);
                BoxShopBehavior boxBehObj = boxStore.AddComponent<BoxShopBehavior>();
                boxBehObj.box1 = boxWorld;
            }
        }
        private static void MicrowaveReset()
        {
            SaveUtility.Remove();
        }
    }
}
