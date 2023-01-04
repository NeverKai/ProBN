using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000298 RID: 664
	[CallbackIdentity(1331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		// Token: 0x040006C7 RID: 1735
		public const int k_iCallback = 1331;

		// Token: 0x040006C8 RID: 1736
		public EResult m_eResult;
	}
}
