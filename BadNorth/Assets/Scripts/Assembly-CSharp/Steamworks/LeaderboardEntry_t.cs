using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000323 RID: 803
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntry_t
	{
		// Token: 0x04000C22 RID: 3106
		public CSteamID m_steamIDUser;

		// Token: 0x04000C23 RID: 3107
		public int m_nGlobalRank;

		// Token: 0x04000C24 RID: 3108
		public int m_nScore;

		// Token: 0x04000C25 RID: 3109
		public int m_cDetails;

		// Token: 0x04000C26 RID: 3110
		public UGCHandle_t m_hUGC;
	}
}
