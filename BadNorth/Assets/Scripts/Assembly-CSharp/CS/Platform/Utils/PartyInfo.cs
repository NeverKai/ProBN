using System;

namespace CS.Platform.Utils
{
	// Token: 0x0200006B RID: 107
	public struct PartyInfo
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00014142 File Offset: 0x00012542
		public PartyInfo(Guid lobbyID, string platform)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._lobbyID = lobbyID;
			this._platformKey = platform;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00014160 File Offset: 0x00012560
		public PartyInfo(string fromString)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._lobbyID = Guid.Empty;
			this._platformKey = "no platform";
			int num = 0;
			int num2 = fromString.IndexOf(':', num);
			this._platformKey = fromString.Substring(num, num2 - num);
			if (num + 1 > fromString.Length)
			{
				return;
			}
			num = num2 + 1;
			num2 = fromString.IndexOf(':', num);
			this._lobbyID = new Guid(fromString.Substring(num, num2 - num));
			if (num + 1 > fromString.Length)
			{
				return;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000141EE File Offset: 0x000125EE
		public Guid LobbyID
		{
			get
			{
				return this._lobbyID;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000141F6 File Offset: 0x000125F6
		public string platformKey
		{
			get
			{
				return this._platformKey;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000141FE File Offset: 0x000125FE
		public void Blank()
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._lobbyID = Guid.Empty;
			this._platformKey = "no platform";
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014224 File Offset: 0x00012624
		public void Set(string platform, Guid lobbyID)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._lobbyID = lobbyID;
			if (platform != null)
			{
				this._platformKey = platform;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00014248 File Offset: 0x00012648
		public static bool operator ==(PartyInfo left, PartyInfo right)
		{
			return left.platformKey == right.platformKey && left.LobbyID == right.LobbyID;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00014277 File Offset: 0x00012677
		public static bool operator !=(PartyInfo left, PartyInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00014283 File Offset: 0x00012683
		public override bool Equals(object obj)
		{
			return obj is PartyInfo && this == (PartyInfo)obj;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000142A3 File Offset: 0x000126A3
		public bool RawEquals(PartyInfo check)
		{
			return this.LobbyID == check.LobbyID && this.platformKey == check.platformKey;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000142D4 File Offset: 0x000126D4
		public override int GetHashCode()
		{
			if (!this._hasGenedHas)
			{
				this._hasGenedHas = true;
				if (this.platformKey != null)
				{
					this._hashCode = string.Format("{0}-{1}", this.platformKey, this.LobbyID).GetHashCode();
				}
				else
				{
					this._hashCode = string.Format("{0}-{1}", string.Empty, string.Empty).GetHashCode();
				}
			}
			return this._hashCode;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001434E File Offset: 0x0001274E
		public override string ToString()
		{
			return this.platformKey + ":" + this.LobbyID;
		}

		// Token: 0x040001F8 RID: 504
		private Guid _lobbyID;

		// Token: 0x040001F9 RID: 505
		private string _platformKey;

		// Token: 0x040001FA RID: 506
		private bool _hasGenedHas;

		// Token: 0x040001FB RID: 507
		private int _hashCode;
	}
}
