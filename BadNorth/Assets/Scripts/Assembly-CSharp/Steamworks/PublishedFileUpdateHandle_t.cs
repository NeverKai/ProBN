using System;

namespace Steamworks
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x00029401 File Offset: 0x00027801
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0002940A File Offset: 0x0002780A
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0002941D File Offset: 0x0002781D
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0002943E File Offset: 0x0002783E
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00029451 File Offset: 0x00027851
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00029463 File Offset: 0x00027863
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0002946F File Offset: 0x0002786F
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00029477 File Offset: 0x00027877
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00029480 File Offset: 0x00027880
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00029491 File Offset: 0x00027891
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x04000CB2 RID: 3250
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000CB3 RID: 3251
		public ulong m_PublishedFileUpdateHandle;
	}
}
