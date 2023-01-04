using System;

namespace I2.Loc
{
	// Token: 0x0200041A RID: 1050
	internal class TashkeelLocation
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x0003DF58 File Offset: 0x0003C358
		public TashkeelLocation(char tashkeel, int position)
		{
			this.tashkeel = tashkeel;
			this.position = position;
		}

		// Token: 0x04000F1A RID: 3866
		public char tashkeel;

		// Token: 0x04000F1B RID: 3867
		public int position;
	}
}
