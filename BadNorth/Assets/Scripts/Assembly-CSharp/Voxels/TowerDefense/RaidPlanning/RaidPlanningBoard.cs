using System;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B1 RID: 1457
	public class RaidPlanningBoard : Singleton<RaidPlanningBoard>
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x00077D7C File Offset: 0x0007617C
		private void OnValidate()
		{
			if (base.transform.localPosition.y != 0f)
			{
				base.transform.localPosition = base.transform.localPosition.SetY(0f);
			}
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00077DC6 File Offset: 0x000761C6
		protected override void Awake()
		{
			base.Awake();
			this.waves = base.GetComponentsInChildren<RaidWaveToken>();
		}

		// Token: 0x0400182E RID: 6190
		public RaidWaveToken[] waves;
	}
}
