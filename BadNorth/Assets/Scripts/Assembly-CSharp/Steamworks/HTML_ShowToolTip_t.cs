using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000256 RID: 598
	[CallbackIdentity(4524)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ShowToolTip_t
	{
		// Token: 0x040005D2 RID: 1490
		public const int k_iCallback = 4524;

		// Token: 0x040005D3 RID: 1491
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005D4 RID: 1492
		public string pchMsg;
	}
}
