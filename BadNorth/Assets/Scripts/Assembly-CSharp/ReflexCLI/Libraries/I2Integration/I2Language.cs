using System;

namespace ReflexCLI.Libraries.I2Integration
{
	// Token: 0x02000463 RID: 1123
	internal struct I2Language
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x000436A2 File Offset: 0x00041AA2
		public I2Language(string languageName)
		{
			this.languageName = languageName;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000436AB File Offset: 0x00041AAB
		public override string ToString()
		{
			return this.languageName;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000436B3 File Offset: 0x00041AB3
		public static implicit operator I2Language(string inStr)
		{
			return new I2Language(inStr);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000436BB File Offset: 0x00041ABB
		public static implicit operator string(I2Language langauge)
		{
			return langauge.languageName;
		}

		// Token: 0x04000FCC RID: 4044
		public string languageName;
	}
}
