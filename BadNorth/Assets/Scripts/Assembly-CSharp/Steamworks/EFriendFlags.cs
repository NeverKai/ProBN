using System;

namespace Steamworks
{
	// Token: 0x020002D5 RID: 725
	[Flags]
	public enum EFriendFlags
	{
		// Token: 0x04000907 RID: 2311
		k_EFriendFlagNone = 0,
		// Token: 0x04000908 RID: 2312
		k_EFriendFlagBlocked = 1,
		// Token: 0x04000909 RID: 2313
		k_EFriendFlagFriendshipRequested = 2,
		// Token: 0x0400090A RID: 2314
		k_EFriendFlagImmediate = 4,
		// Token: 0x0400090B RID: 2315
		k_EFriendFlagClanMember = 8,
		// Token: 0x0400090C RID: 2316
		k_EFriendFlagOnGameServer = 16,
		// Token: 0x0400090D RID: 2317
		k_EFriendFlagRequestingFriendship = 128,
		// Token: 0x0400090E RID: 2318
		k_EFriendFlagRequestingInfo = 256,
		// Token: 0x0400090F RID: 2319
		k_EFriendFlagIgnored = 512,
		// Token: 0x04000910 RID: 2320
		k_EFriendFlagIgnoredFriend = 1024,
		// Token: 0x04000911 RID: 2321
		k_EFriendFlagChatMember = 4096,
		// Token: 0x04000912 RID: 2322
		k_EFriendFlagAll = 65535
	}
}
