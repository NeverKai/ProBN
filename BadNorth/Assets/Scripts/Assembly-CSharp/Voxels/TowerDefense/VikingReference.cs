using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration.CampaignAc3;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000726 RID: 1830
	public class VikingReference : LevelAssigner, IGameSetup
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000C0BFC File Offset: 0x000BEFFC
		public Agent agent
		{
			get
			{
				return this.vikingClone.agent;
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000C0C0C File Offset: 0x000BF00C
		protected override IEnumerable<LevelObjectReference> GetAssignments()
		{
			yield return new LevelObjectReference(this.key, base.name);
			yield break;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000C0C2F File Offset: 0x000BF02F
		private void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x000C0C44 File Offset: 0x000BF044
		public bool seen
		{
			get
			{
				return Profile.campaign.HasEverSeen(this.type);
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000C0C58 File Offset: 0x000BF058
		private void Start()
		{
			GameObject gameObject = base.gameObject.AddEmptyChild("Container");
			gameObject.SetActive(false);
			this.vikingClone = UnityEngine.Object.Instantiate<VikingAgent>(this.viking, gameObject.transform);
			this.vikingClone.agent.UpdateVisuals();
			this.sprite2 = this.agent.GetComponentInChildren<SpriteAnimator>(true).sprite2;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000C0CBB File Offset: 0x000BF0BB
		public void Saw()
		{
			Profile.campaign.Saw(this.type);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000C0CCD File Offset: 0x000BF0CD
		void IGameSetup.OnGameAwake()
		{
			LevelStateObjectReferences.AddToDict(this);
		}

		// Token: 0x04001FAB RID: 8107
		[SerializeField]
		private VikingAgent viking;

		// Token: 0x04001FAC RID: 8108
		public VikingAgent vikingClone;

		// Token: 0x04001FAD RID: 8109
		public VikingAgent.Type type;

		// Token: 0x04001FAE RID: 8110
		public int bounty;

		// Token: 0x04001FAF RID: 8111
		public Sprite sprite2;

		// Token: 0x04001FB0 RID: 8112
		public FabricEventReference approachAudioId = "Mus/VikingSwordWave";

		// Token: 0x04001FB1 RID: 8113
		public FabricEventReference arriveAudioId = "Mus/VikingSwordSting";

		// Token: 0x04001FB2 RID: 8114
		private bool initialized;

		// Token: 0x04001FB3 RID: 8115
		[SerializeField]
		private float bottom = 0.2f;

		// Token: 0x04001FB4 RID: 8116
		[SerializeField]
		private float top = 0.3f;

		// Token: 0x04001FB5 RID: 8117
		[SerializeField]
		private Vector3 offset;

		// Token: 0x04001FB6 RID: 8118
		[SerializeField]
		private Material material;
	}
}
