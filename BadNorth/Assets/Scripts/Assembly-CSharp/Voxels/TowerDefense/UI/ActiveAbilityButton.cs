using RTM.Pools;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200083F RID: 2111
	[SelectionBase]
	public class ActiveAbilityButton : ConfirmButton, IPoolable
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x000EC365 File Offset: 0x000EA765
		public ActiveAbility activeAbility
		{
			get
			{
				return this._activeAbility;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000EC372 File Offset: 0x000EA772
		public bool inUse
		{
			get
			{
				return this._activeAbility;
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000EC380 File Offset: 0x000EA780
		public void SetupButton(IslandUIManager manager, ActiveAbility activeAbility)
		{
			IslandGameplayManager gameplayManager = manager.gameplayManager;
			this._activeAbility.Target = activeAbility;
			activeAbility.SetGameplayManager(manager.gameplayManager);
			this.cursorManager = gameplayManager.cursorManager;
			this.navigatorNavSpots = gameplayManager.navigatorNavSpotPool;
			base.gameObject.SetActive(true);
			ActiveAbilityButtonVisual component = base.GetComponent<ActiveAbilityButtonVisual>();
			component.Setup();
			activeAbility.onActivated += this.ActiveAbility_onActivated;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000EC3EF File Offset: 0x000EA7EF
		private void ActiveAbility_onActivated()
		{
			if (base.hasFocus)
			{
				ConfirmButton.SetCurrent(null);
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000EC404 File Offset: 0x000EA804
		protected override void OnConfirmed()
		{
			if (this.activeAbility)
			{
				if (this.activeAbility.isCancelable)
				{
					this.activeAbility.Cancel();
				}
				else if (this.activeAbility && this.activeAbility.IsAvailable() && !this.activeAbility.WillHandleConfirmation())
				{
					this.activeAbility.OnConfirmed();
				}
			}
			ConfirmButton.SetCurrent(null);
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000EC484 File Offset: 0x000EA884
		protected override void OnGainedFocus()
		{
			FabricWrapper.PostEvent("UI/Menu/SelectAbility");
			this.activeAbility.focus.SetActive(true);
			if (this.activeAbility.pointerCursor != null)
			{
				this.cursorManager.Add(this.activeAbility.pointerCursor);
			}
			if (this.activeAbility.joystickCursor != null && this.activeAbility.IsAvailable())
			{
				this.cursorManager.Add(this.activeAbility.joystickCursor);
			}
			this.activeAbility.squad.upgradeManager.OnAbilitySelected(this.activeAbility);
			this.navigatorNavSpots.SetDirty();
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000EC530 File Offset: 0x000EA930
		protected override void OnLostFocus()
		{
			this.activeAbility.focus.SetActive(false);
			if (this.activeAbility.pointerCursor != null)
			{
				this.cursorManager.Remove(this.activeAbility.pointerCursor);
			}
			if (this.activeAbility.joystickCursor != null && this.cursorManager.Contains(this.activeAbility.joystickCursor))
			{
				this.cursorManager.Remove(this.activeAbility.joystickCursor);
			}
			this.activeAbility.UpdateNotification(false);
			this.activeAbility.squad.upgradeManager.OnAbilityDeselected(this.activeAbility);
			this.navigatorNavSpots.SetDirty();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000EC5E8 File Offset: 0x000EA9E8
		private void OnDisable()
		{
			if (base.hasFocus)
			{
				ConfirmButton.SetCurrent(null);
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000EC5FC File Offset: 0x000EA9FC
		private void Update()
		{
			if (this.activeAbility && this.activeAbility.squad.isSelected && this.activeAbility.squad.selectable)
			{
				this.activeAbility.UpdateSquadSelected();
				if (base.hasFocus)
				{
					CursorManager.IJoystickCursor joystickCursor = this.activeAbility.joystickCursor;
					bool flag = this.activeAbility.IsAvailable();
					if (joystickCursor != null)
					{
						if (flag && !this.cursorManager.Contains(joystickCursor))
						{
							this.cursorManager.Add(joystickCursor);
						}
						else if (!flag && this.cursorManager.Contains(joystickCursor))
						{
							this.cursorManager.Remove(joystickCursor);
						}
					}
					this.activeAbility.UpdateNotification(true);
				}
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000EC6CE File Offset: 0x000EAACE
		public bool CanDisplay()
		{
			return this._activeAbility && this._activeAbility.Target.CanDisplay();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000EC6F4 File Offset: 0x000EAAF4
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			ActiveAbilityButtonVisual component = base.GetComponent<ActiveAbilityButtonVisual>();
			component.Init(this);
			UIPointerReceiver component2 = base.GetComponent<UIPointerReceiver>();
			UIPointerReceiver.State state = component2.state;
			int lastChangeFrame = 0;
			component2.onStateChanged += delegate(UIPointerReceiver.State s)
			{
				int frameCount = Time.frameCount;
				if (frameCount != lastChangeFrame && state == UIPointerReceiver.State.None && s == UIPointerReceiver.State.Hover)
				{
					FabricWrapper.PostEvent(FabricID.uiHover);
				}
				state = s;
				lastChangeFrame = frameCount;
			};
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000EC741 File Offset: 0x000EAB41
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000EC74F File Offset: 0x000EAB4F
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
			this._activeAbility.Target = null;
			this.cursorManager = null;
			base.name = "[unused]";
		}

		// Token: 0x0400256D RID: 9581
		private WeakReference<ActiveAbility> _activeAbility = new WeakReference<ActiveAbility>(null);

		// Token: 0x0400256E RID: 9582
		private CursorManager cursorManager;

		// Token: 0x0400256F RID: 9583
		private NavigatorNavSpotPool navigatorNavSpots;

		// Token: 0x04002570 RID: 9584
		private const IslandUINotification.Priority notificationPrio = IslandUINotification.Priority.ActiveAbility;
	}
}
