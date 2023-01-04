using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007ED RID: 2029
	public class SquadProximity : SquadComponent
	{
		// Token: 0x060034DB RID: 13531 RVA: 0x000E3564 File Offset: 0x000E1964
		private void Update()
		{
			this.checkIndex++;
			if (this.checkIndex >= Squad.allLiving.Count)
			{
				this.checkIndex = 0;
			}
			Squad squad = Squad.allLiving[this.checkIndex];
			for (int i = this.threats.Count - 1; i >= 0; i--)
			{
				if (this.threats[i] == null)
				{
					this.threats.RemoveAt(i);
				}
			}
			if (squad.faction != base.squad.faction)
			{
				bool flag = false;
				if (base.squad.bounds.Intersects(squad.bounds))
				{
					flag = true;
				}
				else
				{
					Vector3 a = base.squad.bounds.ClosestPoint(squad.bounds.center);
					Vector3 b = squad.bounds.ClosestPoint(base.squad.bounds.center);
					if (Vector3.SqrMagnitude(a - b) < this.radius * this.radius)
					{
						flag = true;
					}
				}
				if (flag && !this.threats.Contains(squad))
				{
					this.threats.Add(squad);
				}
				if (!flag && this.threats.Contains(squad))
				{
					this.threats.Remove(squad);
				}
			}
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000E36CD File Offset: 0x000E1ACD
		private void OnDestroy()
		{
			this.threats.Clear();
			this.threats = null;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000E36E4 File Offset: 0x000E1AE4
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			for (int i = 0; i < this.threats.Count; i++)
			{
				Squad squad = this.threats[i];
				Gizmos.DrawLine(base.squad.bounds.center, squad.bounds.center);
			}
		}

		// Token: 0x040023F5 RID: 9205
		public List<Squad> threats;

		// Token: 0x040023F6 RID: 9206
		private int checkIndex;

		// Token: 0x040023F7 RID: 9207
		private float radius = 3f;
	}
}
