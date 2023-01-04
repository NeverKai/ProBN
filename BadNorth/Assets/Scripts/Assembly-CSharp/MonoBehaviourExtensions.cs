using System;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public static class MonoBehaviourExtensions
{
	// Token: 0x0600212C RID: 8492 RVA: 0x0005B410 File Offset: 0x00059810
	public static void Invoke(this MonoBehaviour monoBehaviour, Action action, float time)
	{
		monoBehaviour.Invoke(action.Method.Name, time);
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x0005B424 File Offset: 0x00059824
	public static void CancelInvoke(this MonoBehaviour monoBehaviour, Action action)
	{
		monoBehaviour.CancelInvoke(action.Method.Name);
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x0005B437 File Offset: 0x00059837
	public static void InvokeRepeating(this MonoBehaviour monoBehaviour, Action action, float time, float repeatRate)
	{
		monoBehaviour.InvokeRepeating(action.Method.Name, time, repeatRate);
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x0005B44C File Offset: 0x0005984C
	public static bool IsInvoking(this MonoBehaviour monoBehaviour, Action action)
	{
		return monoBehaviour.IsInvoking(action.Method.Name);
	}
}
