using System;
using System.Collections;
using System.Collections.Generic;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000539 RID: 1337
	public class NavSpotPoolManager : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x00066F60 File Offset: 0x00065360
		public LocalPool<TargetNavSpot> GetPool(TargetNavSpot prefab)
		{
			LocalPool<TargetNavSpot> result = null;
			if (this.pools.TryGetValue(prefab, out result))
			{
				return result;
			}
			Debug.LogWarningFormat("NavSpotPoolManager - creating NavSpotPool on-the-fly for {0}, please add to the default pools list", new object[]
			{
				prefab.name
			});
			return this.CreatePool(prefab);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x00066FA4 File Offset: 0x000653A4
		private LocalPool<TargetNavSpot> CreatePool(TargetNavSpot prefab)
		{
			GameObject gameObject = base.gameObject.AddEmptyChild(prefab.name);
			TargetNavSpot reference = gameObject.gameObject.InstantiateChild(prefab, "template");
			LocalPool<TargetNavSpot> localPool = new LocalPool<TargetNavSpot>(reference, null);
			this.pools.Add(prefab, localPool);
			return localPool;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00066FEC File Offset: 0x000653EC
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.pools = new Dictionary<TargetNavSpot, LocalPool<TargetNavSpot>>(this.defaultPoolPrefabs.Count);
			foreach (TargetNavSpot prefab in this.defaultPoolPrefabs)
			{
				this.CreatePool(prefab);
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00067060 File Offset: 0x00065460
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			foreach (LocalPool<TargetNavSpot> pool in this.pools.Values)
			{
				while (pool.capacity < island.navSpotter.navSpots.Count)
				{
					pool.AddInstance();
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00067084 File Offset: 0x00065484
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			foreach (LocalPool<TargetNavSpot> localPool in this.pools.Values)
			{
				localPool.ReturnAll();
			}
		}

		// Token: 0x0400154A RID: 5450
		[SerializeField]
		private List<TargetNavSpot> defaultPoolPrefabs;

		// Token: 0x0400154B RID: 5451
		private Dictionary<TargetNavSpot, LocalPool<TargetNavSpot>> pools;
	}
}
