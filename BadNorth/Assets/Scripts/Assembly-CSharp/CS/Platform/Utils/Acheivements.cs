using System;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000076 RID: 118
	public static class Acheivements
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0001575E File Offset: 0x00013B5E
		public static void AcheivementSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/AchievementData";
			}
			Acheivements._achievementInfomation = (Resources.Load(databaseLocation) as PlatformAchievementDatabase);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015782 File Offset: 0x00013B82
		public static int AcheivementPS4Index(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.PS4Index(key);
			}
			return -1;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000157A1 File Offset: 0x00013BA1
		public static string AcheivementSteamAPI(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.SteamAPI(key);
			}
			return null;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000157C0 File Offset: 0x00013BC0
		public static string AcheivementOculusAPI(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.OculusAPI(key);
			}
			return null;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000157DF File Offset: 0x00013BDF
		public static string AcheivementXboxAPI(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.XboxAPI(key);
			}
			return null;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000157FE File Offset: 0x00013BFE
		public static string AcheivementName(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.Name(key);
			}
			return null;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001581D File Offset: 0x00013C1D
		public static string AcheivementDescription(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.Description(key);
			}
			return null;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001583C File Offset: 0x00013C3C
		public static int AcheivementLargestPS4()
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.LargesPS4();
			}
			return 0;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001585A File Offset: 0x00013C5A
		public static string[] AcheivementKeys()
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.AllKeys();
			}
			return null;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015878 File Offset: 0x00013C78
		public static string AcheivementGOGAPI(string key)
		{
			if (Acheivements._achievementInfomation != null)
			{
				return Acheivements._achievementInfomation.GOGAPI(key);
			}
			return null;
		}

		// Token: 0x0400022A RID: 554
		private static PlatformAchievementDatabase _achievementInfomation;
	}
}
