using System;

namespace Steamworks
{
	// Token: 0x020002FD RID: 765
	public enum ESteamAPICallFailure
	{
		// Token: 0x04000A51 RID: 2641
		k_ESteamAPICallFailureNone = -1,
		// Token: 0x04000A52 RID: 2642
		k_ESteamAPICallFailureSteamGone,
		// Token: 0x04000A53 RID: 2643
		k_ESteamAPICallFailureNetworkFailure,
		// Token: 0x04000A54 RID: 2644
		k_ESteamAPICallFailureInvalidHandle,
		// Token: 0x04000A55 RID: 2645
		k_ESteamAPICallFailureMismatchedCallback
	}
}
