using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000608 RID: 1544
public static class ComponentEnumerator
{
	// Token: 0x060027C3 RID: 10179 RVA: 0x00081483 File Offset: 0x0007F883
	public static IEnumerable<T> EnumerateComponentsInChildren<T>(this Component component, bool includeInactive = false, bool forward = true)
	{
		return component.gameObject.EnumerateComponentsInChildren(includeInactive, forward);
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x00081494 File Offset: 0x0007F894
	public static IEnumerable<T> EnumerateComponentsInChildren<T>(this GameObject gameObject, bool includeInactive = false, bool forward = true)
	{
		ComponentEnumerator.PooledEnumerator<T> pooledEnumerator = ComponentEnumerator.PooledEnumerator<T>.Get();
		gameObject.GetComponentsInChildren<T>(includeInactive, pooledEnumerator.list);
		if (!forward)
		{
			pooledEnumerator.list.Reverse();
		}
		return pooledEnumerator;
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x000814C6 File Offset: 0x0007F8C6
	public static IEnumerable<T> EnumerateComponents<T>(this Component component, bool forward = true)
	{
		return component.gameObject.EnumerateComponentsInChildren(forward, true);
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x000814D8 File Offset: 0x0007F8D8
	public static IEnumerable<T> EnumerateComponents<T>(this GameObject gameObject, bool forward = true)
	{
		ComponentEnumerator.PooledEnumerator<T> pooledEnumerator = ComponentEnumerator.PooledEnumerator<T>.Get();
		gameObject.GetComponents<T>(pooledEnumerator.list);
		if (!forward)
		{
			pooledEnumerator.list.Reverse();
		}
		return pooledEnumerator;
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x00081509 File Offset: 0x0007F909
	public static void Initialize<T>(int capacity, int poolSize = 1)
	{
		ComponentEnumerator.PooledEnumerator<T>.Initialize(capacity, poolSize);
	}

	// Token: 0x02000609 RID: 1545
	private class PooledEnumerator<T> : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		// Token: 0x060027C8 RID: 10184 RVA: 0x00081512 File Offset: 0x0007F912
		public PooledEnumerator(int capacity)
		{
			this.list = new List<T>(capacity);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00081526 File Offset: 0x0007F926
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00081529 File Offset: 0x0007F929
		public  IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x0008152C File Offset: 0x0007F92C
		public T Current => this.list[this.index - 1];

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x00081541 File Offset: 0x0007F941
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.index - 1];
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0008155B File Offset: 0x0007F95B
		bool IEnumerator.MoveNext()
		{
			this.index++;
			return this.list != null && this.list.Count >= this.index;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0008158F File Offset: 0x0007F98F
		void IEnumerator.Reset()
		{
			this.index = 0;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00081598 File Offset: 0x0007F998
		void IDisposable.Dispose()
		{
			this.ReturnToPool();
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000815A0 File Offset: 0x0007F9A0
		public static ComponentEnumerator.PooledEnumerator<T> Get()
		{
			if (ComponentEnumerator.PooledEnumerator<T>.pool == null)
			{
				ComponentEnumerator.PooledEnumerator<T>.pool = new Stack<ComponentEnumerator.PooledEnumerator<T>>();
			}
			if (ComponentEnumerator.PooledEnumerator<T>.pool.Count > 0)
			{
				return ComponentEnumerator.PooledEnumerator<T>.pool.Pop();
			}
			return new ComponentEnumerator.PooledEnumerator<T>(32);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000815D8 File Offset: 0x0007F9D8
		private void ReturnToPool()
		{
			this.list.Clear();
			this.index = 0;
			ComponentEnumerator.PooledEnumerator<T>.pool.Push(this);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000815F8 File Offset: 0x0007F9F8
		public static void Initialize(int capacity, int poolSize = 1)
		{
			if (ComponentEnumerator.PooledEnumerator<T>.pool == null)
			{
				ComponentEnumerator.PooledEnumerator<T>.pool = new Stack<ComponentEnumerator.PooledEnumerator<T>>(poolSize);
			}
			for (int i = ComponentEnumerator.PooledEnumerator<T>.pool.Count; i < poolSize; i++)
			{
				ComponentEnumerator.PooledEnumerator<T>.pool.Push(new ComponentEnumerator.PooledEnumerator<T>(capacity));
			}
		}

		// Token: 0x0400197D RID: 6525
		private static Stack<ComponentEnumerator.PooledEnumerator<T>> pool;

		// Token: 0x0400197E RID: 6526
		public readonly List<T> list;

		// Token: 0x0400197F RID: 6527
		private int index;
	}
}
