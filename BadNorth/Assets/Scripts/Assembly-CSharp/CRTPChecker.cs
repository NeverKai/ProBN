using System;

// Token: 0x02000444 RID: 1092
public static class CRTPChecker
{
	// Token: 0x060018E8 RID: 6376 RVA: 0x00040F47 File Offset: 0x0003F347
	public static void Check<T>(object instance) where T : class
	{
		if (!(instance is T))
		{
			throw new ApplicationException(string.Format("Type {0} must recur itself but recurred {1}", instance.GetType(), typeof(T)));
		}
	}
}
