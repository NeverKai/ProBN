using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000089 RID: 137
	public class PlatformPresenceDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x000185B4 File Offset: 0x000169B4
		public string GetSteamPresence(string key)
		{
			if (this.presence.ContainsKey(key))
			{
				return this.presence[key].steamLocCode;
			}
			return null;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000185DA File Offset: 0x000169DA
		public string GetPS4Presence(string key)
		{
			if (this.presence.ContainsKey(key))
			{
				return this.presence[key].ps4LocCode;
			}
			return null;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018600 File Offset: 0x00016A00
		public string GetDiscordPresence(string key)
		{
			if (this.presence.ContainsKey(key))
			{
				return this.presence[key].discordLocCode;
			}
			return null;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018626 File Offset: 0x00016A26
		public string GetXboxPresence(string key)
		{
			if (this.presence.ContainsKey(key))
			{
				return this.presence[key].xboxKey;
			}
			return null;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001864C File Offset: 0x00016A4C
		public string GetSteamStatus(string key)
		{
			if (this.status.ContainsKey(key))
			{
				return this.status[key].steamLocCode;
			}
			return null;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018672 File Offset: 0x00016A72
		public string GetPS4Status(string key)
		{
			if (this.status.ContainsKey(key))
			{
				return this.status[key].ps4LocCode;
			}
			return null;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00018698 File Offset: 0x00016A98
		public string GetDiscordStatus(string key)
		{
			if (this.status.ContainsKey(key))
			{
				return this.status[key].discordLocCode;
			}
			return null;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000186BE File Offset: 0x00016ABE
		public string GetDiscordImage(string key)
		{
			if (this.images.ContainsKey(key))
			{
				return this.images[key].discordCode;
			}
			return null;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000186E4 File Offset: 0x00016AE4
		public string GetDiscordImageText(string key)
		{
			if (this.images.ContainsKey(key))
			{
				return this.images[key].discordTextCode;
			}
			return null;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001870C File Offset: 0x00016B0C
		public void OnBeforeSerialize()
		{
			this._serializerP = new List<PlatformPresenceDatabase.Presence>();
			this._serializerS = new List<PlatformPresenceDatabase.Status>();
			this._serializerI = new List<PlatformPresenceDatabase.Images>();
			Dictionary<string, PlatformPresenceDatabase.Presence>.Enumerator enumerator = this.presence.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<PlatformPresenceDatabase.Presence> serializerP = this._serializerP;
				KeyValuePair<string, PlatformPresenceDatabase.Presence> keyValuePair = enumerator.Current;
				serializerP.Add(keyValuePair.Value);
			}
			enumerator.Dispose();
			Dictionary<string, PlatformPresenceDatabase.Status>.Enumerator enumerator2 = this.status.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				List<PlatformPresenceDatabase.Status> serializerS = this._serializerS;
				KeyValuePair<string, PlatformPresenceDatabase.Status> keyValuePair2 = enumerator2.Current;
				serializerS.Add(keyValuePair2.Value);
			}
			enumerator2.Dispose();
			Dictionary<string, PlatformPresenceDatabase.Images>.Enumerator enumerator3 = this.images.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				List<PlatformPresenceDatabase.Images> serializerI = this._serializerI;
				KeyValuePair<string, PlatformPresenceDatabase.Images> keyValuePair3 = enumerator3.Current;
				serializerI.Add(keyValuePair3.Value);
			}
			enumerator3.Dispose();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000187F8 File Offset: 0x00016BF8
		public void OnAfterDeserialize()
		{
			if (this.presence != null)
			{
				this.presence.Clear();
			}
			else
			{
				this.presence = new Dictionary<string, PlatformPresenceDatabase.Presence>();
			}
			if (this._serializerP != null)
			{
				for (int i = 0; i < this._serializerP.Count; i++)
				{
					if (this._serializerP[i] != null)
					{
						if (this.presence.ContainsKey(this._serializerP[i].key))
						{
							Debug.LogWarning("[PUPD] Deserialize Presence: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializerP[i].key
							});
						}
						else
						{
							this.presence.Add(this._serializerP[i].key, this._serializerP[i]);
						}
					}
				}
			}
			this._serializerP = null;
			if (this.status != null)
			{
				this.status.Clear();
			}
			else
			{
				this.status = new Dictionary<string, PlatformPresenceDatabase.Status>();
			}
			if (this._serializerS != null)
			{
				for (int j = 0; j < this._serializerS.Count; j++)
				{
					if (this._serializerS[j] != null)
					{
						if (this.status.ContainsKey(this._serializerS[j].key))
						{
							Debug.LogWarning("[PUPD] Deserialize: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializerS[j].key
							});
						}
						else
						{
							this.status.Add(this._serializerS[j].key, this._serializerS[j]);
						}
					}
				}
			}
			this._serializerS = null;
			if (this.images != null)
			{
				this.images.Clear();
			}
			else
			{
				this.images = new Dictionary<string, PlatformPresenceDatabase.Images>();
			}
			if (this._serializerI != null)
			{
				for (int k = 0; k < this._serializerI.Count; k++)
				{
					if (this._serializerI[k] != null)
					{
						if (this.images.ContainsKey(this._serializerI[k].key))
						{
							Debug.LogWarning("[PUPD] Deserialize Images: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializerI[k].key
							});
						}
						else
						{
							this.images.Add(this._serializerI[k].key, this._serializerI[k]);
						}
					}
				}
			}
			this._serializerI = null;
		}

		// Token: 0x04000269 RID: 617
		[SerializeField]
		public string StatusConnectionType = ": ";

		// Token: 0x0400026A RID: 618
		public Dictionary<string, PlatformPresenceDatabase.Presence> presence = new Dictionary<string, PlatformPresenceDatabase.Presence>();

		// Token: 0x0400026B RID: 619
		public Dictionary<string, PlatformPresenceDatabase.Status> status = new Dictionary<string, PlatformPresenceDatabase.Status>();

		// Token: 0x0400026C RID: 620
		public Dictionary<string, PlatformPresenceDatabase.Images> images = new Dictionary<string, PlatformPresenceDatabase.Images>();

		// Token: 0x0400026D RID: 621
		[SerializeField]
		private List<PlatformPresenceDatabase.Presence> _serializerP;

		// Token: 0x0400026E RID: 622
		[SerializeField]
		private List<PlatformPresenceDatabase.Status> _serializerS;

		// Token: 0x0400026F RID: 623
		[SerializeField]
		private List<PlatformPresenceDatabase.Images> _serializerI;

		// Token: 0x0200008A RID: 138
		[Serializable]
		public class Presence
		{
			// Token: 0x060005C8 RID: 1480 RVA: 0x00018A8C File Offset: 0x00016E8C
			public Presence(string newKey)
			{
				this.key = newKey;
			}

			// Token: 0x04000270 RID: 624
			public string key = string.Empty;

			// Token: 0x04000271 RID: 625
			public string steamLocCode = string.Empty;

			// Token: 0x04000272 RID: 626
			public string discordLocCode = string.Empty;

			// Token: 0x04000273 RID: 627
			public string ps4LocCode = string.Empty;

			// Token: 0x04000274 RID: 628
			public string xboxKey = string.Empty;
		}

		// Token: 0x0200008B RID: 139
		[Serializable]
		public class Status
		{
			// Token: 0x060005C9 RID: 1481 RVA: 0x00018ADD File Offset: 0x00016EDD
			public Status(string newKey)
			{
				this.key = newKey;
			}

			// Token: 0x04000275 RID: 629
			public string key = string.Empty;

			// Token: 0x04000276 RID: 630
			public string steamLocCode = string.Empty;

			// Token: 0x04000277 RID: 631
			public string discordLocCode = string.Empty;

			// Token: 0x04000278 RID: 632
			public string ps4LocCode = string.Empty;
		}

		// Token: 0x0200008C RID: 140
		[Serializable]
		public class Images
		{
			// Token: 0x060005CA RID: 1482 RVA: 0x00018B18 File Offset: 0x00016F18
			public Images(string newKey)
			{
				this.key = newKey;
			}

			// Token: 0x04000279 RID: 633
			public string key = string.Empty;

			// Token: 0x0400027A RID: 634
			public string discordCode = string.Empty;

			// Token: 0x0400027B RID: 635
			public string discordTextCode = string.Empty;
		}
	}
}
