using System;

namespace Steamworks
{
	// Token: 0x02000369 RID: 873
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x00028C95 File Offset: 0x00027095
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00028C9E File Offset: 0x0002709E
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00028CB1 File Offset: 0x000270B1
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00028CD2 File Offset: 0x000270D2
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00028CE5 File Offset: 0x000270E5
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00028CF7 File Offset: 0x000270F7
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00028D03 File Offset: 0x00027103
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00028D0B File Offset: 0x0002710B
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00028D14 File Offset: 0x00027114
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00028D25 File Offset: 0x00027125
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x04000C9F RID: 3231
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000CA0 RID: 3232
		public uint m_HHTMLBrowser;
	}
}
