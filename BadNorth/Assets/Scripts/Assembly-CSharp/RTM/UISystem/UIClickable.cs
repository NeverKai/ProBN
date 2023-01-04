using System;
using System.Diagnostics;
using Rewired;
using RTM.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RTM.UISystem
{
	// Token: 0x020004D4 RID: 1236
	public class UIClickable : UIInteractable
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x00053D78 File Offset: 0x00052178
		public UIClickable()
		{
			this.onClickEvent = new UnityEvent();
			this.onClick = delegate()
			{
			};
			this.onClickFailed = delegate()
			{
			};
			base..ctor();
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06001F2C RID: 7980 RVA: 0x00053DDC File Offset: 0x000521DC
		// (remove) Token: 0x06001F2D RID: 7981 RVA: 0x00053E14 File Offset: 0x00052214
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onClick;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06001F2E RID: 7982 RVA: 0x00053E4C File Offset: 0x0005224C
		// (remove) Token: 0x06001F2F RID: 7983 RVA: 0x00053E84 File Offset: 0x00052284
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onClickFailed;

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00053EBA File Offset: 0x000522BA
		private bool joystickActive
		{
			get
			{
				return InputHelpers.ControllerTypeIs(ControllerType.Joystick);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00053EC2 File Offset: 0x000522C2
		private bool pointerActive
		{
			get
			{
				return !this.joystickActive;
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00053ED0 File Offset: 0x000522D0
		protected override void Awake()
		{
			base.Awake();
			if (base.pointer)
			{
				base.pointer.onClick += this.OnPointerClick;
			}
			if (base.navigable != null)
			{
				base.navigable.onClicked += this.OnNavigableClicked;
			}
			this.actionListener = base.GetComponent<UIActionListener>();
			if (this.actionListener)
			{
				this.actionListener.onActionRecieved += this.OnActionReceived;
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00053F5F File Offset: 0x0005235F
		private void OnPointerClick(PointerEventData.InputButton button, Vector2 pos)
		{
			if (this.pointerActive && button == PointerEventData.InputButton.Left)
			{
				this.OnClick();
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00053F78 File Offset: 0x00052378
		private void OnNavigableClicked()
		{
			this.OnClick();
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00053F80 File Offset: 0x00052380
		private void OnActionReceived()
		{
			this.OnClick();
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00053F88 File Offset: 0x00052388
		private void OnClick()
		{
			if (base.disabled)
			{
				FabricWrapper.PostEvent(FabricID.uiError);
				this.onClickFailed();
				return;
			}
			if (base.isActiveAndEnabled)
			{
				this.onClick();
				this.onClickEvent.Invoke();
			}
		}

		// Token: 0x0400135F RID: 4959
		protected UIActionListener actionListener;
	}
}
