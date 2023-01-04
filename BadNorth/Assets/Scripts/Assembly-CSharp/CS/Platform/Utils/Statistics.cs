using System;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000077 RID: 119
	public static class Statistics
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00015897 File Offset: 0x00013C97
		public static void StatisticsSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/StatisticsData";
			}
			Statistics._statisticsInfomation = (Resources.Load(databaseLocation) as PlatformStatisticsDatabase);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000158BB File Offset: 0x00013CBB
		public static string StatisticName(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.Name(key);
			}
			return null;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000158DA File Offset: 0x00013CDA
		public static string StatisticDescription(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.Description(key);
			}
			return null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000158F9 File Offset: 0x00013CF9
		public static string StatisticSteamAPI(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.SteamAPI(key);
			}
			return null;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015918 File Offset: 0x00013D18
		public static bool StatisticAchievementLinked(string key)
		{
			return Statistics._statisticsInfomation != null && Statistics._statisticsInfomation.AchievementLinked(key);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015937 File Offset: 0x00013D37
		public static string StatisticAchievementLink(string key, int index)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.AchievementLink(key, index);
			}
			return null;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015957 File Offset: 0x00013D57
		public static float StatisticAchievementUnlockAt(string key, int index)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.AchievementUnlock(key, index);
			}
			return 0f;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001597B File Offset: 0x00013D7B
		public static string StatisticAchievementUnlockCheck(string key, float value, int index)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.AchievementUnlockCheck(key, value, index);
			}
			return null;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001599C File Offset: 0x00013D9C
		public static void StatisticAchievementUnlock(string key, float value, Action<string> unlockCall)
		{
			if (unlockCall != null && Statistics._statisticsInfomation != null)
			{
				Statistics._statisticsInfomation.AchievementUnlock(key, value, unlockCall);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000159C2 File Offset: 0x00013DC2
		public static void StatisticAchievementUpdate(string key, float value, Action<string, float, float> update)
		{
			if (update != null && Statistics._statisticsInfomation != null)
			{
				Statistics._statisticsInfomation.AchievementUpdate(key, value, update);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000159E8 File Offset: 0x00013DE8
		public static string[] StatisticList()
		{
			return (!(Statistics._statisticsInfomation != null)) ? null : Statistics._statisticsInfomation.Keys;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015A0A File Offset: 0x00013E0A
		public static string[] StatisticAllXbox()
		{
			return (!(Statistics._statisticsInfomation != null)) ? null : Statistics._statisticsInfomation.XboxAPIs;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015A2C File Offset: 0x00013E2C
		public static string StatisticXboxAPI(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.XboxAPI(key);
			}
			return null;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015A4B File Offset: 0x00013E4B
		public static string StatisticsSaveFile()
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.SystemStatsStorageLocation;
			}
			return null;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015A69 File Offset: 0x00013E69
		public static int StatisticsSaveEstSize()
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.EstFileSize;
			}
			return 0;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015A87 File Offset: 0x00013E87
		public static PlatformStatisticsDatabase.StatsType StatisticsType(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.Type(key);
			}
			return PlatformStatisticsDatabase.StatsType.FLOAT;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015AA6 File Offset: 0x00013EA6
		public static string StatisticGOGAPI(string key)
		{
			if (Statistics._statisticsInfomation != null)
			{
				return Statistics._statisticsInfomation.GogAPI(key);
			}
			return null;
		}

		// Token: 0x0400022B RID: 555
		private static PlatformStatisticsDatabase _statisticsInfomation;
	}
}
