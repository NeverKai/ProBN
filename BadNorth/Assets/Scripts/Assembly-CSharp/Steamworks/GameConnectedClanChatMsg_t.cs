using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022A RID: 554
	[CallbackIdentity(338)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedClanChatMsg_t
	{
		// Token: 0x0400051B RID: 1307
		public const int k_iCallback = 338;

		// Token: 0x0400051C RID: 1308
		public CSteamID m_steamIDClanChat;

		// Token: 0x0400051D RID: 1309
		public CSteamID m_steamIDUser;

		// Token: 0x0400051E RID: 1310
		public int m_iMessageID;
	}
}
