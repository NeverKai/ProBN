using System;

namespace Steamworks
{
	// Token: 0x02000364 RID: 868
	[Serializable]
	public struct ControllerActionSetHandle_t : IEquatable<ControllerActionSetHandle_t>, IComparable<ControllerActionSetHandle_t>
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x00028954 File Offset: 0x00026D54
		public ControllerActionSetHandle_t(ulong value)
		{
			this.m_ControllerActionSetHandle = value;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0002895D File Offset: 0x00026D5D
		public override string ToString()
		{
			return this.m_ControllerActionSetHandle.ToString();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00028970 File Offset: 0x00026D70
		public override bool Equals(object other)
		{
			return other is ControllerActionSetHandle_t && this == (ControllerActionSetHandle_t)other;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00028991 File Offset: 0x00026D91
		public override int GetHashCode()
		{
			return this.m_ControllerActionSetHandle.GetHashCode();
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000289A4 File Offset: 0x00026DA4
		public static bool operator ==(ControllerActionSetHandle_t x, ControllerActionSetHandle_t y)
		{
			return x.m_ControllerActionSetHandle == y.m_ControllerActionSetHandle;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000289B6 File Offset: 0x00026DB6
		public static bool operator !=(ControllerActionSetHandle_t x, ControllerActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000289C2 File Offset: 0x00026DC2
		public static explicit operator ControllerActionSetHandle_t(ulong value)
		{
			return new ControllerActionSetHandle_t(value);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000289CA File Offset: 0x00026DCA
		public static explicit operator ulong(ControllerActionSetHandle_t that)
		{
			return that.m_ControllerActionSetHandle;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000289D3 File Offset: 0x00026DD3
		public bool Equals(ControllerActionSetHandle_t other)
		{
			return this.m_ControllerActionSetHandle == other.m_ControllerActionSetHandle;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000289E4 File Offset: 0x00026DE4
		public int CompareTo(ControllerActionSetHandle_t other)
		{
			return this.m_ControllerActionSetHandle.CompareTo(other.m_ControllerActionSetHandle);
		}

		// Token: 0x04000C99 RID: 3225
		public ulong m_ControllerActionSetHandle;
	}
}
