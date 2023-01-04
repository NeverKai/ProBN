using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000265 RID: 613
	[CallbackIdentity(507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatMsg_t
	{
		// Token: 0x0400060D RID: 1549
		public const int k_iCallback = 507;

		// Token: 0x0400060E RID: 1550
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400060F RID: 1551
		public ulong m_ulSteamIDUser;

		// Token: 0x04000610 RID: 1552
		public byte m_eChatEntryType;

		// Token: 0x04000611 RID: 1553
		public uint m_iChatID;
	}
}
