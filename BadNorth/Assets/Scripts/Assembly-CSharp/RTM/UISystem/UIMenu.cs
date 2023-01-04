using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rewired;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace RTM.UISystem
{
	// Token: 0x020004D9 RID: 1241
	public class UIMenu : MonoBehaviour
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00054B78 File Offset: 0x00052F78
		public bool enableTabNavigation
		{
			get
			{
				return this._enableBumperNavigation;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x00054B80 File Offset: 0x00052F80
		public bool wantsBackButton
		{
			get
			{
				return this._wantsBackButton;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x00054B88 File Offset: 0x00052F88
		public bool parentCloseAllowed
		{
			get
			{
				return this._parentCloseAllowed;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x00054B90 File Offset: 0x00052F90
		public bool rememberFocusOnClose
		{
			get
			{
				return this._rememberFocusOnClose;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00054B98 File Offset: 0x00052F98
		public bool keepInFocus
		{
			get
			{
				return this._keepInFocus;
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00054BA0 File Offset: 0x00052FA0
		public IUINavigable GetLastNavigable()
		{
			return this.lastNavigable.Get();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00054BAD File Offset: 0x00052FAD
		public IUINavigable GetCurrentNavigable()
		{
			return this.currentNavigable.Get();
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x00054BBA File Offset: 0x00052FBA
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x00054BC2 File Offset: 0x00052FC2
		public bool isOpen { get; private set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00054BCB File Offset: 0x00052FCB
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x00054BD3 File Offset: 0x00052FD3
		public bool isFocussed { get; private set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00054BDC File Offset: 0x00052FDC
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x00054BE4 File Offset: 0x00052FE4
		public virtual bool blockingInput { get; protected set; }

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06001F8C RID: 8076 RVA: 0x00054BF0 File Offset: 0x00052FF0
		// (remove) Token: 0x06001F8D RID: 8077 RVA: 0x00054C28 File Offset: 0x00053028
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onGainedFocus = delegate()
		{
		};

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06001F8E RID: 8078 RVA: 0x00054C60 File Offset: 0x00053060
		// (remove) Token: 0x06001F8F RID: 8079 RVA: 0x00054C98 File Offset: 0x00053098
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onLostFocus = delegate()
		{
		};

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06001F90 RID: 8080 RVA: 0x00054CD0 File Offset: 0x000530D0
		// (remove) Token: 0x06001F91 RID: 8081 RVA: 0x00054D08 File Offset: 0x00053108
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> onFocusChanged = delegate(bool A_0)
		{
		};

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06001F92 RID: 8082 RVA: 0x00054D40 File Offset: 0x00053140
		// (remove) Token: 0x06001F93 RID: 8083 RVA: 0x00054D78 File Offset: 0x00053178
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onOpened = delegate()
		{
		};

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06001F94 RID: 8084 RVA: 0x00054DB0 File Offset: 0x000531B0
		// (remove) Token: 0x06001F95 RID: 8085 RVA: 0x00054DE8 File Offset: 0x000531E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onClosed = delegate()
		{
		};

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06001F96 RID: 8086 RVA: 0x00054E20 File Offset: 0x00053220
		// (remove) Token: 0x06001F97 RID: 8087 RVA: 0x00054E58 File Offset: 0x00053258
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IUINavigable> onFocusedNavigableChanged = delegate(IUINavigable A_0)
		{
		};

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06001F98 RID: 8088 RVA: 0x00054E90 File Offset: 0x00053290
		// (remove) Token: 0x06001F99 RID: 8089 RVA: 0x00054EC8 File Offset: 0x000532C8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IUINavigable, bool> forceScrollToNavigable = delegate(IUINavigable A_0, bool A_1)
		{
		};

		// Token: 0x06001F9A RID: 8090 RVA: 0x00054EFE File Offset: 0x000532FE
		public virtual void OpenMenu()
		{
			this.isOpen = true;
			Singleton<UIManager>.instance.Add(this);
			this.openTime = FrameTimeStamp.now;
			this.onOpened();
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00054F28 File Offset: 0x00053328
		public virtual void CloseMenu()
		{
			if (Singleton<UIManager>.instance)
			{
				Singleton<UIManager>.instance.Remove(this, this._autoCloseChildren);
			}
			this.isOpen = false;
			if (!this._rememberFocusOnClose)
			{
				this.ResetLastNavigable();
			}
			this.closeTime = FrameTimeStamp.now;
			this.onClosed();
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00054F83 File Offset: 0x00053383
		protected virtual void OnGainedFocus()
		{
			this.gainedFocusTime = FrameTimeStamp.now;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00054F90 File Offset: 0x00053390
		protected virtual void OnLostFocus()
		{
			this.lostFocusTime = FrameTimeStamp.now;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00054F9D File Offset: 0x0005339D
		public virtual bool HandleBackButton()
		{
			this.CloseMenu();
			FabricWrapper.PostEvent(FabricID.uiBack);
			return true;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00054FB4 File Offset: 0x000533B4
		public void SetFocussed(bool focus)
		{
			if (focus)
			{
				this.isFocussed = true;
				this.OnGainedFocus();
				this.onGainedFocus();
			}
			else
			{
				this.FocusOn(null);
				this.isFocussed = false;
				this.OnLostFocus();
				this.onLostFocus();
			}
			this.onFocusChanged(focus);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00055010 File Offset: 0x00053410
		public void OnControllerTypeChanged(ControllerType controllerType)
		{
			switch (controllerType)
			{
			case ControllerType.Keyboard:
				return;
			case ControllerType.Joystick:
				if (!this.blockingInput)
				{
					this.FocusOn(this.GetDefaultNavigable());
				}
				return;
			}
			this.FocusOn(null);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00055061 File Offset: 0x00053461
		public virtual void Navigate(Vector2 navigationNormal)
		{
			if (this.GetCurrentNavigable() == null)
			{
				this.FocusDefault();
			}
			else if (!this.currentNavigable.Get().ConsumeNavigation(navigationNormal))
			{
				this.MoveFocus(navigationNormal);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00055096 File Offset: 0x00053496
		public void OnUIActionPressed(EUIPadAction action)
		{
			if (!this.TryTriggerActionListener(action))
			{
				this.HandleSpecialAction(action);
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000550AC File Offset: 0x000534AC
		private bool HandleSpecialAction(EUIPadAction action)
		{
			switch (action)
			{
			case EUIPadAction.Submit:
				if (this.currentNavigable)
				{
					this.currentNavigable.Get().Click();
				}
				else
				{
					this.FocusDefault();
				}
				return true;
			case EUIPadAction.Cancel:
				if (this.wantsBackButton)
				{
					this.HandleBackButton();
					return true;
				}
				break;
			case EUIPadAction.TabRight:
			case EUIPadAction.TabLeft:
				if (this.enableTabNavigation)
				{
					this.MoveFocus((action != EUIPadAction.TabLeft) ? Vector2.right : Vector2.left);
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00055154 File Offset: 0x00053554
		private bool TryTriggerActionListener(EUIPadAction action)
		{
			this.RefreshActionListeners();
			foreach (UIActionListener uiactionListener in this.actionListeners)
			{
				if (uiactionListener.isActiveAndEnabled && uiactionListener.action == action)
				{
					uiactionListener.ReceiveAction();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000551D8 File Offset: 0x000535D8
		private void MoveFocus(Vector2 newInput)
		{
			if (!this.currentNavigable)
			{
				this.FocusOn(this.GetDefaultNavigable());
				return;
			}
			this.RefreshNavigables();
			Vector2 currentPos = this.currentNavigable.Get().transform.position;
			Vector2 normalized = newInput.normalized;
			float num = float.MinValue;
			IUINavigable iuinavigable = null;
			foreach (WeakInterfaceReference<IUINavigable> weakInterfaceReference in this.navigables)
			{
				IUINavigable iuinavigable2 = weakInterfaceReference.Get();
				if (iuinavigable2 != null && iuinavigable2 != this.currentNavigable.Get() && iuinavigable2.isNavigable)
				{
					Vector3 position = iuinavigable2.transform.position;
					float weight = UIMenu.GetWeight(currentPos, position, normalized);
					if (weight > num)
					{
						iuinavigable = iuinavigable2;
						num = weight;
					}
				}
			}
			if (iuinavigable != null)
			{
				this.FocusOn(iuinavigable);
			}
			else if (!this.HandleNavigationFail(this.currentNavigable.Get(), newInput))
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00055310 File Offset: 0x00053710
		protected virtual bool HandleNavigationFail(IUINavigable currentNavigable, Vector2 direction)
		{
			return false;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00055313 File Offset: 0x00053713
		protected void RefreshNavigables()
		{
			base.transform.GetComponentsInChildren<IUINavigable>(true, UIMenu.navigablesRaw);
			WeakInterfaceReference<IUINavigable>.Convert(UIMenu.navigablesRaw, this.navigables);
			UIMenu.navigablesRaw.Clear();
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00055340 File Offset: 0x00053740
		private void RefreshActionListeners()
		{
			base.transform.GetComponentsInChildren<UIActionListener>(true, this.actionListeners);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00055354 File Offset: 0x00053754
		private void Refresh()
		{
			this.RefreshNavigables();
			this.RefreshActionListeners();
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00055364 File Offset: 0x00053764
		private static float GetWeight(Vector2 currentPos, Vector2 candidatePos, Vector2 stickInputNormal)
		{
			float num = 2.1474836E+09f;
			if (num > 1f)
			{
				num = Mathf.Cos(1.0471976f);
			}
			Vector2 normalized = (candidatePos - currentPos).normalized;
			float num2 = Vector2.Dot(normalized, stickInputNormal);
			float num3 = Vector2.Distance(currentPos, candidatePos);
			if (num2 < num || num3 > 3.4028235E+38f || num3 < 0.001f)
			{
				return float.MinValue;
			}
			num2 = Mathf.Pow(num2, 3f);
			return num2 / num3;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000553E3 File Offset: 0x000537E3
		public void ScrollTo(IUINavigable navigable, bool snap)
		{
			this.forceScrollToNavigable(navigable, snap);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000553F4 File Offset: 0x000537F4
		protected void FocusOn(IUINavigable newNavigable)
		{
			if (newNavigable == this.currentNavigable.Get())
			{
				return;
			}
			if (this.currentNavigable)
			{
				this.currentNavigable.Get().SetFocus(false, null);
			}
			this.lastNavigable = this.currentNavigable;
			if (newNavigable != null)
			{
				newNavigable.SetFocus(true, this.lastNavigable.Get());
			}
			this.currentNavigable = new WeakInterfaceReference<IUINavigable>(newNavigable);
			this.onFocusedNavigableChanged(newNavigable);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00055474 File Offset: 0x00053874
		protected virtual IUINavigable GetDefaultNavigable()
		{
			this.RefreshNavigables();
			if (this.currentNavigable && this.currentNavigable.Get().isNavigable)
			{
				return this.currentNavigable.Get();
			}
			if (this.lastNavigable && this.lastNavigable.Get().isNavigable)
			{
				return this.lastNavigable.Get();
			}
			return this.GetFirstNavigable(false);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000554F0 File Offset: 0x000538F0
		protected IUINavigable GetFirstNavigable()
		{
			return this.GetFirstNavigable(true);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000554FC File Offset: 0x000538FC
		protected IUINavigable GetFirstNavigable(bool refresh)
		{
			if (refresh)
			{
				this.RefreshNavigables();
			}
			foreach (WeakInterfaceReference<IUINavigable> weakRef in this.navigables)
			{
				if (weakRef && weakRef.Get().isNavigable)
				{
					return weakRef.Get();
				}
			}
			return null;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005558C File Offset: 0x0005398C
		public void FocusDefault()
		{
			if (this.blockingInput)
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
			else
			{
				this.FocusOn(this.GetDefaultNavigable());
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000555B5 File Offset: 0x000539B5
		protected void ResetLastNavigable()
		{
			this.lastNavigable = null;
		}

		// Token: 0x04001393 RID: 5011
		private const int poolSize = 32;

		// Token: 0x04001394 RID: 5012
		[Header("Navigation Options")]
		[SerializeField]
		[Tooltip("Tabs / bumpers will navigate horizontally")]
		private bool _enableBumperNavigation;

		// Token: 0x04001395 RID: 5013
		[SerializeField]
		[Tooltip("display common back button")]
		protected bool _wantsBackButton = true;

		// Token: 0x04001396 RID: 5014
		[SerializeField]
		[Tooltip("Closing closes Children too")]
		private bool _autoCloseChildren;

		// Token: 0x04001397 RID: 5015
		[SerializeField]
		[Tooltip("Can be auto closed by parent")]
		protected bool _parentCloseAllowed = true;

		// Token: 0x04001398 RID: 5016
		[SerializeField]
		[Tooltip("When closing the menu, should it remember it's last focussed navigable?")]
		private bool _rememberFocusOnClose;

		// Token: 0x04001399 RID: 5017
		[SerializeField]
		private bool _keepInFocus;

		// Token: 0x0400139A RID: 5018
		[SerializeField]
		protected ScrollRect scrollRect;

		// Token: 0x0400139B RID: 5019
		private static List<IUINavigable> navigablesRaw = new List<IUINavigable>(32);

		// Token: 0x0400139C RID: 5020
		protected List<WeakInterfaceReference<IUINavigable>> navigables = new List<WeakInterfaceReference<IUINavigable>>(32);

		// Token: 0x0400139D RID: 5021
		private List<UIActionListener> actionListeners = new List<UIActionListener>(32);

		// Token: 0x0400139E RID: 5022
		protected WeakInterfaceReference<IUINavigable> currentNavigable = null;

		// Token: 0x0400139F RID: 5023
		protected WeakInterfaceReference<IUINavigable> lastNavigable = null;

		// Token: 0x040013AA RID: 5034
		public FrameTimeStamp openTime = FrameTimeStamp.negativeInfinity;

		// Token: 0x040013AB RID: 5035
		public FrameTimeStamp gainedFocusTime = FrameTimeStamp.negativeInfinity;

		// Token: 0x040013AC RID: 5036
		public FrameTimeStamp lostFocusTime = FrameTimeStamp.negativeInfinity;

		// Token: 0x040013AD RID: 5037
		public FrameTimeStamp closeTime = FrameTimeStamp.negativeInfinity;
	}
}
