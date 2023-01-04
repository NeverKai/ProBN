using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000266 RID: 614
	[CallbackIdentity(509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyGameCreated_t
	{
		// Token: 0x04000612 RID: 1554
		public const int k_iCallback = 509;

		// Token: 0x04000613 RID: 1555
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000614 RID: 1556
		public ulong m_ulSteamIDGameServer;

		// Token: 0x04000615 RID: 1557
		public uint m_unIP;

		// Token: 0x04000616 RID: 1558
		public ushort m_usPort;
	}
}
