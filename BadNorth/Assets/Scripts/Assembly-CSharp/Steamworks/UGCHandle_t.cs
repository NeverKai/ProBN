using System;

namespace Steamworks
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x00029565 File Offset: 0x00027965
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0002956E File Offset: 0x0002796E
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00029581 File Offset: 0x00027981
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000295A2 File Offset: 0x000279A2
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000295B5 File Offset: 0x000279B5
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000295C7 File Offset: 0x000279C7
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x000295D3 File Offset: 0x000279D3
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000295DB File Offset: 0x000279DB
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000295E4 File Offset: 0x000279E4
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000295F5 File Offset: 0x000279F5
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x04000CB6 RID: 3254
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000CB7 RID: 3255
		public ulong m_UGCHandle;
	}
}
