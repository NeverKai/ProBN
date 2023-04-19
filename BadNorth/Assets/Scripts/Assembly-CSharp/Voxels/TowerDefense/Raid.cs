using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.RaidGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000721 RID: 1825
	public class Raid : IslandComponent, IIslandFirstEnter, IIslandWipe, IIslandPlay
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000BDF8C File Offset: 0x000BC38C
		public Wave randomWave => this.waves[UnityEngine.Random.Range(0, this.waves.Count)];

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000BDFAA File Offset: 0x000BC3AA
		private int bounty
		{
			get
			{
				return this.waves.Sum((Wave x) => x.bounty);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000BDFD4 File Offset: 0x000BC3D4
		public int numWaves
		{
			get
			{
				return this.waves.Count;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000BDFE1 File Offset: 0x000BC3E1
		public int finalWaveIdx
		{
			get
			{
				return this.waves.Count - 1;
			}
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06002F4F RID: 12111 RVA: 0x000BDFF0 File Offset: 0x000BC3F0
		// (remove) Token: 0x06002F50 RID: 12112 RVA: 0x000BE028 File Offset: 0x000BC428
		
		public event BeginWaveDelegate OnBeginWave = delegate(int A_0)
		{
		};

		// Token: 0x06002F51 RID: 12113 RVA: 0x000BE05E File Offset: 0x000BC45E
		public float GetWaveStartTime(int waveIndex)
		{
			return (waveIndex < 0 || waveIndex >= this.waves.Count) ? 0f : this.waves[waveIndex].waveStartTime;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000BE094 File Offset: 0x000BC494
		public int GetCurrentWaveIdx()
		{
			if (this.waveIndex >= this.waves.Count)
			{
				return this.waves.Count - 1;
			}
			if (!this.waves[this.waveIndex].triggered)
			{
				return this.waveIndex - 1;
			}
			return this.waveIndex;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000BE0EF File Offset: 0x000BC4EF
		private Wave GetCurrentWave()
		{
			return this.waves[this.GetCurrentWaveIdx()];
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000BE104 File Offset: 0x000BC504
		public float GetTimeToNextWave()
		{
			int num = this.GetCurrentWaveIdx() + 1;
			if (num >= this.waves.Count)
			{
				return float.MaxValue;
			}
			return this.GetWaveStartTime(num) - this.timer;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000BE13F File Offset: 0x000BC53F
		public bool AllEnemiesDefeated()
		{
			return this.waves.Last<Wave>().haveAllLaunched && base.island.vikings.agents.Count == 0;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000BE171 File Offset: 0x000BC571
		public bool AllWavesLaunched()
		{
			return this.waves.Last<Wave>().haveAllLaunched;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000BE183 File Offset: 0x000BC583
		public bool AllWavesSpawned()
		{
			return this.waves.Last().haveAllSpawned;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000BE198 File Offset: 0x000BC598
		public int TotalNumberRaiders()
		{
			int num = 0;
			foreach (Wave wave in this.waves)
			{
				foreach (ShipGroup shipGroup in wave.shipGroups)
				{
					foreach (Landing landing in shipGroup.landings)
					{
						num += landing.agentCount;
					}
				}
			}
			return num;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000BE288 File Offset: 0x000BC688
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			List<Longship> possibleShips = island.levelNode.possibleShips;
			float minArea = possibleShips.Min((Longship x) => x.area);
			LevelState levelState = island.levelNode.levelState;
			int waveCount = (int)levelState.wavesCount;
			float bountyPerWave = (float)levelState.bountyPerWave * Raid.bountyMultiplier;
			CampaignDifficultySettings difficulty = island.levelNode.diffiucltySettings;
			float minWaveSpacing = (float)levelState.minWaveSpacing;
			float maxWaveSpacing = (float)levelState.maxWaveSpacing;
			if (minWaveSpacing == 0f || maxWaveSpacing == 0f)
			{
				float num = (island.levelNode.campaign.campaignSave.prefs.difficulty != Difficulty.Easy) ? 1f : 1.2f;
				minWaveSpacing = 20f * num;
				maxWaveSpacing = 22f * num;
			}
			this.landingContainer = base.gameObject.AddEmptyChild("Landings").transform;
			this.waves = new List<Wave>(waveCount);
			this.possibleAgents = island.levelNode.enemies;
			for (int k = 0; k < waveCount; k++)
			{
				Wave wave3 = this.landingContainer.gameObject.AddEmptyChild("Wave").AddComponent<Wave>();
				ShipGroup shipGroup2 = wave3.gameObject.AddEmptyChild("Group").AddComponent<ShipGroup>();
				Landing landing5 = shipGroup2.gameObject.AddEmptyChild("Landing").AddComponent<Landing>();
				ShipLoad shipLoad = landing5.gameObject.AddEmptyChild("Load").AddComponent<ShipLoad>();
				wave3.raid = this;
				wave3.AddShipGroup(shipGroup2);
				shipGroup2.AddLanding(landing5);
				landing5.Init(island);
				landing5.AddShipLoad(shipLoad);
				if (k < this.possibleAgents.Count)
				{
					shipLoad.vikingRef = this.possibleAgents[k];
				}
				else
				{
					shipLoad.vikingRef = this.possibleAgents[UnityEngine.Random.Range(0, this.possibleAgents.Count)];
				}
				shipLoad.count = Mathf.Max(1, Mathf.RoundToInt(minArea / shipLoad.vikingRef.agent.area));
				this.waves.Add(wave3);
			}
			List<Action> raidModifiers = Raid.RaidModifier.GetActions(this);
			int safetyCount = 0;
			float bountyTarget = (float)waveCount * bountyPerWave;
			while ((float)this.bounty < bountyTarget)
			{
				int num2;
				safetyCount = (num2 = safetyCount) + 1;
				if (num2 >= 10000)
				{
					break;
				}
				raidModifiers[UnityEngine.Random.Range(0, raidModifiers.Count)]();
				yield return new GenInfo("Raid", GenInfo.Mode.forceInterrupt);
			}
			this.waves = (from x in this.waves
			orderby (float)x.bounty * UnityEngine.Random.Range(1f, 2f)
			select x).ToList<Wave>();
			float startTime = 0f;
			for (int l2 = 0; l2 < this.waves.Count; l2++)
			{
				this.waves[l2].transform.SetSiblingIndex(l2);
				this.waves[l2].waveStartTime = startTime;
				startTime += UnityEngine.Random.Range(minWaveSpacing, maxWaveSpacing) + this.waves[l2].launchDuration;
			}
			using (IEnumerator<Landing> enumerator = this.waves.SelectMany((Wave w) => w.shipGroups.SelectMany((ShipGroup g) => g.landings)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Landing landing = enumerator.Current;
					landing.shipPrefab = (from x in possibleShips
					where x.area >= landing.agentArea
					select x).FirstOrDefault<Longship>();
					if (!landing.shipPrefab)
					{
						landing.shipPrefab = possibleShips.Last<Longship>();
					}
				}
			}
			float shipWidth = 0f;
			foreach (Wave wave4 in this.waves)
			{
				foreach (ShipGroup shipGroup3 in wave4.shipGroups)
				{
					foreach (Landing landing2 in shipGroup3.landings)
					{
						shipWidth += landing2.radius * 2f;
					}
				}
			}
			List<ShipGroup> orderedGroups = (from x in this.waves.SelectMany((Wave wave) => wave.shipGroups)
			orderby (float)x.bounty * 0.1f + (float)x.landings.Count
			select x).ToList<ShipGroup>();
			List<Beaches.Beach.Pos> beachPositions = island.beaches.GetBeachPositions(0.1f);
			Action<Landing> RemoveBeachPositions = delegate(Landing landing)
			{
				float num4 = landing.radius * landing.radius;
				Vector3 b = landing.navPos.pos + landing.dir * landing.radius * 0.5f;
				for (int num5 = beachPositions.Count - 1; num5 >= 0; num5--)
				{
					if ((beachPositions[num5].navPos.pos - b).sqrMagnitude < num4)
					{
						beachPositions.RemoveAt(num5);
					}
				}
			};
			yield return new GenInfo("before of ordered groups", GenInfo.Mode.forceInterrupt);
			List<Landing> placedLandings = new List<Landing>();
			for (int i = 0; i < orderedGroups.Count; i++)
			{
				yield return new GenInfo("beginning of ordered groups loop", GenInfo.Mode.forceInterrupt);
				ShipGroup group = orderedGroups[i];
				float extent = group.landings.Sum((Landing x) => x.shipPrefab.radius);
				Wave wave = group.wave;
				Landing landing0 = group.landings[0];
				Vector3 dir0 = Vector3.zero;
				Beaches.Beach.Pos beachPos0 = default(Beaches.Beach.Pos);
				IOrderedEnumerable<Beaches.Beach.Pos> fittingPositions = from x in beachPositions
				where x.distToEdge > extent
				orderby UnityEngine.Random.value + x.randomOffset
				select x;
				IOrderedEnumerable<Beaches.Beach.Pos> orderedPositions = from p in fittingPositions
				orderby UnityEngine.Random.value + 1f / (from g in wave.shipGroups
				where g.anyPlaced
				select g).Sum((ShipGroup g) => (g.avPos - p.navPos.pos).sqrMagnitude)
				select p;
				yield return new GenInfo("before ordered positions", GenInfo.Mode.forceInterrupt);
				foreach (Beaches.Beach.Pos beachPos in orderedPositions)
				{
					dir0 = beachPos.dir + beachPos.navPos.pos.normalized * 0.3f;
					if (landing0.TryPlace(beachPos.navPos, dir0, difficulty.shipSpeedMultiplier, placedLandings))
					{
						yield return new GenInfo("placed", GenInfo.Mode.forceInterrupt);
						RemoveBeachPositions(landing0);
						beachPos0 = beachPos;
						break;
					}
				}
				yield return new GenInfo("after ordered positions", GenInfo.Mode.forceInterrupt);
				List<Landing> pool = (from l in @group.landings
				where l != landing0
				select l).ToList<Landing>();
				if (pool.Count > 0 && beachPos0.navPos.valid)
				{
					orderedPositions = from x in beachPositions
					where x.beach == beachPos0.beach
					select x into p
					orderby (p.navPos.pos - landing0.pos).magnitude - Vector3.Dot(p.dir, beachPos0.dir) * 2f
					select p;
					foreach (Beaches.Beach.Pos beachPos2 in orderedPositions)
					{
						if (pool[0].TryPlace(beachPos2.navPos, beachPos2.dir + dir0 * 0.6f, difficulty.shipSpeedMultiplier, placedLandings))
						{
							yield return new GenInfo("placed", GenInfo.Mode.forceInterrupt);
							RemoveBeachPositions(pool[0]);
							pool.RemoveAt(0);
							if (pool.Count == 0)
							{
								break;
							}
						}
						yield return new GenInfo("Raid", GenInfo.Mode.interruptable);
					}
				}
				yield return new GenInfo("after ordered positions 2", GenInfo.Mode.forceInterrupt);
				if (pool.Count > 0)
				{
					ShipGroup shipGroup4 = group.wave.AddShipGroup(wave.gameObject.AddEmptyChild("Group").AddComponent<ShipGroup>());
					foreach (Landing landing3 in pool)
					{
						shipGroup4.AddLanding(landing3);
					}
					orderedGroups.Add(shipGroup4);
				}
			}
			for (int w2 = this.waves.Count - 1; w2 >= 0; w2--)
			{
				Wave wave5 = this.waves[w2];
				for (int s = wave5.shipGroups.Count - 1; s >= 0; s--)
				{
					ShipGroup shipGroup = wave5.shipGroups[s];
					for (int j = shipGroup.landings.Count - 1; j >= 0; j--)
					{
						Landing landing = shipGroup.landings[j];
						if (!landing.placed)
						{
							if (beachPositions.Any((Beaches.Beach.Pos p) => landing.TryPlace(p.navPos, -p.navPos.GetCliffVector(), difficulty.shipSpeedMultiplier, placedLandings)))
							{
								RemoveBeachPositions(landing);
							}
							else
							{
								yield return new GenInfo("before destroyed", GenInfo.Mode.forceInterrupt);
								shipGroup.landings.Remove(landing);
								UnityEngine.Object.Destroy(landing.gameObject);
								yield return new GenInfo("after destroyed", GenInfo.Mode.forceInterrupt);
							}
							if (shipGroup.landings.Count == 0)
							{
								wave5.shipGroups.RemoveAt(s);
								UnityEngine.Object.Destroy(shipGroup.gameObject);
							}
						}
					}
				}
				if (wave5.shipGroups.Count == 0)
				{
					UnityEngine.Debug.Log(string.Format("Removing unused wave at {0}", w2));
					if (w2 < this.waves.Count - 1)
					{
						float num3 = this.waves[w2 + 1].waveStartTime - this.waves[w2].waveStartTime;
						for (int m = w2 + 1; m < this.waves.Count; m++)
						{
							this.waves[m].waveStartTime -= num3;
						}
					}
					this.waves.RemoveAt(w2);
					UnityEngine.Object.Destroy(wave5.gameObject);
				}
			}
			int actualBounty = this.bounty;
			yield return new GenInfo("before disable", GenInfo.Mode.forceInterrupt);
			foreach (Collider collider in this.landingContainer.GetComponentsInChildren<Collider>())
			{
				collider.enabled = false;
			}
			yield return new GenInfo("Picking wave audio", GenInfo.Mode.interruptable);
			foreach (Wave wave2 in this.waves)
			{
				int bounty = 0;
				foreach (ShipGroup shipGroup5 in wave2.shipGroups)
				{
					foreach (Landing landing4 in shipGroup5.landings)
					{
						foreach (ShipLoad shipLoad2 in landing4.shipLoads)
						{
							if (shipLoad2.agentBounty > bounty)
							{
								bounty = shipLoad2.agentBounty;
								VikingReference vikingRef = shipLoad2.vikingRef;
								wave2.approachAudioId = vikingRef.approachAudioId;
								wave2.arriveAudioId = vikingRef.arriveAudioId;
							}
						}
					}
				}
				yield return new GenInfo("Assigning wave audio", GenInfo.Mode.interruptable);
			}
			yield break;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000BE2AC File Offset: 0x000BC6AC
		IEnumerator<GenInfo> IIslandPlay.OnIslandPlay(Island island)
		{
			yield return new GenInfo("BeforeSpawnShips", GenInfo.Mode.forceInterrupt);
			foreach (Wave wave2 in this.waves)
			{
				wave2.RefreshLandings();
			}
			foreach (Wave wave in this.waves)
			{
				foreach (ShipGroup shipGroups in wave.shipGroups)
				{
					foreach (Landing landing in shipGroups.landings)
					{
						landing.Spawn();
						yield return new GenInfo("Spawning longships", GenInfo.Mode.interruptable);
					}
				}
			}
			yield return new GenInfo("Spawning raid", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000BE2C8 File Offset: 0x000BC6C8
		public bool MaybeLaunchWaves()
		{
			if (this.waveIndex < this.waves.Count)
			{
				this.timer += Time.deltaTime;
				Wave wave = this.waves[this.waveIndex];
				if (wave.triggered)
				{
					if (wave.haveAllLaunched)
					{
						this.waveIndex++;
					}
				}
				else if (this.timer >= wave.waveStartTime)
				{
					this.BeginNextWave();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000BE352 File Offset: 0x000BC752
		public Landing GetFirstLanding()
		{
			return this.waves[0].shipGroups[0].landings[0];
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000BE378 File Offset: 0x000BC778
		public void BeginNextWave()
		{
			if (this.waveIndex < 0 || this.waveIndex >= this.waves.Count)
			{
				return;
			}
			Wave wave = this.waves[this.waveIndex];
			base.StartCoroutine(wave.BeginWave());
			this.timer = Mathf.Max(this.timer, wave.waveStartTime);
			this.OnBeginWave(this.waveIndex);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000BE3F0 File Offset: 0x000BC7F0
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			this.waveIndex = 0;
			this.timer = 0f;
			base.StopAllCoroutines();
			foreach (Wave wave in this.waves)
			{
				wave.Reset();
				foreach (ShipGroup shipGroup in wave.shipGroups)
				{
					foreach (Landing landing in shipGroup.landings)
					{
						landing.Reset();
					}
				}
			}
			yield return new GenInfo("Raid reset", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000BE40B File Offset: 0x000BC80B
		private void OnDestroy()
		{
			this.landingContainer = null;
			this.waves = null;
			this.possibleAgents = null;
			this.OnBeginWave = null;
		}

		// Token: 0x04001F98 RID: 8088
		public Transform landingContainer;

		// Token: 0x04001F99 RID: 8089
		[Header("DebugView - set up at runtime")]
		[SerializeField]
		public List<Wave> waves;

		// Token: 0x04001F9A RID: 8090
		private int waveIndex;

		// Token: 0x04001F9B RID: 8091
		private float timer;

		// Token: 0x04001F9D RID: 8093
		[ConsoleCommand("")]
		private static float bountyMultiplier = 1f;

		// Token: 0x04001F9E RID: 8094
		private List<VikingReference> possibleAgents;

		// Token: 0x02000722 RID: 1826
		// (Invoke) Token: 0x06002F64 RID: 12132
		public delegate void BeginWaveDelegate(int waveNum);

		// Token: 0x02000723 RID: 1827
		private static class RaidModifier
		{
			public static List<Action> GetActions(Raid raid)
			{
				return new List<Action>
				{
					delegate()
					{
						DoubleLoad(raid);
					},
					delegate()
					{
						MultiplyOneLoad(raid);
					},
					delegate()
					{
						MultiplyOneGroupLoads(raid);
					},
					delegate()
					{
						MultiplyOneWaveLoads(raid);
					},
					delegate()
					{
						ChangeOneLoad(raid);
					},
					delegate()
					{
						DuplicateOneShip(raid);
					},
					delegate()
					{
						Raid.RaidModifier.DuplicateOneGroup(raid);
					},
					delegate()
					{
						Raid.RaidModifier.MirrorOneGroup(raid);
					},
					delegate()
					{
						Raid.RaidModifier.SwapTwoShips(raid);
					},
					delegate()
					{
						Raid.RaidModifier.SwapTwoGroups(raid);
					}
				};
			}

			// Token: 0x06002F68 RID: 12136 RVA: 0x000BE518 File Offset: 0x000BC918
			private static void SetLoadCount(Raid raid, ShipLoad load, int newCount)
			{
				float num = raid.island.levelNode.possibleShips.Max((Longship x) => x.area);
				num -= (from x in load.landing.shipLoads
				where x != load
				select x).Sum((ShipLoad x) => x.area);
				int b = Mathf.RoundToInt(num / load.vikingRef.agent.area);
				load.count = Mathf.Max(1, Mathf.Min(newCount, b));
			}

			// Token: 0x06002F69 RID: 12137 RVA: 0x000BE5E0 File Offset: 0x000BC9E0
			private static void DoubleLoad(Raid raid)
			{
				Wave wave = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>();
				ShipLoad randomLoad = wave.randomGroup.randomLanding.randomLoad;
				Raid.RaidModifier.SetLoadCount(raid, randomLoad, randomLoad.count * 2);
			}

			// Token: 0x06002F6A RID: 12138 RVA: 0x000BE63C File Offset: 0x000BCA3C
			private static void MultiplyOneLoad(Raid raid)
			{
				Wave wave = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>();
				ShipLoad randomLoad = wave.randomGroup.randomLanding.randomLoad;
				Raid.RaidModifier.SetLoadCount(raid, randomLoad, (int)((float)randomLoad.count * UnityEngine.Random.Range(1f, 2f)));
			}

			// Token: 0x06002F6B RID: 12139 RVA: 0x000BE6A8 File Offset: 0x000BCAA8
			private static void MultiplyOneGroupLoads(Raid raid)
			{
				ShipGroup randomGroup = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>().randomGroup;
				float num = UnityEngine.Random.Range(1f, 2f);
				foreach (Landing landing in randomGroup.landings)
				{
					ShipLoad randomLoad = landing.randomLoad;
					Raid.RaidModifier.SetLoadCount(raid, randomLoad, (int)((float)randomLoad.count * num));
				}
			}

			// Token: 0x06002F6C RID: 12140 RVA: 0x000BE75C File Offset: 0x000BCB5C
			private static void MultiplyOneWaveLoads(Raid raid)
			{
				Wave wave = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>();
				float num = UnityEngine.Random.Range(1f, 2f);
				foreach (Landing landing in wave.shipGroups.SelectMany((ShipGroup x) => x.landings))
				{
					ShipLoad randomLoad = landing.randomLoad;
					Raid.RaidModifier.SetLoadCount(raid, randomLoad, (int)((float)randomLoad.count * num));
				}
			}

			// Token: 0x06002F6D RID: 12141 RVA: 0x000BE82C File Offset: 0x000BCC2C
			private static void ChangeOneLoad(Raid raid)
			{
				ShipLoad randomLoad = raid.randomWave.randomGroup.randomLanding.randomLoad;
				ShipLoad randomLoad2 = raid.randomWave.randomGroup.randomLanding.randomLoad;
				if (randomLoad.vikingRef == randomLoad2.vikingRef)
				{
					return;
				}
				VikingReference vikingRef = randomLoad2.vikingRef;
				VikingReference vikingRef2 = randomLoad.vikingRef;
				int newCount = randomLoad.bounty / vikingRef.bounty;
				int newCount2 = randomLoad2.bounty / vikingRef2.bounty;
				randomLoad.vikingRef = vikingRef;
				Raid.RaidModifier.SetLoadCount(raid, randomLoad, newCount);
				randomLoad2.vikingRef = vikingRef2;
				Raid.RaidModifier.SetLoadCount(raid, randomLoad2, newCount2);
			}

			// Token: 0x06002F6E RID: 12142 RVA: 0x000BE8C8 File Offset: 0x000BCCC8
			private static void DuplicateOneShip(Raid raid)
			{
				Landing landing = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>().randomGroup.randomLanding.Duplicate();
				landing.timeOffset = UnityEngine.Random.value;
			}

			// Token: 0x06002F6F RID: 12143 RVA: 0x000BE920 File Offset: 0x000BCD20
			private static void DuplicateOneGroup(Raid raid)
			{
				ShipGroup shipGroup = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>().randomGroup.Duplicate();
				shipGroup.timeOffset = UnityEngine.Random.value;
			}

			// Token: 0x06002F70 RID: 12144 RVA: 0x000BE970 File Offset: 0x000BCD70
			private static void MirrorOneGroup(Raid raid)
			{
				Wave wave = (from x in raid.waves
				orderby x.bounty
				select x).First<Wave>();
				ShipGroup randomGroup = wave.randomGroup;
				List<Landing> landings = randomGroup.landings;
				int num = (UnityEngine.Random.value >= 0.5f) ? 1 : 0;
				for (int i = landings.Count - 1; i >= num; i--)
				{
					Landing landing = landings[i];
					landing.Duplicate();
				}
			}

			// Token: 0x06002F71 RID: 12145 RVA: 0x000BEA00 File Offset: 0x000BCE00
			private static void SwapTwoShips(Raid raid)
			{
				Landing randomLanding = raid.randomWave.randomGroup.randomLanding;
				Landing randomLanding2 = raid.randomWave.randomGroup.randomLanding;
				ShipGroup shipGroup = randomLanding.shipGroup;
				ShipGroup shipGroup2 = randomLanding2.shipGroup;
				shipGroup.AddLanding(randomLanding2);
				shipGroup2.AddLanding(randomLanding);
			}

			// Token: 0x06002F72 RID: 12146 RVA: 0x000BEA50 File Offset: 0x000BCE50
			private static void SwapTwoGroups(Raid raid)
			{
				ShipGroup randomGroup = raid.randomWave.randomGroup;
				ShipGroup randomGroup2 = raid.randomWave.randomGroup;
				Wave wave = randomGroup.wave;
				Wave wave2 = randomGroup2.wave;
				wave.AddShipGroup(randomGroup2);
				wave2.AddShipGroup(randomGroup);
			}

			// Token: 0x06002F73 RID: 12147 RVA: 0x000BEA94 File Offset: 0x000BCE94
			[DebugSetting("Modify Raid Multiplier", "修改雷击倍频", DebugSettingLocation.Campaign)]
			private static void ModRaidMultiplier()
			{
				int num = Mathf.RoundToInt(Raid.bountyMultiplier);
				num = (num + 1) % 6;
				Raid.bountyMultiplier = ((num != 0) ? ((float)num) : 1f);
			}
		}
	}
}
