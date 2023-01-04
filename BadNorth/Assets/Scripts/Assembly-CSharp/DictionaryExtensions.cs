using System;
using System.Collections.Generic;

// Token: 0x02000506 RID: 1286
public static class DictionaryExtensions
{
	// Token: 0x060020EB RID: 8427 RVA: 0x00059E52 File Offset: 0x00058252
	public static TValue GetOrCreateKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : class, new()
	{
		if (!dictionary.ContainsKey(key))
		{
			dictionary.Add(key, Activator.CreateInstance<TValue>());
		}
		return dictionary[key];
	}
}
