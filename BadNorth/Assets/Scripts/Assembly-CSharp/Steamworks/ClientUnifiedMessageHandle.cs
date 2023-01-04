using System;

namespace Steamworks
{
	// Token: 0x0200037F RID: 895
	[Serializable]
	public struct ClientUnifiedMessageHandle : IEquatable<ClientUnifiedMessageHandle>, IComparable<ClientUnifiedMessageHandle>
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x00029B96 File Offset: 0x00027F96
		public ClientUnifiedMessageHandle(ulong value)
		{
			this.m_ClientUnifiedMessageHandle = value;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00029B9F File Offset: 0x00027F9F
		public override string ToString()
		{
			return this.m_ClientUnifiedMessageHandle.ToString();
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00029BB2 File Offset: 0x00027FB2
		public override bool Equals(object other)
		{
			return other is ClientUnifiedMessageHandle && this == (ClientUnifiedMessageHandle)other;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00029BD3 File Offset: 0x00027FD3
		public override int GetHashCode()
		{
			return this.m_ClientUnifiedMessageHandle.GetHashCode();
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00029BE6 File Offset: 0x00027FE6
		public static bool operator ==(ClientUnifiedMessageHandle x, ClientUnifiedMessageHandle y)
		{
			return x.m_ClientUnifiedMessageHandle == y.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00029BF8 File Offset: 0x00027FF8
		public static bool operator !=(ClientUnifiedMessageHandle x, ClientUnifiedMessageHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00029C04 File Offset: 0x00028004
		public static explicit operator ClientUnifiedMessageHandle(ulong value)
		{
			return new ClientUnifiedMessageHandle(value);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00029C0C File Offset: 0x0002800C
		public static explicit operator ulong(ClientUnifiedMessageHandle that)
		{
			return that.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00029C15 File Offset: 0x00028015
		public bool Equals(ClientUnifiedMessageHandle other)
		{
			return this.m_ClientUnifiedMessageHandle == other.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00029C26 File Offset: 0x00028026
		public int CompareTo(ClientUnifiedMessageHandle other)
		{
			return this.m_ClientUnifiedMessageHandle.CompareTo(other.m_ClientUnifiedMessageHandle);
		}

		// Token: 0x04000CC7 RID: 3271
		public static readonly ClientUnifiedMessageHandle Invalid = new ClientUnifiedMessageHandle(0UL);

		// Token: 0x04000CC8 RID: 3272
		public ulong m_ClientUnifiedMessageHandle;
	}
}
