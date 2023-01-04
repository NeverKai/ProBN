using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000249 RID: 585
	[CallbackIdentity(4507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x04000597 RID: 1431
		public const int k_iCallback = 4507;

		// Token: 0x04000598 RID: 1432
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000599 RID: 1433
		public string pchURL;
	}
}
