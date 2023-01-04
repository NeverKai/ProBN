using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
public struct ScopedStopwatch : IDisposable
{
	// Token: 0x0600275E RID: 10078 RVA: 0x0007FA1F File Offset: 0x0007DE1F
	public ScopedStopwatch(Stopwatch stopwatch)
	{
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x0007FA21 File Offset: 0x0007DE21
	public ScopedStopwatch(string name, UnityEngine.Object targetObject = null, float minReportMs = 0f)
	{
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x0007FA23 File Offset: 0x0007DE23
	public static float GetMs(long ticks)
	{
		return (float)ticks * 0.0001f;
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06002761 RID: 10081 RVA: 0x0007FA2D File Offset: 0x0007DE2D
	public float elapsedMS
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x0007FA34 File Offset: 0x0007DE34
	void IDisposable.Dispose()
	{
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x0007FA36 File Offset: 0x0007DE36
	public static implicit operator ScopedStopwatch(string name)
	{
		return new ScopedStopwatch(name, null, 0f);
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x0007FA44 File Offset: 0x0007DE44
	public static implicit operator ScopedStopwatch(Stopwatch stopwatch)
	{
		return new ScopedStopwatch(stopwatch);
	}

	// Token: 0x0400193E RID: 6462
	public const float ticksToMS = 0.0001f;
}
