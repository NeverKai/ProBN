using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000854 RID: 2132
	internal class MineLayerComponent : AgentComponent
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000F0869 File Offset: 0x000EEC69
		private DistanceField distanceField
		{
			get
			{
				return (!this.navSpot) ? null : this.navSpot.distanceField;
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000F088C File Offset: 0x000EEC8C
		public override void Setup()
		{
			base.Setup();
			this.mineLaying = new AgentState("MineLaying", base.agent.exclusives, false, true);
			this.mineLaying.OnUpdate += delegate()
			{
				Vector3 zero = Vector3.zero;
				float num = 10f;
				this.navSpot.distanceField.Sample(base.agent.navPos, ref zero, ref num);
				if (num < 0.1f)
				{
					this.Place(this.targetPos);
					this.mineLaying.SetActive(false);
				}
				else if (this.mineLaying.timeSinceActivation > 2f)
				{
					this.targetPos.pos = Vector3.MoveTowards(this.proximityMine.transform.position, this.targetPos.pos, 0.5f);
					this.Place(this.targetPos);
					base.agent.body.StopStep();
					this.mineLaying.SetActive(false);
				}
				else
				{
					base.agent.walkDir = zero;
					base.agent.LookInDirection(zero, 720f, 20f);
					base.agent.movability = 0.1f;
					base.agent.speed = base.agent.maxSpeed * 0.6f;
				}
			};
			AgentState agentState = this.mineLaying;
			agentState.OnDeactivate = (Action)Delegate.Combine(agentState.OnDeactivate, new Action(delegate()
			{
				if (this.proximityMine)
				{
					this.targetPos.pos = Vector3.MoveTowards(this.proximityMine.transform.position, this.targetPos.pos, 0.2f);
					this.Place(this.targetPos);
				}
				if (this.onEnded != null)
				{
					this.onEnded();
				}
				this.onEnded = null;
			}));
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000F08FC File Offset: 0x000EECFC
		public void StartPlacing(ProximityMine proximityMine, NavSpot navSpot, Action onEnded, FabricEventReference audioOverride = null)
		{
			this.proximityMine = proximityMine;
			this.navSpot = navSpot;
			this.onEnded = onEnded;
			this.audioOverride = audioOverride;
			this.targetPos = navSpot.navPos;
			this.targetPos.pos = this.targetPos.pos + UnityEngine.Random.insideUnitSphere * 0.1f;
			proximityMine.transform.SetParent(this.attachmentTransform);
			proximityMine.transform.localPosition = new Vector3(0f, -0.05f, 0.04f);
			proximityMine.transform.localRotation = proximityMine.defaultRotation;
			this.mineLaying.SetActive(true);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000F09A8 File Offset: 0x000EEDA8
		private void Place(NavPos navPos)
		{
			if (this.proximityMine)
			{
				this.proximityMine.Throw(navPos);
				this.proximityMine = null;
				this.mineLaying.SetActive(false);
				IslandGameplayManager.RequestCombatAudio((this.audioOverride == null) ? this.defaultPlaceSound : this.audioOverride, base.agent.gameObject);
				this.audioOverride = null;
			}
		}

		// Token: 0x040025F2 RID: 9714
		private static int placeAnimId = Animator.StringToHash("Throw");

		// Token: 0x040025F3 RID: 9715
		[SerializeField]
		private Transform attachmentTransform;

		// Token: 0x040025F4 RID: 9716
		private FabricEventReference defaultPlaceSound = "Sfx/Torch/Throw";

		// Token: 0x040025F5 RID: 9717
		private FabricEventReference audioOverride;

		// Token: 0x040025F6 RID: 9718
		private const float tricastRange = 0.4f;

		// Token: 0x040025F7 RID: 9719
		private const float placeRange = 0.1f;

		// Token: 0x040025F8 RID: 9720
		private NavSpot navSpot;

		// Token: 0x040025F9 RID: 9721
		private ProximityMine proximityMine;

		// Token: 0x040025FA RID: 9722
		private NavPos targetPos;

		// Token: 0x040025FB RID: 9723
		private AgentState mineLaying;

		// Token: 0x040025FC RID: 9724
		private Action onEnded;
	}
}
