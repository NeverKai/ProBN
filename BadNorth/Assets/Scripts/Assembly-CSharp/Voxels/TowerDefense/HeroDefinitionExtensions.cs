using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006EE RID: 1774
	public static class HeroDefinitionExtensions
	{
		// Token: 0x06002DEF RID: 11759 RVA: 0x000B2944 File Offset: 0x000B0D44
		static HeroDefinitionExtensions()
		{
			if (HeroDefinitionExtensions.action == null)
			{
				HeroDefinitionExtensions.action = new Func<int, int, bool>(HeroDefinitionExtensions.CheapestCostComparison);
			}
			HeroDefinitionExtensions.cheapestCostComparison = HeroDefinitionExtensions.action;
			if (HeroDefinitionExtensions.action1 == null)
			{
				HeroDefinitionExtensions.action1 = new Func<int, int, bool>(HeroDefinitionExtensions.MostExpensiveCostComparison);
			}
			HeroDefinitionExtensions.mostExpensiveCostComparison = HeroDefinitionExtensions.action1;
			HeroDefinitionExtensions.purchasableUpgrades = (from u in ResourceList<HeroUpgradeDefinition>.list
			where (from l in u.levels
			where l.cost > 0
			select l).Count<HeroUpgradeDefinition.Level>() > 0
			select u).ToList<HeroUpgradeDefinition>();
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B29B5 File Offset: 0x000B0DB5
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B29B8 File Offset: 0x000B0DB8
		public static int GetCheapestAvailableUpgradeCost(this HeroDefinition heroDef)
		{
			HeroUpgradeDefinition heroUpgradeDefinition;
			int num;
			int num2;
			return (!heroDef.GetCheapestAvailableUpgrade(out heroUpgradeDefinition, out num, out num2)) ? int.MaxValue : num2;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B29E4 File Offset: 0x000B0DE4
		public static bool GetCheapestAvailableUpgrade(this HeroDefinition heroDef, out HeroUpgradeDefinition upgrade, out int level)
		{
			int num;
			return heroDef.GetCheapestAvailableUpgrade(out upgrade, out level, out num);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000B29FB File Offset: 0x000B0DFB
		public static bool GetCheapestAvailableUpgrade(this HeroDefinition heroDef, out HeroUpgradeDefinition upgrade, out int level, out int cost)
		{
			cost = int.MaxValue;
			return heroDef.GetBestAvailableUpgrade(HeroDefinitionExtensions.cheapestCostComparison, out upgrade, out level, ref cost, int.MaxValue);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B2A18 File Offset: 0x000B0E18
		public static int GetMostExpensiveAvailableUpgradeCost(this HeroDefinition heroDef, int maxCost)
		{
			HeroUpgradeDefinition heroUpgradeDefinition;
			int num;
			int num2;
			return (!heroDef.GetMostExpensiveAvailableUpgradeCost(out heroUpgradeDefinition, out num, out num2, maxCost)) ? -1 : num2;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000B2A40 File Offset: 0x000B0E40
		public static bool GetMostExpensiveAvailableUpgradeCost(this HeroDefinition heroDef, out HeroUpgradeDefinition upgrade, out int level, int maxCost)
		{
			int num;
			return heroDef.GetMostExpensiveAvailableUpgradeCost(out upgrade, out level, out num, maxCost);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000B2A58 File Offset: 0x000B0E58
		public static bool GetMostExpensiveAvailableUpgradeCost(this HeroDefinition heroDef, out HeroUpgradeDefinition upgrade, out int level, out int cost, int maxCost)
		{
			cost = int.MinValue;
			return heroDef.GetBestAvailableUpgrade(HeroDefinitionExtensions.mostExpensiveCostComparison, out upgrade, out level, ref cost, maxCost);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000B2A74 File Offset: 0x000B0E74
		private static bool GetBestAvailableUpgrade(this HeroDefinition heroDef, Func<int, int, bool> costComparison, out HeroUpgradeDefinition upgrade, out int level, ref int cost, int maxCost)
		{
			upgrade = null;
			level = -1;
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in HeroDefinitionExtensions.purchasableUpgrades)
			{
				int num = heroDef.GetUpgradeLevel(heroUpgradeDefinition) + 1;
				if (heroUpgradeDefinition.AvailableFor(heroDef, num))
				{
					int upgradeCost = heroDef.GetUpgradeCost(heroUpgradeDefinition, num);
					if (upgradeCost <= maxCost && costComparison(upgradeCost, cost))
					{
						cost = upgradeCost;
						upgrade = heroUpgradeDefinition;
						level = num;
					}
				}
			}
			return upgrade != null;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000B2B18 File Offset: 0x000B0F18
		private static bool CheapestCostComparison(int nextCost, int cheapest)
		{
			return nextCost > 0 && nextCost < cheapest;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000B2B28 File Offset: 0x000B0F28
		private static bool MostExpensiveCostComparison(int nextCost, int cheapest)
		{
			return nextCost > 0 && nextCost > cheapest;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000B2B38 File Offset: 0x000B0F38
		public static Color GetUIBannerColor(this HeroDefinition heroDef)
		{
			float h;
			float num;
			float v;
			Color.RGBToHSV(heroDef.color, out h, out num, out v);
			num *= 0.5f;
			return Color.HSVToRGB(h, num, v);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000B2B68 File Offset: 0x000B0F68
		public static Color GetComplimentaryUIBannerColor(this HeroDefinition heroDef)
		{
			Color uibannerColor = heroDef.GetUIBannerColor();
			float num;
			float s;
			float v;
			Color.RGBToHSV(uibannerColor, out num, out s, out v);
			num = (num + 0.5f) % 1f;
			return Color.HSVToRGB(num, s, v);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000B2BA0 File Offset: 0x000B0FA0
		public static string GetClassDisplayTerm(this HeroDefinition heroDef)
		{
			SerializableHeroUpgrade classUpgrade = heroDef.classUpgrade;
			if (classUpgrade != null)
			{
				HeroClassUpgradeDefinition heroClassUpgradeDefinition = classUpgrade.definition as HeroClassUpgradeDefinition;
				return heroClassUpgradeDefinition.GetSquadLevelTerm(heroDef.squadLevel);
			}
			return "UPGRADES/GENERIC/LEVEL_CLASS/MILITIA";
		}

		// Token: 0x04001E6F RID: 7791
		private static Func<int, int, bool> cheapestCostComparison;

		// Token: 0x04001E70 RID: 7792
		private static Func<int, int, bool> mostExpensiveCostComparison;

		// Token: 0x04001E71 RID: 7793
		private static List<HeroUpgradeDefinition> purchasableUpgrades;

		// Token: 0x04001E72 RID: 7794
		[CompilerGenerated]
		private static Func<int, int, bool> action;

		// Token: 0x04001E73 RID: 7795
		[CompilerGenerated]
		private static Func<int, int, bool> action1;
	}
}
