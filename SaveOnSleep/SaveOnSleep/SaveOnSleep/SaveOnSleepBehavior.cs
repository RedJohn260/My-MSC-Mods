using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MSCLoader;
using HutongGames.PlayMaker;
using System;

namespace SaveOnSleep
{
    public class SaveOnSleepBehavior : MonoBehaviour
    {
        SaveOnSleep OnSleep;
        public bool guiActive;
        private Font font = Font.CreateDynamicFontFromOSFont("Impact", 30);
        private string gui_text = "SAVING GAME";
        private string gui_text2 = "PLEASE WAIT...";

        public void Start()
        {
        }
        public void OnGUI()
        {
            
            if (guiActive)
            {
                GUIStyle style = new GUIStyle
                {
                    font = font,
                };
                GUI.matrix = AdjustedMatrix;
                GUI.Label(new Rect(Screen.width / 2.1f, Screen.height / 8, 300, 20), string.Format("<color=white>" + gui_text + Environment.NewLine + gui_text2 + "</color>"), style);
            }
        }

        public static Vector3 GUIScale
        {
            get
            {
                float normalWidth = 1920; 
                float normalHeight = 1080;
                return new Vector3(Screen.width / normalWidth, Screen.height / normalHeight, 1);
            }
        }
        public static Matrix4x4 AdjustedMatrix
        {
            get
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, GUIScale);
            }
        }
        public void SaveSleep()
        {
            StartCoroutine(SleepSaveCor());
        }

        private IEnumerator SleepSaveCor()
        {
            yield return new WaitForSeconds(9f);
            ShowGui();
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SaveGame());
        }

        private IEnumerator SaveGame()
        {
            PlayMakerFSM.BroadcastEvent("SAVEGAME");
            yield return new WaitForSeconds(0.1f);
            ShowGui();
            ModConsole.Print("<color=yellow>[SOS]: </color><color=white> Game Saved!</color>");
            OnSleep.SaveGame = false;
            yield return new WaitForSeconds(1);
           
        }

        private void ShowGui()
        {
            guiActive = !guiActive;
        }
    }
}