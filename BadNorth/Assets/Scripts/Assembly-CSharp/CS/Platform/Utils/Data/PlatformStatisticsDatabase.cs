using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x0200008D RID: 141
	public class PlatformStatisticsDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00018B6D File Offset: 0x00016F6D
		public string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00018B75 File Offset: 0x00016F75
		public string[] XboxAPIs
		{
			get
			{
				return this._xboxAPIs;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00018B7D File Offset: 0x00016F7D
		public int EstFileSize
		{
			get
			{
				return this._estFileSize;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018B85 File Offset: 0x00016F85
		public string Name(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].name;
			}
			return null;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018BB6 File Offset: 0x00016FB6
		public string Description(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].description;
			}
			return null;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00018BE7 File Offset: 0x00016FE7
		public string SteamAPI(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].steamAPI;
			}
			return null;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00018C18 File Offset: 0x00017018
		public string GogAPI(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].gogAPI;
			}
			return null;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018C49 File Offset: 0x00017049
		public string XboxAPI(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].xboxAPI;
			}
			return null;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00018C7A File Offset: 0x0001707A
		public PlatformStatisticsDatabase.StatsType Type(string key)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				return this.statistics[key].statType;
			}
			return PlatformStatisticsDatabase.StatsType.FLOAT;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00018CAB File Offset: 0x000170AB
		public bool AchievementLinked(string key)
		{
			return this.statistics != null && this.statistics.ContainsKey(key) && this.statistics[key].achievements.Count > 0;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00018CE4 File Offset: 0x000170E4
		public string AchievementLink(string key, int index)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				PlatformStatisticsDatabase.StatisticInfo statisticInfo = this.statistics[key];
				if (0 <= index && index < statisticInfo.achievements.Count)
				{
					return statisticInfo.achievements[index].achievementKey;
				}
			}
			return null;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00018D48 File Offset: 0x00017148
		public float AchievementUnlock(string key, int index)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				PlatformStatisticsDatabase.StatisticInfo statisticInfo = this.statistics[key];
				if (0 <= index && index < statisticInfo.achievements.Count)
				{
					return statisticInfo.achievements[index].achievementUnlock;
				}
			}
			return 0f;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00018DB0 File Offset: 0x000171B0
		public string AchievementUnlockCheck(string key, float value, int index)
		{
			if (this.statistics != null && this.statistics.ContainsKey(key))
			{
				PlatformStatisticsDatabase.StatisticInfo statisticInfo = this.statistics[key];
				if (0 <= index && index < statisticInfo.achievements.Count)
				{
					return (value < statisticInfo.achievements[index].achievementUnlock) ? null : statisticInfo.achievements[index].achievementKey;
				}
			}
			return null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00018E30 File Offset: 0x00017230
		public string AchievementUnlock(string key, float value, Action<string> unlockCall)
		{
			if (unlockCall != null && this.statistics != null && this.statistics.ContainsKey(key))
			{
				PlatformStatisticsDatabase.StatisticInfo statisticInfo = this.statistics[key];
				for (int i = 0; i < statisticInfo.achievements.Count; i++)
				{
					if (value >= statisticInfo.achievements[i].achievementUnlock)
					{
						unlockCall(statisticInfo.achievements[i].achievementKey);
					}
				}
			}
			return null;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00018EB8 File Offset: 0x000172B8
		public string AchievementUpdate(string key, float value, Action<string, float, float> update)
		{
			if (update != null && this.statistics != null && this.statistics.ContainsKey(key))
			{
				PlatformStatisticsDatabase.StatisticInfo statisticInfo = this.statistics[key];
				for (int i = 0; i < statisticInfo.achievements.Count; i++)
				{
					update(statisticInfo.achievements[i].achievementKey, value, statisticInfo.achievements[i].achievementUnlock);
				}
			}
			return null;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00018F3C File Offset: 0x0001733C
		public void OnBeforeSerialize()
		{
			this._serializer = new List<PlatformStatisticsDatabase.StatisticInfo>();
			this._keys = null;
			this._estFileSize = 6;
			Dictionary<string, PlatformStatisticsDatabase.StatisticInfo>.Enumerator enumerator = this.statistics.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this._estFileSize += 4;
				int estFileSize = this._estFileSize;
				int num = 2;
				KeyValuePair<string, PlatformStatisticsDatabase.StatisticInfo> keyValuePair = enumerator.Current;
				this._estFileSize = estFileSize + num * (keyValuePair.Key.Length + 1);
				List<PlatformStatisticsDatabase.StatisticInfo> serializer = this._serializer;
				KeyValuePair<string, PlatformStatisticsDatabase.StatisticInfo> keyValuePair2 = enumerator.Current;
				serializer.Add(keyValuePair2.Value);
			}
			enumerator.Dispose();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018FD4 File Offset: 0x000173D4
		public void OnAfterDeserialize()
		{
			this._keys = null;
			this._xboxAPIs = null;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (this.statistics != null)
			{
				this.statistics.Clear();
			}
			else
			{
				this.statistics = new Dictionary<string, PlatformStatisticsDatabase.StatisticInfo>();
			}
			if (this._serializer != null)
			{
				for (int i = 0; i < this._serializer.Count; i++)
				{
					if (this._serializer[i] != null)
					{
						if (this.statistics.ContainsKey(this._serializer[i].key))
						{
							Debug.LogWarning("[PUSD] Deserialize: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializer[i].key
							});
						}
						else
						{
							this.statistics.Add(this._serializer[i].key, this._serializer[i]);
							list.Add(this._serializer[i].key);
							if (!string.IsNullOrEmpty(this._serializer[i].xboxAPI))
							{
								list2.Add(this._serializer[i].xboxAPI);
							}
						}
					}
				}
			}
			this._serializer = null;
			if (list.Count > 0)
			{
				this._keys = list.ToArray();
			}
			if (list2.Count > 0)
			{
				this._xboxAPIs = list2.ToArray();
			}
		}

		// Token: 0x0400027C RID: 636
		public Dictionary<string, PlatformStatisticsDatabase.StatisticInfo> statistics = new Dictionary<string, PlatformStatisticsDatabase.StatisticInfo>();

		// Token: 0x0400027D RID: 637
		[NonSerialized]
		private string[] _keys;

		// Token: 0x0400027E RID: 638
		[NonSerialized]
		private string[] _xboxAPIs;

		// Token: 0x0400027F RID: 639
		[SerializeField]
		private int _estFileSize = -1;

		// Token: 0x04000280 RID: 640
		[SerializeField]
		public string SystemStatsStorageLocation = "Statistics";

		// Token: 0x04000281 RID: 641
		[SerializeField]
		private List<PlatformStatisticsDatabase.StatisticInfo> _serializer;

		// Token: 0x0200008E RID: 142
		public enum StatsType
		{
			// Token: 0x04000283 RID: 643
			FLOAT,
			// Token: 0x04000284 RID: 644
			INT
		}

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class StatisticInfo
		{
			// Token: 0x060005DD RID: 1501 RVA: 0x0001914C File Offset: 0x0001754C
			public StatisticInfo(string userKey)
			{
				this.key = userKey;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x000191B3 File Offset: 0x000175B3
			public bool achievementLinked
			{
				get
				{
					return this.achievements.Count > 0;
				}
			}

			// Token: 0x04000285 RID: 645
			public string key = string.Empty;

			// Token: 0x04000286 RID: 646
			public string name = string.Empty;

			// Token: 0x04000287 RID: 647
			public string description = string.Empty;

			// Token: 0x04000288 RID: 648
			public string steamAPI = string.Empty;

			// Token: 0x04000289 RID: 649
			public string xboxAPI = string.Empty;

			// Token: 0x0400028A RID: 650
			public string gogAPI = string.Empty;

			// Token: 0x0400028B RID: 651
			public PlatformStatisticsDatabase.StatsType statType;

			// Token: 0x0400028C RID: 652
			public List<PlatformStatisticsDatabase.StatisticInfo.AchievementLink> achievements = new List<PlatformStatisticsDatabase.StatisticInfo.AchievementLink>();

			// Token: 0x02000090 RID: 144
			[Serializable]
			public class AchievementLink
			{
				// Token: 0x0400028D RID: 653
				public string achievementKey = string.Empty;

				// Token: 0x0400028E RID: 654
				public float achievementUnlock;
			}
		}
	}
}
