using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PaintBooth
{
    public class Painter : MonoBehaviour
    {
        private class PaintJob
        {
            public string name;

            public string creator;

            public string url;

            public string previewUrl;
        }
       

        private Canvas m_canvas;

        private List<PaintJob> m_paintJobs = new List<PaintJob>();

        private AssetBundle m_bundle;

        private GameObject m_satsuma;

        private Texture2D m_satsumaDecalTexture;

        private PlayMakerFSM m_playerViewFsm;

        private MeshCollider m_magazineCollider;

        private Canvas m_canvasT;

        public PlayMakerFSM phone;

        public FsmBool repair;

        //private SaveData saveData;

        private PaintBooth booth;

        private int secondsLeft;
        public static int StaticSeconds;

        public static int loadSeconds;

        private string path7;

       //private string path2;

        public static SaveData saveData;
        private string url6;

       

        private void Awake()
        {
            StartCoroutine(SetupMod());
            phone = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Logic/PhoneLogic").GetComponent<PlayMakerFSM>();
            repair = phone.FsmVariables.GetFsmBool("RepairReady");

        }
       
        private IEnumerator SetupMod()
        {
            while (GameObject.Find("PLAYER") == null)
            {
                yield return null;
            }
            PlayMakerFSM[] fsms = GameObject.Find("PLAYER").GetComponents<PlayMakerFSM>();
            PlayMakerFSM[] array = fsms;
            foreach (PlayMakerFSM playMakerFSM in array)
            {
                if (playMakerFSM.FsmStates.Any((FsmState x) => x.Name == "In Menu"))
                {
                    m_playerViewFsm = playMakerFSM;
                }
            }
            string path2 = PaintBooth.assetPath;
            if (SystemInfo.graphicsDeviceVersion.StartsWith("OpenGL") && Application.platform == RuntimePlatform.WindowsPlayer)
            {
                path2 = Path.Combine(path2, "bundle-linux");
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                path2 = Path.Combine(path2, "bundle-windows");
            }
            else if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                path2 = Path.Combine(path2, "bundle-osx");
            }
            else if (Application.platform == RuntimePlatform.LinuxPlayer)
            {
                path2 = Path.Combine(path2, "bundle-linux");
            }
            if (!File.Exists(path2))
            {
                ModConsole.Error("Couldn't find asset bundle from path " + path2);
                yield break;
            }
            m_bundle = AssetBundle.CreateFromMemoryImmediate(File.ReadAllBytes(path2));
            SetupGUI();
            SetupMagazine();
            
            WWW www = new WWW("https://glot.io/snippets/fhoslu1ytm/raw/PMlist");
            yield return www;
            string[] lines = www.text.Split(new string[2]
            {
                "\r\n",
                "\n"
            }, StringSplitOptions.None);
            string[] array2 = lines;
            foreach (string text in array2)
            {
                string[] array3 = text.Split('|');
                if (array3.Length >= 4)
                {
                    m_paintJobs.Add(new PaintJob
                    {
                        name = array3[0],
                        creator = array3[1],
                        url = array3[2],
                        previewUrl = array3[3]
                    });
                }
            }
            StartCoroutine(LoadTimer());
            ModConsole.Print("Loaded " + m_paintJobs.Count + " paintjobs!");
            ModConsole.Print("Custom Paint Setup!");
            
            path2 = Path.Combine(Application.persistentDataPath, "lastpainturl.txt");
            if (File.Exists(path2))
            {
                
                //LoadImageAndSetSatsuma(File.ReadAllText(path2));
            }


            m_bundle.Unload(unloadAllLoadedObjects: false);
            
            //StartCoroutine(LoadTimer());
        }

        private void SetupMagazine()
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(m_bundle.LoadAsset<GameObject>("MagazinePrefab"));
            //gameObject.transform.position = new Vector3(1552.9f, 5.1f, 737.1f); -- flettari chair
            gameObject.transform.position = new Vector3(1562.2f, 5.2f, 725.5001f); //-- fleetari garage
            m_magazineCollider = gameObject.transform.FindChild("Mesh").gameObject.AddComponent<MeshCollider>();
            m_magazineCollider.convex = true;
            m_magazineCollider.isTrigger = true;
        }

        private void SetupGUI()
        {
            m_canvas = UnityEngine.Object.Instantiate(m_bundle.LoadAssetWithSubAssets<GameObject>("PaintJobCanvas")[0]).GetComponent<Canvas>();
            m_canvas.gameObject.SetActive(value: false);
            m_canvasT = UnityEngine.Object.Instantiate(m_bundle.LoadAssetWithSubAssets<GameObject>("TextCanvas")[0]).GetComponent<Canvas>();
            m_canvasT.gameObject.SetActive(value: false);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 2f) && hitInfo.collider == m_magazineCollider)
            {
                GameObject.Find("GUI/Indicators/Interaction").GetComponent<TextMesh>().text = "Paint Magazine";
                if (Input.GetMouseButtonDown(0))
                {
                    m_canvas.gameObject.SetActive(value: true);
                    ((BoolTest)m_playerViewFsm.FsmStates.First((FsmState x) => x.Name == "In Menu").Actions.First((FsmStateAction x) => x is BoolTest)).boolVariable.Value = true;
                    m_playerViewFsm.SendEvent("MENU");
                    if (m_canvas.transform.FindChild("Background/ScrollRect/View/Content").childCount > 0)
                    {
                        return;
                    }
                    Transform parent = m_canvas.transform.FindChild("Background/ScrollRect/View/Content");
                    Transform transform = m_canvas.transform.FindChild("Background/Item");
                    transform.gameObject.SetActive(value: false);
                    foreach (PaintJob paintJob in m_paintJobs)
                    {
                        Transform transform2 = UnityEngine.Object.Instantiate(transform);
                        transform2.transform.SetParent(parent, worldPositionStays: false);
                        transform2.transform.FindChild("Text").GetComponent<Text>().text = paintJob.name + "\nBy: " + paintJob.creator;
                        transform2.gameObject.SetActive(value: true);
                        StartCoroutine(LoadPreviewForImage(paintJob.previewUrl, transform2.GetComponent<Image>()));
                        string copy = paintJob.url;
                        transform2.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            if (loadSeconds == 10)
                            {
                                SetUrl(copy);
                                ResetTimer();
                                CanvasClose();
                                PayMoney();
                                StartTimer(copy);
                                ModConsole.Print("Fleetari Call Value : " + repair.Value.ToString());
                            }
                            else
                            {
                                
                                CanvasClose();
                                StartCoroutine(TextC());
                            }
                            
                        });
                    }
                }
            }
            if (m_canvas.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                m_canvas.gameObject.SetActive(value: false);
            }
           
        }
        private void CanvasClose()
        {
            ((BoolTest)m_playerViewFsm.FsmStates.First((FsmState x) => x.Name == "In Menu").Actions.First((FsmStateAction x) => x is BoolTest)).boolVariable.Value = false;
            m_canvas.gameObject.SetActive(value: false);
        }

        private IEnumerator LoadPreviewForImage(string url, Image image)
        {
            WWW www = new WWW(url);
            yield return www;
            Texture2D t = www.texture;
            image.sprite = Sprite.Create(t, new Rect(0f, 0f, t.width, t.height), Vector2.zero);
        }

        private void LoadImageAndSetSatsuma(string url)
        {
            StartCoroutine(SetSatsumaImage(url));
            ((BoolTest)m_playerViewFsm.FsmStates.First((FsmState x) => x.Name == "In Menu").Actions.First((FsmStateAction x) => x is BoolTest)).boolVariable.Value = false;
            m_canvas.gameObject.SetActive(value: false);
        }

        private IEnumerator SetSatsumaImage(string url)
        {
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "lastpainturl.txt"), url);
            WWW www = new WWW(url);
            yield return www;
            m_satsumaDecalTexture = www.texture;
            
            m_satsuma = PlayMakerGlobals.Instance.Variables.FindFsmGameObject("TheCar").Value;
            ChangeSatsumaDecalTexture("Body/car body(xxxxx)");
            ChangeSatsumaDecalTexture("Body/pivot_hood/hood(Clone)");
            ChangeSatsumaDecalTexture("Body/pivot_door_left/door left(Clone)");
            ChangeSatsumaDecalTexture("Body/pivot_door_right/door right(Clone)");
            ChangeSatsumaDecalTexture("Body/pivot_fender_left/fender left(Clone)");
            ChangeSatsumaDecalTexture("Body/pivot_fender_right/fender right(Clone)");
            ChangeSatsumaDecalTexture("Body/pivot_bootlid/bootlid(Clone)");
        }
        private void SetUrl(string url)
        {
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "lastpainturl.txt"), url);
            WWW www = new WWW(url);
            m_satsumaDecalTexture = www.texture;
        }

        private void ChangeSatsumaDecalTexture(string p)
        {
            Transform transform = m_satsuma.transform.FindChild(p);
            if (transform != null)
            {
                MeshRenderer component = transform.GetComponent<MeshRenderer>();
                component.sharedMaterial.SetTexture("_DetailAlbedoMap", m_satsumaDecalTexture);
            }
        }
        public int seconds;
        private bool timerStopped = false;
        private bool paintjobApplied;
        public void ResetTimer()
        {
            
            seconds = 0;
            ModConsole.Print("Timer Resetted");
        }
        public void StartTimer(string copy)
        {
            StartCoroutine(RunTimer(copy));
            ModConsole.Print("Timer Started");
        }
        public void StopTimer(string copy)
        {
            StopCoroutine(RunTimer(copy));
            ModConsole.Print("Timer Stopped");
        }
        public IEnumerator RunTimer(string copy)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                seconds++;
                StaticSeconds = seconds;
                //ModConsole.Print("Static Seconds :" + StaticSeconds.ToString("F2"));
                if (seconds == 10)
                {
                    seconds = 0;

                    LoadImageAndSetSatsuma(copy);
                    if (!repair.Value)
                    {
                        repair.Value = true;
                        ModConsole.Print("Fleetari Call Value : " + repair.Value.ToString());
                    }
                    else
                    {
                        repair.Value = false;
                    }
                    ModConsole.Print("Timer is at PEAK");
                    ModConsole.Print("Paintjob Done");
                    StopCoroutine(RunTimer(copy));
                    timerStopped = true;
                    paintjobApplied = true;
                    break;

                }
                ModConsole.Print("Timer : " + seconds.ToString("F2"));
            }
        }
        private void PayMoney()
        {
            float amount = 1000f;
            FsmFloat money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
            money.Value -= amount;
        }
        public IEnumerator LoadTimer()
        {
            
            while (true)
            {
                if (loadSeconds < 10)
                {
                    yield return new WaitForSeconds(1);
                    loadSeconds++;
                    StaticSeconds = loadSeconds;
                    if (loadSeconds == 10)
                    {
                        loadSeconds = 10;
                        path7 = PaintBooth.assetPath = Path.Combine(Application.persistentDataPath, "lastpainturl.txt");
                        LoadImageAndSetSatsuma((File.ReadAllText(path7)));
                        StopCoroutine(LoadTimer());
                        if (!repair.Value)
                        {
                            repair.Value = true;
                        }
                        else
                        {
                            repair.Value = false;
                        }
                        ModConsole.Print("Loading Paintjob Complite");
                        break;
                    }
                    ModConsole.Print(loadSeconds.ToString("F2"));

                }
                else if (loadSeconds > 9)
                {
                    path7 = PaintBooth.assetPath = Path.Combine(Application.persistentDataPath, "lastpainturl.txt");
                    LoadImageAndSetSatsuma((File.ReadAllText(path7)));
                    ModConsole.Print("Loading Load Paintjob");
                    StopCoroutine(LoadTimer());
                    break;
                }
            }
        }
        public static void LoadData()
        {
            var path = Path.Combine(Application.persistentDataPath, "PaintBooth.xml");
            if (!File.Exists(path))
                return;

            var data = SaveUtility.DeserializeReadFile<SaveData>(path);
            loadSeconds = data.timeLeft;
            ModConsole.Print("loadSeconds Value Loaded");
        }
        private IEnumerator TextC()
        {
            m_canvasT.gameObject.SetActive(value: true);
            yield return new WaitForSeconds(5);
            m_canvasT.gameObject.SetActive(value: false);
            StopCoroutine(TextC());
        }
        
          
               
                
                
            
        
        

    }
}
