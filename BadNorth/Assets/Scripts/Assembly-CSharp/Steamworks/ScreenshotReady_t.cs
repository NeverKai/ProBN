using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200029A RID: 666
	[CallbackIdentity(2301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotReady_t
	{
		// Token: 0x040006CE RID: 1742
		public const int k_iCallback = 2301;

		// Token: 0x040006CF RID: 1743
		public ScreenshotHandle m_hLocal;

		// Token: 0x040006D0 RID: 1744
		public EResult m_eResult;
	}
}
