using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000233 RID: 563
	[CallbackIdentity(347)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetPersonaNameResponse_t
	{
		// Token: 0x0400053C RID: 1340
		public const int k_iCallback = 347;

		// Token: 0x0400053D RID: 1341
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;

		// Token: 0x0400053E RID: 1342
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocalSuccess;

		// Token: 0x0400053F RID: 1343
		public EResult m_result;
	}
}
