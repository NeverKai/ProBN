using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x02000920 RID: 2336
	[Serializable]
	public struct GridSlot : IComparable<GridSlot>
	{
		// Token: 0x06003ECC RID: 16076 RVA: 0x0011B7B4 File Offset: 0x00119BB4
		public GridSlot(RectTransform rt, Rect pos, float t, bool inFocus)
		{
			this.rect = pos;
			this.t = t;
			this.rt = rt;
			this.inFocus = inFocus;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0011B7D3 File Offset: 0x00119BD3
		int IComparable<GridSlot>.CompareTo(GridSlot other)
		{
			return this.t.CompareTo(other.t);
		}

		// Token: 0x04002BDF RID: 11231
		public RectTransform rt;

		// Token: 0x04002BE0 RID: 11232
		public Rect rect;

		// Token: 0x04002BE1 RID: 11233
		public float t;

		// Token: 0x04002BE2 RID: 11234
		public bool inFocus;
	}
}
