using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace GifuAddons
{
    public class GifuPump : Mod
    {
        public override string ID => "GifuPump"; //Your mod ID (unique)
        public override string Name => "GifuPump"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.5"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;


        private GameObject kutija;
        private GameObject vrataOFF;
        private GameObject vrataON;
        private GameObject prekidacOFF2;
        private GameObject prekidacON2;
        private GameObject prekidacOFF3;
        private GameObject prekidacON3;
        private GameObject worklight;
        private GameObject workilightON;
        private GameObject worklightReflektor;
        private GameObject gauge;
        private GameObject intLightOFF;
        private GameObject intLightON;
        private AssetBundle ab;
        private GameObject GIFU;
        private bool DoorOpened;
        private GameObject switchTrigger1;
        private GameObject switchTrigger2;
        private GameObject switchTrigger3;
        private bool BackWorklight;
        private bool BoxLight;
        private GameObject worklightLight;
        private GameObject intLightLight;
        private GameObject Pump;
        private GameObject switchH;
        private GameObject switchSwitch;
        private GameObject PumpSwitch;
        private Vector3 switchOFFp = new Vector3(310.0001f, 0f, 0f);
        private Vector3 switchONp = new Vector3(294f, 180.0012f, 180.0002f);
        private GameObject panel;
        private GameObject leaver;
        private GameObject drzac;
        private Material handleMat;
        private GameObject gameObject;
        




        private void AssetBundleLoad()
        {
            ab = LoadAssets.LoadBundle(this, "gifuaddons.unity3d");
            kutija = GameObject.Instantiate(ab.LoadAsset("kutija.prefab")) as GameObject;
            vrataOFF = GameObject.Instantiate(ab.LoadAsset("vrata.prefab")) as GameObject;
            vrataON = GameObject.Instantiate(ab.LoadAsset("vrataOpen.prefab")) as GameObject;
            prekidacOFF2 = GameObject.Instantiate(ab.LoadAsset("switchH.prefab")) as GameObject;
            prekidacON2 = GameObject.Instantiate(ab.LoadAsset("switchSwitch.prefab")) as GameObject;
            prekidacOFF3 = GameObject.Instantiate(ab.LoadAsset("switchH.prefab")) as GameObject;
            prekidacON3 = GameObject.Instantiate(ab.LoadAsset("switchSwitch.prefab")) as GameObject;
            worklight = GameObject.Instantiate(ab.LoadAsset("workLight.prefab")) as GameObject;
            workilightON = GameObject.Instantiate(ab.LoadAsset("workLightEmmit.prefab")) as GameObject;
            worklightReflektor = GameObject.Instantiate(ab.LoadAsset("workLightReflektor.prefab")) as GameObject;
            gauge = GameObject.Instantiate(ab.LoadAsset("gauge.prefab")) as GameObject;
            intLightON = GameObject.Instantiate(ab.LoadAsset("intLightEmmit.prefab")) as GameObject;
            intLightOFF = GameObject.Instantiate(ab.LoadAsset("intLight.prefab")) as GameObject;
            switchH = GameObject.Instantiate(ab.LoadAsset("switchH.prefab")) as GameObject;
            switchSwitch = GameObject.Instantiate(ab.LoadAsset("switchSwitch.prefab")) as GameObject;
            panel = GameObject.Instantiate(ab.LoadAsset("panel.prefab")) as GameObject;
            leaver = ab.LoadAsset("handle.prefab") as GameObject;
            drzac = GameObject.Instantiate(ab.LoadAsset("drzac.prefab")) as GameObject;
            handleMat = ab.LoadAsset("handle.mat") as Material;
            ab.Unload(false);

        }
        private void NewGameObjects()
        {
            GIFU = GameObject.Find("GIFU(750/450psi)/MESH");

            gameObject = Object.Instantiate(leaver);
            Object.Destroy(gameObject);

            kutija.transform.SetParent(GIFU.transform, false);
           
            prekidacOFF2.transform.SetParent(GIFU.transform, false);
            prekidacOFF2.name = "prekidacOff2";
            prekidacOFF2.transform.localPosition = new Vector3(-0.6980001f, -1.48f, 1.61f);
            prekidacOFF2.transform.localEulerAngles = new Vector3(6.830189E-06f, 90.00023f, 90.00005f);
            prekidacOFF2.transform.localScale = new Vector3(2f, 2f, 1.5f);

            prekidacOFF3.transform.SetParent(GIFU.transform, false);
            prekidacOFF3.name = "prekidacOff3";
            prekidacOFF3.transform.localPosition = new Vector3(-0.6980001f, -1.33f, 1.61f);
            prekidacOFF3.transform.localEulerAngles = new Vector3(6.830189E-06f, 90.00023f, 90.00005f);
            prekidacOFF3.transform.localScale = new Vector3(2f, 2f, 1.5f);

            
            prekidacON2.transform.SetParent(prekidacOFF2.transform, false);
            prekidacON2.name = "prekidacOn2";
            prekidacON2.transform.localPosition = new Vector3(0f, 0f, 0f);
            prekidacON2.transform.localEulerAngles = switchOFFp;
            prekidacON2.transform.localScale = new Vector3(0.55f, 0.6049999f, 0.5500003f);

            prekidacON3.transform.SetParent(prekidacOFF3.transform, false);
            prekidacON3.name = "prekidacOn3";
            prekidacON3.transform.localPosition = new Vector3(0f, 0f, 0f);
            prekidacON3.transform.localEulerAngles = switchOFFp;
            prekidacON3.transform.localScale = new Vector3(0.55f, 0.6049999f, 0.5500003f);

            worklight.transform.SetParent(GIFU.transform, false);
            workilightON.transform.SetParent(GIFU.transform, false);
            worklightReflektor.transform.SetParent(GIFU.transform, false);
            gauge.transform.SetParent(GIFU.transform, false);
            intLightOFF.transform.SetParent(GIFU.transform, false);
            intLightON.transform.SetParent(GIFU.transform, false);
            vrataOFF.transform.SetParent(GIFU.transform, false);
            vrataON.transform.SetParent(GIFU.transform, false);
            vrataOFF.name = "vrataClosed";
            vrataON.name = "vrataOpened";

            drzac.transform.SetParent(GIFU.transform, false);
            drzac.name = "Drzac";
            drzac.transform.localPosition = new Vector3(-0.6889997f, -1.479001f, 1.180002f);
            drzac.transform.localEulerAngles = new Vector3(2.504729E-06f, -0.002962359f, 90.00002f);
            drzac.transform.localScale = new Vector3(100f, 100f, 100f);

            panel.transform.SetParent(GIFU.transform, false);
            panel.name = "Panel";
            panel.transform.localPosition = new Vector3(-0.687f, -1.478999f, 1.343f);
            panel.transform.localEulerAngles = new Vector3(359.0001f, 180.0015f, 270.0003f);
            panel.transform.localScale = new Vector3(100f, 100f, 100f);

            var gaa = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/KnobHandThrottle").gameObject;
            gaa.transform.localPosition = new Vector3(0.2485996f, -2.256499f, 1.3074f);
            gaa.transform.localEulerAngles = new Vector3(80.00042f, 179.9993f, 90.00027f);
            //gaa.transform.localScale = new Vector3(100f, 100f, 100f);

            handleMat = leaver.GetComponent<MeshRenderer>().material;
            var thh = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/KnobHandThrottle/Knob");
            thh.GetComponent<MeshFilter>().sharedMesh = leaver.GetComponent<MeshFilter>().sharedMesh;
            thh.GetComponent<MeshRenderer>().sharedMaterial = handleMat;
            thh.transform.localScale = new Vector3(100f, 100f, 100f);
            thh.transform.localEulerAngles = new Vector3(-1.272222E-13f, 140f, 9.536747E-07f);

            var pmm = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/ButtonHandThrottle");
            pmm.transform.localPosition = new Vector3(0.2485996f, -2.256499f, 1.3074f);
            pmm.transform.localEulerAngles = new Vector3(80.00042f, 179.9993f, 90.00027f);
            pmm.transform.localScale = new Vector3(2f, 2f, 2f);



            var gg = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/ShitTank/shit_tank_gauge").gameObject;
            gg.transform.localPosition = new Vector3(0.3640001f, -0.711f, 0.206f);
            var gn = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/ShitTank/LitreGauge").gameObject;
            gn.transform.localPosition = new Vector3(-0.7509993f, -1.4911f, 1.38479f);
            gn.transform.localEulerAngles = new Vector3(54.82631f, 0.2540297f, 270.4298f);



            //switch trigger 1
            switchTrigger1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            switchTrigger1.transform.SetParent(GIFU.transform, false);
            switchTrigger1.transform.localScale = new Vector3(0.050f, 0.050f, 0.050f);
            switchTrigger1.transform.localPosition = new Vector3(-0.723f, -1.608001f, 1.621f);
            switchTrigger1.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            switchTrigger1.name = "SwitchTrigger1";
            var collision2 = switchTrigger1.GetComponent<Collider>();
            collision2.isTrigger = true;
            switchTrigger1.GetComponent<MeshRenderer>().enabled = false;
            //switch trigger 2
            switchTrigger2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            switchTrigger2.transform.SetParent(GIFU.transform, false);
            switchTrigger2.transform.localScale = new Vector3(0.050f, 0.050f, 0.050f);
            switchTrigger2.transform.localPosition = new Vector3(-0.716f, -1.48f, 1.621f);
            switchTrigger2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            switchTrigger2.name = "SwitchTrigger2";
            var collision3 = switchTrigger2.GetComponent<Collider>();
            collision3.isTrigger = true;
            switchTrigger2.GetComponent<MeshRenderer>().enabled = false;
            //switch trigger 3
            switchTrigger3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            switchTrigger3.transform.SetParent(GIFU.transform, false);
            switchTrigger3.transform.localScale = new Vector3(0.050f, 0.050f, 0.050f);
            switchTrigger3.transform.localPosition = new Vector3(-0.721f, -1.350001f, 1.621f);
            switchTrigger3.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            switchTrigger3.name = "SwitchTrigger3";
            var collision4 = switchTrigger1.GetComponent<Collider>();
            collision4.isTrigger = true;
            switchTrigger3.GetComponent<MeshRenderer>().enabled = false;

            
            //worklight Light
            worklightLight = new GameObject("WorklightLight");
            Light lightComp11 = worklightLight.AddComponent<Light>();
            lightComp11.color = Color.white;
            lightComp11.type = LightType.Spot;
            lightComp11.spotAngle = 80;
            lightComp11.intensity = 25;
            lightComp11.cullingMask = 3;
            lightComp11.range = 15;
            worklightLight.transform.SetParent(worklight.transform, false);
            worklightLight.transform.localPosition = new Vector3(-0.3f, 4.599998f, 4.899998f);
            worklightLight.transform.localEulerAngles = new Vector3(325.0011f, 170.0018f, 0.0005378015f);
            //int point light
            intLightLight = new GameObject("BoxLight");
            Light lightComp12 = intLightLight.AddComponent<Light>();
            lightComp12.color = Color.white;
            lightComp12.type = LightType.Point;
            lightComp12.intensity = 2.2f;
            lightComp12.range = 1.3f;
            intLightLight.transform.SetParent(intLightOFF.transform, false);
            intLightLight.transform.localPosition = new Vector3(-0.9299997f, -1.46f, 1.61f);
            DoorOpened = false;
            BackWorklight = false;
            BoxLight = false;

            Pump = GameObject.Find("GIFU(750/450psi)/Dashboard/Knobs/Hydraulics");
            Pump.transform.SetParent(GIFU.transform, false);
            Pump.transform.localPosition = new Vector3(-0.7209999f, -1.633f, 1.613f);
            Pump.transform.localEulerAngles = new Vector3(90f, 279.0569f, 0f);

            switchH.transform.SetParent(GIFU.transform, false);
            switchH.transform.localPosition = new Vector3(-0.6980001f, -1.63f, 1.61f);
            switchH.transform.localEulerAngles = new Vector3(6.830189E-06f, 90.00023f, 90.00005f);
            switchH.transform.localScale = new Vector3(2f, 2f, 1.5f);

            PumpSwitch = GameObject.Find("GIFU(750/450psi)/MESH/Hydraulics/mesh");
            PumpSwitch.GetComponent<MeshFilter>().sharedMesh = switchSwitch.GetComponent<MeshFilter>().sharedMesh;





        }
        private void RAY()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.name == "vrataClosed" || hit.collider.name == "vrataOpened")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            DoorOpened = !DoorOpened;
                            

                        }
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Open/Close";
                        break;
                    }
                }
            }
        }

        private void Doorperation()
        {
            if (DoorOpened)
            {
                vrataOFF.SetActive(false);
                vrataON.SetActive(true);
                
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                    foreach (RaycastHit hit in hits)
                    {
                       
                        if (hit.collider.name == "SwitchTrigger2")
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                BackWorklight = !BackWorklight;

                            }
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Back Work Light";
                            break;
                        }
                        if (hit.collider.name == "SwitchTrigger3")
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                BoxLight = !BoxLight;

                            }
                            PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                            PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Light";
                            break;
                        }
                    }
                }
            }
            else
            {
                vrataOFF.SetActive(true);
                vrataON.SetActive(false);
                
            }
        }
        private void SwitchOperation()
        {
         
            
            if (BackWorklight)
            {
                prekidacON2.transform.localEulerAngles = switchONp;
                workilightON.SetActive(true);
                worklightLight.SetActive(true);

            }
            else
            {
                prekidacON2.transform.localEulerAngles = switchOFFp;
                workilightON.SetActive(false);
                worklightLight.SetActive(false);
            }
            if (BoxLight)
            {
                prekidacON3.transform.localEulerAngles = switchONp;
                intLightON.SetActive(true);
                intLightLight.SetActive(true);
            }
            else
            {
                prekidacON3.transform.localEulerAngles = switchOFFp;
                intLightON.SetActive(false);
                intLightLight.SetActive(false);
            }
        }
        public override void OnLoad()
        {
            AssetBundleLoad();
            NewGameObjects();
            ModConsole.Print("<b><color=green>Gifu Pump Mod Loaded</color></b>");
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
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            RAY();
            Doorperation();
            SwitchOperation();
        }
    }
}
