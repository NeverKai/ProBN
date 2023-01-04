using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000281 RID: 641
	[CallbackIdentity(1305)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncStatusCheck_t
	{
		// Token: 0x04000651 RID: 1617
		public const int k_iCallback = 1305;

		// Token: 0x04000652 RID: 1618
		public AppId_t m_nAppID;

		// Token: 0x04000653 RID: 1619
		public EResult m_eResult;
	}
}
