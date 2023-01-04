using System;

namespace Steamworks
{
	// Token: 0x02000372 RID: 882
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x000292AB File Offset: 0x000276AB
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000292B4 File Offset: 0x000276B4
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000292C7 File Offset: 0x000276C7
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000292E8 File Offset: 0x000276E8
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000292FB File Offset: 0x000276FB
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0002930D File Offset: 0x0002770D
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00029319 File Offset: 0x00027719
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00029321 File Offset: 0x00027721
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0002932A File Offset: 0x0002772A
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0002933B File Offset: 0x0002773B
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000CAF RID: 3247
		public uint m_SNetSocket;
	}
}
