using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000582 RID: 1410
	public class PrefabPool : MonoBehaviour
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x00072733 File Offset: 0x00070B33
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x0007273B File Offset: 0x00070B3B
		public PoolablePrefab prefab { get; private set; }

		// Token: 0x0600247C RID: 9340 RVA: 0x00072744 File Offset: 0x00070B44
		public PrefabPool Setup(PoolablePrefab prefab, int count, int capacity)
		{
			base.gameObject.SetActive(false);
			prefab.prefabPool = this;
			this.stack = new List<PoolablePrefab>(capacity);
			this.prefab = prefab;
			for (int i = 0; i < count; i++)
			{
				this.ReturnToPool(this.CreateInstance());
			}
			return this;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00072798 File Offset: 0x00070B98
		private PoolablePrefab CreateInstance()
		{
			PoolablePrefab poolablePrefab = UnityEngine.Object.Instantiate<PoolablePrefab>(this.prefab);
			poolablePrefab.transform.SetParent(base.transform);
			poolablePrefab.prefabPool = this;
			poolablePrefab.OnCreated();
			return poolablePrefab;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000727D0 File Offset: 0x00070BD0
		public PoolablePrefab GetInstance(Transform parent)
		{
			PoolablePrefab poolablePrefab;
			if (this.stack.Count > 0)
			{
				int index = this.stack.Count - 1;
				poolablePrefab = this.stack[index];
				this.stack.RemoveAt(index);
			}
			else
			{
				Debug.LogWarning(string.Format("{0} pool empty, instantiating", this.prefab.name));
				poolablePrefab = this.CreateInstance();
			}
			poolablePrefab.transform.SetParent(parent);
			poolablePrefab.OnRemovedFromPool();
			return poolablePrefab;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0007284E File Offset: 0x00070C4E
		public void ReturnToPool(PoolablePrefab instance)
		{
			this.stack.Add(instance);
			instance.transform.SetParent(base.transform);
			instance.OnReturnedToPool();
		}

		// Token: 0x04001707 RID: 5895
		private List<PoolablePrefab> stack;

		// Token: 0x04001709 RID: 5897
		private int creationIndex;
	}
}
