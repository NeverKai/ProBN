using System;
using System.Collections;
using System.Diagnostics;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x02000753 RID: 1875
	[ConsoleCommandClassCustomizer("Squad")]
	public class EnglishSquad : Squad
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000C98DD File Offset: 0x000C7CDD
		public Agent minionPrefab
		{
			get
			{
				return this.hero.monoHero.minionPrefab;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000C98EF File Offset: 0x000C7CEF
		public Agent heroAgent
		{
			get
			{
				return (!this.standard) ? null : this.standard.agent;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000C9912 File Offset: 0x000C7D12
		public bool heroAlive
		{
			get
			{
				return this.heroAgent && this.heroAgent.aliveState != null && this.heroAgent.aliveState.active;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000C9947 File Offset: 0x000C7D47
		public override bool alive
		{
			get
			{
				return base.alive && this.heroAlive;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000C995D File Offset: 0x000C7D5D
		public static EnglishSquad selected
		{
			get
			{
				return (!Singleton<SquadSelector>.instance) ? null : Singleton<SquadSelector>.instance.selectedSquad;
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000C997E File Offset: 0x000C7D7E
		public void SetHover(bool hover)
		{
			this.hoverState.SetActive(hover);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000C9990 File Offset: 0x000C7D90
		public void ShowSelection(bool show, bool showGodray = false)
		{
			this.selectedState.SetActive(show);
			if (CinematicCamera.isActive)
			{
				showGodray = false;
			}
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].SetSelection(show, showGodray);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000C99E6 File Offset: 0x000C7DE6
		public bool fleeing
		{
			get
			{
				return this.upgradeManager.GetUpgrade<EvacuateAbility>().state >= EvacuateAbility.State.Departed;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000C99FE File Offset: 0x000C7DFE
		public bool isSelected
		{
			get
			{
				return EnglishSquad.selected == this;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000C9A0B File Offset: 0x000C7E0B
		public bool selectable
		{
			get
			{
				return this.alive && this.heroAgent && !this.fleeing;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000C9A34 File Offset: 0x000C7E34
		public Vector3 cameraPos
		{
			get
			{
				return Singleton<LevelCamera>.instance.transform.InverseTransformPoint(this.standard.agent.transform.position);
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x060030ED RID: 12525 RVA: 0x000C9A5C File Offset: 0x000C7E5C
		// (remove) Token: 0x060030EE RID: 12526 RVA: 0x000C9A94 File Offset: 0x000C7E94
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onSquadSpawnComplete = delegate()
		{
		};

		// Token: 0x060030EF RID: 12527 RVA: 0x000C9ACA File Offset: 0x000C7ECA
		public void UpdateName()
		{
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000C9ACC File Offset: 0x000C7ECC
		public string GetLevelName()
		{
			switch (this.level)
			{
			case 1:
				return "Professional";
			case 2:
				return "Veteran";
			case 3:
				return "Elite";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000C9B10 File Offset: 0x000C7F10
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.pather = null;
			this.navSpotOccupant = null;
			this.upgradeManager = null;
			this.hero = null;
			this.standard = null;
			this.heroStun = null;
			this.actions = null;
			this.selectedState = null;
			this.hoverState = null;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000C9B62 File Offset: 0x000C7F62
		private void CameraFocus(ref Vector3 focus, ref float zoom)
		{
			focus = this.navPos.pos;
			zoom = 0.05f;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000C9B7C File Offset: 0x000C7F7C
		public void RegisterImmediateKill(VikingAgent vikingAgent)
		{
			IslandGameplayManager.RequestCombatAudio(EnglishSquad.killAudio[Mathf.Clamp(this.level, 0, EnglishSquad.killAudio.Length - 1)], vikingAgent.gameObject);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000C9BA4 File Offset: 0x000C7FA4
		public void RegisterFinalKill(VikingAgent vikingAgent)
		{
			this.hero.stats.KilledViking(vikingAgent.type, 1);
			this.hero.stats.bountiesCollected += vikingAgent.bounty;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000C9BDA File Offset: 0x000C7FDA
		public override void ReportDead(Agent agent)
		{
			base.ReportDead(agent);
			this.hero.stats.soldiersLost++;
			if (agent == this.heroAgent)
			{
				this.PlaySquadLostSound();
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000C9C12 File Offset: 0x000C8012
		private void PlaySquadLostSound()
		{
			FabricWrapper.PostEvent(this.hero.voice.deathAudio);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000C9C2A File Offset: 0x000C802A
		public void ApplySquadTeplate(SquadTemplate squadTemplate)
		{
			this.SetSquadTemplate(squadTemplate);
			base.BroadcastChange();
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000C9C3C File Offset: 0x000C803C
		public override void Setup()
		{
			this.navSpotOccupant = base.GetComponentInChildren<NavSpotController>();
			base.Setup();
			this.selectedState = new AnimatedState("Selected", base.rootState, false, false);
			this.hoverState = new AnimatedState("Hover", base.rootState, false, false);
			this.actions = new AgentState("Actions", base.rootState, true, false);
			this.idle = new AgentState("Idle", this.actions, true, true);
			AgentState agentState = this.actions;
			agentState.OnEmpty = (Action)Delegate.Combine(agentState.OnEmpty, new Action(this.idle.SetActiveTrue));
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000C9CE8 File Offset: 0x000C80E8
		public IEnumerator FillRoutine(NavSpot navSpot)
		{
			NavPos navPos = navSpot.navPos;
			SquadFormation formation = new SquadFormation(this.maxCount, navSpot);
			yield return null;
			for (int i = 0; i < this.maxCount; i++)
			{
				if (i >= this.agents.Count)
				{
					Vector3 formPos = formation.Get(i);
					Vector3 offset = formPos * this.minionPrefab.radius * 2f * 0.6f;
					offset += UnityEngine.Random.insideUnitSphere.SetY(0f) * this.minionPrefab.radius * 1.2f;
					NavPos spawnPos = navSpot.navPos;
					spawnPos.pos += offset;
					base.CreateAgent(this.minionPrefab, spawnPos);
					yield return null;
				}
			}
			base.BroadcastChange();
			yield return null;
			yield break;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000C9D0C File Offset: 0x000C810C
		public Agent SpawnHero(NavPos spawnPos)
		{
			Agent agent = base.CreateAgent(this.squadTemplate.standardPrefab, spawnPos);
			Singleton<DustParticles>.instance.SpawnParticles(spawnPos.pos, Vector3.up);
			this.standard = agent.GetComponent<Standard>();
			this.heroStun = agent.GetComponent<Stun>();
			return agent;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000C9D5B File Offset: 0x000C815B
		public void SetSquadTemplate(SquadTemplate squadTemplate)
		{
			this.squadTemplate = squadTemplate;
			this.UpdateName();
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000C9D6A File Offset: 0x000C816A
		public bool IsHeroStunned()
		{
			return this.heroStun.fall.active;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000C9D7C File Offset: 0x000C817C
		public override string GetAgentName(Agent agentPrefab)
		{
			return string.Format("{0}_{1}", this.hero.dbgName, agentPrefab.name);
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000C9D99 File Offset: 0x000C8199
		public void OnSquadSpawnComplete()
		{
			this.onSquadSpawnComplete();
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000C9DA8 File Offset: 0x000C81A8
		public override void LevelEnded(EndOfLevel.Reason reason)
		{
			base.LevelEnded(reason);
			foreach (UpgradeComponent upgradeComponent in this.upgradeManager)
			{
				upgradeComponent.LevelEnded();
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000C9E08 File Offset: 0x000C8208
		public NavSpot GetHeroNavSpot()
		{
			return (!this.heroAgent) ? null : NavSpot.GetNavSpot(this.heroAgent.transform.position, true);
		}

		// Token: 0x040020AD RID: 8365
		public EnglishPatherSquad pather;

		// Token: 0x040020AE RID: 8366
		public NavSpotController navSpotOccupant;

		// Token: 0x040020AF RID: 8367
		public SquadUpgradeManager upgradeManager;

		// Token: 0x040020B0 RID: 8368
		public SquadTemplate squadTemplate;

		// Token: 0x040020B1 RID: 8369
		public HeroDefinition hero;

		// Token: 0x040020B2 RID: 8370
		public int maxCount;

		// Token: 0x040020B3 RID: 8371
		public Standard standard;

		// Token: 0x040020B4 RID: 8372
		private Stun heroStun;

		// Token: 0x040020B5 RID: 8373
		private static FabricEventReference[] killAudio = new FabricEventReference[]
		{
			string.Empty,
			string.Empty,
			"Sfx/Juice/VeteranKill",
			"Sfx/Juice/EliteKill"
		};

		// Token: 0x040020B6 RID: 8374
		public AgentState actions;

		// Token: 0x040020B7 RID: 8375
		public AgentState idle;

		// Token: 0x040020B8 RID: 8376
		public AnimatedState selectedState;

		// Token: 0x040020B9 RID: 8377
		public AnimatedState hoverState;

		// Token: 0x040020BA RID: 8378
		public float idealHeroDist = 0.5f;
	}
}
