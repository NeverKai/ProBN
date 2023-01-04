using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200084B RID: 2123
	public class GroundPoundAbility : NavSpotTargetableAbility
	{
		// Token: 0x06003783 RID: 14211 RVA: 0x000EFA00 File Offset: 0x000EDE00
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.settings = this.levelSettings[base.upgradeLevel];
			this.settings.jumpSolver = base.GetComponent<IProjectileSolver>();
			this.settings.fabricLaunchID = base.squad.hero.voice.warhammerJumpSounds[base.upgradeLevel];
			this.settings.fabricLandID = base.squad.hero.voice.warhammerLandSounds[base.upgradeLevel];
			this.gpComp = base.squad.heroAgent.gameObject.GetComponent<GroundPoundComponent>();
			if (!this.gpComp)
			{
				this.gpComp = base.squad.heroAgent.gameObject.AddComponent<GroundPoundComponent>();
				this.gpComp.Setup(base.squad.heroAgent);
			}
			this.gpComp.onComplete += this.onComplete;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000EFB04 File Offset: 0x000EDF04
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			Vector3 worldPos = Vector3.Lerp(heroNavSpot.navPos.pos, target.navPos.pos, this.distanceLerp);
			NavPos landPos = new NavPos(heroNavSpot.navPos.navigationMesh, worldPos, true, 1f);
			this.gpComp.GroundPound(this.settings, landPos);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000EFB5E File Offset: 0x000EDF5E
		private void onComplete()
		{
			base.OnEnded();
		}

		// Token: 0x040025C4 RID: 9668
		[SerializeField]
		private GroundPoundComponent.Settings[] levelSettings = new GroundPoundComponent.Settings[2];

		// Token: 0x040025C5 RID: 9669
		private GroundPoundComponent.Settings settings = default(GroundPoundComponent.Settings);

		// Token: 0x040025C6 RID: 9670
		[SerializeField]
		[Range(0f, 1f)]
		private float distanceLerp = 0.65f;

		// Token: 0x040025C7 RID: 9671
		private GroundPoundComponent gpComp;
	}
}
