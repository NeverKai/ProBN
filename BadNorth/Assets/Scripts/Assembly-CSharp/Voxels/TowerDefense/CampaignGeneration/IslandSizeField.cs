using System;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006FB RID: 1787
	[Serializable]
	public class IslandSizeField
	{
		// Token: 0x06002E56 RID: 11862 RVA: 0x000B47B0 File Offset: 0x000B2BB0
		public IslandSizeField(int minWidth = 6, int maxWidth = 11, int minHeight = 1, int maxHeight = 4)
		{
			int num = maxWidth - minWidth + 1;
			int num2 = maxHeight - minHeight + 1;
			int num3 = num * num2;
			this.ranges = new Vector2[num3];
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000B47E0 File Offset: 0x000B2BE0
		public float GetAverageWidth()
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this.ranges.Length; i++)
			{
				int num3 = i % 6 + 6;
				float num4 = this.ranges[i].y - this.ranges[i].x;
				num += (float)num3 * num4;
				num2 += num4;
			}
			return num / num2;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000B4850 File Offset: 0x000B2C50
		public Vector2Int GetRandomDimensions(float fraction)
		{
			int num = 0;
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (fraction >= this.ranges[i].x && fraction <= this.ranges[i].y && this.ranges[i].y - this.ranges[i].x > 0f)
				{
					num++;
				}
			}
			int num2 = UnityEngine.Random.Range(0, num);
			for (int j = 0; j < this.ranges.Length; j++)
			{
				if (fraction >= this.ranges[j].x && fraction < this.ranges[j].y && this.ranges[j].y - this.ranges[j].x > 0f && num2-- == 0)
				{
					int x = j % 6 + 6;
					int y = j / 6 + 1;
					return new Vector2Int(x, y);
				}
			}
			Debug.LogError("IslandSizes is broken");
			return new Vector2Int(6, 1);
		}

		// Token: 0x04001EA3 RID: 7843
		public const int minWidth = 6;

		// Token: 0x04001EA4 RID: 7844
		public const int minHeight = 1;

		// Token: 0x04001EA5 RID: 7845
		public const int maxHeight = 4;

		// Token: 0x04001EA6 RID: 7846
		public const int maxWidth = 11;

		// Token: 0x04001EA7 RID: 7847
		public const int sizeX = 6;

		// Token: 0x04001EA8 RID: 7848
		[SerializeField]
		public Vector2[] ranges;
	}
}
