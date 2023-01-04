using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006E6 RID: 1766
	public static class DifficultyFunctions
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x000ADE5C File Offset: 0x000AC25C
		public static string GetLocTerm(this Difficulty difficulty)
		{
			switch (difficulty)
			{
			case Difficulty.None:
				return "UI/COMMON/ERROR";
			case Difficulty.Easy:
				return "SETTINGS/DIFFICULTY/EASY";
			case Difficulty.Normal:
				return "SETTINGS/DIFFICULTY/NORMAL";
			case Difficulty.Hard:
				return "SETTINGS/DIFFICULTY/HARD";
			case Difficulty.VeryHard:
				return "SETTINGS/DIFFICULTY/VERY_HARD";
			default:
				throw new NotImplementedException(string.Format("Unknown Difficulty {0}", difficulty));
			}
		}
	}
}
