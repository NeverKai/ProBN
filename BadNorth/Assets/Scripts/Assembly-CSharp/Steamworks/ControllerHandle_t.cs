using System;

namespace Steamworks
{
	// Token: 0x02000367 RID: 871
	[Serializable]
	public struct ControllerHandle_t : IEquatable<ControllerHandle_t>, IComparable<ControllerHandle_t>
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x00028B40 File Offset: 0x00026F40
		public ControllerHandle_t(ulong value)
		{
			this.m_ControllerHandle = value;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00028B49 File Offset: 0x00026F49
		public override string ToString()
		{
			return this.m_ControllerHandle.ToString();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00028B5C File Offset: 0x00026F5C
		public override bool Equals(object other)
		{
			return other is ControllerHandle_t && this == (ControllerHandle_t)other;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00028B7D File Offset: 0x00026F7D
		public override int GetHashCode()
		{
			return this.m_ControllerHandle.GetHashCode();
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00028B90 File Offset: 0x00026F90
		public static bool operator ==(ControllerHandle_t x, ControllerHandle_t y)
		{
			return x.m_ControllerHandle == y.m_ControllerHandle;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00028BA2 File Offset: 0x00026FA2
		public static bool operator !=(ControllerHandle_t x, ControllerHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00028BAE File Offset: 0x00026FAE
		public static explicit operator ControllerHandle_t(ulong value)
		{
			return new ControllerHandle_t(value);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00028BB6 File Offset: 0x00026FB6
		public static explicit operator ulong(ControllerHandle_t that)
		{
			return that.m_ControllerHandle;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00028BBF File Offset: 0x00026FBF
		public bool Equals(ControllerHandle_t other)
		{
			return this.m_ControllerHandle == other.m_ControllerHandle;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00028BD0 File Offset: 0x00026FD0
		public int CompareTo(ControllerHandle_t other)
		{
			return this.m_ControllerHandle.CompareTo(other.m_ControllerHandle);
		}

		// Token: 0x04000C9C RID: 3228
		public ulong m_ControllerHandle;
	}
}
