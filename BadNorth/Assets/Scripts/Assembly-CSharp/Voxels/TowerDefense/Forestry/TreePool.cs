using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x0200075E RID: 1886
	public class TreePool
	{
		// Token: 0x0600312E RID: 12590 RVA: 0x000CB7A4 File Offset: 0x000C9BA4
		public TreePool(Transform parent)
		{
			this.trees = parent.GetComponentsInChildren<Tree>(true);
			this.minRadius = float.MaxValue;
			this.maxRadius = float.MinValue;
			for (int i = 0; i < this.trees.Length; i++)
			{
				this.minRadius = Mathf.Min(this.minRadius, this.trees[i].radius);
				this.maxRadius = Mathf.Max(this.maxRadius, this.trees[i].radius);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000CB82F File Offset: 0x000C9C2F
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x000CB837 File Offset: 0x000C9C37
		public float minRadius { get; private set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000CB840 File Offset: 0x000C9C40
		// (set) Token: 0x06003132 RID: 12594 RVA: 0x000CB848 File Offset: 0x000C9C48
		public float maxRadius { get; private set; }

		// Token: 0x06003133 RID: 12595 RVA: 0x000CB854 File Offset: 0x000C9C54
		public Tree GetTree(Shoot shoot)
		{
			float num = float.MinValue;
			Tree result = null;
			for (int i = 0; i < this.trees.Length; i++)
			{
				Tree tree = this.trees[i];
				float num2 = -Mathf.Abs(tree.radius - shoot.radius);
				num2 *= UnityEngine.Random.Range(0.6f, 1f);
				if (num2 > num)
				{
					num = num2;
					result = this.trees[i];
				}
			}
			return result;
		}

		// Token: 0x04002106 RID: 8454
		private Tree[] trees;
	}
}
