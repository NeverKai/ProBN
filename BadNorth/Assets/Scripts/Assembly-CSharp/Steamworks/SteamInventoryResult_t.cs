using System;

namespace Steamworks
{
	// Token: 0x0200036C RID: 876
	[Serializable]
	public struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x00028EA8 File Offset: 0x000272A8
		public SteamInventoryResult_t(int value)
		{
			this.m_SteamInventoryResult = value;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00028EB1 File Offset: 0x000272B1
		public override string ToString()
		{
			return this.m_SteamInventoryResult.ToString();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00028EC4 File Offset: 0x000272C4
		public override bool Equals(object other)
		{
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00028EE5 File Offset: 0x000272E5
		public override int GetHashCode()
		{
			return this.m_SteamInventoryResult.GetHashCode();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00028EF8 File Offset: 0x000272F8
		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00028F0A File Offset: 0x0002730A
		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00028F16 File Offset: 0x00027316
		public static explicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t(value);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00028F1E File Offset: 0x0002731E
		public static explicit operator int(SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00028F27 File Offset: 0x00027327
		public bool Equals(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00028F38 File Offset: 0x00027338
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}

		// Token: 0x04000CA5 RID: 3237
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);

		// Token: 0x04000CA6 RID: 3238
		public int m_SteamInventoryResult;
	}
}
