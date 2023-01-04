using System;

namespace Steamworks
{
	// Token: 0x0200037D RID: 893
	[Serializable]
	public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06001455 RID: 5205 RVA: 0x00029A32 File Offset: 0x00027E32
		public UGCQueryHandle_t(ulong value)
		{
			this.m_UGCQueryHandle = value;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00029A3B File Offset: 0x00027E3B
		public override string ToString()
		{
			return this.m_UGCQueryHandle.ToString();
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00029A4E File Offset: 0x00027E4E
		public override bool Equals(object other)
		{
			return other is UGCQueryHandle_t && this == (UGCQueryHandle_t)other;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00029A6F File Offset: 0x00027E6F
		public override int GetHashCode()
		{
			return this.m_UGCQueryHandle.GetHashCode();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00029A82 File Offset: 0x00027E82
		public static bool operator ==(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00029A94 File Offset: 0x00027E94
		public static bool operator !=(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00029AA0 File Offset: 0x00027EA0
		public static explicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t(value);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00029AA8 File Offset: 0x00027EA8
		public static explicit operator ulong(UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00029AB1 File Offset: 0x00027EB1
		public bool Equals(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00029AC2 File Offset: 0x00027EC2
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}

		// Token: 0x04000CC3 RID: 3267
		public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(ulong.MaxValue);

		// Token: 0x04000CC4 RID: 3268
		public ulong m_UGCQueryHandle;
	}
}
