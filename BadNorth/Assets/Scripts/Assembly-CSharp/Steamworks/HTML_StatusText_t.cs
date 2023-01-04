using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000255 RID: 597
	[CallbackIdentity(4523)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StatusText_t
	{
		// Token: 0x040005CF RID: 1487
		public const int k_iCallback = 4523;

		// Token: 0x040005D0 RID: 1488
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005D1 RID: 1489
		public string pchMsg;
	}
}
