using System;

namespace Steamworks
{
	// Token: 0x0200036D RID: 877
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x00028F59 File Offset: 0x00027359
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00028F62 File Offset: 0x00027362
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00028F75 File Offset: 0x00027375
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00028F96 File Offset: 0x00027396
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00028FA9 File Offset: 0x000273A9
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00028FBB File Offset: 0x000273BB
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00028FC7 File Offset: 0x000273C7
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00028FCF File Offset: 0x000273CF
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00028FD8 File Offset: 0x000273D8
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00028FE9 File Offset: 0x000273E9
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000CA7 RID: 3239
		public int m_SteamItemDef;
	}
}
