using System;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000754 RID: 1876
	internal class EnglishSquadDifficulty : MonoBehaviour, ISquadSetup
	{
		// Token: 0x06003104 RID: 12548 RVA: 0x000CA0B8 File Offset: 0x000C84B8
		void ISquadSetup.SquadSetup(Squad squad)
		{
			EnglishSquad exists = squad as EnglishSquad;
			if (!exists)
			{
				return;
			}
			CampaignDifficultySettings diffiucltySettings = squad.faction.island.levelNode.diffiucltySettings;
			this.healthMultiplier = diffiucltySettings.englishHealthModifier;
			if (this.healthMultiplier != 1f)
			{
				squad.onAgentSpawned += this.OnAgentSpawned;
			}
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000CA11C File Offset: 0x000C851C
		private void OnAgentSpawned(Agent agent)
		{
			float maxHealth = agent.maxHealth;
			agent.maxHealth = (agent.health = agent.maxHealth * this.healthMultiplier);
		}

		// Token: 0x040020BD RID: 8381
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("EnglishSquadDifficulty", EVerbosity.Quiet, 0);

		// Token: 0x040020BE RID: 8382
		private float healthMultiplier = 1f;
	}
}
