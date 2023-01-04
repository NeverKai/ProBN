using System;
using System.Collections.Generic;
using CS.Platform.Utils.Data;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000078 RID: 120
	public static class DLC
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x00015AC5 File Offset: 0x00013EC5
		public static void DLCSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/DLCData";
			}
			DLC._dlcInfomation = (Resources.Load(databaseLocation) as PlatformDLCDatabase);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015AE9 File Offset: 0x00013EE9
		public static List<string> DLCPS4Index(string key)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.PS4Index(key);
			}
			return null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00015B08 File Offset: 0x00013F08
		public static uint DLCSteamAPI(string key)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.SteamAPI(key);
			}
			return (uint)AppId_t.Invalid;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00015B30 File Offset: 0x00013F30
		public static string DLCOculusAPI(string key)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.OculusAPI(key);
			}
			return null;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00015B4F File Offset: 0x00013F4F
		public static string DLCName(string key)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.Name(key);
			}
			return null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00015B6E File Offset: 0x00013F6E
		public static string DLCDescription(string key)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.Description(key);
			}
			return null;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00015B8D File Offset: 0x00013F8D
		public static long DLCDiscrodAPI(string dlcKey)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.DiscrodAPI(dlcKey);
			}
			return -1L;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00015BAD File Offset: 0x00013FAD
		public static ulong DLCGOGAPI(string dlcKey)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.GogAPI(dlcKey);
			}
			return 0UL;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00015BCD File Offset: 0x00013FCD
		public static string DLCGOGURL(string dlcKey)
		{
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.GogURL(dlcKey);
			}
			return string.Empty;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00015BF0 File Offset: 0x00013FF0
		public static string DLCXboxTitle(string dlcKey, out bool needsMount)
		{
			needsMount = false;
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.XboxTitle(dlcKey, out needsMount);
			}
			return null;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00015C13 File Offset: 0x00014013
		public static bool DLCXboxMount(string titleID)
		{
			return DLC._dlcInfomation != null && DLC._dlcInfomation.XboxMount(titleID);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015C32 File Offset: 0x00014032
		public static int DLCSwitchIndex(string dlcKey, out bool needsMount)
		{
			needsMount = false;
			if (DLC._dlcInfomation != null)
			{
				return DLC._dlcInfomation.SwitchIndex(dlcKey, out needsMount);
			}
			return -1;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00015C55 File Offset: 0x00014055
		public static bool DLCSwitchMount(int indexID)
		{
			return DLC._dlcInfomation != null && DLC._dlcInfomation.SwitchMount(indexID);
		}

		// Token: 0x0400022C RID: 556
		private static PlatformDLCDatabase _dlcInfomation;
	}
}
