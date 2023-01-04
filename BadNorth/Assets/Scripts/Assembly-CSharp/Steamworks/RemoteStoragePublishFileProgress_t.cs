using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000296 RID: 662
	[CallbackIdentity(1329)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileProgress_t
	{
		// Token: 0x040006C0 RID: 1728
		public const int k_iCallback = 1329;

		// Token: 0x040006C1 RID: 1729
		public double m_dPercentFile;

		// Token: 0x040006C2 RID: 1730
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPreview;
	}
}
