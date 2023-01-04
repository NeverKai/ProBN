using System;

namespace Steamworks
{
	// Token: 0x02000203 RID: 515
	public static class SteamGameServer
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x00021528 File Offset: 0x0001F928
		public static bool InitGameServer(uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId_t nGameAppId, string pchVersionString)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				result = NativeMethods.ISteamGameServer_InitGameServer(unIP, usGamePort, usQueryPort, unFlags, nGameAppId, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00021574 File Offset: 0x0001F974
		public static void SetProduct(string pszProduct)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszProduct))
			{
				NativeMethods.ISteamGameServer_SetProduct(utf8StringHandle);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000215B8 File Offset: 0x0001F9B8
		public static void SetGameDescription(string pszGameDescription)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszGameDescription))
			{
				NativeMethods.ISteamGameServer_SetGameDescription(utf8StringHandle);
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000215FC File Offset: 0x0001F9FC
		public static void SetModDir(string pszModDir)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszModDir))
			{
				NativeMethods.ISteamGameServer_SetModDir(utf8StringHandle);
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00021640 File Offset: 0x0001FA40
		public static void SetDedicatedServer(bool bDedicated)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetDedicatedServer(bDedicated);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00021650 File Offset: 0x0001FA50
		public static void LogOn(string pszToken)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszToken))
			{
				NativeMethods.ISteamGameServer_LogOn(utf8StringHandle);
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00021694 File Offset: 0x0001FA94
		public static void LogOnAnonymous()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_LogOnAnonymous();
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000216A0 File Offset: 0x0001FAA0
		public static void LogOff()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_LogOff();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000216AC File Offset: 0x0001FAAC
		public static bool BLoggedOn()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BLoggedOn();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000216B8 File Offset: 0x0001FAB8
		public static bool BSecure()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BSecure();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000216C4 File Offset: 0x0001FAC4
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (CSteamID)NativeMethods.ISteamGameServer_GetSteamID();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000216D5 File Offset: 0x0001FAD5
		public static bool WasRestartRequested()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_WasRestartRequested();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000216E1 File Offset: 0x0001FAE1
		public static void SetMaxPlayerCount(int cPlayersMax)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetMaxPlayerCount(cPlayersMax);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000216EE File Offset: 0x0001FAEE
		public static void SetBotPlayerCount(int cBotplayers)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetBotPlayerCount(cBotplayers);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000216FC File Offset: 0x0001FAFC
		public static void SetServerName(string pszServerName)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszServerName))
			{
				NativeMethods.ISteamGameServer_SetServerName(utf8StringHandle);
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00021740 File Offset: 0x0001FB40
		public static void SetMapName(string pszMapName)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszMapName))
			{
				NativeMethods.ISteamGameServer_SetMapName(utf8StringHandle);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00021784 File Offset: 0x0001FB84
		public static void SetPasswordProtected(bool bPasswordProtected)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetPasswordProtected(bPasswordProtected);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00021791 File Offset: 0x0001FB91
		public static void SetSpectatorPort(ushort unSpectatorPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetSpectatorPort(unSpectatorPort);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000217A0 File Offset: 0x0001FBA0
		public static void SetSpectatorServerName(string pszSpectatorServerName)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszSpectatorServerName))
			{
				NativeMethods.ISteamGameServer_SetSpectatorServerName(utf8StringHandle);
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000217E4 File Offset: 0x0001FBE4
		public static void ClearAllKeyValues()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_ClearAllKeyValues();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000217F0 File Offset: 0x0001FBF0
		public static void SetKeyValue(string pKey, string pValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pValue))
				{
					NativeMethods.ISteamGameServer_SetKeyValue(utf8StringHandle, utf8StringHandle2);
				}
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00021858 File Offset: 0x0001FC58
		public static void SetGameTags(string pchGameTags)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchGameTags))
			{
				NativeMethods.ISteamGameServer_SetGameTags(utf8StringHandle);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002189C File Offset: 0x0001FC9C
		public static void SetGameData(string pchGameData)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchGameData))
			{
				NativeMethods.ISteamGameServer_SetGameData(utf8StringHandle);
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000218E0 File Offset: 0x0001FCE0
		public static void SetRegion(string pszRegion)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszRegion))
			{
				NativeMethods.ISteamGameServer_SetRegion(utf8StringHandle);
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00021924 File Offset: 0x0001FD24
		public static bool SendUserConnectAndAuthenticate(uint unIPClient, byte[] pvAuthBlob, uint cubAuthBlobSize, out CSteamID pSteamIDUser)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_SendUserConnectAndAuthenticate(unIPClient, pvAuthBlob, cubAuthBlobSize, out pSteamIDUser);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00021934 File Offset: 0x0001FD34
		public static CSteamID CreateUnauthenticatedUserConnection()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (CSteamID)NativeMethods.ISteamGameServer_CreateUnauthenticatedUserConnection();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00021945 File Offset: 0x0001FD45
		public static void SendUserDisconnect(CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SendUserDisconnect(steamIDUser);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00021954 File Offset: 0x0001FD54
		public static bool BUpdateUserData(CSteamID steamIDUser, string pchPlayerName, uint uScore)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPlayerName))
			{
				result = NativeMethods.ISteamGameServer_BUpdateUserData(steamIDUser, utf8StringHandle, uScore);
			}
			return result;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002199C File Offset: 0x0001FD9C
		public static HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HAuthTicket)NativeMethods.ISteamGameServer_GetAuthSessionTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000219B0 File Offset: 0x0001FDB0
		public static EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BeginAuthSession(pAuthTicket, cbAuthTicket, steamID);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000219BF File Offset: 0x0001FDBF
		public static void EndAuthSession(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_EndAuthSession(steamID);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000219CC File Offset: 0x0001FDCC
		public static void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_CancelAuthTicket(hAuthTicket);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000219D9 File Offset: 0x0001FDD9
		public static EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_UserHasLicenseForApp(steamID, appID);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000219E7 File Offset: 0x0001FDE7
		public static bool RequestUserGroupStatus(CSteamID steamIDUser, CSteamID steamIDGroup)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_RequestUserGroupStatus(steamIDUser, steamIDGroup);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000219F5 File Offset: 0x0001FDF5
		public static void GetGameplayStats()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_GetGameplayStats();
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00021A01 File Offset: 0x0001FE01
		public static SteamAPICall_t GetServerReputation()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_GetServerReputation();
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00021A12 File Offset: 0x0001FE12
		public static uint GetPublicIP()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_GetPublicIP();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00021A1E File Offset: 0x0001FE1E
		public static bool HandleIncomingPacket(byte[] pData, int cbData, uint srcIP, ushort srcPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_HandleIncomingPacket(pData, cbData, srcIP, srcPort);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00021A2E File Offset: 0x0001FE2E
		public static int GetNextOutgoingPacket(byte[] pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_GetNextOutgoingPacket(pOut, cbMaxOut, out pNetAdr, out pPort);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00021A3E File Offset: 0x0001FE3E
		public static void EnableHeartbeats(bool bActive)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_EnableHeartbeats(bActive);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00021A4B File Offset: 0x0001FE4B
		public static void SetHeartbeatInterval(int iHeartbeatInterval)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetHeartbeatInterval(iHeartbeatInterval);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00021A58 File Offset: 0x0001FE58
		public static void ForceHeartbeat()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_ForceHeartbeat();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00021A64 File Offset: 0x0001FE64
		public static SteamAPICall_t AssociateWithClan(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_AssociateWithClan(steamIDClan);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00021A76 File Offset: 0x0001FE76
		public static SteamAPICall_t ComputeNewPlayerCompatibility(CSteamID steamIDNewPlayer)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_ComputeNewPlayerCompatibility(steamIDNewPlayer);
		}
	}
}
