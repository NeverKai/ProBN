using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x020007B1 RID: 1969
	public class VikingPatherSquad : SquadCoordinatorAgentTracker<Arsonist>
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000D9C75 File Offset: 0x000D8075
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x000D9C7D File Offset: 0x000D807D
		public House house { get; private set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000D9C86 File Offset: 0x000D8086
		public Village village
		{
			get
			{
				return base.squad.faction.island.village;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000D9C9D File Offset: 0x000D809D
		public VikingPatherSquad.State state
		{
			get
			{
				return (!this.house) ? VikingPatherSquad.State.Waiting : VikingPatherSquad.State.HouseBurning;
			}
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000D9CB6 File Offset: 0x000D80B6
		public bool WantsControl()
		{
			return !this.done;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000D9CC1 File Offset: 0x000D80C1
		private void Start()
		{
			if (this.village.houseCount > 0)
			{
				base.StartCoroutine(this.HouseBurning());
			}
			else
			{
				this.done = true;
			}
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000D9CED File Offset: 0x000D80ED
		protected override void OnAgentComponentAdded(Arsonist agentComponent)
		{
			agentComponent.pather = this;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000D9CF6 File Offset: 0x000D80F6
		public void OnDrawGizmosSelected()
		{
			if (this.house)
			{
				this.house.distanceField.DrawGizmos();
			}
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000D9D18 File Offset: 0x000D8118
		private IEnumerator HouseBurning()
		{
			float t = 0f;
			while ((double)t < 0.3)
			{
				yield return null;
				t += Time.deltaTime;
			}
			base.StartCoroutine(this.UpdateWalkSpeeds());
			while (!base.squad.GetAverageNavPos().island)
			{
				if (base.squad.agents.Any((Agent x) => x.rangeWorry.valid))
				{
					break;
				}
				float t2 = 0f;
				while ((double)t2 < 0.3)
				{
					yield return null;
					t2 += Time.deltaTime;
				}
			}
			while (this.village.houseCount > 0)
			{
				this.house = this.GetNewHouse();
				if (this.house)
				{
					while (!this.house.destroyed)
					{
						yield return null;
					}
					this.house = null;
				}
				float t3 = 0f;
				while ((double)t3 < 0.3)
				{
					yield return null;
					t3 += Time.deltaTime;
				}
			}
			float t4 = 0f;
			while ((double)t4 < 0.3)
			{
				yield return null;
				t4 += Time.deltaTime;
			}
			this.done = true;
			yield return null;
			yield break;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000D9D34 File Offset: 0x000D8134
		private House GetNewHouse()
		{
			NavPos medianPos = base.squad.GetAverageNavPos();
			return (from x in this.village.houses
			where !x.destroyed
			orderby this.HouseCost(x, medianPos)
			select x).FirstOrDefault<House>();
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000D9DA4 File Offset: 0x000D81A4
		private float HouseCost(House house, NavPos navPos)
		{
			float num = house.distanceField.SampleDistance(navPos);
			num *= UnityEngine.Random.Range(0.7f, 1f);
			if (this.HouseBlocked(house, navPos))
			{
				num = num * 2f + 2f;
			}
			return num;
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000D9DEC File Offset: 0x000D81EC
		public void SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			VikingPatherSquad.State state = this.state;
			if (state != VikingPatherSquad.State.Waiting)
			{
				if (state != VikingPatherSquad.State.HouseBurning)
				{
					dir = Vector3.zero;
					dist = 100f;
				}
				else if (this.house && navPos.island)
				{
					this.house.distanceField.Sample(navPos, ref dir, ref dist);
					float num = ExtraMath.RemapValue(Mathf.Abs(navPos.pos.y - this.house.transform.position.y), 0.2f, 0f);
					dist = Mathf.Max(0f, dist - num);
					dir = Vector3.ClampMagnitude(dir, dist);
				}
				else
				{
					dir = Vector3.zero;
					dist = 100f;
				}
			}
			else
			{
				dir = Vector3.ClampMagnitude(navPos.GetBorderVector(), ExtraMath.RemapValue(navPos.GetBorderDistance(), 0.2f, 0f));
				dist = 100f;
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000D9F10 File Offset: 0x000D8310
		private bool HouseBlocked(House house, NavPos navPos)
		{
			FlowField presence = base.squad.faction.enemy.presence;
			Vector3 lhs = house.distanceField.SampleDirection(navPos);
			Vector3 rhs = presence.SampleDirection(navPos);
			if (Vector3.Dot(lhs, rhs) < 0f)
			{
				return false;
			}
			Data data = presence.SampleData(navPos);
			if (!data.dangerous || !data.navPos.valid)
			{
				return false;
			}
			float num = house.distanceField.SampleDistance(data.navPos);
			float num2 = house.distanceField.SampleDistance(navPos);
			return num <= num2;
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000D9FB0 File Offset: 0x000D83B0
		private IEnumerator UpdateWalkSpeeds()
		{
			for (;;)
			{
				for (int i = 0; i < this.agentComponents.Count; i++)
				{
					Arsonist arsonist = this.agentComponents[i];
					if (arsonist.agent.navPos.island)
					{
						int num = 0;
						float num2 = 0f;
						float orderDist = arsonist.agent.orderDist;
						for (int j = 0; j < this.agentComponents.Count; j++)
						{
							if (j != i)
							{
								Arsonist arsonist2 = this.agentComponents[j];
								if (arsonist2.agent.navPos.island)
								{
									float num3 = Vector3.SqrMagnitude(arsonist2.agent.navPos.pos - arsonist.agent.navPos.pos);
									if (num3 <= 0.25f)
									{
										float orderDist2 = arsonist2.agent.orderDist;
										if (Mathf.Abs(orderDist2 - orderDist) <= 0.5f)
										{
											num2 += orderDist2;
											num++;
										}
									}
								}
							}
						}
						if (num > 0)
						{
							num2 /= (float)num;
							arsonist.walkSpeed = ExtraMath.RemapValue(orderDist - num2, -1f, 1f, 0.8f, 1.2f);
						}
						else
						{
							arsonist.walkSpeed = 1f;
						}
					}
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x040022A4 RID: 8868
		private bool done;

		// Token: 0x020007B2 RID: 1970
		public enum State
		{
			// Token: 0x040022A7 RID: 8871
			Waiting,
			// Token: 0x040022A8 RID: 8872
			HouseBurning
		}
	}
}
