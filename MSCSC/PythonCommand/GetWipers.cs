using System;
using UnityEngine;

namespace MSCSC
{
    internal class GetWipers
    {
        public static GameObject Wipers(VehiclesEnum vehicle)
        {
            GameObject wiper_value = new GameObject();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    wiper_value = GameObject.Find("SATSUMA(557kg, 248)/Dashboard/pivot_dashboard/dashboard(Clone)/pivot_meters/dashboard meters(Clone)/Knobs/ButtonsDash/ButtonWipers").gameObject;
                    break;
                case VehiclesEnum.Hayosiko:
                    wiper_value = GameObject.Find(VehicleNames.HAYOSIKO).transform.FindChild("LOD/Dashboard/Knobs/ButtonWipers").gameObject;
                    break;
                case VehiclesEnum.Ruscko:
                    wiper_value = GameObject.Find(VehicleNames.RUSCKO).transform.FindChild("LOD/Dashboard/Knobs/ButtonWipers").gameObject;
                    break;
                case VehiclesEnum.Gifu:
                    wiper_value = GameObject.Find(VehicleNames.GIFU).transform.FindChild("LOD/Dashboard/ButtonWipers").gameObject;
                    break;
                case VehiclesEnum.Ferndale:
                    wiper_value = GameObject.Find(VehicleNames.FERNDALE).transform.FindChild("LOD/Dashboard/Knobs/ButtonWipers").gameObject;
                    break;
                case VehiclesEnum.Kekmet:
                    wiper_value = GameObject.Find(VehicleNames.KEKMET).transform.FindChild("LOD/Dashboard/ButtonWipers").gameObject;
                    break;
            }
            return wiper_value;
        }
    }
}
