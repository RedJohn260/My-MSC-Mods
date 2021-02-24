using System.Collections;
using UnityEngine;
using MSCLoader;
using HutongGames.PlayMaker;
using System;
using System.Text.RegularExpressions;

namespace MSCSC
{
    public class Coroutines : MonoBehaviour
    {
        private GameObject ALARM;
        private int wait_seconds = 2;
        private FsmString player_current_vehicle;
        private float unflip_speed = 5000f;
        private float unflip_height = 3f;
        private float min_rot = 5;
        private float max_rot = 355;
        private GameObject SATSUMA;
        private GameObject HAYOSIKO;
        private GameObject RUSCKO;
        private GameObject GIFU;
        private GameObject FERNDALE;
        private GameObject KEKMET;
        private Clock24h clock;
        private GameObject DAY;
        private GameObject SPEAK_DB;
        private GameObject DRINK;
        public Clock24h Clock => clock;

        // Start function
        public void Start()
        {
            ALARM = GameObject.Find("YARD/Building/LIVINGROOM/LOD_livingroom/SmokeDetector/Sound");
            player_current_vehicle = PlayMakerGlobals.Instance.Variables.FindFsmString("PlayerCurrentVehicle");
            HAYOSIKO = GameObject.Find(VehicleNames.HAYOSIKO);
            SATSUMA = GameObject.Find(VehicleNames.SATSUMA);
            RUSCKO = GameObject.Find(VehicleNames.RUSCKO);
            GIFU = GameObject.Find(VehicleNames.GIFU);
            FERNDALE = GameObject.Find(VehicleNames.FERNDALE);
            KEKMET = GameObject.Find(VehicleNames.KEKMET);
            DAY = GameObject.Find("GUI/HUD/Day/HUDValue");
            SPEAK_DB = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/SpeakDatabase");
            DRINK = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/Drink");
            clock = new Clock24h();
        }

        public void Awake()
        {
            //clock = new Clock24h();
        }
        public void Update()
        {

        }

        //Command1 Execute
        private void RunCommand1()
        {
            try
            {
                ALARM.SetActive(true);
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Alarm Activated!</color>");
                SocketConnect.SendData("Alarm Activated!");
                //ModConsole.Print(SocketConnect.recievedMessage);
                SocketConnect.message_recieved = false;
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
        }

        //Command2 Execute
        private void RunCommand2()
        {
            try
            {
                //Satsuma
                if (player_current_vehicle.Value == VehiclesEnum.Satsuma.ToString())
                {
                    GetHorns.Horn(VehiclesEnum.Satsuma).SetActive(true);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Horn Activated!</color>");
                    SocketConnect.SendData("Horn Activated!");
                }
                //Hayosiko
                else if (player_current_vehicle.Value == VehiclesEnum.Hayosiko.ToString())
                {
                    GetHorns.Horn(VehiclesEnum.Hayosiko).SetActive(true);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Horn Activated!</color>");
                    SocketConnect.SendData("Horn Activated!");
                }
                //Ruscko
                else if (player_current_vehicle.Value == VehiclesEnum.Ruscko.ToString())
                {
                    GetHorns.Horn(VehiclesEnum.Ruscko).SetActive(true);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Horn Activated!</color>");
                    SocketConnect.SendData("Horn Activated!");
                }
                //Gifu
                else if (player_current_vehicle.Value == VehiclesEnum.Gifu.ToString())
                {
                    GetHorns.Horn(VehiclesEnum.Gifu).SetActive(true);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Horn Activated!</color>");
                    SocketConnect.SendData("Horn Activated!");
                }
                //Ferndale
                else if (player_current_vehicle.Value == VehiclesEnum.Ferndale.ToString())
                {
                    GetHorns.Horn(VehiclesEnum.Ferndale).SetActive(true);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Horn Activated!</color>");
                    SocketConnect.SendData("Horn Activated!");
                }
                // If Not In Vehicle
                else if (player_current_vehicle.Value == "" || player_current_vehicle.Value == null)
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Player not in vehicle!</color>");
                    SocketConnect.SendData("Player not in vehicle!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command3 Execute
        private void RunCommand3()
        {
            try
            {
                // If not in vehicle
                if (player_current_vehicle.Value == "" || player_current_vehicle.Value == null)
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Player not in vehicle!</color>");
                    SocketConnect.SendData("Player not in vehicle!");
                }
                // satsuma
                else if (player_current_vehicle.Value == VehiclesEnum.Satsuma.ToString())
                {
                    bool dash_trigger = GameObject.Find("SATSUMA(557kg, 248)/Dashboard").transform.FindChild("trigger_dashboard").gameObject.activeInHierarchy;
                    bool meters_trigger = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)").transform.FindChild("trigger_meters").gameObject.activeInHierarchy;
                    bool power = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON").gameObject.activeInHierarchy;

                    if (power == true)
                    {
                        if (dash_trigger == false)
                        {
                            if (meters_trigger == false)
                            {
                                GetLights.Lights(VehiclesEnum.Satsuma).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                                SocketConnect.SendData("Lights Off!");
                            }
                            else
                            {
                                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Car DashboardMeters not installed!</color>");
                                SocketConnect.SendData("Car Dashboard not installed!");
                            }
                        }
                        else
                        {
                            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Car DashboardMeters not installed!</color>");
                            SocketConnect.SendData("Car Dashboard not installed!");
                        }
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>No key in the ignition!</color>");
                        SocketConnect.SendData("No key in the ignition!");
                    }
                }

                //hayosiko
                else if (player_current_vehicle.Value == VehiclesEnum.Hayosiko.ToString())
                {
                    bool power = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject.activeInHierarchy;
                    if (power == true)
                    {
                        GetLights.Lights(VehiclesEnum.Hayosiko).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                        SocketConnect.SendData("Lights Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>No key in the ignition!</color>");
                        SocketConnect.SendData("No key in the ignition!");
                    }
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Ruscko.ToString())
                {
                    bool power = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject.activeInHierarchy;
                    if (power == true)
                    {
                        GetLights.Lights(VehiclesEnum.Ruscko).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                        SocketConnect.SendData("Lights Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>No key in the ignition!</color>");
                        SocketConnect.SendData("No key in the ignition!");
                    }
                }

                //gifu
                else if (player_current_vehicle.Value == VehiclesEnum.Gifu.ToString())
                {
                    bool power = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject.activeInHierarchy;
                    if (power == true)
                    {
                        GetLights.Lights(VehiclesEnum.Gifu).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                        SocketConnect.SendData("Lights Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>No key in the ignition!</color>");
                        SocketConnect.SendData("No key in the ignition!");
                    }
                }

                //ferndale
                else if (player_current_vehicle.Value == VehiclesEnum.Ferndale.ToString())
                {
                    bool power = GameObject.Find("FERNDALE(1630kg)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject.activeInHierarchy;
                    if (power == true)
                    {
                        GetLights.Lights(VehiclesEnum.Ferndale).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                        SocketConnect.SendData("Lights Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>No key in the ignition!</color>");
                        SocketConnect.SendData("No key in the ignition!");
                    }
                }

                //kekmet
                else if (player_current_vehicle.Value == VehiclesEnum.Kekmet.ToString())
                {
                    GetLights.Lights(VehiclesEnum.Kekmet).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                    SocketConnect.SendData("Lights Off!");
                }

                //jonnez
                else if (player_current_vehicle.Value == VehiclesEnum.Jonnez.ToString())
                {
                    GetLights.Lights(VehiclesEnum.Jonnez).GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("ON");
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Lights Off!</color>");
                    SocketConnect.SendData("Lights Off!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        private void RunCommand4()
        {
            try
            {
                Vector3 HAYOSIKO_POS = HAYOSIKO.transform.position;
                Vector3 HAYOSIKO_ROT = HAYOSIKO.transform.localEulerAngles;
                if (HAYOSIKO_ROT.z > min_rot && HAYOSIKO_ROT.z < max_rot)
                {
                    HAYOSIKO.transform.position = new Vector3(HAYOSIKO_POS.x, unflip_height, HAYOSIKO_POS.z);
                    HAYOSIKO.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hayosiko Flipped!</color>");
                    SocketConnect.SendData("Hayosiko Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hayosiko is not flipped!</color>");
                    SocketConnect.SendData("Hayosiko is not flipped!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        private void RunCommand5()
        {
            try
            {
                Vector3 SATSUMA_POS = SATSUMA.transform.position;
                Vector3 SATSUMA_ROT = SATSUMA.transform.localEulerAngles;
                if (SATSUMA_ROT.z > min_rot && SATSUMA_ROT.z < max_rot)
                {
                    SATSUMA.transform.position = new Vector3(SATSUMA_POS.x, unflip_height, SATSUMA_POS.z);
                    SATSUMA.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Satsuma Flipped!</color>");
                    SocketConnect.SendData("Satsuma Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Satsuma is not flipped!</color>");
                    SocketConnect.SendData("Satsuma is not flipped!");
                }
            }
            catch(Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command6 Execute
        private void RunCommand6()
        {
            try
            {
                //Ruscko
                Vector3 RUSCKO_POS = RUSCKO.transform.position;
                Vector3 RUSCKO_ROT = RUSCKO.transform.localEulerAngles;
                if (RUSCKO_ROT.z > min_rot && RUSCKO_ROT.z < max_rot)
                {
                    RUSCKO.transform.position = new Vector3(RUSCKO_POS.x, unflip_height, RUSCKO_POS.z);
                    RUSCKO.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ruscko Flipped!</color>");
                    SocketConnect.SendData("Ruscko Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ruscko is not flipped!</color>");
                    SocketConnect.SendData("Ruscko is not flipped!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command7 Execute
        private void RunCommand7()
        {

            try
            {
                //Gifu
                Vector3 GIFU_POS = GIFU.transform.position;
                Vector3 GIFU_ROT = GIFU.transform.localEulerAngles;
                if (GIFU_ROT.z > min_rot && GIFU_ROT.z < max_rot)
                {
                    GIFU.transform.position = new Vector3(GIFU_POS.x, unflip_height, GIFU_POS.z);
                    GIFU.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Gifu Flipped!</color>");
                    SocketConnect.SendData("Gifu Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Gifu is not flipped!</color>");
                    SocketConnect.SendData("Gifu is not flipped!");
                }
            }
            catch(Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command8 Execute
        private void RunCommand8()
        {

            try
            {
                //Ferndale
                Vector3 FERNDALE_POS = FERNDALE.transform.position;
                Vector3 FERNDALE_ROT = FERNDALE.transform.localEulerAngles;
                if (FERNDALE_ROT.z > min_rot && FERNDALE_ROT.z < max_rot)
                {
                    FERNDALE.transform.position = new Vector3(FERNDALE_POS.x, unflip_height, FERNDALE_POS.z);
                    FERNDALE.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ferndale Flipped!</color>");
                    SocketConnect.SendData("Ferndale Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ferndale is not flipped!</color>");
                    SocketConnect.SendData("Ferndale is not flipped!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command9 Execute
        private void RunCommand9()
        {
            try
            {
                //Kekmet
                Vector3 KEKMET_POS = KEKMET.transform.position;
                Vector3 KEKMET_ROT = KEKMET.transform.localEulerAngles;
                if (KEKMET_ROT.z > min_rot && KEKMET_ROT.z < max_rot)
                {
                    KEKMET.transform.position = new Vector3(KEKMET_POS.x, unflip_height, KEKMET_POS.z);
                    KEKMET.transform.Rotate(Vector3.forward * unflip_speed * Time.deltaTime);
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Kekmet Flipped!</color>");
                    SocketConnect.SendData("Kekmet Flipped!");
                }
                else
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Kekmet is not flipped!</color>");
                    SocketConnect.SendData("Kekmet is not flipped!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command10 Execute
        private void RunCommand10()
        {
            try
            {

                // If not in vehicle
                if (player_current_vehicle.Value == "" || player_current_vehicle.Value == null)
                {
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Player not in vehicle!</color>");
                    SocketConnect.SendData("Player not in vehicle!");
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Satsuma.ToString())
                {
                    if (GetEngines.Ignition(VehiclesEnum.Satsuma).activeSelf == true)
                    {
                        GetEngines.Engines(VehiclesEnum.Satsuma).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Satsuma Engine Off!</color>");
                        SocketConnect.SendData("Satsuma Engine Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Satsuma engine is not running currently!</color>");
                        SocketConnect.SendData("Satsuma engine is not running currently!");
                    }

                }
                else if (player_current_vehicle.Value == VehiclesEnum.Hayosiko.ToString())
                {
                    if (GetEngines.Ignition(VehiclesEnum.Hayosiko).activeSelf == true)
                    {
                        GetEngines.Engines(VehiclesEnum.Hayosiko).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hayosiko Engine Off!</color>");
                        SocketConnect.SendData("Hyosiko Engine Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hayosiko engine is not running currently!</color>");
                        SocketConnect.SendData("Hayosiko engine is not running currently!");
                    }
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Ruscko.ToString())
                {
                    if (GetEngines.Ignition(VehiclesEnum.Ruscko).activeSelf == true)
                    {
                        GetEngines.Engines(VehiclesEnum.Ruscko).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ruscko Engine Off!</color>");
                        SocketConnect.SendData("Ruscko Engine Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ruscko engine is not running currently!</color>");
                        SocketConnect.SendData("Ruscko engine is not running currently!");
                    }
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Gifu.ToString())
                {
                    if (GetEngines.Ignition(VehiclesEnum.Gifu).activeSelf == true)
                    {
                        GetEngines.Engines(VehiclesEnum.Gifu).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Gifu Engine Off!</color>");
                        SocketConnect.SendData("Gifu Engine Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Gifu engine is not running currently!</color>");
                        SocketConnect.SendData("Gifu engine is not running currently!");
                    }
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Ferndale.ToString())
                {
                    if (GetEngines.Ignition(VehiclesEnum.Ferndale).activeSelf == true)
                    {
                        GetEngines.Engines(VehiclesEnum.Ferndale).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ferndale Engine Off!</color>");
                        SocketConnect.SendData("Ferndale Engine Off!");
                    }
                    else
                    {
                        ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ferndale engine is not running currently!</color>");
                        SocketConnect.SendData("Ferndale engine is not running currently!");
                    }
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Kekmet.ToString())
                {
                    GetEngines.Engines(VehiclesEnum.Kekmet).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Kekmet Engine Off!</color>");
                    SocketConnect.SendData("Kekmet Engine Off!");
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Jonnez.ToString())
                {
                    GetEngines.Engines(VehiclesEnum.Jonnez).GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("OFF");
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Jonnez Engine Off!</color>");
                    SocketConnect.SendData("Jonnez Engine Off!");
                }

            }
            catch(Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with !enigne command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with !enigne command... Please contact the author!");
            }

            SocketConnect.message_recieved = false;
        }

        //Command11 Execute
        private void RunCommand11()
        {
            try
            {
                string current_day = DAY.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Text").Value;
                string day_low = current_day.ToLower();
                string day_cap = CapitalizeString.FirstCharToUpper(day_low);
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Today is " + day_cap + " and current msc time is:  </color>" + clock.ToString());
                SocketConnect.SendData("Today is " + day_cap  + " and current msc time is: " + clock.ToString());
                //ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Current msc time is:  </color>");
                //SocketConnect.SendData("Current msc time is: ");
            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command12 Execute
        private void RunCommand12()
        {
            try
            {
                SPEAK_DB.GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("SWEARING");
                string translation = SPEAK_DB.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Translation").Value;
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Swearing: </color>" + translation);
                SocketConnect.SendData(translation);

            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command13 Execute
        private void RunCommand13()
        {
            try
            {
                SPEAK_DB.GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("DRUNK");
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hey there!</color>");
                SocketConnect.SendData("Hey there!");

            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command14 Execute
        private void RunCommand14()
        {
            try
            {
                DRINK.GetComponent<PlayMakerFSM>().SendRemoteFsmEvent("DRINKBEER");
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Have some beer!</color>");
                SocketConnect.SendData("Have some beer!");

            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command15 Execute
        private void RunCommand15()
        {
            SocketConnect.message_recieved = false;
        }

        //Command1 Coroutine
        public void Command1()
        {
            StartCoroutine(CheckMessageCom1());
        }

        //Command2 Coroutine
        public void Command2()
        {
            StartCoroutine(CheckMessageCom2());
        }

        //Command3 Coroutine
        public void Command3()
        {
            StartCoroutine(CheckMessageCom3());
        }

        public void Command4()
        {
            StartCoroutine(CheckMessageCom4());
        }

        public void Command5()
        {
            StartCoroutine(CheckMessageCom5());
        }

        //Command6 Coroutine
        public void Command6()
        {
            StartCoroutine(CheckMessageCom6());
        }

        //Command7 Coroutine
        public void Command7()
        {
            StartCoroutine(CheckMessageCom7());
        }

        //Command8 Coroutine
        public void Command8()
        {
            StartCoroutine(CheckMessageCom8());
        }


        //Command9 Coroutine
        public void Command9()
        {
            StartCoroutine(CheckMessageCom9());
        }

        //Command10 Coroutine
        public void Command10()
        {
            StartCoroutine(CheckMessageCom10());
        }

        //Command11 Coroutine
        public void Command11()
        {
            StartCoroutine(CheckMessageCom11());
        }

        //Command12 Coroutine
        public void Command12()
        {
            StartCoroutine(CheckMessageCom12());
        }

        //Command13 Coroutine
        public void Command13()
        {
            StartCoroutine(CheckMessageCom13());
        }

        //Command14 Coroutine
        public void Command14()
        {
            StartCoroutine(CheckMessageCom14());
        }

        //Command15 Coroutine
        public void Command15()
        {
            StartCoroutine(CheckMessageCom15());
        }


        // Command1 Enumerator
        private IEnumerator CheckMessageCom1()
        {
            RunCommand1();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command2 Enumerator
        private IEnumerator CheckMessageCom2()
        {
            RunCommand2();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command3 Enumerator
        private IEnumerator CheckMessageCom3()
        {
            RunCommand3();
            yield return new WaitForSeconds(wait_seconds);
        }

        private IEnumerator CheckMessageCom4()
        {
            RunCommand4();
            yield return new WaitForSeconds(wait_seconds);
        }

        private IEnumerator CheckMessageCom5()
        {
            RunCommand5();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command6 Enumerator
        private IEnumerator CheckMessageCom6()
        {
            RunCommand6();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command7 Enumerator
        private IEnumerator CheckMessageCom7()
        {
            RunCommand7();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command8 Enumerator
        private IEnumerator CheckMessageCom8()
        {
            RunCommand8();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command9 Enumerator
        private IEnumerator CheckMessageCom9()
        {
            RunCommand9();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command10 Enumerator
        private IEnumerator CheckMessageCom10()
        {
            RunCommand10();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command11 Enumerator
        private IEnumerator CheckMessageCom11()
        {
            RunCommand11();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command12 Enumerator
        private IEnumerator CheckMessageCom12()
        {
            RunCommand12();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command13 Enumerator
        private IEnumerator CheckMessageCom13()
        {
            RunCommand13();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command14 Enumerator
        private IEnumerator CheckMessageCom14()
        {
            RunCommand14();
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command15 Enumerator
        private IEnumerator CheckMessageCom15()
        {
            RunCommand15();
            yield return new WaitForSeconds(wait_seconds);
        }
    }
}