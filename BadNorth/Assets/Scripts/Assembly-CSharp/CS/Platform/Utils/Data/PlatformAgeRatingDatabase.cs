using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000080 RID: 128
	public class PlatformAgeRatingDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x000168FC File Offset: 0x00014CFC
		public static int GetBoardAge(string board, string age)
		{
			board = board.ToUpper();
			age = age.ToUpper();
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4PublishSettings.Length; i++)
			{
				if (PlatformAgeRatingDatabase.BoardAges[i].board == board)
				{
					return PlatformAgeRatingDatabase.BoardAges[i].GetLevel(age);
				}
			}
			return -1;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00016960 File Offset: 0x00014D60
		public static int GetSonyLevelAge(string board, string age)
		{
			board = board.ToUpper();
			age = age.ToUpper();
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4PublishSettings.Length; i++)
			{
				if (PlatformAgeRatingDatabase.PS4PublishSettings[i].board == board)
				{
					return PlatformAgeRatingDatabase.PS4PublishSettings[i].GetLevel(age);
				}
			}
			return 11;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000169C4 File Offset: 0x00014DC4
		public static bool HasBoard(string board)
		{
			board = board.ToUpper();
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4PublishSettings.Length; i++)
			{
				if (PlatformAgeRatingDatabase.PS4PublishSettings[i].board == board)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00016A10 File Offset: 0x00014E10
		public static bool HasPublishLevel(string board, string age)
		{
			board = board.ToUpper();
			age = age.ToUpper();
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4PublishSettings.Length; i++)
			{
				if (PlatformAgeRatingDatabase.PS4PublishSettings[i].board == board)
				{
					return PlatformAgeRatingDatabase.PS4PublishSettings[i].HasLevel(age);
				}
			}
			return false;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00016A74 File Offset: 0x00014E74
		public static bool GetPossableAges(string board, ref List<string> ages)
		{
			board = board.ToUpper();
			if (ages == null)
			{
				ages = new List<string>();
			}
			ages.Clear();
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4PublishSettings.Length; i++)
			{
				if (PlatformAgeRatingDatabase.BoardAges[i].board == board)
				{
					return PlatformAgeRatingDatabase.BoardAges[i].GetAges(ref ages);
				}
			}
			return false;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00016AE4 File Offset: 0x00014EE4
		public string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00016AEC File Offset: 0x00014EEC
		public PlatformAgeRatingDatabase.AgeRatingInfo AgeRating(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return this.ratings[key];
			}
			return null;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00016B18 File Offset: 0x00014F18
		public string Age(string key)
		{
			if (this.ratings == null || !this.ratings.ContainsKey(key))
			{
				return null;
			}
			if (BasePlatformManager.Instance == null)
			{
				return this.ratings[key].AgeNone.ToString();
			}
			string key2 = BasePlatformManager.Instance.Key;
			if (key2 != null)
			{
				if (key2 == "steam")
				{
					return this.ratings[key].AgeSteam.ToString();
				}
				if (key2 == "oculus")
				{
					return this.ratings[key].AgeOculus.ToString();
				}
				if (key2 == "playstation")
				{
					return this.ratings[key].AgePS4;
				}
				if (key2 == "no platform")
				{
					return this.ratings[key].AgeNone.ToString();
				}
			}
			Debug.LogWarning("[PUARD] Unaged Platform: Platform '{0}'", new object[]
			{
				BasePlatformManager.Instance.Key
			});
			return this.ratings[key].AgeNone.ToString();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00016C4A File Offset: 0x0001504A
		public string AgeNone(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return this.ratings[key].AgeSteam;
			}
			return null;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00016C7B File Offset: 0x0001507B
		public string AgeSteam(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return this.ratings[key].AgeSteam;
			}
			return null;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016CAC File Offset: 0x000150AC
		public string AgeOculus(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return this.ratings[key].AgeOculus;
			}
			return null;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00016CDD File Offset: 0x000150DD
		public int AgePS4Publish(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return PlatformAgeRatingDatabase.GetSonyLevelAge(key, this.ratings[key].AgePS4);
			}
			return -1;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00016D14 File Offset: 0x00015114
		public string AgePS4(string key)
		{
			if (this.ratings != null && this.ratings.ContainsKey(key))
			{
				return this.ratings[key].AgePS4;
			}
			return null;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00016D45 File Offset: 0x00015145
		public bool AgePS4Active(string key)
		{
			return this.ratings != null && this.ratings.ContainsKey(key) && this.ratings[key].ps4Active;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00016D78 File Offset: 0x00015178
		public void ActivatePS4(string region)
		{
			for (int i = 0; i < PlatformAgeRatingDatabase.PS4RegionBoards.Length; i++)
			{
				if (PlatformAgeRatingDatabase.PS4RegionBoards[i].region == region)
				{
					Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo>.Enumerator enumerator = this.ratings.GetEnumerator();
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, PlatformAgeRatingDatabase.AgeRatingInfo> keyValuePair = enumerator.Current;
						keyValuePair.Value.ps4Active = false;
						if (PlatformAgeRatingDatabase.PS4RegionBoards[i].board != null)
						{
							for (int j = 0; j < PlatformAgeRatingDatabase.PS4RegionBoards[i].board.Length; j++)
							{
								KeyValuePair<string, PlatformAgeRatingDatabase.AgeRatingInfo> keyValuePair2 = enumerator.Current;
								if (keyValuePair2.Key == PlatformAgeRatingDatabase.PS4RegionBoards[i].board[j])
								{
									KeyValuePair<string, PlatformAgeRatingDatabase.AgeRatingInfo> keyValuePair3 = enumerator.Current;
									keyValuePair3.Value.ps4Active = true;
								}
							}
						}
					}
					enumerator.Dispose();
					return;
				}
			}
			Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo>.Enumerator enumerator2 = this.ratings.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				KeyValuePair<string, PlatformAgeRatingDatabase.AgeRatingInfo> keyValuePair4 = enumerator2.Current;
				keyValuePair4.Value.ps4Active = false;
			}
			enumerator2.Dispose();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00016EAC File Offset: 0x000152AC
		public void OnBeforeSerialize()
		{
			this._serializer = new List<PlatformAgeRatingDatabase.AgeRatingInfo>();
			this._keys = null;
			Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo>.Enumerator enumerator = this.ratings.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<PlatformAgeRatingDatabase.AgeRatingInfo> serializer = this._serializer;
				KeyValuePair<string, PlatformAgeRatingDatabase.AgeRatingInfo> keyValuePair = enumerator.Current;
				serializer.Add(keyValuePair.Value);
			}
			enumerator.Dispose();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00016F0C File Offset: 0x0001530C
		public void OnAfterDeserialize()
		{
			this._keys = null;
			List<string> list = new List<string>();
			if (this.ratings != null)
			{
				this.ratings.Clear();
			}
			else
			{
				this.ratings = new Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo>();
			}
			if (this._serializer != null)
			{
				for (int i = 0; i < this._serializer.Count; i++)
				{
					if (this._serializer[i] != null)
					{
						if (this.ratings.ContainsKey(this._serializer[i].board))
						{
							Debug.LogWarning("[PUARD] Deserialize: Found multiple of key '{0}' | Skipping new", new object[]
							{
								this._serializer[i].board
							});
						}
						else
						{
							this.ratings.Add(this._serializer[i].board, this._serializer[i]);
							list.Add(this._serializer[i].board);
						}
					}
				}
			}
			this._serializer = null;
			if (list.Count > 0)
			{
				this._keys = list.ToArray();
			}
		}

		// Token: 0x0400023F RID: 575
		private static readonly PlatformAgeRatingDatabase.AgeCarrier[] BoardAges = new PlatformAgeRatingDatabase.AgeCarrier[]
		{
			new PlatformAgeRatingDatabase.AgeCarrier("CERO", new object[]
			{
				"A",
				0,
				"Statistical",
				0,
				"B",
				12,
				"C",
				15,
				"D",
				17,
				"Z",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("ESRB", new object[]
			{
				"C",
				3,
				"E",
				6,
				"E 10+",
				10,
				"T",
				13,
				"M",
				17,
				"A",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PEGI", new object[]
			{
				"3",
				3,
				"7",
				7,
				"12",
				12,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PEGI (Portugal)", new object[]
			{
				"4",
				4,
				"6",
				6,
				"12",
				12,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("USK", new object[]
			{
				"0",
				3,
				"6",
				6,
				"12",
				12,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("FPB", new object[]
			{
				"A",
				3,
				"PG",
				6,
				"7-9 PG",
				7,
				"10",
				10,
				"10-12 PG",
				10,
				"13",
				13,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("ACB", new object[]
			{
				"G",
				3,
				"PG",
				8,
				"M",
				13,
				"MA",
				15,
				"R",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("OFLC", new object[]
			{
				"G",
				3,
				"PG",
				8,
				"RP13",
				13,
				"M",
				15,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PCBP", new object[]
			{
				"0",
				3,
				"6",
				6,
				"12",
				12,
				"16",
				16,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("GRB", new object[]
			{
				"A",
				0,
				"12",
				12,
				"15",
				15,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("IDB", new object[]
			{
				"0",
				0,
				"6",
				6,
				"12",
				12,
				"15",
				15,
				"18",
				18
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("MDA", new object[]
			{
				"16",
				16,
				"M18",
				18
			})
		};

		// Token: 0x04000240 RID: 576
		private static readonly PlatformAgeRatingDatabase.AgeCarrier[] PS4PublishSettings = new PlatformAgeRatingDatabase.AgeCarrier[]
		{
			new PlatformAgeRatingDatabase.AgeCarrier("CERO", new object[]
			{
				"A",
				3,
				"Statistical",
				3,
				"B",
				5,
				"C",
				7,
				"D",
				8,
				"Z",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("ESRB", new object[]
			{
				"C",
				2,
				"E",
				3,
				"E 10+",
				4,
				"T",
				5,
				"M",
				9,
				"A",
				10
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PEGI", new object[]
			{
				"3",
				2,
				"7",
				3,
				"12",
				5,
				"16",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PEGI (Portugal)", new object[]
			{
				"4",
				2,
				"6",
				3,
				"12",
				5,
				"16",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("USK", new object[]
			{
				"0",
				1,
				"6",
				3,
				"12",
				5,
				"16",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("FPB", new object[]
			{
				"A",
				1,
				"PG",
				2,
				"7-9 PG",
				3,
				"10",
				4,
				"10-12 PG",
				4,
				"13",
				5,
				"16",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("ACB", new object[]
			{
				"G",
				1,
				"PG",
				3,
				"M",
				7,
				"MA",
				8,
				"R",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("OFLC", new object[]
			{
				"G",
				1,
				"PG",
				3,
				"RP13",
				7,
				"M",
				7,
				"16",
				9,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("PCBP", new object[]
			{
				"0",
				1,
				"6",
				3,
				"12",
				5,
				"16",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("GRB", new object[]
			{
				"A",
				3,
				"12",
				5,
				"15",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("IDB", new object[]
			{
				"0",
				1,
				"6",
				3,
				"12",
				5,
				"15",
				7,
				"18",
				9
			}),
			new PlatformAgeRatingDatabase.AgeCarrier("MDA", new object[]
			{
				"16",
				7,
				"M18",
				9
			})
		};

		// Token: 0x04000241 RID: 577
		public static readonly PlatformAgeRatingDatabase.PS4CountryCodesCarrier[] PS4CountryCodes = new PlatformAgeRatingDatabase.PS4CountryCodesCarrier[]
		{
			new PlatformAgeRatingDatabase.PS4CountryCodesCarrier("ESRB", new string[]
			{
				"us",
				"ar",
				"bo",
				"br",
				"ca",
				"cl",
				"co",
				"cr",
				"ec",
				"sv",
				"gt",
				"hn",
				"mx",
				"ni",
				"pa",
				"py",
				"pe",
				"uy"
			}),
			new PlatformAgeRatingDatabase.PS4CountryCodesCarrier("PEGI", new string[]
			{
				"at",
				"be",
				"bg",
				"hr",
				"cy",
				"cz",
				"dk",
				"fi",
				"fr",
				"gr",
				"de",
				"hu",
				"is",
				"ie",
				"il",
				"it",
				"lu",
				"mt",
				"nl",
				"no",
				"pl",
				"ro",
				"ru",
				"sk",
				"si",
				"za",
				"es",
				"se",
				"ch",
				"tr",
				"gb"
			}),
			new PlatformAgeRatingDatabase.PS4CountryCodesCarrier("PEGI (Portugal)", new string[]
			{
				"pt"
			})
		};

		// Token: 0x04000242 RID: 578
		public static readonly PlatformAgeRatingDatabase.PS4RegionBoardCarrier[] PS4RegionBoards = new PlatformAgeRatingDatabase.PS4RegionBoardCarrier[]
		{
			new PlatformAgeRatingDatabase.PS4RegionBoardCarrier("SIEJA", new string[]
			{
				"CERO",
				"GRB",
				"IDB",
				"MDA"
			}),
			new PlatformAgeRatingDatabase.PS4RegionBoardCarrier("SIEA", new string[]
			{
				"ESRB"
			}),
			new PlatformAgeRatingDatabase.PS4RegionBoardCarrier("SIEE", new string[]
			{
				"PEGI",
				"PEGI (Portugal)",
				"USK",
				"FPB",
				"Classification Board",
				"OFLC",
				"PCBP"
			})
		};

		// Token: 0x04000243 RID: 579
		public Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo> ratings = new Dictionary<string, PlatformAgeRatingDatabase.AgeRatingInfo>();

		// Token: 0x04000244 RID: 580
		private string[] _keys;

		// Token: 0x04000245 RID: 581
		[SerializeField]
		private List<PlatformAgeRatingDatabase.AgeRatingInfo> _serializer;

		// Token: 0x02000081 RID: 129
		private struct AgeCarrier
		{
			// Token: 0x060005A3 RID: 1443 RVA: 0x00017E78 File Offset: 0x00016278
			public AgeCarrier(string newBoard, params object[] newLevels)
			{
				this.board = newBoard.ToUpper();
				this.levels = new Dictionary<string, int>();
				for (int i = 0; i < newLevels.Length; i += 2)
				{
					if (!this.levels.ContainsKey(((string)newLevels[i]).ToUpper()) && i + 1 < newLevels.Length)
					{
						this.levels.Add(((string)newLevels[i]).ToUpper(), (int)newLevels[i + 1]);
					}
				}
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00017EFA File Offset: 0x000162FA
			public bool HasLevel(string age)
			{
				age = age.ToUpper();
				return this.levels.ContainsKey(age);
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00017F10 File Offset: 0x00016310
			public int GetLevel(string age)
			{
				age = age.ToUpper();
				if (this.levels.ContainsKey(age))
				{
					return this.levels[age];
				}
				return -1;
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00017F3C File Offset: 0x0001633C
			public bool GetAges(ref List<string> ages)
			{
				if (ages == null)
				{
					ages = new List<string>();
				}
				ages.Clear();
				Dictionary<string, int>.Enumerator enumerator = this.levels.GetEnumerator();
				while (enumerator.MoveNext())
				{
					List<string> list = ages;
					KeyValuePair<string, int> keyValuePair = enumerator.Current;
					list.Add(keyValuePair.Key);
				}
				return ages.Count != 0;
			}

			// Token: 0x04000246 RID: 582
			public string board;

			// Token: 0x04000247 RID: 583
			private Dictionary<string, int> levels;
		}

		// Token: 0x02000082 RID: 130
		public struct PS4CountryCodesCarrier
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x00017F9E File Offset: 0x0001639E
			public PS4CountryCodesCarrier(string newBoard, string[] newCodes)
			{
				this.board = newBoard.ToUpper();
				this.codes = newCodes;
			}

			// Token: 0x04000248 RID: 584
			public string board;

			// Token: 0x04000249 RID: 585
			public string[] codes;
		}

		// Token: 0x02000083 RID: 131
		public struct PS4RegionBoardCarrier
		{
			// Token: 0x060005A8 RID: 1448 RVA: 0x00017FB3 File Offset: 0x000163B3
			public PS4RegionBoardCarrier(string newRegion, string[] newBoard)
			{
				this.region = newRegion;
				this.board = newBoard;
			}

			// Token: 0x0400024A RID: 586
			public string region;

			// Token: 0x0400024B RID: 587
			public string[] board;
		}

		// Token: 0x02000084 RID: 132
		[Serializable]
		public class AgeRatingInfo
		{
			// Token: 0x060005A9 RID: 1449 RVA: 0x00017FC4 File Offset: 0x000163C4
			public AgeRatingInfo(string userKey)
			{
				this.board = userKey;
			}

			// Token: 0x0400024C RID: 588
			public bool ps4Active;

			// Token: 0x0400024D RID: 589
			public string board = string.Empty;

			// Token: 0x0400024E RID: 590
			public string AgeNone = string.Empty;

			// Token: 0x0400024F RID: 591
			public string AgeSteam = string.Empty;

			// Token: 0x04000250 RID: 592
			public string AgeOculus = string.Empty;

			// Token: 0x04000251 RID: 593
			public string AgePS4 = string.Empty;
		}
	}
}
