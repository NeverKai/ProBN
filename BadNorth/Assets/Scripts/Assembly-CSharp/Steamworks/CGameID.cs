using System;

namespace Steamworks
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x000281AF File Offset: 0x000265AF
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000281B8 File Offset: 0x000265B8
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000281C9 File Offset: 0x000265C9
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000281E8 File Offset: 0x000265E8
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000281F3 File Offset: 0x000265F3
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000281FE File Offset: 0x000265FE
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00028209 File Offset: 0x00026609
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00028214 File Offset: 0x00026614
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00028229 File Offset: 0x00026629
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)(this.m_GameID >> 24 & 255UL);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0002823C File Offset: 0x0002663C
		public uint ModID()
		{
			return (uint)(this.m_GameID >> 32 & (ulong)-1);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0002824C File Offset: 0x0002664C
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) != 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) != 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) != 0U;
			default:
				return false;
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000282F6 File Offset: 0x000266F6
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00028300 File Offset: 0x00026700
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00028309 File Offset: 0x00026709
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = ((this.m_GameID & 18446744073692774400UL) | ((ulong)((uint)other) & 16777215UL) << 0);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0002832F File Offset: 0x0002672F
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = ((this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24));
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00028354 File Offset: 0x00026754
		private void SetModID(uint other)
		{
			this.m_GameID = ((this.m_GameID & (ulong)-1) | ((ulong)other & (ulong)-1) << 32);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0002836E File Offset: 0x0002676E
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00028381 File Offset: 0x00026781
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000283A2 File Offset: 0x000267A2
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000283B5 File Offset: 0x000267B5
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000283C7 File Offset: 0x000267C7
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000283D3 File Offset: 0x000267D3
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000283DB File Offset: 0x000267DB
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000283E4 File Offset: 0x000267E4
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000283F5 File Offset: 0x000267F5
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000C8B RID: 3211
		public ulong m_GameID;

		// Token: 0x02000361 RID: 865
		public enum EGameIDType
		{
			// Token: 0x04000C8D RID: 3213
			k_EGameIDTypeApp,
			// Token: 0x04000C8E RID: 3214
			k_EGameIDTypeGameMod,
			// Token: 0x04000C8F RID: 3215
			k_EGameIDTypeShortcut,
			// Token: 0x04000C90 RID: 3216
			k_EGameIDTypeP2P
		}
	}
}
