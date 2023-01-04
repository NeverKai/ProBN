using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024B RID: 587
	[CallbackIdentity(4509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SearchResults_t
	{
		// Token: 0x0400059D RID: 1437
		public const int k_iCallback = 4509;

		// Token: 0x0400059E RID: 1438
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400059F RID: 1439
		public uint unResults;

		// Token: 0x040005A0 RID: 1440
		public uint unCurrentMatch;
	}
}
