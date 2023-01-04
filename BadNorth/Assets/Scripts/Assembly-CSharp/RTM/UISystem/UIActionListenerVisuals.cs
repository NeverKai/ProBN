using System;
using Rewired;
using RTM.Input;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense;
using Voxels.TowerDefense.ScriptAnimations;

namespace RTM.UISystem
{
	// Token: 0x020004D3 RID: 1235
	public class UIActionListenerVisuals : MonoBehaviour
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00053628 File Offset: 0x00051A28
		public AnimatedState visible
		{
			get
			{
				if (this._visible == null)
				{
					this._visible = new AnimatedState("Visible", this.stateRoot.rootState, false, false);
					this._visible.Subscribe(this.iconImage);
					this._visible.anim.Subscribe(delegate(float x)
					{
						this.iconImage.transform.localScale = (Vector2.one * Mathf.Lerp(0.8f, 1f, x)).SetZ(this.iconImage.transform.localScale.z);
					});
				}
				return this._visible;
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00053690 File Offset: 0x00051A90
		private void OnEnable()
		{
			this.UpdateIconVisibility(true);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0005369C File Offset: 0x00051A9C
		private void Start()
		{
			this.owner = base.transform.GetComponentInParent<UIMenu>();
			if (this.owner)
			{
				this.owner.onFocusChanged += delegate(bool x)
				{
					this.UpdateIconVisibility(false);
				};
			}
			this.actionListener = base.GetComponent<UIActionListener>();
			this.actionListener.onActionRecieved += this.OnActionRecieved;
			this.SetIcon(this.actionListener.action);
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
			this.OnControllerTypeChanged(InputHelpers.GetControllerType());
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00053731 File Offset: 0x00051B31
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0005373E File Offset: 0x00051B3E
		private void OnControllerTypeChanged(ControllerType type)
		{
			this.UpdateIconVisibility(false);
			this.SetIcon(this.actionListener.action);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00053758 File Offset: 0x00051B58
		private void UpdateIconVisibility(bool snap)
		{
			bool active = this.owner && (Platform.Is(EPlatform.Console) || InputHelpers.ControllerTypeIs(ControllerType.Joystick));
			this.visible.SetActive(active);
			if (snap)
			{
				this.visible.anim.ForceToTarget();
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000537B4 File Offset: 0x00051BB4
		private void OnActionRecieved()
		{
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000537B6 File Offset: 0x00051BB6
		private void SetIcon(EUIPadAction action)
		{
			this.iconImage.sprite = Singleton<UIManager>.instance.GetActionIcon(action);
		}

		// Token: 0x04001357 RID: 4951
		[SerializeField]
		private Image iconImage;

		// Token: 0x04001358 RID: 4952
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04001359 RID: 4953
		private UIMenu owner;

		// Token: 0x0400135A RID: 4954
		private UIActionListener actionListener;

		// Token: 0x0400135B RID: 4955
		private AnimatedState _visible;
	}
}
