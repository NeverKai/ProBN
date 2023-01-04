using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000230 RID: 560
	[CallbackIdentity(344)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsGetFollowerCount_t
	{
		// Token: 0x0400052F RID: 1327
		public const int k_iCallback = 344;

		// Token: 0x04000530 RID: 1328
		public EResult m_eResult;

		// Token: 0x04000531 RID: 1329
		public CSteamID m_steamID;

		// Token: 0x04000532 RID: 1330
		public int m_nCount;
	}
}
