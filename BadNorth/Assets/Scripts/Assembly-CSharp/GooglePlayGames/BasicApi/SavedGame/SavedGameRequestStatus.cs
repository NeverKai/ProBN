using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003AF RID: 943
	public enum SavedGameRequestStatus
	{
		// Token: 0x04000D64 RID: 3428
		Success = 1,
		// Token: 0x04000D65 RID: 3429
		TimeoutError = -1,
		// Token: 0x04000D66 RID: 3430
		InternalError = -2,
		// Token: 0x04000D67 RID: 3431
		AuthenticationError = -3,
		// Token: 0x04000D68 RID: 3432
		BadInputError = -4
	}
}
