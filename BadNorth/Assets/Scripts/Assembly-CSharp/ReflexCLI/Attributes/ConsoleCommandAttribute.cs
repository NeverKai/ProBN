using System;

namespace ReflexCLI.Attributes
{
	// Token: 0x02000450 RID: 1104
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class ConsoleCommandAttribute : Attribute
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x000430EC File Offset: 0x000414EC
		public ConsoleCommandAttribute(string customName = "")
		{
			this._customName = customName;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000430FB File Offset: 0x000414FB
		public string CustomName
		{
			get
			{
				return this._customName;
			}
		}

		// Token: 0x04000FA7 RID: 4007
		private string _customName;
	}
}
