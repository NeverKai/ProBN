using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027B RID: 635
	[CallbackIdentity(1202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionRequest_t
	{
		// Token: 0x04000639 RID: 1593
		public const int k_iCallback = 1202;

		// Token: 0x0400063A RID: 1594
		public CSteamID m_steamIDRemote;
	}
}
