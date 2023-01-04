using System;

namespace Steamworks
{
	// Token: 0x02000368 RID: 872
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x00028BE4 File Offset: 0x00026FE4
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00028BED File Offset: 0x00026FED
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00028C00 File Offset: 0x00027000
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00028C21 File Offset: 0x00027021
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00028C34 File Offset: 0x00027034
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00028C46 File Offset: 0x00027046
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00028C52 File Offset: 0x00027052
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00028C5A File Offset: 0x0002705A
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00028C63 File Offset: 0x00027063
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00028C74 File Offset: 0x00027074
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x04000C9D RID: 3229
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000C9E RID: 3230
		public short m_FriendsGroupID;
	}
}
