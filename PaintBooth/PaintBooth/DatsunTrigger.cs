using MSCLoader;
using UnityEngine;

namespace PaintBooth
{
    public class DatsunTrigger : MonoBehaviour
    {
        public static bool CarInBooth = false;
        public static bool CarNotInBooth = false;
        public static bool stopTimer = false;
        void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.name == "gearbox(Clone)")
            {
                ModConsole.Print("Satsuma Entered the Booth");
                CarInBooth = true;
                CarNotInBooth = false;
                stopTimer = false;
            }
        }
        void OnTriggerExit(Collider coll1)
        {
            if (coll1.gameObject.name == "gearbox(Clone)")
            {
                ModConsole.Print("Satsuma Exited the Booth");
                CarInBooth = false;
                CarNotInBooth = true;
                stopTimer = true;
            }
        }
    }
}