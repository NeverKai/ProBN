using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000268 RID: 616
	[CallbackIdentity(512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyKicked_t
	{
		// Token: 0x04000619 RID: 1561
		public const int k_iCallback = 512;

		// Token: 0x0400061A RID: 1562
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400061B RID: 1563
		public ulong m_ulSteamIDAdmin;

		// Token: 0x0400061C RID: 1564
		public byte m_bKickedDueToDisconnect;
	}
}
