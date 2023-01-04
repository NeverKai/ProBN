using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense
{
	// Token: 0x02000886 RID: 2182
	public class Wind : IslandComponent, IIslandFirstEnter, IIslandLeave, IIslandEnter, IIslandDestroyEntered
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000F8161 File Offset: 0x000F6561
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x000F8158 File Offset: 0x000F6558
		public Vector3 size { get; private set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000F8172 File Offset: 0x000F6572
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x000F8169 File Offset: 0x000F6569
		public Matrix4x4 wind2World { get; private set; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000F8183 File Offset: 0x000F6583
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000F817A File Offset: 0x000F657A
		public Matrix4x4 world2Wind { get; private set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000F818B File Offset: 0x000F658B
		private float globalWindSpeed
		{
			get
			{
				return Singleton<WorldWind>.instance.windSpeed;
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000F8198 File Offset: 0x000F6598
		private IEnumerator Setup(Island island)
		{
			this.referenceArray = island.voxelSpace.cornerVoxels;
			this.size = island.voxelSpace.voxelSize;
			this.windTex = new Fake3dTex(this.size, new Color(0.5f, 0.5f, 0.5f, 1f), false, island.texturePool);
			this.windOffset = (this.size - Vector3.one) / -2f;
			this.windOffset.y = -0.5f;
			Vector3 windSize = new Vector3(island.fog.maxRad * 2f, island.voxelSpace.voxelSize.y + 2f, island.fog.maxRad * 2f);
			Bounds bounds = new Bounds(new Vector3(0f, windSize.y / 2f - 0.5f, 0f), windSize);
			this.wind2World = Matrix4x4.TRS(bounds.min, Quaternion.identity, bounds.size);
			this.world2Wind = this.wind2World.inverse;
			this.nodes = new Wind.Node[this.referenceArray.Length];
			for (int j = 0; j < this.nodes.Length; j++)
			{
				VoxelSpace.CornerVoxel cornerVoxel = this.referenceArray[j];
				if (cornerVoxel.coverage < 1f)
				{
					Wind.Node node2 = new Wind.Node(cornerVoxel.pos, cornerVoxel.coverage);
					this.nodes[j] = node2;
					this.nodeList.Add(node2);
				}
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				Wind.Node node = this.nodes[i];
				if (node != null)
				{
					Vector3 plane = new Vector3(0.6f, 0.3f, 0.4f);
					for (int k = -1; k <= 1; k++)
					{
						for (int l = 0; l <= 0; l++)
						{
							for (int m = -1; m <= 1; m++)
							{
								Vector3 vector = new Vector3((float)k, (float)l, (float)m);
								if (Vector3.Dot(vector, plane) >= 0f)
								{
									if (!(vector == Vector3.zero))
									{
										Vector3 vector2 = node.pos + vector;
										if (vector2.y < this.size.y && vector2.y >= 0f)
										{
											Vector3 rhs = vector2;
											vector2.x = (vector2.x + this.size.x) % this.size.x;
											vector2.z = (vector2.z + this.size.z) % this.size.z;
											int num = ExtraMath.CoordinateToIndex(vector2, this.size);
											if (num < 0 || num > this.nodes.Length)
											{
												Debug.LogError(string.Concat(new object[]
												{
													num,
													" ",
													vector2,
													" ",
													node.pos,
													" ",
													vector
												}));
											}
											Wind.Node node3 = this.nodes[ExtraMath.CoordinateToIndex(vector2, this.size)];
											if (node3 != null)
											{
												if (node.coverage <= 0f || node3.coverage <= 0f || !Physics.Raycast(node.pos + this.windOffset, vector, vector.magnitude / 2f, LayerMaster.houseMask))
												{
													Wind.Link link = new Wind.Link(node, node3, vector);
													link.edge = (vector2 != rhs);
													if (!link.edge)
													{
														link.coverage = island.voxelSpace.GetCoverageLinear(node.pos + vector * 0.5f + this.windOffset);
													}
													this.links.Add(link);
												}
											}
										}
									}
								}
							}
						}
					}
					yield return null;
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000F81BC File Offset: 0x000F65BC
		public IEnumerator UpdateWind()
		{
			float maxDelta = 1f;
			int iterations = -1;
			Vector3 normalizedWind = this.windDir.normalized;
			int linkCount = this.links.Count;
			int nodeCount = this.nodeList.Count;
			for (int i = 0; i < linkCount; i++)
			{
				Wind.Link link = this.links[i];
				float num = Mathf.Lerp(0.5f, 1.5f, link.node0.pos.y / this.size.y);
				link.flow = Vector3.Dot(link.dir, normalizedWind) * 0.5f * num;
			}
			while (maxDelta > 0.02f)
			{
				maxDelta = 0f;
				iterations++;
				float dt = ExtraMath.RemapValue((float)iterations, 0f, 500f, 0.5f, 0.2f);
				for (int j = 0; j < linkCount; j++)
				{
					Wind.Link link2 = this.links[j];
					if (!link2.edge)
					{
						Wind.Node node = link2.node0;
						Wind.Node node2 = link2.node1;
						float num2 = node.coverage + node.amount - (node2.coverage + node2.amount);
						num2 *= link2.crossSection;
						num2 /= link2.length;
						link2.flow *= 0.99f;
						link2.flow += num2 * dt;
					}
				}
				yield return null;
				for (int k = 0; k < linkCount; k++)
				{
					Wind.Link link3 = this.links[k];
					link3.node0.delta -= link3.flow;
					link3.node1.delta += link3.flow;
				}
				yield return null;
				for (int l = 0; l < nodeCount; l++)
				{
					Wind.Node node3 = this.nodeList[l];
					maxDelta = Mathf.Max(maxDelta, node3.delta);
					node3.amount += node3.delta * dt;
					node3.delta = 0f;
				}
				yield return null;
			}
			for (int m = 0; m < nodeCount; m++)
			{
				Wind.Node node4 = this.nodeList[m];
				node4.flow = Vector3.zero;
				foreach (Wind.Pipe pipe in node4.pipes)
				{
					if (pipe.flow > 0f)
					{
						node4.flow += pipe.flow * pipe.dir * pipe.link.crossSection;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000F81D8 File Offset: 0x000F65D8
		private void ApplyWindTexture()
		{
			Vector3 a = this.windDir.normalized;
			a *= 0.5f;
			this.windTex.SetAllPixels(new Color((a.x + 1f) / 2f, (a.y + 1f) / 2f, (a.z + 1f) / 2f, 1f));
			this.vectors = new Vector3[(int)this.size.x, (int)this.size.y, (int)this.size.z];
			for (int i = 0; i < this.nodes.Length; i++)
			{
				Wind.Node node = this.nodes[i];
				Color color = new Color(0.5f, 0.5f, 0.5f, 0f);
				Vector3Int pos = this.referenceArray[i].pos;
				if (node != null)
				{
					this.vectors[(int)node.pos.x, (int)node.pos.y, (int)node.pos.z] = node.flow;
					Vector3 a2 = node.flow * 0.5f;
					a2 = (a2 + Vector3.one) / 2f;
					color = new Color(a2.x, a2.y, a2.z, 1f);
				}
				this.windTex.SetPixel(pos, color);
			}
			this.windTex.ApplyPixels();
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000F8384 File Offset: 0x000F6784
		private void ApplyWindMatrix()
		{
			Vector3 vector;
			(Vector3.one * base.island.fog.maxRad * 2f).y = vector.y * 0.7f;
			Wind.staticWind2World = this.wind2World;
			Wind.staticWorld2Wind = this.world2Wind;
			Shader.SetGlobalMatrix("_Tex2World", Wind.staticWind2World);
			Shader.SetGlobalMatrix("_World2Tex", Wind.staticWorld2Wind);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000F8400 File Offset: 0x000F6800
		public Vector3 GetWindPoint(Vector3 worldPos)
		{
			Vector3 vector = worldPos - this.windOffset;
			vector = ExtraMath.Round(vector);
			int num = Mathf.Clamp((int)vector.x, 0, (int)this.size.x);
			int num2 = Mathf.Clamp((int)vector.y, 0, (int)this.size.y);
			int num3 = Mathf.Clamp((int)vector.z, 0, (int)this.size.z);
			return this.vectors[num, num2, num3] * this.globalWindSpeed;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000F8498 File Offset: 0x000F6898
		public Vector3 GetWindLinear(Vector3 worldPos)
		{
			return this.GetWindDirectionLinear(worldPos) * this.globalWindSpeed;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000F84AC File Offset: 0x000F68AC
		public Vector3 GetWindDirectionLinear(Vector3 worldPos)
		{
			Vector3 result;
			using (new ScopedProfiler("Sample wind", null))
			{
				Vector3 vector = worldPos - this.windOffset;
				Vector3 b = ExtraMath.Floor(vector);
				int num = Mathf.Clamp((int)vector.x, 0, (int)this.size.x - 1);
				int num2 = Mathf.Clamp((int)vector.y, 0, (int)this.size.y - 1);
				int num3 = Mathf.Clamp((int)vector.z, 0, (int)this.size.z - 1);
				int num4 = Mathf.Min(num + 1, (int)this.size.x - 1);
				int num5 = Mathf.Min(num2 + 1, (int)this.size.y - 1);
				int num6 = Mathf.Min(num3 + 1, (int)this.size.z - 1);
				Vector3 b2 = vector - b;
				Vector3 vector2 = Vector3.one - b2;
				Vector3 vector3 = Vector3.zero;
				vector3 += this.vectors[num, num2, num3] * vector2.x * vector2.y * vector2.z;
				vector3 += this.vectors[num4, num2, num3] * b2.x * vector2.y * vector2.z;
				vector3 += this.vectors[num, num5, num3] * vector2.x * b2.y * vector2.z;
				vector3 += this.vectors[num4, num5, num3] * b2.x * b2.y * vector2.z;
				vector3 += this.vectors[num, num2, num6] * vector2.x * vector2.y * b2.z;
				vector3 += this.vectors[num4, num2, num6] * b2.x * vector2.y * b2.z;
				vector3 += this.vectors[num, num5, num6] * vector2.x * b2.y * b2.z;
				vector3 += this.vectors[num4, num5, num6] * b2.x * b2.y * b2.z;
				result = vector3;
			}
			return result;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000F87D4 File Offset: 0x000F6BD4
		[ContextMenu("Force Simulate")]
		private void ForceSimulate()
		{
			IEnumerator enumerator = this.UpdateWind();
			while (enumerator.MoveNext())
			{
			}
			this.ApplyWindTexture();
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000F8800 File Offset: 0x000F6C00
		private Vector3 GetBestWindDir()
		{
			Vector3 result = Vector3.zero;
			float num = float.MinValue;
			foreach (Vector3 vector in Wind.directions)
			{
				float num2 = 0f;
				foreach (House house in base.island.village.houses)
				{
					Vector3 worldTargetPos = house.worldTargetPos;
					Ray ray = new Ray(worldTargetPos, vector);
					RaycastHit raycastHit;
					if (Physics.SphereCast(ray, 0.2f, out raycastHit, 10f, LayerMaster.voxelMask))
					{
						num2 += Mathf.Max(0f, 10f - raycastHit.distance);
					}
				}
				if (num2 > num)
				{
					num = num2;
					result = vector;
				}
			}
			return result;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000F88E0 File Offset: 0x000F6CE0
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			this.windDir = this.GetBestWindDir();
			IEnumerator setup = this.Setup(island);
			while (setup.MoveNext())
			{
				yield return new GenInfo("Creating wind space", GenInfo.Mode.interruptable);
			}
			IEnumerator windStep = this.UpdateWind();
			while (windStep.MoveNext())
			{
				yield return new GenInfo("Simulating Wind", GenInfo.Mode.interruptable);
			}
			this.ApplyWindTexture();
			yield return new GenInfo("Wind", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000F8904 File Offset: 0x000F6D04
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			Shader.SetGlobalTexture("_WindTex", this.windTex.texture);
			Shader.SetGlobalVector("_WindDir", this.windDir);
			this.ApplyWindMatrix();
			Singleton<WorldWind>.instance.Reset();
			base.enabled = true;
			yield return new GenInfo("Wind", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000F8920 File Offset: 0x000F6D20
		IEnumerator<GenInfo> IIslandLeave.OnIslandLeave(Island island)
		{
			base.enabled = false;
			base.StopAllCoroutines();
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000F893C File Offset: 0x000F6D3C
		private void OnDrawGizmos()
		{
			if (this.nodes == null)
			{
				return;
			}
			Gizmos.matrix = this.windOffset.GetMoveMatrix();
			foreach (Wind.Node node in this.nodeList)
			{
				Vector3 lhs = base.island.voxelSpace.GetNormalLinear(Gizmos.matrix.MultiplyPoint(node.pos));
				float magnitude = node.flow.magnitude;
				float t = Vector3.Dot(node.flow, this.windDir);
				float num = Mathf.Lerp(0.14f, 0f, magnitude);
				Gizmos.color = Vector3.one.SetW(Mathf.Lerp(1f, (!(lhs == Vector3.zero)) ? 0.3f : 0f, t));
				Gizmos.DrawRay(node.pos + node.flow.normalized * num, node.flow * 0.5f);
				Gizmos.color *= 2f;
				Gizmos.DrawSphere(node.pos, num);
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000F8AA8 File Offset: 0x000F6EA8
		void IIslandDestroyEntered.OnIslandDestroyEntered(Island island)
		{
			this.windTex.Destroy();
		}

		// Token: 0x0400270A RID: 9994
		private Wind.Node[] nodes;

		// Token: 0x0400270B RID: 9995
		private Vector3[,,] vectors;

		// Token: 0x0400270C RID: 9996
		private List<Wind.Node> nodeList = new List<Wind.Node>();

		// Token: 0x0400270D RID: 9997
		private List<Wind.Link> links = new List<Wind.Link>();

		// Token: 0x0400270E RID: 9998
		private VoxelSpace.CornerVoxel[] referenceArray;

		// Token: 0x0400270F RID: 9999
		public Vector3 windDir = Vector3.forward;

		// Token: 0x04002712 RID: 10002
		public static Matrix4x4 staticWind2World;

		// Token: 0x04002713 RID: 10003
		public static Matrix4x4 staticWorld2Wind;

		// Token: 0x04002714 RID: 10004
		public Fake3dTex windTex;

		// Token: 0x04002715 RID: 10005
		private Vector3 windOffset;

		// Token: 0x04002716 RID: 10006
		private static Vector3[] directions = new Vector3[]
		{
			Vector3.forward,
			Vector3.right,
			Vector3.back,
			Vector3.left
		};

		// Token: 0x02000887 RID: 2183
		private class Pipe
		{
			// Token: 0x06003920 RID: 14624 RVA: 0x000F8B14 File Offset: 0x000F6F14
			public Pipe(Wind.Link link, bool forward)
			{
				this.link = link;
				this.forward = forward;
			}

			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06003921 RID: 14625 RVA: 0x000F8B2A File Offset: 0x000F6F2A
			public Wind.Node node0
			{
				get
				{
					return (!this.forward) ? this.link.node1 : this.link.node0;
				}
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06003922 RID: 14626 RVA: 0x000F8B52 File Offset: 0x000F6F52
			public Wind.Node node1
			{
				get
				{
					return (!this.forward) ? this.link.node0 : this.link.node1;
				}
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06003923 RID: 14627 RVA: 0x000F8B7A File Offset: 0x000F6F7A
			public Vector3 dir
			{
				get
				{
					return (!this.forward) ? (-this.link.dir) : this.link.dir;
				}
			}

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06003924 RID: 14628 RVA: 0x000F8BA7 File Offset: 0x000F6FA7
			public float flow
			{
				get
				{
					return (!this.forward) ? (-this.link.flow) : this.link.flow;
				}
			}

			// Token: 0x04002717 RID: 10007
			private bool forward;

			// Token: 0x04002718 RID: 10008
			public Wind.Link link;
		}

		// Token: 0x02000888 RID: 2184
		private class Link
		{
			// Token: 0x06003925 RID: 14629 RVA: 0x000F8BD0 File Offset: 0x000F6FD0
			public Link(Wind.Node node0, Wind.Node node1, Vector3 diff)
			{
				this.node0 = node0;
				this.node1 = node1;
				node0.pipes.Add(new Wind.Pipe(this, true));
				node1.pipes.Add(new Wind.Pipe(this, false));
				this.diff = diff;
				this.length = diff.magnitude;
				this.dir = diff / this.length;
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x06003926 RID: 14630 RVA: 0x000F8C3B File Offset: 0x000F703B
			public float crossSection
			{
				get
				{
					return 1f - this.coverage;
				}
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x06003927 RID: 14631 RVA: 0x000F8C49 File Offset: 0x000F7049
			public Vector3 pos
			{
				get
				{
					return (this.node0.pos + this.node1.pos) / 2f;
				}
			}

			// Token: 0x04002719 RID: 10009
			public float coverage;

			// Token: 0x0400271A RID: 10010
			public Wind.Node node0;

			// Token: 0x0400271B RID: 10011
			public Wind.Node node1;

			// Token: 0x0400271C RID: 10012
			public Vector3 diff;

			// Token: 0x0400271D RID: 10013
			public Vector3 dir;

			// Token: 0x0400271E RID: 10014
			public float length;

			// Token: 0x0400271F RID: 10015
			public float flow;

			// Token: 0x04002720 RID: 10016
			public bool edge;
		}

		// Token: 0x02000889 RID: 2185
		private class Node
		{
			// Token: 0x06003928 RID: 14632 RVA: 0x000F8C70 File Offset: 0x000F7070
			public Node(Vector3 pos, float coverage)
			{
				this.pos = pos;
				this.coverage = coverage;
				this.amount = 1f - coverage;
			}

			// Token: 0x04002721 RID: 10017
			public float amount;

			// Token: 0x04002722 RID: 10018
			public float delta;

			// Token: 0x04002723 RID: 10019
			public Vector3 flow;

			// Token: 0x04002724 RID: 10020
			public Vector3 pos;

			// Token: 0x04002725 RID: 10021
			public float coverage;

			// Token: 0x04002726 RID: 10022
			public List<Wind.Pipe> pipes = new List<Wind.Pipe>();
		}
	}
}
