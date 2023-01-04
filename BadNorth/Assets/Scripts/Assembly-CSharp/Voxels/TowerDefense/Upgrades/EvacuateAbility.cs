using System;
using System.Collections;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.RaidGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000848 RID: 2120
	public class EvacuateAbility : TargetableAbility
	{
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000EE8E8 File Offset: 0x000ECCE8
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000EE8F0 File Offset: 0x000ECCF0
		public EvacuateAbility.State state { get; private set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000EE8F9 File Offset: 0x000ECCF9
		protected override bool isTargeting
		{
			get
			{
				return base.hasFocus && this.locations.Count > 0;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000EE917 File Offset: 0x000ECD17
		public override bool blocksMove
		{
			get
			{
				return this.location && this.location.agentCount > 0;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000EE93A File Offset: 0x000ECD3A
		public override CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return this.joystickProxy;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000EE942 File Offset: 0x000ECD42
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x000EE94C File Offset: 0x000ECD4C
		private SquadEvacuationLocation hoverTarget
		{
			get
			{
				return this._hoverTarget;
			}
			set
			{
				if (this._hoverTarget == value)
				{
					return;
				}
				if (this._hoverTarget)
				{
					this._hoverTarget.SetHover(false);
				}
				this._hoverTarget = value;
				if (this._hoverTarget)
				{
					this._hoverTarget.SetHover(true);
				}
			}
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000EE9AC File Offset: 0x000ECDAC
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			if (base.isBanned)
			{
				return base.GetBannedAbilityTerm(out pn, out pv);
			}
			if (!this.IsAnyShipAvailable() && !string.IsNullOrEmpty(this.currentErrorMessage))
			{
				string text;
				pv = (text = null);
				pn = text;
				return this.currentErrorMessage;
			}
			return base.GetNotificationTerm(out pn, out pv);
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000EEA00 File Offset: 0x000ECE00
		protected override void DoSquadSpawnAction_Implementation()
		{
			this.joystickProxy = new EvacuateAbility.JoystickProxy(this);
			base.DoSquadSpawnAction_Implementation();
			Island island = base.squad.faction.island;
			bool flag = base.squad.hero == island.levelNode.heroDefinition || island.levelNode.wantsTutorial;
			if (flag)
			{
				base.BanAbility("UPGRADES/COMMON/TOOLTIPS/NO_FLEE_HOME", "NAME", base.squad.hero.nameTerm);
			}
			this.landings = Singleton<IslandGameplayManager>.instance.island.raid.landingContainer.transform.GetComponentsInChildren<Landing>(true);
			this.squadPather = base.squad.pather;
			AgentState focus = this.focus;
			focus.OnDeactivate = (Action)Delegate.Combine(focus.OnDeactivate, new Action(this.ClearTargets));
			AnimatedState selectedState = base.squad.selectedState;
			selectedState.OnActivate = (Action)Delegate.Combine(selectedState.OnActivate, new Action(delegate()
			{
				this.ClearTargets();
				this.targetsValid = false;
			}));
			this.onEvacuationDepart = new Action(this.OnEvacuationDepart);
			this.onEvacuationConfirm = new Action(this.OnEvacuationConfirm);
			this.onEvacuationCompleted = new Action(this.OnEvacuationCompleted);
			base.squad.onAgentRemoved += this.Squad_onAgentRemoved;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000EEB57 File Offset: 0x000ECF57
		private void OnDestroy()
		{
			if (base.squad)
			{
				base.squad.onAgentRemoved -= this.Squad_onAgentRemoved;
			}
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000EEB80 File Offset: 0x000ECF80
		private void Squad_onAgentRemoved(Agent agent)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (base.squad.isSelected && !base.isActive)
			{
				this.UpdateTargets();
			}
			if (this.location)
			{
				this.location.RemoveAgent(agent);
				if (base.squad.livingAgents.Count == 0 || agent == base.squad.heroAgent)
				{
					this.CleanUp();
				}
			}
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000EEC06 File Offset: 0x000ED006
		public override bool IsAvailable()
		{
			return !base.isBanned && base.IsAvailable() && this.IsAnyShipAvailable();
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000EEC27 File Offset: 0x000ED027
		public override bool ShouldUpdateTargets()
		{
			return base.ShouldUpdateTargets() || Time.unscaledTime - this.lastUpdateTime > 0.1f;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000EEC4C File Offset: 0x000ED04C
		private float GetSquadArea()
		{
			float num = 0f;
			foreach (Agent agent in base.squad.agents)
			{
				num += agent.area;
			}
			return num / this.shipCapacityScale;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000EECC0 File Offset: 0x000ED0C0
		protected override void UpdateTargets()
		{
			using (new ScopedProfiler("Evac_UpdateTargets", null))
			{
				bool flag = this.locations.Count > 0;
				this.ClearTargets();
				if (!base.isBanned)
				{
					float squadArea = this.GetSquadArea();
					bool flag2 = false;
					foreach (Landing landing in this.landings)
					{
						if (landing.spawnedShip && landing.spawnedShip.landed)
						{
							SquadEvacuationLocation evacuationLocation = landing.spawnedShip.evacuationLocation;
							flag2 |= evacuationLocation.IsAvailable(0f);
							if (evacuationLocation.IsAvailable(squadArea))
							{
								this.locations.Add(evacuationLocation);
							}
						}
					}
					if (this.locations.Count == 0)
					{
						this.currentErrorMessage = ((!flag2) ? "UPGRADES/COMMON/TOOLTIPS/FLEE_NO_SHIPS" : "UPGRADES/COMMON/TOOLTIPS/FLEE_NO_LARGE_SHIPS");
					}
					else
					{
						this.currentErrorMessage = string.Empty;
					}
					if (this.focus.active)
					{
						foreach (SquadEvacuationLocation squadEvacuationLocation in this.locations)
						{
							squadEvacuationLocation.SetHighlight(true);
						}
					}
					if (this.locations.Count > 0 && !flag && this.targetsValid)
					{
						FabricWrapper.PostEvent(EvacuateAbility.fleePossibleAudioId);
					}
					this.targetsValid = true;
					this.lastUpdateTime = Time.unscaledTime;
				}
			}
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000EEE9C File Offset: 0x000ED29C
		protected override void ClearTargets()
		{
			using (new ScopedProfiler("Evac_ClearTargets", null))
			{
				foreach (SquadEvacuationLocation squadEvacuationLocation in this.locations)
				{
					squadEvacuationLocation.SetHighlight(false);
				}
				this.locations.Clear();
			}
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000EEF30 File Offset: 0x000ED330
		protected override bool HandleScreenSpaceClick(Vector2 screenPos)
		{
			SquadEvacuationLocation locationFromScreenPos = this.GetLocationFromScreenPos(screenPos);
			if (locationFromScreenPos)
			{
				this.SelectLocation(locationFromScreenPos);
			}
			return locationFromScreenPos;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000EEF5D File Offset: 0x000ED35D
		private bool IsAnyShipAvailable()
		{
			return this.locations.Count > 0;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000EEF70 File Offset: 0x000ED370
		private SquadEvacuationLocation GetLocationFromScreenPos(Vector2 screenPos)
		{
			this.UpdateTargets();
			if (!this.IsAvailable())
			{
				return null;
			}
			screenPos.x /= (float)Screen.width;
			screenPos.y /= (float)Screen.height;
			Ray ray = Singleton<LevelCamera>.instance.cameraRef.ViewportPointToRay(screenPos);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, this.layerMask))
			{
				SquadEvacuationLocation squadEvacuationLocation = (!raycastHit.collider) ? null : raycastHit.collider.GetComponent<SquadEvacuationLocation>();
				if (squadEvacuationLocation && this.locations.Contains(squadEvacuationLocation))
				{
					return squadEvacuationLocation;
				}
			}
			return null;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000EF030 File Offset: 0x000ED430
		protected override void UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
			bool flag = state == PointerRationalizer.State.Hover || state == PointerRationalizer.State.ButtonDown;
			this.hoverTarget = ((!flag) ? null : this.GetLocationFromScreenPos(screenPos));
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000EF068 File Offset: 0x000ED468
		private void SelectLocation(SquadEvacuationLocation location)
		{
			this.location = location;
			this.squadPather.SetPatherTarget(location, true);
			location.AssignSquad(base.squad);
			location.onEvacuationDepart += this.onEvacuationDepart;
			location.onEvacuationConfirm += this.onEvacuationConfirm;
			location.onEvacuationCompleted += this.onEvacuationCompleted;
			base.StartCoroutine(this.AwaitArrival());
			this.state = EvacuateAbility.State.Requested;
			base.OnActivated();
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000EF0D3 File Offset: 0x000ED4D3
		private void OnEvacuationDepart()
		{
			base.StopAllCoroutines();
			this.state = EvacuateAbility.State.Departed;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000EF0E4 File Offset: 0x000ED4E4
		private void OnEvacuationConfirm()
		{
			if (this.state < EvacuateAbility.State.Confirmed)
			{
				this.state = EvacuateAbility.State.Confirmed;
				if (base.squad.standard)
				{
					base.squad.standard.invulnerability = Standard.Invulnerability.ForceOn;
					FabricWrapper.PostEvent(EvacuateAbility.confirmIconAudioId, base.squad.heroAgent.gameObject);
					ScriptableObjectSingleton<PrefabManager>.instance.abilityFloatingIcon.GetInstance<FloatingIcon>().Setup(base.iconSprite, base.squad.heroAgent.transform);
				}
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000EF170 File Offset: 0x000ED570
		private void OnEvacuationCompleted()
		{
			this.state = EvacuateAbility.State.Completed;
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000EF17C File Offset: 0x000ED57C
		private IEnumerator AwaitArrival()
		{
			float waitTil = 0f;
			bool standardOnShip = false;
			for (;;)
			{
				bool allOthersOnShip = true;
				float minOrderDist = float.MaxValue;
				foreach (Agent agent in base.squad.livingAgents)
				{
					if (!this.location.Contains(agent))
					{
						bool isStandard = agent == base.squad.standard.agent;
						allOthersOnShip = (allOthersOnShip && isStandard);
						minOrderDist = Mathf.Min(agent.orderDist, minOrderDist);
						if (agent.orderDist < 0.1f && agent.moveAnimate)
						{
							this.Agent_onArrived(agent);
							if (isStandard)
							{
								base.squad.standard.invulnerability = Standard.Invulnerability.Default;
								standardOnShip = true;
								waitTil = Mathf.Max(waitTil, Time.time + 4f);
							}
							else
							{
								waitTil = Mathf.Max(waitTil, Time.time + 2f);
							}
							float t = Time.time + 0.3f;
							while (t < Time.time)
							{
								yield return null;
							}
						}
					}
				}
				if (base.squad.livingAgents.Count == 0)
				{
					break;
				}
				if (standardOnShip && (allOthersOnShip || Time.time > waitTil || minOrderDist > 2f))
				{
					goto IL_26A;
				}
				if (allOthersOnShip && !standardOnShip && Time.time > waitTil + 2f)
				{
					base.squad.standard.invulnerability = Standard.Invulnerability.ForceOff;
				}
				yield return null;
			}
			this.location.AssignSquad(null);
			yield break;
			IL_26A:
			this.location.Launch();
			yield break;
			yield break;
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000EF198 File Offset: 0x000ED598
		private void Agent_onArrived(Agent agent)
		{
			agent.SetNavPos(this.location.localNavPos);
			if (agent == base.squad.standard)
			{
				base.squad.standard.invulnerability = Standard.Invulnerability.Default;
			}
			this.location.AddAgent(agent);
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000EF1E9 File Offset: 0x000ED5E9
		public override void Cancel()
		{
			this.CleanUp();
			base.Cancel();
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000EF1F8 File Offset: 0x000ED5F8
		private void CleanUp()
		{
			if (this.location)
			{
				this.location.onEvacuationDepart -= this.onEvacuationDepart;
				this.location.onEvacuationConfirm -= this.onEvacuationConfirm;
				this.location.onEvacuationCompleted -= this.onEvacuationCompleted;
				this.location.AssignSquad(null);
				this.location = null;
			}
			base.StopAllCoroutines();
			this.state = EvacuateAbility.State.None;
		}

		// Token: 0x040025A4 RID: 9636
		private const string islandHeroToolTipText = "UPGRADES/COMMON/TOOLTIPS/NO_FLEE_HOME";

		// Token: 0x040025A5 RID: 9637
		private const string noShipsToolTipText = "UPGRADES/COMMON/TOOLTIPS/FLEE_NO_SHIPS";

		// Token: 0x040025A6 RID: 9638
		private const string nolargeShipsToolTipText = "UPGRADES/COMMON/TOOLTIPS/FLEE_NO_LARGE_SHIPS";

		// Token: 0x040025A7 RID: 9639
		private string currentErrorMessage = string.Empty;

		// Token: 0x040025A8 RID: 9640
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("Evac", EVerbosity.Quiet, 1000);

		// Token: 0x040025A9 RID: 9641
		[SerializeField]
		private LayerMask layerMask = default(LayerMask);

		// Token: 0x040025AA RID: 9642
		[SerializeField]
		private float shipCapacityScale = 1.6f;

		// Token: 0x040025AB RID: 9643
		private EvacuateAbility.JoystickProxy joystickProxy;

		// Token: 0x040025AC RID: 9644
		private Landing[] landings;

		// Token: 0x040025AD RID: 9645
		private List<SquadEvacuationLocation> locations = new List<SquadEvacuationLocation>();

		// Token: 0x040025AE RID: 9646
		private float lastUpdateTime = float.MinValue;

		// Token: 0x040025AF RID: 9647
		private SquadEvacuationLocation location;

		// Token: 0x040025B0 RID: 9648
		private EnglishPatherSquad squadPather;

		// Token: 0x040025B2 RID: 9650
		private bool targetsValid;

		// Token: 0x040025B3 RID: 9651
		private static FabricEventReference confirmIconAudioId = "UI/InGame/UpgradePing";

		// Token: 0x040025B4 RID: 9652
		private static FabricEventReference fleePossibleAudioId = "UI/InGame/FleePossible";

		// Token: 0x040025B5 RID: 9653
		private SquadEvacuationLocation _hoverTarget;

		// Token: 0x040025B6 RID: 9654
		private Action onEvacuationDepart;

		// Token: 0x040025B7 RID: 9655
		private Action onEvacuationConfirm;

		// Token: 0x040025B8 RID: 9656
		private Action onEvacuationCompleted;

		// Token: 0x02000849 RID: 2121
		public enum State
		{
			// Token: 0x040025BA RID: 9658
			None,
			// Token: 0x040025BB RID: 9659
			Requested,
			// Token: 0x040025BC RID: 9660
			Departed,
			// Token: 0x040025BD RID: 9661
			Confirmed,
			// Token: 0x040025BE RID: 9662
			Completed
		}

		// Token: 0x0200084A RID: 2122
		private class JoystickProxy : CursorManager.IJoystickCursor, CursorManager.ICursor
		{
			// Token: 0x06003779 RID: 14201 RVA: 0x000EF297 File Offset: 0x000ED697
			public JoystickProxy(EvacuateAbility ability)
			{
				this.ability = ability;
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x0600377A RID: 14202 RVA: 0x000EF2BB File Offset: 0x000ED6BB
			// (set) Token: 0x0600377B RID: 14203 RVA: 0x000EF2C3 File Offset: 0x000ED6C3
			private SquadEvacuationLocation joystickTarget
			{
				get
				{
					return this._joystickTarget;
				}
				set
				{
					this._joystickTarget = value;
					this.ability.hoverTarget = value;
				}
			}

			// Token: 0x0600377C RID: 14204 RVA: 0x000EF2D8 File Offset: 0x000ED6D8
			private SquadEvacuationLocation GetDefaultJoystickTarget()
			{
				if (this.joystickTarget)
				{
					return this.joystickTarget;
				}
				NavPos navPos = this.ability.squad.heroAgent.navPos;
				float num = float.MaxValue;
				SquadEvacuationLocation result = null;
				foreach (SquadEvacuationLocation squadEvacuationLocation in this.ability.locations)
				{
					float distanceFrom = ((IPathTarget)squadEvacuationLocation).GetDistanceFrom(navPos);
					if (distanceFrom < num)
					{
						num = distanceFrom;
						result = squadEvacuationLocation;
					}
				}
				return result;
			}

			// Token: 0x0600377D RID: 14205 RVA: 0x000EF380 File Offset: 0x000ED780
			private void SetJoystickTarget(SquadEvacuationLocation location)
			{
				if (location)
				{
					bool flag = !this.icon;
					if (flag)
					{
						this.icon = this.ability.wsCursorIcons.Target.GetInstance(this.ability.squad);
						if (this.icon.isNew)
						{
							this.icon.SetCurrentPos(location.GetWSCursorPos());
						}
					}
					this.icon.UpdateFloorPos(location.GetWSCursorPos(), false);
					FabricWrapper.PostEvent("UI/InGame/SnapHouse");
				}
				else
				{
					if (this.icon)
					{
						this.icon.Deactivate();
					}
					this.icon = null;
				}
				this.joystickTarget = location;
			}

			// Token: 0x0600377E RID: 14206 RVA: 0x000EF440 File Offset: 0x000ED840
			void CursorManager.ICursor.SetActive(bool active)
			{
				if (active)
				{
					this.SetJoystickTarget(this.GetDefaultJoystickTarget());
					this.joystickInvalid = true;
				}
				else
				{
					this.joystickTarget = null;
					if (this.icon)
					{
						this.icon.Deactivate();
						this.icon = null;
					}
				}
			}

			// Token: 0x0600377F RID: 14207 RVA: 0x000EF494 File Offset: 0x000ED894
			void CursorManager.IJoystickCursor.SetMoveInput(Vector2 input)
			{
				input = ((input.magnitude >= 0.5f) ? input : Vector2.zero);
				if (input == Vector2.zero)
				{
					this.joystickInvalid = false;
				}
				else if (!this.joystickInvalid)
				{
					this.joystickInvalid = true;
					if (!this.joystickTarget)
					{
						this.SetJoystickTarget(this.GetDefaultJoystickTarget());
					}
					else
					{
						Vector3 startPos = (!this.joystickTarget) ? this.ability.squad.heroAgent.transform.position : this.joystickTarget.transform.position;
						Vector3 topDownInputVector = Singleton<LevelCamera>.instance.cameraRef.GetTopDownInputVector(input);
						SquadEvacuationLocation highestWeighted = this.joystickWeighter.GetHighestWeighted<SquadEvacuationLocation>(startPos, this.ability.locations, topDownInputVector.GetZeroY().normalized);
						if (highestWeighted)
						{
							this.SetJoystickTarget(highestWeighted);
						}
						else
						{
							FabricWrapper.PostEvent("UI/InGame/Error");
						}
					}
				}
			}

			// Token: 0x06003780 RID: 14208 RVA: 0x000EF5A8 File Offset: 0x000ED9A8
			void CursorManager.IJoystickCursor.OnSelectButtonDown()
			{
				if (this.joystickTarget && this.joystickTarget.IsAvailable(this.ability.GetSquadArea()))
				{
					this.icon.SetTargetPos(this.joystickTarget.GetWSCursorPos() - Vector3.up * 0.25f, false);
					this.ability.SelectLocation(this.joystickTarget);
				}
				else
				{
					FabricWrapper.PostEvent("UI/InGame/Error");
				}
			}

			// Token: 0x06003781 RID: 14209 RVA: 0x000EF62C File Offset: 0x000EDA2C
			void CursorManager.IJoystickCursor.OnSelectButtonUp()
			{
			}

			// Token: 0x040025BF RID: 9663
			private EvacuateAbility ability;

			// Token: 0x040025C0 RID: 9664
			private TopDownWeighter joystickWeighter = new TopDownWeighter(50f, float.MaxValue);

			// Token: 0x040025C1 RID: 9665
			private SquadEvacuationLocation _joystickTarget;

			// Token: 0x040025C2 RID: 9666
			private WorldSpaceCursorIcon icon;

			// Token: 0x040025C3 RID: 9667
			private bool joystickInvalid;
		}
	}
}
