using System;
using UnityEngine;

namespace SpriteComposing
{
	// Token: 0x020005CB RID: 1483
	public struct RectRequest
	{
		// Token: 0x060026A6 RID: 9894 RVA: 0x0007AD93 File Offset: 0x00079193
		public RectRequest(int width, int height, int padding = 0, int snapping = 1)
		{
			this.width = width;
			this.height = height;
			this.padding = padding;
			this.snapping = snapping;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0007ADB4 File Offset: 0x000791B4
		public Rect GetSpriteRect(Vector2 min)
		{
			min.x += (float)this.padding;
			min.y += (float)this.padding;
			if (this.snapping > 0)
			{
				min.x = (float)(Mathf.CeilToInt(min.x / (float)this.snapping) * this.snapping);
				min.y = (float)(Mathf.CeilToInt(min.y / (float)this.snapping) * this.snapping);
			}
			Vector2 size = new Vector2((float)this.width, (float)this.height);
			return new Rect(min, size);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0007AE5C File Offset: 0x0007925C
		public Vector2 GetMax(Vector2 min)
		{
			Vector2 max = this.GetSpriteRect(min).max;
			max.x += (float)this.padding;
			max.y += (float)this.padding;
			return max;
		}

		// Token: 0x040018A9 RID: 6313
		public readonly int width;

		// Token: 0x040018AA RID: 6314
		public readonly int height;

		// Token: 0x040018AB RID: 6315
		public readonly int padding;

		// Token: 0x040018AC RID: 6316
		public readonly int snapping;
	}
}
