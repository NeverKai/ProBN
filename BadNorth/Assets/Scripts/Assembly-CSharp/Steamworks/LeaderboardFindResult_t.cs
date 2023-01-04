using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B9 RID: 697
	[CallbackIdentity(1104)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardFindResult_t
	{
		// Token: 0x04000735 RID: 1845
		public const int k_iCallback = 1104;

		// Token: 0x04000736 RID: 1846
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x04000737 RID: 1847
		public byte m_bLeaderboardFound;
	}
}
