using System;
using System.Collections.Generic;

// Token: 0x02000620 RID: 1568
public static class ListPool<T>
{
	// Token: 0x06002856 RID: 10326 RVA: 0x0008563C File Offset: 0x00083A3C
	public static List<T> GetList(int capacity)
	{
		if (ListPool<T>.pool.Count == 0)
		{
			return new List<T>(capacity);
		}
		for (int i = 0; i < ListPool<T>.pool.Count - 1; i++)
		{
			List<T> list = ListPool<T>.pool[i];
			if (list.Capacity >= capacity)
			{
				ListPool<T>.pool.RemoveAt(i);
				return list;
			}
		}
		List<T> result = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
		ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
		return result;
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x000856D0 File Offset: 0x00083AD0
	public static void ReturnList(List<T> list)
	{
		list.Clear();
		for (int i = 0; i < ListPool<T>.pool.Count; i++)
		{
			if (ListPool<T>.pool[i].Capacity >= list.Capacity)
			{
				ListPool<T>.pool.Insert(i, list);
				return;
			}
		}
		ListPool<T>.pool.Add(list);
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x00085734 File Offset: 0x00083B34
	public static void Initialize(int listCapacity, int poolSize = 1)
	{
		if (ListPool<T>.pool == null)
		{
			ListPool<T>.pool = new List<List<T>>(poolSize);
		}
		for (int i = ListPool<T>.pool.Count; i < poolSize; i++)
		{
			ListPool<T>.pool.Add(new List<T>(listCapacity));
		}
	}

	// Token: 0x040019DB RID: 6619
	private static List<List<T>> pool = new List<List<T>>();
}
