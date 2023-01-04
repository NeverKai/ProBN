using System;
using System.Collections;
using System.Collections.Generic;
using CS.Lights;
using CS.Platform;
using ReflexCLI.Attributes;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.CoinDispensing
{
	// Token: 0x0200072D RID: 1837
	public class CoinDispenser : Singleton<CoinDispenser>, IGameSetup, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000C1889 File Offset: 0x000BFC89
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000C1896 File Offset: 0x000BFC96
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000C189E File Offset: 0x000BFC9E
		public int totalCoins { get; private set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000C18A7 File Offset: 0x000BFCA7
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x000C18AF File Offset: 0x000BFCAF
		private int availableCoins
		{
			get
			{
				return this._availableCoins;
			}
			set
			{
				if (this._availableCoins == value)
				{
					return;
				}
				this._availableCoins = value;
				this.coinLight.ShowCoin(this._availableCoins > 0);
				this.onAvaliableCoinsChange(this._availableCoins);
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C18EC File Offset: 0x000BFCEC
		void IGameSetup.OnGameAwake()
		{
			this.goldCoins.Init(this.goldCoinPrefab, base.transform);
			this.coinLight = base.GetComponent<CoinLight>();
			this.checkpointTokens.Init(this.checkpointTokenPrefab, base.transform);
			this.checkpointTokens.ExpandTo(1);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C1940 File Offset: 0x000BFD40
		public bool ExtractCoin()
		{
			if (this.availableCoins > 0)
			{
				this.availableCoins--;
				DispensedCoin dispensedCoin = this.activeGoldCoins[this.availableCoins];
				dispensedCoin.Disappear();
				FabricWrapper.PostEvent(this.coinAddSound, dispensedCoin.gameObject);
				return true;
			}
			return false;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000C1994 File Offset: 0x000BFD94
		public bool ReturnCoin()
		{
			if (this.availableCoins < this.activeGoldCoins.Count)
			{
				this.availableCoins++;
				DispensedCoin dispensedCoin = this.activeGoldCoins[this.availableCoins - 1];
				dispensedCoin.Reappear();
				FabricWrapper.PostEvent(this.coinRemoveSound, dispensedCoin.gameObject);
				return true;
			}
			return false;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000C19F4 File Offset: 0x000BFDF4
		public IEnumerator DispenseCoinsRoutine(bool debug = false)
		{
			List<PhilosophersStoneAbility> philosophersStones = new List<PhilosophersStoneAbility>(4);
			yield return null;
			foreach (Squad squad in this.island.english.allSquads)
			{
				EnglishSquad englishSquad = (EnglishSquad)squad;
				if (englishSquad.alive)
				{
					PhilosophersStoneAbility upgrade = englishSquad.upgradeManager.GetUpgrade<PhilosophersStoneAbility>();
					if (upgrade)
					{
						EvacuateAbility upgrade2 = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
						if (!upgrade2 || upgrade2.state == EvacuateAbility.State.None)
						{
							philosophersStones.Add(upgrade);
						}
					}
				}
			}
			int houseCoins = 0;
			int coinSoundIndex = 0;
			for (float t = 0f; t < 0.1f; t += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			foreach (House house in this.island.village.houses)
			{
				int coinCount = house.coinCount;
				if (coinCount > 0)
				{
					bool allowedAnyCoins = debug || Singleton<IslandGameplayManager>.instance.endOfLevel.reason == EndOfLevel.Reason.Won;
					bool intact = (!debug) ? house.intact : (UnityEngine.Random.value < 0.5f);
					if (intact && allowedAnyCoins)
					{
						FabricWrapper.PostEvent(this.houseSavedSound);
						SquadReplenishLocation highlighter = house.GetComponent<SquadReplenishLocation>();
						highlighter.TriggerGold();
						for (float t2 = 0f; t2 < 0.3f; t2 += Time.unscaledDeltaTime)
						{
							yield return null;
						}
						for (int i = 0; i < coinCount; i++)
						{
							int value;
							coinSoundIndex = (value = coinSoundIndex) + 1;
							int coinSoundId = this.goldCoinSounds[Mathf.Clamp(value, 0, this.goldCoinSounds.Length - 1)];
							DispensedCoin instance = this.goldCoins.GetInstance();
							instance.transform.SetParent(house.coinSpawnPos);
							instance.transform.localPosition = Vector3.up * (this.coinSpacing * (float)i + this.coinPadding);
							FabricWrapper.PostEvent(coinSoundId, house.gameObject);
							this.goldEffect.PlayAt(instance.transform.position);
							this.activeGoldCoins.Add(instance);
							this.totalCoins++;
							houseCoins++;
							for (float t3 = 0f; t3 < 0.3f; t3 += Time.unscaledDeltaTime)
							{
								yield return null;
							}
						}
						coinSoundIndex--;
					}
					else
					{
						for (int j = 0; j < coinCount; j++)
						{
							FabricWrapper.PostEvent(this.burntCoinSound, house.gameObject);
							this.burntEffect.PlayAt(house.coinSpawnPos.transform.position + Vector3.up * (this.coinSpacing * (float)j + this.coinPadding));
							for (float t4 = 0f; t4 < 0.3f; t4 += Time.unscaledDeltaTime)
							{
								yield return null;
							}
						}
						coinSoundIndex = 0;
					}
					for (float t5 = 0f; t5 < 0.3f; t5 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
			}
			foreach (PhilosophersStoneAbility ps in philosophersStones)
			{
				for (float t6 = 0f; t6 < 0.3f; t6 += Time.unscaledDeltaTime)
				{
					yield return null;
				}
				for (int k = 0; k < ps.numCoins; k++)
				{
					DispensedCoin coin = this.goldCoins.GetInstance();
					coin.transform.SetParent(ps.coinSpawnTransform);
					coin.transform.localPosition = Vector3.up * (this.coinSpacing * (float)ps.coins.Count + this.coinPadding);
					FabricWrapper.PostEvent(this.philosophersStoneCoinAppearSound, coin.gameObject);
					this.goldEffect.PlayAt(coin.transform.position);
					coin.SetRed(true);
					this.activeGoldCoins.Add(coin);
					ps.coins.Add(coin);
					this.totalCoins++;
					for (float t7 = 0f; t7 < 0.3f; t7 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
				for (float t8 = 0f; t8 < 0.3f; t8 += Time.unscaledDeltaTime)
				{
					yield return null;
				}
				foreach (DispensedCoin coin2 in ps.coins)
				{
					coin2.SetRed(false);
					FabricWrapper.PostEvent(this.philosophersStoneCoinDropSound, coin2.gameObject);
					for (float t9 = 0f; t9 < 0.1f; t9 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
				ps.coins.Clear();
				for (float t10 = 0f; t10 < 0.2f; t10 += Time.unscaledDeltaTime)
				{
					yield return null;
				}
			}
			if (this.island.village.checkpointHouse)
			{
				House cpHouse = this.island.village.checkpointHouse.house;
				if (cpHouse.intact)
				{
					FabricWrapper.PostEvent(this.houseSavedSound);
					SquadReplenishLocation highlighter2 = cpHouse.GetComponent<SquadReplenishLocation>();
					highlighter2.TriggerGold();
					for (float t11 = 0f; t11 < 0.3f; t11 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
					DispensedCoin token = this.checkpointTokens.GetInstance();
					token.transform.SetParent(cpHouse.coinSpawnPos);
					token.transform.localPosition = Vector3.up * this.coinPadding;
					Animator animator = token.GetComponent<Animator>();
					animator.SetLayerWeight(animator.GetLayerIndex("Red"), 0f);
					FabricWrapper.PostEvent(this.tokenAppearSound, cpHouse.gameObject);
					this.tokenEffect.PlayAt(token.transform.position);
					for (float t12 = 0f; t12 < 0.6f; t12 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
			}
			yield return null;
			this.availableCoins = this.activeGoldCoins.Count;
			Profile.userSave.stats.coinsCollected += this.totalCoins;
			if (this.totalCoins > 0)
			{
				BasePlatformManager.Instance.IncrementStatistic("STAT_GOLD_COLLECTED", (float)this.totalCoins);
			}
			this.onAvaliableCoinsChange(this._availableCoins);
			if (debug)
			{
				for (float t13 = 0f; t13 < 3f; t13 += Time.unscaledDeltaTime)
				{
					yield return null;
				}
				this.goldCoins.ReturnAll();
				this.activeGoldCoins.Clear();
				this.checkpointTokens.ReturnAll();
				this.availableCoins = 0;
				this.totalCoins = 0;
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000C1A18 File Offset: 0x000BFE18
		public IEnumerator TeaseCoinsRoutine(float teaseTime)
		{
			House[] houses = this.island.village.houses;
			yield return null;
			foreach (House house in houses)
			{
				house.GetComponent<SquadReplenishLocation>().TriggerGold();
			}
			FabricWrapper.PostEvent(this.houseSavedSound);
			for (float t = 0f; t < 0.5f; t += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			float endTime = Time.unscaledTime + teaseTime;
			int maxCoins = 0;
			foreach (House house2 in houses)
			{
				maxCoins = Mathf.Max(maxCoins, house2.coinCount);
			}
			for (int i = 0; i < maxCoins; i++)
			{
				foreach (House house3 in houses)
				{
					if (i < house3.coinCount)
					{
						Vector3 worldTargetPos = house3.worldTargetPos;
						worldTargetPos.y += this.coinPadding + (float)i * this.coinSpacing;
						DispensedCoin instance = this.goldCoins.GetInstance();
						instance.transform.SetParent(house3.coinSpawnPos);
						instance.transform.localPosition = Vector3.up * (this.coinSpacing * (float)i + this.coinPadding);
						FabricWrapper.PostEvent(this.goldCoinSounds[i], house3.gameObject);
					}
				}
				for (float t2 = 0f; t2 < 0.5f; t2 += Time.unscaledDeltaTime)
				{
					yield return null;
				}
			}
			while (endTime > Time.unscaledTime)
			{
				yield return null;
			}
			foreach (DispensedCoin dispensedCoin in this.goldCoins.inUse)
			{
				dispensedCoin.Disappear();
			}
			foreach (House house4 in houses)
			{
				FabricWrapper.PostEvent(this.coinVanishSound, house4.gameObject);
			}
			for (float t3 = 0f; t3 < 0.3f; t3 += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			this.goldCoins.ReturnAll();
			this.checkpointTokens.ReturnAll();
			yield break;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000C1A3A File Offset: 0x000BFE3A
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
			this.goldCoins.ReturnAll();
			this.checkpointTokens.ReturnAll();
			this.activeGoldCoins.Clear();
			this.availableCoins = 0;
			this.totalCoins = 0;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000C1A78 File Offset: 0x000BFE78
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			this._island.Target = island;
			int coins = (int)((float)island.levelNode.levelState.coinCount * 1.5f);
			while (this.goldCoins.capacity < coins)
			{
				this.goldCoins.AddInstance();
				yield return null;
			}
			foreach (House house in island.village.houses)
			{
				Vector3 position = house.coinSpawnPos.position;
				Vector3 vector = position + Vector3.up * (this.coinPadding + this.coinTravelDist);
				Vector3 vector2 = vector + Vector3.up * this.coinSpacing * (float)(house.coinCount - 1);
				house.coinBounds = new Bounds((vector + vector2) * 0.5f, vector2 - vector + Vector3.one * 0.33f);
			}
			yield break;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000C1A9A File Offset: 0x000BFE9A
		[ConsoleCommand("")]
		[ContextMenu("Dispense Coins")]
		private static void DispenseCoins()
		{
			if (Singleton<CoinDispenser>.instance.island)
			{
				Singleton<CoinDispenser>.instance.StartCoroutine(Singleton<CoinDispenser>.instance.DispenseCoinsRoutine(true));
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000C1AC6 File Offset: 0x000BFEC6
		[ConsoleCommand("")]
		[ContextMenu("Tease Coins")]
		private static void TeaseCoins()
		{
			if (Singleton<CoinDispenser>.instance.island)
			{
				Singleton<CoinDispenser>.instance.StartCoroutine(Singleton<CoinDispenser>.instance.TeaseCoinsRoutine(2f));
			}
		}

		// Token: 0x04001FC4 RID: 8132
		[SerializeField]
		private DispensedCoin goldCoinPrefab;

		// Token: 0x04001FC5 RID: 8133
		[SerializeField]
		private DispensedCoin checkpointTokenPrefab;

		// Token: 0x04001FC6 RID: 8134
		[SerializeField]
		private ReusableParticle goldEffect;

		// Token: 0x04001FC7 RID: 8135
		[SerializeField]
		private ReusableParticle burntEffect;

		// Token: 0x04001FC8 RID: 8136
		[SerializeField]
		private ReusableParticle tokenEffect;

		// Token: 0x04001FC9 RID: 8137
		[SerializeField]
		private FabricEventArray goldCoinSounds = new FabricEventArray("UI/InGame/Coin{0:00}", 1, 3);

		// Token: 0x04001FCA RID: 8138
		[SerializeField]
		private FabricEventReference burntCoinSound;

		// Token: 0x04001FCB RID: 8139
		[SerializeField]
		private FabricEventReference houseSavedSound = "UI/InGame/HouseSaved";

		// Token: 0x04001FCC RID: 8140
		private FabricEventReference houseTeaseAudioId = "UI/InGame/HouseFlash";

		// Token: 0x04001FCD RID: 8141
		private FabricEventReference coinRemoveSound = "UI/InGame/CoinRemove";

		// Token: 0x04001FCE RID: 8142
		private FabricEventReference coinAddSound = "UI/InGame/CoinAdd";

		// Token: 0x04001FCF RID: 8143
		private FabricEventReference coinVanishSound = "UI/InGame/CoinVanish";

		// Token: 0x04001FD0 RID: 8144
		private FabricEventReference philosophersStoneCoinAppearSound = "UI/InGame/PhilosopherCoinSpawn";

		// Token: 0x04001FD1 RID: 8145
		private FabricEventReference philosophersStoneCoinDropSound = "UI/InGame/PhilosopherCoinDrop";

		// Token: 0x04001FD2 RID: 8146
		private FabricEventReference tokenAppearSound = "UI/InGame/CheckpointToken";

		// Token: 0x04001FD3 RID: 8147
		[Header("Spacing")]
		[SerializeField]
		private float coinPadding = 0.1f;

		// Token: 0x04001FD4 RID: 8148
		[Header("Spacing")]
		[SerializeField]
		private float coinSpacing = 0.2f;

		// Token: 0x04001FD5 RID: 8149
		[SerializeField]
		private float coinSpeed = 10f;

		// Token: 0x04001FD6 RID: 8150
		[SerializeField]
		private float coinTravelDist = 0.5f;

		// Token: 0x04001FD7 RID: 8151
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x04001FD8 RID: 8152
		private LocalPool<DispensedCoin> checkpointTokens = new LocalPool<DispensedCoin>();

		// Token: 0x04001FD9 RID: 8153
		private LocalPool<DispensedCoin> goldCoins = new LocalPool<DispensedCoin>();

		// Token: 0x04001FDA RID: 8154
		private List<DispensedCoin> activeGoldCoins = new List<DispensedCoin>();

		// Token: 0x04001FDC RID: 8156
		public CoinLight coinLight;

		// Token: 0x04001FDD RID: 8157
		private int _availableCoins;

		// Token: 0x04001FDE RID: 8158
		public Action<int> onAvaliableCoinsChange = delegate(int A_0)
		{
		};
	}
}
