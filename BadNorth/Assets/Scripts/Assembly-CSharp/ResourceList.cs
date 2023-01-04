using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
public static class ResourceList<T> where T : UnityEngine.Object
{
	// Token: 0x06002753 RID: 10067 RVA: 0x0007F634 File Offset: 0x0007DA34
	static ResourceList()
	{
		T[] array = Resources.LoadAll<T>(string.Empty);
		foreach (T t in array)
		{
			ResourceList<T>.dictionary.Add(t.name, t);
			ResourceList<T>.list.Add(t);
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x0007F6A4 File Offset: 0x0007DAA4
	public static T Get(string key)
	{
		T t;
		return (!ResourceList<T>.dictionary.TryGetValue(key, out t)) ? ((T)((object)null)) : t;
	}

	// Token: 0x04001933 RID: 6451
	public static readonly Dictionary<string, T> dictionary = new Dictionary<string, T>();

	// Token: 0x04001934 RID: 6452
	public static readonly List<T> list = new List<T>();
}
