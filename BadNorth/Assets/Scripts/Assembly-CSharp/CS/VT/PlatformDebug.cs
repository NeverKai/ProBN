using System;
using CS.Platform;
using CS.Platform.Utils;

namespace CS.VT
{
	// Token: 0x02000389 RID: 905
	public static class PlatformDebug
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0002A69C File Offset: 0x00028A9C
		public static string ListCompleteAchievements()
		{
			string text = "Complete:\n";
			string[] array = Acheivements.AcheivementKeys();
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"-",
						array[i],
						": ",
						BasePlatformManager.Instance.IsAchievementUnlocked(array[i]),
						"\n"
					});
				}
			}
			return text;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0002A718 File Offset: 0x00028B18
		public static string ListStats()
		{
			string text = "Values:\n";
			string[] array = Statistics.StatisticList();
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"-",
						array[i],
						": ",
						BasePlatformManager.Instance.GetStatistic(array[i]),
						"\n"
					});
				}
			}
			return text;
		}
	}
}
