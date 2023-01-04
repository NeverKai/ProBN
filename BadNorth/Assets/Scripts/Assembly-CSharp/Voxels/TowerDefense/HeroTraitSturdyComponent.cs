using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000833 RID: 2099
	internal class HeroTraitSturdyComponent : AgentComponent, IAttackResponder
	{
		// Token: 0x060036B2 RID: 14002 RVA: 0x000EB880 File Offset: 0x000E9C80
		public override void Setup()
		{
			base.Setup();
			if (base.agent && base.agent.attackResponders != null && !base.agent.attackResponders.Contains(this))
			{
				base.agent.attackResponders.Add(this);
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000EB8DC File Offset: 0x000E9CDC
		void IAttackResponder.ModifyAttack(ref Attack attack)
		{
			attack.settings.knockback = attack.settings.knockback * this.stunMultiplier;
			attack.settings.stun = attack.settings.stun * this.stunMultiplier;
			attack.settings.launchImpulse = attack.settings.launchImpulse * this.launchMultiplier;
		}

		// Token: 0x0400251A RID: 9498
		public float knockbackMultiplier = 0.3f;

		// Token: 0x0400251B RID: 9499
		public float stunMultiplier = 0.2f;

		// Token: 0x0400251C RID: 9500
		public float launchMultiplier;
	}
}
