using System.Collections;
using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.Flag
{
	// Token: 0x02000515 RID: 1301
	public class FlagPhysics : AgentComponent
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x0005FCCD File Offset: 0x0005E0CD
		private void Awake()
		{
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0005FCCF File Offset: 0x0005E0CF
		private void OnValidate()
		{
			this.nodes.Clear();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0005FCDC File Offset: 0x0005E0DC
		public override void Setup()
		{
			this.wind = base.agent.faction.island.wind;
			this.updateEnumerator = this.UpdateEnumerator();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0005FD0C File Offset: 0x0005E10C
		public void DropNode()
		{
			this.nodes.Insert(0, new FlagPhysics.Node(base.transform));
			float num = 0f;
			for (int i = 0; i < this.nodes.Count - 1; i++)
			{
				float num2 = Vector3.Distance(this.nodes[i].pos, this.nodes[i + 1].pos);
				num += num2;
				if (num > this.maxLength)
				{
					this.nodes.RemoveRange(i + 2, this.nodes.Count - (i + 2));
					break;
				}
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0005FDB7 File Offset: 0x0005E1B7
		private void Update()
		{
			this.updateEnumerator.MoveNext();
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0005FDC8 File Offset: 0x0005E1C8
		private IEnumerator<object> UpdateEnumerator()
		{
			for (;;)
			{
				if (this.nodes.Count == 0)
				{
					this.nodes.Add(new FlagPhysics.Node(base.transform));
				}
				Vector3 cameraSide = Singleton<LevelCamera>.instance.transform.right;
				if ((this.nodes[0].pos - base.transform.position).sqrMagnitude > 0.040000003f)
				{
					this.DropNode();
				}
				House[] houses = base.agent.faction.island.village.houses;
				House closestHouse = houses[0];
				float closestHouseDistance = float.MaxValue;
				foreach (House house in houses)
				{
					float sqrMagnitude = (house.transform.position - base.transform.position).sqrMagnitude;
					if (sqrMagnitude < closestHouseDistance)
					{
						closestHouseDistance = sqrMagnitude;
						closestHouse = house;
					}
				}
				Bounds houseBounds = closestHouse.boxCollider.bounds;
				houseBounds.extents += new Vector3(0.2f, 0.3f, 0.2f);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					FlagPhysics.Node node = this.nodes[i];
					Vector3 normWindDir = this.wind.Target.GetWindDirectionLinear(node.pos);
					Vector3 windDir = normWindDir.GetMinMagnitude(this.minWindSpeed);
					node.velocity = windDir;
					float windSpeed = windDir.magnitude;
					node.velocity += cameraSide * Mathf.Sign(Vector3.Dot(windDir, cameraSide)) * windSpeed * 0.4f;
					node.velocity += new Vector3(windDir.z, 0f, -windDir.x) * node.noise * 0.5f;
					Vector3 normal;
					float openness;
					this.wind.Target.island.voxelSpace.SampleNormalOpenness(node.pos, out normal, out openness);
					float coverage = 1f - openness;
					node.velocity = ExtraMath.ClampVectorToDirection(node.velocity, normal, coverage);
					node.velocity.y = 0f;
					Bounds b = new Bounds(houseBounds.center - normWindDir * 0.1f, houseBounds.size + normWindDir * 0.1f);
					if (b.Contains(node.pos))
					{
						Vector3 zeroY = (b.center - node.pos).GetZeroY();
						if (Vector3.Dot(zeroY, node.velocity) > 0f)
						{
							float num = 1f - Mathf.Max(Mathf.Abs(zeroY.x / houseBounds.extents.x), Mathf.Abs(zeroY.z / houseBounds.extents.z));
							num = Mathf.Clamp01(num * 2f);
							Vector3 b2 = (Mathf.Abs(windDir.x) >= Mathf.Abs(windDir.z)) ? (new Vector3(Mathf.Sign(-zeroY.x) * (0.5f + node.noise) * 0.3f, 0f, Mathf.Sign(node.velocity.z)) * windSpeed) : (new Vector3(Mathf.Sign(node.velocity.x), 0f, Mathf.Sign(-zeroY.z) * (0.5f + node.noise) * 0.3f) * windSpeed);
							node.velocity = Vector3.Lerp(node.velocity, b2, num);
						}
					}
					this.nodes[i] = node;
					for (int k = 0; k < this.nodes.Count; k++)
					{
						FlagPhysics.Node value = this.nodes[k];
						value.pos += value.velocity * Time.deltaTime;
						this.nodes[k] = value;
					}
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0005FDE4 File Offset: 0x0005E1E4
		public void OnDrawGizmos()
		{
			if (this.nodes.Count > 0)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(base.transform.position, this.nodes[0].pos);
				for (int i = 0; i < this.nodes.Count - 1; i++)
				{
					Gizmos.DrawLine(this.nodes[i].pos, this.nodes[i + 1].pos);
					Gizmos.DrawRay(this.nodes[i].pos, Vector3.up * 0.1f);
				}
			}
		}

		// Token: 0x0400149B RID: 5275
		public float maxLength = 1f;

		// Token: 0x0400149C RID: 5276
		public List<FlagPhysics.Node> nodes = new List<FlagPhysics.Node>();

		// Token: 0x0400149D RID: 5277
		public float minWindSpeed = 0.5f;

		// Token: 0x0400149E RID: 5278
		private IEnumerator updateEnumerator;

		// Token: 0x0400149F RID: 5279
		private WeakReference<Wind> wind;

		// Token: 0x02000516 RID: 1302
		public struct Node
		{
			// Token: 0x060021DD RID: 8669 RVA: 0x0005FEA8 File Offset: 0x0005E2A8
			public Node(Transform transform)
			{
				this.pos = transform.position;
				this.up = transform.up;
				this.velocity = Vector3.zero;
				this.noise = UnityEngine.Random.Range(-1f, 1f);
				this.creationTime = Time.time;
			}

			// Token: 0x040014A0 RID: 5280
			public Vector3 up;

			// Token: 0x040014A1 RID: 5281
			public Vector3 pos;

			// Token: 0x040014A2 RID: 5282
			public Vector3 velocity;

			// Token: 0x040014A3 RID: 5283
			public float noise;

			// Token: 0x040014A4 RID: 5284
			public float creationTime;
		}
	}
}
