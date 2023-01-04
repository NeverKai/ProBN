using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000234 RID: 564
	[CallbackIdentity(1701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GCMessageAvailable_t
	{
		// Token: 0x04000540 RID: 1344
		public const int k_iCallback = 1701;

		// Token: 0x04000541 RID: 1345
		public uint m_nMessageSize;
	}
}
