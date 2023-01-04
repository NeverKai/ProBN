using System;
using Voxels.TowerDefense.Flag;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F0 RID: 2032
	public class Standard : AgentComponent, IAttackResponder
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000E4E30 File Offset: 0x000E3230
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000E4E38 File Offset: 0x000E3238
		public Standard.Invulnerability invulnerability
		{
			get
			{
				return this._invulnerability;
			}
			set
			{
				if (this._invulnerability != value)
				{
					this._invulnerability = value;
					this.UpdateTriflowGehaviour();
				}
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000E4E54 File Offset: 0x000E3254
		public override void Setup()
		{
			using ("Standard.Setup")
			{
				base.Setup();
				HeroDefinition hero = base.enSquad.hero;
				base.GetComponentInChildren<FlagMesh>(true).SetHero(hero);
				base.agent.spriteAnimator.SetSprite2(hero.graphics.agentSpriteHero);
				base.agent.stun.disableTriflowWhenDown = true;
				base.enSquad.onSquadChanged += this.UpdateTriflowGehaviour;
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000E4EF4 File Offset: 0x000E32F4
		private void OnDestroy()
		{
			if (base.agent && base.enSquad)
			{
				base.enSquad.onSquadChanged -= this.UpdateTriflowGehaviour;
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000E4F2D File Offset: 0x000E332D
		private void UpdateTriflowGehaviour()
		{
			base.agent.stun.disableTriflowWhenDown = this.IsInvulnerable();
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000E4F45 File Offset: 0x000E3345
		public void ModifyAttack(ref Attack attack)
		{
			if (this.IsInvulnerable())
			{
				attack.damage = 0f;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000E4F60 File Offset: 0x000E3360
		public bool IsInvulnerable()
		{
			Standard.Invulnerability invulnerability = this.invulnerability;
			return invulnerability == Standard.Invulnerability.ForceOn || (invulnerability != Standard.Invulnerability.ForceOff && base.agent.squad.agents.Count > 1);
		}

		// Token: 0x0400241E RID: 9246
		private Standard.Invulnerability _invulnerability;

		// Token: 0x020007F1 RID: 2033
		public enum Invulnerability
		{
			// Token: 0x04002420 RID: 9248
			Default,
			// Token: 0x04002421 RID: 9249
			ForceOn,
			// Token: 0x04002422 RID: 9250
			ForceOff
		}
	}
}
