using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000269 RID: 617
	[CallbackIdentity(513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyCreated_t
	{
		// Token: 0x0400061D RID: 1565
		public const int k_iCallback = 513;

		// Token: 0x0400061E RID: 1566
		public EResult m_eResult;

		// Token: 0x0400061F RID: 1567
		public ulong m_ulSteamIDLobby;
	}
}
