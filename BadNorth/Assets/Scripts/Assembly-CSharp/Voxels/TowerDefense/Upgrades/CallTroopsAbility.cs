using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000847 RID: 2119
	public class CallTroopsAbility : HouseTargetableAbility
	{
		// Token: 0x06003758 RID: 14168 RVA: 0x000EE7C8 File Offset: 0x000ECBC8
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this._chargesRemaining = (this._chargesPerIsland = this.chargesAtUpgradeLevel[base.upgradeLevel]);
			this.maxAgentsCalled = this.agentsAtUpgradeLevel[base.upgradeLevel];
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000EE80C File Offset: 0x000ECC0C
		public override void SelectLocation(SquadReplenishLocation location)
		{
			this.location = location;
			FabricWrapper.PostEvent(this.hornSound, base.squad.heroAgent.gameObject);
			int numAgents = Mathf.Min(base.squad.maxCount - base.squad.livingAgents.Count, this.maxAgentsCalled);
			location.CallAgents(base.squad, numAgents);
			base.OnActivated();
			this.location = null;
			base.OnEnded();
		}

		// Token: 0x040025A0 RID: 9632
		[Header("Horn ability Leveling")]
		[SerializeField]
		private int[] agentsAtUpgradeLevel = new int[]
		{
			2,
			3,
			3
		};

		// Token: 0x040025A1 RID: 9633
		[SerializeField]
		private int[] chargesAtUpgradeLevel = new int[]
		{
			2,
			2,
			3
		};

		// Token: 0x040025A2 RID: 9634
		private FabricEventReference hornSound = "Sfx/Ability/Warhorn/Toot";

		// Token: 0x040025A3 RID: 9635
		private int maxAgentsCalled;
	}
}
