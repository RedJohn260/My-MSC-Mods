using System;
using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minimap
{
	// Token: 0x0200000B RID: 11
	public class Minimap : Mod
	{
		public override string ID => "Minimap"; //Your mod ID (unique)
		public override string Name => "Minimap"; //You mod name
		public override string Author => "RedJohn260"; //Your Username
		public override string Version => "1.0.3"; //Version

		// Set this to true if you will be load custom assets from Assets folder.
		// This will create subfolder in Assets folder for your mod.
		public override bool UseAssetsFolder => true;
		public override bool LoadInMenu => true;
		public override bool SecondPass => true;

		public readonly Keybind BigMapShow = new Keybind("ShowMapKey", "ShowMap", KeyCode.LeftControl, KeyCode.Keypad5);
		public readonly Keybind EnableMinimap = new Keybind("EnableMinimapKey", "Enable/Disable Minimap", KeyCode.LeftControl, KeyCode.Keypad9);
		private AssetBundle ab;
		public GameObject MinimapCanvas;
		public GameObject MinimapCam;
		public GameObject MinimapPlane;
		public GameObject MinimapPlaneNight;
		public GameObject BigMapCanvas;
		public GameObject BigMapCam;
		public GameObject MinimapPlaneBg;
		public GameObject MinimapPlaneBgNight;
		public GameObject MinimapRenderer;
		public GameObject BigMapRenderer;
		public GameObject BigMapChild1;
		public GameObject MinimapChild1;
		public Image MinimapChild1Image;
		public Button CloseButton;
		public GameObject MinimapOverlay;
		public GameObject BigMapOverlay;
		public Camera MinimapCameraComponent;
		public Camera BigMapCameraComponent;
		public GameObject GameCanvas;
		public GameObject PLAYER;
		public GameObject MAP;
		public GameObject SUN;
		public PlayMakerFSM Clock;
		public GameObject STORE;
		private MiniMapController MiniMapController;
		private BigMapController BigMapController;
		private CameraZoom CameraZoom;
		private DragWindow DragWindow;
		private FollowPlayerCam FollowPlayerCam;
		public GameObject PlayerIcon;
		public GameObject TeimoIcon;
		public GameObject WoodGuyIcon;
		private GameObject AirStripIcon;
		private GameObject BusIcon;
		private GameObject BusStopIcon;
		private GameObject CabinIcon;
		private GameObject ChurchIcon;
		private GameObject DanceHallIcon;
		private GameObject DumpIcon;
		private GameObject FarmerIcon;
		private GameObject FleetariIcon;
		private GameObject GrandmaIcon;
		private GameObject GreenCarIcon;
		private GameObject HomeIcon;
		private GameObject JokkeIcon;
		private GameObject MansionIcon;
		private GameObject PigmanIcon;
		private GameObject TrainIcon;
		private GameObject ShitIcon;
		private GameObject BoatGuyIcon;
		private GameObject InspectionIcon;
		private GameObject PoopDisposalIcon;
		public GameObject JunkCarIcon;
		private GameObject AirStrip;
		private GameObject Bus;
		private GameObject BusStop1;
		private GameObject BusStop2;
		private GameObject BusStop3;
		private GameObject BusStop4;
		private GameObject BusStop5;
		private GameObject BusStop6;
		private GameObject Cabin;
		private GameObject Church;
		private GameObject DanceHall;
		private GameObject Dump;
		private GameObject Farmer;
		private GameObject Fleetari;
		private GameObject Grandma;
		private GameObject GreenCar;
		private GameObject Home;
		private GameObject JokkeOld;
		private GameObject JokkeNew;
		private GameObject Mansion;
		private GameObject Pigman;
		private GameObject Train;
		private GameObject BoatGuy;
		private GameObject ShitHouse1;
		private GameObject ShitHouse2;
		private GameObject ShitHouse3;
		private GameObject ShitHouse4;
		private GameObject ShitHouse5;
		private GameObject WoodGuy;
		private GameObject Inspection;
		private GameObject PoopDisposal;
		private GameObject StoreIconObj;
		private GameObject PlayerIconObj;
		public GameObject WoodGuyIconObj;
		private GameObject AirStripIconObj;
		private GameObject BusIconObj;
		private GameObject BusStopIconObj1;
		private GameObject BusStopIconObj2;
		private GameObject BusStopIconObj3;
		private GameObject BusStopIconObj4;
		private GameObject BusStopIconObj5;
		private GameObject BusStopIconObj6;
		private GameObject CabinIconObj;
		private GameObject ChurchIconObj;
		private GameObject DanceHallIconObj;
		private GameObject DumpIconObj;
		private GameObject FarmerIconObj;
		private GameObject FleetariIconObj;
		private GameObject GrandmaIconObj;
		private GameObject GreenCarIconObj;
		private GameObject HomeIconObj;
		private GameObject JokkeIconObj1;
		private GameObject JokkeIconObj2;
		private GameObject MansionIconObj;
		private GameObject PigmanIconObj;
		private GameObject TrainIconObj;
		private GameObject BoatGuyIconObj;
		private GameObject ShitIconObj1;
		private GameObject ShitIconObj2;
		private GameObject ShitIconObj3;
		private GameObject ShitIconObj4;
		private GameObject ShitIconObj5;
		private GameObject InspectionIconObj;
		private GameObject PoopDisposalIconObj;
		public bool IsNight;
		public float MinimapZoomValue;
		public bool IsMinimapOpened;
		public bool IsBigMapKeyPressed = false;
		private bool IsBigMapOpened = false;
		private bool IsBigmapClosed = false;
		private FsmString _currentvehicle;
		private bool IsTangerineInstalled;
		private JunkCarIcons JunkCarIcons = new JunkCarIcons();
		private float currentMinimapZoom;

		

		public void LoadAbGBInstantiate()
		{
			ab = LoadAssets.LoadBundle(this, "minimap.unity3d");
			GameObject gameObject = ab.LoadAsset("MinimapCanvas.prefab") as GameObject;
			GameObject gameObject2 = ab.LoadAsset("MinimapCam.prefab") as GameObject;
			GameObject gameObject3 = ab.LoadAsset("MinimapPlane.prefab") as GameObject;
			GameObject gameObject4 = ab.LoadAsset("MinimapPlaneNight.prefab") as GameObject;
			GameObject gameObject5 = ab.LoadAsset("BigMap.prefab") as GameObject;
			GameObject gameObject6 = ab.LoadAsset("BigMapCam.prefab") as GameObject;
			GameObject gameObject7 = ab.LoadAsset("MinimapPlaneBg.prefab") as GameObject;
			GameObject gameObject8 = ab.LoadAsset("MinimapPlaneBgNight.prefab") as GameObject;
			GameObject gameObject9 = ab.LoadAsset("JunkCarIcon.prefab") as GameObject;
			PlayerIcon = (ab.LoadAsset("PlayerIcon.prefab") as GameObject);
			TeimoIcon = (ab.LoadAsset("TeimoIcon.prefab") as GameObject);
			WoodGuyIcon = (ab.LoadAsset("WoodGuyIcon.prefab") as GameObject);
			AirStripIcon = (ab.LoadAsset("AirStripIcon.prefab") as GameObject);
			BusIcon = (ab.LoadAsset("BusIcon.prefab") as GameObject);
			BusStopIcon = (ab.LoadAsset("BusStopIcon.prefab") as GameObject);
			CabinIcon = (ab.LoadAsset("CabinIcon.prefab") as GameObject);
			ChurchIcon = (ab.LoadAsset("ChurchIcon.prefab") as GameObject);
			DanceHallIcon = (ab.LoadAsset("DanceHallIcon.prefab") as GameObject);
			DumpIcon = (ab.LoadAsset("DumpIcon.prefab") as GameObject);
			FarmerIcon = (ab.LoadAsset("FarmerIcon.prefab") as GameObject);
			FleetariIcon = (ab.LoadAsset("FleetariIcon.prefab") as GameObject);
			GrandmaIcon = (ab.LoadAsset("GrandmaIcon.prefab") as GameObject);
			GreenCarIcon = (ab.LoadAsset("GreenCarIcon.prefab") as GameObject);
			HomeIcon = (ab.LoadAsset("HomeIcon.prefab") as GameObject);
			JokkeIcon = (ab.LoadAsset("JokkeIcon.prefab") as GameObject);
			MansionIcon = (ab.LoadAsset("MansionIcon.prefab") as GameObject);
			PigmanIcon = (ab.LoadAsset("PigmanIcon.prefab") as GameObject);
			TrainIcon = (ab.LoadAsset("TrainIcon.prefab") as GameObject);
			ShitIcon = (ab.LoadAsset("ShitIcon.prefab") as GameObject);
			BoatGuyIcon = (ab.LoadAsset("BoatGuyIcon.prefab") as GameObject);
			InspectionIcon = (ab.LoadAsset("InspectionIcon.prefab") as GameObject);
			PoopDisposalIcon = (ab.LoadAsset("PoopDisposalIcon.prefab") as GameObject);
			JunkCarIcon = UnityEngine.Object.Instantiate<GameObject>(gameObject9);
			MinimapCanvas = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			MinimapCam = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
			MinimapPlane = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
			MinimapPlaneNight = UnityEngine.Object.Instantiate<GameObject>(gameObject4);
			BigMapCanvas = UnityEngine.Object.Instantiate<GameObject>(gameObject5);
			BigMapCam = UnityEngine.Object.Instantiate<GameObject>(gameObject6);
			MinimapPlaneBg = UnityEngine.Object.Instantiate<GameObject>(gameObject7);
			MinimapPlaneBgNight = UnityEngine.Object.Instantiate<GameObject>(gameObject8);
			UnityEngine.Object.Destroy(gameObject);
			UnityEngine.Object.Destroy(gameObject2);
			UnityEngine.Object.Destroy(gameObject3);
			UnityEngine.Object.Destroy(gameObject4);
			UnityEngine.Object.Destroy(gameObject5);
			UnityEngine.Object.Destroy(gameObject6);
			UnityEngine.Object.Destroy(gameObject7);
			UnityEngine.Object.Destroy(gameObject8);
			UnityEngine.Object.Destroy(gameObject9);
		}

		public void PrefabFind()
		{
			BigMapCameraComponent = BigMapCam.GetComponent<Camera>();
			MinimapChild1Image = MinimapCanvas.transform.FindChild("mask").gameObject.GetComponent<Image>();
			MinimapChild1 = MinimapCanvas.transform.FindChild("mask").gameObject;
			MinimapCameraComponent = MinimapCam.GetComponent<Camera>();
			BigMapOverlay = BigMapCanvas.transform.FindChild("Mask").transform.FindChild("MapMask").transform.FindChild("Map").FindChild("Vignete").gameObject;
			MinimapOverlay = MinimapCanvas.transform.FindChild("mask").transform.FindChild("MinimapRenderMask").transform.FindChild("MinimapRender").transform.FindChild("VigneteOverlay").gameObject;
			CloseButton = BigMapCanvas.transform.FindChild("Mask").transform.FindChild("CloseMap").GetComponent<Button>();
			BigMapChild1 = BigMapCanvas.transform.FindChild("Mask").gameObject;
			BigMapRenderer = BigMapCanvas.transform.FindChild("Mask").transform.FindChild("MapMask").transform.FindChild("Map").gameObject;
			MinimapRenderer = MinimapCanvas.transform.FindChild("mask").transform.FindChild("MinimapRenderMask").transform.FindChild("MinimapRender").gameObject;
		}

		public void MSCGameObjects()
		{
			SUN = GameObject.Find("MAP/SUN/Pivot/SUN");
			MAP = GameObject.Find("MAP");
			PLAYER = GameObject.Find("PLAYER");
			GameCanvas = GameObject.Find("Systems");
			Clock = SUN.GetComponent<PlayMakerFSM>();
		}

		public void AddScripts()
		{
			MiniMapController = MinimapRenderer.AddComponent<MiniMapController>();
			BigMapController = BigMapRenderer.AddComponent<BigMapController>();
			CameraZoom = BigMapCam.AddComponent<CameraZoom>();
			DragWindow = MinimapChild1.AddComponent<DragWindow>();
			FollowPlayerCam = MinimapCam.AddComponent<FollowPlayerCam>();
		}

		public void GoTransforms()
		{
			MiniMapController.mapCamera = MinimapCameraComponent;
			MiniMapController.playerPos = PLAYER.transform;
			BigMapController.mapCamera = BigMapCameraComponent;
			BigMapController.playerPos = PLAYER.transform;
			CameraZoom.BigCam = BigMapCameraComponent;
			CameraZoom.map = BigMapCanvas;
			DragWindow.backgroundImage = MinimapChild1Image;
			DragWindow.canvas = MinimapCanvas.GetComponent<Canvas>();
			DragWindow.dragTransform = MinimapChild1.GetComponent<RectTransform>();
			FollowPlayerCam.player = PLAYER.transform;
			CloseButton.onClick.AddListener(new UnityAction(OpenBigMap));
		}

		public void MSCIconGameObjects()
		{
			BusStop1 = GameObject.Find("MAP/BusStop");
			BusStop2 = GameObject.Find("MAP/BusStop 1");
			BusStop3 = GameObject.Find("PERAJARVI/BusStop 2");
			BusStop4 = GameObject.Find("TRAFFIC/BusStopKesseli");
			BusStop5 = GameObject.Find("TRAFFIC/BusStopLoppe");
			BusStop6 = GameObject.Find("TRAFFIC/BusStopRykipohja");
			Inspection = GameObject.Find("INSPECTION");
			PoopDisposal = GameObject.Find("WATERFACILITY/MESH");
			STORE = GameObject.Find("STORE");
			WoodGuy = GameObject.Find("JOBS/HouseWood1");
			AirStrip = GameObject.Find("DRAGRACE");
			Bus = GameObject.Find("BUS");
			Cabin = GameObject.Find("COTTAGE");
			Church = GameObject.Find("PERAJARVI/CHURCH");
			DanceHall = GameObject.Find("DANCEHALL");
			Dump = GameObject.Find("LANDFILL/LandFillCollider");
			Farmer = GameObject.Find("JOBS/Farm");
			Fleetari = GameObject.Find("REPAIRSHOP");
			Grandma = GameObject.Find("JOBS/Mummola");
			GreenCar = GameObject.Find("TRAFFIC/VehiclesDirtRoad").transform.FindChild("Rally").transform.FindChild("FITTAN").gameObject;
			Home = GameObject.Find("YARD/Building");
			JokkeOld = GameObject.Find("JOBS/HouseDrunk");
			JokkeNew = GameObject.Find("JOBS/HouseDrunkNew");
			Mansion = GameObject.Find("MAP/Buildings/DINGONBIISI");
			Pigman = GameObject.Find("CABIN/Cabin");
			Train = GameObject.Find("TRAIN");
			BoatGuy = GameObject.Find("TRAFFIC/Lake/AIboat1");
			ShitHouse1 = GameObject.Find("JOBS/HouseShit1");
			ShitHouse2 = GameObject.Find("JOBS/HouseShit2");
			ShitHouse3 = GameObject.Find("JOBS/HouseShit3");
			ShitHouse4 = GameObject.Find("JOBS/HouseShit4");
			ShitHouse5 = GameObject.Find("JOBS/HouseShit5");
		}

		public void IconNewGameObjects()
		{
			ShitIconObj1 = new GameObject();
			ShitIconObj1.name = "ShitIconObj1";
			ShitIconObj1.layer = 31;
			ShitIconObj1.transform.SetParent(ShitHouse1.transform, false);
			ShitIconObj2 = new GameObject();
			ShitIconObj2.name = "ShitIconObj2";
			ShitIconObj2.layer = 31;
			ShitIconObj2.transform.SetParent(ShitHouse2.transform, false);
			ShitIconObj3 = new GameObject();
			ShitIconObj3.name = "ShitIconObj3";
			ShitIconObj3.layer = 31;
			ShitIconObj3.transform.SetParent(ShitHouse3.transform, false);
			ShitIconObj4 = new GameObject();
			ShitIconObj4.name = "ShitIconObj4";
			ShitIconObj4.layer = 31;
			ShitIconObj4.transform.SetParent(ShitHouse4.transform, false);
			ShitIconObj5 = new GameObject();
			ShitIconObj5.name = "ShitIconObj5";
			ShitIconObj5.layer = 31;
			ShitIconObj5.transform.SetParent(ShitHouse5.transform, false);
			InspectionIconObj = new GameObject();
			InspectionIconObj.name = "InspectionIconObj";
			InspectionIconObj.layer = 31;
			InspectionIconObj.transform.SetParent(Inspection.transform, false);
			PoopDisposalIconObj = new GameObject();
			PoopDisposalIconObj.name = "PoopDisposalIconObj";
			PoopDisposalIconObj.layer = 31;
			PoopDisposalIconObj.transform.SetParent(PoopDisposal.transform, false);
			BusStopIconObj1 = new GameObject();
			BusStopIconObj1.name = "BusStopIconObj1";
			BusStopIconObj1.layer = 31;
			BusStopIconObj1.transform.SetParent(BusStop1.transform, false);
			BusStopIconObj2 = new GameObject();
			BusStopIconObj2.name = "BusStopIconObj2";
			BusStopIconObj2.layer = 31;
			BusStopIconObj2.transform.SetParent(BusStop2.transform, false);
			BusStopIconObj3 = new GameObject();
			BusStopIconObj3.name = "BusStopIconObj3";
			BusStopIconObj3.layer = 31;
			BusStopIconObj3.transform.SetParent(BusStop3.transform, false);
			BusStopIconObj4 = new GameObject();
			BusStopIconObj4.name = "BusStopIconObj4";
			BusStopIconObj4.layer = 31;
			BusStopIconObj4.transform.SetParent(BusStop4.transform, false);
			BusStopIconObj5 = new GameObject();
			BusStopIconObj5.name = "BusStopIconObj5";
			BusStopIconObj5.layer = 31;
			BusStopIconObj5.transform.SetParent(BusStop5.transform, false);
			BusStopIconObj6 = new GameObject();
			BusStopIconObj6.name = "BusStopIconObj6";
			BusStopIconObj6.layer = 31;
			BusStopIconObj6.transform.SetParent(BusStop6.transform, false);
			StoreIconObj = new GameObject();
			StoreIconObj.name = "StoreIconObj";
			StoreIconObj.layer = 31;
			StoreIconObj.transform.SetParent(STORE.transform, false);
			WoodGuyIconObj = new GameObject();
			WoodGuyIconObj.name = "WoodGuyIconObj";
			WoodGuyIconObj.layer = 31;
			WoodGuyIconObj.transform.SetParent(WoodGuy.transform, false);
			AirStripIconObj = new GameObject();
			AirStripIconObj.name = "AirStripIconObj";
			AirStripIconObj.layer = 31;
			AirStripIconObj.transform.SetParent(AirStrip.transform, false);
			CabinIconObj = new GameObject();
			CabinIconObj.name = "CabinIconObj";
			CabinIconObj.layer = 31;
			CabinIconObj.transform.SetParent(Cabin.transform, false);
			ChurchIconObj = new GameObject();
			ChurchIconObj.name = "ChurchIconObj";
			ChurchIconObj.layer = 31;
			ChurchIconObj.transform.SetParent(Church.transform, false);
			DanceHallIconObj = new GameObject();
			DanceHallIconObj.name = "DanceHallIconObj";
			DanceHallIconObj.layer = 31;
			DanceHallIconObj.transform.SetParent(DanceHall.transform, false);
			DumpIconObj = new GameObject();
			DumpIconObj.name = "DumpIconObj";
			DumpIconObj.layer = 31;
			DumpIconObj.transform.SetParent(Dump.transform, false);
			FarmerIconObj = new GameObject();
			FarmerIconObj.name = "FarmerIconObj";
			FarmerIconObj.layer = 31;
			FarmerIconObj.transform.SetParent(Farmer.transform, false);
			FleetariIconObj = new GameObject();
			FleetariIconObj.name = "FleetariIconObj";
			FleetariIconObj.layer = 31;
			FleetariIconObj.transform.SetParent(Fleetari.transform, false);
			GrandmaIconObj = new GameObject();
			GrandmaIconObj.name = "GrandmaIconObj";
			GrandmaIconObj.layer = 31;
			GrandmaIconObj.transform.SetParent(Grandma.transform, false);
			HomeIconObj = new GameObject();
			HomeIconObj.name = "GreenCarIconObj";
			HomeIconObj.layer = 31;
			HomeIconObj.transform.SetParent(Home.transform, false);
			JokkeIconObj1 = new GameObject();
			JokkeIconObj1.name = "JokkeIconObj1";
			JokkeIconObj1.layer = 31;
			JokkeIconObj1.transform.SetParent(JokkeOld.transform, false);
			JokkeIconObj2 = new GameObject();
			JokkeIconObj2.name = "JokkeIconObj2";
			JokkeIconObj2.layer = 31;
			JokkeIconObj2.transform.SetParent(JokkeNew.transform, false);
			MansionIconObj = new GameObject();
			MansionIconObj.name = "MansionIconObj";
			MansionIconObj.layer = 31;
			MansionIconObj.transform.SetParent(Mansion.transform, false);
			PigmanIconObj = new GameObject();
			PigmanIconObj.name = "PigmanIconObj";
			PigmanIconObj.layer = 31;
			PigmanIconObj.transform.SetParent(Pigman.transform, false);
			GreenCarIconObj = new GameObject();
			GreenCarIconObj.name = "GreenCarIconObj";
			GreenCarIconObj.layer = 31;
			GreenCarIconObj.transform.SetParent(GreenCar.transform, false);
			BusIconObj = new GameObject();
			BusIconObj.name = "BusIconObj";
			BusIconObj.layer = 31;
			BusIconObj.transform.SetParent(Bus.transform, false);
			TrainIconObj = new GameObject();
			TrainIconObj.name = "TrainIconObj";
			TrainIconObj.layer = 31;
			TrainIconObj.transform.SetParent(Train.transform, false);
			BoatGuyIconObj = new GameObject();
			BoatGuyIconObj.name = "BoatGuyIconObj";
			BoatGuyIconObj.layer = 31;
			BoatGuyIconObj.transform.SetParent(BoatGuy.transform, false);
			PlayerIconObj = new GameObject();
			PlayerIconObj.name = "PlayerIconObj";
			PlayerIconObj.layer = 31;
			PlayerIconObj.transform.SetParent(PLAYER.transform, false);
		}

		public void HandleStaticIcons()
		{
			IconAdding.AddIcon(BusStopIconObj1, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusStopIconObj2, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusStopIconObj3, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusStopIconObj4, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusStopIconObj5, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusStopIconObj6, BusStopIcon.GetComponent<Image>());
			IconAdding.AddIcon(WoodGuyIconObj, WoodGuyIcon.GetComponent<Image>());
			IconAdding.AddIcon(ShitIconObj1, ShitIcon.GetComponent<Image>());
			IconAdding.AddIcon(ShitIconObj2, ShitIcon.GetComponent<Image>());
			IconAdding.AddIcon(ShitIconObj3, ShitIcon.GetComponent<Image>());
			IconAdding.AddIcon(ShitIconObj4, ShitIcon.GetComponent<Image>());
			IconAdding.AddIcon(ShitIconObj5, ShitIcon.GetComponent<Image>());
			IconAdding.AddIcon(InspectionIconObj, InspectionIcon.GetComponent<Image>());
			IconAdding.AddIcon(PoopDisposalIconObj, PoopDisposalIcon.GetComponent<Image>());
			IconAdding.AddIcon(StoreIconObj, TeimoIcon.GetComponent<Image>());
			IconAdding.AddIcon(AirStripIconObj, AirStripIcon.GetComponent<Image>());
			IconAdding.AddIcon(CabinIconObj, CabinIcon.GetComponent<Image>());
			IconAdding.AddIcon(ChurchIconObj, ChurchIcon.GetComponent<Image>());
			IconAdding.AddIcon(DanceHallIconObj, DanceHallIcon.GetComponent<Image>());
			IconAdding.AddIcon(DumpIconObj, DumpIcon.GetComponent<Image>());
			IconAdding.AddIcon(FarmerIconObj, FarmerIcon.GetComponent<Image>());
			IconAdding.AddIcon(FleetariIconObj, FleetariIcon.GetComponent<Image>());
			IconAdding.AddIcon(GrandmaIconObj, GrandmaIcon.GetComponent<Image>());
			IconAdding.AddIcon(HomeIconObj, HomeIcon.GetComponent<Image>());
			IconAdding.AddIcon(JokkeIconObj1, JokkeIcon.GetComponent<Image>());
			IconAdding.AddIcon(MansionIconObj, MansionIcon.GetComponent<Image>());
			IconAdding.AddIcon(PigmanIconObj, PigmanIcon.GetComponent<Image>());
			IconAdding.AddIcon(GreenCarIconObj, GreenCarIcon.GetComponent<Image>());
			IconAdding.AddIcon(TrainIconObj, TrainIcon.GetComponent<Image>());
			IconAdding.AddIcon(BoatGuyIconObj, BoatGuyIcon.GetComponent<Image>());
			IconAdding.AddIcon(BusIconObj, BusIcon.GetComponent<Image>());
			IconAdding.AddIcon(PlayerIconObj, PlayerIcon.GetComponent<Image>());
		}

		
		public void MinimapZoom()
		{
		}

		public void EnableDisableMinimap()
		{
			IsMinimapOpened = !IsMinimapOpened;
			bool isMinimapOpened = IsMinimapOpened;
			if (isMinimapOpened)
			{
				MinimapCanvas.SetActive(true);
			}
			else
			{
				MinimapCanvas.SetActive(false);
			}
		}

		public void OpenBigMap()
		{
			IsBigMapKeyPressed = !IsBigMapKeyPressed;
			if (IsBigMapKeyPressed)
			{
				IsBigMapOpened = false;
				if (!IsBigMapOpened)
				{
					BigMapCanvas.SetActive(value: true);
					CameraZoom.ResetPos();
					FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = true;
					IsBigMapOpened = true;
					IsBigmapClosed = false;
				}
			}
			else
			{
				IsBigmapClosed = false;
				if (!IsBigmapClosed)
				{
					BigMapCanvas.SetActive(value: false);
					CameraZoom.ResetPos();
					FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
					IsBigMapOpened = false;
					IsBigmapClosed = true;
				}
			}
		}

		public override void OnMenuLoad()
		{
			IsTangerineInstalled = ModLoader.IsModPresent("TangerinePickup");
			bool isTangerineInstalled = IsTangerineInstalled;
			if (isTangerineInstalled)
			{
				ModConsole.Print("[MINIMAP]: <color=green>TangerinePickup Mod Found!</color>");
			}
			else
			{
				ModConsole.Print("[MINIMAP]: <color=orange>TangerinePickup Mod Not Found!</color>");
				ModConsole.Print("[MINIMAP]: <color=green>Continuing...</color>");
			}
		}
		public bool IsNewGame;
		public override void OnNewGame()
		{
			ModConsole.Print("<color=white>[MINIMAP]: </color><color=orange>New Game Started, Resetting Mod...</color>");
			SaveUtility.Remove();
			JunkCarIcons.newGameStarted = true;
		}
		public override void OnLoad()
		{
			Keybind.Add(this, BigMapShow);
			Keybind.Add(this, EnableMinimap);
			LoadAbGBInstantiate();
			PrefabFind();
			JunkCarIcons.FindJunkCars();
			JunkCarIcons.CreateNewGameObjects();
			MSCGameObjects();
			MSCIconGameObjects();
			AddScripts();
			GoTransforms();
			IconNewGameObjects();
			HandleStaticIcons();
			BigMapCanvas.SetActive(false);
			_currentvehicle = PlayMakerGlobals.Instance.Variables.FindFsmString("PlayerCurrentVehicle");
			SaveUtilLoad();
			JunkCarIcons.CheckForDiscoveredCars();
			ab.Unload(false);
			JunkCarIcons.newGameStarted = false;
			ModConsole.Print("<color=white>[MINIMAP]: </color><color=green>Succesfully Loaded</color>");
		}
		public override void SecondPassOnLoad()
		{
			//JunkCarIcons.FindJunkCars();
			//JunkCarIcons.CreateNewGameObjects();
			//JunkCarIcons.CheckForDiscoveredCars();
			//SaveUtilLoad();
			
		}
		public override void OnSave()
		{
			SaveUtility.Save<SaveData>(new SaveData
			{
				MinimapPosSave = MinimapChild1.GetComponent<RectTransform>().anchoredPosition,
				isMinimpOpened = IsMinimapOpened,
				isBigOpened = IsBigMapOpened,
				IsNightSave = IsNight,
				JunkCar1Discovered = JunkCarIcons.car1discovered,
				JunkCar2Discovered = JunkCarIcons.car2discovered,
				JunkCar3Discovered = JunkCarIcons.car3discovered,
				JunkCar4Discovered = JunkCarIcons.car4discovered
			});
		}

		public override void OnGUI()
		{
		}

		private void TangerineTruck()
		{
			if (IsTangerineInstalled && _currentvehicle.Value == "Tangerine")
			{
				int num = (int)GameObject.Find("TangerinePickup(Clone)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				if (40 <= num && num < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num * 1E-05f);
				}
				else if (45 >= num && num > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num * 0.0007f);
				}
			}
		}


		private void MinimapZoomEffect()
		{
			currentMinimapZoom = MinimapCameraComponent.orthographicSize;
			if (!(currentMinimapZoom >= 150f) || !(currentMinimapZoom <= 700f))
			{
				return;
			}
			TangerineTruck();
			if (_currentvehicle.Value == Vehicles.Hayosiko.ToString())
			{
				int num = (int)GetVehicleSpeed.VehSpeed(Vehicles.Hayosiko);
				if (35 <= num && num < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num * 1E-05f);
				}
				else if (45 >= num && num > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Satsuma.ToString())
			{
				int num2 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Satsuma);
				if (40 <= num2 && num2 < 300)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num2 * 1E-05f);
				}
				else if (45 >= num2 && num2 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num2 * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Ruscko.ToString())
			{
				int num3 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Ruscko);
				if (35 <= num3 && num3 < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num3 * 1E-05f);
				}
				else if (45 >= num3 && num3 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num3 * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Jonnez.ToString())
			{
				int num4 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Jonnez);
				if (35 <= num4 && num4 < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num4 * 1E-05f);
				}
				else if (45 >= num4 && num4 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num4 * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Gifu.ToString())
			{
				int num5 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Gifu);
				if (35 <= num5 && num5 < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num5 * 1E-05f);
				}
				else if (45 >= num5 && num5 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num5 * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Kekmet.ToString())
			{
				int num6 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Kekmet);
				if (35 <= num6 && num6 < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num6 * 1E-05f);
				}
				else if (45 >= num6 && num6 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num6 * 0.0007f);
				}
			}
			if (_currentvehicle.Value == Vehicles.Ferndale.ToString())
			{
				int num7 = (int)GetVehicleSpeed.VehSpeed(Vehicles.Ferndale);
				if (35 <= num7 && num7 < 200)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 700f, (float)num7 * 1E-05f);
				}
				else if (45 >= num7 && num7 > 0)
				{
					MinimapCameraComponent.orthographicSize = Mathf.Lerp(MinimapCameraComponent.orthographicSize, 150f, (float)num7 * 0.0007f);
				}
			}
		}
		public void MinimapTimeTexture()
		{
			IsNight = Clock.FsmVariables.GetFsmBool("Night").Value;
			if (!IsNight)
			{
				MinimapPlane.SetActive(value: true);
				MinimapPlaneBg.SetActive(value: true);
				MinimapPlaneNight.SetActive(value: false);
				MinimapPlaneBgNight.SetActive(value: false);
				MinimapOverlay.SetActive(value: true);
				BigMapOverlay.SetActive(value: true);
			}
			else if (IsNight)
			{
				MinimapPlane.SetActive(value: false);
				MinimapPlaneBg.SetActive(value: false);
				MinimapPlaneNight.SetActive(value: true);
				MinimapPlaneBgNight.SetActive(value: true);
				MinimapOverlay.SetActive(value: false);
				BigMapOverlay.SetActive(value: false);
			}
		}
		public override void Update()
		{
			MinimapTimeTexture();
			JunkCarIcons.GetCarPositions();
			JunkCarIcons.GetDistanceAndSetIcons();
			JunkCarIcons.GetDistanceAndRemoveIcons();
			if (EnableMinimap.GetKeybindDown())
			{
				EnableDisableMinimap();
			}
			if (BigMapShow.GetKeybindDown())
			{
				OpenBigMap();
			}
			MinimapZoomEffect();
		}

		private void SaveUtilLoad()
		{
			SaveData saveData = SaveUtility.Load<SaveData>();
			MinimapChild1.GetComponent<RectTransform>().anchoredPosition = saveData.MinimapPosSave;
			IsMinimapOpened = saveData.isMinimpOpened;
			IsBigMapOpened = saveData.isBigOpened;
			IsNight = saveData.IsNightSave;
			JunkCarIcons.car1discovered = saveData.JunkCar1Discovered;
			JunkCarIcons.car2discovered = saveData.JunkCar2Discovered;
			JunkCarIcons.car3discovered = saveData.JunkCar3Discovered;
			JunkCarIcons.car4discovered = saveData.JunkCar4Discovered;
		}

		
	}
}
