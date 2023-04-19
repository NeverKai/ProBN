using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
public static class BinaryTreePacking
{
	// Token: 0x060026E0 RID: 9952 RVA: 0x0007C5FC File Offset: 0x0007A9FC
	public static int Pack<T>(IEnumerable<T> items, Func<T, Rect> itemToRect, Action<T, Rect> applyRect, int height = 1024, int padding = 2)
	{
		//BinaryTreePacking.<Pack>c__AnonStorey0<T> <Pack>c__AnonStorey = new BinaryTreePacking.<Pack>c__AnonStorey0<T>();
		// <Pack>c__AnonStorey.itemToRect = itemToRect;
		var pool = new List<Rect>();
		int num = 0;
		foreach (T arg in from x in items
		orderby itemToRect(x).width descending
		select x)
		{
			// BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey2 = new BinaryTreePacking.<Pack>c__AnonStorey1<T>();
			// <Pack>c__AnonStorey2.<>f__ref$0 = <Pack>c__AnonStorey;
			
			var inRect = itemToRect(arg);
			//BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey3 = <Pack>c__AnonStorey2;
			inRect.width = inRect.width + (float)(padding * 2);
			//BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey4 = <Pack>c__AnonStorey2;
			inRect.height =inRect.height + (float)(padding * 2);
			Rect arg2;
			if (!pool.Any((Rect x) => x.width >= inRect.width && x.height >= inRect.height))
			{
				arg2 = new Rect((float)num, 0f, inRect.width, inRect.height);
				if (arg2.height < (float)height)
				{
					pool.Add(new Rect((float)num, arg2.height, arg2.width, (float)height - arg2.height));
				}
				num += (int)arg2.width;
			}
			else
			{
				IEnumerable<int> source = Enumerable.Range(0, pool.Count).Where((int x) => pool[x].width >=inRect.width && pool[x].height >= inRect.height);
				int index = source.OrderBy((int x) => Mathf.Min(pool[x].width - inRect.width, pool[x].height - inRect.height)).First<int>();
				Rect rect = pool[index];
				pool.RemoveAt(index);
				arg2 = new Rect(rect.x, rect.y, inRect.width, inRect.height);
				Vector2 lhs = rect.max - arg2.max;
				if (lhs != Vector2.zero)
				{
					if (lhs.x == 0f)
					{
						pool.Add(new Rect(arg2.xMin, arg2.yMax, arg2.width, rect.height - arg2.height));
					}
					else if (lhs.y == 0f)
					{
						pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, arg2.height));
					}
					else if (lhs.x < lhs.y)
					{
						pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, arg2.height));
						pool.Add(new Rect(arg2.xMin, arg2.yMax, rect.width, rect.height - arg2.height));
					}
					else
					{
						pool.Add(new Rect(arg2.xMin, arg2.yMax, arg2.width, rect.height - arg2.height));
						pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, rect.height));
					}
				}
			}
			arg2.width -= (float)(padding * 2);
			arg2.height -= (float)(padding * 2);
			arg2.x += (float)padding;
			arg2.y += (float)padding;
			applyRect(arg, arg2);
		}
		return num;
	}
}
