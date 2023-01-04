using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200057E RID: 1406
	public class PoolablePrefab : MonoBehaviour
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x00072358 File Offset: 0x00070758
		public T GetInstance<T>(Transform parent = null) where T : PoolablePrefab
		{
			PoolablePrefab instance = PooledPrefabManager.GetInstance(this, parent);
			T t = instance as T;
			return (!(t != null)) ? instance.GetComponent<T>() : t;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00072396 File Offset: 0x00070796
		public void ReturnToPool()
		{
			this.prefabPool.ReturnToPool(this);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000723A4 File Offset: 0x000707A4
		public Quaternion defaultRotation
		{
			get
			{
				return this.prefabPool.prefab.transform.localRotation;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000723BB File Offset: 0x000707BB
		public Vector3 defaultScale
		{
			get
			{
				return this.prefabPool.prefab.transform.localScale;
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000723D2 File Offset: 0x000707D2
		public virtual void OnCreated()
		{
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000723D4 File Offset: 0x000707D4
		public virtual void OnReturnedToPool()
		{
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000723D6 File Offset: 0x000707D6
		public virtual void OnRemovedFromPool()
		{
		}

		// Token: 0x04001700 RID: 5888
		[NonSerialized]
		public PrefabPool prefabPool;
	}
}
