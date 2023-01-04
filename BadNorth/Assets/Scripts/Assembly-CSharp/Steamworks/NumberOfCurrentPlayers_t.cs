using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BC RID: 700
	[CallbackIdentity(1107)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NumberOfCurrentPlayers_t
	{
		// Token: 0x04000743 RID: 1859
		public const int k_iCallback = 1107;

		// Token: 0x04000744 RID: 1860
		public byte m_bSuccess;

		// Token: 0x04000745 RID: 1861
		public int m_cPlayers;
	}
}
