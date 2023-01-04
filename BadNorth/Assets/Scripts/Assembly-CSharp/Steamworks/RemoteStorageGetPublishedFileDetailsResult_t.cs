using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028B RID: 651
	[CallbackIdentity(1318)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		// Token: 0x0400067B RID: 1659
		public const int k_iCallback = 1318;

		// Token: 0x0400067C RID: 1660
		public EResult m_eResult;

		// Token: 0x0400067D RID: 1661
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400067E RID: 1662
		public AppId_t m_nCreatorAppID;

		// Token: 0x0400067F RID: 1663
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000680 RID: 1664
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string m_rgchTitle;

		// Token: 0x04000681 RID: 1665
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string m_rgchDescription;

		// Token: 0x04000682 RID: 1666
		public UGCHandle_t m_hFile;

		// Token: 0x04000683 RID: 1667
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000684 RID: 1668
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000685 RID: 1669
		public uint m_rtimeCreated;

		// Token: 0x04000686 RID: 1670
		public uint m_rtimeUpdated;

		// Token: 0x04000687 RID: 1671
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000688 RID: 1672
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x04000689 RID: 1673
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string m_rgchTags;

		// Token: 0x0400068A RID: 1674
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x0400068B RID: 1675
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName;

		// Token: 0x0400068C RID: 1676
		public int m_nFileSize;

		// Token: 0x0400068D RID: 1677
		public int m_nPreviewFileSize;

		// Token: 0x0400068E RID: 1678
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL;

		// Token: 0x0400068F RID: 1679
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000690 RID: 1680
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;
	}
}
