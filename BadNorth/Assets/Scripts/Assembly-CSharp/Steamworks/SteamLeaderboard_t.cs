using System;

namespace Steamworks
{
	// Token: 0x02000380 RID: 896
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x00029C48 File Offset: 0x00028048
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00029C51 File Offset: 0x00028051
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00029C64 File Offset: 0x00028064
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00029C85 File Offset: 0x00028085
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00029C98 File Offset: 0x00028098
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00029CAA File Offset: 0x000280AA
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00029CB6 File Offset: 0x000280B6
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00029CBE File Offset: 0x000280BE
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00029CC7 File Offset: 0x000280C7
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00029CD8 File Offset: 0x000280D8
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000CC9 RID: 3273
		public ulong m_SteamLeaderboard;
	}
}
