using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002AE RID: 686
	[CallbackIdentity(117)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCFailure_t
	{
		// Token: 0x04000714 RID: 1812
		public const int k_iCallback = 117;

		// Token: 0x04000715 RID: 1813
		public byte m_eFailureType;
	}
}
