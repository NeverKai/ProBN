using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C0 RID: 704
	[CallbackIdentity(1111)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardUGCSet_t
	{
		// Token: 0x04000750 RID: 1872
		public const int k_iCallback = 1111;

		// Token: 0x04000751 RID: 1873
		public EResult m_eResult;

		// Token: 0x04000752 RID: 1874
		public SteamLeaderboard_t m_hSteamLeaderboard;
	}
}
