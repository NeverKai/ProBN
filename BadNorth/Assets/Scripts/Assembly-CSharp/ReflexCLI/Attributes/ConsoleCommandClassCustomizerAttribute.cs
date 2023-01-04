using System;

namespace ReflexCLI.Attributes
{
	// Token: 0x02000451 RID: 1105
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class ConsoleCommandClassCustomizerAttribute : Attribute
	{
		// Token: 0x06001947 RID: 6471 RVA: 0x00043103 File Offset: 0x00041503
		public ConsoleCommandClassCustomizerAttribute(string customName)
		{
			this._customName = customName;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00043112 File Offset: 0x00041512
		public string CustomName
		{
			get
			{
				return this._customName;
			}
		}

		// Token: 0x04000FA8 RID: 4008
		private string _customName;
	}
}
