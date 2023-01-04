using System;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x0200072F RID: 1839
	public class ConfirmButton : MonoBehaviour, IslandGameplayManager.IAwake
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000C3098 File Offset: 0x000C1498
		public bool hasFocus
		{
			get
			{
				return this == ConfirmButton.currentConfirmMono;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000C30A5 File Offset: 0x000C14A5
		public static MonoBehaviour currentConfirmMono
		{
			get
			{
				return ConfirmButton._currentConfirmMono;
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C30AC File Offset: 0x000C14AC
		public static void SetCurrent(MonoBehaviour newFocus)
		{
			if (newFocus != ConfirmButton._currentConfirmMono)
			{
				ConfirmButton confirmButton = ConfirmButton._currentConfirmMono as ConfirmButton;
				ConfirmButton confirmButton2 = newFocus as ConfirmButton;
				ConfirmButton._currentConfirmMono = newFocus;
				if (confirmButton)
				{
					confirmButton.OnLostFocus();
				}
				if (confirmButton2)
				{
					confirmButton2.OnGainedFocus();
				}
			}
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000C3104 File Offset: 0x000C1504
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			PointerRationalizer pointerRationalizer = manager.pointerRationalizer;
			pointerRationalizer.onClick += this.PointerWorldClick;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000C312C File Offset: 0x000C152C
		private void Awake()
		{
			UIPointerReceiver component = base.GetComponent<UIPointerReceiver>();
			if (component)
			{
				component.onClick += this.OnClick;
			}
			else
			{
				Debug.LogWarningFormat("{0} needs a pointer receiver", new object[]
				{
					base.name
				});
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000C317B File Offset: 0x000C157B
		private void OnEnable()
		{
			if (this.toolTip)
			{
				this.toolTip.SetActive(false);
			}
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000C3199 File Offset: 0x000C1599
		private void OnDisable()
		{
			if (this.hasFocus)
			{
				ConfirmButton.SetCurrent(null);
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000C31AC File Offset: 0x000C15AC
		private void OnClick(PointerEventData.InputButton arg1, Vector2 arg2)
		{
			bool hasFocus = this.hasFocus;
			if (hasFocus)
			{
				if (hasFocus)
				{
					this.OnConfirmed();
				}
				ConfirmButton.SetCurrent(null);
			}
			else if (this.interactable)
			{
				ConfirmButton.SetCurrent(this);
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000C31EE File Offset: 0x000C15EE
		private void PointerWorldClick(PointerEventData.InputButton arg1, Vector2 arg2)
		{
			if (this.hasFocus)
			{
				ConfirmButton.SetCurrent(null);
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000C3201 File Offset: 0x000C1601
		protected void SetTooltipVisible(bool visible)
		{
			if (this.toolTip)
			{
				this.toolTip.SetActive(visible);
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000C321F File Offset: 0x000C161F
		protected virtual void OnGainedFocus()
		{
			FabricWrapper.PostEvent("UI/ConfirmButtonAsk");
			this.SetTooltipVisible(true);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000C3233 File Offset: 0x000C1633
		protected virtual void OnLostFocus()
		{
			this.SetTooltipVisible(false);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000C323C File Offset: 0x000C163C
		protected virtual void OnConfirmed()
		{
			FabricWrapper.PostEvent("UI/ConfirmButtonConfirm");
		}

		// Token: 0x04001FE7 RID: 8167
		[SerializeField]
		private bool interactable = true;

		// Token: 0x04001FE8 RID: 8168
		public GameObject toolTip;

		// Token: 0x04001FE9 RID: 8169
		private static MonoBehaviour _currentConfirmMono;

		// Token: 0x04001FEA RID: 8170
		private const string askSound = "UI/ConfirmButtonAsk";

		// Token: 0x04001FEB RID: 8171
		private const string confirmSound = "UI/ConfirmButtonConfirm";
	}
}
