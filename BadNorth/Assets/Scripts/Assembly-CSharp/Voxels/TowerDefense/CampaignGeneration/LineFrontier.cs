using System;
using System.Collections.Generic;
using System.Linq;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000713 RID: 1811
	public class LineFrontier : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002F01 RID: 12033 RVA: 0x000B98D0 File Offset: 0x000B7CD0
		private string LoopToString(List<int> loop)
		{
			return "(" + string.Join(", ", (from x in loop
			select x.ToString()).ToArray<string>()) + ")";
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000B991E File Offset: 0x000B7D1E
		private int HashFromEdge(int i0, int i1)
		{
			return i0 * 103 + i1 * 193;
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000B992C File Offset: 0x000B7D2C
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("LineFrontier", GenInfo.Mode.interruptable);
			List<LevelState> levels = campaign.campaignSave.levelStates;
			CampaignTriangles campaignTriangles = campaign.GetComponentInChildren<CampaignTriangles>(true);
			List<CampaignTriangles.Vertex> verts = campaignTriangles.verts;
			List<int> tris = campaignTriangles.tris;
			this.sizeX = campaign.endLevel.frontierDepth + 8;
			this.sizeY = 80;
			this.anchorLists = new List<List<LineFrontier.Anchor>>(this.sizeX);
			Dictionary<Vector2, LineFrontier.Anchor> anchorDict = new Dictionary<Vector2, LineFrontier.Anchor>(campaign.levels.Count);
			int d = 0;
			int startIndex = campaignTriangles.startIndex;
			while (d < this.sizeX)
			{
				yield return info;
				float depth = (float)d + 0.5f - 4f;
				List<LineFrontier.Anchor> anchors = new List<LineFrontier.Anchor>();
				this.anchorLists.Add(anchors);
				while ((float)verts[startIndex + 1].frontierDepth < depth)
				{
					startIndex++;
				}
				Color debugColor = Color.HSVToRGB((float)(d + 4) / 4f % 1f, 1f, 1f);
				CampaignTriangles.Vertex vertex = verts[startIndex];
				CampaignTriangles.Vertex vertex2 = verts[startIndex + 1];
				Vector2 key = vertex.pos + vertex2.pos;
				LineFrontier.Anchor anchor;
				if (!anchorDict.TryGetValue(key, out anchor))
				{
					anchor = new LineFrontier.Anchor(vertex, vertex2, startIndex, startIndex + 1);
					anchorDict.Add(key, anchor);
				}
				anchor.count++;
				anchors.Add(anchor);
				Vector2 pos0 = vertex.pos + vertex2.pos * 3.1415927f;
				int t = 0;
				int max = tris.Count;
				while (t < max)
				{
					int ti = t % tris.Count;
					for (int j = 0; j < 3; j++)
					{
						CampaignTriangles.Vertex vertex3 = verts[tris[ti + (j + 1) % 3]];
						CampaignTriangles.Vertex vertex4 = verts[tris[ti + j]];
						Vector2 b = vertex3.pos * 3.1415927f + vertex4.pos;
						if ((pos0 - b).sqrMagnitude < 1f)
						{
							for (int k = 1; k < 3; k++)
							{
								int num = tris[ti + (j + k + 1) % 3];
								int num2 = tris[ti + (j + k) % 3];
								CampaignTriangles.Vertex vertex5 = verts[num];
								CampaignTriangles.Vertex vertex6 = verts[num2];
								if (((float)vertex5.frontierDepth < depth && (float)vertex6.frontierDepth > depth) || ((float)vertex6.frontierDepth < depth && (float)vertex5.frontierDepth > depth))
								{
									pos0 = vertex5.pos + vertex6.pos * 3.1415927f;
									Vector2 key2 = vertex5.pos + vertex6.pos;
									LineFrontier.Anchor anchor2;
									if (!anchorDict.TryGetValue(key2, out anchor2))
									{
										anchor2 = new LineFrontier.Anchor(vertex5, vertex6, num, num2);
										anchorDict.Add(key2, anchor2);
									}
									anchor2.count++;
									anchors.Add(anchor2);
									break;
								}
							}
							max = t + tris.Count;
							break;
						}
					}
					yield return info;
					t += 3;
				}
				for (int l = 0; l < anchors.Count - 1; l++)
				{
					Debug.DrawLine(anchors[l].pos, anchors[l + 1].pos, debugColor, 2f);
				}
				d++;
			}
			foreach (List<LineFrontier.Anchor> list in this.anchorLists)
			{
				float num3 = 0f;
				for (int m = 0; m < list.Count - 1; m++)
				{
					num3 += (list[m].pos - list[m + 1].pos).magnitude / 2f;
					list[m + 1].t += num3;
				}
				num3 /= 2f;
				foreach (LineFrontier.Anchor anchor3 in list)
				{
					anchor3.t -= num3;
				}
			}
			foreach (LineFrontier.Anchor anchor4 in anchorDict.Values)
			{
				anchor4.t /= (float)anchor4.count;
			}
			for (int i = 0; i < 50; i++)
			{
				foreach (List<LineFrontier.Anchor> list2 in this.anchorLists)
				{
					for (int n = 0; n < list2.Count - 1; n++)
					{
						LineFrontier.Anchor anchor5 = list2[n];
						LineFrontier.Anchor anchor6 = list2[n + 1];
						float num4 = (anchor5.pos - anchor6.pos).magnitude / 2f - (anchor6.t - anchor5.t);
						float num5 = num4 * num4;
						num4 /= 2f;
						num4 *= num5;
						anchor5.dt -= num4;
						anchor6.dt += num4;
						anchor5.denom += num5;
						anchor6.denom += num5;
					}
				}
				float maxDt = 0f;
				foreach (LineFrontier.Anchor anchor7 in anchorDict.Values)
				{
					if (anchor7.denom > 0f)
					{
						float num6 = anchor7.dt / anchor7.denom;
						anchor7.t += num6;
						anchor7.dt = 0f;
						anchor7.denom = 0f;
						maxDt = Math.Max(Mathf.Abs(num6), maxDt);
					}
				}
				if (maxDt < 0.2f)
				{
					break;
				}
				yield return info;
			}
			this.points = new LineFrontier.Point[this.sizeX, this.sizeY];
			List<Vector2> smoothList = ListPool<Vector2>.GetList(this.sizeY);
			for (int num7 = 0; num7 < this.sizeY; num7++)
			{
				smoothList.Add(Vector2.zero);
			}
			for (int num8 = 0; num8 < this.anchorLists.Count; num8++)
			{
				List<LineFrontier.Anchor> list3 = this.anchorLists[num8];
				float num9 = (float)(num8 - 4);
				for (int num10 = 0; num10 < list3.Count; num10++)
				{
					int index = Mathf.Max(0, num10 - 1);
					int index2 = num10;
					int index3 = Mathf.Min(list3.Count - 1, num10 + 1);
					LineFrontier.Anchor anchor8 = list3[index];
					LineFrontier.Anchor anchor9 = list3[index2];
					LineFrontier.Anchor anchor10 = list3[index3];
					float num11 = (anchor8.t + anchor9.t) * 0.5f;
					float num12 = anchor9.t;
					float num13 = (anchor9.t + anchor10.t) * 0.5f;
					num11 = Mathf.Clamp(num11, -40f, 40f);
					num12 = Mathf.Clamp(num12, -40f, 40f);
					num13 = Mathf.Clamp(num13, -40f, 40f);
					if (num13 > num11)
					{
						Vector2 vector = (anchor8.pos + anchor9.pos) * 0.5f;
						Vector2 pos = anchor9.pos;
						Vector2 vector2 = (anchor9.pos + anchor10.pos) * 0.5f;
						Debug.DrawLine(pos, Vector2.Lerp(pos, vector, 0.9f), Color.green, 4f);
						Debug.DrawLine(pos, Vector2.Lerp(pos, vector2, 0.9f), Color.green, 4f);
						float num14 = num9 - (anchor8.depthOffset + anchor9.depthOffset) * 0.5f;
						float num15 = num9 - anchor9.depthOffset;
						float num16 = num9 - (anchor9.depthOffset + anchor10.depthOffset) * 0.5f;
						LineFrontier.Point point = default(LineFrontier.Point);
						point.vert0 = anchor9.index0;
						point.vert1 = anchor9.index1;
						float num17 = 1f - (num12 - num11) / (num13 - num11);
						int num18 = Mathf.CeilToInt(num11);
						while ((float)num18 < num13)
						{
							float num19 = ExtraMath.RemapValue((float)num18, num11, num13);
							float num20 = 1f - num19;
							num19 = Mathf.Lerp(num19, num17 * 2f * num20 * num19 + num19 * num19, 0.5f);
							num20 = 1f - num19;
							float num21 = num20 * num20;
							float num22 = 2f * num20 * num19;
							float num23 = num19 * num19;
							point.dir = (2f * num20 * (pos - vector) + 2f * num19 * (vector2 - pos)).normalized;
							point.pos = vector * num21 + pos * num22 + vector2 * num23;
							point.pos -= point.tangent * (num14 * num21 + num15 * num22 + num16 * num23);
							int num24 = num8;
							int num25 = num18 + 40;
							try
							{
								this.points[num24, num25] = point;
							}
							catch (Exception ex)
							{
								string message = string.Format("points[{0},{1}] - assigning [{2},{3}]", new object[]
								{
									this.points.GetLength(0),
									this.points.GetLength(1),
									num24,
									num25
								});
								Debug.Log(message);
								throw ex;
							}
							num18++;
						}
					}
				}
				for (int num26 = 0; num26 < 2; num26++)
				{
					for (int num27 = 1; num27 < this.sizeY - 1; num27++)
					{
						LineFrontier.Point point2 = this.points[num8, num27 - 1];
						LineFrontier.Point point3 = this.points[num8, num27];
						LineFrontier.Point point4 = this.points[num8, num27 + 1];
						Vector2 value = (point2.pos + point3.pos + point4.pos) / 3f;
						smoothList[num27] = value;
					}
					for (int num28 = 1; num28 < this.sizeY - 1; num28++)
					{
						LineFrontier.Point point5 = this.points[num8, num28];
						point5.pos = smoothList[num28];
						this.points[num8, num28] = point5;
					}
				}
				for (int num29 = 1; num29 < this.sizeY - 1; num29++)
				{
					LineFrontier.Point point6 = this.points[num8, num29 - 1];
					LineFrontier.Point point7 = this.points[num8, num29];
					LineFrontier.Point point8 = this.points[num8, num29 + 1];
					point7.dir = (point8.pos - point6.pos).normalized;
					this.points[num8, num29] = point7;
				}
				for (int num30 = 0; num30 < 2; num30++)
				{
					int num31 = (this.sizeY - 1) * num30;
					LineFrontier.Point point9 = this.points[num8, num31];
					LineFrontier.Anchor anchor11 = list3[(list3.Count - 1) * num30];
					point9.pos.y = anchor11.pos.y;
					point9.dir = Vector2.down;
					point9.vert0 = anchor11.index0;
					point9.vert1 = anchor11.index1;
					this.points[num8, num31] = point9;
				}
			}
			smoothList.ReturnToListPool<Vector2>();
			float commonA = this.commonLine.color.a;
			this.commonLines = new List<LineFrontierGraphic>(this.sizeX);
			for (int depth2 = 0; depth2 < this.sizeX; depth2++)
			{
				LineFrontierGraphic line = UnityEngine.Object.Instantiate<LineFrontierGraphic>(this.commonLine, this.commonLine.transform.parent).Set(this, (float)(depth2 - 4));
				line.name = this.commonLine.name + " " + depth2;
				this.commonLines.Add(line);
				yield return info;
			}
			this.mainLine.Set(this, (float)campaign.campaignSave.vikingFrontierPosition);
			yield return info;
			this.mainLineAnim = new TargetAnimator<float>(() => this.mainLine.frontierDepth, delegate(float x)
			{
				this.mainLine.frontierDepth = x;
			}, campaign.rootState, LerpTowards.standard);
			this.mainLineAnim.Subscribe(delegate(float x)
			{
				Shader.SetGlobalFloat(LineFrontier.frontierDepthId, x);
				foreach (LineFrontierGraphic lineFrontierGraphic in this.commonLines)
				{
					lineFrontierGraphic.gameObject.SetActive(lineFrontierGraphic.frontierDepth > x);
				}
			});
			yield return info;
			this.nextLine = (float)(campaign.campaignSave.vikingFrontierPosition + 1);
			this.nextLineAnim = new TargetAnimator<float>(() => this.nextLine, delegate(float x)
			{
				this.nextLine = x;
			}, campaign.rootState, LerpTowards.standard);
			this.nextLineAnim.Subscribe(delegate(float x)
			{
				foreach (LineFrontierGraphic lineFrontierGraphic in this.commonLines)
				{
					float a = lineFrontierGraphic.color.a;
					float num32 = Mathf.Lerp(commonA, 0.65f, this.GetNextLerp(lineFrontierGraphic.frontierDepth, x));
					if (a != num32)
					{
						lineFrontierGraphic.color = lineFrontierGraphic.color.SetA(num32);
						lineFrontierGraphic.SetMaterialDirty();
					}
				}
			});
			this.darkenAnim = new TargetAnimator<float>(() => this.darken, delegate(float x)
			{
				this.darken = x;
			}, campaign.rootState, LerpTowards.standard);
			this.darkenAnim.Subscribe(delegate(float x)
			{
				this.mainLine.width = Mathf.Lerp(1f, 2f, x);
				this.mainLine.color = Color.black.SetA(Mathf.Lerp(0.8f, 1f, x));
				Shader.SetGlobalFloat(LineFrontier.frontierDarkenId, x);
			});
			yield return info;
			yield break;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000B994E File Offset: 0x000B7D4E
		public float GetNextLerp(float depth, float x)
		{
			return Mathf.Clamp01(x - depth + 1f);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000B9960 File Offset: 0x000B7D60
		private void OnDrawGizmos()
		{
			if (this.anchorLists != null)
			{
				Gizmos.matrix = base.transform.localToWorldMatrix;
				Gizmos.color = this.gizmoColor;
				for (int i = 0; i < this.anchorLists.Count; i++)
				{
					List<LineFrontier.Anchor> list = this.anchorLists[i];
					float num = (float)(i - 4);
					for (int j = 0; j < list.Count - 1; j++)
					{
						LineFrontier.Anchor anchor = list[j];
						LineFrontier.Anchor anchor2 = list[j + 1];
						Vector2 v = anchor.pos + anchor.dir * (num - anchor.depthOffset);
						Vector2 v2 = anchor2.pos + anchor2.dir * (num - anchor2.depthOffset);
						Gizmos.DrawLine(v, v2);
					}
				}
				Gizmos.color = Color.yellow;
			}
			Gizmos.color = Color.black;
		}

		// Token: 0x04001F19 RID: 7961
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("LineFrontier", EVerbosity.Minimal, 0);

		// Token: 0x04001F1A RID: 7962
		public List<List<LineFrontier.Anchor>> anchorLists;

		// Token: 0x04001F1B RID: 7963
		public LineFrontier.Point[,] points;

		// Token: 0x04001F1C RID: 7964
		[SerializeField]
		private LineFrontierGraphic commonLine;

		// Token: 0x04001F1D RID: 7965
		[SerializeField]
		private LineFrontierGraphic mainLine;

		// Token: 0x04001F1E RID: 7966
		private List<LineFrontierGraphic> commonLines;

		// Token: 0x04001F1F RID: 7967
		public TargetAnimator<float> mainLineAnim;

		// Token: 0x04001F20 RID: 7968
		public TargetAnimator<float> darkenAnim;

		// Token: 0x04001F21 RID: 7969
		public TargetAnimator<float> nextLineAnim;

		// Token: 0x04001F22 RID: 7970
		private float darken;

		// Token: 0x04001F23 RID: 7971
		private float nextLine;

		// Token: 0x04001F24 RID: 7972
		[SerializeField]
		public AnimationCurve frontierMoveCurve;

		// Token: 0x04001F25 RID: 7973
		[SerializeField]
		public AnimationCurve frontierDarkenCurve;

		// Token: 0x04001F26 RID: 7974
		private static ShaderId frontierDepthId = "_FrontierDepth";

		// Token: 0x04001F27 RID: 7975
		private static ShaderId frontierDarkenId = "_FrontierDarken";

		// Token: 0x04001F28 RID: 7976
		public const int bezierCount = 80;

		// Token: 0x04001F29 RID: 7977
		public const int halfCount = 40;

		// Token: 0x04001F2A RID: 7978
		public const int spacing = 2;

		// Token: 0x04001F2B RID: 7979
		public const int padding = 4;

		// Token: 0x04001F2C RID: 7980
		public int sizeX;

		// Token: 0x04001F2D RID: 7981
		public int sizeY;

		// Token: 0x04001F2E RID: 7982
		private Color commonCol0;

		// Token: 0x04001F2F RID: 7983
		private Color commonCol1 = new Color(0f, 0f, 0f, 0.6f);

		// Token: 0x04001F30 RID: 7984
		[SerializeField]
		private float debugDepth;

		// Token: 0x04001F31 RID: 7985
		[SerializeField]
		private Color gizmoColor = Color.white;

		// Token: 0x02000714 RID: 1812
		public class Anchor
		{
			// Token: 0x06002F08 RID: 12040 RVA: 0x000B9A8C File Offset: 0x000B7E8C
			public Anchor(CampaignTriangles.Vertex vert0, CampaignTriangles.Vertex vert1, int index0, int index1)
			{
				this.vert0 = vert0;
				this.vert1 = vert1;
				this.index0 = index0;
				this.index1 = index1;
				this.pos = (vert0.pos + vert1.pos) / 2f;
				this.dir = (vert1.pos - vert0.pos).normalized;
				this.depthOffset = (float)(vert0.frontierDepth + vert1.frontierDepth) / 2f;
			}

			// Token: 0x04001F33 RID: 7987
			public float t;

			// Token: 0x04001F34 RID: 7988
			public float dt;

			// Token: 0x04001F35 RID: 7989
			public float denom;

			// Token: 0x04001F36 RID: 7990
			public int count;

			// Token: 0x04001F37 RID: 7991
			public readonly CampaignTriangles.Vertex vert0;

			// Token: 0x04001F38 RID: 7992
			public readonly CampaignTriangles.Vertex vert1;

			// Token: 0x04001F39 RID: 7993
			public readonly int index0;

			// Token: 0x04001F3A RID: 7994
			public readonly int index1;

			// Token: 0x04001F3B RID: 7995
			public readonly Vector2 pos;

			// Token: 0x04001F3C RID: 7996
			public readonly Vector2 dir;

			// Token: 0x04001F3D RID: 7997
			public readonly float depthOffset;
		}

		// Token: 0x02000715 RID: 1813
		public struct Point
		{
			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000B9B16 File Offset: 0x000B7F16
			public Vector2 tangent
			{
				get
				{
					return ExtraMath.Rotate2D90(this.dir);
				}
			}

			// Token: 0x06002F0A RID: 12042 RVA: 0x000B9B23 File Offset: 0x000B7F23
			public static LineFrontier.Point operator *(LineFrontier.Point a, float b)
			{
				a.pos *= b;
				a.dir *= b;
				return a;
			}

			// Token: 0x06002F0B RID: 12043 RVA: 0x000B9B4C File Offset: 0x000B7F4C
			public static LineFrontier.Point operator /(LineFrontier.Point a, float b)
			{
				a.pos /= b;
				a.dir /= b;
				return a;
			}

			// Token: 0x06002F0C RID: 12044 RVA: 0x000B9B75 File Offset: 0x000B7F75
			public static LineFrontier.Point operator +(LineFrontier.Point a, LineFrontier.Point b)
			{
				a.pos += b.pos;
				a.dir += b.dir;
				return a;
			}

			// Token: 0x04001F3E RID: 7998
			public Vector2 pos;

			// Token: 0x04001F3F RID: 7999
			public Vector2 dir;

			// Token: 0x04001F40 RID: 8000
			public int vert0;

			// Token: 0x04001F41 RID: 8001
			public int vert1;
		}
	}
}
