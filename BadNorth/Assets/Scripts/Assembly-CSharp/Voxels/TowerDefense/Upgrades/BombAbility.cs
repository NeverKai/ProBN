using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000845 RID: 2117
	public class BombAbility : NavSpotTargetableAbility
	{
		// Token: 0x06003753 RID: 14163 RVA: 0x000EE1EC File Offset: 0x000EC5EC
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.ballisticTargeter = (base.targeter as BallisticTargeter);
			this.thrower = base.squad.heroAgent.GetComponent<ThrowComponent>();
			this.settings = this.settingsAtLevel[base.upgradeLevel];
			this._chargesRemaining = (this._chargesPerIsland = this.settings.charges);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000EE254 File Offset: 0x000EC654
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			BombProjectile instance = this.projectilePrefab.GetInstance<BombProjectile>(null);
			NavPos navPos = target.navPos;
			navPos.pos += UnityEngine.Random.insideUnitSphere * this.targetError;
			instance.Setup(navPos, this.ballisticTargeter.solver, this.ballisticTargeter.projectileGravity, this.thrower.agent, this.settings.explosion);
			this.thrower.BeginThrowing(instance, navPos.pos, this.settings.throwAudio);
			this.thrower.onThrowComplete += this.Thrower_onThrowComplete;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000EE2FF File Offset: 0x000EC6FF
		private void Thrower_onThrowComplete()
		{
			this.thrower.onThrowComplete -= this.Thrower_onThrowComplete;
			base.OnEnded();
		}

		// Token: 0x04002597 RID: 9623
		[SerializeField]
		private float targetError = 0.2f;

		// Token: 0x04002598 RID: 9624
		[Header("Bomb")]
		[SerializeField]
		private BombProjectile projectilePrefab;

		// Token: 0x04002599 RID: 9625
		[Header("bomb ability Leveling")]
		[SerializeField]
		private BombAbility.Settings[] settingsAtLevel = new BombAbility.Settings[3];

		// Token: 0x0400259A RID: 9626
		private BombAbility.Settings settings;

		// Token: 0x0400259B RID: 9627
		private BallisticTargeter ballisticTargeter;

		// Token: 0x0400259C RID: 9628
		private ThrowComponent thrower;

		// Token: 0x02000846 RID: 2118
		[Serializable]
		private class Settings
		{
			// Token: 0x0400259D RID: 9629
			public ExplosionDef explosion;

			// Token: 0x0400259E RID: 9630
			public int charges = 1;

			// Token: 0x0400259F RID: 9631
			public FabricEventReference throwAudio;
		}
	}
}
