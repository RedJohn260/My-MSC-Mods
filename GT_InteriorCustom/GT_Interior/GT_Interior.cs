using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System;
using System.IO;

namespace GT_Interior
{
    public class GT_Interior : Mod
    {
        public override string ID => "GT_Interior"; //Your mod ID (unique)
        public override string Name => "GT_Interior"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;
        private AssetBundle ab;
        private Material BodyM;
        private Material Panels;
        private Material Seats;
        private GameObject meters;
        private GameObject column;
        private GameObject ladica;
        private Material gtdash;
        private float specular_range = 1;
        private float specular_smoothness = 1;
        public string message = "<b><color=green>Original Mod Link:</color></b> https://www.nexusmods.com/mysummercar/mods/91 <color=orange>Any re-upload of this mod is strictly prohibited. I do not support any of my mods uploaded to different sites without my permission.</color> <b><color=green>Sincerely RedJohn260.</color></b>";
        public string tittle = "Satsuma GT Interior";
        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gtinterior.unity3d");
            BodyM = ab.LoadAsset("BodyM.mat") as Material;
            Panels = ab.LoadAsset("Panels.mat") as Material;
            Seats = ab.LoadAsset("Seats.mat") as Material;
            gtdash = ab.LoadAsset("gtdash.mat") as Material;

            GameObject gameObject1 = ab.LoadAsset("gtcolumn.prefab") as GameObject;
            GameObject gameObject2 = ab.LoadAsset("gtmeters.prefab") as GameObject;
            GameObject gameObject3 = ab.LoadAsset("ladica.prefab") as GameObject;

            column = UnityEngine.Object.Instantiate(gameObject1);
            meters = UnityEngine.Object.Instantiate(gameObject2);
            ladica = UnityEngine.Object.Instantiate(gameObject3);

            UnityEngine.Object.Destroy(gameObject1);
            UnityEngine.Object.Destroy(gameObject2);
            UnityEngine.Object.Destroy(gameObject3);

            ab.Unload(false);

            //Check for steam
            bool SteamActive = ModLoader.CheckSteam();
            if (SteamActive)
            {
                Debug.Log(" " + "Steam client found!");
            }
            else
            {
                Debug.LogError(" " + "Steam client not found!");
            }

            //Get Materials from prefabs
            Material columnmat = column.GetComponent<MeshRenderer>().material;
            Material metersmat = meters.GetComponent<MeshRenderer>().material;
            Material ladicamat = ladica.GetComponent<MeshRenderer>().material;

            //Assign Custom Texture To Materials
            try
            {
                //diffuse
                BodyM.mainTexture = LoadAssets.LoadTexture(this, "textures/body_roof.png");
                Panels.mainTexture = LoadAssets.LoadTexture(this, "textures/door_panels_floor.png");
                Seats.mainTexture = LoadAssets.LoadTexture(this, "textures/seats.png");
                gtdash.mainTexture = LoadAssets.LoadTexture(this, "textures/dashboard.png");
                columnmat.mainTexture = LoadAssets.LoadTexture(this, "textures/column.png");
                metersmat.mainTexture = LoadAssets.LoadTexture(this, "textures/dashboard_meters.png");
                ladicamat.mainTexture = LoadAssets.LoadTexture(this, "textures/glovebox.png");

                //specular
                BodyM.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/body_roof_spec.png"));
                Panels.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/door_panels_floor_spec.png"));
                Seats.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/seats_spec.png"));
                gtdash.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/dashboard_spec.png"));
                columnmat.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/column_spec.png"));
                metersmat.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/dashboard_meters_spec.png"));
                ladicamat.SetTexture("_MetallicGlossMap", LoadAssets.LoadTexture(this, "textures/glovebox_spec.png"));

                //normal
                BodyM.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/body_roof_norm.png"));
                Panels.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/door_panels_floor_norm.png"));
                Seats.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/seats_norm.png"));
                gtdash.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/dashboard_norm.png"));
                columnmat.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/column_norm.png"));
                metersmat.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/dashboard_meters_norm.png"));
                ladicamat.SetTexture("_BumpMap", LoadAssets.LoadTexture(this, "textures/glovebox_norm.png"));
            }
            catch (FileNotFoundException e)
            {
                Debug.LogException(e);
                ModConsole.Error(" " + e.ToString());
            }
            
            //Set Materials to prefabs
            column.GetComponent<MeshRenderer>().material = columnmat;
            meters.GetComponent<MeshRenderer>().material = metersmat;
            ladica.GetComponent<MeshRenderer>().material = ladicamat;

            //panel doorf left
            GameObject.Find("door left(Clone)/panel_door_left1").GetComponent<MeshRenderer>().material = Panels;
            //panel doorf right
            GameObject.Find("door right(Clone)/panel_door_right1").GetComponent<MeshRenderer>().material = Panels;
            //panel doorr left
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Trim/panel_door_left2").GetComponent<MeshRenderer>().material = Panels;
            //panel doorr right
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Trim/panel_door_right2").GetComponent<MeshRenderer>().material = Panels;
            //sunvisors
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Sunvisors/SunvisorLeft/mesh").GetComponent<MeshRenderer>().material = BodyM;
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Sunvisors/SunvisorRight/mesh").GetComponent<MeshRenderer>().material = BodyM;
            //flormats
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Trim/panel_floormat1").GetComponent<MeshRenderer>().material = Panels;
            GameObject.Find("SATSUMA(557kg, 248)/Interior/Trim/panel_floormat2").GetComponent<MeshRenderer>().material = Panels;
            //roofing
            GameObject.Find("SATSUMA(557kg, 248)/Body/car body masse(xxxxx)").GetComponent<MeshRenderer>().material = BodyM;
            //rear seat
             var seat1 = GameObject.Find("seat rear(Clone)").transform.GetChild(1).gameObject;
             seat1.GetComponent<MeshRenderer>().material = Seats;
             var seat2 = GameObject.Find("seat rear(Clone)").transform.GetChild(2).gameObject;
             seat2.GetComponent<MeshRenderer>().material = Seats;
             //Driver seat
             GameObject.Find("seat driver(Clone)").GetComponent<MeshRenderer>().material = Seats;
             GameObject.Find("seat driver(Clone)/seattop").GetComponent<MeshRenderer>().material = Seats;
             //Passinger seat
             GameObject.Find("seat passenger(Clone)").GetComponent<MeshRenderer>().material = Seats;
             GameObject.Find("seat passenger(Clone)/seattop").GetComponent<MeshRenderer>().material = Seats;

            var ar = GameObject.Find("dashboard(Clone)");

            if (ar.CompareTag("PART"))
            {
                ar.GetComponent<MeshRenderer>().material = gtdash;
            }
            else
            {
                GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").GetComponent<MeshRenderer>().material = gtdash;
            }
            var mettt = GameObject.Find("dashboard meters(Clone)");
            mettt.GetComponent<MeshRenderer>().enabled = false;
            meters.transform.SetParent(mettt.transform, false);
            meters.transform.localPosition = new Vector3(0f, 0f, 0f);
            meters.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            GameObject trigg = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering").transform.Find("trigger_steering_column").gameObject;
            if (trigg.activeSelf)
            {
                var colll = GameObject.Find("CARPARTS/PartsCar").transform.Find("steering column(Clone)").gameObject;
                colll.GetComponent<MeshRenderer>().enabled = false;
                column.transform.SetParent(colll.transform, false);
                column.transform.localPosition = new Vector3(0f, 0f, 0f);
                column.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                var colll1 = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/Steering/steering_column2");
                colll1.GetComponent<MeshRenderer>().enabled = false;
                column.transform.SetParent(colll1.transform, false);
                column.transform.localPosition = new Vector3(0f, 0f, 0f);
                column.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            var lad = GameObject.Find("dashboard(Clone)/glovbox");
            lad.GetComponent<MeshRenderer>().enabled = false;
            ladica.transform.SetParent(lad.transform, false);
            ladica.transform.localPosition = new Vector3(0f, 0f, 0f);
            ladica.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            ModConsole.Print("<b><color=green>GT Interior Loaded</color></b>");

            //ModUI.ShowMessage(message, tittle);

        }
    }
}
