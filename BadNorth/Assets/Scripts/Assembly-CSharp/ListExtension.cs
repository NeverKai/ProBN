using System;
using System.Collections.Generic;

// Token: 0x02000621 RID: 1569
public static class ListExtension
{
	// Token: 0x0600285A RID: 10330 RVA: 0x0008578D File Offset: 0x00083B8D
	public static void ReturnToListPool<T>(this List<T> list)
	{
		ListPool<T>.ReturnList(list);
	}
}
