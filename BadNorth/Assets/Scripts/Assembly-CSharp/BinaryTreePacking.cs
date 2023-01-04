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
		BinaryTreePacking.<Pack>c__AnonStorey0<T> <Pack>c__AnonStorey = new BinaryTreePacking.<Pack>c__AnonStorey0<T>();
		<Pack>c__AnonStorey.itemToRect = itemToRect;
		<Pack>c__AnonStorey.pool = new List<Rect>();
		int num = 0;
		foreach (T arg in from x in items
		orderby <Pack>c__AnonStorey.itemToRect(x).width descending
		select x)
		{
			BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey2 = new BinaryTreePacking.<Pack>c__AnonStorey1<T>();
			<Pack>c__AnonStorey2.<>f__ref$0 = <Pack>c__AnonStorey;
			<Pack>c__AnonStorey2.inRect = <Pack>c__AnonStorey.itemToRect(arg);
			BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey3 = <Pack>c__AnonStorey2;
			<Pack>c__AnonStorey3.inRect.width = <Pack>c__AnonStorey3.inRect.width + (float)(padding * 2);
			BinaryTreePacking.<Pack>c__AnonStorey1<T> <Pack>c__AnonStorey4 = <Pack>c__AnonStorey2;
			<Pack>c__AnonStorey4.inRect.height = <Pack>c__AnonStorey4.inRect.height + (float)(padding * 2);
			Rect arg2;
			if (!<Pack>c__AnonStorey.pool.Any((Rect x) => x.width >= <Pack>c__AnonStorey2.inRect.width && x.height >= <Pack>c__AnonStorey2.inRect.height))
			{
				arg2 = new Rect((float)num, 0f, <Pack>c__AnonStorey2.inRect.width, <Pack>c__AnonStorey2.inRect.height);
				if (arg2.height < (float)height)
				{
					<Pack>c__AnonStorey.pool.Add(new Rect((float)num, arg2.height, arg2.width, (float)height - arg2.height));
				}
				num += (int)arg2.width;
			}
			else
			{
				IEnumerable<int> source = Enumerable.Range(0, <Pack>c__AnonStorey.pool.Count).Where((int x) => <Pack>c__AnonStorey2.<>f__ref$0.pool[x].width >= <Pack>c__AnonStorey2.inRect.width && <Pack>c__AnonStorey2.<>f__ref$0.pool[x].height >= <Pack>c__AnonStorey2.inRect.height);
				int index = source.OrderBy((int x) => Mathf.Min(<Pack>c__AnonStorey2.<>f__ref$0.pool[x].width - <Pack>c__AnonStorey2.inRect.width, <Pack>c__AnonStorey2.<>f__ref$0.pool[x].height - <Pack>c__AnonStorey2.inRect.height)).First<int>();
				Rect rect = <Pack>c__AnonStorey.pool[index];
				<Pack>c__AnonStorey.pool.RemoveAt(index);
				arg2 = new Rect(rect.x, rect.y, <Pack>c__AnonStorey2.inRect.width, <Pack>c__AnonStorey2.inRect.height);
				Vector2 lhs = rect.max - arg2.max;
				if (lhs != Vector2.zero)
				{
					if (lhs.x == 0f)
					{
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMin, arg2.yMax, arg2.width, rect.height - arg2.height));
					}
					else if (lhs.y == 0f)
					{
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, arg2.height));
					}
					else if (lhs.x < lhs.y)
					{
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, arg2.height));
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMin, arg2.yMax, rect.width, rect.height - arg2.height));
					}
					else
					{
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMin, arg2.yMax, arg2.width, rect.height - arg2.height));
						<Pack>c__AnonStorey.pool.Add(new Rect(arg2.xMax, arg2.yMin, rect.width - arg2.width, rect.height));
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
