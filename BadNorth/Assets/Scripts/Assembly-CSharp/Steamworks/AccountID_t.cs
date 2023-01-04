using System;

namespace Steamworks
{
	// Token: 0x02000378 RID: 888
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x0600141F RID: 5151 RVA: 0x000296C8 File Offset: 0x00027AC8
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000296D1 File Offset: 0x00027AD1
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000296E4 File Offset: 0x00027AE4
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00029705 File Offset: 0x00027B05
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00029718 File Offset: 0x00027B18
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0002972A File Offset: 0x00027B2A
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00029736 File Offset: 0x00027B36
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0002973E File Offset: 0x00027B3E
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00029747 File Offset: 0x00027B47
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00029758 File Offset: 0x00027B58
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x04000CBA RID: 3258
		public uint m_AccountID;
	}
}
