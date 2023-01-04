using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000504 RID: 1284
public static class ArrayExtensions
{
	// Token: 0x060020D9 RID: 8409 RVA: 0x00059A1A File Offset: 0x00057E1A
	public static bool IsValidIndex<T>(this T[] array, int idx)
	{
		return idx >= 0 && idx < array.Length;
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x00059A2C File Offset: 0x00057E2C
	public static bool IsValidIndex<T>(this List<T> list, int idx)
	{
		return idx >= 0 && idx < list.Count;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x00059A44 File Offset: 0x00057E44
	public static bool Find<T>(this T[] array, T element, out int idx) where T : class
	{
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			if (array[i] == element)
			{
				idx = i;
				return true;
			}
			i++;
		}
		idx = -1;
		return false;
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x00059A88 File Offset: 0x00057E88
	public static T[] Clone<T>(this T[] array)
	{
		int num = array.Length;
		T[] array2 = new T[num];
		for (int i = 0; i < num; i++)
		{
			array2[i] = array[i];
		}
		return array2;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x00059AC4 File Offset: 0x00057EC4
	public static List<T> Clone<T>(this List<T> list)
	{
		int count = list.Count;
		List<T> list2 = new List<T>(count);
		for (int i = 0; i < count; i++)
		{
			list2.Add(list[i]);
		}
		return list2;
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x00059B00 File Offset: 0x00057F00
	public static T Last<T>(this List<T> list)
	{
		return (list.Count <= 0) ? default(T) : list[list.Count - 1];
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x00059B38 File Offset: 0x00057F38
	public static T Pop<T>(this List<T> list)
	{
		int num = list.Count - 1;
		if (num >= 0)
		{
			T result = list[num];
			list.RemoveAt(num);
			return result;
		}
		return default(T);
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x00059B6F File Offset: 0x00057F6F
	public static T GetElementClamped<T>(this T[] array, int idx)
	{
		return array[Mathf.Clamp(idx, 0, array.Length - 1)];
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x00059B83 File Offset: 0x00057F83
	public static T GetElementClamped<T>(this List<T> list, int idx)
	{
		return list[Mathf.Clamp(idx, 0, list.Count - 1)];
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x00059B9C File Offset: 0x00057F9C
	public static void Shuffle<T>(this List<T> collection)
	{
		int i = 0;
		int count = collection.Count;
		while (i < count - 1)
		{
			int index = UnityEngine.Random.Range(i, count);
			T value = collection[i];
			collection[i] = collection[index];
			collection[index] = value;
			i++;
		}
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x00059BEC File Offset: 0x00057FEC
	public static void Shuffle<T>(this T[] collection)
	{
		int i = 0;
		int num = collection.Length;
		while (i < num - 1)
		{
			int num2 = UnityEngine.Random.Range(i, num);
			T t = collection[i];
			collection[i] = collection[num2];
			collection[num2] = t;
			i++;
		}
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x00059C38 File Offset: 0x00058038
	public static void ShuffleWeighted<T>(this List<T> collection, Dictionary<T, float> weights, Func<T, float> weightFunc)
	{
		using (new ScopedProfiler("ShuffleWeighted", null))
		{
			ArrayExtensions.AssignWeights<T>(collection, weights, weightFunc);
			collection.Sort((T a, T b) => Math.Sign(weights[b] - weights[a]));
		}
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x00059CA4 File Offset: 0x000580A4
	public static void ShuffleWeighted<T>(this T[] collection, Dictionary<T, float> weights, Func<T, float> weightFunc)
	{
		using (new ScopedProfiler("ShuffleWeighted", null))
		{
			ArrayExtensions.AssignWeights<T>(collection, weights, weightFunc);
			Array.Sort<T>(collection, (T a, T b) => Math.Sign(weights[b] - weights[a]));
		}
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x00059D10 File Offset: 0x00058110
	private static void AssignWeights<T>(IEnumerable<T> collection, Dictionary<T, float> weights, Func<T, float> weightFunc)
	{
		weights.Clear();
		foreach (T t in collection)
		{
			float num = weightFunc(t);
			float value = num * UnityEngine.Random.value;
			weights.Add(t, value);
		}
	}
}
