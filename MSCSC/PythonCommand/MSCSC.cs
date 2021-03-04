using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;

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
        SaveData saveData = SaveUtility.Load<SaveData>();
        private Settings GenerateComs = new Settings("Generate file for custom commands", "Generate", GenerateCustomCommands);
        private static bool IsCustomCommandsGenerated;


        public override void OnLoad()
        {
            SocketConnect.StartThread();
            player = GameObject.Find("PLAYER");
            cor = player.AddComponent<Coroutines>();
            ConsoleCommand.Add(new RestartTCPClient());

            if (File.Exists(SaveUtility.path))
            {
                IsCustomCommandsGenerated = true;
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>MSCSC.xml file is generated.</color>");
                if (IsCustomCommandsGenerated)
                {
                    ContainCommands.command1 = saveData.command1;
                    ContainCommands.command2 = saveData.command2;
                    ContainCommands.command3 = saveData.command3;
                    ContainCommands.command4 = saveData.command4;
                    ContainCommands.command5 = saveData.command5;
                    ContainCommands.command6 = saveData.command6;
                    ContainCommands.command7 = saveData.command7;
                    ContainCommands.command8 = saveData.command8;
                    ContainCommands.command9 = saveData.command9;
                    ContainCommands.command10 = saveData.command10;
                    ContainCommands.command11 = saveData.command11;
                    ContainCommands.command12 = saveData.command12;
                    ContainCommands.command13 = saveData.command13;
                    ContainCommands.command14 = saveData.command14;
                    ContainCommands.command15 = saveData.command15;
                }
            }
            else
            {
                IsCustomCommandsGenerated = false;
                ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>MSCSC.xml file is not generated.</color>");
            }
            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=green>Successfully loaded! </color>");
        }

        public override void ModSettings()
        {
            Settings.AddText(this, "Use it to generate custom commands xml file.");
            Settings.AddText(this, "File is generated in: Mods/Assets/MSCSC folder.");
            Settings.AddButton(this, GenerateComs);
        }

        public static void GenerateCustomCommands()
        {
            SaveUtility.Save(new SaveData
            {
                command1 = ContainCommands.command1,
                command2 = ContainCommands.command2,
                command3 = ContainCommands.command3,
                command4 = ContainCommands.command4,
                command5 = ContainCommands.command5,
                command6 = ContainCommands.command6,
                command7 = ContainCommands.command7,
                command8 = ContainCommands.command8,
                command9 = ContainCommands.command9,
                command10 = ContainCommands.command10,
                command11 = ContainCommands.command11,
                command12 = ContainCommands.command12,
                command13 = ContainCommands.command13,
                command14 = ContainCommands.command14,
                command15 = ContainCommands.command15,
            });
            IsCustomCommandsGenerated = true;
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
            SpawnFirework();
            TrainHorn();
            PhoneRing();
            SpawnUFO();
            TurnOnWipers();
        }

        //Command1
        private void ActivateAlarm()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command1))
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
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command2))
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
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command3))
                {
                    cor.Command3();
                }
            }
        }

        private void FlipHayosiko()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command4))
                {
                    cor.Command4();
                }
            }
        }

        private void FlipSatsuma()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command5))
                {
                    cor.Command5();
                }
            }
        }

        private void FlipRuscko()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command6))
                {
                    cor.Command6();
                }
            }
        }

        private void FlipGifu()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command7))
                {
                    cor.Command7();
                }
            }
        }

        private void FlipFerndale()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command8))
                {
                    cor.Command8();
                }
            }
        }

        private void FlipKekmet()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command9))
                {
                    cor.Command9();
                }
            }
        }

        private void TurnEngine()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command10))
                {
                    cor.Command10();
                }
            }
        }

        private void GetTime()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command11))
                {
                    cor.Command11();
                }
            }
        }

        private void Swear()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command12))
                {
                    cor.Command12();
                }
            }
        }

        private void SayHello()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command13))
                {
                    cor.Command13();
                }
            }
        }

        private void DrinkBeer()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command14))
                {
                    cor.Command14();
                }
            }
        }

        private void SpawnFirework()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command15))
                {
                    cor.Command15();
                }
            }
        }

        private void TrainHorn()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command16))
                {
                    cor.Command16();
                }
            }
        }

        private void PhoneRing()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command17))
                {
                    cor.Command17();
                }
            }
        }

        private void SpawnUFO()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command18))
                {
                    cor.Command18();
                }
            }
        }

        private void TurnOnWipers()
        {
            if (SocketConnect.message_recieved)
            {
                if (SocketConnect.recievedMessage.Contains(ContainCommands.command19))
                {
                    cor.Command19();
                }
            }
        }
    }
}
