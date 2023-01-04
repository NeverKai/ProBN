using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x0200053E RID: 1342
	public class SquadSpawner : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060022F8 RID: 8952 RVA: 0x00067A1C File Offset: 0x00065E1C
		// (remove) Token: 0x060022F9 RID: 8953 RVA: 0x00067A54 File Offset: 0x00065E54
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<EnglishSquad> onSquadSpawned = delegate(EnglishSquad A_0)
		{
		};

		// Token: 0x060022FA RID: 8954 RVA: 0x00067A8A File Offset: 0x00065E8A
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00067A93 File Offset: 0x00065E93
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.island.Target = island;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00067AA1 File Offset: 0x00065EA1
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.island.Target = null;
			this.spawnSpots.Clear();
			this.bannedNavSpots.Clear();
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00067AC8 File Offset: 0x00065EC8
		public void SpawnEnglishSquads(IEnumerable<HeroDefinition> heroes)
		{
			List<HeroDefinition> list = new List<HeroDefinition>();
			list.AddRange(heroes);
			this.StartCoroutineTimed(3f, this.SpawnRoutine(list));
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00067AF4 File Offset: 0x00065EF4
		private IEnumerator<bool> SpawnRoutine(IEnumerable<HeroDefinition> heroes)
		{
			this.manager.EnglishDeployBegin();
			int maxLevel = 0;
			foreach (HeroDefinition heroDefinition in heroes)
			{
				maxLevel = Mathf.Max(maxLevel, heroDefinition.squadLevel);
			}
			if (maxLevel == 2)
			{
				FabricWrapper.PostEvent(this.beginDeployVeteran);
			}
			else if (maxLevel == 3)
			{
				FabricWrapper.PostEvent(this.beginDeployElite);
			}
			yield return false;
			IEnumerator s = this.SelectNavSpots(heroes.Count<HeroDefinition>());
			while (s.MoveNext())
			{
				yield return false;
			}
			float end = Time.time + this.initialDelay;
			while (end > Time.time)
			{
				yield return true;
			}
			int nIdx = 0;
			foreach (HeroDefinition hero in heroes)
			{
				float nextSquadTime = Time.time + this.secondaryDelay;
				NavSpot navSpot = null;
				do
				{
					List<Tuple<NavSpot, float>> list = this.spawnSpots;
					int index;
					nIdx = (index = nIdx) + 1;
					navSpot = list[index].item1;
				}
				while (this.bannedNavSpots.Contains(navSpot));
				IEnumerator<bool> squad = this.SpawnEnglishSquad(hero, navSpot);
				while (squad.MoveNext())
				{
					bool flag = squad.Current;
					yield return flag;
				}
				while (nextSquadTime > Time.time)
				{
					yield return true;
				}
			}
			this.manager.EnglishDeployComplete();
			FabricWrapper.PostEvent(this.endDeploy);
			yield break;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00067B18 File Offset: 0x00065F18
		public IEnumerator<bool> SpawnEnglishSquad(HeroDefinition heroDef, NavSpot navSpot)
		{
			EnglishSquad squad = null;
			squad = (ScriptableObjectSingleton<PrefabManager>.instance.englishSquad.SpawnGetFromPrefab(navSpot.navPos, this.island.Target.english) as EnglishSquad);
			squad.squadTemplate = ScriptableObjectSingleton<PrefabManager>.instance.militiaTemplate;
			squad.hero = heroDef;
			squad.gameObject.SetActive(false);
			yield return true;
			squad.SpawnHero(navSpot.navPos);
			yield return true;
			squad.gameObject.SetActive(true);
			SquadUpgradeManager squadUpgradeManager = squad.upgradeManager;
			foreach (UpgradeComponent upgradeComp in ScriptableObjectSingleton<PrefabManager>.instance.universalUpgrades)
			{
				squadUpgradeManager.AddUpgrade(upgradeComp, 0);
				yield return false;
			}
			foreach (SerializableHeroUpgrade upgrade in heroDef.upgrades)
			{
				upgrade.definition.OnAppliedToSquad(squad, upgrade.level);
				yield return true;
			}
			foreach (UpgradeComponent upgradeComponent in squadUpgradeManager)
			{
			}
			IEnumerator r = squad.FillRoutine(navSpot);
			while (r.MoveNext())
			{
				yield return false;
			}
			FabricWrapper.PostEvent(this.squadSpawn);
			yield return true;
			int i = 0;
			int count = squad.agents.Count;
			while (i < count)
			{
				Agent agent = squad.agents[i];
				agent.Spawn();
				agent.velocity = (agent.navPos.pos - navSpot.navPos.pos) * 4f;
				Singleton<DustParticles>.instance.SpawnParticles(agent.navPos.wPos);
				i++;
			}
			yield return true;
			using ("onSquadSpawnCompleteDelegate")
			{
				squad.OnSquadSpawnComplete();
			}
			yield return false;
			using ("onSquadSpawnedDelegate")
			{
				this.onSquadSpawned(squad);
			}
			yield return false;
			yield break;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00067B44 File Offset: 0x00065F44
		private IEnumerator SelectNavSpots(int numSquads)
		{
			this.spawnSpots.Clear();
			foreach (NavSpot navSpot in this.island.Target.navSpotter.navSpots)
			{
				NavPos navPos = navSpot.navPos;
				float num = 0f;
				num += navPos.pos.y;
				num += navPos.GetBorderDistance();
				num -= navPos.pos.GetXZ().magnitude;
				num += Vector3.Dot(navSpot.lookDir, navPos.pos.SetY(0f).normalized);
				foreach (House house in this.island.Target.village.houses)
				{
					num -= Mathf.Abs(house.distanceField.SampleDistance(navSpot.vert) - 1.5f) * 0.3f;
				}
				this.spawnSpots.Add(new Tuple<NavSpot, float>(navSpot, num));
			}
			yield return null;
			this.spawnSpots.Sort((Tuple<NavSpot, float> a, Tuple<NavSpot, float> b) => b.item2.CompareTo(a.item2));
			if (numSquads == 4)
			{
				foreach (Tuple<NavSpot, float> tuple in this.spawnSpots)
				{
					NavSpot item = tuple.item1;
					NavSpot navSpot2 = item.neighbours[0];
					NavSpot navSpot3 = item.neighbours[2];
					NavSpot navSpot4 = item.neighbours[1];
					if (item && navSpot2 && navSpot3 && navSpot4)
					{
						this.spawnSpots.Insert(0, new Tuple<NavSpot, float>(item, 0f));
						this.spawnSpots.Insert(1, new Tuple<NavSpot, float>(navSpot2, 0f));
						this.spawnSpots.Insert(2, new Tuple<NavSpot, float>(navSpot3, 0f));
						this.spawnSpots.Insert(3, new Tuple<NavSpot, float>(navSpot4, 0f));
						break;
					}
				}
			}
			else if (numSquads == 3)
			{
				foreach (Tuple<NavSpot, float> tuple2 in this.spawnSpots)
				{
					NavSpot item2 = tuple2.item1;
					if (item2.neighbours[0] && item2.neighbours[4])
					{
						this.spawnSpots.Insert(0, new Tuple<NavSpot, float>(item2.neighbours[0], 0f));
						this.spawnSpots.Insert(1, new Tuple<NavSpot, float>(item2, 0f));
						this.spawnSpots.Insert(2, new Tuple<NavSpot, float>(item2.neighbours[4], 0f));
						break;
					}
					if (item2.neighbours[2] && item2.neighbours[6])
					{
						this.spawnSpots.Insert(0, new Tuple<NavSpot, float>(item2.neighbours[2], 0f));
						this.spawnSpots.Insert(1, new Tuple<NavSpot, float>(item2, 0f));
						this.spawnSpots.Insert(2, new Tuple<NavSpot, float>(item2.neighbours[6], 0f));
						break;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x0400155B RID: 5467
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("SquadSpawner", EVerbosity.Quiet, 0);

		// Token: 0x0400155C RID: 5468
		[SerializeField]
		private float initialDelay = 0.5f;

		// Token: 0x0400155D RID: 5469
		[SerializeField]
		private float secondaryDelay = 5f;

		// Token: 0x0400155E RID: 5470
		private FabricEventReference squadSpawn = "UI/InGame/SquadSpawn";

		// Token: 0x0400155F RID: 5471
		private FabricEventReference beginDeployVeteran = "Mus/VeteranMusic";

		// Token: 0x04001560 RID: 5472
		private FabricEventReference beginDeployElite = "Mus/EliteMusic";

		// Token: 0x04001561 RID: 5473
		private FabricEventReference endDeploy = "Mus/Ready";

		// Token: 0x04001562 RID: 5474
		private IslandGameplayManager manager;

		// Token: 0x04001563 RID: 5475
		private WeakReference<Island> island = new WeakReference<Island>(null);

		// Token: 0x04001564 RID: 5476
		private List<Tuple<NavSpot, float>> spawnSpots = new List<Tuple<NavSpot, float>>(64);

		// Token: 0x04001565 RID: 5477
		public List<NavSpot> bannedNavSpots = new List<NavSpot>();
	}
}
