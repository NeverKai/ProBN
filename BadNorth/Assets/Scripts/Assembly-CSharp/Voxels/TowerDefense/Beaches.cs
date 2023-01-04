using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000651 RID: 1617
	public class Beaches : IslandComponent, IIslandProcessor
	{
		// Token: 0x06002904 RID: 10500 RVA: 0x0008CEEC File Offset: 0x0008B2EC
		public static bool RadiusCheck(NavPos navPos, Vector3 dir, float radius = 0.1f)
		{
			float num = 100f;
			dir = dir.GetZeroY().normalized;
			Ray ray = new Ray(navPos.pos + dir * num, -dir);
			RaycastHit raycastHit;
			bool flag = Physics.SphereCast(ray, radius, out raycastHit, num - radius - 0.2f, LayerMaster.moduleMask);
			return !flag;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0008CF54 File Offset: 0x0008B354
		public List<Beaches.Beach.Pos> GetBeachPositions(float spacing = 0.2f)
		{
			return (from x in this.beaches
			where !x.puddle
			select x).SelectMany((Beaches.Beach x) => x.GetBeachPosList(spacing)).ToList<Beaches.Beach.Pos>();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0008CFAC File Offset: 0x0008B3AC
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			Vert[] verts = island.navMesh.verts;
			List<Vert> vertPool = new List<Vert>();
			foreach (Vert vert in verts)
			{
				if (vert.border)
				{
					for (int j = 0; j < vert.pipes.Count; j++)
					{
						Pipe pipe = vert.pipes[j];
						if (pipe.edge.beach)
						{
							vertPool.Add(vert);
							break;
						}
					}
					yield return new GenInfo("Constructing beach", GenInfo.Mode.interruptable);
				}
			}
			while (vertPool.Count > 0)
			{
				Beaches.Beach beach = new Beaches.Beach(vertPool[0], ref vertPool);
				this.potentialLength += beach.length;
				this.beaches.Add(beach);
				yield return new GenInfo("Constructing beach", GenInfo.Mode.interruptable);
			}
			if (this.potentialLength < island.levelNode.minimumBeach)
			{
				yield return new GenInfo("Not enough potential beach!", GenInfo.Mode.broken);
			}
			float spacing = 0.5f;
			List<Beaches.Beach.Pos> beachPoints = this.GetBeachPositions(spacing);
			this.beachLength = 0f;
			foreach (Beaches.Beach.Pos beachPos in beachPoints)
			{
				if (Beaches.RadiusCheck(beachPos.navPos, -beachPos.navPos.GetCliffVector().GetZeroY(), 0.1f))
				{
					this.beachLength += spacing;
				}
				yield return new GenInfo("Validating beach", GenInfo.Mode.interruptable);
			}
			if (this.beachLength < island.levelNode.minimumBeach)
			{
				yield return new GenInfo("Not enough beach!", GenInfo.Mode.broken);
			}
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0008CFD0 File Offset: 0x0008B3D0
		private void OnDrawGizmos()
		{
			foreach (Beaches.Beach beach in this.beaches)
			{
				Gizmos.color = beach.color;
				foreach (Edge edge in beach.edges)
				{
					Gizmos.DrawLine(edge.verts[0].pos, edge.verts[1].pos);
				}
			}
		}

		// Token: 0x04001AB1 RID: 6833
		public List<Beaches.Beach> beaches = new List<Beaches.Beach>();

		// Token: 0x04001AB2 RID: 6834
		public float beachLength;

		// Token: 0x04001AB3 RID: 6835
		public float potentialLength;

		// Token: 0x02000652 RID: 1618
		[Serializable]
		public class Beach
		{
			// Token: 0x06002909 RID: 10505 RVA: 0x0008D0A8 File Offset: 0x0008B4A8
			public Beach(Vert startVert, ref List<Vert> vertPool)
			{
				this.verts.Add(startVert);
				vertPool.Remove(startVert);
				this.GetNextVert(startVert, true, ref vertPool);
				if (!this.loop)
				{
					this.GetNextVert(startVert, false, ref vertPool);
				}
				this.color = Color.HSVToRGB(UnityEngine.Random.value, 1f, 1f);
				this.puddle = this.CheckIfPuddle();
				if (this.puddle)
				{
					this.color = Color.black;
				}
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x0008D140 File Offset: 0x0008B540
			private bool CheckIfPuddle()
			{
				if (!this.loop)
				{
					return false;
				}
				float num = 0f;
				for (int i = 0; i < this.edges.Count; i++)
				{
					Edge edge = this.edges[i];
					Edge edge2 = this.edges[(i + 1) % this.edges.Count];
					float num2 = Vector2.Angle(edge.dir.GetXZ(), edge2.dir.GetXZ());
					num2 *= Mathf.Sign(Vector3.Dot(edge.dir, edge2.borderVector));
					num += num2;
				}
				return num > 0f;
			}

			// Token: 0x0600290B RID: 10507 RVA: 0x0008D1EC File Offset: 0x0008B5EC
			private void GetNextVert(Vert vert, bool forward, ref List<Vert> pool)
			{
				for (int i = 0; i < vert.pipes.Count; i++)
				{
					Pipe pipe = vert.pipes[i];
					if (pipe.edge.beach && pipe.forward == forward)
					{
						if (pool.Remove(pipe.outVert))
						{
							if (forward)
							{
								this.verts.Add(pipe.outVert);
								this.edges.Add(pipe.edge);
							}
							else
							{
								this.verts.Insert(0, pipe.outVert);
								this.edges.Insert(0, pipe.edge);
							}
							this.length += pipe.edge.length;
							this.GetNextVert(pipe.outVert, forward, ref pool);
						}
						else
						{
							this.loop = true;
						}
					}
				}
			}

			// Token: 0x0600290C RID: 10508 RVA: 0x0008D2D4 File Offset: 0x0008B6D4
			public NavPos GetNavPos(float distance)
			{
				if (distance < 0f)
				{
					return new NavPos(this.verts[0]);
				}
				for (int i = 0; i < this.edges.Count; i++)
				{
					Edge edge = this.edges[i];
					if (distance < edge.length)
					{
						return new NavPos(edge.verts[0])
						{
							pos = Vector3.Lerp(edge.verts[0].pos, edge.verts[1].pos, distance / edge.length)
						};
					}
					distance -= this.edges[i].length;
				}
				return new NavPos(this.verts[this.verts.Count - 1]);
			}

			// Token: 0x0600290D RID: 10509 RVA: 0x0008D3B0 File Offset: 0x0008B7B0
			public List<Beaches.Beach.Pos> GetBeachPosList(float spacing = 0.2f)
			{
				List<Beaches.Beach.Pos> list = new List<Beaches.Beach.Pos>();
				int num = 0;
				spacing += this.length % spacing / (this.length / spacing);
				float num2 = 0f;
				float num3 = num2;
				Edge edge = this.edges[num];
				NavPos navPos = new NavPos(edge);
				while (num3 < this.length)
				{
					while (num2 > edge.length)
					{
						num2 -= edge.length;
						num++;
						if (num == this.edges.Count)
						{
							return list;
						}
						edge = this.edges[num];
						navPos = new NavPos(edge);
					}
					navPos.pos = Vector3.Lerp(edge.verts[0].pos, edge.verts[1].pos, num2 / edge.length);
					list.Add(new Beaches.Beach.Pos(navPos, num3, this.length - num3, this));
					num2 += spacing;
					num3 += spacing;
				}
				return list;
			}

			// Token: 0x04001AB5 RID: 6837
			public List<Vert> verts = new List<Vert>();

			// Token: 0x04001AB6 RID: 6838
			public List<Edge> edges = new List<Edge>();

			// Token: 0x04001AB7 RID: 6839
			public float length;

			// Token: 0x04001AB8 RID: 6840
			public Color color;

			// Token: 0x04001AB9 RID: 6841
			public bool loop;

			// Token: 0x04001ABA RID: 6842
			public bool puddle;

			// Token: 0x02000653 RID: 1619
			public struct Pos
			{
				// Token: 0x0600290E RID: 10510 RVA: 0x0008D4B0 File Offset: 0x0008B8B0
				public Pos(NavPos navPos, float distance0, float distance1, Beaches.Beach beach)
				{
					this.navPos = navPos;
					this.distToEdge = ((!beach.loop) ? Mathf.Min(distance0, distance1) : beach.length);
					this.randomOffset = ExtraMath.RemapValue(navPos.island.village.SampleDistanceToHouse(navPos), 1f, 2f, 1f, 0f);
					this.beach = beach;
					this.dir = -navPos.GetCliffVector();
					this.cost = 0f;
				}

				// Token: 0x04001ABB RID: 6843
				public NavPos navPos;

				// Token: 0x04001ABC RID: 6844
				public Vector3 dir;

				// Token: 0x04001ABD RID: 6845
				public float distToEdge;

				// Token: 0x04001ABE RID: 6846
				public float randomOffset;

				// Token: 0x04001ABF RID: 6847
				public float cost;

				// Token: 0x04001AC0 RID: 6848
				public Beaches.Beach beach;
			}
		}
	}
}
