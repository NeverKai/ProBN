using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Steamworks
{
	// Token: 0x0200021A RID: 538
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06000E86 RID: 3718
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Init")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamAPI_Init();

		// Token: 0x06000E87 RID: 3719
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Shutdown")]
		public static extern void SteamAPI_Shutdown();

		// Token: 0x06000E88 RID: 3720
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "RestartAppIfNecessary")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamAPI_RestartAppIfNecessary(AppId_t unOwnAppID);

		// Token: 0x06000E89 RID: 3721
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ReleaseCurrentThreadMemory")]
		public static extern void SteamAPI_ReleaseCurrentThreadMemory();

		// Token: 0x06000E8A RID: 3722
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WriteMiniDump")]
		public static extern void SteamAPI_WriteMiniDump(uint uStructuredExceptionCode, IntPtr pvExceptionInfo, uint uBuildID);

		// Token: 0x06000E8B RID: 3723
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetMiniDumpComment")]
		public static extern void SteamAPI_SetMiniDumpComment(InteropHelp.UTF8StringHandle pchMsg);

		// Token: 0x06000E8C RID: 3724
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "RunCallbacks")]
		public static extern void SteamAPI_RunCallbacks();

		// Token: 0x06000E8D RID: 3725
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "RegisterCallback")]
		public static extern void SteamAPI_RegisterCallback(IntPtr pCallback, int iCallback);

		// Token: 0x06000E8E RID: 3726
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UnregisterCallback")]
		public static extern void SteamAPI_UnregisterCallback(IntPtr pCallback);

		// Token: 0x06000E8F RID: 3727
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "RegisterCallResult")]
		public static extern void SteamAPI_RegisterCallResult(IntPtr pCallback, ulong hAPICall);

		// Token: 0x06000E90 RID: 3728
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UnregisterCallResult")]
		public static extern void SteamAPI_UnregisterCallResult(IntPtr pCallback, ulong hAPICall);

		// Token: 0x06000E91 RID: 3729
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IsSteamRunning")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamAPI_IsSteamRunning();

		// Token: 0x06000E92 RID: 3730
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Steam_RunCallbacks_")]
		public static extern void Steam_RunCallbacks(HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.I1)] bool bGameServerCallbacks);

		// Token: 0x06000E93 RID: 3731
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Steam_RegisterInterfaceFuncs_")]
		public static extern void Steam_RegisterInterfaceFuncs(IntPtr hModule);

		// Token: 0x06000E94 RID: 3732
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Steam_GetHSteamUserCurrent_")]
		public static extern int Steam_GetHSteamUserCurrent();

		// Token: 0x06000E95 RID: 3733
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetSteamInstallPath")]
		public static extern int SteamAPI_GetSteamInstallPath();

		// Token: 0x06000E96 RID: 3734
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetHSteamPipe_")]
		public static extern int SteamAPI_GetHSteamPipe();

		// Token: 0x06000E97 RID: 3735
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetTryCatchCallbacks")]
		public static extern void SteamAPI_SetTryCatchCallbacks([MarshalAs(UnmanagedType.I1)] bool bTryCatchCallbacks);

		// Token: 0x06000E98 RID: 3736
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetHSteamUser_")]
		public static extern int SteamAPI_GetHSteamUser();

		// Token: 0x06000E99 RID: 3737
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamInternal_CreateInterface_")]
		public static extern void SteamInternal_CreateInterface(IntPtr ver);

		// Token: 0x06000E9A RID: 3738
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UseBreakpadCrashHandler")]
		public static extern void SteamAPI_UseBreakpadCrashHandler(InteropHelp.UTF8StringHandle pchVersion, InteropHelp.UTF8StringHandle pchDate, InteropHelp.UTF8StringHandle pchTime, [MarshalAs(UnmanagedType.I1)] bool bFullMemoryDumps, IntPtr pvContext, IntPtr m_pfnPreMinidumpCallback);

		// Token: 0x06000E9B RID: 3739
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetBreakpadAppID")]
		public static extern void SteamAPI_SetBreakpadAppID(uint unAppID);

		// Token: 0x06000E9C RID: 3740
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_Init")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamGameServer_Init(uint unIP, ushort usSteamPort, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, InteropHelp.UTF8StringHandle pchVersionString);

		// Token: 0x06000E9D RID: 3741
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_Shutdown")]
		public static extern void SteamGameServer_Shutdown();

		// Token: 0x06000E9E RID: 3742
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_RunCallbacks")]
		public static extern void SteamGameServer_RunCallbacks();

		// Token: 0x06000E9F RID: 3743
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_ReleaseCurrentThreadMemory")]
		public static extern void SteamGameServer_ReleaseCurrentThreadMemory();

		// Token: 0x06000EA0 RID: 3744
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_BSecure")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamGameServer_BSecure();

		// Token: 0x06000EA1 RID: 3745
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_GetSteamID")]
		public static extern ulong SteamGameServer_GetSteamID();

		// Token: 0x06000EA2 RID: 3746
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_GetHSteamPipe")]
		public static extern int SteamGameServer_GetHSteamPipe();

		// Token: 0x06000EA3 RID: 3747
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GameServer_GetHSteamUser")]
		public static extern int SteamGameServer_GetHSteamUser();

		// Token: 0x06000EA4 RID: 3748
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamInternal_GameServer_Init_")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamInternal_GameServer_Init(uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, InteropHelp.UTF8StringHandle pchVersionString);

		// Token: 0x06000EA5 RID: 3749
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamClient_")]
		public static extern IntPtr SteamClient();

		// Token: 0x06000EA6 RID: 3750
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamGameServerClient_")]
		public static extern IntPtr SteamGameServerClient();

		// Token: 0x06000EA7 RID: 3751
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BDecryptTicket(byte[] rgubTicketEncrypted, uint cubTicketEncrypted, byte[] rgubTicketDecrypted, ref uint pcubTicketDecrypted, [MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] byte[] rgubKey, int cubKey);

		// Token: 0x06000EA8 RID: 3752
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BIsTicketForApp(byte[] rgubTicketDecrypted, uint cubTicketDecrypted, AppId_t nAppID);

		// Token: 0x06000EA9 RID: 3753
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketIssueTime(byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		// Token: 0x06000EAA RID: 3754
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SteamEncryptedAppTicket_GetTicketSteamID(byte[] rgubTicketDecrypted, uint cubTicketDecrypted, out CSteamID psteamID);

		// Token: 0x06000EAB RID: 3755
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketAppID(byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		// Token: 0x06000EAC RID: 3756
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BUserOwnsAppInTicket(byte[] rgubTicketDecrypted, uint cubTicketDecrypted, AppId_t nAppID);

		// Token: 0x06000EAD RID: 3757
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BUserIsVacBanned(byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		// Token: 0x06000EAE RID: 3758
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SteamEncryptedAppTicket_GetUserVariableData(byte[] rgubTicketDecrypted, uint cubTicketDecrypted, out uint pcubUserData);

		// Token: 0x06000EAF RID: 3759
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamAppList_GetNumInstalledApps();

		// Token: 0x06000EB0 RID: 3760
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamAppList_GetInstalledApps([In] [Out] AppId_t[] pvecAppID, uint unMaxAppIDs);

		// Token: 0x06000EB1 RID: 3761
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamAppList_GetAppName(AppId_t nAppID, IntPtr pchName, int cchNameMax);

		// Token: 0x06000EB2 RID: 3762
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamAppList_GetAppInstallDir(AppId_t nAppID, IntPtr pchDirectory, int cchNameMax);

		// Token: 0x06000EB3 RID: 3763
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamAppList_GetAppBuildId(AppId_t nAppID);

		// Token: 0x06000EB4 RID: 3764
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribed();

		// Token: 0x06000EB5 RID: 3765
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsLowViolence();

		// Token: 0x06000EB6 RID: 3766
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsCybercafe();

		// Token: 0x06000EB7 RID: 3767
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsVACBanned();

		// Token: 0x06000EB8 RID: 3768
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamApps_GetCurrentGameLanguage();

		// Token: 0x06000EB9 RID: 3769
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamApps_GetAvailableGameLanguages();

		// Token: 0x06000EBA RID: 3770
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribedApp(AppId_t appID);

		// Token: 0x06000EBB RID: 3771
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsDlcInstalled(AppId_t appID);

		// Token: 0x06000EBC RID: 3772
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamApps_GetEarliestPurchaseUnixTime(AppId_t nAppID);

		// Token: 0x06000EBD RID: 3773
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribedFromFreeWeekend();

		// Token: 0x06000EBE RID: 3774
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamApps_GetDLCCount();

		// Token: 0x06000EBF RID: 3775
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BGetDLCDataByIndex(int iDLC, out AppId_t pAppID, out bool pbAvailable, IntPtr pchName, int cchNameBufferSize);

		// Token: 0x06000EC0 RID: 3776
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamApps_InstallDLC(AppId_t nAppID);

		// Token: 0x06000EC1 RID: 3777
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamApps_UninstallDLC(AppId_t nAppID);

		// Token: 0x06000EC2 RID: 3778
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamApps_RequestAppProofOfPurchaseKey(AppId_t nAppID);

		// Token: 0x06000EC3 RID: 3779
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_GetCurrentBetaName(IntPtr pchName, int cchNameBufferSize);

		// Token: 0x06000EC4 RID: 3780
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_MarkContentCorrupt([MarshalAs(UnmanagedType.I1)] bool bMissingFilesOnly);

		// Token: 0x06000EC5 RID: 3781
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamApps_GetInstalledDepots(AppId_t appID, [In] [Out] DepotId_t[] pvecDepots, uint cMaxDepots);

		// Token: 0x06000EC6 RID: 3782
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamApps_GetAppInstallDir(AppId_t appID, IntPtr pchFolder, uint cchFolderBufferSize);

		// Token: 0x06000EC7 RID: 3783
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsAppInstalled(AppId_t appID);

		// Token: 0x06000EC8 RID: 3784
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamApps_GetAppOwner();

		// Token: 0x06000EC9 RID: 3785
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamApps_GetLaunchQueryParam(InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06000ECA RID: 3786
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamApps_GetDlcDownloadProgress(AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal);

		// Token: 0x06000ECB RID: 3787
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamApps_GetAppBuildId();

		// Token: 0x06000ECC RID: 3788
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamApps_RequestAllProofOfPurchaseKeys();

		// Token: 0x06000ECD RID: 3789
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamApps_GetFileDetails(InteropHelp.UTF8StringHandle pszFileName);

		// Token: 0x06000ECE RID: 3790
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamClient_CreateSteamPipe();

		// Token: 0x06000ECF RID: 3791
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamClient_BReleaseSteamPipe(HSteamPipe hSteamPipe);

		// Token: 0x06000ED0 RID: 3792
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamClient_ConnectToGlobalUser(HSteamPipe hSteamPipe);

		// Token: 0x06000ED1 RID: 3793
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamClient_CreateLocalUser(out HSteamPipe phSteamPipe, EAccountType eAccountType);

		// Token: 0x06000ED2 RID: 3794
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamClient_ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser);

		// Token: 0x06000ED3 RID: 3795
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000ED4 RID: 3796
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000ED5 RID: 3797
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamClient_SetLocalIPBinding(uint unIP, ushort usPort);

		// Token: 0x06000ED6 RID: 3798
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000ED7 RID: 3799
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamUtils(HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000ED8 RID: 3800
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000ED9 RID: 3801
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDA RID: 3802
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamGenericInterface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDB RID: 3803
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDC RID: 3804
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamGameServerStats(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDD RID: 3805
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDE RID: 3806
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EDF RID: 3807
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamRemoteStorage(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE0 RID: 3808
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamScreenshots(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE1 RID: 3809
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamClient_GetIPCCallCount();

		// Token: 0x06000EE2 RID: 3810
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamClient_SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction);

		// Token: 0x06000EE3 RID: 3811
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamClient_BShutdownIfAllPipesClosed();

		// Token: 0x06000EE4 RID: 3812
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamHTTP(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE5 RID: 3813
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamUnifiedMessages(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE6 RID: 3814
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE7 RID: 3815
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE8 RID: 3816
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamAppList(HSteamUser hSteamUser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EE9 RID: 3817
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamMusic(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EEA RID: 3818
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamMusicRemote(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EEB RID: 3819
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamHTMLSurface(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EEC RID: 3820
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamInventory(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EED RID: 3821
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamClient_GetISteamVideo(HSteamUser hSteamuser, HSteamPipe hSteamPipe, InteropHelp.UTF8StringHandle pchVersion);

		// Token: 0x06000EEE RID: 3822
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamController_Init();

		// Token: 0x06000EEF RID: 3823
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamController_Shutdown();

		// Token: 0x06000EF0 RID: 3824
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_RunFrame();

		// Token: 0x06000EF1 RID: 3825
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamController_GetConnectedControllers([In] [Out] ControllerHandle_t[] handlesOut);

		// Token: 0x06000EF2 RID: 3826
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamController_ShowBindingPanel(ControllerHandle_t controllerHandle);

		// Token: 0x06000EF3 RID: 3827
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamController_GetActionSetHandle(InteropHelp.UTF8StringHandle pszActionSetName);

		// Token: 0x06000EF4 RID: 3828
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_ActivateActionSet(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle);

		// Token: 0x06000EF5 RID: 3829
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamController_GetCurrentActionSet(ControllerHandle_t controllerHandle);

		// Token: 0x06000EF6 RID: 3830
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamController_GetDigitalActionHandle(InteropHelp.UTF8StringHandle pszActionName);

		// Token: 0x06000EF7 RID: 3831
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ControllerDigitalActionData_t ISteamController_GetDigitalActionData(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle);

		// Token: 0x06000EF8 RID: 3832
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamController_GetDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, [In] [Out] EControllerActionOrigin[] originsOut);

		// Token: 0x06000EF9 RID: 3833
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamController_GetAnalogActionHandle(InteropHelp.UTF8StringHandle pszActionName);

		// Token: 0x06000EFA RID: 3834
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ControllerAnalogActionData_t ISteamController_GetAnalogActionData(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle);

		// Token: 0x06000EFB RID: 3835
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamController_GetAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, [In] [Out] EControllerActionOrigin[] originsOut);

		// Token: 0x06000EFC RID: 3836
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_StopAnalogActionMomentum(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction);

		// Token: 0x06000EFD RID: 3837
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_TriggerHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec);

		// Token: 0x06000EFE RID: 3838
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_TriggerRepeatedHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags);

		// Token: 0x06000EFF RID: 3839
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_TriggerVibration(ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed);

		// Token: 0x06000F00 RID: 3840
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamController_SetLEDColor(ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags);

		// Token: 0x06000F01 RID: 3841
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamController_GetGamepadIndexForController(ControllerHandle_t ulControllerHandle);

		// Token: 0x06000F02 RID: 3842
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamController_GetControllerForGamepadIndex(int nIndex);

		// Token: 0x06000F03 RID: 3843
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ControllerMotionData_t ISteamController_GetMotionData(ControllerHandle_t controllerHandle);

		// Token: 0x06000F04 RID: 3844
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamController_ShowDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle, float flScale, float flXPosition, float flYPosition);

		// Token: 0x06000F05 RID: 3845
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamController_ShowAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle, float flScale, float flXPosition, float flYPosition);

		// Token: 0x06000F06 RID: 3846
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamController_GetStringForActionOrigin(EControllerActionOrigin eOrigin);

		// Token: 0x06000F07 RID: 3847
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamController_GetGlyphForActionOrigin(EControllerActionOrigin eOrigin);

		// Token: 0x06000F08 RID: 3848
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetPersonaName();

		// Token: 0x06000F09 RID: 3849
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_SetPersonaName(InteropHelp.UTF8StringHandle pchPersonaName);

		// Token: 0x06000F0A RID: 3850
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EPersonaState ISteamFriends_GetPersonaState();

		// Token: 0x06000F0B RID: 3851
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendCount(EFriendFlags iFriendFlags);

		// Token: 0x06000F0C RID: 3852
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetFriendByIndex(int iFriend, EFriendFlags iFriendFlags);

		// Token: 0x06000F0D RID: 3853
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EFriendRelationship ISteamFriends_GetFriendRelationship(CSteamID steamIDFriend);

		// Token: 0x06000F0E RID: 3854
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EPersonaState ISteamFriends_GetFriendPersonaState(CSteamID steamIDFriend);

		// Token: 0x06000F0F RID: 3855
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetFriendPersonaName(CSteamID steamIDFriend);

		// Token: 0x06000F10 RID: 3856
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo);

		// Token: 0x06000F11 RID: 3857
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName);

		// Token: 0x06000F12 RID: 3858
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendSteamLevel(CSteamID steamIDFriend);

		// Token: 0x06000F13 RID: 3859
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetPlayerNickname(CSteamID steamIDPlayer);

		// Token: 0x06000F14 RID: 3860
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendsGroupCount();

		// Token: 0x06000F15 RID: 3861
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern short ISteamFriends_GetFriendsGroupIDByIndex(int iFG);

		// Token: 0x06000F16 RID: 3862
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetFriendsGroupName(FriendsGroupID_t friendsGroupID);

		// Token: 0x06000F17 RID: 3863
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID);

		// Token: 0x06000F18 RID: 3864
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, [In] [Out] CSteamID[] pOutSteamIDMembers, int nMembersCount);

		// Token: 0x06000F19 RID: 3865
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_HasFriend(CSteamID steamIDFriend, EFriendFlags iFriendFlags);

		// Token: 0x06000F1A RID: 3866
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetClanCount();

		// Token: 0x06000F1B RID: 3867
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetClanByIndex(int iClan);

		// Token: 0x06000F1C RID: 3868
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetClanName(CSteamID steamIDClan);

		// Token: 0x06000F1D RID: 3869
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetClanTag(CSteamID steamIDClan);

		// Token: 0x06000F1E RID: 3870
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting);

		// Token: 0x06000F1F RID: 3871
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_DownloadClanActivityCounts([In] [Out] CSteamID[] psteamIDClans, int cClansToRequest);

		// Token: 0x06000F20 RID: 3872
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendCountFromSource(CSteamID steamIDSource);

		// Token: 0x06000F21 RID: 3873
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend);

		// Token: 0x06000F22 RID: 3874
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource);

		// Token: 0x06000F23 RID: 3875
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_SetInGameVoiceSpeaking(CSteamID steamIDUser, [MarshalAs(UnmanagedType.I1)] bool bSpeaking);

		// Token: 0x06000F24 RID: 3876
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ActivateGameOverlay(InteropHelp.UTF8StringHandle pchDialog);

		// Token: 0x06000F25 RID: 3877
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ActivateGameOverlayToUser(InteropHelp.UTF8StringHandle pchDialog, CSteamID steamID);

		// Token: 0x06000F26 RID: 3878
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ActivateGameOverlayToWebPage(InteropHelp.UTF8StringHandle pchURL);

		// Token: 0x06000F27 RID: 3879
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ActivateGameOverlayToStore(AppId_t nAppID, EOverlayToStoreFlag eFlag);

		// Token: 0x06000F28 RID: 3880
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_SetPlayedWith(CSteamID steamIDUserPlayedWith);

		// Token: 0x06000F29 RID: 3881
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ActivateGameOverlayInviteDialog(CSteamID steamIDLobby);

		// Token: 0x06000F2A RID: 3882
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetSmallFriendAvatar(CSteamID steamIDFriend);

		// Token: 0x06000F2B RID: 3883
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetMediumFriendAvatar(CSteamID steamIDFriend);

		// Token: 0x06000F2C RID: 3884
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetLargeFriendAvatar(CSteamID steamIDFriend);

		// Token: 0x06000F2D RID: 3885
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_RequestUserInformation(CSteamID steamIDUser, [MarshalAs(UnmanagedType.I1)] bool bRequireNameOnly);

		// Token: 0x06000F2E RID: 3886
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_RequestClanOfficerList(CSteamID steamIDClan);

		// Token: 0x06000F2F RID: 3887
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetClanOwner(CSteamID steamIDClan);

		// Token: 0x06000F30 RID: 3888
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetClanOfficerCount(CSteamID steamIDClan);

		// Token: 0x06000F31 RID: 3889
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer);

		// Token: 0x06000F32 RID: 3890
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamFriends_GetUserRestrictions();

		// Token: 0x06000F33 RID: 3891
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_SetRichPresence(InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x06000F34 RID: 3892
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_ClearRichPresence();

		// Token: 0x06000F35 RID: 3893
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetFriendRichPresence(CSteamID steamIDFriend, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06000F36 RID: 3894
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendRichPresenceKeyCount(CSteamID steamIDFriend);

		// Token: 0x06000F37 RID: 3895
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamFriends_GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey);

		// Token: 0x06000F38 RID: 3896
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamFriends_RequestFriendRichPresence(CSteamID steamIDFriend);

		// Token: 0x06000F39 RID: 3897
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_InviteUserToGame(CSteamID steamIDFriend, InteropHelp.UTF8StringHandle pchConnectString);

		// Token: 0x06000F3A RID: 3898
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetCoplayFriendCount();

		// Token: 0x06000F3B RID: 3899
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetCoplayFriend(int iCoplayFriend);

		// Token: 0x06000F3C RID: 3900
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendCoplayTime(CSteamID steamIDFriend);

		// Token: 0x06000F3D RID: 3901
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamFriends_GetFriendCoplayGame(CSteamID steamIDFriend);

		// Token: 0x06000F3E RID: 3902
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_JoinClanChatRoom(CSteamID steamIDClan);

		// Token: 0x06000F3F RID: 3903
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_LeaveClanChatRoom(CSteamID steamIDClan);

		// Token: 0x06000F40 RID: 3904
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetClanChatMemberCount(CSteamID steamIDClan);

		// Token: 0x06000F41 RID: 3905
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetChatMemberByIndex(CSteamID steamIDClan, int iUser);

		// Token: 0x06000F42 RID: 3906
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_SendClanChatMessage(CSteamID steamIDClanChat, InteropHelp.UTF8StringHandle pchText);

		// Token: 0x06000F43 RID: 3907
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter);

		// Token: 0x06000F44 RID: 3908
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser);

		// Token: 0x06000F45 RID: 3909
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat);

		// Token: 0x06000F46 RID: 3910
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_OpenClanChatWindowInSteam(CSteamID steamIDClanChat);

		// Token: 0x06000F47 RID: 3911
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_CloseClanChatWindowInSteam(CSteamID steamIDClanChat);

		// Token: 0x06000F48 RID: 3912
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_SetListenForFriendsMessages([MarshalAs(UnmanagedType.I1)] bool bInterceptEnabled);

		// Token: 0x06000F49 RID: 3913
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamFriends_ReplyToFriendMessage(CSteamID steamIDFriend, InteropHelp.UTF8StringHandle pchMsgToSend);

		// Token: 0x06000F4A RID: 3914
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamFriends_GetFriendMessage(CSteamID steamIDFriend, int iMessageID, IntPtr pvData, int cubData, out EChatEntryType peChatEntryType);

		// Token: 0x06000F4B RID: 3915
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_GetFollowerCount(CSteamID steamID);

		// Token: 0x06000F4C RID: 3916
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_IsFollowing(CSteamID steamID);

		// Token: 0x06000F4D RID: 3917
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamFriends_EnumerateFollowingList(uint unStartIndex);

		// Token: 0x06000F4E RID: 3918
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_InitGameServer(uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId_t nGameAppId, InteropHelp.UTF8StringHandle pchVersionString);

		// Token: 0x06000F4F RID: 3919
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetProduct(InteropHelp.UTF8StringHandle pszProduct);

		// Token: 0x06000F50 RID: 3920
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetGameDescription(InteropHelp.UTF8StringHandle pszGameDescription);

		// Token: 0x06000F51 RID: 3921
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetModDir(InteropHelp.UTF8StringHandle pszModDir);

		// Token: 0x06000F52 RID: 3922
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetDedicatedServer([MarshalAs(UnmanagedType.I1)] bool bDedicated);

		// Token: 0x06000F53 RID: 3923
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_LogOn(InteropHelp.UTF8StringHandle pszToken);

		// Token: 0x06000F54 RID: 3924
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_LogOnAnonymous();

		// Token: 0x06000F55 RID: 3925
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_LogOff();

		// Token: 0x06000F56 RID: 3926
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BLoggedOn();

		// Token: 0x06000F57 RID: 3927
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BSecure();

		// Token: 0x06000F58 RID: 3928
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServer_GetSteamID();

		// Token: 0x06000F59 RID: 3929
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_WasRestartRequested();

		// Token: 0x06000F5A RID: 3930
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetMaxPlayerCount(int cPlayersMax);

		// Token: 0x06000F5B RID: 3931
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetBotPlayerCount(int cBotplayers);

		// Token: 0x06000F5C RID: 3932
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetServerName(InteropHelp.UTF8StringHandle pszServerName);

		// Token: 0x06000F5D RID: 3933
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetMapName(InteropHelp.UTF8StringHandle pszMapName);

		// Token: 0x06000F5E RID: 3934
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetPasswordProtected([MarshalAs(UnmanagedType.I1)] bool bPasswordProtected);

		// Token: 0x06000F5F RID: 3935
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetSpectatorPort(ushort unSpectatorPort);

		// Token: 0x06000F60 RID: 3936
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetSpectatorServerName(InteropHelp.UTF8StringHandle pszSpectatorServerName);

		// Token: 0x06000F61 RID: 3937
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_ClearAllKeyValues();

		// Token: 0x06000F62 RID: 3938
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetKeyValue(InteropHelp.UTF8StringHandle pKey, InteropHelp.UTF8StringHandle pValue);

		// Token: 0x06000F63 RID: 3939
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetGameTags(InteropHelp.UTF8StringHandle pchGameTags);

		// Token: 0x06000F64 RID: 3940
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetGameData(InteropHelp.UTF8StringHandle pchGameData);

		// Token: 0x06000F65 RID: 3941
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetRegion(InteropHelp.UTF8StringHandle pszRegion);

		// Token: 0x06000F66 RID: 3942
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_SendUserConnectAndAuthenticate(uint unIPClient, byte[] pvAuthBlob, uint cubAuthBlobSize, out CSteamID pSteamIDUser);

		// Token: 0x06000F67 RID: 3943
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServer_CreateUnauthenticatedUserConnection();

		// Token: 0x06000F68 RID: 3944
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SendUserDisconnect(CSteamID steamIDUser);

		// Token: 0x06000F69 RID: 3945
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BUpdateUserData(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchPlayerName, uint uScore);

		// Token: 0x06000F6A RID: 3946
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServer_GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket);

		// Token: 0x06000F6B RID: 3947
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EBeginAuthSessionResult ISteamGameServer_BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID);

		// Token: 0x06000F6C RID: 3948
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_EndAuthSession(CSteamID steamID);

		// Token: 0x06000F6D RID: 3949
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_CancelAuthTicket(HAuthTicket hAuthTicket);

		// Token: 0x06000F6E RID: 3950
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EUserHasLicenseForAppResult ISteamGameServer_UserHasLicenseForApp(CSteamID steamID, AppId_t appID);

		// Token: 0x06000F6F RID: 3951
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_RequestUserGroupStatus(CSteamID steamIDUser, CSteamID steamIDGroup);

		// Token: 0x06000F70 RID: 3952
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_GetGameplayStats();

		// Token: 0x06000F71 RID: 3953
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServer_GetServerReputation();

		// Token: 0x06000F72 RID: 3954
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServer_GetPublicIP();

		// Token: 0x06000F73 RID: 3955
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServer_HandleIncomingPacket(byte[] pData, int cbData, uint srcIP, ushort srcPort);

		// Token: 0x06000F74 RID: 3956
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamGameServer_GetNextOutgoingPacket(byte[] pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort);

		// Token: 0x06000F75 RID: 3957
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_EnableHeartbeats([MarshalAs(UnmanagedType.I1)] bool bActive);

		// Token: 0x06000F76 RID: 3958
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_SetHeartbeatInterval(int iHeartbeatInterval);

		// Token: 0x06000F77 RID: 3959
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServer_ForceHeartbeat();

		// Token: 0x06000F78 RID: 3960
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServer_AssociateWithClan(CSteamID steamIDClan);

		// Token: 0x06000F79 RID: 3961
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServer_ComputeNewPlayerCompatibility(CSteamID steamIDNewPlayer);

		// Token: 0x06000F7A RID: 3962
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerStats_RequestUserStats(CSteamID steamIDUser);

		// Token: 0x06000F7B RID: 3963
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserStat(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out int pData);

		// Token: 0x06000F7C RID: 3964
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserStat_(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out float pData);

		// Token: 0x06000F7D RID: 3965
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserAchievement(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		// Token: 0x06000F7E RID: 3966
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserStat(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, int nData);

		// Token: 0x06000F7F RID: 3967
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserStat_(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, float fData);

		// Token: 0x06000F80 RID: 3968
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_UpdateUserAvgRateStat(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, float flCountThisSession, double dSessionLength);

		// Token: 0x06000F81 RID: 3969
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserAchievement(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName);

		// Token: 0x06000F82 RID: 3970
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_ClearUserAchievement(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName);

		// Token: 0x06000F83 RID: 3971
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerStats_StoreUserStats(CSteamID steamIDUser);

		// Token: 0x06000F84 RID: 3972
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTMLSurface_Init();

		// Token: 0x06000F85 RID: 3973
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTMLSurface_Shutdown();

		// Token: 0x06000F86 RID: 3974
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamHTMLSurface_CreateBrowser(InteropHelp.UTF8StringHandle pchUserAgent, InteropHelp.UTF8StringHandle pchUserCSS);

		// Token: 0x06000F87 RID: 3975
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_RemoveBrowser(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F88 RID: 3976
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_LoadURL(HHTMLBrowser unBrowserHandle, InteropHelp.UTF8StringHandle pchURL, InteropHelp.UTF8StringHandle pchPostData);

		// Token: 0x06000F89 RID: 3977
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetSize(HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight);

		// Token: 0x06000F8A RID: 3978
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_StopLoad(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F8B RID: 3979
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_Reload(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F8C RID: 3980
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_GoBack(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F8D RID: 3981
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_GoForward(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F8E RID: 3982
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_AddHeader(HHTMLBrowser unBrowserHandle, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x06000F8F RID: 3983
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_ExecuteJavascript(HHTMLBrowser unBrowserHandle, InteropHelp.UTF8StringHandle pchScript);

		// Token: 0x06000F90 RID: 3984
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_MouseUp(HHTMLBrowser unBrowserHandle, EHTMLMouseButton eMouseButton);

		// Token: 0x06000F91 RID: 3985
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_MouseDown(HHTMLBrowser unBrowserHandle, EHTMLMouseButton eMouseButton);

		// Token: 0x06000F92 RID: 3986
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_MouseDoubleClick(HHTMLBrowser unBrowserHandle, EHTMLMouseButton eMouseButton);

		// Token: 0x06000F93 RID: 3987
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_MouseMove(HHTMLBrowser unBrowserHandle, int x, int y);

		// Token: 0x06000F94 RID: 3988
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_MouseWheel(HHTMLBrowser unBrowserHandle, int nDelta);

		// Token: 0x06000F95 RID: 3989
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_KeyDown(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, EHTMLKeyModifiers eHTMLKeyModifiers);

		// Token: 0x06000F96 RID: 3990
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_KeyUp(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, EHTMLKeyModifiers eHTMLKeyModifiers);

		// Token: 0x06000F97 RID: 3991
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_KeyChar(HHTMLBrowser unBrowserHandle, uint cUnicodeChar, EHTMLKeyModifiers eHTMLKeyModifiers);

		// Token: 0x06000F98 RID: 3992
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetHorizontalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		// Token: 0x06000F99 RID: 3993
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetVerticalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		// Token: 0x06000F9A RID: 3994
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetKeyFocus(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.I1)] bool bHasKeyFocus);

		// Token: 0x06000F9B RID: 3995
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_ViewSource(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F9C RID: 3996
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_CopyToClipboard(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F9D RID: 3997
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_PasteFromClipboard(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000F9E RID: 3998
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_Find(HHTMLBrowser unBrowserHandle, InteropHelp.UTF8StringHandle pchSearchStr, [MarshalAs(UnmanagedType.I1)] bool bCurrentlyInFind, [MarshalAs(UnmanagedType.I1)] bool bReverse);

		// Token: 0x06000F9F RID: 3999
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_StopFind(HHTMLBrowser unBrowserHandle);

		// Token: 0x06000FA0 RID: 4000
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_GetLinkAtPosition(HHTMLBrowser unBrowserHandle, int x, int y);

		// Token: 0x06000FA1 RID: 4001
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetCookie(InteropHelp.UTF8StringHandle pchHostname, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue, InteropHelp.UTF8StringHandle pchPath, uint nExpires, [MarshalAs(UnmanagedType.I1)] bool bSecure, [MarshalAs(UnmanagedType.I1)] bool bHTTPOnly);

		// Token: 0x06000FA2 RID: 4002
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetPageScaleFactor(HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY);

		// Token: 0x06000FA3 RID: 4003
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_SetBackgroundMode(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.I1)] bool bBackgroundMode);

		// Token: 0x06000FA4 RID: 4004
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_AllowStartRequest(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.I1)] bool bAllowed);

		// Token: 0x06000FA5 RID: 4005
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_JSDialogResponse(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.I1)] bool bResult);

		// Token: 0x06000FA6 RID: 4006
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamHTMLSurface_FileLoadDialogResponse(HHTMLBrowser unBrowserHandle, IntPtr pchSelectedFiles);

		// Token: 0x06000FA7 RID: 4007
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamHTTP_CreateHTTPRequest(EHTTPMethod eHTTPRequestMethod, InteropHelp.UTF8StringHandle pchAbsoluteURL);

		// Token: 0x06000FA8 RID: 4008
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue);

		// Token: 0x06000FA9 RID: 4009
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds);

		// Token: 0x06000FAA RID: 4010
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, InteropHelp.UTF8StringHandle pchHeaderValue);

		// Token: 0x06000FAB RID: 4011
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchParamName, InteropHelp.UTF8StringHandle pchParamValue);

		// Token: 0x06000FAC RID: 4012
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SendHTTPRequest(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle);

		// Token: 0x06000FAD RID: 4013
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle);

		// Token: 0x06000FAE RID: 4014
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_DeferHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x06000FAF RID: 4015
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_PrioritizeHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x06000FB0 RID: 4016
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, out uint unResponseHeaderSize);

		// Token: 0x06000FB1 RID: 4017
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize);

		// Token: 0x06000FB2 RID: 4018
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseBodySize(HTTPRequestHandle hRequest, out uint unBodySize);

		// Token: 0x06000FB3 RID: 4019
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseBodyData(HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06000FB4 RID: 4020
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06000FB5 RID: 4021
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_ReleaseHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x06000FB6 RID: 4022
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, out float pflPercentOut);

		// Token: 0x06000FB7 RID: 4023
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchContentType, byte[] pubBody, uint unBodyLen);

		// Token: 0x06000FB8 RID: 4024
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamHTTP_CreateCookieContainer([MarshalAs(UnmanagedType.I1)] bool bAllowResponsesToModify);

		// Token: 0x06000FB9 RID: 4025
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x06000FBA RID: 4026
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetCookie(HTTPCookieContainerHandle hCookieContainer, InteropHelp.UTF8StringHandle pchHost, InteropHelp.UTF8StringHandle pchUrl, InteropHelp.UTF8StringHandle pchCookie);

		// Token: 0x06000FBB RID: 4027
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x06000FBC RID: 4028
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchUserAgentInfo);

		// Token: 0x06000FBD RID: 4029
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.I1)] bool bRequireVerifiedCertificate);

		// Token: 0x06000FBE RID: 4030
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds);

		// Token: 0x06000FBF RID: 4031
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, out bool pbWasTimedOut);

		// Token: 0x06000FC0 RID: 4032
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EResult ISteamInventory_GetResultStatus(SteamInventoryResult_t resultHandle);

		// Token: 0x06000FC1 RID: 4033
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetResultItems(SteamInventoryResult_t resultHandle, [In] [Out] SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize);

		// Token: 0x06000FC2 RID: 4034
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, InteropHelp.UTF8StringHandle pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x06000FC3 RID: 4035
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamInventory_GetResultTimestamp(SteamInventoryResult_t resultHandle);

		// Token: 0x06000FC4 RID: 4036
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected);

		// Token: 0x06000FC5 RID: 4037
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamInventory_DestroyResult(SteamInventoryResult_t resultHandle);

		// Token: 0x06000FC6 RID: 4038
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetAllItems(out SteamInventoryResult_t pResultHandle);

		// Token: 0x06000FC7 RID: 4039
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemsByID(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs);

		// Token: 0x06000FC8 RID: 4040
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize);

		// Token: 0x06000FC9 RID: 4041
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, [MarshalAs(UnmanagedType.I1)] bool bRESERVED_MUST_BE_FALSE);

		// Token: 0x06000FCA RID: 4042
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GenerateItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength);

		// Token: 0x06000FCB RID: 4043
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GrantPromoItems(out SteamInventoryResult_t pResultHandle);

		// Token: 0x06000FCC RID: 4044
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef);

		// Token: 0x06000FCD RID: 4045
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_AddPromoItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayItemDefs, uint unArrayLength);

		// Token: 0x06000FCE RID: 4046
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity);

		// Token: 0x06000FCF RID: 4047
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_ExchangeItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayGenerate, [In] [Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [In] [Out] SteamItemInstanceID_t[] pArrayDestroy, [In] [Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength);

		// Token: 0x06000FD0 RID: 4048
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest);

		// Token: 0x06000FD1 RID: 4049
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamInventory_SendItemDropHeartbeat();

		// Token: 0x06000FD2 RID: 4050
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition);

		// Token: 0x06000FD3 RID: 4051
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, [In] [Out] SteamItemInstanceID_t[] pArrayGive, [In] [Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [In] [Out] SteamItemInstanceID_t[] pArrayGet, [In] [Out] uint[] pArrayGetQuantity, uint nArrayGetLength);

		// Token: 0x06000FD4 RID: 4052
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_LoadItemDefinitions();

		// Token: 0x06000FD5 RID: 4053
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemDefinitionIDs([In] [Out] SteamItemDef_t[] pItemDefIDs, out uint punItemDefIDsArraySize);

		// Token: 0x06000FD6 RID: 4054
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemDefinitionProperty(SteamItemDef_t iDefinition, InteropHelp.UTF8StringHandle pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x06000FD7 RID: 4055
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID);

		// Token: 0x06000FD8 RID: 4056
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetEligiblePromoItemDefinitionIDs(CSteamID steamID, [In] [Out] SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		// Token: 0x06000FD9 RID: 4057
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_GetFavoriteGameCount();

		// Token: 0x06000FDA RID: 4058
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetFavoriteGame(int iGame, out AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer);

		// Token: 0x06000FDB RID: 4059
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_AddFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer);

		// Token: 0x06000FDC RID: 4060
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_RemoveFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags);

		// Token: 0x06000FDD RID: 4061
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_RequestLobbyList();

		// Token: 0x06000FDE RID: 4062
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListStringFilter(InteropHelp.UTF8StringHandle pchKeyToMatch, InteropHelp.UTF8StringHandle pchValueToMatch, ELobbyComparison eComparisonType);

		// Token: 0x06000FDF RID: 4063
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListNumericalFilter(InteropHelp.UTF8StringHandle pchKeyToMatch, int nValueToMatch, ELobbyComparison eComparisonType);

		// Token: 0x06000FE0 RID: 4064
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListNearValueFilter(InteropHelp.UTF8StringHandle pchKeyToMatch, int nValueToBeCloseTo);

		// Token: 0x06000FE1 RID: 4065
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(int nSlotsAvailable);

		// Token: 0x06000FE2 RID: 4066
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter eLobbyDistanceFilter);

		// Token: 0x06000FE3 RID: 4067
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListResultCountFilter(int cMaxResults);

		// Token: 0x06000FE4 RID: 4068
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(CSteamID steamIDLobby);

		// Token: 0x06000FE5 RID: 4069
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_GetLobbyByIndex(int iLobby);

		// Token: 0x06000FE6 RID: 4070
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_CreateLobby(ELobbyType eLobbyType, int cMaxMembers);

		// Token: 0x06000FE7 RID: 4071
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_JoinLobby(CSteamID steamIDLobby);

		// Token: 0x06000FE8 RID: 4072
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_LeaveLobby(CSteamID steamIDLobby);

		// Token: 0x06000FE9 RID: 4073
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_InviteUserToLobby(CSteamID steamIDLobby, CSteamID steamIDInvitee);

		// Token: 0x06000FEA RID: 4074
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_GetNumLobbyMembers(CSteamID steamIDLobby);

		// Token: 0x06000FEB RID: 4075
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_GetLobbyMemberByIndex(CSteamID steamIDLobby, int iMember);

		// Token: 0x06000FEC RID: 4076
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmaking_GetLobbyData(CSteamID steamIDLobby, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06000FED RID: 4077
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyData(CSteamID steamIDLobby, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x06000FEE RID: 4078
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_GetLobbyDataCount(CSteamID steamIDLobby);

		// Token: 0x06000FEF RID: 4079
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetLobbyDataByIndex(CSteamID steamIDLobby, int iLobbyData, IntPtr pchKey, int cchKeyBufferSize, IntPtr pchValue, int cchValueBufferSize);

		// Token: 0x06000FF0 RID: 4080
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_DeleteLobbyData(CSteamID steamIDLobby, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06000FF1 RID: 4081
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmaking_GetLobbyMemberData(CSteamID steamIDLobby, CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06000FF2 RID: 4082
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_SetLobbyMemberData(CSteamID steamIDLobby, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x06000FF3 RID: 4083
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SendLobbyChatMsg(CSteamID steamIDLobby, byte[] pvMsgBody, int cubMsgBody);

		// Token: 0x06000FF4 RID: 4084
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_GetLobbyChatEntry(CSteamID steamIDLobby, int iChatID, out CSteamID pSteamIDUser, byte[] pvData, int cubData, out EChatEntryType peChatEntryType);

		// Token: 0x06000FF5 RID: 4085
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_RequestLobbyData(CSteamID steamIDLobby);

		// Token: 0x06000FF6 RID: 4086
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmaking_SetLobbyGameServer(CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, CSteamID steamIDGameServer);

		// Token: 0x06000FF7 RID: 4087
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetLobbyGameServer(CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out CSteamID psteamIDGameServer);

		// Token: 0x06000FF8 RID: 4088
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyMemberLimit(CSteamID steamIDLobby, int cMaxMembers);

		// Token: 0x06000FF9 RID: 4089
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmaking_GetLobbyMemberLimit(CSteamID steamIDLobby);

		// Token: 0x06000FFA RID: 4090
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyType(CSteamID steamIDLobby, ELobbyType eLobbyType);

		// Token: 0x06000FFB RID: 4091
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyJoinable(CSteamID steamIDLobby, [MarshalAs(UnmanagedType.I1)] bool bLobbyJoinable);

		// Token: 0x06000FFC RID: 4092
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamMatchmaking_GetLobbyOwner(CSteamID steamIDLobby);

		// Token: 0x06000FFD RID: 4093
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyOwner(CSteamID steamIDLobby, CSteamID steamIDNewOwner);

		// Token: 0x06000FFE RID: 4094
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLinkedLobby(CSteamID steamIDLobby, CSteamID steamIDLobbyDependent);

		// Token: 0x06000FFF RID: 4095
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestInternetServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x06001000 RID: 4096
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestLANServerList(AppId_t iApp, IntPtr pRequestServersResponse);

		// Token: 0x06001001 RID: 4097
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestFriendsServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x06001002 RID: 4098
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestFavoritesServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x06001003 RID: 4099
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestHistoryServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x06001004 RID: 4100
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_RequestSpectatorServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x06001005 RID: 4101
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmakingServers_ReleaseRequest(HServerListRequest hServerListRequest);

		// Token: 0x06001006 RID: 4102
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamMatchmakingServers_GetServerDetails(HServerListRequest hRequest, int iServer);

		// Token: 0x06001007 RID: 4103
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmakingServers_CancelQuery(HServerListRequest hRequest);

		// Token: 0x06001008 RID: 4104
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmakingServers_RefreshQuery(HServerListRequest hRequest);

		// Token: 0x06001009 RID: 4105
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMatchmakingServers_IsRefreshing(HServerListRequest hRequest);

		// Token: 0x0600100A RID: 4106
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmakingServers_GetServerCount(HServerListRequest hRequest);

		// Token: 0x0600100B RID: 4107
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmakingServers_RefreshServer(HServerListRequest hRequest, int iServer);

		// Token: 0x0600100C RID: 4108
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmakingServers_PingServer(uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x0600100D RID: 4109
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmakingServers_PlayerDetails(uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x0600100E RID: 4110
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamMatchmakingServers_ServerRules(uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x0600100F RID: 4111
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMatchmakingServers_CancelServerQuery(HServerQuery hServerQuery);

		// Token: 0x06001010 RID: 4112
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusic_BIsEnabled();

		// Token: 0x06001011 RID: 4113
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusic_BIsPlaying();

		// Token: 0x06001012 RID: 4114
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern AudioPlayback_Status ISteamMusic_GetPlaybackStatus();

		// Token: 0x06001013 RID: 4115
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMusic_Play();

		// Token: 0x06001014 RID: 4116
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMusic_Pause();

		// Token: 0x06001015 RID: 4117
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMusic_PlayPrevious();

		// Token: 0x06001016 RID: 4118
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMusic_PlayNext();

		// Token: 0x06001017 RID: 4119
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamMusic_SetVolume(float flVolume);

		// Token: 0x06001018 RID: 4120
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern float ISteamMusic_GetVolume();

		// Token: 0x06001019 RID: 4121
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_RegisterSteamMusicRemote(InteropHelp.UTF8StringHandle pchName);

		// Token: 0x0600101A RID: 4122
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_DeregisterSteamMusicRemote();

		// Token: 0x0600101B RID: 4123
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_BIsCurrentMusicRemote();

		// Token: 0x0600101C RID: 4124
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_BActivationSuccess([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x0600101D RID: 4125
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetDisplayName(InteropHelp.UTF8StringHandle pchDisplayName);

		// Token: 0x0600101E RID: 4126
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetPNGIcon_64x64(byte[] pvBuffer, uint cbBufferLength);

		// Token: 0x0600101F RID: 4127
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnablePlayPrevious([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001020 RID: 4128
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnablePlayNext([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001021 RID: 4129
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnableShuffled([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001022 RID: 4130
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnableLooped([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001023 RID: 4131
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnableQueue([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001024 RID: 4132
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_EnablePlaylists([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001025 RID: 4133
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdatePlaybackStatus(AudioPlayback_Status nStatus);

		// Token: 0x06001026 RID: 4134
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateShuffled([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001027 RID: 4135
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateLooped([MarshalAs(UnmanagedType.I1)] bool bValue);

		// Token: 0x06001028 RID: 4136
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateVolume(float flValue);

		// Token: 0x06001029 RID: 4137
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_CurrentEntryWillChange();

		// Token: 0x0600102A RID: 4138
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_CurrentEntryIsAvailable([MarshalAs(UnmanagedType.I1)] bool bAvailable);

		// Token: 0x0600102B RID: 4139
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateCurrentEntryText(InteropHelp.UTF8StringHandle pchText);

		// Token: 0x0600102C RID: 4140
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds(int nValue);

		// Token: 0x0600102D RID: 4141
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_UpdateCurrentEntryCoverArt(byte[] pvBuffer, uint cbBufferLength);

		// Token: 0x0600102E RID: 4142
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_CurrentEntryDidChange();

		// Token: 0x0600102F RID: 4143
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_QueueWillChange();

		// Token: 0x06001030 RID: 4144
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_ResetQueueEntries();

		// Token: 0x06001031 RID: 4145
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetQueueEntry(int nID, int nPosition, InteropHelp.UTF8StringHandle pchEntryText);

		// Token: 0x06001032 RID: 4146
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetCurrentQueueEntry(int nID);

		// Token: 0x06001033 RID: 4147
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_QueueDidChange();

		// Token: 0x06001034 RID: 4148
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_PlaylistWillChange();

		// Token: 0x06001035 RID: 4149
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_ResetPlaylistEntries();

		// Token: 0x06001036 RID: 4150
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetPlaylistEntry(int nID, int nPosition, InteropHelp.UTF8StringHandle pchEntryText);

		// Token: 0x06001037 RID: 4151
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_SetCurrentPlaylistEntry(int nID);

		// Token: 0x06001038 RID: 4152
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamMusicRemote_PlaylistDidChange();

		// Token: 0x06001039 RID: 4153
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_SendP2PPacket(CSteamID steamIDRemote, byte[] pubData, uint cubData, EP2PSend eP2PSendType, int nChannel);

		// Token: 0x0600103A RID: 4154
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel);

		// Token: 0x0600103B RID: 4155
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel);

		// Token: 0x0600103C RID: 4156
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_AcceptP2PSessionWithUser(CSteamID steamIDRemote);

		// Token: 0x0600103D RID: 4157
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_CloseP2PSessionWithUser(CSteamID steamIDRemote);

		// Token: 0x0600103E RID: 4158
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel);

		// Token: 0x0600103F RID: 4159
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetP2PSessionState(CSteamID steamIDRemote, out P2PSessionState_t pConnectionState);

		// Token: 0x06001040 RID: 4160
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_AllowP2PPacketRelay([MarshalAs(UnmanagedType.I1)] bool bAllow);

		// Token: 0x06001041 RID: 4161
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamNetworking_CreateListenSocket(int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs(UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		// Token: 0x06001042 RID: 4162
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamNetworking_CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs(UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		// Token: 0x06001043 RID: 4163
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamNetworking_CreateConnectionSocket(uint nIP, ushort nPort, int nTimeoutSec);

		// Token: 0x06001044 RID: 4164
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_DestroySocket(SNetSocket_t hSocket, [MarshalAs(UnmanagedType.I1)] bool bNotifyRemoteEnd);

		// Token: 0x06001045 RID: 4165
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_DestroyListenSocket(SNetListenSocket_t hSocket, [MarshalAs(UnmanagedType.I1)] bool bNotifyRemoteEnd);

		// Token: 0x06001046 RID: 4166
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_SendDataOnSocket(SNetSocket_t hSocket, byte[] pubData, uint cubData, [MarshalAs(UnmanagedType.I1)] bool bReliable);

		// Token: 0x06001047 RID: 4167
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize);

		// Token: 0x06001048 RID: 4168
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_RetrieveDataFromSocket(SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize);

		// Token: 0x06001049 RID: 4169
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket);

		// Token: 0x0600104A RID: 4170
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_RetrieveData(SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket);

		// Token: 0x0600104B RID: 4171
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out uint punIPRemote, out ushort punPortRemote);

		// Token: 0x0600104C RID: 4172
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetListenSocketInfo(SNetListenSocket_t hListenSocket, out uint pnIP, out ushort pnPort);

		// Token: 0x0600104D RID: 4173
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ESNetSocketConnectionType ISteamNetworking_GetSocketConnectionType(SNetSocket_t hSocket);

		// Token: 0x0600104E RID: 4174
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamNetworking_GetMaxPacketSize(SNetSocket_t hSocket);

		// Token: 0x0600104F RID: 4175
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWrite(InteropHelp.UTF8StringHandle pchFile, byte[] pvData, int cubData);

		// Token: 0x06001050 RID: 4176
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamRemoteStorage_FileRead(InteropHelp.UTF8StringHandle pchFile, byte[] pvData, int cubDataToRead);

		// Token: 0x06001051 RID: 4177
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_FileWriteAsync(InteropHelp.UTF8StringHandle pchFile, byte[] pvData, uint cubData);

		// Token: 0x06001052 RID: 4178
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_FileReadAsync(InteropHelp.UTF8StringHandle pchFile, uint nOffset, uint cubToRead);

		// Token: 0x06001053 RID: 4179
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileReadAsyncComplete(SteamAPICall_t hReadCall, byte[] pvBuffer, uint cubToRead);

		// Token: 0x06001054 RID: 4180
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileForget(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001055 RID: 4181
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileDelete(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001056 RID: 4182
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_FileShare(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001057 RID: 4183
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_SetSyncPlatforms(InteropHelp.UTF8StringHandle pchFile, ERemoteStoragePlatform eRemoteStoragePlatform);

		// Token: 0x06001058 RID: 4184
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_FileWriteStreamOpen(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001059 RID: 4185
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, byte[] pvData, int cubData);

		// Token: 0x0600105A RID: 4186
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle);

		// Token: 0x0600105B RID: 4187
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle);

		// Token: 0x0600105C RID: 4188
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileExists(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x0600105D RID: 4189
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FilePersisted(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x0600105E RID: 4190
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamRemoteStorage_GetFileSize(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x0600105F RID: 4191
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern long ISteamRemoteStorage_GetFileTimestamp(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001060 RID: 4192
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ERemoteStoragePlatform ISteamRemoteStorage_GetSyncPlatforms(InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001061 RID: 4193
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamRemoteStorage_GetFileCount();

		// Token: 0x06001062 RID: 4194
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamRemoteStorage_GetFileNameAndSize(int iFile, out int pnFileSizeInBytes);

		// Token: 0x06001063 RID: 4195
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetQuota(out ulong pnTotalBytes, out ulong puAvailableBytes);

		// Token: 0x06001064 RID: 4196
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_IsCloudEnabledForAccount();

		// Token: 0x06001065 RID: 4197
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_IsCloudEnabledForApp();

		// Token: 0x06001066 RID: 4198
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamRemoteStorage_SetCloudEnabledForApp([MarshalAs(UnmanagedType.I1)] bool bEnabled);

		// Token: 0x06001067 RID: 4199
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_UGCDownload(UGCHandle_t hContent, uint unPriority);

		// Token: 0x06001068 RID: 4200
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetUGCDownloadProgress(UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected);

		// Token: 0x06001069 RID: 4201
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetUGCDetails(UGCHandle_t hContent, out AppId_t pnAppID, out IntPtr ppchName, out int pnFileSizeInBytes, out CSteamID pSteamIDOwner);

		// Token: 0x0600106A RID: 4202
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamRemoteStorage_UGCRead(UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, EUGCReadAction eAction);

		// Token: 0x0600106B RID: 4203
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamRemoteStorage_GetCachedUGCCount();

		// Token: 0x0600106C RID: 4204
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_GetCachedUGCHandle(int iCachedContent);

		// Token: 0x0600106D RID: 4205
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_PublishWorkshopFile(InteropHelp.UTF8StringHandle pchFile, InteropHelp.UTF8StringHandle pchPreviewFile, AppId_t nConsumerAppId, InteropHelp.UTF8StringHandle pchTitle, InteropHelp.UTF8StringHandle pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IntPtr pTags, EWorkshopFileType eWorkshopFileType);

		// Token: 0x0600106E RID: 4206
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_CreatePublishedFileUpdateRequest(PublishedFileId_t unPublishedFileId);

		// Token: 0x0600106F RID: 4207
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileFile(PublishedFileUpdateHandle_t updateHandle, InteropHelp.UTF8StringHandle pchFile);

		// Token: 0x06001070 RID: 4208
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle_t updateHandle, InteropHelp.UTF8StringHandle pchPreviewFile);

		// Token: 0x06001071 RID: 4209
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileTitle(PublishedFileUpdateHandle_t updateHandle, InteropHelp.UTF8StringHandle pchTitle);

		// Token: 0x06001072 RID: 4210
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileDescription(PublishedFileUpdateHandle_t updateHandle, InteropHelp.UTF8StringHandle pchDescription);

		// Token: 0x06001073 RID: 4211
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileVisibility(PublishedFileUpdateHandle_t updateHandle, ERemoteStoragePublishedFileVisibility eVisibility);

		// Token: 0x06001074 RID: 4212
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileTags(PublishedFileUpdateHandle_t updateHandle, IntPtr pTags);

		// Token: 0x06001075 RID: 4213
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_CommitPublishedFileUpdate(PublishedFileUpdateHandle_t updateHandle);

		// Token: 0x06001076 RID: 4214
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_GetPublishedFileDetails(PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld);

		// Token: 0x06001077 RID: 4215
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_DeletePublishedFile(PublishedFileId_t unPublishedFileId);

		// Token: 0x06001078 RID: 4216
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_EnumerateUserPublishedFiles(uint unStartIndex);

		// Token: 0x06001079 RID: 4217
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_SubscribePublishedFile(PublishedFileId_t unPublishedFileId);

		// Token: 0x0600107A RID: 4218
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_EnumerateUserSubscribedFiles(uint unStartIndex);

		// Token: 0x0600107B RID: 4219
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_UnsubscribePublishedFile(PublishedFileId_t unPublishedFileId);

		// Token: 0x0600107C RID: 4220
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle_t updateHandle, InteropHelp.UTF8StringHandle pchChangeDescription);

		// Token: 0x0600107D RID: 4221
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_GetPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId);

		// Token: 0x0600107E RID: 4222
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_UpdateUserPublishedItemVote(PublishedFileId_t unPublishedFileId, [MarshalAs(UnmanagedType.I1)] bool bVoteUp);

		// Token: 0x0600107F RID: 4223
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_GetUserPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId);

		// Token: 0x06001080 RID: 4224
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(CSteamID steamId, uint unStartIndex, IntPtr pRequiredTags, IntPtr pExcludedTags);

		// Token: 0x06001081 RID: 4225
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_PublishVideo(EWorkshopVideoProvider eVideoProvider, InteropHelp.UTF8StringHandle pchVideoAccount, InteropHelp.UTF8StringHandle pchVideoIdentifier, InteropHelp.UTF8StringHandle pchPreviewFile, AppId_t nConsumerAppId, InteropHelp.UTF8StringHandle pchTitle, InteropHelp.UTF8StringHandle pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IntPtr pTags);

		// Token: 0x06001082 RID: 4226
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_SetUserPublishedFileAction(PublishedFileId_t unPublishedFileId, EWorkshopFileAction eAction);

		// Token: 0x06001083 RID: 4227
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(EWorkshopFileAction eAction, uint unStartIndex);

		// Token: 0x06001084 RID: 4228
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(EWorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, IntPtr pTags, IntPtr pUserTags);

		// Token: 0x06001085 RID: 4229
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamRemoteStorage_UGCDownloadToLocation(UGCHandle_t hContent, InteropHelp.UTF8StringHandle pchLocation, uint unPriority);

		// Token: 0x06001086 RID: 4230
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamScreenshots_WriteScreenshot(byte[] pubRGB, uint cubRGB, int nWidth, int nHeight);

		// Token: 0x06001087 RID: 4231
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamScreenshots_AddScreenshotToLibrary(InteropHelp.UTF8StringHandle pchFilename, InteropHelp.UTF8StringHandle pchThumbnailFilename, int nWidth, int nHeight);

		// Token: 0x06001088 RID: 4232
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamScreenshots_TriggerScreenshot();

		// Token: 0x06001089 RID: 4233
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamScreenshots_HookScreenshots([MarshalAs(UnmanagedType.I1)] bool bHook);

		// Token: 0x0600108A RID: 4234
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_SetLocation(ScreenshotHandle hScreenshot, InteropHelp.UTF8StringHandle pchLocation);

		// Token: 0x0600108B RID: 4235
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_TagUser(ScreenshotHandle hScreenshot, CSteamID steamID);

		// Token: 0x0600108C RID: 4236
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_TagPublishedFile(ScreenshotHandle hScreenshot, PublishedFileId_t unPublishedFileID);

		// Token: 0x0600108D RID: 4237
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_IsScreenshotsHooked();

		// Token: 0x0600108E RID: 4238
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamScreenshots_AddVRScreenshotToLibrary(EVRScreenshotType eType, InteropHelp.UTF8StringHandle pchFilename, InteropHelp.UTF8StringHandle pchVRFilename);

		// Token: 0x0600108F RID: 4239
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_CreateQueryUserUGCRequest(AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage);

		// Token: 0x06001090 RID: 4240
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage);

		// Token: 0x06001091 RID: 4241
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_CreateQueryUGCDetailsRequest([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x06001092 RID: 4242
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_SendQueryUGCRequest(UGCQueryHandle_t handle);

		// Token: 0x06001093 RID: 4243
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails);

		// Token: 0x06001094 RID: 4244
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize);

		// Token: 0x06001095 RID: 4245
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize);

		// Token: 0x06001096 RID: 4246
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, [In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x06001097 RID: 4247
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue);

		// Token: 0x06001098 RID: 4248
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUGC_GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index);

		// Token: 0x06001099 RID: 4249
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType);

		// Token: 0x0600109A RID: 4250
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUGC_GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index);

		// Token: 0x0600109B RID: 4251
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize);

		// Token: 0x0600109C RID: 4252
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_ReleaseQueryUGCRequest(UGCQueryHandle_t handle);

		// Token: 0x0600109D RID: 4253
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddRequiredTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pTagName);

		// Token: 0x0600109E RID: 4254
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddExcludedTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pTagName);

		// Token: 0x0600109F RID: 4255
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnOnlyIDs(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnOnlyIDs);

		// Token: 0x060010A0 RID: 4256
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnKeyValueTags(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnKeyValueTags);

		// Token: 0x060010A1 RID: 4257
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnLongDescription(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnLongDescription);

		// Token: 0x060010A2 RID: 4258
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnMetadata(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnMetadata);

		// Token: 0x060010A3 RID: 4259
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnChildren(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnChildren);

		// Token: 0x060010A4 RID: 4260
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnAdditionalPreviews(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnAdditionalPreviews);

		// Token: 0x060010A5 RID: 4261
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnTotalOnly(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnTotalOnly);

		// Token: 0x060010A6 RID: 4262
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060010A7 RID: 4263
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetLanguage(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pchLanguage);

		// Token: 0x060010A8 RID: 4264
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds);

		// Token: 0x060010A9 RID: 4265
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetCloudFileNameFilter(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pMatchCloudFileName);

		// Token: 0x060010AA RID: 4266
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetMatchAnyTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bMatchAnyTag);

		// Token: 0x060010AB RID: 4267
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetSearchText(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pSearchText);

		// Token: 0x060010AC RID: 4268
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060010AD RID: 4269
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddRequiredKeyValueTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pKey, InteropHelp.UTF8StringHandle pValue);

		// Token: 0x060010AE RID: 4270
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds);

		// Token: 0x060010AF RID: 4271
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType);

		// Token: 0x060010B0 RID: 4272
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060010B1 RID: 4273
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemTitle(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchTitle);

		// Token: 0x060010B2 RID: 4274
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemDescription(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchDescription);

		// Token: 0x060010B3 RID: 4275
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemUpdateLanguage(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchLanguage);

		// Token: 0x060010B4 RID: 4276
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemMetadata(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchMetaData);

		// Token: 0x060010B5 RID: 4277
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility);

		// Token: 0x060010B6 RID: 4278
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemTags(UGCUpdateHandle_t updateHandle, IntPtr pTags);

		// Token: 0x060010B7 RID: 4279
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemContent(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszContentFolder);

		// Token: 0x060010B8 RID: 4280
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemPreview(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszPreviewFile);

		// Token: 0x060010B9 RID: 4281
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveItemKeyValueTags(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x060010BA RID: 4282
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemKeyValueTag(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x060010BB RID: 4283
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemPreviewFile(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszPreviewFile, EItemPreviewType type);

		// Token: 0x060010BC RID: 4284
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemPreviewVideo(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszVideoID);

		// Token: 0x060010BD RID: 4285
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, InteropHelp.UTF8StringHandle pszPreviewFile);

		// Token: 0x060010BE RID: 4286
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, InteropHelp.UTF8StringHandle pszVideoID);

		// Token: 0x060010BF RID: 4287
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveItemPreview(UGCUpdateHandle_t handle, uint index);

		// Token: 0x060010C0 RID: 4288
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_SubmitItemUpdate(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchChangeNote);

		// Token: 0x060010C1 RID: 4289
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EItemUpdateStatus ISteamUGC_GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal);

		// Token: 0x060010C2 RID: 4290
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_SetUserItemVote(PublishedFileId_t nPublishedFileID, [MarshalAs(UnmanagedType.I1)] bool bVoteUp);

		// Token: 0x060010C3 RID: 4291
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_GetUserItemVote(PublishedFileId_t nPublishedFileID);

		// Token: 0x060010C4 RID: 4292
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060010C5 RID: 4293
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060010C6 RID: 4294
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_SubscribeItem(PublishedFileId_t nPublishedFileID);

		// Token: 0x060010C7 RID: 4295
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_UnsubscribeItem(PublishedFileId_t nPublishedFileID);

		// Token: 0x060010C8 RID: 4296
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUGC_GetNumSubscribedItems();

		// Token: 0x060010C9 RID: 4297
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUGC_GetSubscribedItems([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x060010CA RID: 4298
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUGC_GetItemState(PublishedFileId_t nPublishedFileID);

		// Token: 0x060010CB RID: 4299
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, out uint punTimeStamp);

		// Token: 0x060010CC RID: 4300
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal);

		// Token: 0x060010CD RID: 4301
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_DownloadItem(PublishedFileId_t nPublishedFileID, [MarshalAs(UnmanagedType.I1)] bool bHighPriority);

		// Token: 0x060010CE RID: 4302
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUGC_BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, InteropHelp.UTF8StringHandle pszFolder);

		// Token: 0x060010CF RID: 4303
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUGC_SuspendDownloads([MarshalAs(UnmanagedType.I1)] bool bSuspend);

		// Token: 0x060010D0 RID: 4304
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_StartPlaytimeTracking([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x060010D1 RID: 4305
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_StopPlaytimeTracking([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x060010D2 RID: 4306
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_StopPlaytimeTrackingForAllItems();

		// Token: 0x060010D3 RID: 4307
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID);

		// Token: 0x060010D4 RID: 4308
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUGC_RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID);

		// Token: 0x060010D5 RID: 4309
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUnifiedMessages_SendMethod(InteropHelp.UTF8StringHandle pchServiceMethod, byte[] pRequestBuffer, uint unRequestBufferSize, ulong unContext);

		// Token: 0x060010D6 RID: 4310
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUnifiedMessages_GetMethodResponseInfo(ClientUnifiedMessageHandle hHandle, out uint punResponseSize, out EResult peResult);

		// Token: 0x060010D7 RID: 4311
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUnifiedMessages_GetMethodResponseData(ClientUnifiedMessageHandle hHandle, byte[] pResponseBuffer, uint unResponseBufferSize, [MarshalAs(UnmanagedType.I1)] bool bAutoRelease);

		// Token: 0x060010D8 RID: 4312
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUnifiedMessages_ReleaseMethod(ClientUnifiedMessageHandle hHandle);

		// Token: 0x060010D9 RID: 4313
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUnifiedMessages_SendNotification(InteropHelp.UTF8StringHandle pchServiceNotification, byte[] pNotificationBuffer, uint unNotificationBufferSize);

		// Token: 0x060010DA RID: 4314
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUser_GetHSteamUser();

		// Token: 0x060010DB RID: 4315
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BLoggedOn();

		// Token: 0x060010DC RID: 4316
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUser_GetSteamID();

		// Token: 0x060010DD RID: 4317
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUser_InitiateGameConnection(byte[] pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs(UnmanagedType.I1)] bool bSecure);

		// Token: 0x060010DE RID: 4318
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_TerminateGameConnection(uint unIPServer, ushort usPortServer);

		// Token: 0x060010DF RID: 4319
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_TrackAppUsageEvent(CGameID gameID, int eAppUsageEvent, InteropHelp.UTF8StringHandle pchExtraInfo);

		// Token: 0x060010E0 RID: 4320
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_GetUserDataFolder(IntPtr pchBuffer, int cubBuffer);

		// Token: 0x060010E1 RID: 4321
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_StartVoiceRecording();

		// Token: 0x060010E2 RID: 4322
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_StopVoiceRecording();

		// Token: 0x060010E3 RID: 4323
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EVoiceResult ISteamUser_GetAvailableVoice(out uint pcbCompressed, IntPtr pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		// Token: 0x060010E4 RID: 4324
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EVoiceResult ISteamUser_GetVoice([MarshalAs(UnmanagedType.I1)] bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, [MarshalAs(UnmanagedType.I1)] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, IntPtr nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		// Token: 0x060010E5 RID: 4325
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EVoiceResult ISteamUser_DecompressVoice(byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate);

		// Token: 0x060010E6 RID: 4326
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUser_GetVoiceOptimalSampleRate();

		// Token: 0x060010E7 RID: 4327
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUser_GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket);

		// Token: 0x060010E8 RID: 4328
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EBeginAuthSessionResult ISteamUser_BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID);

		// Token: 0x060010E9 RID: 4329
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_EndAuthSession(CSteamID steamID);

		// Token: 0x060010EA RID: 4330
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_CancelAuthTicket(HAuthTicket hAuthTicket);

		// Token: 0x060010EB RID: 4331
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EUserHasLicenseForAppResult ISteamUser_UserHasLicenseForApp(CSteamID steamID, AppId_t appID);

		// Token: 0x060010EC RID: 4332
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsBehindNAT();

		// Token: 0x060010ED RID: 4333
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUser_AdvertiseGame(CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer);

		// Token: 0x060010EE RID: 4334
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUser_RequestEncryptedAppTicket(byte[] pDataToInclude, int cbDataToInclude);

		// Token: 0x060010EF RID: 4335
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_GetEncryptedAppTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket);

		// Token: 0x060010F0 RID: 4336
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUser_GetGameBadgeLevel(int nSeries, [MarshalAs(UnmanagedType.I1)] bool bFoil);

		// Token: 0x060010F1 RID: 4337
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUser_GetPlayerSteamLevel();

		// Token: 0x060010F2 RID: 4338
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUser_RequestStoreAuthURL(InteropHelp.UTF8StringHandle pchRedirectURL);

		// Token: 0x060010F3 RID: 4339
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneVerified();

		// Token: 0x060010F4 RID: 4340
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsTwoFactorEnabled();

		// Token: 0x060010F5 RID: 4341
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneIdentifying();

		// Token: 0x060010F6 RID: 4342
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneRequiringVerification();

		// Token: 0x060010F7 RID: 4343
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_RequestCurrentStats();

		// Token: 0x060010F8 RID: 4344
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetStat(InteropHelp.UTF8StringHandle pchName, out int pData);

		// Token: 0x060010F9 RID: 4345
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetStat_(InteropHelp.UTF8StringHandle pchName, out float pData);

		// Token: 0x060010FA RID: 4346
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetStat(InteropHelp.UTF8StringHandle pchName, int nData);

		// Token: 0x060010FB RID: 4347
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetStat_(InteropHelp.UTF8StringHandle pchName, float fData);

		// Token: 0x060010FC RID: 4348
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_UpdateAvgRateStat(InteropHelp.UTF8StringHandle pchName, float flCountThisSession, double dSessionLength);

		// Token: 0x060010FD RID: 4349
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievement(InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		// Token: 0x060010FE RID: 4350
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetAchievement(InteropHelp.UTF8StringHandle pchName);

		// Token: 0x060010FF RID: 4351
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_ClearAchievement(InteropHelp.UTF8StringHandle pchName);

		// Token: 0x06001100 RID: 4352
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementAndUnlockTime(InteropHelp.UTF8StringHandle pchName, out bool pbAchieved, out uint punUnlockTime);

		// Token: 0x06001101 RID: 4353
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_StoreStats();

		// Token: 0x06001102 RID: 4354
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetAchievementIcon(InteropHelp.UTF8StringHandle pchName);

		// Token: 0x06001103 RID: 4355
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamUserStats_GetAchievementDisplayAttribute(InteropHelp.UTF8StringHandle pchName, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x06001104 RID: 4356
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_IndicateAchievementProgress(InteropHelp.UTF8StringHandle pchName, uint nCurProgress, uint nMaxProgress);

		// Token: 0x06001105 RID: 4357
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUserStats_GetNumAchievements();

		// Token: 0x06001106 RID: 4358
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamUserStats_GetAchievementName(uint iAchievement);

		// Token: 0x06001107 RID: 4359
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_RequestUserStats(CSteamID steamIDUser);

		// Token: 0x06001108 RID: 4360
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserStat(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out int pData);

		// Token: 0x06001109 RID: 4361
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserStat_(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out float pData);

		// Token: 0x0600110A RID: 4362
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserAchievement(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		// Token: 0x0600110B RID: 4363
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserAchievementAndUnlockTime(CSteamID steamIDUser, InteropHelp.UTF8StringHandle pchName, out bool pbAchieved, out uint punUnlockTime);

		// Token: 0x0600110C RID: 4364
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_ResetAllStats([MarshalAs(UnmanagedType.I1)] bool bAchievementsToo);

		// Token: 0x0600110D RID: 4365
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_FindOrCreateLeaderboard(InteropHelp.UTF8StringHandle pchLeaderboardName, ELeaderboardSortMethod eLeaderboardSortMethod, ELeaderboardDisplayType eLeaderboardDisplayType);

		// Token: 0x0600110E RID: 4366
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_FindLeaderboard(InteropHelp.UTF8StringHandle pchLeaderboardName);

		// Token: 0x0600110F RID: 4367
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamUserStats_GetLeaderboardName(SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x06001110 RID: 4368
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetLeaderboardEntryCount(SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x06001111 RID: 4369
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ELeaderboardSortMethod ISteamUserStats_GetLeaderboardSortMethod(SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x06001112 RID: 4370
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ELeaderboardDisplayType ISteamUserStats_GetLeaderboardDisplayType(SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x06001113 RID: 4371
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_DownloadLeaderboardEntries(SteamLeaderboard_t hSteamLeaderboard, ELeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd);

		// Token: 0x06001114 RID: 4372
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_DownloadLeaderboardEntriesForUsers(SteamLeaderboard_t hSteamLeaderboard, [In] [Out] CSteamID[] prgUsers, int cUsers);

		// Token: 0x06001115 RID: 4373
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetDownloadedLeaderboardEntry(SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, out LeaderboardEntry_t pLeaderboardEntry, [In] [Out] int[] pDetails, int cDetailsMax);

		// Token: 0x06001116 RID: 4374
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_UploadLeaderboardScore(SteamLeaderboard_t hSteamLeaderboard, ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In] [Out] int[] pScoreDetails, int cScoreDetailsCount);

		// Token: 0x06001117 RID: 4375
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_AttachLeaderboardUGC(SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC);

		// Token: 0x06001118 RID: 4376
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_GetNumberOfCurrentPlayers();

		// Token: 0x06001119 RID: 4377
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_RequestGlobalAchievementPercentages();

		// Token: 0x0600111A RID: 4378
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetMostAchievedAchievementInfo(IntPtr pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved);

		// Token: 0x0600111B RID: 4379
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetNextMostAchievedAchievementInfo(int iIteratorPrevious, IntPtr pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved);

		// Token: 0x0600111C RID: 4380
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementAchievedPercent(InteropHelp.UTF8StringHandle pchName, out float pflPercent);

		// Token: 0x0600111D RID: 4381
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUserStats_RequestGlobalStats(int nHistoryDays);

		// Token: 0x0600111E RID: 4382
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetGlobalStat(InteropHelp.UTF8StringHandle pchStatName, out long pData);

		// Token: 0x0600111F RID: 4383
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetGlobalStat_(InteropHelp.UTF8StringHandle pchStatName, out double pData);

		// Token: 0x06001120 RID: 4384
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetGlobalStatHistory(InteropHelp.UTF8StringHandle pchStatName, [In] [Out] long[] pData, uint cubData);

		// Token: 0x06001121 RID: 4385
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamUserStats_GetGlobalStatHistory_(InteropHelp.UTF8StringHandle pchStatName, [In] [Out] double[] pData, uint cubData);

		// Token: 0x06001122 RID: 4386
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetSecondsSinceAppActive();

		// Token: 0x06001123 RID: 4387
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetSecondsSinceComputerActive();

		// Token: 0x06001124 RID: 4388
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EUniverse ISteamUtils_GetConnectedUniverse();

		// Token: 0x06001125 RID: 4389
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetServerRealTime();

		// Token: 0x06001126 RID: 4390
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamUtils_GetIPCountry();

		// Token: 0x06001127 RID: 4391
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetImageSize(int iImage, out uint pnWidth, out uint pnHeight);

		// Token: 0x06001128 RID: 4392
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize);

		// Token: 0x06001129 RID: 4393
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetCSERIPPort(out uint unIP, out ushort usPort);

		// Token: 0x0600112A RID: 4394
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte ISteamUtils_GetCurrentBatteryPower();

		// Token: 0x0600112B RID: 4395
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetAppID();

		// Token: 0x0600112C RID: 4396
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUtils_SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition);

		// Token: 0x0600112D RID: 4397
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed);

		// Token: 0x0600112E RID: 4398
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ESteamAPICallFailure ISteamUtils_GetAPICallFailureReason(SteamAPICall_t hSteamAPICall);

		// Token: 0x0600112F RID: 4399
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed);

		// Token: 0x06001130 RID: 4400
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetIPCCallCount();

		// Token: 0x06001131 RID: 4401
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUtils_SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction);

		// Token: 0x06001132 RID: 4402
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsOverlayEnabled();

		// Token: 0x06001133 RID: 4403
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_BOverlayNeedsPresent();

		// Token: 0x06001134 RID: 4404
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamUtils_CheckFileSignature(InteropHelp.UTF8StringHandle szFileName);

		// Token: 0x06001135 RID: 4405
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, InteropHelp.UTF8StringHandle pchDescription, uint unCharMax, InteropHelp.UTF8StringHandle pchExistingText);

		// Token: 0x06001136 RID: 4406
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamUtils_GetEnteredGamepadTextLength();

		// Token: 0x06001137 RID: 4407
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetEnteredGamepadTextInput(IntPtr pchText, uint cchText);

		// Token: 0x06001138 RID: 4408
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamUtils_GetSteamUILanguage();

		// Token: 0x06001139 RID: 4409
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamRunningInVR();

		// Token: 0x0600113A RID: 4410
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUtils_SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset);

		// Token: 0x0600113B RID: 4411
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamInBigPictureMode();

		// Token: 0x0600113C RID: 4412
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUtils_StartVRDashboard();

		// Token: 0x0600113D RID: 4413
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsVRHeadsetStreamingEnabled();

		// Token: 0x0600113E RID: 4414
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamUtils_SetVRHeadsetStreamingEnabled([MarshalAs(UnmanagedType.I1)] bool bEnabled);

		// Token: 0x0600113F RID: 4415
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamVideo_GetVideoURL(AppId_t unVideoAppID);

		// Token: 0x06001140 RID: 4416
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamVideo_IsBroadcasting(out int pnNumViewers);

		// Token: 0x06001141 RID: 4417
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamVideo_GetOPFSettings(AppId_t unVideoAppID);

		// Token: 0x06001142 RID: 4418
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamVideo_GetOPFStringForApp(AppId_t unVideoAppID, IntPtr pchBuffer, ref int pnBufferSize);

		// Token: 0x06001143 RID: 4419
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerHTTP_CreateHTTPRequest(EHTTPMethod eHTTPRequestMethod, InteropHelp.UTF8StringHandle pchAbsoluteURL);

		// Token: 0x06001144 RID: 4420
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue);

		// Token: 0x06001145 RID: 4421
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds);

		// Token: 0x06001146 RID: 4422
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, InteropHelp.UTF8StringHandle pchHeaderValue);

		// Token: 0x06001147 RID: 4423
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchParamName, InteropHelp.UTF8StringHandle pchParamValue);

		// Token: 0x06001148 RID: 4424
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SendHTTPRequest(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle);

		// Token: 0x06001149 RID: 4425
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle);

		// Token: 0x0600114A RID: 4426
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_DeferHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x0600114B RID: 4427
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_PrioritizeHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x0600114C RID: 4428
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, out uint unResponseHeaderSize);

		// Token: 0x0600114D RID: 4429
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize);

		// Token: 0x0600114E RID: 4430
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPResponseBodySize(HTTPRequestHandle hRequest, out uint unBodySize);

		// Token: 0x0600114F RID: 4431
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPResponseBodyData(HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06001150 RID: 4432
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06001151 RID: 4433
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_ReleaseHTTPRequest(HTTPRequestHandle hRequest);

		// Token: 0x06001152 RID: 4434
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, out float pflPercentOut);

		// Token: 0x06001153 RID: 4435
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchContentType, byte[] pubBody, uint unBodyLen);

		// Token: 0x06001154 RID: 4436
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerHTTP_CreateCookieContainer([MarshalAs(UnmanagedType.I1)] bool bAllowResponsesToModify);

		// Token: 0x06001155 RID: 4437
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x06001156 RID: 4438
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetCookie(HTTPCookieContainerHandle hCookieContainer, InteropHelp.UTF8StringHandle pchHost, InteropHelp.UTF8StringHandle pchUrl, InteropHelp.UTF8StringHandle pchCookie);

		// Token: 0x06001157 RID: 4439
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x06001158 RID: 4440
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, InteropHelp.UTF8StringHandle pchUserAgentInfo);

		// Token: 0x06001159 RID: 4441
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.I1)] bool bRequireVerifiedCertificate);

		// Token: 0x0600115A RID: 4442
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds);

		// Token: 0x0600115B RID: 4443
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerHTTP_GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, out bool pbWasTimedOut);

		// Token: 0x0600115C RID: 4444
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EResult ISteamGameServerInventory_GetResultStatus(SteamInventoryResult_t resultHandle);

		// Token: 0x0600115D RID: 4445
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetResultItems(SteamInventoryResult_t resultHandle, [In] [Out] SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize);

		// Token: 0x0600115E RID: 4446
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, InteropHelp.UTF8StringHandle pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x0600115F RID: 4447
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerInventory_GetResultTimestamp(SteamInventoryResult_t resultHandle);

		// Token: 0x06001160 RID: 4448
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected);

		// Token: 0x06001161 RID: 4449
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerInventory_DestroyResult(SteamInventoryResult_t resultHandle);

		// Token: 0x06001162 RID: 4450
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetAllItems(out SteamInventoryResult_t pResultHandle);

		// Token: 0x06001163 RID: 4451
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetItemsByID(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs);

		// Token: 0x06001164 RID: 4452
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize);

		// Token: 0x06001165 RID: 4453
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, [MarshalAs(UnmanagedType.I1)] bool bRESERVED_MUST_BE_FALSE);

		// Token: 0x06001166 RID: 4454
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GenerateItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength);

		// Token: 0x06001167 RID: 4455
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GrantPromoItems(out SteamInventoryResult_t pResultHandle);

		// Token: 0x06001168 RID: 4456
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef);

		// Token: 0x06001169 RID: 4457
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_AddPromoItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayItemDefs, uint unArrayLength);

		// Token: 0x0600116A RID: 4458
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity);

		// Token: 0x0600116B RID: 4459
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_ExchangeItems(out SteamInventoryResult_t pResultHandle, [In] [Out] SteamItemDef_t[] pArrayGenerate, [In] [Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [In] [Out] SteamItemInstanceID_t[] pArrayDestroy, [In] [Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength);

		// Token: 0x0600116C RID: 4460
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest);

		// Token: 0x0600116D RID: 4461
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerInventory_SendItemDropHeartbeat();

		// Token: 0x0600116E RID: 4462
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition);

		// Token: 0x0600116F RID: 4463
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, [In] [Out] SteamItemInstanceID_t[] pArrayGive, [In] [Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [In] [Out] SteamItemInstanceID_t[] pArrayGet, [In] [Out] uint[] pArrayGetQuantity, uint nArrayGetLength);

		// Token: 0x06001170 RID: 4464
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_LoadItemDefinitions();

		// Token: 0x06001171 RID: 4465
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetItemDefinitionIDs([In] [Out] SteamItemDef_t[] pItemDefIDs, out uint punItemDefIDsArraySize);

		// Token: 0x06001172 RID: 4466
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetItemDefinitionProperty(SteamItemDef_t iDefinition, InteropHelp.UTF8StringHandle pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x06001173 RID: 4467
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerInventory_RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID);

		// Token: 0x06001174 RID: 4468
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerInventory_GetEligiblePromoItemDefinitionIDs(CSteamID steamID, [In] [Out] SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		// Token: 0x06001175 RID: 4469
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_SendP2PPacket(CSteamID steamIDRemote, byte[] pubData, uint cubData, EP2PSend eP2PSendType, int nChannel);

		// Token: 0x06001176 RID: 4470
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel);

		// Token: 0x06001177 RID: 4471
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel);

		// Token: 0x06001178 RID: 4472
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_AcceptP2PSessionWithUser(CSteamID steamIDRemote);

		// Token: 0x06001179 RID: 4473
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_CloseP2PSessionWithUser(CSteamID steamIDRemote);

		// Token: 0x0600117A RID: 4474
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel);

		// Token: 0x0600117B RID: 4475
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_GetP2PSessionState(CSteamID steamIDRemote, out P2PSessionState_t pConnectionState);

		// Token: 0x0600117C RID: 4476
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_AllowP2PPacketRelay([MarshalAs(UnmanagedType.I1)] bool bAllow);

		// Token: 0x0600117D RID: 4477
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerNetworking_CreateListenSocket(int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs(UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		// Token: 0x0600117E RID: 4478
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerNetworking_CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs(UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		// Token: 0x0600117F RID: 4479
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerNetworking_CreateConnectionSocket(uint nIP, ushort nPort, int nTimeoutSec);

		// Token: 0x06001180 RID: 4480
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_DestroySocket(SNetSocket_t hSocket, [MarshalAs(UnmanagedType.I1)] bool bNotifyRemoteEnd);

		// Token: 0x06001181 RID: 4481
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_DestroyListenSocket(SNetListenSocket_t hSocket, [MarshalAs(UnmanagedType.I1)] bool bNotifyRemoteEnd);

		// Token: 0x06001182 RID: 4482
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_SendDataOnSocket(SNetSocket_t hSocket, byte[] pubData, uint cubData, [MarshalAs(UnmanagedType.I1)] bool bReliable);

		// Token: 0x06001183 RID: 4483
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize);

		// Token: 0x06001184 RID: 4484
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_RetrieveDataFromSocket(SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize);

		// Token: 0x06001185 RID: 4485
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket);

		// Token: 0x06001186 RID: 4486
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_RetrieveData(SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket);

		// Token: 0x06001187 RID: 4487
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out uint punIPRemote, out ushort punPortRemote);

		// Token: 0x06001188 RID: 4488
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerNetworking_GetListenSocketInfo(SNetListenSocket_t hListenSocket, out uint pnIP, out ushort pnPort);

		// Token: 0x06001189 RID: 4489
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ESNetSocketConnectionType ISteamGameServerNetworking_GetSocketConnectionType(SNetSocket_t hSocket);

		// Token: 0x0600118A RID: 4490
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ISteamGameServerNetworking_GetMaxPacketSize(SNetSocket_t hSocket);

		// Token: 0x0600118B RID: 4491
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_CreateQueryUserUGCRequest(AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage);

		// Token: 0x0600118C RID: 4492
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage);

		// Token: 0x0600118D RID: 4493
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_CreateQueryUGCDetailsRequest([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x0600118E RID: 4494
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_SendQueryUGCRequest(UGCQueryHandle_t handle);

		// Token: 0x0600118F RID: 4495
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails);

		// Token: 0x06001190 RID: 4496
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize);

		// Token: 0x06001191 RID: 4497
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize);

		// Token: 0x06001192 RID: 4498
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, [In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x06001193 RID: 4499
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue);

		// Token: 0x06001194 RID: 4500
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUGC_GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index);

		// Token: 0x06001195 RID: 4501
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType);

		// Token: 0x06001196 RID: 4502
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUGC_GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index);

		// Token: 0x06001197 RID: 4503
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize);

		// Token: 0x06001198 RID: 4504
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_ReleaseQueryUGCRequest(UGCQueryHandle_t handle);

		// Token: 0x06001199 RID: 4505
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddRequiredTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pTagName);

		// Token: 0x0600119A RID: 4506
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddExcludedTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pTagName);

		// Token: 0x0600119B RID: 4507
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnOnlyIDs(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnOnlyIDs);

		// Token: 0x0600119C RID: 4508
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnKeyValueTags(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnKeyValueTags);

		// Token: 0x0600119D RID: 4509
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnLongDescription(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnLongDescription);

		// Token: 0x0600119E RID: 4510
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnMetadata(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnMetadata);

		// Token: 0x0600119F RID: 4511
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnChildren(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnChildren);

		// Token: 0x060011A0 RID: 4512
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnAdditionalPreviews(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnAdditionalPreviews);

		// Token: 0x060011A1 RID: 4513
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnTotalOnly(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bReturnTotalOnly);

		// Token: 0x060011A2 RID: 4514
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060011A3 RID: 4515
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetLanguage(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pchLanguage);

		// Token: 0x060011A4 RID: 4516
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds);

		// Token: 0x060011A5 RID: 4517
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetCloudFileNameFilter(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pMatchCloudFileName);

		// Token: 0x060011A6 RID: 4518
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetMatchAnyTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.I1)] bool bMatchAnyTag);

		// Token: 0x060011A7 RID: 4519
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetSearchText(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pSearchText);

		// Token: 0x060011A8 RID: 4520
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060011A9 RID: 4521
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddRequiredKeyValueTag(UGCQueryHandle_t handle, InteropHelp.UTF8StringHandle pKey, InteropHelp.UTF8StringHandle pValue);

		// Token: 0x060011AA RID: 4522
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds);

		// Token: 0x060011AB RID: 4523
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType);

		// Token: 0x060011AC RID: 4524
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060011AD RID: 4525
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemTitle(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchTitle);

		// Token: 0x060011AE RID: 4526
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemDescription(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchDescription);

		// Token: 0x060011AF RID: 4527
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemUpdateLanguage(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchLanguage);

		// Token: 0x060011B0 RID: 4528
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemMetadata(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchMetaData);

		// Token: 0x060011B1 RID: 4529
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility);

		// Token: 0x060011B2 RID: 4530
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemTags(UGCUpdateHandle_t updateHandle, IntPtr pTags);

		// Token: 0x060011B3 RID: 4531
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemContent(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszContentFolder);

		// Token: 0x060011B4 RID: 4532
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_SetItemPreview(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszPreviewFile);

		// Token: 0x060011B5 RID: 4533
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_RemoveItemKeyValueTags(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchKey);

		// Token: 0x060011B6 RID: 4534
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddItemKeyValueTag(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchKey, InteropHelp.UTF8StringHandle pchValue);

		// Token: 0x060011B7 RID: 4535
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddItemPreviewFile(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszPreviewFile, EItemPreviewType type);

		// Token: 0x060011B8 RID: 4536
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_AddItemPreviewVideo(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pszVideoID);

		// Token: 0x060011B9 RID: 4537
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, InteropHelp.UTF8StringHandle pszPreviewFile);

		// Token: 0x060011BA RID: 4538
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, InteropHelp.UTF8StringHandle pszVideoID);

		// Token: 0x060011BB RID: 4539
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_RemoveItemPreview(UGCUpdateHandle_t handle, uint index);

		// Token: 0x060011BC RID: 4540
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_SubmitItemUpdate(UGCUpdateHandle_t handle, InteropHelp.UTF8StringHandle pchChangeNote);

		// Token: 0x060011BD RID: 4541
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EItemUpdateStatus ISteamGameServerUGC_GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal);

		// Token: 0x060011BE RID: 4542
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_SetUserItemVote(PublishedFileId_t nPublishedFileID, [MarshalAs(UnmanagedType.I1)] bool bVoteUp);

		// Token: 0x060011BF RID: 4543
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_GetUserItemVote(PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C0 RID: 4544
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C1 RID: 4545
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C2 RID: 4546
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_SubscribeItem(PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C3 RID: 4547
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_UnsubscribeItem(PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C4 RID: 4548
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUGC_GetNumSubscribedItems();

		// Token: 0x060011C5 RID: 4549
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUGC_GetSubscribedItems([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x060011C6 RID: 4550
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUGC_GetItemState(PublishedFileId_t nPublishedFileID);

		// Token: 0x060011C7 RID: 4551
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, out uint punTimeStamp);

		// Token: 0x060011C8 RID: 4552
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal);

		// Token: 0x060011C9 RID: 4553
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_DownloadItem(PublishedFileId_t nPublishedFileID, [MarshalAs(UnmanagedType.I1)] bool bHighPriority);

		// Token: 0x060011CA RID: 4554
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUGC_BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, InteropHelp.UTF8StringHandle pszFolder);

		// Token: 0x060011CB RID: 4555
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUGC_SuspendDownloads([MarshalAs(UnmanagedType.I1)] bool bSuspend);

		// Token: 0x060011CC RID: 4556
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_StartPlaytimeTracking([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x060011CD RID: 4557
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_StopPlaytimeTracking([In] [Out] PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x060011CE RID: 4558
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_StopPlaytimeTrackingForAllItems();

		// Token: 0x060011CF RID: 4559
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID);

		// Token: 0x060011D0 RID: 4560
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUGC_RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID);

		// Token: 0x060011D1 RID: 4561
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetSecondsSinceAppActive();

		// Token: 0x060011D2 RID: 4562
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetSecondsSinceComputerActive();

		// Token: 0x060011D3 RID: 4563
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern EUniverse ISteamGameServerUtils_GetConnectedUniverse();

		// Token: 0x060011D4 RID: 4564
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetServerRealTime();

		// Token: 0x060011D5 RID: 4565
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamGameServerUtils_GetIPCountry();

		// Token: 0x060011D6 RID: 4566
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_GetImageSize(int iImage, out uint pnWidth, out uint pnHeight);

		// Token: 0x060011D7 RID: 4567
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize);

		// Token: 0x060011D8 RID: 4568
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_GetCSERIPPort(out uint unIP, out ushort usPort);

		// Token: 0x060011D9 RID: 4569
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte ISteamGameServerUtils_GetCurrentBatteryPower();

		// Token: 0x060011DA RID: 4570
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetAppID();

		// Token: 0x060011DB RID: 4571
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUtils_SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition);

		// Token: 0x060011DC RID: 4572
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed);

		// Token: 0x060011DD RID: 4573
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ESteamAPICallFailure ISteamGameServerUtils_GetAPICallFailureReason(SteamAPICall_t hSteamAPICall);

		// Token: 0x060011DE RID: 4574
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed);

		// Token: 0x060011DF RID: 4575
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetIPCCallCount();

		// Token: 0x060011E0 RID: 4576
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUtils_SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction);

		// Token: 0x060011E1 RID: 4577
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_IsOverlayEnabled();

		// Token: 0x060011E2 RID: 4578
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_BOverlayNeedsPresent();

		// Token: 0x060011E3 RID: 4579
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong ISteamGameServerUtils_CheckFileSignature(InteropHelp.UTF8StringHandle szFileName);

		// Token: 0x060011E4 RID: 4580
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, InteropHelp.UTF8StringHandle pchDescription, uint unCharMax, InteropHelp.UTF8StringHandle pchExistingText);

		// Token: 0x060011E5 RID: 4581
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ISteamGameServerUtils_GetEnteredGamepadTextLength();

		// Token: 0x060011E6 RID: 4582
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_GetEnteredGamepadTextInput(IntPtr pchText, uint cchText);

		// Token: 0x060011E7 RID: 4583
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ISteamGameServerUtils_GetSteamUILanguage();

		// Token: 0x060011E8 RID: 4584
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_IsSteamRunningInVR();

		// Token: 0x060011E9 RID: 4585
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUtils_SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset);

		// Token: 0x060011EA RID: 4586
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_IsSteamInBigPictureMode();

		// Token: 0x060011EB RID: 4587
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUtils_StartVRDashboard();

		// Token: 0x060011EC RID: 4588
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool ISteamGameServerUtils_IsVRHeadsetStreamingEnabled();

		// Token: 0x060011ED RID: 4589
		[DllImport("CSteamworks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ISteamGameServerUtils_SetVRHeadsetStreamingEnabled([MarshalAs(UnmanagedType.I1)] bool bEnabled);

		// Token: 0x040004EB RID: 1259
		internal const string NativeLibraryName = "CSteamworks";

		// Token: 0x040004EC RID: 1260
		internal const string NativeLibrary_SDKEncryptedAppTicket = "sdkencryptedappticket";
	}
}
