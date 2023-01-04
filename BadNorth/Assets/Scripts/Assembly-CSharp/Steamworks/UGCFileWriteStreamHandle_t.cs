using System;

namespace Steamworks
{
	// Token: 0x02000375 RID: 885
	[Serializable]
	public struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x000294B3 File Offset: 0x000278B3
		public UGCFileWriteStreamHandle_t(ulong value)
		{
			this.m_UGCFileWriteStreamHandle = value;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000294BC File Offset: 0x000278BC
		public override string ToString()
		{
			return this.m_UGCFileWriteStreamHandle.ToString();
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000294CF File Offset: 0x000278CF
		public override bool Equals(object other)
		{
			return other is UGCFileWriteStreamHandle_t && this == (UGCFileWriteStreamHandle_t)other;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000294F0 File Offset: 0x000278F0
		public override int GetHashCode()
		{
			return this.m_UGCFileWriteStreamHandle.GetHashCode();
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00029503 File Offset: 0x00027903
		public static bool operator ==(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00029515 File Offset: 0x00027915
		public static bool operator !=(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00029521 File Offset: 0x00027921
		public static explicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t(value);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00029529 File Offset: 0x00027929
		public static explicit operator ulong(UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00029532 File Offset: 0x00027932
		public bool Equals(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00029543 File Offset: 0x00027943
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}

		// Token: 0x04000CB4 RID: 3252
		public static readonly UGCFileWriteStreamHandle_t Invalid = new UGCFileWriteStreamHandle_t(ulong.MaxValue);

		// Token: 0x04000CB5 RID: 3253
		public ulong m_UGCFileWriteStreamHandle;
	}
}
