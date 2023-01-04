using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200058C RID: 1420
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public struct HouseState
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x00073C7C File Offset: 0x0007207C
		public HouseState(House house)
		{
			this.condition = HouseState.Condition.Intact;
			this.value = (byte)house.coinCount;
			Bounds bounds = house.bounds;
			this.xMin = bounds.min.x;
			this.yMin = bounds.min.z;
			this.width = bounds.size.x;
			this.height = bounds.size.z;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00073CFC File Offset: 0x000720FC
		public void SetBounds(House house)
		{
			Bounds bounds = house.bounds;
			this.xMin = bounds.min.x;
			this.yMin = bounds.min.z;
			this.width = bounds.size.x;
			this.height = bounds.size.z;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x00073D9B File Offset: 0x0007219B
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x00073D65 File Offset: 0x00072165
		public Rect rect
		{
			get
			{
				return new Rect(this.xMin, this.yMin, this.width, this.height);
			}
			set
			{
				this.xMin = value.xMin;
				this.yMin = value.yMin;
				this.width = value.height;
				this.height = value.height;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00073DBA File Offset: 0x000721BA
		public int availableCoins
		{
			get
			{
				return (int)((this.condition != HouseState.Condition.Intact) ? 0 : this.value);
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00073DD3 File Offset: 0x000721D3
		public override string ToString()
		{
			return string.Format("{0} - {1} coins, {2}", this.condition, this.value, this.rect);
		}

		// Token: 0x04001762 RID: 5986
		public HouseState.Condition condition;

		// Token: 0x04001763 RID: 5987
		public byte value;

		// Token: 0x04001764 RID: 5988
		private float xMin;

		// Token: 0x04001765 RID: 5989
		private float yMin;

		// Token: 0x04001766 RID: 5990
		private float width;

		// Token: 0x04001767 RID: 5991
		private float height;

		// Token: 0x0200058D RID: 1421
		public enum Condition : byte
		{
			// Token: 0x04001769 RID: 5993
			Intact,
			// Token: 0x0400176A RID: 5994
			Pillaged,
			// Token: 0x0400176B RID: 5995
			Saved
		}
	}
}
