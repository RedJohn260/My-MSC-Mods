using System.Collections;
using UnityEngine;
using MSCLoader;
using HutongGames.PlayMaker;
using System;

namespace MSCSC
{
    public static class CommandEnumerators
    {
         public static float wait_seconds = 1f;
       
        // Command1 Enumerator
        public static IEnumerator EnumCom1(GameObject alarm)
        {
            ExecuteCommands.ExCom1(alarm);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command2 Enumerator
        public static IEnumerator EnumCom2(FsmString player_current_vehicle)
        {
            ExecuteCommands.ExCom2(player_current_vehicle);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command3 Enumerator
        public static IEnumerator EnumCom3(FsmString player_current_vehicle)
        {
            ExecuteCommands.ExCom3(player_current_vehicle);
            yield return new WaitForSeconds(wait_seconds);
        }

        public static IEnumerator EnumCom4(GameObject HAYOSIKO, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom4(HAYOSIKO, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        public static IEnumerator EnumCom5(GameObject SATSUMA, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom5(SATSUMA, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command6 Enumerator
        public static IEnumerator EnumCom6(GameObject RUSCKO, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom6(RUSCKO, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command7 Enumerator
        public static IEnumerator EnumCom7(GameObject GIFU, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom7(GIFU, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command8 Enumerator
        public static IEnumerator EnumCom8(GameObject FERNDALE, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom8(FERNDALE, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command9 Enumerator
        public static IEnumerator EnumCom9(GameObject KEKMET, float min_rot, float max_rot, float unflip_height, float unflip_speed)
        {
            ExecuteCommands.ExCom9(KEKMET, min_rot, max_rot, unflip_height, unflip_speed);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command10 Enumerator
        public static IEnumerator EnumCom10(FsmString player_current_vehicle)
        {
            ExecuteCommands.ExCom10(player_current_vehicle);
           yield return new WaitForSeconds(wait_seconds);
        }

        // Command11 Enumerator
        public static IEnumerator EnumCom11(GameObject DAY, Clock24h clock)
        {
            ExecuteCommands.ExCom11(DAY, clock);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command12 Enumerator
        public static IEnumerator EnumCom12(GameObject SPEAK_DB)
        {
            ExecuteCommands.ExCom12(SPEAK_DB);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command13 Enumerator
        public static IEnumerator EnumCom13(GameObject SPEAK_DB)
        {
            ExecuteCommands.ExCom13(SPEAK_DB);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command14 Enumerator
        public static IEnumerator EnumCom14(GameObject DRINK)
        {
            ExecuteCommands.ExCom14(DRINK);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command15 Enumerator
        public static IEnumerator EnumCom15(GameObject ROCKET, GameObject PLAYER)
        {
            ExecuteCommands.ExCom15(ROCKET, PLAYER);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command16 Enumerator
        public static IEnumerator EnumCom16(AudioSource train_horn)
        {
            ExecuteCommands.ExCom16(train_horn);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command17 Enumerator
        public static IEnumerator EnumCom17(AudioSource phone_ring)
        {
            ExecuteCommands.ExCom17(phone_ring);
            yield return new WaitForSeconds(wait_seconds);
        }

        // Command18 Enumerator
        public static IEnumerator EnumCom18(GameObject UFO, GameObject PLAYER)
        {
            ExecuteCommands.ExCom18(UFO, PLAYER);
            yield return new WaitForSeconds(20f);
            UFO.SetActive(false);
        }
    }
}
