using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002AC RID: 684
	[CallbackIdentity(103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersDisconnected_t
	{
		// Token: 0x0400070C RID: 1804
		public const int k_iCallback = 103;

		// Token: 0x0400070D RID: 1805
		public EResult m_eResult;
	}
}
