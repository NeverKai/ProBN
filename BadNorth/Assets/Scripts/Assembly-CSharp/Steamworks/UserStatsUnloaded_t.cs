using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BD RID: 701
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsUnloaded_t
	{
		// Token: 0x04000746 RID: 1862
		public const int k_iCallback = 1108;

		// Token: 0x04000747 RID: 1863
		public CSteamID m_steamIDUser;
	}
}
