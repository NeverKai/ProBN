using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000261 RID: 609
	[CallbackIdentity(503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyInvite_t
	{
		// Token: 0x040005FB RID: 1531
		public const int k_iCallback = 503;

		// Token: 0x040005FC RID: 1532
		public ulong m_ulSteamIDUser;

		// Token: 0x040005FD RID: 1533
		public ulong m_ulSteamIDLobby;

		// Token: 0x040005FE RID: 1534
		public ulong m_ulGameID;
	}
}
