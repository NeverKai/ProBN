using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200025F RID: 607
	[CallbackIdentity(4703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x040005EE RID: 1518
		public const int k_iCallback = 4703;

		// Token: 0x040005EF RID: 1519
		public EResult m_result;

		// Token: 0x040005F0 RID: 1520
		public CSteamID m_steamID;

		// Token: 0x040005F1 RID: 1521
		public int m_numEligiblePromoItemDefs;

		// Token: 0x040005F2 RID: 1522
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
