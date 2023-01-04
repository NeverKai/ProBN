using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200025D RID: 605
	[CallbackIdentity(4701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryFullUpdate_t
	{
		// Token: 0x040005EB RID: 1515
		public const int k_iCallback = 4701;

		// Token: 0x040005EC RID: 1516
		public SteamInventoryResult_t m_handle;
	}
}
