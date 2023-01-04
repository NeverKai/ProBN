using System;

namespace Steamworks
{
	// Token: 0x020002D6 RID: 726
	public enum EUserRestriction
	{
		// Token: 0x04000914 RID: 2324
		k_nUserRestrictionNone,
		// Token: 0x04000915 RID: 2325
		k_nUserRestrictionUnknown,
		// Token: 0x04000916 RID: 2326
		k_nUserRestrictionAnyChat,
		// Token: 0x04000917 RID: 2327
		k_nUserRestrictionVoiceChat = 4,
		// Token: 0x04000918 RID: 2328
		k_nUserRestrictionGroupChat = 8,
		// Token: 0x04000919 RID: 2329
		k_nUserRestrictionRating = 16,
		// Token: 0x0400091A RID: 2330
		k_nUserRestrictionGameInvites = 32,
		// Token: 0x0400091B RID: 2331
		k_nUserRestrictionTrading = 64
	}
}
