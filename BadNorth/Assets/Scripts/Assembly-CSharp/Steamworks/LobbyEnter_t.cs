using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000262 RID: 610
	[CallbackIdentity(504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyEnter_t
	{
		// Token: 0x040005FF RID: 1535
		public const int k_iCallback = 504;

		// Token: 0x04000600 RID: 1536
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000601 RID: 1537
		public uint m_rgfChatPermissions;

		// Token: 0x04000602 RID: 1538
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocked;

		// Token: 0x04000603 RID: 1539
		public uint m_EChatRoomEnterResponse;
	}
}
