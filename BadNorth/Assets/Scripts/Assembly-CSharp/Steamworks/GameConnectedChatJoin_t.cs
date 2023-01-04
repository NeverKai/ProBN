using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022B RID: 555
	[CallbackIdentity(339)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameConnectedChatJoin_t
	{
		// Token: 0x0400051F RID: 1311
		public const int k_iCallback = 339;

		// Token: 0x04000520 RID: 1312
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000521 RID: 1313
		public CSteamID m_steamIDUser;
	}
}
