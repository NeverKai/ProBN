using System;
using System.Diagnostics;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004D6 RID: 1238
	public class UIInteractable : MonoBehaviour
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000538C5 File Offset: 0x00051CC5
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x000538D0 File Offset: 0x00051CD0
		public UIInteractable.State state
		{
			get
			{
				return this._state;
			}
			protected set
			{
				UIInteractable.State state = this._state;
				this._state = value;
				if (this._state != state)
				{
					this.onStateChanged(this.state);
					if (this.allowHoverAudio && state == UIInteractable.State.None && this._state == UIInteractable.State.Hover && this.lastChangeFrame < Time.frameCount)
					{
						FabricWrapper.PostEvent((this.hoverAudio != null && !string.IsNullOrEmpty(this.hoverAudio.name)) ? this.hoverAudio : FabricID.uiHover);
					}
					this.lastChangeFrame = Time.frameCount;
				}
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00053976 File Offset: 0x00051D76
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x00053980 File Offset: 0x00051D80
		public bool selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				bool selected = this._selected;
				this._selected = value;
				if (this._selected != selected)
				{
					this.onSelectedChanged(this._selected);
					if (this._selected && this.navigable != null && !this.navigable.hasFocus && base.isActiveAndEnabled)
					{
						FabricWrapper.PostEvent(this.navigable.selectAudio);
					}
				}
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000539FA File Offset: 0x00051DFA
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00053A02 File Offset: 0x00051E02
		public bool disabled
		{
			get
			{
				return this._disabled;
			}
			set
			{
				if (this._disabled == value)
				{
					return;
				}
				this._disabled = value;
				this.onDisabledChanged(this._disabled);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00053A29 File Offset: 0x00051E29
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00053A31 File Offset: 0x00051E31
		public UIPointerReceiver pointer { get; protected set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00053A3A File Offset: 0x00051E3A
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00053A42 File Offset: 0x00051E42
		public IUINavigable navigable { get; protected set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00053A4B File Offset: 0x00051E4B
		private bool joystickActive
		{
			get
			{
				return InputHelpers.ControllerTypeIs(ControllerType.Joystick);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00053A53 File Offset: 0x00051E53
		private bool pointerActive
		{
			get
			{
				return !this.joystickActive;
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06001F4D RID: 8013 RVA: 0x00053A60 File Offset: 0x00051E60
		// (remove) Token: 0x06001F4E RID: 8014 RVA: 0x00053A98 File Offset: 0x00051E98
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<UIInteractable.State> onStateChanged = delegate(UIInteractable.State A_0)
		{
		};

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06001F4F RID: 8015 RVA: 0x00053AD0 File Offset: 0x00051ED0
		// (remove) Token: 0x06001F50 RID: 8016 RVA: 0x00053B08 File Offset: 0x00051F08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> onSelectedChanged = delegate(bool A_0)
		{
		};

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001F51 RID: 8017 RVA: 0x00053B40 File Offset: 0x00051F40
		// (remove) Token: 0x06001F52 RID: 8018 RVA: 0x00053B78 File Offset: 0x00051F78
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> onDisabledChanged = delegate(bool A_0)
		{
		};

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06001F53 RID: 8019 RVA: 0x00053BB0 File Offset: 0x00051FB0
		// (remove) Token: 0x06001F54 RID: 8020 RVA: 0x00053BE8 File Offset: 0x00051FE8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onFlash;

		// Token: 0x06001F55 RID: 8021 RVA: 0x00053C20 File Offset: 0x00052020
		public bool Flash()
		{
			bool flag = this.onFlash != null;
			if (flag)
			{
				this.onFlash();
			}
			else
			{
				UnityEngine.Debug.LogWarning("trying to flash UIInteractible with nothing to handle it");
			}
			return flag;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00053C5B File Offset: 0x0005205B
		public void SetHoverAudio(FabricEventReference hoverAudio)
		{
			this.hoverAudio = hoverAudio;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00053C64 File Offset: 0x00052064
		protected virtual void Awake()
		{
			this.pointer = base.GetComponent<UIPointerReceiver>();
			if (this.pointer)
			{
				this.pointer.onStateChanged += this.OnPointerStateChanged;
			}
			this.navigable = base.GetComponent<IUINavigable>();
			if (this.navigable != null)
			{
				this.navigable.onFocusChanged += this.OnNavigableFocusChanged;
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00053CD2 File Offset: 0x000520D2
		private void OnNavigableFocusChanged(bool hasFocus)
		{
			if (hasFocus && this.state != UIInteractable.State.Focus)
			{
				this.state = UIInteractable.State.Focus;
			}
			if (!hasFocus && this.state == UIInteractable.State.Focus)
			{
				this.state = UIInteractable.State.None;
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00053D06 File Offset: 0x00052106
		private void OnPointerStateChanged(UIPointerReceiver.State pointerState)
		{
			if (base.isActiveAndEnabled && this.state < UIInteractable.State.Focus)
			{
				this.state = this.GetState(pointerState);
			}
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00053D2C File Offset: 0x0005212C
		private UIInteractable.State GetState(UIPointerReceiver.State pointerState)
		{
			switch (pointerState)
			{
			case UIPointerReceiver.State.None:
				return UIInteractable.State.None;
			case UIPointerReceiver.State.Hover:
				return UIInteractable.State.Hover;
			case UIPointerReceiver.State.ButtonDown:
				return UIInteractable.State.PointerButtonDown;
			default:
				throw new NotImplementedException(string.Format("Unhandled pointer state ({0})", pointerState));
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00053D60 File Offset: 0x00052160
		private void OnDisable()
		{
			this.state = UIInteractable.State.None;
			this.selected = false;
		}

		// Token: 0x04001364 RID: 4964
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("UIInteractable", EVerbosity.Quiet, 0);

		// Token: 0x04001365 RID: 4965
		[SerializeField]
		private bool allowHoverAudio = true;

		// Token: 0x04001366 RID: 4966
		private FabricEventReference hoverAudio;

		// Token: 0x04001367 RID: 4967
		private UIInteractable.State _state;

		// Token: 0x04001368 RID: 4968
		private bool _selected;

		// Token: 0x04001369 RID: 4969
		private bool _disabled;

		// Token: 0x0400136A RID: 4970
		private int lastChangeFrame;

		// Token: 0x020004D7 RID: 1239
		public enum State
		{
			// Token: 0x04001375 RID: 4981
			None,
			// Token: 0x04001376 RID: 4982
			Hover,
			// Token: 0x04001377 RID: 4983
			PointerButtonDown,
			// Token: 0x04001378 RID: 4984
			Focus
		}
	}
}
