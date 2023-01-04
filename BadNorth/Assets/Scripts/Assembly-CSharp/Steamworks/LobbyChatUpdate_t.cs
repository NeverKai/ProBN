using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000264 RID: 612
	[CallbackIdentity(506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatUpdate_t
	{
		// Token: 0x04000608 RID: 1544
		public const int k_iCallback = 506;

		// Token: 0x04000609 RID: 1545
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400060A RID: 1546
		public ulong m_ulSteamIDUserChanged;

		// Token: 0x0400060B RID: 1547
		public ulong m_ulSteamIDMakingChange;

		// Token: 0x0400060C RID: 1548
		public uint m_rgfChatMemberStateChange;
	}
}
