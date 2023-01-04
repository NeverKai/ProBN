using System;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000843 RID: 2115
	public class ArcheryFocusComponent : ChildComponent<Archery>
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000EDFB8 File Offset: 0x000EC3B8
		public bool done
		{
			get
			{
				return this.settings.ammo <= 0;
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000EDFCC File Offset: 0x000EC3CC
		public void MaybeSetup()
		{
			if (this.focus == null)
			{
				this.focus = new AgentState("FocusFire", base.manager.aiming, false, true);
				AgentState agentState = this.focus;
				agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
				{
					this.timer = Time.time + 0.5f;
				}));
				this.focus.OnUpdate += delegate()
				{
					if (!base.manager.AimAt(this.settings.trajectoryCalculator, this.targetPos, Vector3.zero))
					{
						this.focus.SetActive(false);
					}
					if (Time.time > this.timer)
					{
						Arrow arrow = base.manager.Shoot(base.manager.aimDir, this.settings.trajectoryCalculator.settings) as Arrow;
						this.focusAbility.ModifyArrow(arrow);
						if ((this.settings.ammo = this.settings.ammo - 1) > 0)
						{
							this.targetPos += this.targetDelta;
							this.timer += 0.2f;
						}
						else
						{
							this.focus.SetActive(false);
						}
					}
				};
				AgentState agentState2 = this.focus;
				agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
				{
					this.focusAbility = null;
					this.settings = default(ArcheryFocusComponent.Settings);
				}));
			}
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000EE068 File Offset: 0x000EC468
		public void ShootAt(ArcheryFocusAbility focusAbility, ArcheryFocusComponent.Settings settings, Vector3 targetPos, Vector3 targetDelta)
		{
			if (!base.manager.brainState.active)
			{
				return;
			}
			this.settings = settings;
			this.targetPos = targetPos;
			this.targetDelta = targetDelta;
			this.focusAbility = focusAbility;
			this.focus.SetActive(true);
		}

		// Token: 0x0400258D RID: 9613
		private ArcheryFocusComponent.Settings settings;

		// Token: 0x0400258E RID: 9614
		private float timer;

		// Token: 0x0400258F RID: 9615
		private Vector3 targetPos;

		// Token: 0x04002590 RID: 9616
		private Vector3 targetDelta;

		// Token: 0x04002591 RID: 9617
		[NonSerialized]
		public float order;

		// Token: 0x04002592 RID: 9618
		private ArcheryFocusAbility focusAbility;

		// Token: 0x04002593 RID: 9619
		public AgentState focus;

		// Token: 0x02000844 RID: 2116
		[Serializable]
		public struct Settings
		{
			// Token: 0x04002594 RID: 9620
			public TrajectoryUtility trajectoryCalculator;

			// Token: 0x04002595 RID: 9621
			public int ammo;

			// Token: 0x04002596 RID: 9622
			public AttackSettings attackSettings;
		}
	}
}
