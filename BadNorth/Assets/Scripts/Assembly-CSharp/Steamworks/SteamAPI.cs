using System;

namespace Steamworks
{
	// Token: 0x02000357 RID: 855
	public static class SteamAPI
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x00027AD3 File Offset: 0x00025ED3
		public static bool InitSafe()
		{
			return SteamAPI.Init();
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00027ADA File Offset: 0x00025EDA
		public static bool Init()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_Init();
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00027AE6 File Offset: 0x00025EE6
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_Shutdown();
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00027AF2 File Offset: 0x00025EF2
		public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00027AFF File Offset: 0x00025EFF
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00027B0B File Offset: 0x00025F0B
		public static void RunCallbacks()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_RunCallbacks();
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00027B17 File Offset: 0x00025F17
		public static bool IsSteamRunning()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_IsSteamRunning();
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00027B23 File Offset: 0x00025F23
		public static HSteamUser GetHSteamUserCurrent()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.Steam_GetHSteamUserCurrent();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00027B34 File Offset: 0x00025F34
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00027B45 File Offset: 0x00025F45
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
