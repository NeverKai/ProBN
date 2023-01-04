using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027E RID: 638
	[CallbackIdentity(1301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncedClient_t
	{
		// Token: 0x04000643 RID: 1603
		public const int k_iCallback = 1301;

		// Token: 0x04000644 RID: 1604
		public AppId_t m_nAppID;

		// Token: 0x04000645 RID: 1605
		public EResult m_eResult;

		// Token: 0x04000646 RID: 1606
		public int m_unNumDownloads;
	}
}
