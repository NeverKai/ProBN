using System;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000707 RID: 1799
	public class LevelLoading : ChildComponent<LevelNode>, LevelNode.ILevelSetup
	{
		// Token: 0x06002E87 RID: 11911 RVA: 0x000B5860 File Offset: 0x000B3C60
		void LevelNode.ILevelSetup.OnLevelSetup(LevelNode level)
		{
			this.StateChanged(level.islandProxy.state);
			level.islandProxy.onStateChanged += this.StateChanged;
			base.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
			base.enabled = false;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000B58BC File Offset: 0x000B3CBC
		private void StateChanged(IslandProxy.State newState)
		{
			if (base.manager.islandProxy.isGenerating)
			{
				base.enabled = true;
			}
			else
			{
				base.enabled = false;
				base.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
			}
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000B5910 File Offset: 0x000B3D10
		private void Update()
		{
			float b = Mathf.Floor(Time.time + (float)base.manager.levelState.index * 0.25f) * 90f;
			this.angle = Mathf.LerpAngle(this.angle, b, Time.deltaTime * 10f);
			base.transform.rotation = Quaternion.Euler(0f, 0f, 45f - this.angle);
		}

		// Token: 0x04001EC2 RID: 7874
		private float angle;
	}
}
