using System;

namespace ReflexCLI.Libraries.I2Integration
{
	// Token: 0x02000466 RID: 1126
	internal struct I2Term
	{
		// Token: 0x06001998 RID: 6552 RVA: 0x0004381A File Offset: 0x00041C1A
		public I2Term(string term)
		{
			this.term = term;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00043823 File Offset: 0x00041C23
		public override string ToString()
		{
			return this.term;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0004382B File Offset: 0x00041C2B
		public static implicit operator I2Term(string inStr)
		{
			return new I2Term(inStr);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00043833 File Offset: 0x00041C33
		public static implicit operator string(I2Term term)
		{
			return term.term;
		}

		// Token: 0x04000FCE RID: 4046
		public string term;
	}
}
