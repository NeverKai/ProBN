using System;
using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200053A RID: 1338
	public class NavSpotTargetCache
	{
		// Token: 0x060022E9 RID: 8937 RVA: 0x0006727C File Offset: 0x0006567C
		public void Resize(int capacity)
		{
			this.entries.Capacity = Mathf.Max(capacity, this.entries.Capacity);
			while (this.entries.Count < capacity)
			{
				this.entries.Add(default(NavSpotTargetCache.Entry));
			}
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000672D0 File Offset: 0x000656D0
		public void Reset()
		{
			int i = 0;
			int count = this.entries.Count;
			while (i < count)
			{
				this.entries[i] = default(NavSpotTargetCache.Entry);
				i++;
			}
		}

		// Token: 0x0400154C RID: 5452
		public List<NavSpotTargetCache.Entry> entries = new List<NavSpotTargetCache.Entry>();

		// Token: 0x0200053B RID: 1339
		public struct Entry
		{
			// Token: 0x0400154D RID: 5453
			public BitMask192 targetMask;

			// Token: 0x0400154E RID: 5454
			public string errorMessage;
		}
	}
}
