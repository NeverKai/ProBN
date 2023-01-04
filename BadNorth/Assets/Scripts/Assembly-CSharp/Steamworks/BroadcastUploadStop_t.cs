using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C9 RID: 713
	[CallbackIdentity(4605)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct BroadcastUploadStop_t
	{
		// Token: 0x04000764 RID: 1892
		public const int k_iCallback = 4605;

		// Token: 0x04000765 RID: 1893
		public EBroadcastUploadResult m_eResult;
	}
}
