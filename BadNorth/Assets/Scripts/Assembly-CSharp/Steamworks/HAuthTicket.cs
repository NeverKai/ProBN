using System;

namespace Steamworks
{
	// Token: 0x02000363 RID: 867
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x06001340 RID: 4928 RVA: 0x000288A3 File Offset: 0x00026CA3
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000288AC File Offset: 0x00026CAC
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000288BF File Offset: 0x00026CBF
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000288E0 File Offset: 0x00026CE0
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000288F3 File Offset: 0x00026CF3
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00028905 File Offset: 0x00026D05
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00028911 File Offset: 0x00026D11
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00028919 File Offset: 0x00026D19
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00028922 File Offset: 0x00026D22
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00028933 File Offset: 0x00026D33
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x04000C97 RID: 3223
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000C98 RID: 3224
		public uint m_HAuthTicket;
	}
}
