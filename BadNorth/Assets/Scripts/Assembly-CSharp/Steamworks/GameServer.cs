using System;

namespace Steamworks
{
	// Token: 0x02000358 RID: 856
	public static class GameServer
	{
		// Token: 0x060012B5 RID: 4789 RVA: 0x00027B58 File Offset: 0x00025F58
		public static bool Init(uint unIP, ushort usSteamPort, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString)
		{
			InteropHelp.TestIfPlatformSupported();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				result = NativeMethods.SteamGameServer_Init(unIP, usSteamPort, usGamePort, usQueryPort, eServerMode, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00027BA4 File Offset: 0x00025FA4
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_Shutdown();
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00027BB0 File Offset: 0x00025FB0
		public static void RunCallbacks()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_RunCallbacks();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00027BBC File Offset: 0x00025FBC
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00027BC8 File Offset: 0x00025FC8
		public static bool BSecure()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamGameServer_BSecure();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00027BD4 File Offset: 0x00025FD4
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfPlatformSupported();
			return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00027BE5 File Offset: 0x00025FE5
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00027BF6 File Offset: 0x00025FF6
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
