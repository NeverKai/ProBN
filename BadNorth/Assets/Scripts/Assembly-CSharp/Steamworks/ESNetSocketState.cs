using System;

namespace Steamworks
{
	// Token: 0x020002E5 RID: 741
	public enum ESNetSocketState
	{
		// Token: 0x04000996 RID: 2454
		k_ESNetSocketStateInvalid,
		// Token: 0x04000997 RID: 2455
		k_ESNetSocketStateConnected,
		// Token: 0x04000998 RID: 2456
		k_ESNetSocketStateInitiated = 10,
		// Token: 0x04000999 RID: 2457
		k_ESNetSocketStateLocalCandidatesFound,
		// Token: 0x0400099A RID: 2458
		k_ESNetSocketStateReceivedRemoteCandidates,
		// Token: 0x0400099B RID: 2459
		k_ESNetSocketStateChallengeHandshake = 15,
		// Token: 0x0400099C RID: 2460
		k_ESNetSocketStateDisconnecting = 21,
		// Token: 0x0400099D RID: 2461
		k_ESNetSocketStateLocalDisconnect,
		// Token: 0x0400099E RID: 2462
		k_ESNetSocketStateTimeoutDuringConnect,
		// Token: 0x0400099F RID: 2463
		k_ESNetSocketStateRemoteEndDisconnected,
		// Token: 0x040009A0 RID: 2464
		k_ESNetSocketStateConnectionBroken
	}
}
