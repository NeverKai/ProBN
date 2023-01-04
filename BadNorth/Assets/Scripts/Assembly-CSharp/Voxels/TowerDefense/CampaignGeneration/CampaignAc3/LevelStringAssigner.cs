using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x020006FF RID: 1791
	[RequireComponent(typeof(DomainBool))]
	public class LevelStringAssigner : LevelAssigner
	{
		// Token: 0x06002E65 RID: 11877 RVA: 0x000B4E50 File Offset: 0x000B3250
		protected override IEnumerable<LevelObjectReference> GetAssignments()
		{
			yield return new LevelObjectReference(this.key, this.toAssign);
			yield break;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000B4E73 File Offset: 0x000B3273
		[ContextMenu("Set name to string")]
		private void SetNameToString()
		{
			base.name = this.toAssign;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000B4E81 File Offset: 0x000B3281
		[ContextMenu("Set string to name")]
		private void SetStringToName()
		{
			this.toAssign = base.name;
		}

		// Token: 0x04001EAC RID: 7852
		[SerializeField]
		private string toAssign;
	}
}
