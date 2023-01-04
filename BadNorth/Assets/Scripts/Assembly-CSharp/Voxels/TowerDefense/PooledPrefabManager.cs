using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200057F RID: 1407
	public class PooledPrefabManager : Singleton<PooledPrefabManager>
	{
		// Token: 0x0600246C RID: 9324 RVA: 0x000723E0 File Offset: 0x000707E0
		protected override void Awake()
		{
			base.Awake();
			foreach (PooledPrefabManager.DefaultPoolable defaultPoolable in this.defaults)
			{
				this.CreatePrefabPool(defaultPoolable.prefab, defaultPoolable.count, defaultPoolable.count);
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00072458 File Offset: 0x00070858
		public static PoolablePrefab GetInstance(PoolablePrefab prefab, Transform parent = null)
		{
			return Singleton<PooledPrefabManager>.instance.GetInstanceInternal(prefab, parent);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00072468 File Offset: 0x00070868
		private PoolablePrefab GetInstanceInternal(PoolablePrefab prefab, Transform parent)
		{
			if (!prefab.prefabPool)
			{
				Debug.LogWarning(string.Format("No pool for {0}, creating one", prefab.name));
				prefab.prefabPool = this.CreatePrefabPool(prefab, 1, 4);
			}
			return prefab.prefabPool.GetInstance(parent);
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000724B5 File Offset: 0x000708B5
		private PrefabPool CreatePrefabPool(PoolablePrefab prefab, int count, int capacity)
		{
			return base.gameObject.AddEmptyChild(null).AddComponent<PrefabPool>().Setup(prefab, count, capacity);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000724D0 File Offset: 0x000708D0
		public void ReturnToPool(GameObject instance)
		{
			PoolablePrefab component = instance.GetComponent<PoolablePrefab>();
			this.ReturnToPool(component);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000724EB File Offset: 0x000708EB
		public void ReturnToPool(PoolablePrefab instance)
		{
			instance.prefabPool.ReturnToPool(instance);
		}

		// Token: 0x04001701 RID: 5889
		[SerializeField]
		private List<PooledPrefabManager.DefaultPoolable> defaults;

		// Token: 0x02000580 RID: 1408
		[Serializable]
		private struct DefaultPoolable
		{
			// Token: 0x04001702 RID: 5890
			public PoolablePrefab prefab;

			// Token: 0x04001703 RID: 5891
			public int count;
		}
	}
}
