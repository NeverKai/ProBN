using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense
{
	// Token: 0x020006CF RID: 1743
	public class CampaignBackground : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002D33 RID: 11571 RVA: 0x000A8A30 File Offset: 0x000A6E30
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("CampaignBackground", GenInfo.Mode.interruptable);
			this.lineFrontier = campaign.GetComponentInChildren<LineFrontier>(true);
			this.campaignTriangles = campaign.GetComponentInChildren<CampaignTriangles>(true);
			int sizeX = this.lineFrontier.sizeX;
			int sizeY = this.lineFrontier.sizeY;
			this.verts0 = new List<UIVertex>(this.campaignTriangles.verts.Count + sizeY);
			this.tris0 = new List<int>();
			this.tris1 = new List<int>();
			foreach (UIVertexListGraphic uivertexListGraphic in this.graphics0)
			{
				uivertexListGraphic.verts = this.verts0;
				uivertexListGraphic.tris = this.tris0;
			}
			foreach (UIVertexListGraphic uivertexListGraphic2 in this.graphics1)
			{
				uivertexListGraphic2.verts = this.verts0;
				uivertexListGraphic2.tris = this.tris1;
			}
			UIVertex vert = new UIVertex
			{
				color = Color.white
			};
			yield return info;
			for (int k = 0; k < this.campaignTriangles.verts.Count; k++)
			{
				CampaignTriangles.Vertex vertex = this.campaignTriangles.verts[k];
				vert.position = vertex.pos;
				vert.uv0 = vertex.pos;
				this.verts0.Add(vert);
			}
			for (int l = 0; l < sizeY; l++)
			{
				this.verts0.Add(vert);
			}
			this.lineFrontier.darkenAnim.Subscribe(delegate(float x)
			{
				this.colorGraphic.color = Color.Lerp(this.color0, this.color1, x);
				this.colorGraphic.SetMaterialDirty();
			});
			yield return info;
			List<CampaignTriangles.Vertex> triVerts = this.campaignTriangles.verts;
			List<int> triTris = this.campaignTriangles.tris;
			this.lineFrontier.mainLineAnim.Subscribe(delegate(float d)
			{
				LineFrontier.Point[,] points = this.lineFrontier.points;
				float num = d + 4f;
				int num2 = Mathf.FloorToInt(num);
				int num3 = Mathf.CeilToInt(num);
				for (int n = 0; n < sizeY; n++)
				{
					LineFrontier.Point point = points[num2, n];
					LineFrontier.Point point2 = points[num3, n];
					int index = triVerts.Count + n;
					UIVertex value = this.verts0[index];
					value.position = Vector2.Lerp(point.pos, point2.pos, num - (float)num2);
					this.verts0[index] = value;
				}
				if (Mathf.RoundToInt(d) != this.lastDepth)
				{
					this.enabled = true;
				}
				this.SetAllDirty();
			});
			for (int m = 0; m < campaign.levels.Count; m++)
			{
				AnimatedState cloudState = campaign.levels[m].levelVisuals.cloudState;
				cloudState.OnChange = (Action<bool>)Delegate.Combine(cloudState.OnChange, new Action<bool>(this.OnCloudChange));
			}
			yield return info;
			yield break;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000A8A54 File Offset: 0x000A6E54
		private void SetAllDirty()
		{
			foreach (UIVertexListGraphic uivertexListGraphic in this.graphics0)
			{
				uivertexListGraphic.SetVerticesDirty();
			}
			foreach (UIVertexListGraphic uivertexListGraphic2 in this.graphics1)
			{
				uivertexListGraphic2.SetVerticesDirty();
			}
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000A8AB3 File Offset: 0x000A6EB3
		private void OnCloudChange(bool x)
		{
			base.enabled = true;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000A8ABC File Offset: 0x000A6EBC
		private void UpdateTriangles()
		{
			List<CampaignTriangles.Vertex> verts = this.campaignTriangles.verts;
			List<int> tris = this.campaignTriangles.tris;
			LineFrontier.Point[,] points = this.lineFrontier.points;
			List<LevelNode> levels = base.campaign.levels;
			float current = this.lineFrontier.mainLineAnim.current;
			int num = Mathf.RoundToInt(current);
			this.lastDepth = num;
			this.tris0.Clear();
			this.tris1.Clear();
			for (int i = 0; i < tris.Count; i += 3)
			{
				int index = tris[i];
				int index2 = tris[i + 1];
				int index3 = tris[i + 2];
				CampaignTriangles.Vertex vertex = verts[index];
				CampaignTriangles.Vertex vertex2 = verts[index2];
				CampaignTriangles.Vertex vertex3 = verts[index3];
				LevelNode levelNode = levels[vertex.levelIndex];
				LevelNode levelNode2 = levels[vertex2.levelIndex];
				LevelNode levelNode3 = levels[vertex3.levelIndex];
				if (levelNode.levelVisuals.cloudState.active || levelNode2.levelVisuals.cloudState.active || levelNode3.levelVisuals.cloudState.active)
				{
					if (Mathf.Max(vertex.frontierDepth, Mathf.Max(vertex2.frontierDepth, vertex3.frontierDepth)) <= num)
					{
						this.tris0.Add(tris[i]);
						this.tris0.Add(tris[i + 1]);
						this.tris0.Add(tris[i + 2]);
					}
					else if (Mathf.Min(vertex.frontierDepth, Mathf.Min(vertex2.frontierDepth, vertex3.frontierDepth)) > num)
					{
						this.tris1.Add(tris[i]);
						this.tris1.Add(tris[i + 1]);
						this.tris1.Add(tris[i + 2]);
					}
				}
			}
			int num2 = num + 4;
			for (int j = 0; j < this.lineFrontier.sizeY - 1; j++)
			{
				LineFrontier.Point point = points[num2, j];
				LineFrontier.Point point2 = points[num2, j + 1];
				if (point.vert0 != point2.vert0)
				{
					this.tris0.Add(point2.vert0);
					this.tris0.Add(point.vert0);
					this.tris0.Add(verts.Count + j);
				}
				this.tris0.Add(point2.vert0);
				this.tris0.Add(verts.Count + j);
				this.tris0.Add(verts.Count + j + 1);
				if (point.vert1 != point2.vert1)
				{
					this.tris1.Add(point.vert1);
					this.tris1.Add(point2.vert1);
					this.tris1.Add(verts.Count + j);
				}
				this.tris1.Add(point2.vert1);
				this.tris1.Add(verts.Count + j + 1);
				this.tris1.Add(verts.Count + j);
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000A8E17 File Offset: 0x000A7217
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000A8E20 File Offset: 0x000A7220
		private void LateUpdate()
		{
			this.UpdateTriangles();
			this.SetAllDirty();
			base.enabled = false;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000A8E38 File Offset: 0x000A7238
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			if (this.tris0 == null)
			{
				return;
			}
			Gizmos.color = this.gizmoColor0;
			for (int i = 0; i < this.tris0.Count; i += 3)
			{
				for (int j = 0; j < 3; j++)
				{
					Gizmos.DrawLine(this.verts0[this.tris0[i + j]].position, this.verts0[this.tris0[i + (j + 1) % 3]].position);
				}
			}
			Gizmos.color = this.gizmoColor1;
			for (int k = 0; k < this.tris1.Count; k += 3)
			{
				for (int l = 0; l < 3; l++)
				{
					Gizmos.DrawLine(this.verts0[this.tris1[k + l]].position, this.verts0[this.tris1[k + (l + 1) % 3]].position);
				}
			}
		}

		// Token: 0x04001DAF RID: 7599
		[SerializeField]
		private UIVertexListGraphic[] graphics0;

		// Token: 0x04001DB0 RID: 7600
		[SerializeField]
		private UIVertexListGraphic[] graphics1;

		// Token: 0x04001DB1 RID: 7601
		[SerializeField]
		private UIVertexListGraphic colorGraphic;

		// Token: 0x04001DB2 RID: 7602
		[SerializeField]
		private Color color0 = Color.black;

		// Token: 0x04001DB3 RID: 7603
		[SerializeField]
		private Color color1 = Color.red;

		// Token: 0x04001DB4 RID: 7604
		private const int spacing = 6;

		// Token: 0x04001DB5 RID: 7605
		private const int halfSpacing = 3;

		// Token: 0x04001DB6 RID: 7606
		private List<UIVertex> verts0;

		// Token: 0x04001DB7 RID: 7607
		private List<int> tris0;

		// Token: 0x04001DB8 RID: 7608
		private List<int> tris1;

		// Token: 0x04001DB9 RID: 7609
		private int lastDepth = -99;

		// Token: 0x04001DBA RID: 7610
		private CampaignTriangles campaignTriangles;

		// Token: 0x04001DBB RID: 7611
		private LineFrontier lineFrontier;

		// Token: 0x04001DBC RID: 7612
		[SerializeField]
		private Color gizmoColor0 = Color.white;

		// Token: 0x04001DBD RID: 7613
		[SerializeField]
		private Color gizmoColor1 = Color.white;
	}
}
