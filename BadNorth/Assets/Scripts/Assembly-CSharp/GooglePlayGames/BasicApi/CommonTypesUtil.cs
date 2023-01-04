using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020003A2 RID: 930
	public class CommonTypesUtil
	{
		// Token: 0x0600150B RID: 5387 RVA: 0x0002BAD6 File Offset: 0x00029ED6
		public static bool StatusIsSuccess(ResponseStatus status)
		{
			return status > (ResponseStatus)0;
		}
	}
}
