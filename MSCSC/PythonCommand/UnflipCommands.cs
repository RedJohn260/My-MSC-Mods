using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MSCSC
{
    internal class UnflipCommands
    {
        public static Vector3 GetVehiclesPosition(VehiclesEnum vehicle)
        {
            Vector3 position = new Vector3();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    position = GameObject.Find("SATSUMA(557kg, 248)").transform.position;
                        break;
                case VehiclesEnum.Hayosiko:
                    position = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.position;
                    break;
                case VehiclesEnum.Ruscko:
                    position = GameObject.Find("RCO_RUSCKO12(270)").transform.position;
                    break;
                case VehiclesEnum.Gifu:
                    position = GameObject.Find("GIFU(750/450psi)").transform.position;
                    break;
                case VehiclesEnum.Ferndale:
                    position = GameObject.Find("FERNDALE(1630kg)").transform.position;
                    break;
                case VehiclesEnum.Kekmet:
                    position = GameObject.Find("KEKMET(350-400psi)").transform.position;
                    break;
            }
            return position;
        }

        public static Vector3 GetVehiclesRotation(VehiclesEnum vehicle)
        {
            Vector3 rotation = new Vector3();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    rotation = GameObject.Find("SATSUMA(557kg, 248)").transform.localEulerAngles;
                    break;
                case VehiclesEnum.Hayosiko:
                    rotation = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.localEulerAngles;
                    break;
                case VehiclesEnum.Ruscko:
                    rotation = GameObject.Find("RCO_RUSCKO12(270)").transform.localEulerAngles;
                    break;
                case VehiclesEnum.Gifu:
                    rotation = GameObject.Find("GIFU(750/450psi)").transform.localEulerAngles;
                    break;
                case VehiclesEnum.Ferndale:
                    rotation = GameObject.Find("FERNDALE(1630kg)").transform.localEulerAngles;
                    break;
                case VehiclesEnum.Kekmet:
                    rotation = GameObject.Find("KEKMET(350-400psi)").transform.localEulerAngles;
                    break;
            }
            return rotation;
        }
    }
}
