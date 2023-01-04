using System;

namespace ReflexCLI.Parameters
{
	// Token: 0x02000457 RID: 1111
	public class Suggestion
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x0004320B File Offset: 0x0004160B
		public Suggestion(string value, string display)
		{
			this.Value = value;
			this.Display = display;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00043221 File Offset: 0x00041621
		public static implicit operator Suggestion(string value)
		{
			return new Suggestion(value, value);
		}

		// Token: 0x04000FB1 RID: 4017
		public string Value;

		// Token: 0x04000FB2 RID: 4018
		public string Display;
	}
}
