using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000238 RID: 568
	[CallbackIdentity(203)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientKick_t
	{
		// Token: 0x0400054A RID: 1354
		public const int k_iCallback = 203;

		// Token: 0x0400054B RID: 1355
		public CSteamID m_SteamID;

		// Token: 0x0400054C RID: 1356
		public EDenyReason m_eDenyReason;
	}
}
