using System;
using UnityEngine;

namespace MSCSC
{
    internal class GetHorns
    {
        public static GameObject Horn(VehiclesEnum vehicle)
        {
            GameObject horn_active = new GameObject();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    horn_active = GameObject.Find("SATSUMA(557kg, 248)/CarSimulation").transform.FindChild("CarHorn").transform.gameObject;
                    break;
                case VehiclesEnum.Hayosiko:
                    horn_active = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD").transform.FindChild("Dashboard/ButtonHorn").transform.FindChild("CarHorn").transform.gameObject;
                    break;
                case VehiclesEnum.Ruscko:
                    horn_active = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD").transform.FindChild("Dashboard/Knobs/ButtonHorn").transform.FindChild("CarHorn").transform.gameObject;
                    break;
                case VehiclesEnum.Gifu:
                    horn_active = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD").transform.FindChild("Dashboard/ButtonHorn").transform.FindChild("CarHorn").transform.gameObject;
                    break;
                case VehiclesEnum.Ferndale:
                    horn_active = GameObject.Find("FERNDALE(1630kg)").transform.FindChild("LOD").transform.FindChild("Dashboard/Knobs/ButtonHorn").transform.FindChild("CarHorn").transform.gameObject;
                    break;
            }
            return horn_active;
        }
    }
}
