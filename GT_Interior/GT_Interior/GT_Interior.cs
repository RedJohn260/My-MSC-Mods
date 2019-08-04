using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace GT_Interior
{
    public class GT_Interior : Mod
    {
        public override string ID => "GT_Interior"; //Your mod ID (unique)
        public override string Name => "GT_Interior"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;
        private AssetBundle ab;
        private Material BodyM;
        private Material Panels;
        private Material Seats;
        private GameObject meters;
        private GameObject console;
        private GameObject column;
        private GameObject gwheel;
        private GameObject gwheel1;
        private GameObject stick;
        private GameObject ladica;
        private Material gtdash;
        public string message = "<b><color=green>Original Mod Link:</color></b> https://www.nexusmods.com/mysummercar/mods/91 <color=orange>Any re-upload of this mod is strictly prohibited. I do not support any of my mods uploaded to different sites without my permission.</color> <b><color=green>Sincerely RedJohn260.</color></b>";
        public string tittle = "Satsuma GT Interior";
        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gtinterior.unity3d");
            BodyM = ab.LoadAsset("BodyM.mat") as Material;
            Panels = ab.LoadAsset("Panels.mat") as Material;
            Seats = ab.LoadAsset("Seats.mat") as Material;
            gtdash = ab.LoadAsset("gtdash.mat") as Material;

            column = Object.Instantiate(ab.LoadAsset<GameObject>("gtcolumn.prefab"));
            console = Object.Instantiate(ab.LoadAsset<GameObject>("gtconsole.prefab"));
            meters = Object.Instantiate(ab.LoadAsset<GameObject>("gtmeters.prefab"));
            gwheel = Object.Instantiate(ab.LoadAsset<GameObject>("gtwheel.prefab"));
            gwheel1 = Object.Instantiate(ab.LoadAsset<GameObject>("gtwheel1.prefab"));
            stick = Object.Instantiate(ab.LoadAsset<GameObject>("gtstick.prefab"));
            ladica = Object.Instantiate(ab.LoadAsset<GameObject>("ladica.prefab"));
            ab.Unload(false);

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
            var whh = GameObject.Find("gt steering wheel(Clone)");
            whh.GetComponent<MeshRenderer>().enabled = false;
            gwheel.transform.SetParent(whh.transform, false);
            gwheel.transform.localPosition = new Vector3(0f, 0f, 0f);
            gwheel.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            gwheel1.transform.SetParent(whh.transform, false);
            gwheel1.transform.localPosition = new Vector3(0f, 0f, 0f);
            gwheel1.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            var con = GameObject.Find("SATSUMA(557kg, 248)/Dashboard").transform.FindChild("center console gt(xxxxx)");
            con.GetComponent<MeshRenderer>().enabled = false;
            console.transform.SetParent(con.transform, false);
            console.transform.localPosition = new Vector3(0f, 0f, 0f);
            console.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            var ss = GameObject.Find("SATSUMA(557kg, 248)/Dashboard").transform.FindChild("center console gt(xxxxx)/GearLever/Pivot/Lever/gear_stick");
            ss.GetComponent<MeshRenderer>().enabled = false;
            stick.transform.SetParent(ss.transform, false);
            stick.transform.localPosition = new Vector3(0f, 0f, 0f);
            stick.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            var lad = GameObject.Find("dashboard(Clone)/glovbox");
            lad.GetComponent<MeshRenderer>().enabled = false;
            ladica.transform.SetParent(lad.transform, false);
            ladica.transform.localPosition = new Vector3(0f, 0f, 0f);
            ladica.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            ModConsole.Print("<b><color=green>GT Interior Loaded</color></b>");

            ModUI.ShowMessage(message, tittle);

        }
    }
}
