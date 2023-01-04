using System;
using System.Collections;
using System.Collections.Generic;
using Fabric;
using UnityEngine;

// Token: 0x02000394 RID: 916
[Serializable]
public struct FabricEventArray : IEnumerable<int>, IEnumerable
{
	// Token: 0x060014E7 RID: 5351 RVA: 0x0002B62A File Offset: 0x00029A2A
	public FabricEventArray(string format, int min, int max)
	{
		this.format = format;
		this.min = min;
		this.max = max;
		this.valid = false;
		this.ids = null;
	}

	// Token: 0x170000F7 RID: 247
	public int this[int key]
	{
		get
		{
			if (!this.valid)
			{
				this.Init();
			}
			return this.ids[key];
		}
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x0002B66C File Offset: 0x00029A6C
	public void Init()
	{
		if (this.valid)
		{
			return;
		}
		this.ids = new int[this.max - this.min + 1];
		int i = 0;
		int num = this.ids.Length;
		while (i < num)
		{
			string eventName = string.Format(this.format, i + this.min);
			this.ids[i] = EventManager.GetIDFromEventName(eventName);
			i++;
		}
		this.valid = true;
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x0002B6E8 File Offset: 0x00029AE8
	IEnumerator<int> IEnumerable<int>.GetEnumerator()
	{
		return ((IEnumerable<int>)this.ids).GetEnumerator();
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x0002B6F5 File Offset: 0x00029AF5
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.ids).GetEnumerator();
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060014EC RID: 5356 RVA: 0x0002B702 File Offset: 0x00029B02
	public int Length
	{
		get
		{
			return this.max - this.min + 1;
		}
	}

	// Token: 0x04000CFC RID: 3324
	[SerializeField]
	private string format;

	// Token: 0x04000CFD RID: 3325
	[SerializeField]
	private int min;

	// Token: 0x04000CFE RID: 3326
	[SerializeField]
	private int max;

	// Token: 0x04000CFF RID: 3327
	private bool valid;

	// Token: 0x04000D00 RID: 3328
	private int[] ids;
}
