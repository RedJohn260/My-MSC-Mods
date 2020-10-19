using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace SecondHelmet
{
    public class SecondHelmet : Mod
    {
        public override string ID => "SecondHelmet"; //Your mod ID (unique)
        public override string Name => "SecondHelmet"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;


        private GameObject gameHelmet;
        private GameObject newHelmet;
        private GameObject FPSHelmet;
        private GameObject newFPSHelmet;
        private GameObject PlayerFPS;
        private AssetBundle ab;
        private Material helmetNewMat;
        private Material helmetVisorMat;
        private Sprite icon;
        private Keybind reloadTex = new Keybind("ReloadTex", "Reload Textures", KeyCode.F5);
        private Settings resetButton = new Settings("Second Helmet Reset", "Reset", ResetSave);
        private bool IsPurchased;
        private Texture2D overlayTex;
        private ScreenOverlay overlayCam;
        SaveData saveData = SaveUtility.Load<SaveData>();
        public override void OnNewGame()
        {
            ModConsole.Print("SECOND HELMET: NEW GAME STARTED, RESETTING MOD.");
            SaveUtility.Remove();
        }

        public override void OnLoad()
        {
            Keybind.Add(this, reloadTex);
            ab = LoadAssets.LoadBundle(this, "helmet.unity3d");
            Mesh helmetMesh = ab.LoadAsset<Mesh>("helmet.fbx") as Mesh;
            helmetNewMat = ab.LoadAsset<Material>("NewHelmet.mat") as Material;
            helmetVisorMat = ab.LoadAsset<Material>("HelmetVisorMat.mat") as Material;
            icon = ab.LoadAsset<Sprite>("icon.png") as Sprite;
            // Find and Instantiate Helmet and GameObject on Player
            gameHelmet = GameObject.Find("helmet(itemx)");
            FPSHelmet = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/Helmet");
            PlayerFPS = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera");

            newFPSHelmet = UnityEngine.Object.Instantiate(FPSHelmet);
            newFPSHelmet.name = "NewHelmet";
            newFPSHelmet.transform.SetParent(PlayerFPS.transform, false);

            newHelmet = UnityEngine.Object.Instantiate(gameHelmet);
            newHelmet.name = "Helmet(Clone)";

            // New PlayMaker BS
            PlayMakerFSM playMaker = newFPSHelmet.GetComponent<PlayMakerFSM>(); //Get PM on new Player Helmet
            FsmGameObject gameObject = playMaker.FsmVariables.FindFsmGameObject("Helmet"); // Find FsmGameObject Variable
            gameObject.Name = "NewHelmet"; // rename FsmGameObject so FsmState isn't confused
            gameObject.Value = newHelmet; // set new Helmet as a FsmGameObject, it's automatically changed in states as well
            FsmEvent equip = playMaker.FsmEvents.ElementAt(1); // select "EQUIP" event from all events
            FsmOwnerDefault owner = new FsmOwnerDefault(); // setup the new Default Owner
            owner.GameObject = gameObject; // declare the FsmGameObject as the owner object 
            owner.OwnerOption = OwnerDefaultOption.UseOwner; // set owner option
            FsmEventTarget target = new FsmEventTarget(); // setup the new FSMEvent target
            target.fsmComponent = playMaker; // set the default componnent
            target.gameObject = owner; // set the owner
            target.sendToChildren = true; // can send to children as well
            target.target = FsmEventTarget.EventTarget.FSMComponent; // set the target on entire FSMComponent
            target.fsmName = gameObject.Name; //set the FsmName


            PlayMakerFSM playMaker1 = newHelmet.GetComponents<PlayMakerFSM>()[0]; // get the PM on the Actual helmet
            FsmState pickupState = playMaker1.FsmStates.First(state => state.Name == "State 3"); // select state to modify
            List<FsmStateAction> actionList = pickupState.Actions.ToList(); // convert actions array to list
            actionList.Clear(); // clear the actions
            actionList.Insert(0, new SendEventByName // make new action and assign properties
            {
                eventTarget = target,
                sendEvent = equip.Name,
                delay = 0f,
                everyFrame = false
            });

            pickupState.Actions = actionList.ToArray(); // convert the list back to array and assign it to the PM actions

            // Set New Materials and Textures
            GameObject helmetShell = newHelmet.transform.FindChild("helmet_shell").gameObject;
            MeshFilter HelmetShellFilter = helmetShell.GetComponent<MeshFilter>();
            MeshRenderer HelmetShellRenderer = helmetShell.GetComponent<MeshRenderer>();
            HelmetShellFilter.mesh = helmetMesh;
            Material[] matarray = HelmetShellRenderer.materials;
            List<Material> matlist = matarray.ToList();
            helmetNewMat.mainTexture = LoadAssets.LoadTexture(this, "helmet.png");
            matlist.Insert(1, helmetNewMat);
            HelmetShellRenderer.materials = matlist.ToArray();

            GameObject helmetVisor = newHelmet.transform.FindChild("VisorPivot").transform.FindChild("helmet_visor").gameObject;
            helmetVisorMat.mainTexture = LoadAssets.LoadTexture(this, "helmet_visor.png");
            MeshRenderer visorRenderer = helmetVisor.GetComponent<MeshRenderer>();
            visorRenderer.material = helmetVisorMat;

            overlayCam = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/HandCamera").GetComponent<ScreenOverlay>();
            overlayTex = LoadAssets.LoadTexture(this, "helmet_vision.png");
            overlayCam.texture = overlayTex;

            AddToShopCatalog();
            ab.Unload(false);
            LoadData();
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
                position = newHelmet.transform.position,
                eularAngles = newHelmet.transform.eulerAngles,
                IsPurchased = IsPurchased
            });
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            if (reloadTex.GetKeybindDown())
            {
                helmetNewMat.mainTexture = LoadAssets.LoadTexture(this, "helmet.png");
                helmetVisorMat.mainTexture = LoadAssets.LoadTexture(this, "helmet_visor.png");
                overlayTex = LoadAssets.LoadTexture(this, "helmet_vision.png");
                overlayCam.texture = overlayTex;
            }
        }

        private void AddToShopCatalog()
        {
            if (GameObject.Find("Shop for mods") != null)
            {
                //newHelmet.SetActive(false); //Set this product to not active
                                                    //Shop for mods is installed get ModsShop.ShopItem component now (to variable called shop)
                ModsShop.ShopItem shop;
                shop = GameObject.Find("Shop for mods").GetComponent<ModsShop.ShopItem>();
                //Create product
                ModsShop.ProductDetails myProduct = new ModsShop.ProductDetails
                {
                    productName = "Motorcycle Helmet", //Name of your product
                    multiplePurchases = false, //Can this item be purchased more that once
                    productCategory = "Helmets", //Category of your product (currently has no effect)
                    productIcon = icon, //Icon for your product (optional)
                    productPrice = 1299 //Price of your product
                };
                //Add This product to shop
                if (!saveData.IsPurchased)
                {
                    shop.Add(this, myProduct, ModsShop.ShopType.Fleetari, FunctionToExectute, newHelmet);
                }
                
            }
        }
        public void FunctionToExectute(ModsShop.PurchaseInfo item)
        {
            //transform your product to one of spawn Loacations
            item.gameObject.transform.position = ModsShop.FleetariSpawnLocation.desk;
            //Set object to active
            item.gameObject.SetActive(true);
            //IF item can be purchased more than once, you can instantiate that object.
            //to read how much someone purchased use item.qty;
            if (item.qty > 0)
            {
                IsPurchased = true;
            }
            else
            {
                IsPurchased = false;
            }
        }

        public static void ResetSave()
        {
            SaveUtility.Remove();
        }

        public void LoadData()
        {
            
            newHelmet.transform.position = saveData.position;
            newHelmet.transform.eulerAngles = saveData.eularAngles;
            IsPurchased = saveData.IsPurchased;
            if (saveData.IsPurchased)
            {
                newHelmet.SetActive(true);
            }
            else
            {
                newHelmet.SetActive(false);
            }
        }
    }
}
