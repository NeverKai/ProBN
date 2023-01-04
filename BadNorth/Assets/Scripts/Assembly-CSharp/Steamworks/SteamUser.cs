using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000216 RID: 534
	public static class SteamUser
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x00025BA4 File Offset: 0x00023FA4
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamUser)NativeMethods.ISteamUser_GetHSteamUser();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00025BB5 File Offset: 0x00023FB5
		public static bool BLoggedOn()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BLoggedOn();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00025BC1 File Offset: 0x00023FC1
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamUser_GetSteamID();
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00025BD2 File Offset: 0x00023FD2
		public static int InitiateGameConnection(byte[] pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, bool bSecure)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_InitiateGameConnection(pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00025BE6 File Offset: 0x00023FE6
		public static void TerminateGameConnection(uint unIPServer, ushort usPortServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_TerminateGameConnection(unIPServer, usPortServer);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00025BF4 File Offset: 0x00023FF4
		public static void TrackAppUsageEvent(CGameID gameID, int eAppUsageEvent, string pchExtraInfo = "")
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchExtraInfo))
			{
				NativeMethods.ISteamUser_TrackAppUsageEvent(gameID, eAppUsageEvent, utf8StringHandle);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00025C38 File Offset: 0x00024038
		public static bool GetUserDataFolder(out string pchBuffer, int cubBuffer)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubBuffer);
			bool flag = NativeMethods.ISteamUser_GetUserDataFolder(intPtr, cubBuffer);
			pchBuffer = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00025C74 File Offset: 0x00024074
		public static void StartVoiceRecording()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_StartVoiceRecording();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00025C80 File Offset: 0x00024080
		public static void StopVoiceRecording()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_StopVoiceRecording();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00025C8C File Offset: 0x0002408C
		public static EVoiceResult GetAvailableVoice(out uint pcbCompressed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetAvailableVoice(out pcbCompressed, IntPtr.Zero, 0U);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00025CA0 File Offset: 0x000240A0
		public static EVoiceResult GetVoice(bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetVoice(bWantCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, false, IntPtr.Zero, 0U, IntPtr.Zero, 0U);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00025CC8 File Offset: 0x000240C8
		public static EVoiceResult DecompressVoice(byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_DecompressVoice(pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00025CDC File Offset: 0x000240DC
		public static uint GetVoiceOptimalSampleRate()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetVoiceOptimalSampleRate();
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00025CE8 File Offset: 0x000240E8
		public static HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return (HAuthTicket)NativeMethods.ISteamUser_GetAuthSessionTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00025CFC File Offset: 0x000240FC
		public static EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BeginAuthSession(pAuthTicket, cbAuthTicket, steamID);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00025D0B File Offset: 0x0002410B
		public static void EndAuthSession(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_EndAuthSession(steamID);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00025D18 File Offset: 0x00024118
		public static void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_CancelAuthTicket(hAuthTicket);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00025D25 File Offset: 0x00024125
		public static EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_UserHasLicenseForApp(steamID, appID);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00025D33 File Offset: 0x00024133
		public static bool BIsBehindNAT()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsBehindNAT();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00025D3F File Offset: 0x0002413F
		public static void AdvertiseGame(CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_AdvertiseGame(steamIDGameServer, unIPServer, usPortServer);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00025D4E File Offset: 0x0002414E
		public static SteamAPICall_t RequestEncryptedAppTicket(byte[] pDataToInclude, int cbDataToInclude)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUser_RequestEncryptedAppTicket(pDataToInclude, cbDataToInclude);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00025D61 File Offset: 0x00024161
		public static bool GetEncryptedAppTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetEncryptedAppTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00025D70 File Offset: 0x00024170
		public static int GetGameBadgeLevel(int nSeries, bool bFoil)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetGameBadgeLevel(nSeries, bFoil);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00025D7E File Offset: 0x0002417E
		public static int GetPlayerSteamLevel()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetPlayerSteamLevel();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00025D8C File Offset: 0x0002418C
		public static SteamAPICall_t RequestStoreAuthURL(string pchRedirectURL)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchRedirectURL))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUser_RequestStoreAuthURL(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00025DD4 File Offset: 0x000241D4
		public static bool BIsPhoneVerified()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneVerified();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00025DE0 File Offset: 0x000241E0
		public static bool BIsTwoFactorEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsTwoFactorEnabled();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00025DEC File Offset: 0x000241EC
		public static bool BIsPhoneIdentifying()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneIdentifying();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00025DF8 File Offset: 0x000241F8
		public static bool BIsPhoneRequiringVerification()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneRequiringVerification();
		}
	}
}
