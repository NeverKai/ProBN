using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000684 RID: 1668
	[Serializable]
	public class Worry
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x0009887B File Offset: 0x00096C7B
		public Worry(Agent owner)
		{
			this.owner = owner;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00098895 File Offset: 0x00096C95
		public bool valid
		{
			get
			{
				return this.threatComponent && this.threat != null && this.threat.GetTreatValid(this.owner);
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000988C8 File Offset: 0x00096CC8
		public void Update()
		{
			using ("AgentUpdateRangeWorry")
			{
				if (this.valid)
				{
					this.dir = this.threat.GetThreatDir(this.owner);
					this.distance = this.threat.GetThreatDistance(this.owner);
				}
				else
				{
					this.Clear();
				}
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00098948 File Offset: 0x00096D48
		public void Clear()
		{
			this.threat = null;
			this.threatComponent = null;
			this.distance = float.MaxValue;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00098964 File Offset: 0x00096D64
		public void PoseThreat(IThreat newThreat)
		{
			float threatDistance = newThreat.GetThreatDistance(this.owner);
			if (threatDistance < this.distance)
			{
				this.threat = newThreat;
				this.threatComponent = (newThreat as MonoBehaviour);
				this.distance = threatDistance;
			}
		}

		// Token: 0x04001BC3 RID: 7107
		private Agent owner;

		// Token: 0x04001BC4 RID: 7108
		public IThreat threat;

		// Token: 0x04001BC5 RID: 7109
		public MonoBehaviour threatComponent;

		// Token: 0x04001BC6 RID: 7110
		public float distance;

		// Token: 0x04001BC7 RID: 7111
		public Vector3 dir = Vector3.zero;
	}
}
