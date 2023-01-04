using System;

namespace Steamworks
{
	// Token: 0x02000365 RID: 869
	[Serializable]
	public struct ControllerAnalogActionHandle_t : IEquatable<ControllerAnalogActionHandle_t>, IComparable<ControllerAnalogActionHandle_t>
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x000289F8 File Offset: 0x00026DF8
		public ControllerAnalogActionHandle_t(ulong value)
		{
			this.m_ControllerAnalogActionHandle = value;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00028A01 File Offset: 0x00026E01
		public override string ToString()
		{
			return this.m_ControllerAnalogActionHandle.ToString();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00028A14 File Offset: 0x00026E14
		public override bool Equals(object other)
		{
			return other is ControllerAnalogActionHandle_t && this == (ControllerAnalogActionHandle_t)other;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00028A35 File Offset: 0x00026E35
		public override int GetHashCode()
		{
			return this.m_ControllerAnalogActionHandle.GetHashCode();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00028A48 File Offset: 0x00026E48
		public static bool operator ==(ControllerAnalogActionHandle_t x, ControllerAnalogActionHandle_t y)
		{
			return x.m_ControllerAnalogActionHandle == y.m_ControllerAnalogActionHandle;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00028A5A File Offset: 0x00026E5A
		public static bool operator !=(ControllerAnalogActionHandle_t x, ControllerAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00028A66 File Offset: 0x00026E66
		public static explicit operator ControllerAnalogActionHandle_t(ulong value)
		{
			return new ControllerAnalogActionHandle_t(value);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00028A6E File Offset: 0x00026E6E
		public static explicit operator ulong(ControllerAnalogActionHandle_t that)
		{
			return that.m_ControllerAnalogActionHandle;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00028A77 File Offset: 0x00026E77
		public bool Equals(ControllerAnalogActionHandle_t other)
		{
			return this.m_ControllerAnalogActionHandle == other.m_ControllerAnalogActionHandle;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00028A88 File Offset: 0x00026E88
		public int CompareTo(ControllerAnalogActionHandle_t other)
		{
			return this.m_ControllerAnalogActionHandle.CompareTo(other.m_ControllerAnalogActionHandle);
		}

		// Token: 0x04000C9A RID: 3226
		public ulong m_ControllerAnalogActionHandle;
	}
}
