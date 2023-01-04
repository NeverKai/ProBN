using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000852 RID: 2130
	public class MineAbility : NavSpotTargetableAbility
	{
		// Token: 0x060037C5 RID: 14277 RVA: 0x000F077C File Offset: 0x000EEB7C
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.settings = this.settingsAtLevel[base.upgradeLevel];
			this._chargesRemaining = (this._chargesPerIsland = this.settings.charges);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000F07BC File Offset: 0x000EEBBC
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			ProximityMine instance = this.minePrefab.GetInstance<ProximityMine>(base.squad.heroAgent.transform);
			instance.Setup(base.squad.heroAgent, this.settings.explosion, this.settings.detectionRadius);
			MineLayerComponent component = base.squad.heroAgent.GetComponent<MineLayerComponent>();
			component.StartPlacing(instance, target, new Action(base.OnEnded), this.placeAudio);
		}

		// Token: 0x040025EB RID: 9707
		[Header("Mine ability Leveling")]
		[SerializeField]
		private MineAbility.Settings[] settingsAtLevel = new MineAbility.Settings[3];

		// Token: 0x040025EC RID: 9708
		private MineAbility.Settings settings;

		// Token: 0x040025ED RID: 9709
		[SerializeField]
		private ProximityMine minePrefab;

		// Token: 0x040025EE RID: 9710
		[SerializeField]
		private FabricEventReference placeAudio = "Sfx/Mine/Place";

		// Token: 0x02000853 RID: 2131
		[Serializable]
		public class Settings
		{
			// Token: 0x040025EF RID: 9711
			public ExplosionDef explosion;

			// Token: 0x040025F0 RID: 9712
			public int charges = 1;

			// Token: 0x040025F1 RID: 9713
			public float detectionRadius = 0.55f;
		}
	}
}
