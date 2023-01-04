using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020006E7 RID: 1767
	public class Frontier : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000ADEC3 File Offset: 0x000AC2C3
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000ADECB File Offset: 0x000AC2CB
		public int currentDepthTarget { get; private set; }

		// Token: 0x06002D98 RID: 11672 RVA: 0x000ADED4 File Offset: 0x000AC2D4
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			this.extents.y = campaign.rect.yMax + 2f;
			this.totalRect = new Rect(-this.extents, this.extents * 2f);
			base.enabled = true;
			this.currentDepthTarget = campaign.campaignSave.vikingFrontierPosition;
			yield return new GenInfo("Frontier", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000ADEF6 File Offset: 0x000AC2F6
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x04001E2C RID: 7724
		private const float predictionLineDelay = 1f;

		// Token: 0x04001E2D RID: 7725
		private CanvasRenderer frontierRenderer;

		// Token: 0x04001E2E RID: 7726
		private static ShaderId frontierTexId = "_FrontierTex";

		// Token: 0x04001E2F RID: 7727
		private static ShaderId frontierParamsId = "_FrontierParams";

		// Token: 0x04001E30 RID: 7728
		private static ShaderId frontierRectId = "_FrontierRect";

		// Token: 0x04001E31 RID: 7729
		private static ShaderId colorId = "_Color";

		// Token: 0x04001E32 RID: 7730
		private static ShaderId radiusId = "_Radius";

		// Token: 0x04001E33 RID: 7731
		private static ShaderId cloudOffsetId = "_CloudOffset";

		// Token: 0x04001E35 RID: 7733
		private float timeStamp;

		// Token: 0x04001E36 RID: 7734
		private Vector2 extents;

		// Token: 0x04001E37 RID: 7735
		private Rect totalRect;

		// Token: 0x04001E38 RID: 7736
		private Rect drawRect;

		// Token: 0x04001E39 RID: 7737
		private Rect uvRect;

		// Token: 0x04001E3A RID: 7738
		private static CommandBuffer buffer;

		// Token: 0x04001E3B RID: 7739
		private static CommandBuffer permanentBuffer;

		// Token: 0x04001E3C RID: 7740
		private static MaterialPropertyBlock frontierBlock;
	}
}
