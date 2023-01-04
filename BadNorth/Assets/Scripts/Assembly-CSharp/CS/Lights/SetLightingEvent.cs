using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x0200038F RID: 911
	public class SetLightingEvent
	{
		// Token: 0x060014C7 RID: 5319 RVA: 0x0002AFDC File Offset: 0x000293DC
		public SetLightingEvent()
		{
			this.ColourLayout = new SortedList<int, Color>[3][][];
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.ColourLayout.Length))
			{
				this.ColourLayout[(int)((UIntPtr)num)] = new SortedList<int, Color>[3][];
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)this.ColourLayout.Length))
				{
					this.ColourLayout[(int)((UIntPtr)num)][(int)((UIntPtr)num2)] = new SortedList<int, Color>[3];
					uint num3 = 0U;
					while ((ulong)num3 < (ulong)((long)this.ColourLayout.Length))
					{
						this.ColourLayout[(int)((UIntPtr)num)][(int)((UIntPtr)num2)][(int)((UIntPtr)num3)] = new SortedList<int, Color>();
						num3 += 1U;
					}
					num2 += 1U;
				}
				num += 1U;
			}
			this.Reset();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0002B094 File Offset: 0x00029494
		public virtual void Reset()
		{
			this.Used = false;
			this.TargetedOverride = false;
			this.ForceClear = false;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.ColourLayout.Length))
			{
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)this.ColourLayout[(int)((UIntPtr)num)].Length))
				{
					uint num3 = 0U;
					while ((ulong)num3 < (ulong)((long)this.ColourLayout[(int)((UIntPtr)num)][(int)((UIntPtr)num2)].Length))
					{
						this.ColourLayout[(int)((UIntPtr)num)][(int)((UIntPtr)num2)][(int)((UIntPtr)num3)].Clear();
						num3 += 1U;
					}
					num2 += 1U;
				}
				num += 1U;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0002B124 File Offset: 0x00029524
		public void SetPoint(uint x, uint y, Color colour, int layer)
		{
			if (x < 3U && y < 3U)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)].Length))
				{
					if (this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)num)].ContainsKey(layer))
					{
						this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)num)][layer] = colour;
					}
					else
					{
						this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)num)].Add(layer, colour);
					}
					num += 1U;
				}
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0002B1B0 File Offset: 0x000295B0
		public void SetPoint(uint x, uint y, uint z, Color colour, int layer)
		{
			if (x < 3U && y < 3U && z < 3U)
			{
				if (this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)z)].ContainsKey(layer))
				{
					this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)z)][layer] = colour;
				}
				else
				{
					this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)z)].Add(layer, colour);
				}
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0002B224 File Offset: 0x00029624
		public Color GetPoint(uint x, uint y, uint z)
		{
			if (x < 3U && y < 3U && z < 3U)
			{
				Color clear = Color.clear;
				IList<Color> values = this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)z)].Values;
				if (values.Count > 1)
				{
					foreach (Color color in values)
					{
						clear.r = color.a * color.r + (1f - color.a) * clear.a * clear.r;
						clear.g = color.a * color.g + (1f - color.a) * clear.a * clear.g;
						clear.b = color.a * color.b + (1f - color.a) * clear.a * clear.b;
						clear.a = color.a * color.a + (1f - color.a) * clear.a * clear.a;
					}
					return clear;
				}
				if (values.Count == 1)
				{
					return values[0];
				}
			}
			return Color.black;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0002B398 File Offset: 0x00029798
		public Color GetPoint(Vector3Int point)
		{
			return this.GetPoint((uint)point.x, (uint)point.y, (uint)point.z);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0002B3B8 File Offset: 0x000297B8
		public Color GetPoint(uint x, uint y)
		{
			Color a = this.GetPoint(x, y, 0U);
			a += this.GetPoint(x, y, 1U);
			a += this.GetPoint(x, y, 2U);
			return a / 3f;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0002B3FA File Offset: 0x000297FA
		public Color GetPoint(Vector2Int point)
		{
			return this.GetPoint((uint)point.x, (uint)point.y);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0002B410 File Offset: 0x00029810
		public Color GetPointOnLayer(uint x, uint y, int layer)
		{
			return this.GetPointOnLayer(x, y, 0U, layer);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0002B41C File Offset: 0x0002981C
		public Color GetPointOnLayer(uint x, uint y, uint z, int layer)
		{
			Color black = Color.black;
			if (x < 3U && y < 3U && z < 3U)
			{
				this.ColourLayout[(int)((UIntPtr)x)][(int)((UIntPtr)y)][(int)((UIntPtr)z)].TryGetValue(layer, out black);
				return black;
			}
			return black;
		}

		// Token: 0x04000CF0 RID: 3312
		public DeviceType ActiveType = DeviceType.ALL;

		// Token: 0x04000CF1 RID: 3313
		private SortedList<int, Color>[][][] ColourLayout;

		// Token: 0x04000CF2 RID: 3314
		public bool ForceClear;

		// Token: 0x04000CF3 RID: 3315
		protected bool TargetedOverride;

		// Token: 0x04000CF4 RID: 3316
		public bool Used;
	}
}
