using System;

namespace ReflexCLI.Parameters
{
	// Token: 0x02000456 RID: 1110
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ParameterProcessorAttribute : Attribute
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x000431F4 File Offset: 0x000415F4
		public ParameterProcessorAttribute(Type type)
		{
			this._ProcessedType = type;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00043203 File Offset: 0x00041603
		public Type ProcessedType
		{
			get
			{
				return this._ProcessedType;
			}
		}

		// Token: 0x04000FB0 RID: 4016
		private Type _ProcessedType;
	}
}
