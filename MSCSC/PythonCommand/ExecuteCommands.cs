using System.Collections;
using UnityEngine;
using MSCLoader;
using HutongGames.PlayMaker;
using System;

namespace MSCSC
{
    public static class ExecuteCommands
    {
        //com1
        public static void ExCom1(GameObject alarm)
        {
            try
            {
                alarm.SetActive(true);
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

        //com2
        public static void ExCom2(FsmString player_current_vehicle)
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

        //com3
        public static void ExCom3(FsmString player_current_vehicle)
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

        //com4
        public static void ExCom4( GameObject HAYOSIKO, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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

        //com 5
        public static void ExCom5(GameObject SATSUMA, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command6 Execute
        public static void ExCom6(GameObject RUSCKO, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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
        public static void ExCom7(GameObject GIFU, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }

        //Command8 Execute
        public static void ExCom8(GameObject FERNDALE, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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
        public static void ExCom9(GameObject KEKMET, float min_rot, float max_rot, float unflip_height, float unflip_speed)
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
        public static void ExCom10(FsmString player_current_vehicle)
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
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with !enigne command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with !enigne command... Please contact the author!");
            }

            SocketConnect.message_recieved = false;
        }

        //Command11 Execute
        public static void ExCom11(GameObject DAY, Clock24h clock)
        {
            try
            {
                string current_day = DAY.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Text").Value;
                string day_low = current_day.ToLower();
                string day_cap = CapitalizeString.FirstCharToUpper(day_low);
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Today is " + day_cap + " and current msc time is:  </color>" + clock.ToString());
                SocketConnect.SendData("Today is " + day_cap + " and current msc time is: " + clock.ToString());
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
        public static void ExCom12(GameObject SPEAK_DB)
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
        public static void ExCom13(GameObject SPEAK_DB)
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
        public static void ExCom14(GameObject DRINK)
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
        public static void ExCom15(GameObject ROCKET, GameObject PLAYER)
        {
            try
            {
                GetFireworks.SpawnRocket(ROCKET, PLAYER);
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Rocket Spawned!</color>");
                SocketConnect.SendData("Rocket Spawned!");
            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command16 Execute
        public static void ExCom16(AudioSource train_horn)
        {
            try
            {
                train_horn.volume = 1f;
                train_horn.Play();
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Train Horn Activated!</color>");
                SocketConnect.SendData("Train Horn Activated!");
            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command17 Execute
        public static void ExCom17(AudioSource phone_ring)
        {
            try
            {
                phone_ring.Play();
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Phone Ringed!</color>");
                SocketConnect.SendData("Phone Ringed!");
            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //Command18 Execute
        public static void ExCom18(GameObject UFO, GameObject PLAYER)
        {
            try
            {
                UFO.transform.position = PLAYER.transform.position + PLAYER.transform.forward * 1f + PLAYER.transform.up * 3f;
                UFO.SetActive(true);
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>UFO Spawned!</color>");
                SocketConnect.SendData("UFO? Where?");
            }
            catch (Exception e)
            {
                ModConsole.Error("<color=yellow>[MSCSC]: </color><color=white>Something went wrong with command... Please contact the author! </color>" + e.ToString());
                SocketConnect.SendData("Something went wrong with command... Please contact the author!");
            }
            SocketConnect.message_recieved = false;
        }

        //com19
        public static void ExCom19(FsmString player_current_vehicle)
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
                                GetWipers.Wipers(VehiclesEnum.Satsuma).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Satsuma Wipers Activated!</color>");
                                SocketConnect.SendData("Satsuma Wipers Activated!");
                            }
                            else
                            {
                                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Car DashboardMeters not installed!</color>");
                                SocketConnect.SendData("Car Dashboard not installed!");
                            }
                        }
                        else
                        {
                            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Car Dashboard not installed!</color>");
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
                    GetWipers.Wipers(VehiclesEnum.Hayosiko).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Hayosiko Wipers Activated!</color>");
                    SocketConnect.SendData("Hayosiko Wipers Activated!");
                }
                else if (player_current_vehicle.Value == VehiclesEnum.Ruscko.ToString())
                {
                    GetWipers.Wipers(VehiclesEnum.Ruscko).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ruscko Wipers Activated!</color>");
                    SocketConnect.SendData("Ruscko Wipers Activated!");
                }

                //gifu
                else if (player_current_vehicle.Value == VehiclesEnum.Gifu.ToString())
                {
                    GetWipers.Wipers(VehiclesEnum.Gifu).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Gifu Wipers Activated!</color>");
                    SocketConnect.SendData("Gifu Wipers Activated!");
                }

                //ferndale
                else if (player_current_vehicle.Value == VehiclesEnum.Ferndale.ToString())
                {
                    GetWipers.Wipers(VehiclesEnum.Ferndale).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Ferndale Wipers Activated!</color>");
                    SocketConnect.SendData("Ferndale Wipers Activated!");
                }

                //kekmet
                else if (player_current_vehicle.Value == VehiclesEnum.Kekmet.ToString())
                {
                    GetWipers.Wipers(VehiclesEnum.Kekmet).GetComponents<PlayMakerFSM>()[1].FsmVariables.FindFsmBool("On").Value = true;
                    ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Kekmet Wipers Activated!</color>");
                    SocketConnect.SendData("Kekmet Wipers Activated!");
                }
            }
            catch (Exception e)
            {
                ModConsole.Error(e.Message.ToString());
            }
            SocketConnect.message_recieved = false;
        }
    }
}
