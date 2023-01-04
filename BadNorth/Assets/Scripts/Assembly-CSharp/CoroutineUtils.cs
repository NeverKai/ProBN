using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Voxels;

// Token: 0x020005DB RID: 1499
internal static class CoroutineUtils
{
	// Token: 0x060026E6 RID: 9958 RVA: 0x0007CCB0 File Offset: 0x0007B0B0
	public static IEnumerator GenerateTimer(float milliseconds, IEnumerator<GenInfo> coroutine)
	{
		int maxTicks = (int)(milliseconds * 10000f);
		Stopwatch stopwatch = Stopwatch.StartNew();
		stopwatch.Start();
		while (coroutine.MoveNext())
		{
			GenInfo current = coroutine.Current;
			if ((stopwatch.ElapsedTicks >= (long)maxTicks && current.interruptable) || current.forceInterrupt)
			{
				yield return null;
				stopwatch.Reset();
				stopwatch.Start();
			}
		}
		yield break;
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x0007CCD4 File Offset: 0x0007B0D4
	public static IEnumerator GenerateTimer(float milliseconds, IEnumerator<bool> coroutine)
	{
		int maxTicks = (int)(milliseconds * 10000f);
		Stopwatch stopwatch = Stopwatch.StartNew();
		stopwatch.Start();
		while (coroutine.MoveNext())
		{
			if (stopwatch.ElapsedTicks >= (long)maxTicks || coroutine.Current)
			{
				yield return coroutine.Current;
				stopwatch.Reset();
				stopwatch.Start();
			}
		}
		yield break;
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x0007CCF8 File Offset: 0x0007B0F8
	public static IEnumerator GenerateTimer(float milliseconds, params IEnumerator[] coroutines)
	{
		int maxTicks = (int)(milliseconds * 10000f);
		Stopwatch stopwatch = Stopwatch.StartNew();
		stopwatch.Start();
		foreach (IEnumerator coroutine in coroutines)
		{
			while (coroutine.MoveNext())
			{
				if (stopwatch.ElapsedTicks >= (long)maxTicks)
				{
					yield return coroutine.Current;
					stopwatch.Reset();
					stopwatch.Start();
				}
			}
		}
		yield break;
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x0007CD1C File Offset: 0x0007B11C
	public static IEnumerator ExceptionHandler(IEnumerator routine, Action handler = null)
	{
		bool running = true;
		while (running)
		{
			try
			{
				running = routine.MoveNext();
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				try
				{
					if (handler != null)
					{
						handler();
					}
				}
				catch (Exception exception2)
				{
					UnityEngine.Debug.LogException(exception2);
				}
				running = false;
			}
			if (running)
			{
				yield return routine.Current;
			}
		}
		yield break;
	}

	// Token: 0x040018EC RID: 6380
	private const float ticksPerMs = 10000f;
}
