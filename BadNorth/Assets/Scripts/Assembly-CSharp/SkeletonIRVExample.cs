using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200044A RID: 1098
[RequireComponent(typeof(InternetReachabilityVerifier))]
public class SkeletonIRVExample : MonoBehaviour
{
	// Token: 0x0600191A RID: 6426 RVA: 0x000423E9 File Offset: 0x000407E9
	private bool isNetVerified()
	{
		return this.internetReachabilityVerifier.status == InternetReachabilityVerifier.Status.NetVerified;
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000423F9 File Offset: 0x000407F9
	private void forceReverification()
	{
		this.internetReachabilityVerifier.forceReverification();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x00042406 File Offset: 0x00040806
	private void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
	{
		Debug.Log("netStatusChanged: new InternetReachabilityVerifier.Status = " + newStatus);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x00042420 File Offset: 0x00040820
	private IEnumerator waitForNetwork()
	{
		yield return new WaitForEndOfFrame();
		yield return base.StartCoroutine(this.internetReachabilityVerifier.waitForNetVerifiedStatus());
		Debug.Log("waitForNetwork coroutine succeeded and stopped.");
		yield break;
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x0004243B File Offset: 0x0004083B
	private void Start()
	{
		this.internetReachabilityVerifier = base.GetComponent<InternetReachabilityVerifier>();
		this.internetReachabilityVerifier.statusChangedDelegate += this.netStatusChanged;
		base.StartCoroutine(this.waitForNetwork());
	}

	// Token: 0x04000F7C RID: 3964
	private InternetReachabilityVerifier internetReachabilityVerifier;
}
