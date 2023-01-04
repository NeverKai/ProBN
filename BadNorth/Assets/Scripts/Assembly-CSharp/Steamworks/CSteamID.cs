using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000362 RID: 866
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06001317 RID: 4887 RVA: 0x00028409 File Offset: 0x00026809
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0002841C File Offset: 0x0002681C
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00028431 File Offset: 0x00026831
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0002843A File Offset: 0x0002683A
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
			}
			else
			{
				this.SetAccountInstance(1U);
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00028472 File Offset: 0x00026872
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00028491 File Offset: 0x00026891
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0002849B File Offset: 0x0002689B
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000284BE File Offset: 0x000268BE
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000284E2 File Offset: 0x000268E2
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00028511 File Offset: 0x00026911
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0002852B File Offset: 0x0002692B
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00028536 File Offset: 0x00026936
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00028541 File Offset: 0x00026941
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0002854C File Offset: 0x0002694C
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00028557 File Offset: 0x00026957
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00028562 File Offset: 0x00026962
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) != 0U;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00028585 File Offset: 0x00026985
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000285A0 File Offset: 0x000269A0
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000285BB File Offset: 0x000269BB
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000285C7 File Offset: 0x000269C7
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000285D3 File Offset: 0x000269D3
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = ((this.m_SteamID & 18446744069414584320UL) | ((ulong)((uint)other) & (ulong)-1) << 0);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000285F8 File Offset: 0x000269F8
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = ((this.m_SteamID & 18442240478377148415UL) | ((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0002861D File Offset: 0x00026A1D
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = ((this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52));
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0002863F File Offset: 0x00026A3F
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = ((this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56));
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00028664 File Offset: 0x00026A64
		public void ClearIndividualInstance()
		{
			if (this.BIndividualAccount())
			{
				this.SetAccountInstance(0U);
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00028678 File Offset: 0x00026A78
		public bool HasNoIndividualInstance()
		{
			return this.BIndividualAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00028691 File Offset: 0x00026A91
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)-1));
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000286A2 File Offset: 0x00026AA2
		public uint GetUnAccountInstance()
		{
			return (uint)(this.m_SteamID >> 32 & 1048575UL);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000286B5 File Offset: 0x00026AB5
		public EAccountType GetEAccountType()
		{
			return (EAccountType)(this.m_SteamID >> 52 & 15UL);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000286C5 File Offset: 0x00026AC5
		public EUniverse GetEUniverse()
		{
			return (EUniverse)(this.m_SteamID >> 56 & 255UL);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000286D8 File Offset: 0x00026AD8
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 4U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0002879E File Offset: 0x00026B9E
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000287B1 File Offset: 0x00026BB1
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000287D2 File Offset: 0x00026BD2
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000287E5 File Offset: 0x00026BE5
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000287F7 File Offset: 0x00026BF7
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00028803 File Offset: 0x00026C03
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0002880B File Offset: 0x00026C0B
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00028814 File Offset: 0x00026C14
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00028825 File Offset: 0x00026C25
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x04000C91 RID: 3217
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000C92 RID: 3218
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C93 RID: 3219
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C94 RID: 3220
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C95 RID: 3221
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C96 RID: 3222
		public ulong m_SteamID;
	}
}
