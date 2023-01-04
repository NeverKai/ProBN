using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000586 RID: 1414
	[Serializable]
	public class CampaignSave : ISaveGameObject
	{
		// Token: 0x06002495 RID: 9365 RVA: 0x00072F28 File Offset: 0x00071328
		public CampaignSave()
		{
			this.loadTime = Time.unscaledTime;
			this.saveTimeStamp = DateTime.UtcNow;
			WorldWeather.WeatherKey weatherKey = new WorldWeather.WeatherKey
			{
				time = this.day.value,
				cloudCoverage = 0f,
				wind = 0f
			};
			WorldWeather.WeatherKey key = weatherKey;
			key.time += 0.1f;
			this.weatherSystem = new WorldWeather.WeatherSystem(weatherKey.time, weatherKey, key);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00073045 File Offset: 0x00071445
		public static string FileNameGen(int slot)
		{
			return string.Format("{0} - {1}", "campaign", slot);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0007305C File Offset: 0x0007145C
		public void Saw(VikingAgent.Type vikingType)
		{
			if (!this.HasEverSeen(vikingType))
			{
				this.vikingsSeen.Add(vikingType);
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x0007307C File Offset: 0x0007147C
		public bool HasEverSeen(VikingAgent.Type vikingType)
		{
			foreach (SerializeFriendlyEnum<VikingAgent.Type> serializeFriendlyEnum in this.vikingsSeen)
			{
				if (serializeFriendlyEnum.value == vikingType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x000730E8 File Offset: 0x000714E8
		public int timeSinceLoad
		{
			get
			{
				return (int)(Time.unscaledTime - this.loadTime);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000730F7 File Offset: 0x000714F7
		public int playTime
		{
			get
			{
				return this.timeSinceLoad + this.savedPlayTime;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00073106 File Offset: 0x00071506
		public Year year2
		{
			get
			{
				return 0.3f + this.FractionComplete();
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00073119 File Offset: 0x00071519
		public bool gameOver
		{
			get
			{
				return this.gameOverReason != GameOverReason.None;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00073127 File Offset: 0x00071527
		public bool lostGame
		{
			get
			{
				return this.gameOverReason != GameOverReason.None && this.gameOverReason != GameOverReason.Won;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00073143 File Offset: 0x00071543
		public bool wonGame
		{
			get
			{
				return this.gameOverReason == GameOverReason.Won;
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00073150 File Offset: 0x00071550
		public bool HeroesAvailableThisTurn()
		{
			foreach (HeroDefinition heroDefinition in this.heroes)
			{
				if (heroDefinition.availableThisTurn)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000731BC File Offset: 0x000715BC
		public void NextTurn()
		{
			this.turnCount++;
			this.vikingFrontierPosition++;
			this.battlesWon = 0;
			this.battleCount = 0;
			this.day = Mathf.Ceil(this.day);
			this.day.hour = this.day.hour + UnityEngine.Random.Range(6.5f, 14f);
			this.turnStartDay = this.day;
			foreach (HeroDefinition heroDefinition in this.heroes)
			{
				heroDefinition.timesUsedThisTurn = 0;
			}
			foreach (int item in this.oldDeployOrder)
			{
				if (!this.deployOrder.Contains(item))
				{
					this.deployOrder.Add(item);
				}
			}
			this.oldDeployOrder.Clear();
			foreach (int item2 in this.deployOrder)
			{
				this.oldDeployOrder.Add(item2);
			}
			this.deployOrder.Clear();
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00073354 File Offset: 0x00071754
		public int GetNumHeroesAvailableThisTurn()
		{
			int num = 0;
			foreach (HeroDefinition heroDefinition in this.heroes)
			{
				num += ((!heroDefinition.availableThisTurn) ? 0 : 1);
			}
			return num;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000733C4 File Offset: 0x000717C4
		public int GetNumAvailableHeroes()
		{
			int num = 0;
			foreach (HeroDefinition heroDefinition in this.heroes)
			{
				num += ((!heroDefinition.available) ? 0 : 1);
			}
			return num;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00073434 File Offset: 0x00071834
		public HeroDefinition TryGetHeroDefinition(int heroId)
		{
			foreach (HeroDefinition heroDefinition in this.heroes)
			{
				if (heroDefinition.id == heroId)
				{
					return heroDefinition;
				}
			}
			return null;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000734A0 File Offset: 0x000718A0
		public float GetLevelFraction(LevelState level)
		{
			return (float)level.stepsFromStart / (float)(level.stepsFromStart + level.stepsFromEnd);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000734B8 File Offset: 0x000718B8
		public float FractionComplete()
		{
			if (this.levelStates == null)
			{
				return 0f;
			}
			float num = (!this.levelStates[1].unlocked) ? 0f : 0.02f;
			foreach (LevelState levelState in this.levelStates)
			{
				if (levelState.unlocked && levelState.playCount > 0)
				{
					foreach (int index in levelState.connections)
					{
						LevelState levelState2 = this.levelStates[index];
						if (levelState2.unlocked && levelState2.stepsFromEnd < levelState.stepsFromEnd)
						{
							num = Mathf.Max(num, this.GetLevelFraction(levelState));
						}
					}
				}
			}
			return Mathf.Clamp01(num);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000735E0 File Offset: 0x000719E0
		public int PercentComplete()
		{
			return Mathf.RoundToInt(this.FractionComplete() * 100f);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000735F4 File Offset: 0x000719F4
		public void ApplyLevelStatesToCheckpoint(Dictionary<int, int> knownSeeds)
		{
			int i = 0;
			int count = this.levelStates.Count;
			while (i < count)
			{
				int num;
				if (knownSeeds.TryGetValue(i, out num))
				{
					LevelState levelState = this.levelStates[i];
					bool flag = !levelState.goodSeed;
					levelState.seed = ((!flag) ? levelState.seed : num);
				}
				i++;
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0007365E File Offset: 0x00071A5E
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.saveTimeStamp = DateTime.UtcNow;
			this.savedPlayTime = this.playTime;
			this.loadTime = Time.unscaledTime;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00073684 File Offset: 0x00071A84
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.loadTime = Time.unscaledTime;
			if (this.serializedVersion != 19)
			{
				throw new Exception("Deliberately trashing campaign save");
			}
			if (this.stats == null)
			{
				Debug.Log("creating campaign stats");
				this.stats = new CampaignStats();
			}
			if (this.inventory == null)
			{
				Debug.Log("creating inventory");
				this.inventory = new List<SerializableHeroUpgrade>();
			}
			if (this.oldDeployOrder == null)
			{
				this.deployOrder = new List<int>(16);
				this.oldDeployOrder = new List<int>(16);
				foreach (HeroDefinition heroDefinition in this.heroes)
				{
					if (heroDefinition.recruited && heroDefinition.alive && !heroDefinition.availableThisTurn)
					{
						this.deployOrder.Add(heroDefinition.id);
					}
				}
			}
			if (this.prefs.difficulty == Difficulty.None)
			{
				this.prefs.difficulty = Difficulty.Hard;
			}
			if (this.vikingsSeen == null)
			{
				this.vikingsSeen = new List<SerializeFriendlyEnum<VikingAgent.Type>>();
			}
			else
			{
				this.vikingsSeen.Capacity = Mathf.Max(this.vikingsSeen.Capacity, VikingAgent.numTypes);
			}
			foreach (HeroDefinition heroDefinition2 in this.heroes)
			{
				if (heroDefinition2.recruited && heroDefinition2.alive)
				{
					this.coinBank += heroDefinition2.coins;
				}
				heroDefinition2.coins = 0;
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00073860 File Offset: 0x00071C60
		public void Unload()
		{
			this.levelAtlas.Unload();
			this.paintAtlas.Unload();
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00073878 File Offset: 0x00071C78
		public static implicit operator bool(CampaignSave cs)
		{
			return cs != null;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00073881 File Offset: 0x00071C81
		string ISaveGameObject.fileName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00073888 File Offset: 0x00071C88
		void ISaveGameObject.PostCreate(string fileName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001719 RID: 5913
		public const string fileNameBase = "campaign";

		// Token: 0x0400171A RID: 5914
		public const int expectedVersion = 19;

		// Token: 0x0400171B RID: 5915
		public readonly int serializedVersion = 19;

		// Token: 0x0400171C RID: 5916
		[ObjectDumper.FormatAttribute("0x{0:X8} ({0})")]
		public int seed;

		// Token: 0x0400171D RID: 5917
		[ObjectDumper.HideValuesAttribute]
		public List<LevelState> levelStates;

		// Token: 0x0400171E RID: 5918
		public List<HeroDefinition> heroes;

		// Token: 0x0400171F RID: 5919
		public List<SerializableHeroUpgrade> inventory = new List<SerializableHeroUpgrade>();

		// Token: 0x04001720 RID: 5920
		public int lastPlayedLevelIdx;

		// Token: 0x04001721 RID: 5921
		public int vikingFrontierPosition = -2;

		// Token: 0x04001722 RID: 5922
		public int turnCount;

		// Token: 0x04001723 RID: 5923
		public int battleCount;

		// Token: 0x04001724 RID: 5924
		public int battlesWon;

		// Token: 0x04001725 RID: 5925
		public int coinBank;

		// Token: 0x04001726 RID: 5926
		public bool hasAnyCheckpoints;

		// Token: 0x04001727 RID: 5927
		[ObjectDumper.HideValuesAttribute]
		public SavedTexture levelAtlas;

		// Token: 0x04001728 RID: 5928
		[ObjectDumper.HideValuesAttribute]
		public SavedTexture paintAtlas;

		// Token: 0x04001729 RID: 5929
		private int savedPlayTime;

		// Token: 0x0400172A RID: 5930
		public DateTime saveTimeStamp;

		// Token: 0x0400172B RID: 5931
		public List<int> deployOrder = new List<int>(16);

		// Token: 0x0400172C RID: 5932
		public List<int> oldDeployOrder = new List<int>(16);

		// Token: 0x0400172D RID: 5933
		[NonSerialized]
		public float loadTime;

		// Token: 0x0400172E RID: 5934
		private List<SerializeFriendlyEnum<VikingAgent.Type>> vikingsSeen = new List<SerializeFriendlyEnum<VikingAgent.Type>>(VikingAgent.numTypes);

		// Token: 0x0400172F RID: 5935
		[ObjectDumper.HideValuesAttribute]
		public WorldWeather.WeatherSystem weatherSystem;

		// Token: 0x04001730 RID: 5936
		public Day turnStartDay = 0.45f;

		// Token: 0x04001731 RID: 5937
		public Day day = 0.45f;

		// Token: 0x04001732 RID: 5938
		public GameOverReason gameOverReason;

		// Token: 0x04001733 RID: 5939
		[ObjectDumper.HideValuesAttribute]
		public CampaignStats stats = new CampaignStats();

		// Token: 0x04001734 RID: 5940
		public CampaignPrefs prefs = new CampaignPrefs(Difficulty.Normal, false, false);

		// Token: 0x04001735 RID: 5941
		public bool hasCheckpoint;

		// Token: 0x04001736 RID: 5942
		public int perfectDefenceStreak;

		// Token: 0x04001737 RID: 5943
		[Obsolete]
		private Year year = 0.3f;

		// Token: 0x04001738 RID: 5944
		[Obsolete]
		private string _fileName = string.Empty;
	}
}
