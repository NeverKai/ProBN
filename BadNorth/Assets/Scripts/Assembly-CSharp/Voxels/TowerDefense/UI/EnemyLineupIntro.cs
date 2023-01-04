using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000551 RID: 1361
	public class EnemyLineupIntro : MonoBehaviour, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x06002364 RID: 9060 RVA: 0x0006D6E0 File Offset: 0x0006BAE0
		private void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			EnemyLineupIntro.instance = this;
			foreach (VikingReference vikingReference in LevelStateObjectReferences.GetReferencedObjects<VikingReference>())
			{
				EnemyLineupIntro.VikingInfo vikingInfo = new EnemyLineupIntro.VikingInfo();
				AgentInfo agentInfo = UnityEngine.Object.Instantiate<AgentInfo>(this.introAgentExample, this.introAgentExample.transform.parent);
				agentInfo.image.sprite = vikingReference.sprite2;
				agentInfo.vikingReference = vikingReference;
				agentInfo.image.transform.localScale = Vector3.one * Mathf.Lerp(vikingReference.agent.scale, 1f, 0.5f);
				vikingInfo.intro = agentInfo;
				vikingInfo.vikingReference = vikingReference;
				this.vikingInfoList.Add(vikingInfo);
			}
			this.introAgentExample.gameObject.SetActive(false);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0006D7E4 File Offset: 0x0006BBE4
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.MaybeInitialize();
			foreach (EnemyLineupIntro.VikingInfo vikingInfo in this.vikingInfoList)
			{
				vikingInfo.kills = 0;
				if (island.levelNode.enemies.Contains(vikingInfo.vikingReference))
				{
					vikingInfo.onIsland = true;
					vikingInfo.intro.gameObject.SetActive(true && !island.levelNode.isStart);
					vikingInfo.wasSeen = vikingInfo.vikingReference.seen;
					vikingInfo.intro.image.gameObject.SetActive(vikingInfo.wasSeen);
					vikingInfo.intro.unseenImage.gameObject.SetActive(!vikingInfo.wasSeen);
				}
				else
				{
					vikingInfo.onIsland = false;
					vikingInfo.intro.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0006D8F8 File Offset: 0x0006BCF8
		public void RegisterKill(VikingReference vikingReference)
		{
			foreach (EnemyLineupIntro.VikingInfo vikingInfo in this.vikingInfoList)
			{
				if (vikingInfo.vikingReference == vikingReference)
				{
					vikingInfo.kills++;
				}
			}
		}

		// Token: 0x0400160B RID: 5643
		[SerializeField]
		private AgentInfo introAgentExample;

		// Token: 0x0400160C RID: 5644
		public List<EnemyLineupIntro.VikingInfo> vikingInfoList = new List<EnemyLineupIntro.VikingInfo>(8);

		// Token: 0x0400160D RID: 5645
		private bool initialized;

		// Token: 0x0400160E RID: 5646
		public static EnemyLineupIntro instance;

		// Token: 0x02000552 RID: 1362
		public class VikingInfo
		{
			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x06002368 RID: 9064 RVA: 0x0006D974 File Offset: 0x0006BD74
			public bool revealed
			{
				get
				{
					return !this.wasSeen && this.vikingReference.seen;
				}
			}

			// Token: 0x0400160F RID: 5647
			public AgentInfo intro;

			// Token: 0x04001610 RID: 5648
			public VikingReference vikingReference;

			// Token: 0x04001611 RID: 5649
			public int kills;

			// Token: 0x04001612 RID: 5650
			public bool onIsland;

			// Token: 0x04001613 RID: 5651
			public bool wasSeen;
		}
	}
}
