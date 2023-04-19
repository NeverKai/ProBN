using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReflexCLI.Attributes;
using UnityEngine;

// Token: 0x02000604 RID: 1540
public class TimeManager : MonoBehaviour
{
	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0008048A File Offset: 0x0007E88A
	// (set) Token: 0x060027A2 RID: 10146 RVA: 0x00080491 File Offset: 0x0007E891
	[ConsoleCommand("")]
	private static bool enableTimeScale
	{
		get
		{
			return TimeManager._enableTimeScale;
		}
		set
		{
			TimeManager._enableTimeScale = value;
			TimeManager.UpdateTimeScale();
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x060027A3 RID: 10147 RVA: 0x0008049E File Offset: 0x0007E89E
	// (set) Token: 0x060027A4 RID: 10148 RVA: 0x000804A5 File Offset: 0x0007E8A5
	[ConsoleCommand("")]
	private static float debugTimeScale
	{
		get
		{
			return TimeManager._debugTimeScale;
		}
		set
		{
			TimeManager._debugTimeScale = value;
			TimeManager.UpdateTimeScale();
		}
	}

	// Token: 0x14000088 RID: 136
	// (add) Token: 0x060027A5 RID: 10149 RVA: 0x000804B4 File Offset: 0x0007E8B4
	// (remove) Token: 0x060027A6 RID: 10150 RVA: 0x000804E8 File Offset: 0x0007E8E8
	
	public static event Action<float> onTimeScaleChanged;

	// Token: 0x060027A7 RID: 10151 RVA: 0x0008051C File Offset: 0x0007E91C
	public static void RequestTimeScale(object requester, float timeScale)
	{
		if (TimeManager.scales.ContainsKey(requester))
		{
			TimeManager.scales[requester] = timeScale;
		}
		else
		{
			TimeManager.scales.Add(requester, timeScale);
		}
		TimeManager.UpdateTimeScale();
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x00080550 File Offset: 0x0007E950
	public static void RemoveTimeScale(object requester)
	{
		TimeManager.scales.Remove(requester);
		TimeManager.UpdateTimeScale();
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x00080563 File Offset: 0x0007E963
	public static bool HasTimeScaleRequest(object requester)
	{
		return TimeManager.scales.ContainsKey(requester);
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x00080570 File Offset: 0x0007E970
	private static void UpdateTimeScale()
	{
		float num = 1f;
		foreach (KeyValuePair<object, float> keyValuePair in TimeManager.scales)
		{
			if (keyValuePair.Value < num)
			{
				num = keyValuePair.Value;
			}
		}
		bool flag = TimeManager.current != num;
		TimeManager.current = num;
		if (flag)
		{
			TimeManager.onTimeScaleChanged(num);
		}
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x00080604 File Offset: 0x0007EA04
	private void LateUpdate()
	{
		Time.timeScale = TimeManager.current;
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x00080610 File Offset: 0x0007EA10
	// Note: this type is marked as 'beforefieldinit'.
	static TimeManager()
	{
		TimeManager.onTimeScaleChanged = delegate(float A_0)
		{
		};
	}

	// Token: 0x04001971 RID: 6513
	private static bool _enableTimeScale = true;

	// Token: 0x04001972 RID: 6514
	private static float _debugTimeScale = 1f;

	// Token: 0x04001973 RID: 6515
	private static Dictionary<object, float> scales = new Dictionary<object, float>();

	// Token: 0x04001974 RID: 6516
	private static float current = 1f;
}
