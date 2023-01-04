using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000245 RID: 581
	[CallbackIdentity(4503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StartRequest_t
	{
		// Token: 0x04000584 RID: 1412
		public const int k_iCallback = 4503;

		// Token: 0x04000585 RID: 1413
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000586 RID: 1414
		public string pchURL;

		// Token: 0x04000587 RID: 1415
		public string pchTarget;

		// Token: 0x04000588 RID: 1416
		public string pchPostData;

		// Token: 0x04000589 RID: 1417
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;
	}
}
