using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027D RID: 637
	[CallbackIdentity(1201)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SocketStatusCallback_t
	{
		// Token: 0x0400063E RID: 1598
		public const int k_iCallback = 1201;

		// Token: 0x0400063F RID: 1599
		public SNetSocket_t m_hSocket;

		// Token: 0x04000640 RID: 1600
		public SNetListenSocket_t m_hListenSocket;

		// Token: 0x04000641 RID: 1601
		public CSteamID m_steamIDRemote;

		// Token: 0x04000642 RID: 1602
		public int m_eSNetSocketState;
	}
}
