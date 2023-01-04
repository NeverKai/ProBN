﻿using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000290 RID: 656
	[CallbackIdentity(1323)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		// Token: 0x040006A6 RID: 1702
		public const int k_iCallback = 1323;

		// Token: 0x040006A7 RID: 1703
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006A8 RID: 1704
		public AppId_t m_nAppID;
	}
}
