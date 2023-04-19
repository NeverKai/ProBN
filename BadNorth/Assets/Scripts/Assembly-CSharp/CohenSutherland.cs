using System;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class CohenSutherland
{
	// Token: 0x060021CA RID: 8650 RVA: 0x0005EC28 File Offset: 0x0005D028
	private static byte ComputeOutCode(Rect extents, double x, double y)
	{
		byte b = 0;
		if (x < (double)extents.min.x)
		{
			b |= 1;
		}
		else if (x > (double)extents.max.x)
		{
			b |= 2;
		}
		if (y < (double)extents.min.y)
		{
			b |= 4;
		}
		else if (y > (double)extents.max.y)
		{
			b |= 8;
		}
		return b;
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x0005ECB0 File Offset: 0x0005D0B0
	public static bool Clip(Rect extents, ref Vector2 p0, ref Vector2 p1, ref float t0, ref float t1)
	{
		float num = p0.x;
		float num2 = p0.y;
		float num3 = p1.x;
		float num4 = p1.y;
		byte b = CohenSutherland.ComputeOutCode(extents, (double)num, (double)num2);
		byte b2 = CohenSutherland.ComputeOutCode(extents, (double)num3, (double)num4);
		bool flag = false;
		while ((b | b2) != 0)
		{
			if ((b & b2) != 0)
			{
				IL_1A7:
				if (flag)
				{
					if (num != num3)
					{
						t0 = 1f - (p1.x - num) / (p1.x - p0.x);
						t1 = (p0.x - num3) / (p0.x - p1.x);
					}
					else
					{
						t0 = 1f - (p1.y - num2) / (p1.y - p0.y);
						t1 = (p0.y - num4) / (p0.y - p1.y);
					}
					p0 = new Vector2(num, num2);
					p1 = new Vector2(num3, num4);
				}
				return flag;
			}
			byte b3 = (b == 0) ? b2 : b;
			float num5;
			float num6;
			if ((b3 & 8) != 0)
			{
				num5 = num + (num3 - num) * (extents.max.y - num2) / (num4 - num2);
				num6 = extents.max.y;
			}
			else if ((b3 & 4) != 0)
			{
				num5 = num + (num3 - num) * (extents.min.y - num2) / (num4 - num2);
				num6 = extents.min.y;
			}
			else if ((b3 & 2) != 0)
			{
				num6 = num2 + (num4 - num2) * (extents.max.x - num) / (num3 - num);
				num5 = extents.max.x;
			}
			else if ((b3 & 1) != 0)
			{
				num6 = num2 + (num4 - num2) * (extents.min.x - num) / (num3 - num);
				num5 = extents.min.x;
			}
			else
			{
				num5 = float.NaN;
				num6 = float.NaN;
			}
			if (b3 == b)
			{
				num = num5;
				num2 = num6;
				b = CohenSutherland.ComputeOutCode(extents, (double)num, (double)num2);
			}
			else
			{
				num3 = num5;
				num4 = num6;
				b2 = CohenSutherland.ComputeOutCode(extents, (double)num3, (double)num4);
			}
		}
		flag = true;
		//goto IL_1A7;
		return flag;
	}

	// Token: 0x04001482 RID: 5250
	private const byte INSIDE = 0;

	// Token: 0x04001483 RID: 5251
	private const byte LEFT = 1;

	// Token: 0x04001484 RID: 5252
	private const byte RIGHT = 2;

	// Token: 0x04001485 RID: 5253
	private const byte BOTTOM = 4;

	// Token: 0x04001486 RID: 5254
	private const byte TOP = 8;
}
