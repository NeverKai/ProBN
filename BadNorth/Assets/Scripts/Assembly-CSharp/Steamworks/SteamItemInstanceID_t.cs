using System;

namespace Steamworks
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x060013B4 RID: 5044 RVA: 0x00028FFD File Offset: 0x000273FD
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00029006 File Offset: 0x00027406
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00029019 File Offset: 0x00027419
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0002903A File Offset: 0x0002743A
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0002904D File Offset: 0x0002744D
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0002905F File Offset: 0x0002745F
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0002906B File Offset: 0x0002746B
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00029073 File Offset: 0x00027473
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0002907C File Offset: 0x0002747C
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0002908D File Offset: 0x0002748D
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x04000CA8 RID: 3240
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000CA9 RID: 3241
		public ulong m_SteamItemInstanceID;
	}
}
