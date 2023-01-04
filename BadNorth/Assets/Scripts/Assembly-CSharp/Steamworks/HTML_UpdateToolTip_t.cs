using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000257 RID: 599
	[CallbackIdentity(4525)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_UpdateToolTip_t
	{
		// Token: 0x040005D5 RID: 1493
		public const int k_iCallback = 4525;

		// Token: 0x040005D6 RID: 1494
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005D7 RID: 1495
		public string pchMsg;
	}
}
