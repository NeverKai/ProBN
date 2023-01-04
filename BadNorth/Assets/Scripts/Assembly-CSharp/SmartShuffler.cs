using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020005FF RID: 1535
internal class SmartShuffler<T>
{
	// Token: 0x06002783 RID: 10115 RVA: 0x0007FF5D File Offset: 0x0007E35D
	public SmartShuffler(IEnumerable<T> enumerable)
	{
		this.list = (from t in enumerable
		select new SmartShuffler<T>.Item(t, 1f)).ToList<SmartShuffler<T>.Item>();
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x0007FF94 File Offset: 0x0007E394
	public SmartShuffler(IEnumerable<T> enumerable, Func<T, float> frequency)
	{
		this.list = (from t in enumerable
		select new SmartShuffler<T>.Item(t, frequency(t))).ToList<SmartShuffler<T>.Item>();
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x0007FFD4 File Offset: 0x0007E3D4
	public T Get()
	{
		return (from x in this.list
		orderby x.count, UnityEngine.Random.value
		select x).First<SmartShuffler<T>.Item>().Get();
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x00080038 File Offset: 0x0007E438
	public T GetByCriteria(Func<T, bool> criteria)
	{
		if (!this.list.Any((SmartShuffler<T>.Item x) => criteria(x.t)))
		{
			return default(T);
		}
		return (from x in this.list
		where criteria(x.t)
		orderby x.count, UnityEngine.Random.value
		select x).First<SmartShuffler<T>.Item>().Get();
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x000800E0 File Offset: 0x0007E4E0
	public T GetByCriteriaFirst(Func<T, bool> criteria)
	{
		if (!this.list.Any((SmartShuffler<T>.Item x) => criteria(x.t)))
		{
			return this.Get();
		}
		return (from x in this.list
		where criteria(x.t)
		orderby x.count, UnityEngine.Random.value
		select x).First<SmartShuffler<T>.Item>().Get();
	}

	// Token: 0x04001960 RID: 6496
	private List<SmartShuffler<T>.Item> list;

	// Token: 0x02000600 RID: 1536
	private class Item
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x000801BC File Offset: 0x0007E5BC
		public Item(T t, float frequency = 1f)
		{
			this.interval = 1f / frequency;
			this.t = t;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000801D8 File Offset: 0x0007E5D8
		public T Get()
		{
			this.count += this.interval;
			return this.t;
		}

		// Token: 0x04001968 RID: 6504
		public T t;

		// Token: 0x04001969 RID: 6505
		public float count;

		// Token: 0x0400196A RID: 6506
		public float interval;
	}
}
