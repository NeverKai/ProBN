using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BE RID: 702
	[CallbackIdentity(1109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementIconFetched_t
	{
		// Token: 0x04000748 RID: 1864
		public const int k_iCallback = 1109;

		// Token: 0x04000749 RID: 1865
		public CGameID m_nGameID;

		// Token: 0x0400074A RID: 1866
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName;

		// Token: 0x0400074B RID: 1867
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAchieved;

		// Token: 0x0400074C RID: 1868
		public int m_nIconHandle;
	}
}
