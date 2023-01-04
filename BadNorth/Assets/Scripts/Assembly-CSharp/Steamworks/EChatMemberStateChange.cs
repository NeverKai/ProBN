using System;

namespace Steamworks
{
	// Token: 0x020002E1 RID: 737
	[Flags]
	public enum EChatMemberStateChange
	{
		// Token: 0x0400097F RID: 2431
		k_EChatMemberStateChangeEntered = 1,
		// Token: 0x04000980 RID: 2432
		k_EChatMemberStateChangeLeft = 2,
		// Token: 0x04000981 RID: 2433
		k_EChatMemberStateChangeDisconnected = 4,
		// Token: 0x04000982 RID: 2434
		k_EChatMemberStateChangeKicked = 8,
		// Token: 0x04000983 RID: 2435
		k_EChatMemberStateChangeBanned = 16
	}
}
