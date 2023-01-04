using System;

namespace Steamworks
{
	// Token: 0x02000366 RID: 870
	[Serializable]
	public struct ControllerDigitalActionHandle_t : IEquatable<ControllerDigitalActionHandle_t>, IComparable<ControllerDigitalActionHandle_t>
	{
		// Token: 0x0600135F RID: 4959 RVA: 0x00028A9C File Offset: 0x00026E9C
		public ControllerDigitalActionHandle_t(ulong value)
		{
			this.m_ControllerDigitalActionHandle = value;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00028AA5 File Offset: 0x00026EA5
		public override string ToString()
		{
			return this.m_ControllerDigitalActionHandle.ToString();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00028AB8 File Offset: 0x00026EB8
		public override bool Equals(object other)
		{
			return other is ControllerDigitalActionHandle_t && this == (ControllerDigitalActionHandle_t)other;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00028AD9 File Offset: 0x00026ED9
		public override int GetHashCode()
		{
			return this.m_ControllerDigitalActionHandle.GetHashCode();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00028AEC File Offset: 0x00026EEC
		public static bool operator ==(ControllerDigitalActionHandle_t x, ControllerDigitalActionHandle_t y)
		{
			return x.m_ControllerDigitalActionHandle == y.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00028AFE File Offset: 0x00026EFE
		public static bool operator !=(ControllerDigitalActionHandle_t x, ControllerDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00028B0A File Offset: 0x00026F0A
		public static explicit operator ControllerDigitalActionHandle_t(ulong value)
		{
			return new ControllerDigitalActionHandle_t(value);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00028B12 File Offset: 0x00026F12
		public static explicit operator ulong(ControllerDigitalActionHandle_t that)
		{
			return that.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00028B1B File Offset: 0x00026F1B
		public bool Equals(ControllerDigitalActionHandle_t other)
		{
			return this.m_ControllerDigitalActionHandle == other.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00028B2C File Offset: 0x00026F2C
		public int CompareTo(ControllerDigitalActionHandle_t other)
		{
			return this.m_ControllerDigitalActionHandle.CompareTo(other.m_ControllerDigitalActionHandle);
		}

		// Token: 0x04000C9B RID: 3227
		public ulong m_ControllerDigitalActionHandle;
	}
}
