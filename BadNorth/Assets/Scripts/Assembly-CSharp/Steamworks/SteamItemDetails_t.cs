using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200031E RID: 798
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamItemDetails_t
	{
		// Token: 0x04000BF6 RID: 3062
		public SteamItemInstanceID_t m_itemId;

		// Token: 0x04000BF7 RID: 3063
		public SteamItemDef_t m_iDefinition;

		// Token: 0x04000BF8 RID: 3064
		public ushort m_unQuantity;

		// Token: 0x04000BF9 RID: 3065
		public ushort m_unFlags;
	}
}
