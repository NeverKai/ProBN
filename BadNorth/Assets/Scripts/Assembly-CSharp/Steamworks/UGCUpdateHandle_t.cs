using System;

namespace Steamworks
{
	// Token: 0x0200037E RID: 894
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00029AE4 File Offset: 0x00027EE4
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00029AED File Offset: 0x00027EED
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00029B00 File Offset: 0x00027F00
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00029B21 File Offset: 0x00027F21
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00029B34 File Offset: 0x00027F34
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00029B46 File Offset: 0x00027F46
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00029B52 File Offset: 0x00027F52
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00029B5A File Offset: 0x00027F5A
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00029B63 File Offset: 0x00027F63
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00029B74 File Offset: 0x00027F74
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x04000CC5 RID: 3269
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000CC6 RID: 3270
		public ulong m_UGCUpdateHandle;
	}
}
