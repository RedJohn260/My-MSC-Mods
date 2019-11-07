using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;

namespace ThirdPersonCam
{
    public class ThirdPersonCam : Mod
    {
        public override string ID => "ThirdPersonCam(Reloaded)"; //Your mod ID (unique)
        public override string Name => "ThirdPersonCam(Reloaded)"; //You mod name
        public override string Author => "RedJohn260, Roman266"; //Your Username
        public override string Version => "1.0.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;
        private GameObject CAMERA;
        private Keybind camera1Key = new Keybind("CameraKey1", "Reset camera", KeyCode.Alpha1, KeyCode.LeftAlt);
        private Keybind camera2Key = new Keybind("CameraKey2", "Reset camera for Satsuma", KeyCode.Alpha2, KeyCode.LeftAlt);
        private Keybind camera3Key = new Keybind("CameraKey3", "Camera for Satsuma", KeyCode.Alpha3, KeyCode.LeftAlt);
        private Keybind camera4Key = new Keybind("CameraKey4", "Camera for others vehicles", KeyCode.Alpha4, KeyCode.LeftAlt);
        private Transform camera4;
        private float ZposO = -4.5f;
        private float XposO = 0.4f;
        private float YposO = 2f;
        private float ZposS = -3.5f;
        private float XposS = 0.282f;
        private float YposS = 0.5f;
        private Vector3 OlocalPosition;
        private Rect guiBox1 = new Rect((float)(Screen.width - 2100 / 2), 70f, 260f, 360f);
        private readonly Keybind openGUI = new Keybind("ShowGUI", "ShowGUI", KeyCode.Keypad9);
        private bool GUI1;
        private bool GUI2;
        private bool GUI3;
        private Rect guiBox2 = new Rect((float)(Screen.width - 2100 / 2), 70f, 260f, 210f);
        private Rect guiBox3 = new Rect((float)(Screen.width - 2100 / 2), 70f, 260f, 210f);
        private string path;
        private FsmString playerInVechicle;
        private bool inOvehicle;
        private bool camResetO;
        private bool camResetS;
        private bool inSvehicle;
        private bool defaultCam;
        private bool defaultCamS;
        public override void OnLoad()
        {
            Keybind.Add(this, camera1Key);
            Keybind.Add(this, camera2Key);
            Keybind.Add(this, camera3Key);
            Keybind.Add(this, camera4Key);
            CAMERA = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera");
            camera4 = CAMERA.transform;
            playerInVechicle = FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle");
            inOvehicle = false;
            camResetO = false;
            camResetS = false;
            inSvehicle = false;
            defaultCam = false;
            defaultCamS = false;
            this.path = ModLoader.GetModAssetsFolder(this);
            Loadsettings();
            ModConsole.Print(ID + " Loaded");
        }
                
        public override void Update()
        {
            if (this.openGUI.IsDown())
            {
                this.GuiShow();
            }

            if (inOvehicle || camera4Key.IsDown())
            {
                Ocam();
            }
            if (inSvehicle || camera3Key.IsDown())
            {
                Scam();
            }

            if (camResetO)
            {
                CamReset();
                
            }
            if (camResetS)
            {
                CamResetS();

            }
            if (defaultCam || camera1Key.IsDown())
            {
                DefaultCam();
            }
            if (defaultCamS || camera2Key.IsDown())
            {
                DefaultCamS();
            }


        }
        private void GuiShow()
        {
            this.GUI1 = !this.GUI1;
            if (this.GUI1)
            {
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = true;
                return;
            }
            FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
        }
        public override void OnGUI()
        {
            if (this.GUI1)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, this.guiBox1, new GUI.WindowFunction(this.Window1), "ThirdPersonCam(Reloaded)");
            }
            if (this.GUI2)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, this.guiBox2, new GUI.WindowFunction(this.Window2), "Other Vehicles Cam Position");
            }
            if (this.GUI3)
            {
                GUI.backgroundColor = new Color(0, 00f, 0.00f, 0.55f);
                GUI.ModalWindow(1, this.guiBox3, new GUI.WindowFunction(this.Window3), "Satsuma Cam Position");
            }
        }
        private void Window1(int windowid)
        {
            if (GUI.Button(new Rect(55f, 50f, 150f, 40f), "Oher Vehs Cam"))
            {
                if (playerInVechicle.Value == "Kekmet" || playerInVechicle.Value == "Gifu" || playerInVechicle.Value == "Hayosiko" || playerInVechicle.Value == "Ferndale" || playerInVechicle.Value == "Ruscko")
                {
                    this.GUI1 = false;
                    this.GUI2 = true;
                    inSvehicle = false;
                    inOvehicle = !inOvehicle;
                }
                else
                {
                    ModConsole.Warning("<b><color=yellow>You need to be in a Vehicle to activate this button.</color></b>");
                }
            }
            if (GUI.Button(new Rect(55f, 90f, 150f, 40f), "Satsuma Cam"))
            {
                if (playerInVechicle.Value == "Satsuma")
                {
                    this.GUI1 = false;
                    this.GUI3 = true;
                    inOvehicle = false;
                    inSvehicle = !inSvehicle;
                }
                else
                {
                    ModConsole.Warning("<b><color=yellow>You need to be in a Satsuma to activate this button.</color></b>");
                }
            }
            if (GUI.Button(new Rect(55f, 130f, 150f, 40f), "Default Cam"))
            {
                if (playerInVechicle.Value == "Kekmet" || playerInVechicle.Value == "Gifu" || playerInVechicle.Value == "Hayosiko" || playerInVechicle.Value == "Ferndale" || playerInVechicle.Value == "Ruscko")
                {
                    defaultCam = !defaultCam;
                }
                else
                {
                    ModConsole.Warning("<b><color=yellow>You need to be in a Vehicle to Reset the camera.</color></b>");
                }
            }
            if (GUI.Button(new Rect(55f, 170f, 150f, 40f), "DefaultCamSatsuma"))
            {
                if (playerInVechicle.Value == "Satsuma")
                {
                     defaultCamS = !defaultCamS;
                }
                else
                {
                    ModConsole.Warning("<b><color=yellow>You need to be in a Satsuma to reset the camera.</color></b>");
                }

            }
            if (GUI.Button(new Rect(55f, 210f, 150f, 40f), "Save Settings"))
            {
                Savesettings();
                ModConsole.Warning("<b><color=Green>Third Person Cam Settings Saved.</color></b>");
            }
            if (GUI.Button(new Rect(55f, 250f, 150f, 40f), "Load Settings"))
            {
                Loadsettings();
                ModConsole.Warning("<b><color=Green>Third Person Cam Settings Loaded.</color></b>");
            }
            if (GUI.Button(new Rect(55f, 290f, 150f, 40f), "Close Window"))
            {
                this.GUI1 = false;
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }
            GUI.Label(new Rect(85f, 335f, 150f, 20f), "by RedJohn260");
            GUI.DragWindow();

        }
        private void Window2(int windowid)
        {
            GUI.Label(new Rect(10f, 15f, 10f, 40f), "X");
            XposO = GUI.HorizontalSlider(new Rect(30f, 30f, 220f, 40f), XposO, -5f, 5f);

            GUI.Label(new Rect(10f, 55f, 10f, 40f), "Y");
            YposO = GUI.HorizontalSlider(new Rect(30f, 70f, 220f, 40f), YposO, -5f, 5f);

            GUI.Label(new Rect(10f, 95f, 10f, 40f), "Z");
            ZposO = GUI.HorizontalSlider(new Rect(30f, 110f, 220f, 40f), ZposO, -10f, 2f);

            if (GUI.Button(new Rect(10f, 160f, 100f, 40f), "<< BACK"))
            {
                this.GUI1 = true;
                this.GUI2 = false;
                inOvehicle = !inOvehicle;
                
                
            }
            if (GUI.Button(new Rect(130f, 160f, 120f, 40f), "RESET"))
            {
                
                camResetO = !camResetO;
                
            }
            GUI.DragWindow();
        }
        private void Window3(int windowid)
        {
            GUI.Label(new Rect(10f, 15f, 10f, 40f), "X");
            XposS = GUI.HorizontalSlider(new Rect(30f, 30f, 220f, 40f), XposS, -5f, 5f);

            GUI.Label(new Rect(10f, 55f, 10f, 40f), "Y");
            YposS = GUI.HorizontalSlider(new Rect(30f, 70f, 220f, 40f), YposS, -5f, 5f);

            GUI.Label(new Rect(10f, 95f, 10f, 40f), "Z");
            ZposS = GUI.HorizontalSlider(new Rect(30f, 110f, 220f, 40f), ZposS, -10f, 2f);

            if (GUI.Button(new Rect(10f, 160f, 100f, 40f), "<< BACK"))
            {
                this.GUI1 = true;
                this.GUI3 = false;
                inSvehicle = !inSvehicle;


            }
            if (GUI.Button(new Rect(130f, 160f, 120f, 40f), "RESET"))
            {

                camResetS = !camResetS;

            }
            GUI.DragWindow();
        }
        private void Ocam()
        {
            OlocalPosition = camera4.localPosition;
            OlocalPosition.x = XposO;
            OlocalPosition.y = YposO;
            OlocalPosition.z = ZposO;
            camera4.localPosition = OlocalPosition;
        }
        private void Scam()
        {
            OlocalPosition = camera4.localPosition;
            OlocalPosition.x = XposS;
            OlocalPosition.y = YposS;
            OlocalPosition.z = ZposS;
            camera4.localPosition = OlocalPosition;
        }
        private void CamReset()
        {
            XposO = 0.4f;
            YposO = 2f;
            ZposO = -4.5f;
            camResetO = !camResetO;
        }
        private void CamResetS()
        {
            XposS = 0.282f;
            YposS = 0.5f;
            ZposS = -3.5f;
            camResetS = !camResetS;
        }
        private void DefaultCam()
        {
            OlocalPosition = camera4.localPosition;
            OlocalPosition.x = 0f;
            OlocalPosition.y = 0.3f;
            OlocalPosition.z = 0f;
            camera4.localPosition = OlocalPosition;
            defaultCam = !defaultCam;
        }
        private void DefaultCamS()
        {
            if (playerInVechicle.Value == "Satsuma")
            {
                var FPScamera = GameObject.Find("SATSUMA(557kg, 248)/DriverHeadPivot/CameraPivot/PivotSeatR/PLAYER/Pivot/AnimPivot/Camera/FPSCamera").transform;
                Vector3 OlocalPosition1 = FPScamera.localPosition;
                OlocalPosition1.x = 0f;
                OlocalPosition1.y = 0.6f;
                OlocalPosition1.z = 0f;
                FPScamera.localPosition = OlocalPosition1;

                OlocalPosition = camera4.localPosition;
                OlocalPosition.x = 0f;
                OlocalPosition.y = 0.3f;
                OlocalPosition.z = 0f;
                camera4.localPosition = OlocalPosition;

                defaultCamS = !defaultCamS;
            }
            else
            {
                ModConsole.Warning("<b><color=yellow>Player not in Satsuma.</color></b>");
            }
        }
        private void Savesettings()
        {
            string[] str = new string[6];
            int num = 0;
            float XO = this.XposO;
            str[num] = XO.ToString();
            int num1 = 1;
            float YO = this.YposO;
            str[num1] = YO.ToString();
            int num2 = 2;
            float ZO = this.ZposO;
            str[num2] = ZO.ToString();
            int num3 = 3;
            float XS = this.XposS;
            str[num3] = XS.ToString();
            int num4 = 4;
            float YS = this.YposS;
            str[num4] = YS.ToString();
            int num5 = 5;
            float ZS = this.ZposS;
            str[num5] = ZS.ToString();
            File.WriteAllLines(string.Concat(this.path, "/SaveSettings.txt"), str);
        }
        private void Loadsettings()
        {
            string[] strArrays = new string[6];

            strArrays = File.ReadAllLines(string.Concat(this.path, "/SaveSettings.txt"));
            this.XposO = float.Parse(strArrays[0]);
            this.YposO = float.Parse(strArrays[1]);
            this.ZposO = float.Parse(strArrays[2]);
            this.XposS = float.Parse(strArrays[3]);
            this.YposS = float.Parse(strArrays[4]);
            this.ZposS = float.Parse(strArrays[5]);
        }
        public override void OnSave()
        {
            Savesettings();
        }
    }
}
