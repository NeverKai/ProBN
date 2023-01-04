using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000231 RID: 561
	[CallbackIdentity(345)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsIsFollowing_t
	{
		// Token: 0x04000533 RID: 1331
		public const int k_iCallback = 345;

		// Token: 0x04000534 RID: 1332
		public EResult m_eResult;

		// Token: 0x04000535 RID: 1333
		public CSteamID m_steamID;

		// Token: 0x04000536 RID: 1334
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsFollowing;
	}
}
