using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000242 RID: 578
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsUnloaded_t
	{
		// Token: 0x04000573 RID: 1395
		public const int k_iCallback = 1108;

		// Token: 0x04000574 RID: 1396
		public CSteamID m_steamIDUser;
	}
}
