using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x02000534 RID: 1332
	public class IslandWinConditions : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x00064EE4 File Offset: 0x000632E4
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00064EF1 File Offset: 0x000632F1
		private Raid raid
		{
			get
			{
				return this.island.raid;
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00064EFE File Offset: 0x000632FE
		public void OnAwake(IslandGameplayManager manager)
		{
			this.manager = manager;
			this.runningState = manager.states.running;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00064F18 File Offset: 0x00063318
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00064F26 File Offset: 0x00063326
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00064F34 File Offset: 0x00063334
		private void Update()
		{
			if (this.runningState.active)
			{
				if (this.AllHeroesDead())
				{
					this.manager.endOfLevel.AllEnglishKilled();
				}
				else if (this.AllEnglishDeadOrEvac(EvacuateAbility.State.Confirmed))
				{
					this.manager.endOfLevel.Evacuated();
				}
				else if (this.AllEnemiesDefeated() && !this.AllEnglishDeadOrEvac(EvacuateAbility.State.Departed))
				{
					this.manager.endOfLevel.AllVikingsKilled();
				}
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00064FBC File Offset: 0x000633BC
		public bool AllHeroesDead()
		{
			if (this.island.english.agents.Count == 0)
			{
				return true;
			}
			foreach (Squad squad in this.island.english.allSquads)
			{
				EnglishSquad englishSquad = (EnglishSquad)squad;
				if (englishSquad.heroAgent)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00065058 File Offset: 0x00063458
		public bool AllEnemiesDefeated()
		{
			return this.raid.AllWavesSpawned() && this.island.vikings.agents.Count == 0;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x00065088 File Offset: 0x00063488
		private bool AllEnglishDeadOrEvac(EvacuateAbility.State minState)
		{
			foreach (Squad squad in this.island.english.allSquads)
			{
				EnglishSquad englishSquad = (EnglishSquad)squad;
				if (englishSquad.heroAgent)
				{
					EvacuateAbility upgrade = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
					if (upgrade && upgrade.state < minState)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400152B RID: 5419
		private IslandGameplayManager manager;

		// Token: 0x0400152C RID: 5420
		private State runningState;

		// Token: 0x0400152D RID: 5421
		private WeakReference<Island> _island = new WeakReference<Island>(null);
	}
}
