using System;
using System.Collections.Generic;

// Token: 0x020005E0 RID: 1504
public static class Enumeration
{
	// Token: 0x06002707 RID: 9991 RVA: 0x0007D778 File Offset: 0x0007BB78
	public static IEnumerable<T> Traverse<T>(T root, Func<T, IEnumerable<T>> children)
	{
		Stack<T> stack = new Stack<T>();
		stack.Push(root);
		while (stack.Count != 0)
		{
			T item = stack.Pop();
			yield return item;
			foreach (T t in children(item))
			{
				stack.Push(t);
			}
		}
		yield break;
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x0007D7A4 File Offset: 0x0007BBA4
	public static IEnumerable<T> Closure<T>(T root, Func<T, IEnumerable<T>> children)
	{
		HashSet<T> seen = new HashSet<T>();
		Stack<T> stack = new Stack<T>();
		stack.Push(root);
		while (stack.Count != 0)
		{
			T item = stack.Pop();
			if (!seen.Contains(item))
			{
				seen.Add(item);
				yield return item;
				foreach (T t in children(item))
				{
					stack.Push(t);
				}
			}
		}
		yield break;
	}
}
