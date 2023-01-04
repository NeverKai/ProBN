using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B0 RID: 944
	public enum SelectUIStatus
	{
		// Token: 0x04000D6A RID: 3434
		SavedGameSelected = 1,
		// Token: 0x04000D6B RID: 3435
		UserClosedUI,
		// Token: 0x04000D6C RID: 3436
		InternalError = -1,
		// Token: 0x04000D6D RID: 3437
		TimeoutError = -2,
		// Token: 0x04000D6E RID: 3438
		AuthenticationError = -3,
		// Token: 0x04000D6F RID: 3439
		BadInputError = -4
	}
}
