using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020006D4 RID: 1748
	public class CampaignFog : CampaignComponent, Campaign.ICampaignGenerator, Campaign.ICampaignDestroy
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x000A99B4 File Offset: 0x000A7DB4
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("CampaignFog", GenInfo.Mode.interruptable);
			this.campaignTriangles = campaign.GetComponentInChildren<CampaignTriangles>(true);
			CampaignFog.verts.Clear();
			CampaignFog.tris.Clear();
			int vertCount = this.campaignTriangles.verts.Count + campaign.levels.Count * 7;
			this.indexes = new List<Vector2Int>(vertCount);
			this.colors = new List<Color32>(vertCount);
			CampaignFog.verts.Capacity = Mathf.Min(CampaignFog.verts.Capacity, this.indexes.Count);
			CampaignFog.tris.Capacity = Mathf.Min(CampaignFog.tris.Capacity, this.campaignTriangles.tris.Count + campaign.levels.Count * 6);
			for (int i = 0; i < this.campaignTriangles.verts.Count; i++)
			{
				this.colors.Add(Color.black);
				CampaignFog.verts.Add(this.campaignTriangles.verts[i].pos);
				this.indexes.Add(new Vector2Int(this.campaignTriangles.verts[i].levelIndex, this.campaignTriangles.verts[i].levelIndex));
			}
			for (int j = 0; j < this.campaignTriangles.tris.Count; j++)
			{
				CampaignFog.tris.Add(this.campaignTriangles.tris[j]);
			}
			for (int k = 0; k < campaign.levels.Count; k++)
			{
				LevelNode levelNode = campaign.levels[k];
				int count = CampaignFog.verts.Count;
				this.colors.Add(new Color(0f, 1f, 0f, 1f));
				CampaignFog.verts.Add(levelNode.pos);
				this.indexes.Add(new Vector2Int(k, k));
				Vector2 vector = new Vector2(0f, levelNode.outerRadius + 3f);
				for (int l = 0; l < 6; l++)
				{
					CampaignFog.tris.Add(count);
					CampaignFog.tris.Add(count + 1 + (l + 1) % 6);
					CampaignFog.tris.Add(count + 1 + l);
					CampaignFog.verts.Add(levelNode.pos + vector);
					this.indexes.Add(new Vector2Int(k, k));
					vector = ExtraMath.Rotate2D(vector, 60f);
					this.colors.Add(new Color(0f, 1f, 0f, 0f));
				}
			}
			this.mesh = campaign.meshPool.GetMesh();
			this.mesh.name = "CloudMesh";
			this.mesh.SetVertices(CampaignFog.verts);
			this.mesh.SetTriangles(CampaignFog.tris, 0);
			this.graphic.verts.AddRange(from x in this.campaignTriangles.verts
			select new UIVertex
			{
				position = x.pos,
				color = Color.white
			});
			this.graphic.tris.AddRange(this.campaignTriangles.tris);
			int height = 32;
			int width = Mathf.ClosestPowerOfTwo((int)((float)height * (campaign.rect.width / campaign.rect.height)));
			float ratio = (float)(width / height);
			this.renderTex = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			this.renderTex.name = "RenderTex";
			this.extents = Vector4.zero;
			this.extents.y = campaign.rect.yMax + 10f;
			this.extents.x = this.extents.y * ratio;
			this.extents.z = 1f / this.extents.x;
			this.extents.w = 1f / this.extents.y;
			this.totalRect = new Rect(-this.extents.x, -this.extents.y, this.extents.x * 2f, this.extents.y * 2f);
			Shader.SetGlobalVector(CampaignFog.frontierParamsId, this.extents);
			Shader.SetGlobalTexture(CampaignFog.frontierTexId, this.renderTex);
			yield return info;
			if (CampaignFog.material == null)
			{
				CampaignFog.buffer = new CommandBuffer();
				CampaignFog.material = new Material(this.levelFrontierShader);
				CampaignFog.material.enableInstancing = true;
			}
			else
			{
				CampaignFog.buffer.Clear();
			}
			float pixelSize = this.extents.y * 2f / (float)this.renderTex.height;
			yield return info;
			this.Update();
			this.UpdateTriangles();
			yield return info;
			foreach (LevelNode levelNode2 in campaign.levels)
			{
				this.SubscribeToState(levelNode2.levelVisuals.cloudState.anim.state);
				this.SubscribeToState(levelNode2.behindFrontier.anim.state);
				this.SubscribeToState(levelNode2.played.anim.state);
			}
			yield return info;
			yield break;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000A99D6 File Offset: 0x000A7DD6
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000A99DF File Offset: 0x000A7DDF
		private void SubscribeToState(AgentState state)
		{
			if (state.active)
			{
				this.stateCount++;
			}
			state.OnChange = (Action<bool>)Delegate.Combine(state.OnChange, new Action<bool>(this.StateChange));
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000A9A1C File Offset: 0x000A7E1C
		private void StateChange(bool change)
		{
			if (change)
			{
				this.stateCount++;
				if (!base.enabled)
				{
					this.UpdateTriangles();
					base.enabled = true;
				}
			}
			else
			{
				this.stateCount--;
				if (this.stateCount <= 0)
				{
					base.enabled = false;
					this.Update();
					this.UpdateTriangles();
				}
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000A9A88 File Offset: 0x000A7E88
		private void Update()
		{
			GL.LoadPixelMatrix(-this.extents.x, this.extents.x, -this.extents.y, this.extents.y);
			RenderTexture.active = this.renderTex;
			Graphics.SetRenderTarget(this.renderTex);
			GL.Clear(true, true, Color.clear);
			CampaignFog.buffer.Clear();
			float num = 0f;
			for (int i = 0; i < this.mesh.vertexCount; i++)
			{
				LevelNode levelNode = base.campaign.levels[this.indexes[i].x];
				LevelNode levelNode2 = base.campaign.levels[this.indexes[i].y];
				float num2 = 1f - (levelNode.levelVisuals.cloudState.value + levelNode2.levelVisuals.cloudState.value) / 2f;
				num += num2;
				Color c = this.colors[i];
				c.r = num2;
				this.colors[i] = c;
			}
			this.mesh.SetColors(this.colors);
			CampaignFog.buffer.DrawMesh(this.mesh, Matrix4x4.Translate(Vector3.back), CampaignFog.material);
			Shader.SetGlobalFloat(CampaignFog.cloudOffsetId, num);
			Graphics.ExecuteCommandBuffer(CampaignFog.buffer);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000A9C10 File Offset: 0x000A8010
		private void UpdateTriangles()
		{
			List<int> list = this.campaignTriangles.tris;
			this.graphic.tris.Clear();
			for (int i = 0; i < list.Count; i += 3)
			{
				if (this.colors[list[i]].r > 0 || this.colors[list[i + 1]].r > 0 || this.colors[list[i + 2]].r > 0)
				{
					this.graphic.tris.Add(list[i]);
					this.graphic.tris.Add(list[i + 1]);
					this.graphic.tris.Add(list[i + 2]);
				}
			}
			this.graphic.SetAllDirty();
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000A9D09 File Offset: 0x000A8109
		void Campaign.ICampaignDestroy.OnCampaignDestroy(Campaign campaign)
		{
			campaign.meshPool.ReturnMesh(ref this.mesh);
			UnityEngine.Object.Destroy(this.renderTex);
		}

		// Token: 0x04001DDE RID: 7646
		private static ShaderId frontierTexId = "_FrontierTex";

		// Token: 0x04001DDF RID: 7647
		private static ShaderId frontierParamsId = "_FrontierParams";

		// Token: 0x04001DE0 RID: 7648
		private static ShaderId cloudOffsetId = "_CloudOffset";

		// Token: 0x04001DE1 RID: 7649
		[SerializeField]
		private Shader levelFrontierShader;

		// Token: 0x04001DE2 RID: 7650
		[SerializeField]
		private UIVertexListGraphic graphic;

		// Token: 0x04001DE3 RID: 7651
		[Space]
		public RenderTexture renderTex;

		// Token: 0x04001DE4 RID: 7652
		private Vector4 extents;

		// Token: 0x04001DE5 RID: 7653
		private Rect totalRect;

		// Token: 0x04001DE6 RID: 7654
		private static CommandBuffer buffer;

		// Token: 0x04001DE7 RID: 7655
		private static Material material;

		// Token: 0x04001DE8 RID: 7656
		private CampaignTriangles campaignTriangles;

		// Token: 0x04001DE9 RID: 7657
		public Mesh mesh;

		// Token: 0x04001DEA RID: 7658
		private static List<Vector3> verts = new List<Vector3>();

		// Token: 0x04001DEB RID: 7659
		private static List<int> tris = new List<int>();

		// Token: 0x04001DEC RID: 7660
		private List<Vector2Int> indexes;

		// Token: 0x04001DED RID: 7661
		private List<Color32> colors;

		// Token: 0x04001DEE RID: 7662
		private const int segments = 6;

		// Token: 0x04001DEF RID: 7663
		public int stateCount;
	}
}
