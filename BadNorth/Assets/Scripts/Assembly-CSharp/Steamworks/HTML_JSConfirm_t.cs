using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000251 RID: 593
	[CallbackIdentity(4515)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSConfirm_t
	{
		// Token: 0x040005BD RID: 1469
		public const int k_iCallback = 4515;

		// Token: 0x040005BE RID: 1470
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005BF RID: 1471
		public string pchMessage;
	}
}
