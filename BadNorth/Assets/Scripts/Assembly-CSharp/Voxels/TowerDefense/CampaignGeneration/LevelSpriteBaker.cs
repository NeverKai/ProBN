using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006C6 RID: 1734
	public class LevelSpriteBaker : ChildComponent<Campaign>, Campaign.ICampaignGenerator, Campaign.ICampaignCreator
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000A7660 File Offset: 0x000A5A60
		private int aa
		{
			get
			{
				return (int)Mathf.Pow(2f, 3f);
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000A7674 File Offset: 0x000A5A74
		public IEnumerable<object> BakeSprite(Island island)
		{
			LevelState levelState = island.levelNode.levelState;
			Rect spriteRect = new Rect(levelState.spriteRect.min * (float)this.pixelsPerWidth, levelState.spriteRect.size * (float)this.pixelsPerWidth);
			int width = (int)spriteRect.width;
			int height = (int)spriteRect.height;
			int texSize = LevelSpriteBaker.smallerTex.width;
			Rect pixelRect = new Rect((float)(texSize - width) / 2f, (float)(texSize - height) / 2f, (float)width, (float)height);
			float orthoSize = (float)(texSize / (this.pixelsPerWidth * 2));
			Matrix4x4 projectionMatrix = Matrix4x4.Ortho(-orthoSize, orthoSize, -orthoSize, orthoSize, -20f, 20f);
			projectionMatrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);
			IslandMeshes islandMeshes = island.GetComponentInChildren<IslandMeshes>(true);
			LevelSpriteBaker.buffer.SetProjectionMatrix(projectionMatrix);
			LevelSpriteBaker.buffer.SetRenderTarget(new RenderTargetIdentifier(LevelSpriteBaker.targetTex));
			LevelSpriteBaker.buffer.ClearRenderTarget(true, true, this.clearColor);
			LevelSpriteBaker.buffer.DrawMesh(islandMeshes.grassMesh.mesh, Matrix4x4.identity, LevelSpriteBaker.grassMaterial);
			LevelSpriteBaker.buffer.DrawMesh(islandMeshes.cliffMesh.mesh, Matrix4x4.identity, LevelSpriteBaker.cliffMaterial);
			LevelSpriteBaker.buffer.GenerateMips(LevelSpriteBaker.targetTex);
			LevelSpriteBaker.buffer.Blit(new RenderTargetIdentifier(LevelSpriteBaker.targetTex), new RenderTargetIdentifier(LevelSpriteBaker.smallerTex));
			yield return null;
			Graphics.ExecuteCommandBuffer(LevelSpriteBaker.buffer);
			yield return null;
			RenderTexture.active = LevelSpriteBaker.smallerTex;
			this.atlas.ReadPixels(pixelRect, (int)spriteRect.xMin, (int)spriteRect.yMin);
			this.atlas.Apply();
			LevelSpriteBaker.buffer.Clear();
			levelState.hasSprite = true;
			yield return null;
			yield break;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000A76A0 File Offset: 0x000A5AA0
		public Sprite GetSprite(LevelState levelState)
		{
			if (!this.atlas)
			{
				this.atlas = base.manager.campaignSave.levelAtlas.tex;
			}
			return Sprite.Create(this.atlas, new Rect(levelState.spriteRect.min * (float)this.pixelsPerWidth, levelState.spriteRect.size * (float)this.pixelsPerWidth), Vector2.one / 2f, (float)(this.pixelsPerWidth * 10), 0U, SpriteMeshType.FullRect);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000A773C File Offset: 0x000A5B3C
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			this.atlas = campaign.campaignSave.levelAtlas.tex;
			if (LevelSpriteBaker.buffer == null)
			{
				LevelSpriteBaker.buffer = new CommandBuffer();
				LevelSpriteBaker.grassMaterial = new Material(this.shader);
				LevelSpriteBaker.cliffMaterial = new Material(this.shader);
				LevelSpriteBaker.cliffMaterial.EnableKeyword("_CLIFF_ON");
				int num = Mathf.NextPowerOfTwo(13 * this.pixelsPerWidth);
				LevelSpriteBaker.smallerTex = new RenderTexture(num, num, 0, RenderTextureFormat.ARGB32);
				LevelSpriteBaker.targetTex = new RenderTexture(num * this.aa, num * this.aa, 24, RenderTextureFormat.ARGB32);
				LevelSpriteBaker.targetTex.useMipMap = true;
				LevelSpriteBaker.targetTex.autoGenerateMips = false;
				LevelSpriteBaker.targetTex.Create();
			}
			yield return new GenInfo("LevelSpriteBaker", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000A7760 File Offset: 0x000A5B60
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			List<LevelState> levels = campaign.campaignSave.levelStates;
			int area = levels.Sum((LevelState l) => (int)(l.width * l.width));
			int height = 128;
			Rect rect = default(Rect);
			foreach (LevelState levelState in from x in levels
			orderby (float)x.width + (float)x.index * 0.0001f
			select x)
			{
				int num = (int)(levelState.width + 1);
				if (rect.height + (float)num > (float)height)
				{
					rect = new Rect(rect.max.x, 0f, 0f, 0f);
				}
				Rect spriteRect = new Rect(rect.min.x, rect.max.y, (float)num, (float)num);
				levelState.spriteRect = spriteRect;
				rect.max = Vector2.Max(rect.max, spriteRect.max + Vector2.one);
			}
			int width = Mathf.NextPowerOfTwo((int)rect.max.x);
			width *= this.pixelsPerWidth;
			height *= this.pixelsPerWidth;
			campaign.campaignSave.levelAtlas = new SavedTexture(width, height);
			this.atlas = campaign.campaignSave.levelAtlas.tex;
			Color32[] pixels = new Color32[width * height];
			Color32 c = new UnityEngine.Color(1f, 0f, 0f, 0f);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = c;
			}
			this.atlas.SetPixels32(pixels);
			this.atlas.Apply();
			yield return null;
			yield break;
		}

		// Token: 0x04001D8A RID: 7562
		[SerializeField]
		public Texture2D atlas;

		// Token: 0x04001D8B RID: 7563
		public int pixelsPerWidth = 3;

		// Token: 0x04001D8C RID: 7564
		private const int mip = 3;

		// Token: 0x04001D8D RID: 7565
		[SerializeField]
		private UnityEngine.Color clearColor;

		// Token: 0x04001D8E RID: 7566
		private static CommandBuffer buffer;

		// Token: 0x04001D8F RID: 7567
		private static Material grassMaterial;

		// Token: 0x04001D90 RID: 7568
		private static Material cliffMaterial;

		// Token: 0x04001D91 RID: 7569
		private static RenderTexture targetTex;

		// Token: 0x04001D92 RID: 7570
		private static RenderTexture smallerTex;

		// Token: 0x04001D93 RID: 7571
		[SerializeField]
		private Material shader;
	}
}
