using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace MSCSC
{
    public class MSCSC : Mod
    {
        public override string ID => "MSCSC"; //Your mod ID (unique)
        public override string Name => "MSCSC"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;
        private Coroutines cor;
        public GameObject player;

        private string command1 = "alarm";
        private string command2 = "horn";
        private string command3 = "light";
        private string command4 = "flip hayosiko";
        private string command5 = "flip satsuma";
        private string command6 = "flip ruscko";
        private string command7 = "flip gifu";
        private string command8 = "flip ferndale";
        private string command9 = "flip kekmet";
        private string command10 = "engine";
        private string command11 = "time";
        private string command12 = "swear";
        private string command13 = "hello";
        private string command14 = "beer";

        public override void OnLoad()
        {
            SocketConnect.StartThread();
            player = GameObject.Find("PLAYER");
            cor = player.AddComponent<Coroutines>();
            ConsoleCommand.Add(new RestartTCPClient());
            ModConsole.Print("<color=yellow>MSCSC Loaded!</color>");
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
            ActivateAlarm();
            ActivateHorns();
            ActivateLights();
            FlipHayosiko();
            FlipSatsuma();
            FlipRuscko();
            FlipGifu();
            FlipFerndale();
            FlipKekmet();
            TurnEngine();
            GetTime();
            Swear();
            SayHello();
            DrinkBeer();
        }



        //Command1
        private void ActivateAlarm()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command1))
                {
                    cor.Command1();
                }
            }
        }

        //Command2
        private void ActivateHorns()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command2))
                {
                    cor.Command2();
                }
            }
        }

        //Command3
        private void ActivateLights()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command3))
                {
                    cor.Command3();
                }
            }
        }

        private void FlipHayosiko()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command4))
                {
                    cor.Command4();
                }
            }
        }

        private void FlipSatsuma()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command5))
                {
                    cor.Command5();
                }
            }
        }

        private void FlipRuscko()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command6))
                {
                    cor.Command6();
                }
            }
        }

        private void FlipGifu()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command7))
                {
                    cor.Command7();
                }
            }
        }

        private void FlipFerndale()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command8))
                {
                    cor.Command8();
                }
            }
        }

        private void FlipKekmet()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command9))
                {
                    cor.Command9();
                }
            }
        }

        private void TurnEngine()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command10))
                {
                    cor.Command10();
                }
            }
        }

        private void GetTime()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command11))
                {
                    cor.Command11();
                }
            }
        }

        private void Swear()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command12))
                {
                    cor.Command12();
                }
            }
        }

        private void SayHello()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command13))
                {
                    cor.Command13();
                }
            }
        }

        private void DrinkBeer()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(command14))
                {
                    cor.Command14();
                }
            }
        }
    }
}
