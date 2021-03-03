using MSCLoader;
using UnityEngine;

namespace SatsumaSound
{
    public class SatsumaSound : Mod
    {
        public override string ID => "SatsumaSound"; //Your mod ID (unique)
        public override string Name => "SatsumaSound"; //You mod name
        public override string Author => "RedJohn260"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        public override void OnLoad()
        {
            AssetBundle ab = LoadAssets.LoadBundle(this, "ss.unity3d");
            AudioClip sisa4 = ab.LoadAsset("idle_sisa4_new.wav") as AudioClip;
            AudioClip sisa4b = ab.LoadAsset("idle_sisa4b_new.wav") as AudioClip;
            AudioClip sisa5 = ab.LoadAsset("idle_sisa5_new.wav") as AudioClip;
            AudioClip sisa_pipe = ab.LoadAsset("idle_sisa_pipe.wav") as AudioClip;

            GameObject SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");

            GameObject audioS1 = SATSUMA.transform.GetChild(40).gameObject;
            GameObject audioS2 = SATSUMA.transform.GetChild(41).gameObject;

            AudioSource s1 = audioS1.GetComponent<AudioSource>();
            AudioSource s2 = audioS2.GetComponent<AudioSource>();
            s1.clip = sisa5;
            s2.clip = sisa4b;
            s1.Play();
            s2.Play();

            GameObject muffler = SATSUMA.transform.FindChild("CarSimulation/Exhaust").transform.FindChild("FromMuffler").gameObject;
            GameObject headers = SATSUMA.transform.FindChild("CarSimulation/Exhaust").transform.FindChild("FromHeaders").gameObject;
            GameObject pipe = SATSUMA.transform.FindChild("CarSimulation/Exhaust").transform.FindChild("FromPipe").gameObject;
            GameObject engine = SATSUMA.transform.FindChild("CarSimulation/Exhaust").transform.FindChild("FromEngine").gameObject;
            muffler.GetComponent<AudioSource>().clip = sisa4b;
            headers.GetComponent<AudioSource>().clip = sisa4;
            pipe.GetComponent<AudioSource>().clip = sisa_pipe;
            engine.GetComponent<AudioSource>().clip = sisa4b;
            ab.Unload(false);
        }
    }
}
