using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x0200039A RID: 922
	public enum UIStatus
	{
		// Token: 0x04000D16 RID: 3350
		Valid = 1,
		// Token: 0x04000D17 RID: 3351
		InternalError = -2,
		// Token: 0x04000D18 RID: 3352
		NotAuthorized = -3,
		// Token: 0x04000D19 RID: 3353
		VersionUpdateRequired = -4,
		// Token: 0x04000D1A RID: 3354
		Timeout = -5,
		// Token: 0x04000D1B RID: 3355
		UserClosedUI = -6,
		// Token: 0x04000D1C RID: 3356
		UiBusy = -12,
		// Token: 0x04000D1D RID: 3357
		LeftRoom = -18
	}
}
