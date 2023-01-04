using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense
{
	// Token: 0x02000719 RID: 1817
	public class PaintAtlas : ChildComponent<Campaign>, Campaign.ICampaignGenerator, Campaign.ICampaignCreator
	{
		// Token: 0x06002F28 RID: 12072 RVA: 0x000BCB9C File Offset: 0x000BAF9C
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			Vector3 dimensions = this.GetDimensions();
			int width = Mathf.NextPowerOfTwo((int)dimensions.x * (int)dimensions.y);
			int height = Mathf.NextPowerOfTwo((int)dimensions.z);
			yield return null;
			base.manager.campaignSave.paintAtlas = new SavedTexture(width, height);
			this.tex = base.manager.campaignSave.paintAtlas.tex;
			yield return null;
			Color32[] pixels = new Color32[width * height];
			Color32 c = UnityEngine.Color.black;
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = c;
			}
			this.tex.SetPixels32(pixels);
			this.tex.Apply();
			yield return null;
			yield break;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000BCBB8 File Offset: 0x000BAFB8
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("PaintAtlas", GenInfo.Mode.interruptable);
			if (!this.tex)
			{
				this.tex = campaign.campaignSave.paintAtlas.tex;
			}
			Vector3 dimensions = this.GetDimensions();
			this.tex3d = new Fake3dTex(dimensions, this.tex);
			this.tex3d.SetShaderVariables("_PaintTex", "_PaintTexVolume", "_PaintTexSize");
			this.cachedPixels = new Color32[this.tex3d.texture.height * this.tex3d.texture.width];
			yield return info;
			yield break;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000BCBDA File Offset: 0x000BAFDA
		public void SavePixels()
		{
			this.tex3d.pixels.CopyTo(this.cachedPixels, 0);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000BCBF3 File Offset: 0x000BAFF3
		public void LoadPixels()
		{
			this.cachedPixels.CopyTo(this.tex3d.pixels, 0);
			this.tex3d.ApplyPixels();
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000BCC18 File Offset: 0x000BB018
		private Vector3 GetDimensions()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (LevelState levelState in base.manager.campaignSave.levelStates)
			{
				num = Mathf.Max(num, levelState.rectMaxX);
				num2 = Mathf.Max(num2, levelState.rectMaxY);
				num3 = Mathf.Max(num3, (int)levelState.height);
			}
			num3 += 2;
			return new Vector3((float)num, (float)num3, (float)num2);
		}

		// Token: 0x04001F61 RID: 8033
		[SerializeField]
		private Texture2D tex;

		// Token: 0x04001F62 RID: 8034
		[SerializeField]
		public Fake3dTex tex3d;

		// Token: 0x04001F63 RID: 8035
		private Color32[] cachedPixels;
	}
}
