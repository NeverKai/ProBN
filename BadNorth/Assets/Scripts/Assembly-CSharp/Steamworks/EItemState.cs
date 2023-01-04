using System;

namespace Steamworks
{
	// Token: 0x020002F5 RID: 757
	[Flags]
	public enum EItemState
	{
		// Token: 0x04000A1F RID: 2591
		k_EItemStateNone = 0,
		// Token: 0x04000A20 RID: 2592
		k_EItemStateSubscribed = 1,
		// Token: 0x04000A21 RID: 2593
		k_EItemStateLegacyItem = 2,
		// Token: 0x04000A22 RID: 2594
		k_EItemStateInstalled = 4,
		// Token: 0x04000A23 RID: 2595
		k_EItemStateNeedsUpdate = 8,
		// Token: 0x04000A24 RID: 2596
		k_EItemStateDownloading = 16,
		// Token: 0x04000A25 RID: 2597
		k_EItemStateDownloadPending = 32
	}
}
