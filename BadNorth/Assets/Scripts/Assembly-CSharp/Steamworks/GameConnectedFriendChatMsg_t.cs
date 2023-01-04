using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022F RID: 559
	[CallbackIdentity(343)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedFriendChatMsg_t
	{
		// Token: 0x0400052C RID: 1324
		public const int k_iCallback = 343;

		// Token: 0x0400052D RID: 1325
		public CSteamID m_steamIDUser;

		// Token: 0x0400052E RID: 1326
		public int m_iMessageID;
	}
}
