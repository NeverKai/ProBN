using System;
using System.Collections.Generic;
using System.Diagnostics;
using CS.Platform;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense;

namespace RTM.UISystem
{
	// Token: 0x020004D8 RID: 1240
	public class UIManager : Singleton<UIManager>, IGameSetup
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00054210 File Offset: 0x00052610
		public UIMenu activeMenu
		{
			get
			{
				return this.menuStack.Last<UIMenu>();
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06001F61 RID: 8033 RVA: 0x00054220 File Offset: 0x00052620
		// (remove) Token: 0x06001F62 RID: 8034 RVA: 0x00054258 File Offset: 0x00052658
		
		public event Action<UIMenu> onActiveMenuChanged = delegate(UIMenu A_0)
		{
		};

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0005428E File Offset: 0x0005268E
		private bool receivingInput
		{
			get
			{
				return this.activeMenu && !this.activeMenu.blockingInput && !ConsoleStatus.IsConsoleOpen();
			}
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000542BB File Offset: 0x000526BB
		void IGameSetup.OnGameAwake()
		{
			this.pcControllerIcons = this.xboxPadIcons;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000542CC File Offset: 0x000526CC
		protected override void Awake()
		{
			base.Awake();
			Player player = ReInput.players.GetPlayer(0);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.Submit);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiSubmit);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.Secondary);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiSecondary);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.Tertiary);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiTertiary);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.Cancel);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiCancel);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.TabLeft);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiTabLeft);
			player.AddInputEventDelegate(delegate(InputActionEventData data)
			{
				this.OnUIAction(EUIPadAction.TabRight);
			}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.uiTabRight);
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.UpdateHorizontal), UpdateLoopType.Update, InputActionEventType.AxisActive, this.uiHorizontal);
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.UpdateVertical), UpdateLoopType.Update, InputActionEventType.AxisActive, this.uiVertical);
			this.pcControllerIcons = this.xboxPadIcons;
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
			this.OnControllerTypeChanged(InputHelpers.GetControllerType());
			PlatformEvents.OnPlatformInitializedEvent += this.CheckInvertedSelect;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0005441E File Offset: 0x0005281E
		private void OnDestroy()
		{
			InputHelpers.onControllerTypeChanged -= this.OnControllerTypeChanged;
			PlatformEvents.OnPlatformInitializedEvent -= this.CheckInvertedSelect;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00054444 File Offset: 0x00052844
		private void Update()
		{
			for (int i = this.menuStack.Count - 1; i >= 0; i--)
			{
			}
			if (this.invalidateInput)
			{
				if (this.pendingInput == Vector2.zero)
				{
					this.invalidateInput = false;
				}
				this.pendingInput = Vector2.zero;
			}
			if (!this.receivingInput)
			{
				this.invalidateInput = true;
			}
			else if (this.pendingInput.sqrMagnitude > 0.25f)
			{
				if (this.previousInput.magnitude <= 0.5f)
				{
					this.Navigate(this.pendingInput);
				}
				else if (Time.unscaledTime - this.lastNavTime > this.repeatTime)
				{
					this.Navigate(this.pendingInput);
					this.repeatTime = Mathf.Lerp(this.repeatTime, 0.075f, 0.4f);
				}
			}
			else
			{
				this.repeatTime = 0.3f;
			}
			if (this.receivingInput && this.activeMenu && this.activeMenu.GetCurrentNavigable() == null && InputHelpers.ControllerTypeIs(ControllerType.Joystick))
			{
				this.activeMenu.FocusDefault();
			}
			this.previousInput = this.pendingInput;
			this.pendingInput = Vector3.zero;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000545A0 File Offset: 0x000529A0
		private void CheckInvertedSelect()
		{
			if (InputHelpers.IsSelectInverted())
			{
				RewiredHelpers.SwapActions(ReInput.players.GetPlayer(0), this.uiSubmit, this.uiCancel);
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000545D4 File Offset: 0x000529D4
		private void Navigate(Vector2 input)
		{
			this.activeMenu.Navigate(input.normalized);
			IUINavigable currentNavigable = this.activeMenu.GetCurrentNavigable();
			if (currentNavigable == null || !currentNavigable.allowRepeatNavigation)
			{
				this.invalidateInput = true;
			}
			this.lastNavTime = Time.unscaledTime;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00054624 File Offset: 0x00052A24
		private void OnControllerTypeChanged(ControllerType controllerType)
		{
			if (controllerType == ControllerType.Joystick)
			{
				this.invalidateInput = true;
				Joystick joystick = InputHelpers.GetController() as Joystick;
				if (joystick != null)
				{
					this.pcControllerIcons = this.GetIconsForJoystick(joystick.hardwareTypeGuid);
				}
			}
			if (this.activeMenu)
			{
				this.activeMenu.OnControllerTypeChanged(controllerType);
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0005467E File Offset: 0x00052A7E
		private void UpdateHorizontal(InputActionEventData data)
		{
			if (this.blockUIInput)
			{
				return;
			}
			this.pendingInput.x = data.GetAxis();
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0005469E File Offset: 0x00052A9E
		private void UpdateVertical(InputActionEventData data)
		{
			if (this.blockUIInput)
			{
				return;
			}
			this.pendingInput.y = data.GetAxis();
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000546C0 File Offset: 0x00052AC0
		private void OnUIAction(EUIPadAction action)
		{
			if (this.blockUIInput)
			{
				return;
			}
			if (this.receivingInput)
			{
				this.activeMenu.OnUIActionPressed(action);
			}
			else if (this.activeMenu && !ConsoleStatus.IsConsoleOpen())
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0005471A File Offset: 0x00052B1A
		public GamepadIconCollection GetIconCollection()
		{
			return this.pcControllerIcons;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00054724 File Offset: 0x00052B24
		private GamepadIconCollection GetIconsForJoystick(Guid joystickGuid)
		{
			if (joystickGuid == Guid.Empty)
			{
				return this.xboxPadIcons;
			}
			string a = joystickGuid.ToString();
			bool flag = a == "c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb" || a == "71dfe6c8-9e81-428f-a58e-c7e664b7fbed" || a == "cd9718bf-a87a-44bc-8716-60a0def28a9f";
			if (flag)
			{
				return this.playstationPadIcons;
			}
			bool flag2 = a == "521b808c-0248-4526-bc10-f1d16ee76bf1" || a == "7bf3154b-9db8-4d52-950f-cd0eed8a5819";
			if (flag2)
			{
				return this.switchPadIcons;
			}
			return this.xboxPadIcons;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000547C4 File Offset: 0x00052BC4
		public Sprite GetActionIcon(EUIPadAction action)
		{
			return this.GetIconCollection().GetSpriteFor(action);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000547D2 File Offset: 0x00052BD2
		public bool StackContains(UIMenu menu)
		{
			return this.menuStack.Contains(menu);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000547E0 File Offset: 0x00052BE0
		public void Add(UIMenu menu)
		{
			if (this.activeMenu && this.activeMenu.keepInFocus && !menu.keepInFocus)
			{
				int i;
				for (i = this.menuStack.Count; i > 0; i--)
				{
					if (!this.menuStack[i - 1].keepInFocus)
					{
						break;
					}
				}
				this.menuStack.Insert(i, menu);
			}
			else
			{
				if (this.activeMenu)
				{
					this.activeMenu.SetFocussed(false);
				}
				this.menuStack.Add(menu);
				menu.SetFocussed(true);
				this.onActiveMenuChanged(menu);
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000548A0 File Offset: 0x00052CA0
		public void CloseAll()
		{
			for (int i = this.menuStack.Count - 1; i >= 0; i--)
			{
				this.menuStack[i].CloseMenu();
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000548DC File Offset: 0x00052CDC
		public void Remove(UIMenu menu, bool includeChildren = false)
		{
			if (includeChildren)
			{
				this.RemoveWithChildren(menu);
			}
			else
			{
				this.RemoveSingle(menu);
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000548F8 File Offset: 0x00052CF8
		private void RemoveWithChildren(UIMenu menu)
		{
			for (int i = this.menuStack.Count - 1; i >= 0; i--)
			{
				UIMenu uimenu = this.menuStack[i];
				if (uimenu == menu)
				{
					this.RemoveSingle(menu);
					return;
				}
				if (uimenu.parentCloseAllowed)
				{
					uimenu.CloseMenu();
				}
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00054958 File Offset: 0x00052D58
		private void RemoveSingle(UIMenu menu)
		{
			if (menu == this.activeMenu)
			{
				menu.SetFocussed(false);
				this.menuStack.Remove(menu);
				if (this.activeMenu)
				{
					this.activeMenu.SetFocussed(true);
				}
				this.onActiveMenuChanged(this.activeMenu);
			}
			else
			{
				this.menuStack.Remove(menu);
			}
		}

		// Token: 0x04001379 RID: 4985
		private const float maxRepeatTime = 0.3f;

		// Token: 0x0400137A RID: 4986
		private const float minRepeatTime = 0.075f;

		// Token: 0x0400137B RID: 4987
		private const float inputMag = 0.5f;

		// Token: 0x0400137C RID: 4988
		private const float inputMagSqr = 0.25f;

		// Token: 0x0400137D RID: 4989
		[SerializeField]
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("UIManager", EVerbosity.Quiet, 100);

		// Token: 0x0400137E RID: 4990
		[SerializeField]
		private GamepadIconCollection xboxPadIcons;

		// Token: 0x0400137F RID: 4991
		[SerializeField]
		private GamepadIconCollection switchPadIcons;

		// Token: 0x04001380 RID: 4992
		[SerializeField]
		private GamepadIconCollection playstationPadIcons;

		// Token: 0x04001381 RID: 4993
		private GamepadIconCollection pcControllerIcons;

		// Token: 0x04001382 RID: 4994
		private List<UIMenu> menuStack = new List<UIMenu>();

		// Token: 0x04001384 RID: 4996
		private RewiredActionReference uiHorizontal = "UIHorizontal";

		// Token: 0x04001385 RID: 4997
		private RewiredActionReference uiVertical = "UIVertical";

		// Token: 0x04001386 RID: 4998
		private RewiredActionReference uiSubmit = "UISubmit";

		// Token: 0x04001387 RID: 4999
		private RewiredActionReference uiSecondary = "UISecondary";

		// Token: 0x04001388 RID: 5000
		private RewiredActionReference uiTertiary = "UITertiary";

		// Token: 0x04001389 RID: 5001
		private RewiredActionReference uiCancel = "UICancel";

		// Token: 0x0400138A RID: 5002
		private RewiredActionReference uiTabLeft = "UITabLeft";

		// Token: 0x0400138B RID: 5003
		private RewiredActionReference uiTabRight = "UITabRight";

		// Token: 0x0400138C RID: 5004
		private bool invalidateInput;

		// Token: 0x0400138D RID: 5005
		private Vector2 previousInput = Vector2.zero;

		// Token: 0x0400138E RID: 5006
		private Vector2 pendingInput = Vector2.zero;

		// Token: 0x0400138F RID: 5007
		private float lastNavTime = float.MinValue;

		// Token: 0x04001390 RID: 5008
		private float repeatTime = 0.3f;

		// Token: 0x04001391 RID: 5009
		public bool blockUIInput;
	}
}
