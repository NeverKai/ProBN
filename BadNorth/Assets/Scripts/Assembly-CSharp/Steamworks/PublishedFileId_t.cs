using System;

namespace Steamworks
{
	// Token: 0x02000373 RID: 883
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x060013E8 RID: 5096 RVA: 0x0002934F File Offset: 0x0002774F
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00029358 File Offset: 0x00027758
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0002936B File Offset: 0x0002776B
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0002938C File Offset: 0x0002778C
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0002939F File Offset: 0x0002779F
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000293B1 File Offset: 0x000277B1
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000293BD File Offset: 0x000277BD
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000293C5 File Offset: 0x000277C5
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000293CE File Offset: 0x000277CE
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000293DF File Offset: 0x000277DF
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x04000CB0 RID: 3248
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000CB1 RID: 3249
		public ulong m_PublishedFileId;
	}
}
