using System;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200083B RID: 2107
	public class TemplateUpgrade : UpgradeComponent
	{
		// Token: 0x060036D6 RID: 14038 RVA: 0x000EBA3D File Offset: 0x000E9E3D
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.squad.ApplySquadTeplate(this.squadTemplate);
		}

		// Token: 0x04002542 RID: 9538
		public SquadTemplate squadTemplate;
	}
}
