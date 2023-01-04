using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BB RID: 699
	[CallbackIdentity(1106)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoreUploaded_t
	{
		// Token: 0x0400073C RID: 1852
		public const int k_iCallback = 1106;

		// Token: 0x0400073D RID: 1853
		public byte m_bSuccess;

		// Token: 0x0400073E RID: 1854
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x0400073F RID: 1855
		public int m_nScore;

		// Token: 0x04000740 RID: 1856
		public byte m_bScoreChanged;

		// Token: 0x04000741 RID: 1857
		public int m_nGlobalRankNew;

		// Token: 0x04000742 RID: 1858
		public int m_nGlobalRankPrevious;
	}
}
