using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200031C RID: 796
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendGameInfo_t
	{
		// Token: 0x04000BEF RID: 3055
		public CGameID m_gameID;

		// Token: 0x04000BF0 RID: 3056
		public uint m_unGameIP;

		// Token: 0x04000BF1 RID: 3057
		public ushort m_usGamePort;

		// Token: 0x04000BF2 RID: 3058
		public ushort m_usQueryPort;

		// Token: 0x04000BF3 RID: 3059
		public CSteamID m_steamIDLobby;
	}
}
