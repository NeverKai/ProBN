using System;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000861 RID: 2145
	internal class VikingSquadDifficulty : MonoBehaviour, ISquadSetup
	{
		// Token: 0x06003838 RID: 14392 RVA: 0x000F2994 File Offset: 0x000F0D94
		void ISquadSetup.SquadSetup(Squad squad)
		{
			this.difficultySettings = squad.faction.island.levelNode.diffiucltySettings;
			if (this.difficultySettings.vikingHealthMultipliers.Count > 0)
			{
				squad.onAgentSpawned += this.OnAgentSpawned;
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000F29E4 File Offset: 0x000F0DE4
		private void OnAgentSpawned(Agent agent)
		{
			VikingAgent component = agent.GetComponent<VikingAgent>();
			if (component)
			{
				float num;
				if (!this.difficultySettings.vikingHealthMultipliers.TryGetValue(component.type, out num))
				{
					num = 1f;
				}
				float maxHealth = agent.maxHealth;
				agent.maxHealth = (agent.health = agent.maxHealth * num);
			}
		}

		// Token: 0x0400264E RID: 9806
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("VikingSquadDifficulty", EVerbosity.Quiet, 0);

		// Token: 0x0400264F RID: 9807
		private CampaignDifficultySettings difficultySettings;
	}
}
