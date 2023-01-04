using System;
using UnityEngine;

// Token: 0x0200050C RID: 1292
public static class RectExtensions
{
	// Token: 0x06002133 RID: 8499 RVA: 0x0005B4D8 File Offset: 0x000598D8
	public static Rect[] SplitHorizontal(this Rect rect, params float[] widths)
	{
		Rect[] array = new Rect[widths.Length];
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < widths.Length; i++)
		{
			if (widths[i] < 0f)
			{
				num2 += widths[i];
			}
			else
			{
				num += widths[i];
			}
		}
		float num3 = rect.width - num;
		float num4 = rect.x;
		for (int j = 0; j < widths.Length; j++)
		{
			if (widths[j] < 0f)
			{
				widths[j] = num3 * widths[j] / num2;
			}
			array[j] = rect;
			array[j].x = num4;
			array[j].width = widths[j];
			num4 += widths[j];
		}
		return array;
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x0005B5AC File Offset: 0x000599AC
	public static Rect[] SplitVertical(this Rect rect, params float[] heights)
	{
		Rect[] array = new Rect[heights.Length];
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < heights.Length; i++)
		{
			if (heights[i] < 0f)
			{
				num2 += heights[i];
			}
			else
			{
				num += heights[i];
			}
		}
		float num3 = rect.height - num;
		float num4 = rect.y;
		for (int j = 0; j < heights.Length; j++)
		{
			if (heights[j] < 0f)
			{
				heights[j] = num3 * heights[j] / num2;
			}
			array[j] = rect;
			array[j].y = num4;
			array[j].height = heights[j];
			num4 += heights[j];
		}
		return array;
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x0005B680 File Offset: 0x00059A80
	public static Rect[] SplitHorizontal(this Rect rect, int numEntries)
	{
		float[] array = new float[numEntries];
		for (int i = 0; i < numEntries; i++)
		{
			array[i] = -1f;
		}
		return rect.SplitHorizontal(array);
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x0005B6B8 File Offset: 0x00059AB8
	public static Rect[] SplitVertical(this Rect rect, int numEntries)
	{
		float[] array = new float[numEntries];
		for (int i = 0; i < numEntries; i++)
		{
			array[i] = -1f;
		}
		return rect.SplitVertical(array);
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x0005B6ED File Offset: 0x00059AED
	public static float GetArea(this Rect rect)
	{
		return rect.width * rect.height;
	}
}
