using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027F RID: 639
	[CallbackIdentity(1302)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncedServer_t
	{
		// Token: 0x04000647 RID: 1607
		public const int k_iCallback = 1302;

		// Token: 0x04000648 RID: 1608
		public AppId_t m_nAppID;

		// Token: 0x04000649 RID: 1609
		public EResult m_eResult;

		// Token: 0x0400064A RID: 1610
		public int m_unNumUploads;
	}
}
