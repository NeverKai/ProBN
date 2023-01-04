using System;

namespace InspectorExpressions
{
	// Token: 0x0200042F RID: 1071
	public class Bool : IBool
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x0003F93C File Offset: 0x0003DD3C
		public Bool(bool aValue)
		{
			this.m_Value = aValue;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0003F94B File Offset: 0x0003DD4B
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0003F953 File Offset: 0x0003DD53
		public bool BoolValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0003F95C File Offset: 0x0003DD5C
		public override string ToString()
		{
			return (!this.m_Value) ? "False" : "True";
		}

		// Token: 0x04000F33 RID: 3891
		private bool m_Value;
	}
}
