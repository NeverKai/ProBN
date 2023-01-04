using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000225 RID: 549
	[CallbackIdentity(333)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameLobbyJoinRequested_t
	{
		// Token: 0x04000509 RID: 1289
		public const int k_iCallback = 333;

		// Token: 0x0400050A RID: 1290
		public CSteamID m_steamIDLobby;

		// Token: 0x0400050B RID: 1291
		public CSteamID m_steamIDFriend;
	}
}
