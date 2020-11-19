using MSCLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
    internal class JunkCarIcons
    {
        private GameObject JunkCar1;

        private GameObject JunkCar2;

        private GameObject JunkCar3;

        private GameObject JunkCar4;

        private GameObject player;

        private Vector3 Car1Pos;

        private Vector3 Car2Pos;

        private Vector3 Car3Pos;

        private Vector3 Car4Pos;

        private Vector3 PlayerPos;

        private float car1Distance;

        private float car2Distance;

        private float car3Distance;

        private float car4Distance;

        private float desiredDistance = 30f;

        private GameObject JunkCar1IconObj;

        private GameObject JunkCar2IconObj;

        private GameObject JunkCar3IconObj;

        private GameObject JunkCar4IconObj;

        private Image JunkIcon;

        private GameObject Fleetari;

        private Vector3 FleetariPos;

        public bool JunkCarIcon1added = false;

        public bool JunkCarIcon1removed = false;

        public bool JunkCarIcon2added = false;

        public bool JunkCarIcon2removed = false;

        public bool JunkCarIcon3added = false;

        public bool JunkCarIcon3removed = false;

        public bool JunkCarIcon4added = false;

        public bool JunkCarIcon4removed = false;

        public bool car1discovered;

        public bool car2discovered;

        public bool car3discovered;

        public bool car4discovered;

        private float FleetariDistanceCar1;

        private float FleetariDistanceCar2;

        private float FleetariDistanceCar3;

        private float FleetariDistanceCar4;

        private float FleetariDesiredDistance = 30f;
        private bool junkCarsFound;
        public bool newGameStarted;

        public void FindJunkCars()
        {
            try
            {
                if (newGameStarted)
                {
                    JunkCar1 = GameObject.Find("Spawn/JunkCar1");
                    JunkCar2 = GameObject.Find("Spawn/JunkCar2");
                    JunkCar3 = GameObject.Find("Spawn/JunkCar3");
                    JunkCar4 = GameObject.Find("Spawn/JunkCar4");
                    junkCarsFound = true;
                    ModConsole.Print("<color=white>[MINIMAP]: </color><color=green>Junk Cars Loading New Game!</color>");
                }
                else
                {
                    JunkCar1 = GameObject.Find("REPAIRSHOP/JunkCar1");
                    JunkCar2 = GameObject.Find("REPAIRSHOP/JunkCar2");
                    JunkCar3 = GameObject.Find("REPAIRSHOP/JunkCar3");
                    JunkCar4 = GameObject.Find("REPAIRSHOP/JunkCar4");
                    junkCarsFound = true;
                    ModConsole.Print("<color=white>[MINIMAP]: </color><color=green>Junk Cars Loading Save Game!</color>");
                }
                player = GameObject.Find("PLAYER");
                JunkIcon = GameObject.Find("JunkCarIcon(Clone)").GetComponent<Image>();
                Fleetari = GameObject.Find("REPAIRSHOP");
                ModConsole.Print("<color=white>[MINIMAP]: </color><color=green>Junk Cars Found!</color>");
            }
            catch
            {
                junkCarsFound = false;
                ModConsole.Print("[MINIMAP]: <color=orange>Junk Cars Not Found!</color>");
            }
        }

        public void CreateNewGameObjects()
        {
            if (junkCarsFound)
            {
                JunkCar1IconObj = new GameObject();
                JunkCar1IconObj.name = "JunkCar1IconObj";
                JunkCar1IconObj.layer = 31;
                JunkCar1IconObj.transform.SetParent(JunkCar1.transform, false);
                JunkCar2IconObj = new GameObject();
                JunkCar2IconObj.name = "JunkCar2IconObj";
                JunkCar2IconObj.layer = 31;
                JunkCar2IconObj.transform.SetParent(JunkCar2.transform, false);
                JunkCar3IconObj = new GameObject();
                JunkCar3IconObj.name = "JunkCar3IconObj";
                JunkCar3IconObj.layer = 31;
                JunkCar3IconObj.transform.SetParent(JunkCar3.transform, false);
                JunkCar4IconObj = new GameObject();
                JunkCar4IconObj.name = "JunkCar4IconObj";
                JunkCar4IconObj.layer = 31;
                JunkCar4IconObj.transform.SetParent(JunkCar4.transform, false);
            }
        }

        public void GetCarPositions()
        {
            if (junkCarsFound)
            {
                Car1Pos = JunkCar1.transform.position;
                Car2Pos = JunkCar2.transform.position;
                Car3Pos = JunkCar3.transform.position;
                Car4Pos = JunkCar4.transform.position;
                PlayerPos = player.transform.position;
                FleetariPos = Fleetari.transform.position;
            }
        }

        public void GetDistanceAndSetIcons()
        {
            if (junkCarsFound)
            {
                car1Distance = Vector3.Distance(PlayerPos, Car1Pos);
                car2Distance = Vector3.Distance(PlayerPos, Car2Pos);
                car3Distance = Vector3.Distance(PlayerPos, Car3Pos);
                car4Distance = Vector3.Distance(PlayerPos, Car4Pos);
                if (car1Distance <= desiredDistance && !JunkCarIcon1removed)
                {
                    car1discovered = true;
                    if (!JunkCarIcon1added)
                    {
                        IconAdding.AddIcon(JunkCar1IconObj, JunkIcon);
                        JunkCarIcon1added = true;
                        car1discovered = true;
                    }
                }
                if (car2Distance <= desiredDistance && !JunkCarIcon2removed)
                {
                    car2discovered = true;
                    if (!JunkCarIcon2added)
                    {
                        IconAdding.AddIcon(JunkCar2IconObj, JunkIcon);
                        JunkCarIcon2added = true;
                    }
                }
                if (car3Distance <= desiredDistance && !JunkCarIcon3removed)
                {
                    car3discovered = true;
                    if (!JunkCarIcon3added)
                    {
                        IconAdding.AddIcon(JunkCar3IconObj, JunkIcon);
                        JunkCarIcon3added = true;
                    }
                }
                if (car4Distance <= desiredDistance && !JunkCarIcon4removed)
                {
                    car4discovered = true;
                    if (!JunkCarIcon4added)
                    {
                        IconAdding.AddIcon(JunkCar4IconObj, JunkIcon);
                        JunkCarIcon4added = true;
                    }
                }
            }
        }

        public void GetDistanceAndRemoveIcons()
        {
            if (junkCarsFound)
            {
                FleetariDistanceCar1 = Vector3.Distance(FleetariPos, Car1Pos);
                FleetariDistanceCar2 = Vector3.Distance(FleetariPos, Car2Pos);
                FleetariDistanceCar3 = Vector3.Distance(FleetariPos, Car3Pos);
                FleetariDistanceCar4 = Vector3.Distance(FleetariPos, Car4Pos);
                if (FleetariDistanceCar1 <= FleetariDesiredDistance && !JunkCarIcon1removed)
                {
                    IconAdding.DestroyIcon(JunkCar1IconObj);
                    JunkCarIcon1removed = true;
                    car1discovered = false;
                }
                if (FleetariDistanceCar2 <= FleetariDesiredDistance && !JunkCarIcon2removed)
                {
                    IconAdding.DestroyIcon(JunkCar2IconObj);
                    JunkCarIcon2removed = true;
                    car2discovered = false;
                }
                if (FleetariDistanceCar3 <= FleetariDesiredDistance && !JunkCarIcon3removed)
                {
                    IconAdding.DestroyIcon(JunkCar3IconObj);
                    JunkCarIcon3removed = true;
                    car3discovered = false;
                }
                if (FleetariDistanceCar4 <= FleetariDesiredDistance && !JunkCarIcon4removed)
                {
                    IconAdding.DestroyIcon(JunkCar4IconObj);
                    JunkCarIcon4removed = true;
                    car4discovered = false;
                }
            }
        }

        public void CheckForDiscoveredCars()
        {
            if (junkCarsFound)
            {
                if (car1discovered)
                {
                    IconAdding.AddIcon(JunkCar1IconObj, JunkIcon);
                    JunkCarIcon1added = true;
                }
                if (car2discovered)
                {
                    IconAdding.AddIcon(JunkCar2IconObj, JunkIcon);
                    JunkCarIcon2added = true;
                }
                if (car3discovered)
                {
                    IconAdding.AddIcon(JunkCar3IconObj, JunkIcon);
                    JunkCarIcon3added = true;
                }
                if (car4discovered)
                {
                    IconAdding.AddIcon(JunkCar4IconObj, JunkIcon);
                    JunkCarIcon4added = true;
                }
            }
        }
    }
}
