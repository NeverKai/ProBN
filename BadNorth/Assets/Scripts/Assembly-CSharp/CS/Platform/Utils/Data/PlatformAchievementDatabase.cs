using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x0200007E RID: 126
	public class PlatformAchievementDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00016531 File Offset: 0x00014931
		public int PS4Index(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].ps4Index;
			}
			return -1;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016562 File Offset: 0x00014962
		public string SteamAPI(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].steamAPI;
			}
			return null;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00016593 File Offset: 0x00014993
		public string OculusAPI(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].oculusAPI;
			}
			return null;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000165C4 File Offset: 0x000149C4
		public string Name(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].name;
			}
			return null;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000165F5 File Offset: 0x000149F5
		public string Description(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].description;
			}
			return null;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016628 File Offset: 0x00014A28
		public int LargesPS4()
		{
			int num = -1;
			Dictionary<string, PlatformAchievementDatabase.AchievementInfo>.Enumerator enumerator = this.achievements.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int val = num;
				KeyValuePair<string, PlatformAchievementDatabase.AchievementInfo> keyValuePair = enumerator.Current;
				num = Math.Max(val, keyValuePair.Value.ps4Index);
			}
			enumerator.Dispose();
			return num;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016677 File Offset: 0x00014A77
		public string XboxAPI(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].xboxAPI;
			}
			return null;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000166A8 File Offset: 0x00014AA8
		public string[] AllKeys()
		{
			List<string> list = new List<string>();
			if (this.achievements != null)
			{
				Dictionary<string, PlatformAchievementDatabase.AchievementInfo>.Enumerator enumerator = this.achievements.GetEnumerator();
				while (enumerator.MoveNext())
				{
					List<string> list2 = list;
					KeyValuePair<string, PlatformAchievementDatabase.AchievementInfo> keyValuePair = enumerator.Current;
					list2.Add(keyValuePair.Value.key);
				}
				enumerator.Dispose();
			}
			return list.ToArray();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001670A File Offset: 0x00014B0A
		public string GOGAPI(string key)
		{
			if (this.achievements != null && this.achievements.ContainsKey(key))
			{
				return this.achievements[key].gogAPI;
			}
			return null;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001673C File Offset: 0x00014B3C
		public void OnBeforeSerialize()
		{
			this._serializer = new List<PlatformAchievementDatabase.AchievementInfo>();
			Dictionary<string, PlatformAchievementDatabase.AchievementInfo>.Enumerator enumerator = this.achievements.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<PlatformAchievementDatabase.AchievementInfo> serializer = this._serializer;
				KeyValuePair<string, PlatformAchievementDatabase.AchievementInfo> keyValuePair = enumerator.Current;
				serializer.Add(keyValuePair.Value);
			}
			enumerator.Dispose();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016794 File Offset: 0x00014B94
		public void OnAfterDeserialize()
		{
			if (this.achievements != null)
			{
				this.achievements.Clear();
			}
			else
			{
				this.achievements = new Dictionary<string, PlatformAchievementDatabase.AchievementInfo>();
			}
			if (this._serializer != null)
			{
				for (int i = 0; i < this._serializer.Count; i++)
				{
					if (this._serializer[i] != null)
					{
						if (this.achievements.ContainsKey(this._serializer[i].key))
						{
							Debug.LogWarning("[PUAD] Deserialize: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializer[i].key
							});
						}
						else
						{
							this.achievements.Add(this._serializer[i].key, this._serializer[i]);
						}
					}
				}
			}
			this._serializer = null;
		}

		// Token: 0x04000235 RID: 565
		public Dictionary<string, PlatformAchievementDatabase.AchievementInfo> achievements = new Dictionary<string, PlatformAchievementDatabase.AchievementInfo>();

		// Token: 0x04000236 RID: 566
		[SerializeField]
		private List<PlatformAchievementDatabase.AchievementInfo> _serializer;

		// Token: 0x0200007F RID: 127
		[Serializable]
		public class AchievementInfo
		{
			// Token: 0x0600058F RID: 1423 RVA: 0x00016878 File Offset: 0x00014C78
			public AchievementInfo(string newKey)
			{
				this.key = newKey;
			}

			// Token: 0x04000237 RID: 567
			public string key = string.Empty;

			// Token: 0x04000238 RID: 568
			public string name = string.Empty;

			// Token: 0x04000239 RID: 569
			public string description = string.Empty;

			// Token: 0x0400023A RID: 570
			public int ps4Index = -1;

			// Token: 0x0400023B RID: 571
			public string steamAPI = string.Empty;

			// Token: 0x0400023C RID: 572
			public string oculusAPI = string.Empty;

			// Token: 0x0400023D RID: 573
			public string xboxAPI = string.Empty;

			// Token: 0x0400023E RID: 574
			public string gogAPI = string.Empty;
		}
	}
}
