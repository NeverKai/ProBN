using System;
using Rewired;
using RTM.Input;
using UnityEngine;
using Voxels.TowerDefense.UI;

namespace RTM.UISystem
{
	// Token: 0x020004D5 RID: 1237
	public class UICommonBackButton : MonoBehaviour
	{
		// Token: 0x06001F3A RID: 7994 RVA: 0x00053FE4 File Offset: 0x000523E4
		private void Awake()
		{
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
			this.manager.onActiveMenuChanged += this.OnActiveMenuChanged;
			this.OnActiveMenuChanged(this.manager.activeMenu);
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0005404F File Offset: 0x0005244F
		private void OnControllerTypeChanged(ControllerType controllerType)
		{
			this.updateVisibility(this.manager.activeMenu, controllerType);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00054063 File Offset: 0x00052463
		private void OnActiveMenuChanged(UIMenu menu)
		{
			this.updateVisibility(menu, InputHelpers.GetControllerType());
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00054074 File Offset: 0x00052474
		private void updateVisibility(UIMenu menu, ControllerType controllerType)
		{
			bool visible = menu && menu.wantsBackButton && this.IsAcceptableInputMethod(controllerType);
			this.visibility.SetVisible(visible, false);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000540AF File Offset: 0x000524AF
		private bool IsAcceptableInputMethod(ControllerType controllerType)
		{
			return true;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000540B4 File Offset: 0x000524B4
		public void HandleClick()
		{
			bool flag = this.manager.activeMenu && this.manager.activeMenu.HandleBackButton();
			if (flag)
			{
				FabricWrapper.PostEvent(FabricID.uiBack);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x04001362 RID: 4962
		[SerializeField]
		private UIManager manager;

		// Token: 0x04001363 RID: 4963
		private IUIVisibility visibility;
	}
}
