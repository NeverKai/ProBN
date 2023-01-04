using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027C RID: 636
	[CallbackIdentity(1203)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct P2PSessionConnectFail_t
	{
		// Token: 0x0400063B RID: 1595
		public const int k_iCallback = 1203;

		// Token: 0x0400063C RID: 1596
		public CSteamID m_steamIDRemote;

		// Token: 0x0400063D RID: 1597
		public byte m_eP2PSessionError;
	}
}
