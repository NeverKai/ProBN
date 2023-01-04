using System;
using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000750 RID: 1872
	[Serializable]
	public class DistanceField
	{
		// Token: 0x060030CF RID: 12495 RVA: 0x000C7C98 File Offset: 0x000C6098
		public DistanceField(NavigationMesh navMesh, List<int> vertIndexes, string name)
		{
			this.name = name;
			this._navMesh = navMesh;
			this.flowContents = new DistanceField.FlowContent[navMesh.verts.Length];
			for (int i = 0; i < this.flowContents.Length; i++)
			{
				this.flowContents[i].Reset();
			}
			for (int j = 0; j < vertIndexes.Count; j++)
			{
				this.flowContents[vertIndexes[j]] = new DistanceField.FlowContent
				{
					distance = 0f,
					hasDistance = true,
					direction = Vector3.zero
				};
			}
			this.MaybeInitializeFlow();
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000C7D58 File Offset: 0x000C6158
		public DistanceField(NavigationMesh navMesh, int vertIndex, string name)
		{
			this.name = name;
			this._navMesh = navMesh;
			this.flowContents = new DistanceField.FlowContent[navMesh.verts.Length];
			for (int i = 0; i < this.flowContents.Length; i++)
			{
				this.flowContents[i].Reset();
			}
			this.flowContents[vertIndex] = new DistanceField.FlowContent
			{
				distance = 0f,
				hasDistance = true,
				direction = Vector3.zero
			};
			this.MaybeInitializeFlow();
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000C7DFC File Offset: 0x000C61FC
		public Vector3 SampleDirection(NavPos navPos)
		{
			if (navPos.navigationMesh != this._navMesh)
			{
				return Vector3.zero;
			}
			Vector3 a = Vector3.zero;
			Vector3 bary = navPos.bary;
			Tri tri = navPos.tri;
			a += this.GetDirection(tri.verts[0]) * bary.x;
			a += this.GetDirection(tri.verts[1]) * bary.y;
			return a + this.GetDirection(tri.verts[2]) * bary.z;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000C7EB1 File Offset: 0x000C62B1
		public float SampleDistance(Vert vert)
		{
			if (vert.navMesh != this._navMesh.Target)
			{
				return 100f;
			}
			return this.GetDistance(vert);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000C7EDC File Offset: 0x000C62DC
		public float SampleDistance(NavPos navPos)
		{
			if (navPos.navigationMesh != this._navMesh.Target)
			{
				return 100f;
			}
			Vector3 bary = navPos.bary;
			Tri tri = navPos.tri;
			float num = 0f;
			num += this.GetDistance(tri.verts[0]) * bary.x;
			num += this.GetDistance(tri.verts[1]) * bary.y;
			return num + this.GetDistance(tri.verts[2]) * bary.z;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000C7F7C File Offset: 0x000C637C
		public void Sample(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			using (new ScopedProfiler("DistanceField.Sample", null))
			{
				if (navPos.navigationMesh != this._navMesh.Target)
				{
					dir = Vector3.zero;
					dist = 100f;
				}
				else
				{
					Vector3 bary = navPos.bary;
					Tri tri = navPos.tri;
					DistanceField.FlowContent flowContent = this.flowContents[(int)tri.verts[0].index];
					DistanceField.FlowContent flowContent2 = this.flowContents[(int)tri.verts[1].index];
					DistanceField.FlowContent flowContent3 = this.flowContents[(int)tri.verts[2].index];
					dist = 0f;
					dist += flowContent.distance * bary.x;
					dist += flowContent2.distance * bary.y;
					dist += flowContent3.distance * bary.z;
					dir = Vector3.zero;
					dir += flowContent.direction * bary.x;
					dir += flowContent2.direction * bary.y;
					dir += flowContent3.direction * bary.z;
				}
			}
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000C812C File Offset: 0x000C652C
		private void MaybeInitializeFlow()
		{
			using (new ScopedProfiler("DistanceField.MaybeInitializeFlow", null))
			{
				NavigationMesh target = this._navMesh.Target;
				DistanceField.pipeList.Clear();
				for (int i = 0; i < target.verts.Length; i++)
				{
					if (this.flowContents[i].hasDistance)
					{
						Vert vert = target.verts[i];
						for (int j = 0; j < vert.pipes.Count; j++)
						{
							Pipe pipe = vert.pipes[j];
							float cost = this.GetCost(pipe);
							if (!this.flowContents[(int)pipe.outVert.index].hasDistance)
							{
								int k;
								for (k = 0; k < DistanceField.pipeList.Count; k++)
								{
									Pipe pipe2 = DistanceField.pipeList[k];
									float cost2 = this.GetCost(pipe2);
									if (cost < cost2)
									{
										break;
									}
								}
								DistanceField.pipeList.Insert(k, pipe);
							}
						}
					}
				}
				for (int l = 0; l < DistanceField.pipeList.Count; l++)
				{
					Pipe pipe3 = DistanceField.pipeList[l];
					for (int m = l + 1; m < DistanceField.pipeList.Count; m++)
					{
						Pipe pipe4 = DistanceField.pipeList[m];
						if (pipe3.outVert == pipe4.outVert)
						{
							DistanceField.pipeList.RemoveAt(m);
							m--;
						}
					}
					DistanceField.FlowContent flowContent = new DistanceField.FlowContent
					{
						distance = this.GetCost(pipe3),
						hasDistance = true,
						direction = -pipe3.dir * pipe3.edge.length
					};
					this.flowContents[(int)pipe3.outVert.index] = flowContent;
				}
				while (DistanceField.pipeList.Count > 0)
				{
					Pipe pipe5 = DistanceField.pipeList[0];
					DistanceField.pipeList.RemoveAt(0);
					Vert outVert = pipe5.outVert;
					DistanceField.FlowContent flowContent2 = this.flowContents[(int)pipe5.outVert.index];
					for (int n = 0; n < outVert.pipes.Count; n++)
					{
						Pipe pipe6 = outVert.pipes[n];
						Vert outVert2 = pipe6.outVert;
						DistanceField.FlowContent flowContent3 = this.flowContents[(int)outVert2.index];
						float num = flowContent2.distance + this.GetCost(pipe6);
						if (num < flowContent3.distance)
						{
							flowContent3.distance = num;
							flowContent3.hasDistance = true;
							this.flowContents[(int)outVert2.index] = flowContent3;
							int num2;
							for (num2 = 0; num2 < DistanceField.pipeList.Count; num2++)
							{
								DistanceField.FlowContent flowContent4 = this.flowContents[(int)DistanceField.pipeList[num2].inVert.index];
								if (num < flowContent4.distance)
								{
									break;
								}
							}
							DistanceField.pipeList.Insert(num2, pipe6);
						}
					}
				}
				for (int num3 = 0; num3 < target.verts.Length; num3++)
				{
					Vert vert2 = target.verts[num3];
					DistanceField.FlowContent flowContent5 = this.flowContents[num3];
					float distance = flowContent5.distance;
					for (int num4 = 0; num4 < vert2.pipes.Count; num4++)
					{
						Pipe pipe7 = vert2.pipes[num4];
						DistanceField.FlowContent flowContent6 = this.flowContents[(int)pipe7.outVert.index];
						flowContent5.direction += pipe7.dir * pipe7.weight * (distance - flowContent6.distance);
					}
					if (vert2.border)
					{
						Pipe pipe8 = vert2.pipes[0];
						Vert outVert3 = pipe8.outVert;
						DistanceField.FlowContent flowContent7 = this.flowContents[(int)outVert3.index];
						Pipe pipe9 = vert2.pipes[vert2.pipes.Count - 1];
						Vert outVert4 = pipe9.outVert;
						DistanceField.FlowContent flowContent8 = this.flowContents[(int)outVert4.index];
						if (flowContent7.distance < flowContent5.distance && flowContent7.distance < flowContent8.distance)
						{
							Vector3 vector = pipe8.edge.dir;
							Vector3 vector2 = new Vector3(vector.z, 0f, -vector.x);
							vector = vector2.normalized;
							float num5 = Vector3.Dot(flowContent5.direction, vector) - 0.05f;
							if (num5 < 0f)
							{
								flowContent5.direction -= num5 * vector;
							}
						}
						else if (flowContent8.distance < flowContent5.distance && flowContent8.distance < flowContent7.distance)
						{
							Vector3 vector3 = pipe9.edge.dir;
							Vector3 vector4 = new Vector3(vector3.z, 0f, -vector3.x);
							vector3 = vector4.normalized;
							float num6 = Vector3.Dot(flowContent5.direction, vector3) - 0.05f;
							if (num6 < 0f)
							{
								flowContent5.direction -= num6 * vector3;
							}
						}
						else
						{
							float num7 = Vector3.Dot(flowContent5.direction, vert2.borderVector);
							if (num7 < 0f)
							{
								flowContent5.direction -= num7 * vert2.borderVector;
							}
						}
					}
					flowContent5.direction = flowContent5.direction.normalized * Mathf.Min(1f, flowContent5.distance);
					this.flowContents[num3] = flowContent5;
				}
			}
			DistanceField.pipeList.Clear();
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000C87B4 File Offset: 0x000C6BB4
		private float GetDistance(Vert vert)
		{
			return this.GetDistance(this.flowContents[(int)vert.index]);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000C87D2 File Offset: 0x000C6BD2
		private float GetDistance(DistanceField.FlowContent flow)
		{
			return flow.distance;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000C87DB File Offset: 0x000C6BDB
		private Vector3 GetDirection(Vert vert)
		{
			return this.flowContents[(int)vert.index].direction;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000C87F4 File Offset: 0x000C6BF4
		private float GetCost(Pipe pipe)
		{
			float num = pipe.edge.length;
			if (pipe.edge.border)
			{
				num *= 1.2f;
			}
			return num;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000C8828 File Offset: 0x000C6C28
		public void DrawGizmos()
		{
			if (this.flowContents == null)
			{
				return;
			}
			NavigationMesh target = this._navMesh.Target;
			for (int i = 0; i < this.flowContents.Length; i++)
			{
				DistanceField.FlowContent flowContent = this.flowContents[i];
				Gizmos.DrawRay(target.verts[i].pos, flowContent.direction * 0.1f);
			}
			for (int j = 0; j < target.tris.Length; j++)
			{
				NavPos navPos = new NavPos(target.tris[j]);
				Gizmos.DrawRay(navPos.pos, this.SampleDirection(navPos) * 0.2f);
			}
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000C88E1 File Offset: 0x000C6CE1
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000C88E9 File Offset: 0x000C6CE9
		public static implicit operator bool(DistanceField reference)
		{
			return reference != null;
		}

		// Token: 0x040020A6 RID: 8358
		private string name;

		// Token: 0x040020A7 RID: 8359
		private DistanceField.FlowContent[] flowContents;

		// Token: 0x040020A8 RID: 8360
		private WeakReference<NavigationMesh> _navMesh;

		// Token: 0x040020A9 RID: 8361
		private static List<Pipe> pipeList = new List<Pipe>(16);

		// Token: 0x02000751 RID: 1873
		private struct FlowContent
		{
			// Token: 0x060030DE RID: 12510 RVA: 0x000C8900 File Offset: 0x000C6D00
			public void Reset()
			{
				this.direction = Vector3.zero;
				this.distance = float.PositiveInfinity;
				this.hasDistance = false;
			}

			// Token: 0x040020AA RID: 8362
			public Vector3 direction;

			// Token: 0x040020AB RID: 8363
			public float distance;

			// Token: 0x040020AC RID: 8364
			public bool hasDistance;
		}
	}
}
