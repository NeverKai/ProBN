using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000243 RID: 579
	[CallbackIdentity(4501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x04000575 RID: 1397
		public const int k_iCallback = 4501;

		// Token: 0x04000576 RID: 1398
		public HHTMLBrowser unBrowserHandle;
	}
}
