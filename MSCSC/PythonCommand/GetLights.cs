using System;
using UnityEngine;

namespace MSCSC
{
    internal class GetLights
    {
        public static GameObject Lights(VehiclesEnum vehicle)
        {
            GameObject light_value = new GameObject();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    light_value = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Knobs/ButtonsDash/LightModes").gameObject;
                    break;
                case VehiclesEnum.Hayosiko:
                    light_value = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD").transform.FindChild("Dashboard/Knobs/Lights").transform.gameObject;
                    break;
                case VehiclesEnum.Ruscko:
                    light_value = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD").transform.FindChild("Dashboard/Knobs/Lights").transform.gameObject;
                    break;
                case VehiclesEnum.Gifu:
                    light_value = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD").transform.FindChild("Dashboard/ButtonLights").transform.gameObject;
                    break;
                case VehiclesEnum.Ferndale:
                    light_value = GameObject.Find("FERNDALE(1630kg)").transform.FindChild("LOD").transform.FindChild("Dashboard/Knobs/Lights").transform.gameObject;
                    break;
                case VehiclesEnum.Kekmet:
                    light_value = GameObject.Find("KEKMET(350-400psi)").transform.FindChild("LOD").transform.FindChild("Dashboard/Lights").transform.gameObject;
                    break;
                case VehiclesEnum.Jonnez:
                    light_value = GameObject.Find("JONNEZ ES(Clone)").transform.FindChild("LOD").transform.FindChild("Dashboard/Lights").transform.gameObject;
                    break;
            }
            return light_value;
        }
    }
}
