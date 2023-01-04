using System;

namespace Steamworks
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x00029207 File Offset: 0x00027607
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00029210 File Offset: 0x00027610
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00029223 File Offset: 0x00027623
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00029244 File Offset: 0x00027644
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00029257 File Offset: 0x00027657
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00029269 File Offset: 0x00027669
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00029275 File Offset: 0x00027675
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0002927D File Offset: 0x0002767D
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00029286 File Offset: 0x00027686
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00029297 File Offset: 0x00027697
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000CAE RID: 3246
		public uint m_SNetListenSocket;
	}
}
