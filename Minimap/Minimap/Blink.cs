using System;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
	public Image imageToToggle;

	public float interval = 1f;

	public float startDelay = 0.5f;

	public bool currentState = false;

	public bool defaultState = false;

	private bool isBlinking = false;

	private void Start()
	{
		this.imageToToggle.enabled = this.defaultState;
		this.StartBlink();
	}

	public void StartBlink()
	{
		bool flag = this.isBlinking;
		if (!flag)
		{
			bool flag2 = this.imageToToggle != null;
			if (flag2)
			{
				this.isBlinking = true;
				base.InvokeRepeating("ToggleState", this.startDelay, this.interval);
			}
		}
	}

	public void ToggleState()
	{
		this.imageToToggle.enabled = !this.imageToToggle.enabled;
	}
}
