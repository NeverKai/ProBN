using System;

namespace Steamworks
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x00029980 File Offset: 0x00027D80
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00029989 File Offset: 0x00027D89
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0002999C File Offset: 0x00027D9C
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000299BD File Offset: 0x00027DBD
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000299D0 File Offset: 0x00027DD0
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000299E2 File Offset: 0x00027DE2
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000299EE File Offset: 0x00027DEE
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000299F6 File Offset: 0x00027DF6
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000299FF File Offset: 0x00027DFF
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00029A10 File Offset: 0x00027E10
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x04000CC1 RID: 3265
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000CC2 RID: 3266
		public ulong m_SteamAPICall;
	}
}
