using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x020006FE RID: 1790
	[RequireComponent(typeof(DomainBool))]
	public class LevelObjectsAssigner : LevelAssigner, IGameSetup
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x000B4CC0 File Offset: 0x000B30C0
		protected override IEnumerable<LevelObjectReference> GetAssignments()
		{
			foreach (UnityEngine.Object o in this.objectReferences)
			{
				yield return new LevelObjectReference(this.key, o.name);
			}
			yield break;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000B4CE4 File Offset: 0x000B30E4
		void IGameSetup.OnGameAwake()
		{
			foreach (UnityEngine.Object value in this.objectReferences)
			{
				LevelStateObjectReferences.AddToDict(value);
			}
		}

		// Token: 0x04001EAB RID: 7851
		[SerializeField]
		private UnityEngine.Object[] objectReferences;
	}
}
