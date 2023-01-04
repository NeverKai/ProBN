using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AB RID: 1963
	public struct SquadFormation
	{
		// Token: 0x060032D0 RID: 13008 RVA: 0x000D8784 File Offset: 0x000D6B84
		public SquadFormation(int count, NavSpot navSpot)
		{
			this = new SquadFormation(count, navSpot.meshBounds, navSpot.lookDir);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000D879C File Offset: 0x000D6B9C
		public SquadFormation(int count, Bounds bounds, Vector3 dir)
		{
			this.count = count;
			int num = Mathf.CeilToInt(Mathf.Sqrt((float)count));
			int num2 = Mathf.CeilToInt((float)count / (float)num);
			bool flag = bounds.size.x > bounds.size.z;
			this.sizeX = ((!flag) ? num2 : num);
			this.sizeY = ((!flag) ? num : num2);
			this.squareCount = this.sizeX * this.sizeY;
			this.remainder = this.squareCount - count;
			this.rotation = ((Mathf.Abs(dir.x) <= Mathf.Abs(dir.z)) ? ((dir.z <= 0f) ? 1 : 3) : ((dir.x <= 0f) ? 2 : 0));
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000D888C File Offset: 0x000D6C8C
		public Vector3 Get(int index)
		{
			Vector2 inVec;
			if (this.remainder == 0)
			{
				int num = index % this.sizeX;
				int num2 = index / this.sizeX;
				inVec.x = (float)num - (float)(this.sizeX - 1) / 2f;
				inVec.y = (float)num2 - (float)(this.sizeY - 1) / 2f;
				if (num2 % 2 == 0)
				{
					inVec.x = -inVec.x;
				}
				return inVec.GetXZtoXYZ();
			}
			int num3 = this.remainder / 2;
			int num4 = this.remainder - num3;
			switch (this.rotation)
			{
			case 0:
			{
				index = this.count - index - 1;
				index += Mathf.Min(num4, index / (this.sizeX - 1));
				index += Mathf.Max(0, (index - this.squareCount + num3 * this.sizeX) / (this.sizeX - 1));
				int num = index % this.sizeX;
				int num2 = index / this.sizeX;
				if (num2 % 2 == 0)
				{
					num = this.sizeX - num - 1;
					if (num2 < num4 || num2 >= this.sizeY - num3)
					{
						num--;
					}
				}
				inVec.x = (float)(this.sizeX - 1) / 2f - (float)num;
				inVec.y = (float)(this.sizeY - 1) / 2f - (float)num2;
				if (num3 != num4 && num == this.sizeX - 1)
				{
					inVec.y += 0.5f;
				}
				return inVec.GetXZtoXYZ();
			}
			case 1:
			{
				int num = index % this.sizeX;
				int num2 = index / this.sizeX;
				inVec.x = (float)num - (float)(this.sizeX - 1) / 2f;
				inVec.y = (float)num2 - (float)(this.sizeY - 1) / 2f;
				if (num2 == this.sizeY - 1)
				{
					inVec.x += (float)(this.sizeX * this.sizeY - this.count) / 2f;
				}
				if (num2 % 2 == 0)
				{
					inVec.x = -inVec.x;
				}
				return inVec.GetXZtoXYZ();
			}
			case 2:
			{
				index += Mathf.Min(num3, index / (this.sizeX - 1));
				index += Mathf.Max(0, (index - this.squareCount + num4 * this.sizeX) / (this.sizeX - 1));
				int num = index % this.sizeX;
				int num2 = index / this.sizeX;
				if (num2 % 2 == 0)
				{
					num = this.sizeX - num - 1;
					if (num2 < num3 || num2 >= this.sizeY - num4)
					{
						num--;
					}
				}
				inVec.x = (float)num - (float)(this.sizeX - 1) / 2f;
				inVec.y = (float)num2 - (float)(this.sizeY - 1) / 2f;
				if (num3 != num4 && num == this.sizeX - 1)
				{
					inVec.y += 0.5f;
				}
				return inVec.GetXZtoXYZ();
			}
			case 3:
			{
				index = this.count - index - 1;
				int num = index % this.sizeX;
				int num2 = index / this.sizeX;
				inVec.x = (float)(this.sizeX - 1) / 2f - (float)num;
				inVec.y = (float)(this.sizeY - 1) / 2f - (float)num2;
				if (num2 == this.sizeY - 1)
				{
					inVec.x -= (float)(this.sizeX * this.sizeY - this.count) / 2f;
				}
				if ((num2 + this.sizeY) % 2 == 1)
				{
					inVec.x = -inVec.x;
				}
				return inVec.GetXZtoXYZ();
			}
			default:
				return Vector3.zero;
			}
		}

		// Token: 0x0400228F RID: 8847
		public readonly int count;

		// Token: 0x04002290 RID: 8848
		public readonly int sizeX;

		// Token: 0x04002291 RID: 8849
		public readonly int sizeY;

		// Token: 0x04002292 RID: 8850
		public readonly int rotation;

		// Token: 0x04002293 RID: 8851
		public readonly int squareCount;

		// Token: 0x04002294 RID: 8852
		public readonly int remainder;
	}
}
