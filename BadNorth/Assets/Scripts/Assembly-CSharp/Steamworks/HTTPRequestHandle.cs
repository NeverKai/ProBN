using System;

namespace Steamworks
{
	// Token: 0x0200036B RID: 875
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x00028DF7 File Offset: 0x000271F7
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00028E00 File Offset: 0x00027200
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00028E13 File Offset: 0x00027213
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00028E34 File Offset: 0x00027234
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00028E47 File Offset: 0x00027247
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00028E59 File Offset: 0x00027259
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00028E65 File Offset: 0x00027265
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00028E6D File Offset: 0x0002726D
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00028E76 File Offset: 0x00027276
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00028E87 File Offset: 0x00027287
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x04000CA3 RID: 3235
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000CA4 RID: 3236
		public uint m_HTTPRequestHandle;
	}
}
