using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x020006FD RID: 1789
	[RequireComponent(typeof(DomainBool))]
	public class LevelObjectAssigner : LevelAssigner, IGameSetup
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x000B4B70 File Offset: 0x000B2F70
		protected override IEnumerable<LevelObjectReference> GetAssignments()
		{
			yield return new LevelObjectReference(this.key, this.objectReference.name);
			yield break;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000B4B93 File Offset: 0x000B2F93
		[ContextMenu("Name after reference")]
		private void NameObjectAfterReference()
		{
			if (this.objectReference)
			{
				base.name = this.objectReference.name;
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000B4BB6 File Offset: 0x000B2FB6
		[ContextMenu("Set reference to self")]
		private void SetReferenceToSelf()
		{
			this.objectReference = base.gameObject;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000B4BC4 File Offset: 0x000B2FC4
		void IGameSetup.OnGameAwake()
		{
			LevelStateObjectReferences.AddToDict(this.objectReference);
		}

		// Token: 0x04001EAA RID: 7850
		[SerializeField]
		private UnityEngine.Object objectReference;
	}
}
