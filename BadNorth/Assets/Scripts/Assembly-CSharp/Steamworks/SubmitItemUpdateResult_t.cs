using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200029F RID: 671
	[CallbackIdentity(3404)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitItemUpdateResult_t
	{
		// Token: 0x040006DF RID: 1759
		public const int k_iCallback = 3404;

		// Token: 0x040006E0 RID: 1760
		public EResult m_eResult;

		// Token: 0x040006E1 RID: 1761
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
