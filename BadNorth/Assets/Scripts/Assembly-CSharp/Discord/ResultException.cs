using System;

namespace Discord
{
	// Token: 0x020000E8 RID: 232
	public class ResultException : Exception
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x0001C8B2 File Offset: 0x0001ACB2
		public ResultException(Result result) : base(result.ToString())
		{
		}

		// Token: 0x040003EE RID: 1006
		public readonly Result Result;
	}
}
