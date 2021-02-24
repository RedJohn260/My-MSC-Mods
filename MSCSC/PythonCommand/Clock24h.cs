using HutongGames.PlayMaker;
using UnityEngine;



public class Clock24h
{
	private  GameObject sun;

	private  FsmFloat sun_rotation;

	private Transform needle_h;

	private Transform needle_m;

	public bool IsAfternoon
	{
		get
		{
			if ((double)sun_rotation.Value <= 330.0)
			{
				return (double)sun_rotation.Value <= 150.0;
			}
			return true;
		}
	}

	public float Hour12F => (float)(((360.0 - (double)needle_h.localRotation.eulerAngles.y) / 30.0 + 2.0) % 12.0);

	public float Hour24F
	{
		get
		{
			if (!IsAfternoon)
			{
				return Hour12F;
			}
			return Hour12F + 12f;
		}
	}

	public float MinuteF => (float)((360.0 - (double)needle_m.localRotation.eulerAngles.y) / 6.0);

	public float SecondF => (float)((double)MinuteF * 60.0 % 60.0);

	public int Hour12 => Mathf.FloorToInt(Hour12F);

	public int Hour24 => Mathf.FloorToInt(Hour24F);

	public int Minute => Mathf.FloorToInt(MinuteF);

	public int Second => Mathf.FloorToInt(SecondF);

	public float AngleHour => needle_h.localRotation.eulerAngles.y;

	public float AngleMinute => needle_m.localRotation.eulerAngles.y;

	public float AngleSun => sun_rotation.Value;

	public Clock24h()
	{
		sun = GameObject.Find("MAP/SUN/Pivot");
		sun_rotation = sun.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmFloat("Rotation");
		needle_h = GameObject.Find("YARD/Building/Dynamics/SuomiClock/Clock/hour/NeedleHour").transform;
		needle_m = GameObject.Find("YARD/Building/Dynamics/SuomiClock/Clock/minute/NeedleMinute").transform;
	}

	public override string ToString()
	{
		return $"{Hour24:0}:{Minute:00}";
	}
}

