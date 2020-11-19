using System;
using UnityEngine;

namespace Minimap
{
	// Token: 0x02000007 RID: 7
	internal class GetVehicleSpeed
	{
		public static float VehSpeed(Vehicles _currentVehicle)
		{
			float differentialSpeed;
			switch (_currentVehicle)
			{
			case Vehicles.Satsuma:
				differentialSpeed = GameObject.Find("SATSUMA(557kg, 248)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Hayosiko:
				differentialSpeed = GameObject.Find("HAYOSIKO(1500kg, 250)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Ruscko:
				differentialSpeed = GameObject.Find("RCO_RUSCKO12(270)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Jonnez:
				differentialSpeed = GameObject.Find("JONNEZ ES(Clone)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Gifu:
				differentialSpeed = GameObject.Find("GIFU(750/450psi)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Kekmet:
				differentialSpeed = GameObject.Find("KEKMET(350-400psi)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			case Vehicles.Ferndale:
				differentialSpeed = GameObject.Find("FERNDALE(1630kg)").gameObject.GetComponent<Drivetrain>().differentialSpeed;
				break;
			default:
				throw new Exception("Invalid Vehicle Type");
			}
			return differentialSpeed;
		}
	}
}
