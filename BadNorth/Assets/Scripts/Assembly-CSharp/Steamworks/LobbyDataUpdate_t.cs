using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000263 RID: 611
	[CallbackIdentity(505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyDataUpdate_t
	{
		// Token: 0x04000604 RID: 1540
		public const int k_iCallback = 505;

		// Token: 0x04000605 RID: 1541
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000606 RID: 1542
		public ulong m_ulSteamIDMember;

		// Token: 0x04000607 RID: 1543
		public byte m_bSuccess;
	}
}
