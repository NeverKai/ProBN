using System;
using System.Collections.Generic;

namespace Steamworks
{
	// Token: 0x02000212 RID: 530
	public static class SteamRemoteStorage
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00024594 File Offset: 0x00022994
		public static bool FileWrite(string pchFile, byte[] pvData, int cubData)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileWrite(utf8StringHandle, pvData, cubData);
			}
			return result;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000245DC File Offset: 0x000229DC
		public static int FileRead(string pchFile, byte[] pvData, int cubDataToRead)
		{
			InteropHelp.TestIfAvailableClient();
			int result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileRead(utf8StringHandle, pvData, cubDataToRead);
			}
			return result;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00024624 File Offset: 0x00022A24
		public static SteamAPICall_t FileWriteAsync(string pchFile, byte[] pvData, uint cubData)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileWriteAsync(utf8StringHandle, pvData, cubData);
			}
			return result;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00024670 File Offset: 0x00022A70
		public static SteamAPICall_t FileReadAsync(string pchFile, uint nOffset, uint cubToRead)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileReadAsync(utf8StringHandle, nOffset, cubToRead);
			}
			return result;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000246BC File Offset: 0x00022ABC
		public static bool FileReadAsyncComplete(SteamAPICall_t hReadCall, byte[] pvBuffer, uint cubToRead)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileReadAsyncComplete(hReadCall, pvBuffer, cubToRead);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000246CC File Offset: 0x00022ACC
		public static bool FileForget(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileForget(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00024710 File Offset: 0x00022B10
		public static bool FileDelete(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileDelete(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00024754 File Offset: 0x00022B54
		public static SteamAPICall_t FileShare(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileShare(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002479C File Offset: 0x00022B9C
		public static bool SetSyncPlatforms(string pchFile, ERemoteStoragePlatform eRemoteStoragePlatform)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_SetSyncPlatforms(utf8StringHandle, eRemoteStoragePlatform);
			}
			return result;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000247E0 File Offset: 0x00022BE0
		public static UGCFileWriteStreamHandle_t FileWriteStreamOpen(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			UGCFileWriteStreamHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (UGCFileWriteStreamHandle_t)NativeMethods.ISteamRemoteStorage_FileWriteStreamOpen(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00024828 File Offset: 0x00022C28
		public static bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, byte[] pvData, int cubData)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamWriteChunk(writeHandle, pvData, cubData);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00024837 File Offset: 0x00022C37
		public static bool FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamClose(writeHandle);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00024844 File Offset: 0x00022C44
		public static bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamCancel(writeHandle);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00024854 File Offset: 0x00022C54
		public static bool FileExists(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileExists(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00024898 File Offset: 0x00022C98
		public static bool FilePersisted(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FilePersisted(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000248DC File Offset: 0x00022CDC
		public static int GetFileSize(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			int result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetFileSize(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00024920 File Offset: 0x00022D20
		public static long GetFileTimestamp(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			long result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetFileTimestamp(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00024964 File Offset: 0x00022D64
		public static ERemoteStoragePlatform GetSyncPlatforms(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			ERemoteStoragePlatform result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetSyncPlatforms(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000249A8 File Offset: 0x00022DA8
		public static int GetFileCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetFileCount();
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000249B4 File Offset: 0x00022DB4
		public static string GetFileNameAndSize(int iFile, out int pnFileSizeInBytes)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemoteStorage_GetFileNameAndSize(iFile, out pnFileSizeInBytes));
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000249C7 File Offset: 0x00022DC7
		public static bool GetQuota(out ulong pnTotalBytes, out ulong puAvailableBytes)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetQuota(out pnTotalBytes, out puAvailableBytes);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000249D5 File Offset: 0x00022DD5
		public static bool IsCloudEnabledForAccount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_IsCloudEnabledForAccount();
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000249E1 File Offset: 0x00022DE1
		public static bool IsCloudEnabledForApp()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_IsCloudEnabledForApp();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000249ED File Offset: 0x00022DED
		public static void SetCloudEnabledForApp(bool bEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamRemoteStorage_SetCloudEnabledForApp(bEnabled);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000249FA File Offset: 0x00022DFA
		public static SteamAPICall_t UGCDownload(UGCHandle_t hContent, uint unPriority)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UGCDownload(hContent, unPriority);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00024A0D File Offset: 0x00022E0D
		public static bool GetUGCDownloadProgress(UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetUGCDownloadProgress(hContent, out pnBytesDownloaded, out pnBytesExpected);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00024A1C File Offset: 0x00022E1C
		public static bool GetUGCDetails(UGCHandle_t hContent, out AppId_t pnAppID, out string ppchName, out int pnFileSizeInBytes, out CSteamID pSteamIDOwner)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr nativeUtf;
			bool flag = NativeMethods.ISteamRemoteStorage_GetUGCDetails(hContent, out pnAppID, out nativeUtf, out pnFileSizeInBytes, out pSteamIDOwner);
			ppchName = ((!flag) ? null : InteropHelp.PtrToStringUTF8(nativeUtf));
			return flag;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00024A50 File Offset: 0x00022E50
		public static int UGCRead(UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, EUGCReadAction eAction)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UGCRead(hContent, pvData, cubDataToRead, cOffset, eAction);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00024A62 File Offset: 0x00022E62
		public static int GetCachedUGCCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetCachedUGCCount();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00024A6E File Offset: 0x00022E6E
		public static UGCHandle_t GetCachedUGCHandle(int iCachedContent)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCHandle_t)NativeMethods.ISteamRemoteStorage_GetCachedUGCHandle(iCachedContent);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00024A80 File Offset: 0x00022E80
		public static SteamAPICall_t PublishWorkshopFile(string pchFile, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IList<string> pTags, EWorkshopFileType eWorkshopFileType)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchPreviewFile))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchTitle))
					{
						using (InteropHelp.UTF8StringHandle utf8StringHandle4 = new InteropHelp.UTF8StringHandle(pchDescription))
						{
							result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_PublishWorkshopFile(utf8StringHandle, utf8StringHandle2, nConsumerAppId, utf8StringHandle3, utf8StringHandle4, eVisibility, new InteropHelp.SteamParamStringArray(pTags), eWorkshopFileType);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00024B40 File Offset: 0x00022F40
		public static PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (PublishedFileUpdateHandle_t)NativeMethods.ISteamRemoteStorage_CreatePublishedFileUpdateRequest(unPublishedFileId);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00024B54 File Offset: 0x00022F54
		public static bool UpdatePublishedFileFile(PublishedFileUpdateHandle_t updateHandle, string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileFile(updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00024B98 File Offset: 0x00022F98
		public static bool UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPreviewFile))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFilePreviewFile(updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00024BDC File Offset: 0x00022FDC
		public static bool UpdatePublishedFileTitle(PublishedFileUpdateHandle_t updateHandle, string pchTitle)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchTitle))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTitle(updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00024C20 File Offset: 0x00023020
		public static bool UpdatePublishedFileDescription(PublishedFileUpdateHandle_t updateHandle, string pchDescription)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileDescription(updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00024C64 File Offset: 0x00023064
		public static bool UpdatePublishedFileVisibility(PublishedFileUpdateHandle_t updateHandle, ERemoteStoragePublishedFileVisibility eVisibility)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UpdatePublishedFileVisibility(updateHandle, eVisibility);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00024C72 File Offset: 0x00023072
		public static bool UpdatePublishedFileTags(PublishedFileUpdateHandle_t updateHandle, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTags(updateHandle, new InteropHelp.SteamParamStringArray(pTags));
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00024C8A File Offset: 0x0002308A
		public static SteamAPICall_t CommitPublishedFileUpdate(PublishedFileUpdateHandle_t updateHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_CommitPublishedFileUpdate(updateHandle);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00024C9C File Offset: 0x0002309C
		public static SteamAPICall_t GetPublishedFileDetails(PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetPublishedFileDetails(unPublishedFileId, unMaxSecondsOld);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00024CAF File Offset: 0x000230AF
		public static SteamAPICall_t DeletePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_DeletePublishedFile(unPublishedFileId);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00024CC1 File Offset: 0x000230C1
		public static SteamAPICall_t EnumerateUserPublishedFiles(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserPublishedFiles(unStartIndex);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00024CD3 File Offset: 0x000230D3
		public static SteamAPICall_t SubscribePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_SubscribePublishedFile(unPublishedFileId);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00024CE5 File Offset: 0x000230E5
		public static SteamAPICall_t EnumerateUserSubscribedFiles(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserSubscribedFiles(unStartIndex);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00024CF7 File Offset: 0x000230F7
		public static SteamAPICall_t UnsubscribePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UnsubscribePublishedFile(unPublishedFileId);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00024D0C File Offset: 0x0002310C
		public static bool UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchChangeDescription))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00024D50 File Offset: 0x00023150
		public static SteamAPICall_t GetPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetPublishedItemVoteDetails(unPublishedFileId);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00024D62 File Offset: 0x00023162
		public static SteamAPICall_t UpdateUserPublishedItemVote(PublishedFileId_t unPublishedFileId, bool bVoteUp)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UpdateUserPublishedItemVote(unPublishedFileId, bVoteUp);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00024D75 File Offset: 0x00023175
		public static SteamAPICall_t GetUserPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetUserPublishedItemVoteDetails(unPublishedFileId);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00024D87 File Offset: 0x00023187
		public static SteamAPICall_t EnumerateUserSharedWorkshopFiles(CSteamID steamId, uint unStartIndex, IList<string> pRequiredTags, IList<string> pExcludedTags)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(steamId, unStartIndex, new InteropHelp.SteamParamStringArray(pRequiredTags), new InteropHelp.SteamParamStringArray(pExcludedTags));
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00024DB0 File Offset: 0x000231B0
		public static SteamAPICall_t PublishVideo(EWorkshopVideoProvider eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVideoAccount))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchVideoIdentifier))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchPreviewFile))
					{
						using (InteropHelp.UTF8StringHandle utf8StringHandle4 = new InteropHelp.UTF8StringHandle(pchTitle))
						{
							using (InteropHelp.UTF8StringHandle utf8StringHandle5 = new InteropHelp.UTF8StringHandle(pchDescription))
							{
								result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_PublishVideo(eVideoProvider, utf8StringHandle, utf8StringHandle2, utf8StringHandle3, nConsumerAppId, utf8StringHandle4, utf8StringHandle5, eVisibility, new InteropHelp.SteamParamStringArray(pTags));
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00024E98 File Offset: 0x00023298
		public static SteamAPICall_t SetUserPublishedFileAction(PublishedFileId_t unPublishedFileId, EWorkshopFileAction eAction)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_SetUserPublishedFileAction(unPublishedFileId, eAction);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00024EAB File Offset: 0x000232AB
		public static SteamAPICall_t EnumeratePublishedFilesByUserAction(EWorkshopFileAction eAction, uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(eAction, unStartIndex);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00024EBE File Offset: 0x000232BE
		public static SteamAPICall_t EnumeratePublishedWorkshopFiles(EWorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, IList<string> pTags, IList<string> pUserTags)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(eEnumerationType, unStartIndex, unCount, unDays, new InteropHelp.SteamParamStringArray(pTags), new InteropHelp.SteamParamStringArray(pUserTags));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00024EEC File Offset: 0x000232EC
		public static SteamAPICall_t UGCDownloadToLocation(UGCHandle_t hContent, string pchLocation, uint unPriority)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLocation))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UGCDownloadToLocation(hContent, utf8StringHandle, unPriority);
			}
			return result;
		}
	}
}
