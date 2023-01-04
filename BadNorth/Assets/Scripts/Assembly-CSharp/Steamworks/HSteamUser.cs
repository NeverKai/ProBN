using System;

namespace Steamworks
{
	// Token: 0x0200035D RID: 861
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x0002810B File Offset: 0x0002650B
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00028114 File Offset: 0x00026514
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00028127 File Offset: 0x00026527
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00028148 File Offset: 0x00026548
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0002815B File Offset: 0x0002655B
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0002816D File Offset: 0x0002656D
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00028179 File Offset: 0x00026579
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00028181 File Offset: 0x00026581
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0002818A File Offset: 0x0002658A
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0002819B File Offset: 0x0002659B
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000C8A RID: 3210
		public int m_HSteamUser;
	}
}
