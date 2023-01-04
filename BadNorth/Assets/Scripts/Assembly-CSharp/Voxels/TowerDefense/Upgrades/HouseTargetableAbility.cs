using System;
using System.Collections.Generic;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200084E RID: 2126
	[ConsoleCommandClassCustomizer("ReplenishAbility")]
	public abstract class HouseTargetableAbility : TargetableAbility
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000EE352 File Offset: 0x000EC752
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x000EE35A File Offset: 0x000EC75A
		public List<SquadReplenishLocation> locations { get; private set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000EE363 File Offset: 0x000EC763
		public override CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return this.joystickProxy;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000EE36B File Offset: 0x000EC76B
		public override bool blocksMove
		{
			get
			{
				return this.location && this.location.numContainedAgents > 0 && !this.location.squadExiting;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000EE39F File Offset: 0x000EC79F
		// (set) Token: 0x0600379B RID: 14235 RVA: 0x000EE3A8 File Offset: 0x000EC7A8
		public SquadReplenishLocation hoverTarget
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

		// Token: 0x0600379C RID: 14236 RVA: 0x000EE408 File Offset: 0x000EC808
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.joystickProxy = new HouseTargetingJoystick(this);
			this.locations = new List<SquadReplenishLocation>();
			House[] houses = base.island.village.houses;
			int i = 0;
			int num = houses.Length;
			while (i < num)
			{
				SquadReplenishLocation component = houses[i].GetComponent<SquadReplenishLocation>();
				if (component)
				{
					this.locations.Add(component);
				}
				i++;
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000EE479 File Offset: 0x000EC879
		private void OnDestroy()
		{
			this.joystickProxy = null;
			this.locations = null;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000EE489 File Offset: 0x000EC889
		protected override bool isTargeting
		{
			get
			{
				return base.hasFocus && this.locations.Count > 0;
			}
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000EE4A8 File Offset: 0x000EC8A8
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			string text;
			pv = (text = null);
			pn = text;
			if (this.IsSquadFullStrength())
			{
				return "UPGRADES/COMMON/TOOLTIPS/REPLENISH_FULL";
			}
			if (this.location != null)
			{
				return "UPGRADES/COMMON/TOOLTIPS/REPLENISH_ACTIVE";
			}
			if (!this.IsAnyLocationAvailable())
			{
				return "UPGRADES/COMMON/TOOLTIPS/REPLENISH_NO_HOUSE";
			}
			return base.GetNotificationTerm(out pn, out pv);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000EE4FE File Offset: 0x000EC8FE
		private bool IsSquadFullStrength()
		{
			return base.squad.livingAgents.Count >= base.squad.maxCount;
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000EE520 File Offset: 0x000EC920
		private bool IsAnyLocationAvailable()
		{
			int i = 0;
			int count = this.locations.Count;
			while (i < count)
			{
				if (this.locations[i].available)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000EE564 File Offset: 0x000EC964
		public override bool IsAvailable()
		{
			return base.IsAvailable() && this.location == null && !this.IsSquadFullStrength() && this.IsAnyLocationAvailable();
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000EE598 File Offset: 0x000EC998
		protected override void UpdateTargets()
		{
			bool flag = this.IsAvailable();
			int i = 0;
			int count = this.locations.Count;
			while (i < count)
			{
				this.locations[i].SetHighlight(flag && this.locations[i].available);
				i++;
			}
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000EE5F8 File Offset: 0x000EC9F8
		protected override void ClearTargets()
		{
			int i = 0;
			int count = this.locations.Count;
			while (i < count)
			{
				this.locations[i].SetHighlight(false);
				i++;
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000EE638 File Offset: 0x000ECA38
		protected override bool HandleScreenSpaceClick(Vector2 screenPos)
		{
			if (this.IsAvailable())
			{
				this.location = this.GetLocationFromScreenPos(screenPos);
				if (this.location)
				{
					this.SelectLocation(this.location);
				}
				return this.location;
			}
			return false;
		}

		// Token: 0x060037A6 RID: 14246
		public abstract void SelectLocation(SquadReplenishLocation location);

		// Token: 0x060037A7 RID: 14247 RVA: 0x000EE688 File Offset: 0x000ECA88
		protected override void UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
			bool flag = state == PointerRationalizer.State.Hover || state == PointerRationalizer.State.ButtonDown;
			this.hoverTarget = ((!flag || !this.IsAvailable()) ? null : this.GetLocationFromScreenPos(screenPos));
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000EE6C8 File Offset: 0x000ECAC8
		private SquadReplenishLocation GetLocationFromScreenPos(Vector2 screenPos)
		{
			screenPos.x /= (float)Screen.width;
			screenPos.y /= (float)Screen.height;
			Ray ray = Singleton<LevelCamera>.instance.cameraRef.ViewportPointToRay(screenPos);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, this.layerMask))
			{
				SquadReplenishLocation squadReplenishLocation = (!raycastHit.collider) ? null : raycastHit.collider.GetComponentInParent<SquadReplenishLocation>();
				return (!squadReplenishLocation || !squadReplenishLocation.available) ? null : squadReplenishLocation;
			}
			return null;
		}

		// Token: 0x040025D9 RID: 9689
		private const string squadFullToolTipText = "UPGRADES/COMMON/TOOLTIPS/REPLENISH_FULL";

		// Token: 0x040025DA RID: 9690
		private const string noHousesToolTipText = "UPGRADES/COMMON/TOOLTIPS/REPLENISH_NO_HOUSE";

		// Token: 0x040025DB RID: 9691
		private const string alreadyHealingToolTipText = "UPGRADES/COMMON/TOOLTIPS/REPLENISH_ACTIVE";

		// Token: 0x040025DC RID: 9692
		[SerializeField]
		[ConsoleCommand("")]
		private bool alwaysAvailable;

		// Token: 0x040025DD RID: 9693
		[SerializeField]
		private LayerMask layerMask = default(LayerMask);

		// Token: 0x040025DF RID: 9695
		protected SquadReplenishLocation location;

		// Token: 0x040025E0 RID: 9696
		private HouseTargetingJoystick joystickProxy;

		// Token: 0x040025E1 RID: 9697
		private SquadReplenishLocation _hoverTarget;
	}
}
