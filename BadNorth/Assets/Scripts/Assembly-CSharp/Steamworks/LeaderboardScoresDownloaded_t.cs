using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BA RID: 698
	[CallbackIdentity(1105)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x04000738 RID: 1848
		public const int k_iCallback = 1105;

		// Token: 0x04000739 RID: 1849
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x0400073A RID: 1850
		public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		// Token: 0x0400073B RID: 1851
		public int m_cEntryCount;
	}
}
