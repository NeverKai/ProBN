using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTM.Pools
{
	// Token: 0x020004C5 RID: 1221
	public class LocalPool<T> where T : Component, IPoolable
	{
		// Token: 0x06001ECA RID: 7882 RVA: 0x00052C4F File Offset: 0x0005104F
		public LocalPool()
		{
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00052C6D File Offset: 0x0005106D
		public LocalPool(T reference, Transform spawnParent = null)
		{
			this.Init(reference, spawnParent);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00052C93 File Offset: 0x00051093
		public LocalPool(IEnumerable<T> references, Transform spawnParent = null)
		{
			this.Init(references, spawnParent);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00052CB9 File Offset: 0x000510B9
		public bool initialized
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x00052CCB File Offset: 0x000510CB
		public int capacity
		{
			get
			{
				return this.inUse.Count + this.available.Count;
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00052CE4 File Offset: 0x000510E4
		public void Init(T reference, Transform spawnParent = null)
		{
			this.reference = reference;
			this.spawnParent = ((!spawnParent) ? reference.transform.parent : spawnParent);
			reference.gameObject.SetActive(false);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00052D34 File Offset: 0x00051134
		public void Init(IEnumerable<T> references, Transform spawnParent = null)
		{
			this.available.AddRange(references);
			this.Init(this.available[0], spawnParent);
			this.available.Remove(this.reference);
			int i = 0;
			int count = this.available.Count;
			while (i < count)
			{
				T t = this.available[i];
				t.SetPool<T>(this);
				t.OnReturned();
				i++;
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00052DB8 File Offset: 0x000511B8
		public void ExpandTo(int capacity)
		{
			int num = capacity - this.capacity;
			for (int i = 0; i < num; i++)
			{
				this.AddInstance();
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00052DE8 File Offset: 0x000511E8
		public void AddInstance()
		{
			T item = this.createInstance();
			this.available.Add(item);
			item.OnReturned();
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00052E18 File Offset: 0x00051218
		public T GetInstance()
		{
			T t = this.available.Pop<T>();
			if (!t)
			{
				t = this.createInstance();
			}
			this.inUse.Add(t);
			t.OnRemoved();
			return t;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00052E64 File Offset: 0x00051264
		private T createInstance()
		{
			T result = UnityEngine.Object.Instantiate<T>(this.reference, this.spawnParent);
			result.SetPool<T>(this);
			return result;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00052E94 File Offset: 0x00051294
		public void ReturnToPool(T instance)
		{
			this.inUse.Remove(instance);
			this.available.Add(instance);
			instance.transform.SetParent(this.spawnParent, false);
			instance.OnReturned();
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00052EE0 File Offset: 0x000512E0
		public void ReturnAll()
		{
			T t = this.inUse.Pop<T>();
			while (t)
			{
				t.OnReturned();
				this.available.Add(t);
				t = this.inUse.Pop<T>();
			}
		}

		// Token: 0x04001329 RID: 4905
		private T reference;

		// Token: 0x0400132A RID: 4906
		private Transform spawnParent;

		// Token: 0x0400132B RID: 4907
		public List<T> available = new List<T>();

		// Token: 0x0400132C RID: 4908
		public List<T> inUse = new List<T>();
	}
}
