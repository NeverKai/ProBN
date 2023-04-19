using System;
using System.Diagnostics;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense;

namespace RTM.UISystem
{
	// Token: 0x020004DD RID: 1245
	public class UIPointerReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDropHandler, IEventSystemHandler
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00055DB4 File Offset: 0x000541B4
		static UIPointerReceiver()
		{
			Func<UserSettings, bool> settingsTouch = (UserSettings s) => s && s.cursorBehaviour == UserSettings.CursorBehaviour.Touch;
			UserSettings.onUpdated += delegate(UserSettings s)
			{
				UIPointerReceiver.isTouch = settingsTouch(s);
			};
			UIPointerReceiver.isTouch = settingsTouch(Profile.userSettings);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00055E9A File Offset: 0x0005429A
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x00055EA2 File Offset: 0x000542A2
		public UIMenu menu { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00055EAB File Offset: 0x000542AB
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x00055EB3 File Offset: 0x000542B3
		public UIPointerReceiver.State state { get; private set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00055EBC File Offset: 0x000542BC
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x00055EC4 File Offset: 0x000542C4
		public PointerEventData.InputButton inputButton { get; private set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00055ECD File Offset: 0x000542CD
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x00055ED5 File Offset: 0x000542D5
		public bool unblocked { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x00055EDE File Offset: 0x000542DE
		public bool menuBlockingInput
		{
			get
			{
				return this.menu && this.menu.blockingInput;
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06001FEB RID: 8171 RVA: 0x00055F00 File Offset: 0x00054300
		// (remove) Token: 0x06001FEC RID: 8172 RVA: 0x00055F38 File Offset: 0x00054338
		
		public event Action<UIPointerReceiver.State> onStateChanged = delegate(UIPointerReceiver.State A_0)
		{
		};

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06001FED RID: 8173 RVA: 0x00055F70 File Offset: 0x00054370
		// (remove) Token: 0x06001FEE RID: 8174 RVA: 0x00055FA8 File Offset: 0x000543A8
		
		public event Action<PointerEventData.InputButton, Vector2> onButtonDown = delegate(PointerEventData.InputButton A_0, Vector2 A_1)
		{
		};

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06001FEF RID: 8175 RVA: 0x00055FE0 File Offset: 0x000543E0
		// (remove) Token: 0x06001FF0 RID: 8176 RVA: 0x00056018 File Offset: 0x00054418
		
		public event Action<PointerEventData.InputButton, Vector2> onClick = delegate(PointerEventData.InputButton A_0, Vector2 A_1)
		{
		};

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00056050 File Offset: 0x00054450
		private void SetState(UIPointerReceiver.State state)
		{
			if (this.menuBlockingInput)
			{
				state = UIPointerReceiver.State.None;
			}
			UIPointerReceiver.State state2 = this.state;
			this.state = state;
			if (state != state2)
			{
				this.onStateChanged(state);
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0005608C File Offset: 0x0005448C
		public bool Is(UIPointerReceiver.State state)
		{
			return this.state == state;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00056097 File Offset: 0x00054497
		public bool Is(PointerEventData.InputButton button)
		{
			return this.state >= UIPointerReceiver.State.ButtonDown && this.inputButton == button;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000560B1 File Offset: 0x000544B1
		private void Awake()
		{
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
			this.menu = this.GetDisabledComponentInParent<UIMenu>();
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000560D0 File Offset: 0x000544D0
		private void OnDestroy()
		{
			InputHelpers.onControllerTypeChanged -= this.OnControllerTypeChanged;
			this.menu = null;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000560EA File Offset: 0x000544EA
		private void OnControllerTypeChanged(ControllerType type)
		{
			if (type == ControllerType.Joystick)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000560FA File Offset: 0x000544FA
		private void OnDisable()
		{
			this.SetState(UIPointerReceiver.State.None);
			this.unblocked = false;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0005610C File Offset: 0x0005450C
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.menuBlockingInput)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this.state < UIPointerReceiver.State.ButtonDown)
			{
				this.SetState(UIPointerReceiver.State.ButtonDown);
				this.inputButton = inputButton;
				this.onButtonDown(this.inputButton, eventData.position);
			}
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00056168 File Offset: 0x00054568
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (this.menuBlockingInput)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this.state == UIPointerReceiver.State.ButtonDown)
			{
				this.clickAvailableFrame = Time.frameCount;
			}
			this.SetState(UIPointerReceiver.State.None);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000561B4 File Offset: 0x000545B4
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (this.menuBlockingInput)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this.clickAvailableFrame == Time.frameCount && this.inputButton == inputButton)
			{
				this.SetState((!this.unblocked || UIPointerReceiver.isTouch) ? UIPointerReceiver.State.None : UIPointerReceiver.State.Hover);
				this.onClick(this.inputButton, eventData.position);
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00056234 File Offset: 0x00054634
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (this.menuBlockingInput)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			if (eventData.eligibleForClick || eventData.dragging)
			{
				return;
			}
			if (InputHelpers.ControllerTypeIs(ControllerType.Joystick))
			{
				return;
			}
			this.unblocked = true;
			if (!UIPointerReceiver.isTouch)
			{
				this.SetState(UIPointerReceiver.State.Hover);
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0005628E File Offset: 0x0005468E
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (this.menuBlockingInput)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			this.unblocked = false;
			this.SetState(UIPointerReceiver.State.None);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000562B0 File Offset: 0x000546B0
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (this.menuBlockingInput || UIPointerReceiver.isTouch)
			{
				this.SetState(UIPointerReceiver.State.None);
			}
			else
			{
				this.SetState(UIPointerReceiver.State.Hover);
			}
		}

		// Token: 0x040013CC RID: 5068
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("UIPointerRationalizer", EVerbosity.Quiet, 100);

		// Token: 0x040013D1 RID: 5073
		private int clickAvailableFrame = -2147483647;

		// Token: 0x040013D5 RID: 5077
		private static bool isTouch;

		// Token: 0x020004DE RID: 1246
		public enum State
		{
			// Token: 0x040013DA RID: 5082
			None,
			// Token: 0x040013DB RID: 5083
			Hover,
			// Token: 0x040013DC RID: 5084
			ButtonDown
		}
	}
}
