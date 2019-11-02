using MSCLoader;
using UnityEngine;

namespace Kanister
{
    public class Kanister : Mod
    {
        public override string ID => "Kanister"; //Your mod ID (unique)
        public override string Name => "Kanister"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        private AssetBundle ab;
        private GameObject kanisterDizel;
        private GameObject kanisterBenzin;
        private GameObject cepOpen;
        private GameObject cepClosed;
        private Material kanMat;
        private Material cepMat;
        private GameObject gameobject1;
        private GameObject gameobject2;
        private GameObject gameobject3;
        private GameObject gameobject4;




        public override void OnLoad()
        {
            ab = LoadAssets.LoadBundle(this, "kanister.unity3d");
            kanisterBenzin = ab.LoadAsset("kanister.prefab") as GameObject;
            kanisterDizel = ab.LoadAsset("kanister.prefab") as GameObject;
            cepClosed = ab.LoadAsset("cepClosed.prefab") as GameObject;
            cepOpen = ab.LoadAsset("cepOpen.prefab") as GameObject;
            kanMat = ab.LoadAsset("defaultMat.mat") as Material;
            cepMat = ab.LoadAsset("cepClosedMat.mat") as Material;
            
            ab.Unload(false);

            gameobject1 = Object.Instantiate(kanisterBenzin);
            Object.Destroy(gameobject1);
            gameobject2 = Object.Instantiate(kanisterDizel);
            Object.Destroy(gameobject2);
            gameobject3 = Object.Instantiate(cepOpen);
            Object.Destroy(gameobject3);
            gameobject4 = Object.Instantiate(cepClosed);
            Object.Destroy(gameobject4);
 


            GameObject.Find("gasoline(itemx)").GetComponentInChildren<MeshFilter>().sharedMesh = kanisterBenzin.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("diesel(itemx)").GetComponentInChildren<MeshFilter>().sharedMesh = kanisterDizel.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("diesel(itemx)/CapClosed").GetComponentInChildren<MeshFilter>().sharedMesh = cepClosed.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("gasoline(itemx)/CapClosed").GetComponentInChildren<MeshFilter>().sharedMesh = cepClosed.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("diesel(itemx)/CapClosed").GetComponentInChildren<MeshRenderer>().sharedMaterial = cepMat;
            GameObject.Find("gasoline(itemx)/CapClosed").GetComponentInChildren<MeshRenderer>().sharedMaterial = cepMat;
            GameObject.Find("gasoline(itemx)").GetComponentInChildren<MeshRenderer>().sharedMaterial = kanMat;
            GameObject.Find("diesel(itemx)").GetComponentInChildren<MeshRenderer>().sharedMaterial = kanMat;
            var tt = GameObject.Find("gasoline(itemx)").transform.Find("Triggers/CapOpen").gameObject;
            tt.GetComponent<MeshFilter>().sharedMesh = cepOpen.GetComponent<MeshFilter>().sharedMesh;
            tt.GetComponent<MeshRenderer>().sharedMaterial = cepMat;
            var tr = GameObject.Find("diesel(itemx)").transform.Find("Triggers/CapOpen").gameObject;
            tr.GetComponent<MeshFilter>().sharedMesh = cepOpen.GetComponent<MeshFilter>().sharedMesh;
            tr.GetComponent<MeshRenderer>().sharedMaterial = cepMat;

            ModConsole.Print("<b><color=green>New JerryCan Loaded</color></b>");


            
        }

        public override void Update()
        {
            
        }
    }
}
