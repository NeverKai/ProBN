using System;
using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200054F RID: 1359
	public class EnemyLineup : MonoBehaviour
	{
		// Token: 0x0600235E RID: 9054 RVA: 0x0006D1B0 File Offset: 0x0006B5B0
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			foreach (VikingReference vikingReference in LevelStateObjectReferences.GetReferencedObjects<VikingReference>())
			{
				AgentInfo agentInfo = UnityEngine.Object.Instantiate<AgentInfo>(this.outroAgentExample, this.outroAgentExample.transform.parent);
				agentInfo.image.sprite = vikingReference.sprite2;
				agentInfo.vikingReference = vikingReference;
				agentInfo.image.transform.localScale = Vector3.one * Mathf.Lerp(vikingReference.agent.scale, 1f, 0.5f);
				this.vikingInfoList.Add(agentInfo);
			}
			this.outroAgentExample.gameObject.SetActive(false);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0006D29C File Offset: 0x0006B69C
		public void Prepare()
		{
			this.MaybeInitialize();
			for (int i = 0; i < this.vikingInfoList.Count; i++)
			{
				AgentInfo agentInfo = this.vikingInfoList[i];
				EnemyLineupIntro.VikingInfo vikingInfo = EnemyLineupIntro.instance.vikingInfoList[i];
				if (vikingInfo.onIsland)
				{
					agentInfo.gameObject.SetActive(true);
					agentInfo.text.text = IntStringCache.GetClean(vikingInfo.kills);
					agentInfo.GetComponent<Animator>().Play((!vikingInfo.wasSeen) ? EnemyLineup.unseen : EnemyLineup.seen);
					Transform transform = agentInfo.transform.Find("Blood");
					int num = (!Profile.userSettings.showBlood) ? -1 : UnityEngine.Random.Range(0, transform.childCount);
					transform.transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.value * 360f);
					for (int j = 0; j < transform.childCount; j++)
					{
						transform.GetChild(j).gameObject.SetActive(j == num);
					}
				}
				else
				{
					agentInfo.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0006D3E0 File Offset: 0x0006B7E0
		public IEnumerator<object> EndOfLevelRoutine()
		{
			for (float t = 0f; t < 0.3f; t += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			for (int i = 0; i < this.vikingInfoList.Count; i++)
			{
				AgentInfo thisInfo = this.vikingInfoList[i];
				EnemyLineupIntro.VikingInfo statsInfo = EnemyLineupIntro.instance.vikingInfoList[i];
				if (thisInfo.gameObject.activeSelf)
				{
					Animator animator = thisInfo.GetComponent<Animator>();
					if (statsInfo.revealed)
					{
						animator.Play(EnemyLineup.reveal);
						FabricWrapper.PostEvent(EnemyLineup.revealAudio);
						for (float t2 = 0f; t2 < 0.6f; t2 += Time.unscaledDeltaTime)
						{
							yield return null;
						}
					}
					animator.Play(EnemyLineup.drop);
					FabricWrapper.PostEvent(EnemyLineup.dropAudio);
					for (float t3 = 0f; t3 < 0.6f; t3 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x04001602 RID: 5634
		[SerializeField]
		private AgentInfo outroAgentExample;

		// Token: 0x04001603 RID: 5635
		private List<AgentInfo> vikingInfoList = new List<AgentInfo>(8);

		// Token: 0x04001604 RID: 5636
		private bool initialized;

		// Token: 0x04001605 RID: 5637
		private static AnimId reveal = "Reveal";

		// Token: 0x04001606 RID: 5638
		private static AnimId seen = "Seen";

		// Token: 0x04001607 RID: 5639
		private static AnimId unseen = "Unseen";

		// Token: 0x04001608 RID: 5640
		private static AnimId drop = "Drop";

		// Token: 0x04001609 RID: 5641
		private static FabricEventReference revealAudio = "UI/InGame/EnemyFaceTurn";

		// Token: 0x0400160A RID: 5642
		private static FabricEventReference dropAudio = "UI/InGame/EnemyFaceDeath";
	}
}
