using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000232 RID: 562
	[CallbackIdentity(346)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsEnumerateFollowingList_t
	{
		// Token: 0x04000537 RID: 1335
		public const int k_iCallback = 346;

		// Token: 0x04000538 RID: 1336
		public EResult m_eResult;

		// Token: 0x04000539 RID: 1337
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public CSteamID[] m_rgSteamID;

		// Token: 0x0400053A RID: 1338
		public int m_nResultsReturned;

		// Token: 0x0400053B RID: 1339
		public int m_nTotalResultCount;
	}
}
