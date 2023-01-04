using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006F6 RID: 1782
	public class HeroTraitAssigner : CampaignComponent, Campaign.ICampaignCreator
	{
		// Token: 0x06002E47 RID: 11847 RVA: 0x000B3FE4 File Offset: 0x000B23E4
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			List<HeroDefinition> heroes = campaign.campaignSave.heroes;
			int numAssignedTraits = 0;
			int metaHeroIdx0;
			int metaHeroIdx;
			HeroTraitAssigner.GetMetaHeroIdx(campaign.campaignSave.levelStates, out metaHeroIdx0, out metaHeroIdx);
			yield return null;
			List<HeroUpgradeDefinition> unlockedStartingTraits = Profile.userSave.inventory.startingTraits;
			List<HeroUpgradeDefinition> traitDefs = (from def in ResourceList<HeroUpgradeDefinition>.list
			where def.typeEnum == HeroUpgradeTypeEnum.Trait
			select def).ToList<HeroUpgradeDefinition>();
			yield return null;
			foreach (HeroDefinition heroDefinition in heroes)
			{
				if (heroDefinition.traitUpgrade != null)
				{
					numAssignedTraits += ((!traitDefs.Remove(heroDefinition.traitUpgrade)) ? 0 : 1);
				}
			}
			Dictionary<HeroUpgradeDefinition, float> traitWeights = new Dictionary<HeroUpgradeDefinition, float>(traitDefs.Count);
			yield return null;
			traitDefs.ShuffleWeighted(traitWeights, (HeroUpgradeDefinition t) => HeroTraitAssigner.GetMetaRewardWeight(t, unlockedStartingTraits));
			numAssignedTraits += ((!HeroTraitAssigner.MaybeAssignMetaRewardTrait(heroes, traitDefs, unlockedStartingTraits, ref metaHeroIdx0, 1)) ? 0 : 1);
			numAssignedTraits += ((!HeroTraitAssigner.MaybeAssignMetaRewardTrait(heroes, traitDefs, unlockedStartingTraits, ref metaHeroIdx, 0)) ? 0 : 1);
			yield return null;
			traitDefs.ShuffleWeighted(traitWeights, (HeroUpgradeDefinition t) => HeroTraitAssigner.GetTraitWeight(t, unlockedStartingTraits.Count > 1));
			yield return null;
			int firstTraitable = 2;
			int maxTraits = Mathf.Min(heroes.Count - firstTraitable - 1, 7);
			List<int> traitableHeroes = new List<int>(heroes.Count);
			int j = firstTraitable;
			int count = heroes.Count;
			while (j < count)
			{
				traitableHeroes.Add(j);
				j++;
			}
			traitableHeroes.Remove(metaHeroIdx0);
			traitableHeroes.Remove(metaHeroIdx);
			yield return null;
			SmartShuffler<int> traitableHeroShuffle = new SmartShuffler<int>(traitableHeroes);
			int i = 0;
			while (numAssignedTraits < maxTraits)
			{
				int idx = traitableHeroShuffle.Get();
				heroes[idx].SetTrait(traitDefs[i % traitDefs.Count]);
				yield return null;
				i++;
				numAssignedTraits++;
			}
			yield break;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000B4000 File Offset: 0x000B2400
		private static float GetMetaRewardWeight(HeroUpgradeDefinition trait, List<HeroUpgradeDefinition> metaTraits)
		{
			if (metaTraits.Contains(trait))
			{
				return 0f;
			}
			switch (trait.unlockValue)
			{
			case HeroUpgradeDefinition.UnlockValue.Normal:
				return 1f;
			case HeroUpgradeDefinition.UnlockValue.High:
				return 0.4f;
			case HeroUpgradeDefinition.UnlockValue.Negative:
				return (metaTraits.Count >= 2) ? 0.5f : 0f;
			default:
				throw new NotImplementedException(string.Format("Unknown trait unlock value {0}", trait.unlockValue));
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000B4080 File Offset: 0x000B2480
		private static bool MaybeAssignMetaRewardTrait(List<HeroDefinition> heroes, List<HeroUpgradeDefinition> traitDefs, List<HeroUpgradeDefinition> unlockedStartingTraits, ref int heroIdx, int traitIdx)
		{
			if (HeroTraitAssigner.GetMetaRewardWeight(traitDefs[traitIdx], unlockedStartingTraits) > 0f)
			{
				HeroDefinition heroDefinition = heroes[heroIdx];
				heroDefinition.SetTrait(traitDefs[traitIdx]);
				heroDefinition.propertyBank.SetBool("MetaTrait", true);
				traitDefs.RemoveAt(traitIdx);
				return true;
			}
			heroIdx = -1;
			return false;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000B40DC File Offset: 0x000B24DC
		private static float GetTraitWeight(HeroUpgradeDefinition trait, bool allowNegative)
		{
			switch (trait.unlockValue)
			{
			case HeroUpgradeDefinition.UnlockValue.Normal:
				return 1f;
			case HeroUpgradeDefinition.UnlockValue.High:
				return 0.75f;
			case HeroUpgradeDefinition.UnlockValue.Negative:
				return (!allowNegative) ? 0f : 0.4f;
			default:
				throw new NotImplementedException(string.Format("Unknown trait unlock value {0}", trait.unlockValue));
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000B4144 File Offset: 0x000B2544
		private static void GetMetaHeroIdx(List<LevelState> levels, out int idx0, out int idx1)
		{
			idx0 = (idx1 = -1);
			foreach (LevelState levelState in levels)
			{
				if (levelState.metaReward && levelState.heroId >= 0)
				{
					if (idx0 >= 0)
					{
						idx1 = levelState.heroId;
						break;
					}
					idx0 = levelState.heroId;
				}
			}
		}

		// Token: 0x04001E97 RID: 7831
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroTraitAssigner", EVerbosity.Quiet, 100);

		// Token: 0x04001E98 RID: 7832
		private const int numTraits = 7;
	}
}
