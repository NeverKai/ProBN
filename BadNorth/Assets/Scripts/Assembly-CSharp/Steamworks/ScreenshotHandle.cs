using System;

namespace Steamworks
{
	// Token: 0x02000377 RID: 887
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x00029617 File Offset: 0x00027A17
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00029620 File Offset: 0x00027A20
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00029633 File Offset: 0x00027A33
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00029654 File Offset: 0x00027A54
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00029667 File Offset: 0x00027A67
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00029679 File Offset: 0x00027A79
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00029685 File Offset: 0x00027A85
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0002968D File Offset: 0x00027A8D
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00029696 File Offset: 0x00027A96
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000296A7 File Offset: 0x00027AA7
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x04000CB8 RID: 3256
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000CB9 RID: 3257
		public uint m_ScreenshotHandle;
	}
}
