using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020006DE RID: 1758
	public class CampaignTriangles : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x000AC924 File Offset: 0x000AAD24
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("CampaignTriangles", GenInfo.Mode.interruptable);
			CampaignLoops campaignLoops = campaign.GetComponentInChildren<CampaignLoops>();
			List<LevelState> levels = campaign.campaignSave.levelStates;
			Loop longestLoop = campaignLoops.loops[0];
			this.verts = new List<CampaignTriangles.Vertex>(campaign.campaignSave.levelStates.Count + longestLoop.Count + 4);
			for (int i = 0; i < campaign.campaignSave.levelStates.Count; i++)
			{
				this.verts.Add(new CampaignTriangles.Vertex(campaign.campaignSave.levelStates[i]));
			}
			for (int j = 1; j < campaignLoops.loops.Count; j++)
			{
				Loop loop = campaignLoops.loops[j];
				if (loop.Count > 3)
				{
					int num = 0;
					float num2 = -1f;
					for (int k = 0; k < loop.Count; k++)
					{
						Vector2 pos = loop[k % loop.Count].pos;
						Vector2 pos2 = loop[(k + 1) % loop.Count].pos;
						Vector2 pos3 = loop[(k + 2) % loop.Count].pos;
						Vector2 normalized = (pos - pos2).normalized;
						Vector2 normalized2 = (pos2 - pos3).normalized;
						float num3 = Vector2.Dot(-normalized, ExtraMath.Rotate2D90(normalized + normalized2).normalized);
						if (num3 > num2)
						{
							num2 = num3;
							num = (k + 1) % loop.Count;
						}
					}
					for (int l = 0; l < loop.Count - 2; l++)
					{
						this.tris.Add((int)loop[num].index);
						this.tris.Add((int)loop[(num + l + 2) % loop.Count].index);
						this.tris.Add((int)loop[(num + l + 1) % loop.Count].index);
					}
				}
				else
				{
					for (int m = 0; m < loop.Count - 2; m++)
					{
						this.tris.Add((int)loop[0].index);
						this.tris.Add((int)loop[m + 2].index);
						this.tris.Add((int)loop[m + 1].index);
					}
				}
			}
			Vector2 extents = new Vector2(campaign.rect.width / 2f + 30f, campaign.rect.height / 2f + 80f);
			int midIndex = longestLoop.IndexOf(levels.Last<LevelState>());
			int endIndex = campaign.endLevel.index;
			this.innerLoopStart = this.verts.Count;
			float loopOffset = 6f;
			for (int n = 0; n <= midIndex; n++)
			{
				LevelState levelState = longestLoop[Mathf.Max(0, n - 1)];
				LevelState levelState2 = longestLoop[n];
				LevelState levelState3 = longestLoop[Mathf.Min(midIndex, n + 1)];
				Vector2 vector = levelState2.pos;
				vector += (ExtraMath.Rotate2D90(levelState.pos - levelState2.pos) + ExtraMath.Rotate2D90(levelState2.pos - levelState3.pos)).normalized * loopOffset * 0.4f;
				vector += Vector2.up * loopOffset * 0.6f;
				vector.x = Mathf.Clamp(vector.x, campaign.startLevel.pos.x, campaign.endLevel.pos.x);
				this.verts.Add(new CampaignTriangles.Vertex(levelState2, vector));
			}
			this.innerLoopMid = this.verts.Count;
			yield return info;
			for (int num4 = midIndex; num4 <= longestLoop.Count; num4++)
			{
				LevelState levelState4 = longestLoop[Mathf.Max(midIndex, num4 - 1) % longestLoop.Count];
				LevelState levelState5 = longestLoop[num4 % longestLoop.Count];
				LevelState levelState6 = longestLoop[Mathf.Min(longestLoop.Count, num4 + 1) % longestLoop.Count];
				Vector2 vector2 = levelState5.pos;
				vector2 += (ExtraMath.Rotate2D90(levelState4.pos - levelState5.pos) + ExtraMath.Rotate2D90(levelState5.pos - levelState6.pos)).normalized * loopOffset * 0.4f;
				vector2 += Vector2.down * loopOffset * 0.6f;
				vector2.x = Mathf.Clamp(vector2.x, campaign.startLevel.pos.x, campaign.endLevel.pos.x);
				this.verts.Add(new CampaignTriangles.Vertex(levelState5, vector2));
			}
			this.innerLoopCount = this.verts.Count - this.innerLoopStart;
			yield return info;
			this.startIndex = this.verts.Count;
			float outerMargin = 80f;
			this.verts.Add(new CampaignTriangles.Vertex(levels[0], new Vector2(-extents.x - outerMargin, extents.y), -4));
			this.verts.Add(new CampaignTriangles.Vertex(levels[0], new Vector2(-extents.x, extents.y), -1));
			for (int num5 = this.innerLoopStart; num5 < this.innerLoopMid; num5++)
			{
				this.verts.Add(new CampaignTriangles.Vertex(levels[this.verts[num5].levelIndex], this.verts[num5].pos.SetY(extents.y)));
			}
			yield return info;
			this.verts.Add(new CampaignTriangles.Vertex(levels.Last<LevelState>(), new Vector2(extents.x, extents.y), 1));
			this.verts.Add(new CampaignTriangles.Vertex(levels.Last<LevelState>(), new Vector2(extents.x + outerMargin, extents.y), 4));
			this.outerLoopMid = this.verts.Count;
			this.verts.Add(new CampaignTriangles.Vertex(levels.Last<LevelState>(), new Vector2(extents.x + outerMargin, 0f), 4));
			this.verts.Add(new CampaignTriangles.Vertex(levels.Last<LevelState>(), new Vector2(extents.x + outerMargin, -extents.y), 4));
			this.verts.Add(new CampaignTriangles.Vertex(levels.Last<LevelState>(), new Vector2(extents.x, -extents.y), 1));
			for (int num6 = this.innerLoopMid; num6 < this.innerLoopStart + this.innerLoopCount; num6++)
			{
				this.verts.Add(new CampaignTriangles.Vertex(levels[this.verts[num6].levelIndex], this.verts[num6].pos.SetY(-extents.y)));
			}
			this.verts.Add(new CampaignTriangles.Vertex(levels[0], new Vector2(-extents.x, -extents.y), -1));
			this.verts.Add(new CampaignTriangles.Vertex(levels[0], new Vector2(-extents.x - outerMargin, -extents.y), -4));
			this.verts.Add(new CampaignTriangles.Vertex(levels[0], new Vector2(-extents.x - outerMargin, 0f), -4));
			this.outerLoopStart = this.innerLoopStart + this.innerLoopCount;
			this.outerLoopCount = this.verts.Count - this.outerLoopStart;
			yield return info;
			for (int num7 = 0; num7 < midIndex; num7++)
			{
				int num8 = this.innerLoopStart + num7;
				int num9 = this.innerLoopStart + num7 + 1;
				int v = this.outerLoopStart + num7 + 2;
				int v2 = this.outerLoopStart + num7 + 3;
				this.AddQuad((int)longestLoop[num7].index, (int)longestLoop[num7 + 1].index, num8, num9);
				this.AddQuad(num8, num9, v, v2);
			}
			yield return info;
			this.AddTriangle(this.innerLoopMid - 1, this.outerLoopMid - 3, this.outerLoopMid - 2);
			this.AddTriangle(endIndex, this.innerLoopMid - 1, this.outerLoopMid - 2);
			this.AddTriangle(endIndex, this.outerLoopMid - 2, this.outerLoopMid + 2);
			this.AddTriangle(endIndex, this.outerLoopMid + 2, this.innerLoopMid);
			this.AddTriangle(this.innerLoopMid, this.outerLoopMid + 2, this.outerLoopMid + 3);
			this.AddTriangle(this.outerLoopMid - 2, this.outerLoopMid - 1, this.outerLoopMid);
			this.AddTriangle(this.outerLoopMid - 2, this.outerLoopMid, this.outerLoopMid + 2);
			this.AddTriangle(this.outerLoopMid + 2, this.outerLoopMid, this.outerLoopMid + 1);
			yield return info;
			for (int num10 = 0; num10 < longestLoop.Count - midIndex; num10++)
			{
				int num11 = this.innerLoopMid + num10;
				int num12 = this.innerLoopMid + num10 + 1;
				int v3 = this.outerLoopMid + num10 + 3;
				int v4 = this.outerLoopMid + num10 + 4;
				this.AddQuad((int)longestLoop[midIndex + num10].index, (int)longestLoop[(midIndex + num10 + 1) % longestLoop.Count].index, num11, num12);
				this.AddQuad(num11, num12, v3, v4);
			}
			yield return info;
			this.AddTriangle(this.outerLoopStart - 1, this.verts.Count - 4, this.verts.Count - 3);
			this.AddTriangle(0, this.outerLoopStart - 1, this.verts.Count - 3);
			this.AddTriangle(0, this.verts.Count - 3, this.outerLoopStart + 1);
			this.AddTriangle(0, this.outerLoopStart + 1, this.innerLoopStart);
			this.AddTriangle(this.innerLoopStart, this.outerLoopStart + 1, this.outerLoopStart + 2);
			this.AddTriangle(this.verts.Count - 3, this.verts.Count - 2, this.verts.Count - 1);
			this.AddTriangle(this.verts.Count - 3, this.verts.Count - 1, this.outerLoopStart + 1);
			this.AddTriangle(this.verts.Count - 1, this.outerLoopStart, this.outerLoopStart + 1);
			yield return info;
			this.startIndex = this.outerLoopStart;
			yield break;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000AC946 File Offset: 0x000AAD46
		private void AddTriangle(int v0, int v1, int v2)
		{
			this.tris.Add(v0);
			this.tris.Add(v1);
			this.tris.Add(v2);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000AC96C File Offset: 0x000AAD6C
		private void AddQuad(int v00, int v01, int v10, int v11)
		{
			if ((this.verts[v00].pos - this.verts[v11].pos).sqrMagnitude < (this.verts[v01].pos - this.verts[v10].pos).sqrMagnitude)
			{
				this.tris.Add(v00);
				this.tris.Add(v11);
				this.tris.Add(v01);
				this.tris.Add(v00);
				this.tris.Add(v10);
				this.tris.Add(v11);
			}
			else
			{
				this.tris.Add(v00);
				this.tris.Add(v10);
				this.tris.Add(v01);
				this.tris.Add(v10);
				this.tris.Add(v11);
				this.tris.Add(v01);
			}
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000ACA78 File Offset: 0x000AAE78
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = this.gizmoColor;
			for (int i = 0; i < this.tris.Count; i += 3)
			{
				Vector2 pos = this.verts[this.tris[i]].pos;
				Vector2 pos2 = this.verts[this.tris[i + 1]].pos;
				Vector2 pos3 = this.verts[this.tris[i + 2]].pos;
				Gizmos.DrawLine(pos, pos2);
				Gizmos.DrawLine(pos2, pos3);
				Gizmos.DrawLine(pos3, pos);
			}
		}

		// Token: 0x04001DFF RID: 7679
		[NonSerialized]
		public List<CampaignTriangles.Vertex> verts;

		// Token: 0x04001E00 RID: 7680
		[NonSerialized]
		public List<int> tris = new List<int>();

		// Token: 0x04001E01 RID: 7681
		public int startIndex;

		// Token: 0x04001E02 RID: 7682
		private int innerLoopStart;

		// Token: 0x04001E03 RID: 7683
		private int innerLoopMid;

		// Token: 0x04001E04 RID: 7684
		private int innerLoopCount;

		// Token: 0x04001E05 RID: 7685
		private int outerLoopStart;

		// Token: 0x04001E06 RID: 7686
		private int outerLoopMid;

		// Token: 0x04001E07 RID: 7687
		private int outerLoopCount;

		// Token: 0x04001E08 RID: 7688
		[SerializeField]
		private Color gizmoColor = Color.white;

		// Token: 0x020006DF RID: 1759
		public class Vertex
		{
			// Token: 0x06002D76 RID: 11638 RVA: 0x000ACB4A File Offset: 0x000AAF4A
			public Vertex(LevelState level, Vector2 pos)
			{
				this.levelIndex = (int)level.index;
				this.frontierDepth = (int)level.frontierDepth;
				this.pos = pos;
			}

			// Token: 0x06002D77 RID: 11639 RVA: 0x000ACB71 File Offset: 0x000AAF71
			public Vertex(LevelState level0) : this(level0, level0.pos)
			{
				this.onLevel = true;
			}

			// Token: 0x06002D78 RID: 11640 RVA: 0x000ACB87 File Offset: 0x000AAF87
			public Vertex(LevelState level0, Vector2 pos, int frontierOffset) : this(level0, pos)
			{
				this.frontierDepth = (int)level0.frontierDepth + frontierOffset;
			}

			// Token: 0x04001E09 RID: 7689
			public readonly bool onLevel;

			// Token: 0x04001E0A RID: 7690
			public readonly int levelIndex;

			// Token: 0x04001E0B RID: 7691
			public readonly int frontierDepth;

			// Token: 0x04001E0C RID: 7692
			public readonly Vector2 pos;
		}
	}
}
