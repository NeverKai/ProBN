using System;
using System.Collections.Generic;

namespace RTM.Pools
{
	// Token: 0x020004C6 RID: 1222
	internal class SimplePool<T> where T : IPoolable, new()
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x00052F34 File Offset: 0x00051334
		public SimplePool(int poolSize)
		{
			this.pool = new List<T>(poolSize);
			for (int i = 0; i < poolSize; i++)
			{
				T item = Activator.CreateInstance<T>();
				item.OnReturned();
				this.pool.Add(item);
			}
			this.init = true;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00052F98 File Offset: 0x00051398
		public T GetInstance()
		{
			T result = default(T);
			object obj = this.semaphore;
			lock (obj)
			{
				int index = this.pool.Count - 1;
				result = this.pool[index];
				this.pool.RemoveAt(index);
			}
			result.OnRemoved();
			return result;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00053010 File Offset: 0x00051410
		public void Fill(List<T> list, int size = -1)
		{
			size = ((size != -1) ? size : list.Capacity);
			list.Clear();
			list.Capacity = size;
			object obj = this.semaphore;
			lock (obj)
			{
				int num = this.pool.Count - 1;
				int i = 0;
				int capacity = list.Capacity;
				while (i < capacity)
				{
					list[i] = this.pool[num - i];
					i++;
				}
				this.pool.RemoveRange(this.pool.Count - list.Count, list.Count);
			}
			foreach (T t in list)
			{
				t.OnRemoved();
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00053118 File Offset: 0x00051518
		public void ReturnToPool(T item)
		{
			item.OnReturned();
			object obj = this.semaphore;
			lock (obj)
			{
				this.pool.Add(item);
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00053168 File Offset: 0x00051568
		public void ReturnToPool(List<T> list)
		{
			foreach (T t in list)
			{
				t.OnReturned();
			}
			object obj = this.semaphore;
			lock (obj)
			{
				this.pool.AddRange(list);
			}
			list.Clear();
		}

		// Token: 0x0400132D RID: 4909
		private List<T> pool;

		// Token: 0x0400132E RID: 4910
		private bool init;

		// Token: 0x0400132F RID: 4911
		private object semaphore = new object();
	}
}
