using System;

namespace Steamworks
{
	// Token: 0x0200035C RID: 860
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x00028067 File Offset: 0x00026467
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00028070 File Offset: 0x00026470
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00028083 File Offset: 0x00026483
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000280A4 File Offset: 0x000264A4
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000280B7 File Offset: 0x000264B7
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000280C9 File Offset: 0x000264C9
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000280D5 File Offset: 0x000264D5
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000280DD File Offset: 0x000264DD
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000280E6 File Offset: 0x000264E6
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000280F7 File Offset: 0x000264F7
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000C89 RID: 3209
		public int m_HSteamPipe;
	}
}
