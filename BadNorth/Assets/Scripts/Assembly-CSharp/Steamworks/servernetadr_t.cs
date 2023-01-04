using System;

namespace Steamworks
{
	// Token: 0x0200035B RID: 859
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x00027DF5 File Offset: 0x000261F5
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00027E0C File Offset: 0x0002620C
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00027E14 File Offset: 0x00026214
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00027E1D File Offset: 0x0002621D
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00027E25 File Offset: 0x00026225
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00027E2E File Offset: 0x0002622E
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00027E36 File Offset: 0x00026236
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00027E3F File Offset: 0x0002623F
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00027E52 File Offset: 0x00026252
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00027E68 File Offset: 0x00026268
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00027EDA File Offset: 0x000262DA
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00027F18 File Offset: 0x00026318
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00027F56 File Offset: 0x00026356
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00027F77 File Offset: 0x00026377
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00027FAE File Offset: 0x000263AE
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00027FE9 File Offset: 0x000263E9
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00027FF5 File Offset: 0x000263F5
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0002802D File Offset: 0x0002642D
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000C86 RID: 3206
		private ushort m_usConnectionPort;

		// Token: 0x04000C87 RID: 3207
		private ushort m_usQueryPort;

		// Token: 0x04000C88 RID: 3208
		private uint m_unIP;
	}
}
