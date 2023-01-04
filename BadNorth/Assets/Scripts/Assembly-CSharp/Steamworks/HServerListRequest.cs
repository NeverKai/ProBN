using System;

namespace Steamworks
{
	// Token: 0x0200036F RID: 879
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x000290AF File Offset: 0x000274AF
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000290B8 File Offset: 0x000274B8
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000290CB File Offset: 0x000274CB
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000290EC File Offset: 0x000274EC
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000290FF File Offset: 0x000274FF
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00029114 File Offset: 0x00027514
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00029120 File Offset: 0x00027520
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00029128 File Offset: 0x00027528
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00029131 File Offset: 0x00027531
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x04000CAA RID: 3242
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000CAB RID: 3243
		public IntPtr m_HServerListRequest;
	}
}
