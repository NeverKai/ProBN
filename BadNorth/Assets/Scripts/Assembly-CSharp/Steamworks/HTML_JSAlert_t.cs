using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000250 RID: 592
	[CallbackIdentity(4514)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSAlert_t
	{
		// Token: 0x040005BA RID: 1466
		public const int k_iCallback = 4514;

		// Token: 0x040005BB RID: 1467
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005BC RID: 1468
		public string pchMessage;
	}
}
