using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002AD RID: 685
	[CallbackIdentity(113)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClientGameServerDeny_t
	{
		// Token: 0x0400070E RID: 1806
		public const int k_iCallback = 113;

		// Token: 0x0400070F RID: 1807
		public uint m_uAppID;

		// Token: 0x04000710 RID: 1808
		public uint m_unGameServerIP;

		// Token: 0x04000711 RID: 1809
		public ushort m_usGameServerPort;

		// Token: 0x04000712 RID: 1810
		public ushort m_bSecure;

		// Token: 0x04000713 RID: 1811
		public uint m_uReason;
	}
}
