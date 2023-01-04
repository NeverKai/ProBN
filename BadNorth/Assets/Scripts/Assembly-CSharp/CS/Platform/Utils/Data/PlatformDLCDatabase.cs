using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000085 RID: 133
	public class PlatformDLCDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00018028 File Offset: 0x00016428
		public List<string> PS4Index(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].ps4Index;
			}
			return null;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00018059 File Offset: 0x00016459
		public uint SteamAPI(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].steamAPI;
			}
			return (uint)AppId_t.Invalid;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018093 File Offset: 0x00016493
		public string OculusAPI(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].oculusAPI;
			}
			return null;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000180C4 File Offset: 0x000164C4
		public string Name(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].name;
			}
			return null;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000180F5 File Offset: 0x000164F5
		public string Description(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].description;
			}
			return null;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018126 File Offset: 0x00016526
		public long DiscrodAPI(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].discordAPI;
			}
			return -1L;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018158 File Offset: 0x00016558
		public ulong GogAPI(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].gogAPI;
			}
			return 0UL;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001818A File Offset: 0x0001658A
		public string GogURL(string key)
		{
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				return this.DLCs[key].gogUrl;
			}
			return string.Empty;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000181C0 File Offset: 0x000165C0
		public string XboxTitle(string key, out bool needsMount)
		{
			needsMount = false;
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				needsMount = this.DLCs[key].xboxMount;
				return this.DLCs[key].xboxTitle;
			}
			return null;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018214 File Offset: 0x00016614
		public bool XboxMount(string titleID)
		{
			bool result = false;
			if (this.DLCs != null)
			{
				Dictionary<string, PlatformDLCDatabase.DLCInfo>.Enumerator enumerator = this.DLCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, PlatformDLCDatabase.DLCInfo> keyValuePair = enumerator.Current;
					if (keyValuePair.Value.xboxTitle == titleID)
					{
						KeyValuePair<string, PlatformDLCDatabase.DLCInfo> keyValuePair2 = enumerator.Current;
						result = keyValuePair2.Value.xboxMount;
						break;
					}
				}
				enumerator.Dispose();
			}
			return result;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001828C File Offset: 0x0001668C
		public int SwitchIndex(string key, out bool needsMount)
		{
			needsMount = false;
			if (this.DLCs != null && this.DLCs.ContainsKey(key))
			{
				needsMount = this.DLCs[key].switchMount;
				return this.DLCs[key].switchIndex;
			}
			return -1;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000182E0 File Offset: 0x000166E0
		public bool SwitchMount(int indexID)
		{
			bool result = false;
			if (this.DLCs != null)
			{
				Dictionary<string, PlatformDLCDatabase.DLCInfo>.Enumerator enumerator = this.DLCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, PlatformDLCDatabase.DLCInfo> keyValuePair = enumerator.Current;
					if (keyValuePair.Value.switchIndex == indexID)
					{
						KeyValuePair<string, PlatformDLCDatabase.DLCInfo> keyValuePair2 = enumerator.Current;
						result = keyValuePair2.Value.switchMount;
						break;
					}
				}
				enumerator.Dispose();
			}
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018354 File Offset: 0x00016754
		public void OnBeforeSerialize()
		{
			this._serializer = new List<PlatformDLCDatabase.DLCInfo>();
			Dictionary<string, PlatformDLCDatabase.DLCInfo>.Enumerator enumerator = this.DLCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<PlatformDLCDatabase.DLCInfo> serializer = this._serializer;
				KeyValuePair<string, PlatformDLCDatabase.DLCInfo> keyValuePair = enumerator.Current;
				serializer.Add(keyValuePair.Value);
			}
			enumerator.Dispose();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000183AC File Offset: 0x000167AC
		public void OnAfterDeserialize()
		{
			if (this.DLCs != null)
			{
				this.DLCs.Clear();
			}
			else
			{
				this.DLCs = new Dictionary<string, PlatformDLCDatabase.DLCInfo>();
			}
			if (this._serializer != null)
			{
				for (int i = 0; i < this._serializer.Count; i++)
				{
					if (this._serializer[i] != null)
					{
						if (this.DLCs.ContainsKey(this._serializer[i].key))
						{
							Debug.LogWarning("[PUDD] Deserialize: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializer[i].key
							});
						}
						else
						{
							this.DLCs.Add(this._serializer[i].key, this._serializer[i]);
						}
					}
				}
			}
			this._serializer = null;
		}

		// Token: 0x04000252 RID: 594
		public Dictionary<string, PlatformDLCDatabase.DLCInfo> DLCs = new Dictionary<string, PlatformDLCDatabase.DLCInfo>();

		// Token: 0x04000253 RID: 595
		[SerializeField]
		private List<PlatformDLCDatabase.DLCInfo> _serializer;

		// Token: 0x02000086 RID: 134
		[Serializable]
		public class DLCInfo
		{
			// Token: 0x060005B9 RID: 1465 RVA: 0x00018490 File Offset: 0x00016890
			public DLCInfo(string newKey)
			{
				this.key = newKey;
			}

			// Token: 0x04000254 RID: 596
			public string key = string.Empty;

			// Token: 0x04000255 RID: 597
			public string name = string.Empty;

			// Token: 0x04000256 RID: 598
			public string description = string.Empty;

			// Token: 0x04000257 RID: 599
			public List<string> ps4Index;

			// Token: 0x04000258 RID: 600
			public uint steamAPI = (uint)AppId_t.Invalid;

			// Token: 0x04000259 RID: 601
			public string oculusAPI = string.Empty;

			// Token: 0x0400025A RID: 602
			public long discordAPI = -1L;

			// Token: 0x0400025B RID: 603
			public ulong gogAPI;

			// Token: 0x0400025C RID: 604
			public string gogUrl = string.Empty;

			// Token: 0x0400025D RID: 605
			public string xboxTitle = string.Empty;

			// Token: 0x0400025E RID: 606
			public bool xboxMount;

			// Token: 0x0400025F RID: 607
			public int switchIndex = -1;

			// Token: 0x04000260 RID: 608
			public bool switchMount;
		}
	}
}
