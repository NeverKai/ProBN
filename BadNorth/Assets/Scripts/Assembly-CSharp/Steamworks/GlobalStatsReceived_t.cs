using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C1 RID: 705
	[CallbackIdentity(1112)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x04000753 RID: 1875
		public const int k_iCallback = 1112;

		// Token: 0x04000754 RID: 1876
		public ulong m_nGameID;

		// Token: 0x04000755 RID: 1877
		public EResult m_eResult;
	}
}
