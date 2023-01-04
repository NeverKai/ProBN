using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000223 RID: 547
	[CallbackIdentity(331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameOverlayActivated_t
	{
		// Token: 0x04000504 RID: 1284
		public const int k_iCallback = 331;

		// Token: 0x04000505 RID: 1285
		public byte m_bActive;
	}
}
