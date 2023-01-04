using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003AE RID: 942
	public enum ConflictResolutionStrategy
	{
		// Token: 0x04000D5D RID: 3421
		UseLongestPlaytime,
		// Token: 0x04000D5E RID: 3422
		UseOriginal,
		// Token: 0x04000D5F RID: 3423
		UseUnmerged,
		// Token: 0x04000D60 RID: 3424
		UseManual,
		// Token: 0x04000D61 RID: 3425
		UseLastKnownGood,
		// Token: 0x04000D62 RID: 3426
		UseMostRecentlySaved
	}
}
