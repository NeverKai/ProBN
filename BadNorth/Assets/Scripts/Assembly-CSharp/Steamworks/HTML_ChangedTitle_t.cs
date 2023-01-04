using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024A RID: 586
	[CallbackIdentity(4508)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ChangedTitle_t
	{
		// Token: 0x0400059A RID: 1434
		public const int k_iCallback = 4508;

		// Token: 0x0400059B RID: 1435
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400059C RID: 1436
		public string pchTitle;
	}
}
