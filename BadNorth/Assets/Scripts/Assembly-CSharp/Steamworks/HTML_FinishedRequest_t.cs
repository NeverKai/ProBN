using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000248 RID: 584
	[CallbackIdentity(4506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FinishedRequest_t
	{
		// Token: 0x04000593 RID: 1427
		public const int k_iCallback = 4506;

		// Token: 0x04000594 RID: 1428
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000595 RID: 1429
		public string pchURL;

		// Token: 0x04000596 RID: 1430
		public string pchPageTitle;
	}
}
