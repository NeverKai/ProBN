using System;

namespace Steamworks
{
	// Token: 0x02000381 RID: 897
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06001480 RID: 5248 RVA: 0x00029CEC File Offset: 0x000280EC
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00029CF5 File Offset: 0x000280F5
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00029D08 File Offset: 0x00028108
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00029D29 File Offset: 0x00028129
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00029D3C File Offset: 0x0002813C
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00029D4E File Offset: 0x0002814E
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00029D5A File Offset: 0x0002815A
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00029D62 File Offset: 0x00028162
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00029D6B File Offset: 0x0002816B
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00029D7C File Offset: 0x0002817C
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000CCA RID: 3274
		public ulong m_SteamLeaderboardEntries;
	}
}
