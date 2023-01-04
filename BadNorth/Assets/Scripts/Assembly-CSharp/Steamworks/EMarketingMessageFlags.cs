using System;

namespace Steamworks
{
	// Token: 0x02000311 RID: 785
	[Flags]
	public enum EMarketingMessageFlags
	{
		// Token: 0x04000B6B RID: 2923
		k_EMarketingMessageFlagsNone = 0,
		// Token: 0x04000B6C RID: 2924
		k_EMarketingMessageFlagsHighPriority = 1,
		// Token: 0x04000B6D RID: 2925
		k_EMarketingMessageFlagsPlatformWindows = 2,
		// Token: 0x04000B6E RID: 2926
		k_EMarketingMessageFlagsPlatformMac = 4,
		// Token: 0x04000B6F RID: 2927
		k_EMarketingMessageFlagsPlatformLinux = 8,
		// Token: 0x04000B70 RID: 2928
		k_EMarketingMessageFlagsPlatformRestrictions = 14
	}
}
