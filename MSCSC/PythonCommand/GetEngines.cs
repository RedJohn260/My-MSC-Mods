using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MSCSC
{
    internal class GetEngines
    {
        public static GameObject Engines(VehiclesEnum vehicle)
        {
            GameObject engine = new GameObject();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    engine = GameObject.Find("SATSUMA(557kg, 248)/CarSimulation/Car/Starter").gameObject;
                    break;
                case VehiclesEnum.Hayosiko:
                    engine = GameObject.Find("HAYOSIKO(1500kg, 250)/Starter").gameObject;
                    break;
                case VehiclesEnum.Ruscko:
                    engine = GameObject.Find("RCO_RUSCKO12(270)/Starter").gameObject;
                    break;
                case VehiclesEnum.Gifu:
                    engine = GameObject.Find("GIFU(750/450psi)/Simulation/Starter").gameObject;
                    break;
                case VehiclesEnum.Ferndale:
                    engine = GameObject.Find("FERNDALE(1630kg)/Starter").gameObject;
                    break;
                case VehiclesEnum.Kekmet:
                    engine = GameObject.Find("KEKMET(350-400psi)/Simulation/Starter").gameObject;
                    break;
                case VehiclesEnum.Jonnez:
                    engine = GameObject.Find("JONNEZ ES(Clone)/Starter").gameObject;
                    break;
            }
            return engine;
        }
        public static GameObject Ignition(VehiclesEnum vehicle)
        {
            GameObject ignition = new GameObject();
            switch (vehicle)
            {
                case VehiclesEnum.Satsuma:
                    ignition = GameObject.Find("SATSUMA(557kg, 248)/Electricity").transform.FindChild("PowerON").gameObject;
                    break;
                case VehiclesEnum.Hayosiko:
                    ignition = GameObject.Find("HAYOSIKO(1500kg, 250)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject;
                    break;
                case VehiclesEnum.Ruscko:
                    ignition = GameObject.Find("RCO_RUSCKO12(270)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject;
                    break;
                case VehiclesEnum.Gifu:
                    ignition = GameObject.Find("GIFU(750/450psi)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject;
                    break;
                case VehiclesEnum.Ferndale:
                    ignition = GameObject.Find("FERNDALE(1630kg)").transform.FindChild("LOD").transform.FindChild("Electricity").transform.FindChild("PowerON").gameObject;
                    break;
            }
            return ignition;
        }
    }
}
