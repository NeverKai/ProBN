using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022C RID: 556
	[CallbackIdentity(340)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GameConnectedChatLeave_t
	{
		// Token: 0x04000522 RID: 1314
		public const int k_iCallback = 340;

		// Token: 0x04000523 RID: 1315
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000524 RID: 1316
		public CSteamID m_steamIDUser;

		// Token: 0x04000525 RID: 1317
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bKicked;

		// Token: 0x04000526 RID: 1318
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDropped;
	}
}
