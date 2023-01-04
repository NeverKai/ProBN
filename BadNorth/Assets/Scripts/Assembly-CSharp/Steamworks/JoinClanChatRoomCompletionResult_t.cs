using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022E RID: 558
	[CallbackIdentity(342)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct JoinClanChatRoomCompletionResult_t
	{
		// Token: 0x04000529 RID: 1321
		public const int k_iCallback = 342;

		// Token: 0x0400052A RID: 1322
		public CSteamID m_steamIDClanChat;

		// Token: 0x0400052B RID: 1323
		public EChatRoomEnterResponse m_eChatRoomEnterResponse;
	}
}
