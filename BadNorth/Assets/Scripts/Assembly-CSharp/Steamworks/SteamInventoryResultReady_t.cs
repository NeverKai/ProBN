using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200025C RID: 604
	[CallbackIdentity(4700)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryResultReady_t
	{
		// Token: 0x040005E8 RID: 1512
		public const int k_iCallback = 4700;

		// Token: 0x040005E9 RID: 1513
		public SteamInventoryResult_t m_handle;

		// Token: 0x040005EA RID: 1514
		public EResult m_result;
	}
}
