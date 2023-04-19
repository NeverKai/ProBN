using System;
using System.Collections.Generic;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000855 RID: 2133
	public abstract class NavSpotTargetableAbility : TargetableAbility
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060037D1 RID: 14289 RVA: 0x000ED0C5 File Offset: 0x000EB4C5
		protected override bool isTargeting
		{
			get
			{
				return this.candidates.Count > 0;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000ED0D5 File Offset: 0x000EB4D5
		public override CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000ED0DD File Offset: 0x000EB4DD
		// (set) Token: 0x060037D4 RID: 14292 RVA: 0x000ED0E5 File Offset: 0x000EB4E5
		public ITargeter targeter { get; private set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000ED0EE File Offset: 0x000EB4EE
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x000ED0F6 File Offset: 0x000EB4F6
		private protected NavSpot heroNavSpot { get; private set; }

		// Token: 0x060037D7 RID: 14295 RVA: 0x000ED100 File Offset: 0x000EB500
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.targeter = base.GetComponent<ITargeter>();
			WorldSpaceNavSpotCursor navSpotCursor = ScriptableObjectSingleton<PrefabManager>.instance.navSpotCursor;
			this.cursor = base.gameObject.InstantiateChild(navSpotCursor, navSpotCursor.name);
			this.cursor.Init(base.squad, base.island, new Action<NavSpot>(this.OnJoystickSelect), new Action<NavSpot>(this.OnJoystickNavSpotChanged));
			this.navSpotPool = Singleton<IslandGameplayManager>.instance.navSpotPoolManager.GetPool(this.targetNavspotPrefab);
			this.candidates.Capacity = base.island.navSpotter.navSpots.Count;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000ED1B1 File Offset: 0x000EB5B1
		public override void SetGameplayManager(IslandGameplayManager manager)
		{
			base.SetGameplayManager(manager);
			this.targetCache = manager.nsTargetManager.GetCache(this);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000ED1CC File Offset: 0x000EB5CC
		public override bool ShouldUpdateTargets()
		{
			if (base.idle)
			{
				NavSpot heroNavSpot = this.heroNavSpot;
				this.heroNavSpot = base.squad.GetHeroNavSpot();
				return this.focus.active && (this.heroNavSpot != heroNavSpot || !this.isTargeting || (this.isTargeting && this.IsBlocked()));
			}
			return false;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000ED244 File Offset: 0x000EB644
		protected override void UpdateTargets()
		{
			if ((base.hasLimitedCharges && base.chargesRemaining == 0) || this.IsBlocked() || this.heroNavSpot == null)
			{
				foreach (TargetNavSpot targetNavSpot in this.candidates)
				{
					targetNavSpot.SetVisible(false);
				}
				this.candidates.Clear();
				return;
			}
			bool flag = this.candidates.Count == 0;
			using (new ScopedProfiler("TargetingFindTargets", null))
			{
				int index = this.heroNavSpot.index;
				NavSpotTargetCache.Entry entry = this.targetCache.entries[index];
				if (!entry.targetMask.anySet)
				{
					foreach (TargetNavSpot targetNavSpot2 in this.candidates)
					{
						targetNavSpot2.SetVisible(false);
					}
					this.candidates.Clear();
				}
				else
				{
					int i = 0;
					int count = base.island.navSpotter.navSpots.Count;
					while (i < count)
					{
						NavSpot navSpot = base.island.navSpotter.navSpots[i];
						TargetNavSpot targetNavSpot3 = this.GetTarget(navSpot);
						if (entry.targetMask.Get(i))
						{
							if (!targetNavSpot3)
							{
								targetNavSpot3 = this.navSpotPool.Target.GetInstance();
								targetNavSpot3.Setup(navSpot);
								targetNavSpot3.SetVisible(true);
								if (flag)
								{
									targetNavSpot3.DoFlash();
								}
								this.candidates.Add(targetNavSpot3);
							}
						}
						else if (targetNavSpot3)
						{
							this.candidates.Remove(targetNavSpot3);
							targetNavSpot3.SetVisible(false);
						}
						i++;
					}
				}
			}
			this.cursor.SetCandidates(this.candidates);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000ED4B4 File Offset: 0x000EB8B4
		private TargetNavSpot GetTarget(NavSpot ns)
		{
			if (ns != null)
			{
				foreach (TargetNavSpot targetNavSpot in this.candidates)
				{
					if (targetNavSpot.navSpot == ns)
					{
						return targetNavSpot;
					}
				}
			}
			return null;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000ED530 File Offset: 0x000EB930
		public override bool IsAvailable()
		{
			return base.IsAvailable() && this.targetCache != null && this.heroNavSpot != null && this.targetCache.entries[this.heroNavSpot.index].targetMask.anySet;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000ED590 File Offset: 0x000EB990
		protected override void ClearTargets()
		{
			foreach (TargetNavSpot targetNavSpot in this.candidates)
			{
				targetNavSpot.SetVisible(false);
			}
			this.candidates.Clear();
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000ED5F8 File Offset: 0x000EB9F8
		protected override void UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
			NavSpot ns = (!this.IsAvailable()) ? null : NavSpot.NavSpotCast(screenPos);
			this.UpdateHoverTarget(this.GetTarget(ns));
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000ED62C File Offset: 0x000EBA2C
		private void UpdateHoverTarget(TargetNavSpot target)
		{
			if (target != this.hoverTarget)
			{
				if (this.hoverTarget)
				{
					this.hoverTarget.SetHover(false);
				}
				if (target)
				{
					target.SetHover(true);
				}
			}
			this.hoverTarget = target;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000ED680 File Offset: 0x000EBA80
		protected override bool HandleScreenSpaceClick(Vector2 screenPos)
		{
			NavSpot navSpot = (!this.hoverTarget) ? null : this.hoverTarget.navSpot;
			if (navSpot)
			{
				this.heroNavSpot = ((!this.heroNavSpot) ? base.squad.GetHeroNavSpot() : this.heroNavSpot);
				this.DoTargetedAction(this.heroNavSpot, navSpot);
				base.OnActivated();
				return true;
			}
			return false;
		}

		// Token: 0x060037E1 RID: 14305
		protected abstract void DoTargetedAction(NavSpot heroNavSpot, NavSpot target);

		// Token: 0x060037E2 RID: 14306 RVA: 0x000ED6FC File Offset: 0x000EBAFC
		private void OnJoystickSelect(NavSpot navSpot)
		{
			this.DoTargetedAction(this.heroNavSpot, navSpot);
			base.OnActivated();
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000ED711 File Offset: 0x000EBB11
		private void OnJoystickNavSpotChanged(NavSpot navSpot)
		{
			this.UpdateHoverTarget(this.GetTarget(navSpot));
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000ED720 File Offset: 0x000EBB20
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			string result;
			if (!this.IsBlocked() && this.GetStaticNotificationTerm(this.heroNavSpot.index, out result))
			{
				string text;
				pv = (text = null);
				pn = text;
				return result;
			}
			return base.GetNotificationTerm(out pn, out pv);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000ED764 File Offset: 0x000EBB64
		protected bool GetStaticNotificationTerm(int navSpotIdx, out string message)
		{
			message = this.targetCache.entries[navSpotIdx].errorMessage;
			return !string.IsNullOrEmpty(message);
		}

		// Token: 0x040025FD RID: 9725
		protected WorldSpaceNavSpotCursor cursor;

		// Token: 0x040025FF RID: 9727
		private NavSpotTargetCache targetCache;

		// Token: 0x04002601 RID: 9729
		[SerializeField]
		private TargetNavSpot targetNavspotPrefab;

		// Token: 0x04002602 RID: 9730
		private RTM.Utilities.WeakReference<LocalPool<TargetNavSpot>> navSpotPool = new RTM.Utilities.WeakReference<LocalPool<TargetNavSpot>>(null);

		// Token: 0x04002603 RID: 9731
		private List<TargetNavSpot> candidates = new List<TargetNavSpot>();

		// Token: 0x04002604 RID: 9732
		private TargetNavSpot hoverTarget;
	}
}
