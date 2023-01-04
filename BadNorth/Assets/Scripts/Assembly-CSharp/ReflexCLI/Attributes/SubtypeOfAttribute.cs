using System;

namespace ReflexCLI.Attributes
{
	// Token: 0x02000452 RID: 1106
	[AttributeUsage(AttributeTargets.Parameter)]
	public class SubtypeOfAttribute : Attribute
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0004311A File Offset: 0x0004151A
		private SubtypeOfAttribute()
		{
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00043129 File Offset: 0x00041529
		public SubtypeOfAttribute(Type baseType, bool allowBase = true, bool includeInterfaces = false)
		{
			this.BaseType = baseType;
			this.AllowBase = allowBase;
			this.IncludeInterfaces = includeInterfaces;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00043150 File Offset: 0x00041550
		public bool IsValidType(Type type)
		{
			return (this.AllowBase && type == this.BaseType) || type.IsSubclassOf(this.BaseType) || (this.IncludeInterfaces && type.IsInterface);
		}

		// Token: 0x04000FA9 RID: 4009
		private Type BaseType;

		// Token: 0x04000FAA RID: 4010
		private bool AllowBase = true;

		// Token: 0x04000FAB RID: 4011
		private bool IncludeInterfaces;
	}
}
