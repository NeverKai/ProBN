using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000239 RID: 569
	[CallbackIdentity(206)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientAchievementStatus_t
	{
		// Token: 0x0400054D RID: 1357
		public const int k_iCallback = 206;

		// Token: 0x0400054E RID: 1358
		public ulong m_SteamID;

		// Token: 0x0400054F RID: 1359
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_pchAchievement;

		// Token: 0x04000550 RID: 1360
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUnlocked;
	}
}
