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
        private GameObject ROCKET;
        private GameObject PLAYER;
        private GameObject TRAIN_HORN;
        private AudioSource train_horn;
        private AudioSource phone_ring;
        private GameObject UFO;
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
            PLAYER = GameObject.Find("PLAYER");
            SPEAK_DB = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/SpeakDatabase");
            DRINK = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/Drink");
            TRAIN_HORN = GameObject.Find("TRAIN/SpawnEast/TRAIN/Whistle");
            GameObject gameObject = UnityEngine.Object.Instantiate(TRAIN_HORN);
            gameObject.name = "TrainHorn";
            gameObject.transform.SetParent(PLAYER.transform, false);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            train_horn = gameObject.GetComponent<AudioSource>();
            phone_ring = GameObject.Find("MasterAudio/HouseFoley/phone_ring").GetComponent<AudioSource>();
            GameObject gameObject2 = GameObject.Find("MAP/Buildings/DINGONBIISI").transform.FindChild("Misc").transform.FindChild("Thing1").transform.FindChild("Mover").gameObject;
            UFO = new GameObject();
            UFO.name = "UFO";
            GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2);
            gameObject3.SetActive(true);
            gameObject3.transform.SetParent(UFO.transform, false);
            UFO.SetActive(false);
            clock = new Clock24h();
            GameObject[] a = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var obj in a)
            {
                if (obj.name == "rocket")
                {
                    ROCKET = obj;
                    break;
                }
            }
        }

        //Command1 Coroutine
        public void Command1()
        {
            StartCoroutine(CommandEnumerators.EnumCom1(ALARM));
        }

        //Command2 Coroutine
        public void Command2()
        {
            StartCoroutine(CommandEnumerators.EnumCom2(player_current_vehicle));
        }

        //Command3 Coroutine
        public void Command3()
        {
            StartCoroutine(CommandEnumerators.EnumCom3(player_current_vehicle));
        }

        public void Command4()
        {
            StartCoroutine(CommandEnumerators.EnumCom4(HAYOSIKO, min_rot, max_rot, unflip_height, unflip_speed));
        }

        public void Command5()
        {
            StartCoroutine(CommandEnumerators.EnumCom5(SATSUMA, min_rot, max_rot, unflip_height, unflip_speed));
        }

        //Command6 Coroutine
        public void Command6()
        {
            StartCoroutine(CommandEnumerators.EnumCom6(RUSCKO, min_rot, max_rot, unflip_height, unflip_speed));
        }

        //Command7 Coroutine
        public void Command7()
        {
            StartCoroutine(CommandEnumerators.EnumCom7(GIFU, min_rot, max_rot, unflip_height, unflip_speed));
        }

        //Command8 Coroutine
        public void Command8()
        {
            StartCoroutine(CommandEnumerators.EnumCom8(FERNDALE, min_rot, max_rot, unflip_height, unflip_speed));
        }


        //Command9 Coroutine
        public void Command9()
        {
            StartCoroutine(CommandEnumerators.EnumCom9(KEKMET, min_rot, max_rot, unflip_height, unflip_speed));
        }

        //Command10 Coroutine
        public void Command10()
        {
            StartCoroutine(CommandEnumerators.EnumCom10(player_current_vehicle));
        }

        //Command11 Coroutine
        public void Command11()
        {
            StartCoroutine(CommandEnumerators.EnumCom11(DAY, clock));
        }

        //Command12 Coroutine
        public void Command12()
        {
            StartCoroutine(CommandEnumerators.EnumCom12(SPEAK_DB));
        }

        //Command13 Coroutine
        public void Command13()
        {
            StartCoroutine(CommandEnumerators.EnumCom13(SPEAK_DB));
        }

        //Command14 Coroutine
        public void Command14()
        {
            StartCoroutine(CommandEnumerators.EnumCom14(DRINK));
        }

        //Command15 Coroutine
        public void Command15()
        {
            StartCoroutine(CommandEnumerators.EnumCom15(ROCKET, PLAYER));
        }

        //Command16 Coroutine
        public void Command16()
        {
            StartCoroutine(CommandEnumerators.EnumCom16(train_horn));
        }

        //Command17Coroutine
        public void Command17()
        {
            StartCoroutine(CommandEnumerators.EnumCom17(phone_ring));
        }

        //Command18Coroutine
        public void Command18()
        {
            StartCoroutine(CommandEnumerators.EnumCom18(UFO, PLAYER));
        }
    }
}