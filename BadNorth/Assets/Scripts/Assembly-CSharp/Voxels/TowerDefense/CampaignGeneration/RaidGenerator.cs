using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000724 RID: 1828
	public class RaidGenerator : MonoBehaviour, Campaign.ICampaignCreator
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x000C0878 File Offset: 0x000BEC78
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			UnityEngine.Random.InitState(campaign.seed);
			CampaignSave campaignSave = campaign.campaignSave;
			List<LevelState> levels = campaignSave.levelStates;
			int num = levels.Count;
			CampaignDifficultySettings settings = campaign.GetDifficultySettings();
			for (int i = 0; i < num; i++)
			{
				LevelState level = levels[i];
				Node node = protoCampaign.nodes[i];
				float fraction = campaignSave.GetLevelFraction(level);
				int sStart = (int)level.stepsFromStart;
				int sEnd = (int)level.stepsFromEnd;
				level.relativeDifficulty = (byte)Mathf.RoundToInt(Mathf.Lerp(0f, 255f, node.difficulty));
				level.wavesCount = (byte)Mathf.RoundToInt(settings.waves.Sample(sStart, sEnd, fraction, node.difficulty));
				level.bountyPerWave = (short)Mathf.RoundToInt(settings.bounty.Sample(sStart, sEnd, fraction, node.difficulty));
				node.enemyTypes = (byte)Mathf.RoundToInt(settings.enemyTypes.Sample(sStart, sEnd, fraction, node.difficulty));
				if (node.reward == RewardType.Checkpoint || node.reward == RewardType.Eldorado)
				{
					LevelState levelState = level;
					levelState.wavesCount += 1;
					node.enemyTypes += 1;
				}
				Vector2 raidSpacing = settings.waveSpacing.SampleMinMax(sStart, sEnd, fraction);
				level.minWaveSpacing = (byte)Mathf.RoundToInt(raidSpacing.x);
				level.maxWaveSpacing = (byte)Mathf.RoundToInt(raidSpacing.y);
				protoCampaign.nodes[i] = node;
				yield return null;
			}
			yield break;
		}
	}
}
