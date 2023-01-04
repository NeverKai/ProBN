using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D5 RID: 213
	public struct UserAchievement
	{
		// Token: 0x040003D8 RID: 984
		public long UserId;

		// Token: 0x040003D9 RID: 985
		public long AchievementId;

		// Token: 0x040003DA RID: 986
		public byte PercentComplete;

		// Token: 0x040003DB RID: 987
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string UnlockedAt;
	}
}
