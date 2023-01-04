using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000258 RID: 600
	[CallbackIdentity(4526)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HideToolTip_t
	{
		// Token: 0x040005D8 RID: 1496
		public const int k_iCallback = 4526;

		// Token: 0x040005D9 RID: 1497
		public HHTMLBrowser unBrowserHandle;
	}
}
