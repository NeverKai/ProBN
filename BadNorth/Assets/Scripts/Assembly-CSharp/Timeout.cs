using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rewired;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200056D RID: 1389
public class Timeout : MonoBehaviour
{
	// Token: 0x1400007F RID: 127
	// (add) Token: 0x06002411 RID: 9233 RVA: 0x00071084 File Offset: 0x0006F484
	// (remove) Token: 0x06002412 RID: 9234 RVA: 0x000710BC File Offset: 0x0006F4BC
	
	public event Action OnTimedOut = delegate()
	{
	};

	// Token: 0x14000080 RID: 128
	// (add) Token: 0x06002413 RID: 9235 RVA: 0x000710F4 File Offset: 0x0006F4F4
	// (remove) Token: 0x06002414 RID: 9236 RVA: 0x0007112C File Offset: 0x0006F52C
	
	public event Action OnTimerReset = delegate()
	{
	};

	// Token: 0x06002415 RID: 9237 RVA: 0x00071162 File Offset: 0x0006F562
	[Conditional("DEMO_BUILD")]
	public static void AddBlocker(object blocker)
	{
		if (!Timeout.blockers.Contains(blocker))
		{
			Timeout.blockers.Add(blocker);
			Timeout.OnUpdatedBlockers();
		}
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x00071184 File Offset: 0x0006F584
	[Conditional("DEMO_BUILD")]
	public static void RemoveBlocker(object blocker)
	{
		Timeout.blockers.Remove(blocker);
		Timeout.OnUpdatedBlockers();
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x00071197 File Offset: 0x0006F597
	public static void OnUpdatedBlockers()
	{
		if (Timeout.instance)
		{
			Timeout.instance.enabled = (Timeout.blockers.Count > 0);
		}
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x000711BF File Offset: 0x0006F5BF
	private void Awake()
	{
		Timeout.instance = this;
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x000711C8 File Offset: 0x0006F5C8
	private void Start()
	{
		if (Application.isEditor && !this.allowInEditor)
		{
			this.allow = false;
		}
		foreach (string a in Environment.GetCommandLineArgs())
		{
			if (string.Equals(a, "NOATTRACT", StringComparison.OrdinalIgnoreCase))
			{
				this.allow = false;
			}
		}
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x00071227 File Offset: 0x0006F627
	private void OnDisable()
	{
		this.ResetTimer();
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x00071230 File Offset: 0x0006F630
	private void Update()
	{
		if (!SplashScreen.isFinished)
		{
			return;
		}
		if (this.TestInputs())
		{
			this.ResetTimer();
			return;
		}
		float num = Mathf.Min(Time.unscaledDeltaTime, Time.maximumDeltaTime);
		this.timer += num;
		if (this.allow && this.timer >= this.timeOutTime && !this.timerExpired)
		{
			this.ExpireTimeout();
		}
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x000712A8 File Offset: 0x0006F6A8
	private bool TestInputs()
	{
		if (this.timer >= this.timeOutTime && this.timer < this.timeOutTime + 1f)
		{
			return false;
		}
		using (new ScopedProfiler("Unity Touches", null))
		{
			if (Input.touchCount > 0)
			{
				return true;
			}
		}
		using (new ScopedProfiler("Keyboards", null))
		{
			if (ReInput.controllers.Keyboard != null && ReInput.controllers.Keyboard.GetAnyButton())
			{
				return true;
			}
		}
		using (new ScopedProfiler("joysticks", null))
		{
			Player player = ReInput.players.GetPlayer(0);
			int joystickCount = player.controllers.joystickCount;
			for (int i = 0; i < joystickCount; i++)
			{
				Joystick joystick = player.controllers.Joysticks[i];
				if (joystick.GetAnyButton())
				{
					return true;
				}
				int axisCount = joystick.axisCount;
				for (int j = 0; j < axisCount; j++)
				{
					float axis = joystick.GetAxis(i);
					if (axis != 0f)
					{
						return true;
					}
				}
				int axis2DCount = joystick.axis2DCount;
				for (int k = 0; k < axis2DCount; k++)
				{
					Vector2 axis2D = joystick.GetAxis2D(i);
					if (axis2D != Vector2.zero)
					{
						return true;
					}
				}
			}
		}
		Mouse mouse = ReInput.controllers.Mouse;
		if (mouse != null)
		{
			using (new ScopedProfiler("ReInput Mouse", null))
			{
				if (mouse.GetAnyButton())
				{
					return true;
				}
				int axisCount2 = mouse.axisCount;
				for (int l = 0; l < axisCount2; l++)
				{
					if (mouse.GetAxis(l) != 0f)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x00071510 File Offset: 0x0006F910
	public void ExpireTimeout()
	{
		this.timer = this.timeOutTime;
		this.OnTimedOut();
		this.timerExpired = true;
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x00071530 File Offset: 0x0006F930
	public void ResetTimer()
	{
		this.timer = 0f;
		this.timerExpired = false;
		this.OnTimerReset();
	}

	// Token: 0x040016B5 RID: 5813
	private static Timeout instance;

	// Token: 0x040016B6 RID: 5814
	private DebugChannelGroup dbgGroup = new DebugChannelGroup("Timout", EVerbosity.Quiet, 0);

	// Token: 0x040016B7 RID: 5815
	[SerializeField]
	private bool allow;

	// Token: 0x040016B8 RID: 5816
	[SerializeField]
	private bool allowInEditor;

	// Token: 0x040016B9 RID: 5817
	[SerializeField]
	private float timeOutTime = 30f;

	// Token: 0x040016BA RID: 5818
	private bool timerExpired;

	// Token: 0x040016BB RID: 5819
	private float timer;

	// Token: 0x040016BE RID: 5822
	private static List<object> blockers = new List<object>();
}
