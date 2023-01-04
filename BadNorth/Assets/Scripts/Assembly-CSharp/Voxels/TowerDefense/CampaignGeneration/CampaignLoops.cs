using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006D7 RID: 1751
	public class CampaignLoops : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002D50 RID: 11600 RVA: 0x000AACE6 File Offset: 0x000A90E6
		private int HashFromEdge(int i0, int i1)
		{
			return i0 * 103 + i1 * 193;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000AACF4 File Offset: 0x000A90F4
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("CampaignLoops", GenInfo.Mode.interruptable);
			CampaignSave campaignSave = campaign.campaignSave;
			List<LevelState> levels = campaignSave.levelStates;
			HashSet<int> hashSet = new HashSet<int>();
			for (int i0 = 0; i0 < levels.Count; i0++)
			{
				LevelState level0 = levels[i0];
				for (int j = 0; j < level0.connections.Count; j++)
				{
					int i = level0.connections[j];
					int hash = this.HashFromEdge(i0, i);
					if (!hashSet.Contains(hash))
					{
						hashSet.Add(hash);
						Loop loop = new Loop(3)
						{
							levels[i]
						};
						this.loops.Add(loop);
						int previousIndex = i0;
						int currentIndex = i;
						while (currentIndex != i0)
						{
							LevelState current = levels[currentIndex];
							int previousInCurrent = current.connections.IndexOf(previousIndex);
							int next = current.connections[(previousInCurrent + 1) % current.connections.Count];
							previousIndex = currentIndex;
							currentIndex = next;
							hashSet.Add(this.HashFromEdge(previousIndex, currentIndex));
							loop.Add(levels[next]);
							yield return info;
						}
					}
				}
				yield return info;
			}
			for (int k = 0; k < this.loops.Count; k++)
			{
				yield return info;
				Loop loop2 = this.loops[k];
				if (loop2.Count != 3)
				{
					int bestIndex = -1;
					int bestCount = -1;
					float lowestCost = float.MaxValue;
					for (int l = 0; l < loop2.Count; l++)
					{
						yield return info;
						LevelState vert0 = loop2[l];
						Vector3 tan0 = this.GetTangent(loop2, l, true);
						Vector3 tan = this.GetTangent(loop2, l, false);
						for (int m = 2; m < loop2.Count - 1; m++)
						{
							yield return info;
							int i2 = (l + m) % loop2.Count;
							LevelState vert = loop2[i2];
							if (vert0 != vert)
							{
								Vector2 diff = vert.pos - vert0.pos;
								float distance = diff.magnitude;
								if (distance <= campaign.rect.height * 1.5f)
								{
									float cost = distance;
									if (cost <= lowestCost)
									{
										if (!((Vector2.Dot(ExtraMath.Rotate2D90(tan0), tan) <= 0f) ? (Vector2.Dot(tan0, diff) <= 0f && Vector2.Dot(tan, diff) <= 0f) : (Vector2.Dot(tan0, diff) <= 0f || Vector2.Dot(tan, diff) <= 0f)))
										{
											Vector3 tan2 = this.GetTangent(loop2, i2, true);
											Vector3 tan3 = this.GetTangent(loop2, i2, false);
											if (!((Vector2.Dot(ExtraMath.Rotate2D90(tan2), tan3) <= 0f) ? (Vector2.Dot(tan2, -diff) <= 0f && Vector2.Dot(tan3, -diff) <= 0f) : (Vector2.Dot(tan2, -diff) <= 0f || Vector2.Dot(tan3, -diff) <= 0f)))
											{
												cost *= ExtraMath.RemapValue(Vector2.Dot((tan0 + tan).normalized, (tan2 + tan3).normalized), -1f, 1f, 1f, 0.2f);
												if (cost <= lowestCost)
												{
													bool intersects = false;
													int num = 0;
													while (num < loop2.Count - 1 && !intersects)
													{
														LevelState levelState = loop2[num];
														LevelState levelState2 = loop2[num + 1];
														if (levelState != vert0 && levelState2 != vert0 && levelState != vert && levelState2 != vert)
														{
															if (ExtraMath.LineIntersection(vert0.pos, vert.pos, levelState.pos, levelState2.pos))
															{
																intersects = true;
															}
														}
														num++;
													}
													if (!intersects)
													{
														Vector2 midPoint = (vert0.pos + vert.pos) * 0.5f;
														Vector2 dir = diff / distance;
														Vector2 tan4 = dir / (distance * 0.5f);
														Vector2 tan5 = ExtraMath.Rotate2D90(dir / (distance * 0.3f));
														int num2 = 0;
														while (num2 < loop2.Count && !intersects)
														{
															LevelState levelState3 = loop2[num2];
															if (levelState3 != vert0 && levelState3 != vert)
															{
																Vector2 lhs = levelState3.pos - midPoint;
																if (Mathf.Abs(Vector2.Dot(lhs, tan4)) + Mathf.Abs(Vector2.Dot(lhs, tan5)) < 1f)
																{
																	intersects = true;
																}
															}
															num2++;
														}
														if (!intersects)
														{
															if (!intersects)
															{
																lowestCost = cost;
																bestIndex = l;
																bestCount = m;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					if (bestCount != -1)
					{
						for (int n = 0; n < bestIndex; n++)
						{
							LevelState item = loop2[0];
							loop2.RemoveAt(0);
							loop2.Add(item);
						}
						Loop loop3 = new Loop(loop2.Count - bestCount);
						for (int num3 = bestCount; num3 < loop2.Count; num3++)
						{
							loop3.Add(loop2[num3]);
						}
						loop3.Add(loop2[0]);
						loop2.RemoveRange(bestCount + 1, loop2.Count - bestCount - 1);
						this.loops.Add(loop3);
						k--;
					}
				}
			}
			this.loops.Sort((Loop a, Loop b) => b.Count.CompareTo(a.Count));
			Loop longestLoop = this.loops[0];
			int lowestIndex = longestLoop.IndexOf(levels[0]);
			for (int num4 = 0; num4 < lowestIndex; num4++)
			{
				LevelState item2 = longestLoop[0];
				longestLoop.RemoveAt(0);
				longestLoop.Add(item2);
			}
			yield return info;
			yield break;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000AAD18 File Offset: 0x000A9118
		private Vector3 GetTangent(List<LevelState> loop, int index, bool forward)
		{
			int num = (!forward) ? 0 : 1;
			LevelState levelState = loop[(loop.Count + index + num - 1) % loop.Count];
			LevelState levelState2 = loop[(loop.Count + index + num) % loop.Count];
			return ExtraMath.Rotate2D90(levelState.pos - levelState2.pos);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000AAD80 File Offset: 0x000A9180
		private bool IsConvex(List<LevelState> loop, int index)
		{
			LevelState levelState = loop[(loop.Count + index - 1) % loop.Count];
			LevelState levelState2 = loop[(loop.Count + index) % loop.Count];
			LevelState levelState3 = loop[(loop.Count + index + 1) % loop.Count];
			return Vector3.Dot(ExtraMath.Rotate2D90(levelState.pos - levelState2.pos), levelState3.pos - levelState2.pos) < 0f;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000AAE10 File Offset: 0x000A9210
		private void OnDrawGizmos()
		{
			Gizmos.matrix = Matrix4x4.Translate(Vector3.back * 10f);
			Gizmos.color = UnityEngine.Color.white.SetA(0.2f);
			for (int i = 1; i < this.loops.Count; i++)
			{
				Loop loop = this.loops[i];
				for (int j = 0; j < loop.Count; j++)
				{
					Vector2 pos = loop[j].pos;
					Vector2 pos2 = loop[(j + 1) % loop.Count].pos;
					Vector2 pos3 = loop[(j + 2) % loop.Count].pos;
					Vector2 pos4 = loop[(j + 3) % loop.Count].pos;
					Vector2 vector = ExtraMath.Rotate2D90(pos - pos2).normalized / 2f;
					Vector2 b = ExtraMath.Rotate2D90(pos2 - pos3).normalized / 2f;
					Vector2 b2 = ExtraMath.Rotate2D90(pos3 - pos4).normalized / 2f;
					Vector2 v = pos2 + vector + b;
					Vector2 v2 = pos3 + b + b2;
					Gizmos.DrawLine(v, v2);
					if (this.IsConvex(loop, j + 1))
					{
						Gizmos.DrawRay(v, vector + b);
					}
				}
			}
		}

		// Token: 0x04001DF2 RID: 7666
		public List<Loop> loops = new List<Loop>();
	}
}
