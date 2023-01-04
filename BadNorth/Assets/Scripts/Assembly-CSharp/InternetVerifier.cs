using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000023 RID: 35
[RequireComponent(typeof(InternetReachabilityVerifier))]
public class InternetVerifier : Singleton<InternetVerifier>
{
	// Token: 0x06000080 RID: 128 RVA: 0x00004D01 File Offset: 0x00003101
	public bool isNetVerified()
	{
		return this.internetReachabilityVerifier.status == InternetReachabilityVerifier.Status.NetVerified;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004D11 File Offset: 0x00003111
	public void forceReverification()
	{
		this.internetReachabilityVerifier.forceReverification();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00004D1E File Offset: 0x0000311E
	private void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
	{
		Debug.Log("netStatusChanged: new InternetReachabilityVerifier.Status = " + newStatus);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00004D38 File Offset: 0x00003138
	private IEnumerator waitForNetwork()
	{
		yield return new WaitForEndOfFrame();
		yield return base.StartCoroutine(this.internetReachabilityVerifier.waitForNetVerifiedStatus());
		Debug.Log("waitForNetwork coroutine succeeded and stopped.");
		yield break;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00004D53 File Offset: 0x00003153
	private void Start()
	{
		this.internetReachabilityVerifier = base.GetComponent<InternetReachabilityVerifier>();
		this.internetReachabilityVerifier.statusChangedDelegate += this.netStatusChanged;
		base.StartCoroutine(this.waitForNetwork());
	}

	// Token: 0x04000044 RID: 68
	private InternetReachabilityVerifier internetReachabilityVerifier;
}
