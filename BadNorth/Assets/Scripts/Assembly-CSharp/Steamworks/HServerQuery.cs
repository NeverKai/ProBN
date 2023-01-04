using System;

namespace Steamworks
{
	// Token: 0x02000370 RID: 880
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00029156 File Offset: 0x00027556
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0002915F File Offset: 0x0002755F
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00029172 File Offset: 0x00027572
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00029193 File Offset: 0x00027593
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000291A6 File Offset: 0x000275A6
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000291B8 File Offset: 0x000275B8
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x000291C4 File Offset: 0x000275C4
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000291CC File Offset: 0x000275CC
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000291D5 File Offset: 0x000275D5
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000291E6 File Offset: 0x000275E6
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x04000CAC RID: 3244
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000CAD RID: 3245
		public int m_HServerQuery;
	}
}
