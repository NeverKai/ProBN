using System;

namespace Steamworks
{
	// Token: 0x0200036A RID: 874
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x00028D46 File Offset: 0x00027146
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00028D4F File Offset: 0x0002714F
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00028D62 File Offset: 0x00027162
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00028D83 File Offset: 0x00027183
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00028D96 File Offset: 0x00027196
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00028DA8 File Offset: 0x000271A8
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00028DB4 File Offset: 0x000271B4
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00028DBC File Offset: 0x000271BC
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00028DC5 File Offset: 0x000271C5
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00028DD6 File Offset: 0x000271D6
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x04000CA1 RID: 3233
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000CA2 RID: 3234
		public uint m_HTTPCookieContainerHandle;
	}
}
