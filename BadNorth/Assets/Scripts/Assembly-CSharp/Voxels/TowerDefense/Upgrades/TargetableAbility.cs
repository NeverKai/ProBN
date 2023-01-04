using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200085F RID: 2143
	public abstract class TargetableAbility : ActiveAbility
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000ECED3 File Offset: 0x000EB2D3
		public override CursorManager.IPointerCursor pointerCursor
		{
			get
			{
				return this.pointerProxy;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600381E RID: 14366
		protected abstract bool isTargeting { get; }

		// Token: 0x0600381F RID: 14367
		protected abstract bool HandleScreenSpaceClick(Vector2 screenPos);

		// Token: 0x06003820 RID: 14368
		protected abstract void UpdateTargets();

		// Token: 0x06003821 RID: 14369
		protected abstract void ClearTargets();

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x000ECEDB File Offset: 0x000EB2DB
		public bool hasFocus
		{
			get
			{
				return this.focus.active;
			}
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000ECEE8 File Offset: 0x000EB2E8
		public override bool WillHandleConfirmation()
		{
			return true;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000ECEEC File Offset: 0x000EB2EC
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.pointerProxy = new TargetableAbility.PointerProxy(this);
			this.focus.OnUpdate += delegate()
			{
				this.MaybeUpdateTargets();
			};
			AgentState focus = this.focus;
			focus.OnActivate = (Action)Delegate.Combine(focus.OnActivate, new Action(delegate()
			{
				this.MaybeUpdateTargets();
			}));
			AgentState focus2 = this.focus;
			focus2.OnDeactivate = (Action)Delegate.Combine(focus2.OnDeactivate, new Action(delegate()
			{
				this.ClearTargets();
			}));
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000ECF70 File Offset: 0x000EB370
		public override bool IsAvailable()
		{
			return base.IsAvailable() && (!this.hasFocus || this.isTargeting);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000ECF94 File Offset: 0x000EB394
		public override void UpdateSquadSelected()
		{
			base.UpdateSquadSelected();
			this.MaybeUpdateTargets();
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000ECFA2 File Offset: 0x000EB3A2
		public override void OnConfirmed()
		{
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000ECFA4 File Offset: 0x000EB3A4
		private void MaybeUpdateTargets()
		{
			if (this.ShouldUpdateTargets())
			{
				this.UpdateTargets();
			}
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000ECFB7 File Offset: 0x000EB3B7
		public virtual bool ShouldUpdateTargets()
		{
			return this.focus.active && base.idle;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000ECFD4 File Offset: 0x000EB3D4
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			string text;
			pv = (text = null);
			pn = text;
			string notificationTerm = base.GetNotificationTerm(out pn, out pv);
			if (string.IsNullOrEmpty(notificationTerm) && !this.isTargeting)
			{
				return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
			}
			return notificationTerm;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000ED00F File Offset: 0x000EB40F
		protected virtual void UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000ED011 File Offset: 0x000EB411
		protected virtual void OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
		{
		}

		// Token: 0x0400264A RID: 9802
		public const string noTargetsToolTipText = "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";

		// Token: 0x0400264B RID: 9803
		private TargetableAbility.PointerProxy pointerProxy;

		// Token: 0x0400264C RID: 9804
		private static FabricEventReference deselectAudioId = "UI/InGame/UnitDeselect";

		// Token: 0x02000860 RID: 2144
		private class PointerProxy : CursorManager.IPointerCursor, CursorManager.ICursor
		{
			// Token: 0x06003831 RID: 14385 RVA: 0x000ED03C File Offset: 0x000EB43C
			public PointerProxy(TargetableAbility ability)
			{
				this.ability = ability;
			}

			// Token: 0x06003832 RID: 14386 RVA: 0x000ED04B File Offset: 0x000EB44B
			void CursorManager.ICursor.SetActive(bool active)
			{
			}

			// Token: 0x06003833 RID: 14387 RVA: 0x000ED04D File Offset: 0x000EB44D
			void CursorManager.IPointerCursor.OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
			{
			}

			// Token: 0x06003834 RID: 14388 RVA: 0x000ED050 File Offset: 0x000EB450
			void CursorManager.IPointerCursor.OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos)
			{
				if (!this.ability.HandleScreenSpaceClick(screenPos))
				{
					Singleton<SquadSelector>.instance.SelectSquad(null, false);
					FabricWrapper.PostEvent(TargetableAbility.deselectAudioId);
				}
			}

			// Token: 0x06003835 RID: 14389 RVA: 0x000ED087 File Offset: 0x000EB487
			void CursorManager.IPointerCursor.UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
			{
				this.ability.UpdateHoverTarget(state, screenPos);
			}

			// Token: 0x06003836 RID: 14390 RVA: 0x000ED096 File Offset: 0x000EB496
			void CursorManager.IPointerCursor.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
			{
				this.ability.OverrideCursorTexture(state, ref texture, ref position);
			}

			// Token: 0x0400264D RID: 9805
			private TargetableAbility ability;
		}
	}
}
