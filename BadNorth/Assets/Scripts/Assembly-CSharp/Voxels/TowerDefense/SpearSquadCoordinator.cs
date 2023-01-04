using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E6 RID: 2022
	public class SpearSquadCoordinator : SquadCoordinatorAgentTracker<Spear>
	{
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000E2527 File Offset: 0x000E0927
		public List<Spear> spears
		{
			get
			{
				return this.agentComponents;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000E252F File Offset: 0x000E092F
		public NavPos navPos
		{
			get
			{
				return base.enSquad.pather.target.navPos;
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000E2548 File Offset: 0x000E0948
		public override void Setup(Squad squad)
		{
			base.Setup(squad);
			Agent heroAgent = base.enSquad.heroAgent;
			Swordsman component = heroAgent.GetComponent<Swordsman>();
			component.SetSpearSquadCoordinator(this);
			this.states = new AgentState("SpearSquad", squad.rootState, true, false);
			this.spearsDown = new AgentState("SpearsDown", this.states, false, true);
			this.spearsUp = new AgentState("SpearsUp", this.states, true, true);
			AgentState agentState = this.spearsDown;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				Agent agent = null;
				squad.faction.enemy.presence.SampleFullData(this.navPos, ref this.enemyDist, ref this.enemyDir, ref agent);
				this.enemyDir.Normalize();
			}));
			AgentState agentState2 = this.spearsDown;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(delegate(bool x)
			{
				this.enSquad.idealHeroDist = ((!x) ? 0.5f : 4f);
				foreach (Spear spear in this.spears)
				{
					spear.OnSpearsDownChange(x);
				}
			}));
			this.spearsDown.OnUpdate += delegate()
			{
				Agent agent = null;
				squad.faction.enemy.presence.SampleFullData(this.navPos, ref this.enemyDist, ref this.enemyDir, ref agent);
				this.enemyDir.Normalize();
				if (this.enemyDist > 9f)
				{
					this.spearsUp.SetActive(true);
				}
			};
			this.spearsUp.OnUpdate += delegate()
			{
				if (this.isNavSpot)
				{
					this.enemyDist = squad.faction.enemy.presence.SampleDistance(this.navPos);
					if (this.enemyDist < 8f)
					{
						this.spearsDown.SetActive(true);
					}
				}
			};
			this.spearsUp.SetActive(true);
			AgentState agentState3 = this.spearsUp;
			agentState3.OnActivate = (Action)Delegate.Combine(agentState3.OnActivate, new Action(delegate()
			{
				if (this.AnySpearDown())
				{
					IslandGameplayManager.RequestCombatAudio(this.spearsUpCommandSound, this.enSquad.heroAgent.gameObject);
				}
				this.hasTriggeredDown = false;
			}));
			EnglishPatherSquad squadCoordinator = squad.GetSquadCoordinator<EnglishPatherSquad>();
			this.isNavSpot = (squadCoordinator.target is NavSpot);
			squadCoordinator.onPathTargetChanged += delegate(IPathTarget target)
			{
				if (this.AnySpearDown())
				{
					IslandGameplayManager.RequestCombatAudio(this.spearsUpCommandSound, this.enSquad.heroAgent.gameObject);
				}
				this.hasTriggeredDown = false;
				this.isNavSpot = (target is NavSpot);
			};
			this.spearsDownCommandSound = base.enSquad.hero.voice.spearsDownSound;
			this.spearsUpCommandSound = base.enSquad.hero.voice.spearsUpSound;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000E26F4 File Offset: 0x000E0AF4
		protected override void OnAgentComponentAdded(Spear agentComponent)
		{
			base.OnAgentComponentAdded(agentComponent);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000E26FD File Offset: 0x000E0AFD
		public void OnSpearDown()
		{
			if (!this.hasTriggeredDown)
			{
				IslandGameplayManager.RequestCombatAudio(this.spearsDownCommandSound, base.enSquad.heroAgent.gameObject);
				this.hasTriggeredDown = true;
			}
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000E272C File Offset: 0x000E0B2C
		public bool AnySpearDown()
		{
			foreach (Spear spear in this.spears)
			{
				if (spear.spearDown.active)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000E279C File Offset: 0x000E0B9C
		private void OnDrawGizmos()
		{
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			Gizmos.DrawRay(this.navPos.pos, this.enemyDir);
			Gizmos.color *= 2f;
		}

		// Token: 0x040023CA RID: 9162
		public List<Spear> stabQueue = new List<Spear>();

		// Token: 0x040023CB RID: 9163
		public AgentState states;

		// Token: 0x040023CC RID: 9164
		public AgentState spearsUp;

		// Token: 0x040023CD RID: 9165
		public AgentState spearsDown;

		// Token: 0x040023CE RID: 9166
		private bool isNavSpot = true;

		// Token: 0x040023CF RID: 9167
		public Vector3 enemyDir;

		// Token: 0x040023D0 RID: 9168
		public float enemyDist;

		// Token: 0x040023D1 RID: 9169
		private bool hasTriggeredDown;

		// Token: 0x040023D2 RID: 9170
		private FabricEventReference spearsDownCommandSound;

		// Token: 0x040023D3 RID: 9171
		private FabricEventReference spearsUpCommandSound;
	}
}
