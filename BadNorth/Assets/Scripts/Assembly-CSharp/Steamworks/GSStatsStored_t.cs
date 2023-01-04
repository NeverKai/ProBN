using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000241 RID: 577
	[CallbackIdentity(1801)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsStored_t
	{
		// Token: 0x04000570 RID: 1392
		public const int k_iCallback = 1801;

		// Token: 0x04000571 RID: 1393
		public EResult m_eResult;

		// Token: 0x04000572 RID: 1394
		public CSteamID m_steamIDUser;
	}
}
