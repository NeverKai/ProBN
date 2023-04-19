using System;
using Rewired;
using RTM.Input;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000851 RID: 2129
	public class JoystickMoveAbility : ActiveAbility
	{
		// Token: 0x060037BB RID: 14267 RVA: 0x000F0620 File Offset: 0x000EEA20
		public override void OnConfirmed()
		{
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000F0622 File Offset: 0x000EEA22
		public override CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000F062C File Offset: 0x000EEA2C
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.navSpots.Target = Singleton<IslandGameplayManager>.instance.GetComponentInChildren<NavigatorNavSpotPool>(true);
			this.cursor = base.gameObject.InstantiateChild(this.cursorPrefab, this.cursorPrefab.name);
			this.cursor.Init(base.squad, base.island, new Action<NavSpot>(this.OnJoystickSelect), new Action<NavSpot>(this.OnHoverNavSpotChanged));
			this.cursor.SetCandidates(base.island.navSpotter.navSpots);
			this.cursor.requiresSomeInput = false;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000F06CD File Offset: 0x000EEACD
		private void OnHoverNavSpotChanged(NavSpot navSpot)
		{
			this.navSpots.Target.SetHover(navSpot);
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000F06E0 File Offset: 0x000EEAE0
		public override bool CanDisplay()
		{
			return base.CanDisplay() && InputHelpers.ControllerTypeIs(ControllerType.Joystick);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000F06F6 File Offset: 0x000EEAF6
		public override bool IsAvailable()
		{
			return !this.squadUpgradeManager.IsBlockingMove();
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000F0706 File Offset: 0x000EEB06
		private void OnJoystickSelect(NavSpot navSpot)
		{
			SquadMover.MoveTo(base.squad, navSpot);
			base.OnActivated();
			base.OnEnded();
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000F0720 File Offset: 0x000EEB20
		public NavSpot GetNavSpotTarget()
		{
			if (this.cursor.isActiveAndEnabled)
			{
				return this.cursor.navSpot;
			}
			return null;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000F0740 File Offset: 0x000EEB40
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			string text;
			pv = (text = null);
			pn = text;
			return null;
		}

		// Token: 0x040025E8 RID: 9704
		[Header("Prefabs")]
		[SerializeField]
		private WorldSpaceNavSpotCursor cursorPrefab;

		// Token: 0x040025E9 RID: 9705
		private WorldSpaceNavSpotCursor cursor;

		// Token: 0x040025EA RID: 9706
		private RTM.Utilities.WeakReference<NavigatorNavSpotPool> navSpots = new RTM.Utilities.WeakReference<NavigatorNavSpotPool>(null);
	}
}
