using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000584 RID: 1412
	public class SelfPoolingPrefab : MonoBehaviour
	{
		// Token: 0x06002483 RID: 9347 RVA: 0x000729D8 File Offset: 0x00070DD8
		public void Initialize()
		{
			if (this.itemPool != null && this.itemPool.Capacity >= this.initialPoolSize)
			{
				return;
			}
			using ("SelfPoolingPrefab.Initialize()")
			{
				this.nextIdx = 0;
				if (this.container)
				{
					UnityEngine.Object.Destroy(this.container.gameObject);
				}
				this.itemPool = new List<SelfPoolingPrefab>();
				this.container = PoolManager.instance.poolContainer.AddEmptyChild(base.name + "Pool");
				this.itemPool.Capacity = this.initialPoolSize;
				this.InstatiateItemsInPool(this.initialPoolSize);
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00072AAC File Offset: 0x00070EAC
		public void Clear()
		{
			if (this.itemPool != null)
			{
				foreach (SelfPoolingPrefab selfPoolingPrefab in this.itemPool)
				{
					if (selfPoolingPrefab)
					{
						UnityEngine.Object.Destroy(selfPoolingPrefab.gameObject);
					}
				}
			}
			this.itemPool = null;
			if (this.container)
			{
				UnityEngine.Object.Destroy(this.container.gameObject);
			}
			this.container = null;
			this.nextIdx = 0;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00072B58 File Offset: 0x00070F58
		public SelfPoolingPrefab GetInstance()
		{
			SelfPoolingPrefab result;
			using ("SelfPoolingPrefab.GetInstance()")
			{
				if (this.itemPool == null || this.itemPool.Count == 0 || this.itemPool[0] == null)
				{
					Debug.LogWarningFormat("Self-Pooling Prefab [{0}] is being created on-demand (add it to the pool manager)", new object[]
					{
						base.name
					});
					this.Initialize();
				}
				int num = -1;
				int i = 0;
				int count = this.itemPool.Count;
				while (i < count)
				{
					int num2 = (i + this.nextIdx) % count;
					SelfPoolingPrefab selfPoolingPrefab = this.itemPool[num2];
					if (selfPoolingPrefab)
					{
						if (!selfPoolingPrefab.gameObject.activeSelf)
						{
							this.RemoveFromPool(selfPoolingPrefab);
							this.nextIdx = num2 + 1;
							return this.Get(selfPoolingPrefab);
						}
					}
					else if (num < 0)
					{
						num = num2;
					}
					i++;
				}
				if (this.itemPool.IsValidIndex(num))
				{
					SelfPoolingPrefab selfPoolingPrefab2 = this.InstantiateItem(num);
					selfPoolingPrefab2.OnGet();
					this.itemPool[num] = selfPoolingPrefab2;
					result = this.Get(selfPoolingPrefab2);
				}
				else if (this.TryGrowPool())
				{
					result = this.GetInstance();
				}
				else
				{
					Debug.LogWarning("Pool failed to spawn an item");
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00072CE0 File Offset: 0x000710E0
		public T GetInstance<T>() where T : class
		{
			SelfPoolingPrefab instance = this.GetInstance();
			T t = instance as T;
			return (t == null) ? instance.GetComponent<T>() : t;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00072D1C File Offset: 0x0007111C
		public void RepairPool()
		{
			this.itemPool.RemoveAll((SelfPoolingPrefab i) => i == null);
			for (int j = this.itemPool.Count; j < this.itemPool.Capacity; j++)
			{
				this.itemPool.Add(this.InstantiateItem(j));
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00072D8C File Offset: 0x0007118C
		private bool TryGrowPool()
		{
			if (!this.canGrow)
			{
				return false;
			}
			int count = this.itemPool.Count;
			int num = Mathf.Min(count * 2, this.maxSize);
			if (num <= count)
			{
				Debug.LogError("Pool failed to grow beyond " + count);
				return false;
			}
			this.itemPool.Capacity = num;
			this.InstatiateItemsInPool(num - count);
			return true;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00072DF8 File Offset: 0x000711F8
		private void InstatiateItemsInPool(int num)
		{
			for (int i = 0; i < num; i++)
			{
				this.itemPool.Add(this.InstantiateItem(i));
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x00072E2C File Offset: 0x0007122C
		private SelfPoolingPrefab InstantiateItem(int idx)
		{
			SelfPoolingPrefab selfPoolingPrefab = UnityEngine.Object.Instantiate<SelfPoolingPrefab>(this);
			selfPoolingPrefab.owner = this;
			selfPoolingPrefab.container = this.container;
			selfPoolingPrefab.transform.SetParent(this.container);
			selfPoolingPrefab.OnInstantiate();
			selfPoolingPrefab.gameObject.SetActive(false);
			return selfPoolingPrefab;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00072E77 File Offset: 0x00071277
		private void RemoveFromPool(SelfPoolingPrefab instance)
		{
			instance.gameObject.SetActive(true);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00072E85 File Offset: 0x00071285
		public void ReturnToPool()
		{
			base.gameObject.SetActive(false);
			this.ReturnToParent();
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00072E99 File Offset: 0x00071299
		public void ReturnToParent()
		{
			base.transform.AttachTo(this.owner.container.transform);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00072EB6 File Offset: 0x000712B6
		protected virtual void OnDisable()
		{
			if (this.owner)
			{
				this.ReturnToParent();
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00072ECE File Offset: 0x000712CE
		private SelfPoolingPrefab Get(SelfPoolingPrefab item)
		{
			item.OnGet();
			return item;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00072ED7 File Offset: 0x000712D7
		protected virtual void OnInstantiate()
		{
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00072ED9 File Offset: 0x000712D9
		protected virtual void OnGet()
		{
		}

		// Token: 0x0400170E RID: 5902
		[Header("Pool")]
		[SerializeField]
		private int initialPoolSize = 16;

		// Token: 0x0400170F RID: 5903
		[SerializeField]
		private bool canGrow;

		// Token: 0x04001710 RID: 5904
		[SerializeField]
		private int maxSize = 256;

		// Token: 0x04001711 RID: 5905
		protected Transform container;

		// Token: 0x04001712 RID: 5906
		private List<SelfPoolingPrefab> itemPool;

		// Token: 0x04001713 RID: 5907
		private SelfPoolingPrefab owner;

		// Token: 0x04001714 RID: 5908
		private int nextIdx;
	}
}
