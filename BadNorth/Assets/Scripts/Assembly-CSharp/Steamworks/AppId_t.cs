using System;

namespace Steamworks
{
	// Token: 0x02000379 RID: 889
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x0002976C File Offset: 0x00027B6C
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00029775 File Offset: 0x00027B75
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00029788 File Offset: 0x00027B88
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x000297A9 File Offset: 0x00027BA9
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000297BC File Offset: 0x00027BBC
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000297CE File Offset: 0x00027BCE
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000297DA File Offset: 0x00027BDA
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000297E2 File Offset: 0x00027BE2
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000297EB File Offset: 0x00027BEB
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x000297FC File Offset: 0x00027BFC
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x04000CBB RID: 3259
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000CBC RID: 3260
		public uint m_AppId;
	}
}
