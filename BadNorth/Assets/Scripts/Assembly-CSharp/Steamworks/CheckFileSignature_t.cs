using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C6 RID: 710
	[CallbackIdentity(705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature_t
	{
		// Token: 0x0400075E RID: 1886
		public const int k_iCallback = 705;

		// Token: 0x0400075F RID: 1887
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
