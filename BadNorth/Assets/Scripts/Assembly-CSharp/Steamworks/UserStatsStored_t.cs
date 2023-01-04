using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B7 RID: 695
	[CallbackIdentity(1102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsStored_t
	{
		// Token: 0x0400072C RID: 1836
		public const int k_iCallback = 1102;

		// Token: 0x0400072D RID: 1837
		public ulong m_nGameID;

		// Token: 0x0400072E RID: 1838
		public EResult m_eResult;
	}
}
