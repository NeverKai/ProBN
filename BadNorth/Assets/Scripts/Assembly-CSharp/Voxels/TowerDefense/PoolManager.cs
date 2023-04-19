using System;
using System.Collections;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000581 RID: 1409
	public class PoolManager : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland, IslandGameplayManager.IExitCampaign
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x0007250D File Offset: 0x0007090D
		public static PoolManager instance
		{
			get
			{
				return PoolManager._instance;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00072514 File Offset: 0x00070914
		public Transform poolContainer
		{
			get
			{
				return this._poolContainer;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0007251C File Offset: 0x0007091C
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			PoolManager._instance = this;
			this._poolContainer = base.transform.AddEmptyChild("Container");
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0007253C File Offset: 0x0007093C
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			foreach (SelfPoolingPrefab pool in this.DefaultPools)
			{
				yield return null;
				if (pool != null)
				{
					pool.Initialize();
				}
				yield return null;
				//using ("RepairPool")
				{
					pool.RepairPool();
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00072558 File Offset: 0x00070958
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			foreach (SelfPoolingPrefab selfPoolingPrefab in this.poolContainer.GetComponentsInChildren<SelfPoolingPrefab>())
			{
				selfPoolingPrefab.ReturnToPool();
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00072590 File Offset: 0x00070990
		void IslandGameplayManager.IExitCampaign.OnExitCampaign(CampaignManager campaignManager, Campaign campaign)
		{
			foreach (SelfPoolingPrefab selfPoolingPrefab in this.DefaultPools)
			{
				selfPoolingPrefab.Clear();
			}
		}

		// Token: 0x04001704 RID: 5892
		[SerializeField]
		public SelfPoolingPrefab[] DefaultPools = new SelfPoolingPrefab[0];

		// Token: 0x04001705 RID: 5893
		private static PoolManager _instance;

		// Token: 0x04001706 RID: 5894
		private Transform _poolContainer;
	}
}
