using System;
using I2.Loc;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000708 RID: 1800
	internal static class IslandNameTerms
	{
		// Token: 0x04001EC3 RID: 7875
		public static string[] names = LocalizationManager.GetTermsList("ISLAND_NAMES").ToArray();

		// Token: 0x04001EC4 RID: 7876
		public static string[] genericNames = LocalizationManager.GetTermsList("ISLAND_NAMES/GENERIC").ToArray();
	}
}
