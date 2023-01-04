using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000228 RID: 552
	[CallbackIdentity(336)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendRichPresenceUpdate_t
	{
		// Token: 0x04000515 RID: 1301
		public const int k_iCallback = 336;

		// Token: 0x04000516 RID: 1302
		public CSteamID m_steamIDFriend;

		// Token: 0x04000517 RID: 1303
		public AppId_t m_nAppID;
	}
}
