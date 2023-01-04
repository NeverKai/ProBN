using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x02000399 RID: 921
	public enum ResponseStatus
	{
		// Token: 0x04000D0E RID: 3342
		Success = 1,
		// Token: 0x04000D0F RID: 3343
		SuccessWithStale,
		// Token: 0x04000D10 RID: 3344
		LicenseCheckFailed = -1,
		// Token: 0x04000D11 RID: 3345
		InternalError = -2,
		// Token: 0x04000D12 RID: 3346
		NotAuthorized = -3,
		// Token: 0x04000D13 RID: 3347
		VersionUpdateRequired = -4,
		// Token: 0x04000D14 RID: 3348
		Timeout = -5
	}
}
