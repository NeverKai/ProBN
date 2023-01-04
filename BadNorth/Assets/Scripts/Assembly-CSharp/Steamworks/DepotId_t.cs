using System;

namespace Steamworks
{
	// Token: 0x0200037A RID: 890
	[Serializable]
	public struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x0002981D File Offset: 0x00027C1D
		public DepotId_t(uint value)
		{
			this.m_DepotId = value;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00029826 File Offset: 0x00027C26
		public override string ToString()
		{
			return this.m_DepotId.ToString();
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00029839 File Offset: 0x00027C39
		public override bool Equals(object other)
		{
			return other is DepotId_t && this == (DepotId_t)other;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0002985A File Offset: 0x00027C5A
		public override int GetHashCode()
		{
			return this.m_DepotId.GetHashCode();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0002986D File Offset: 0x00027C6D
		public static bool operator ==(DepotId_t x, DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0002987F File Offset: 0x00027C7F
		public static bool operator !=(DepotId_t x, DepotId_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0002988B File Offset: 0x00027C8B
		public static explicit operator DepotId_t(uint value)
		{
			return new DepotId_t(value);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00029893 File Offset: 0x00027C93
		public static explicit operator uint(DepotId_t that)
		{
			return that.m_DepotId;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0002989C File Offset: 0x00027C9C
		public bool Equals(DepotId_t other)
		{
			return this.m_DepotId == other.m_DepotId;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000298AD File Offset: 0x00027CAD
		public int CompareTo(DepotId_t other)
		{
			return this.m_DepotId.CompareTo(other.m_DepotId);
		}

		// Token: 0x04000CBD RID: 3261
		public static readonly DepotId_t Invalid = new DepotId_t(0U);

		// Token: 0x04000CBE RID: 3262
		public uint m_DepotId;
	}
}
