using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteComposing
{
	// Token: 0x020005CC RID: 1484
	[Serializable]
	public class Sheet
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x0007AEA4 File Offset: 0x000792A4
		public Sheet(Texture2D tex)
		{
			this.avaliableSquares = new List<Rect>
			{
				new Rect(0f, 0f, (float)tex.width, (float)tex.height)
			};
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0007AEE7 File Offset: 0x000792E7
		public void ReturnRect(Rect rect)
		{
			this.AddSquare(rect);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0007AEF0 File Offset: 0x000792F0
		private void AddSquare(Rect rect0)
		{
			for (int i = 0; i < this.avaliableSquares.Count; i++)
			{
				Rect rect = this.avaliableSquares[i];
				if (rect0.xMax == rect.xMin && rect0.yMax == rect.yMax && rect0.yMin == rect.yMin)
				{
					rect0 = new Rect(rect0.xMin, rect0.yMin, rect0.width + rect.width, rect0.height);
					this.avaliableSquares.RemoveAt(i);
					i = 0;
				}
				else if (rect0.xMin == rect.xMax && rect0.yMax == rect.yMax && rect0.yMin == rect.yMin)
				{
					rect0 = new Rect(rect.xMin, rect.yMin, rect0.width + rect.width, rect0.height);
					this.avaliableSquares.RemoveAt(i);
					i = 0;
				}
				else if (rect0.yMax == rect.yMin && rect0.xMax == rect.xMax && rect0.xMin == rect.xMin)
				{
					rect0 = new Rect(rect0.xMin, rect0.yMin, rect0.width, rect0.height + rect.height);
					this.avaliableSquares.RemoveAt(i);
					i = 0;
				}
				else if (rect0.yMin == rect.yMax && rect0.xMax == rect.xMax && rect0.xMin == rect.xMin)
				{
					rect0 = new Rect(rect.xMin, rect.yMin, rect0.width, rect0.height + rect.height);
					this.avaliableSquares.RemoveAt(i);
					i = 0;
				}
			}
			this.avaliableSquares.Add(rect0);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0007B10C File Offset: 0x0007950C
		public Rect GetSquare(RectRequest occupant)
		{
			Rect rect = default(Rect);
			Vector2 vector = Vector2.zero;
			float num = float.MaxValue;
			int index = -1;
			for (int i = 0; i < this.avaliableSquares.Count; i++)
			{
				Rect rect2 = this.avaliableSquares[i];
				Vector2 max = occupant.GetMax(rect2.min);
				float num2 = Mathf.Min(rect2.xMax - max.x, rect2.yMax - max.y);
				if (num2 >= 0f)
				{
					if (num2 < num)
					{
						num = num2;
						vector = max;
						rect = rect2;
						index = i;
					}
				}
			}
			this.avaliableSquares.RemoveAt(index);
			Rect rect3 = rect;
			Rect result = new Rect(rect3.x, rect3.y, vector.x - rect3.xMin, vector.y - rect3.yMin);
			Vector2 lhs = rect3.max - result.max;
			if (lhs != Vector2.zero)
			{
				if (lhs.x == 0f)
				{
					this.AddSquare(new Rect(result.xMin, result.yMax, result.width, rect3.height - result.height));
				}
				else if (lhs.y == 0f)
				{
					this.AddSquare(new Rect(result.xMax, result.yMin, rect3.width - result.width, result.height));
				}
				else if (lhs.x < lhs.y)
				{
					this.AddSquare(new Rect(result.xMax, result.yMin, rect3.width - result.width, result.height));
					this.AddSquare(new Rect(result.xMin, result.yMax, rect3.width, rect3.height - result.height));
				}
				else
				{
					this.AddSquare(new Rect(result.xMin, result.yMax, result.width, rect3.height - result.height));
					this.AddSquare(new Rect(result.xMax, result.yMin, rect3.width - result.width, rect3.height));
				}
			}
			return result;
		}

		// Token: 0x040018AD RID: 6317
		public List<Rect> avaliableSquares;
	}
}
