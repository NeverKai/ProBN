using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B6 RID: 694
	[CallbackIdentity(1101)]
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	public struct UserStatsReceived_t
	{
		// Token: 0x04000728 RID: 1832
		public const int k_iCallback = 1101;

		// Token: 0x04000729 RID: 1833
		[FieldOffset(0)]
		public ulong m_nGameID;

		// Token: 0x0400072A RID: 1834
		[FieldOffset(8)]
		public EResult m_eResult;

		// Token: 0x0400072B RID: 1835
		[FieldOffset(12)]
		public CSteamID m_steamIDUser;
	}
}
