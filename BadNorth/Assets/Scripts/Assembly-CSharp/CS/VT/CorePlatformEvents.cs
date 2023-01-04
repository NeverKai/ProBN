using System;
using CS.Platform;
using CS.Platform.Utils;
using I2.Loc;
using Rewired.Integration.UnityUI;
using RTM.UISystem;
using Voxels.TowerDefense;
using Voxels.TowerDefense.UI;

namespace CS.VT
{
	// Token: 0x02000385 RID: 901
	public class CorePlatformEvents : PlatformSystemMessenger
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x00029FF8 File Offset: 0x000283F8
		private void Awake()
		{
			PlatformSystemMessenger.SYSTEM_MESSAGE_YES = "UI/COMMON/YES";
			PlatformSystemMessenger.SYSTEM_MESSAGE_NO = "UI/COMMON/NO";
			PlatformSystemMessenger.SYSTEM_MESSAGE_OK = "UI/COMMON/OK";
			PlatformEvents.OnMainUserStateEvent += this.PlatformEvents_OnMainUserStateEvent;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0002A029 File Offset: 0x00028429
		private void OnDestroy()
		{
			PlatformEvents.OnMainUserStateEvent -= this.PlatformEvents_OnMainUserStateEvent;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0002A03C File Offset: 0x0002843C
		protected override void DiscardMessage()
		{
			if (this._activeModal != null)
			{
				this._activeModal.onLostFocus -= this.CorePlatformEvents_onLostFocus;
				this._activeModal.Dismiss();
				this._activeModal = null;
			}
			base.DiscardMessage();
			if (this._activeModal != null)
			{
				RewiredStandaloneInputModule.systemBlocked = this.wasSystemBlocked;
				Singleton<UIManager>.instance.blockUIInput = this.wasSystemBlocked;
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0002A0B6 File Offset: 0x000284B6
		protected override void ShowMessage()
		{
			base.ShowMessage();
			this.GenerateMessage();
			if (this._activeMessage == null)
			{
				this.wasSystemBlocked = Singleton<UIManager>.instance.blockUIInput;
			}
			RewiredStandaloneInputModule.systemBlocked = false;
			Singleton<UIManager>.instance.blockUIInput = false;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0002A0F0 File Offset: 0x000284F0
		private void GenerateMessage()
		{
			if (this._activeMessage != null)
			{
				Debug.LogInfo("[CPE] Showing Message: {0}", new object[]
				{
					this._activeMessage.messageBody
				});
				this._activeModal = ModalOverlay.GetInstance().InitializeNonLocalized(LocalizationManager.GetTranslation("SYSTEM/TITLE_WARNING", true, 0, true, false, null, null), this._activeMessage.messageBody, false);
				this._activeModal.onLostFocus += this.CorePlatformEvents_onLostFocus;
				this._activeModal.AddButton(this._activeMessage.optionAText, new Func<bool>(this.TriggerOptionA), null, null);
				if (this._activeMessage.HasOptionB)
				{
					this._activeModal.AddButton(this._activeMessage.optionBText, new Func<bool>(this.TriggerOptionB), null, null);
				}
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0002A1C4 File Offset: 0x000285C4
		private void CorePlatformEvents_onLostFocus()
		{
			if (this._activeModal != null)
			{
				Debug.LogInfo("[CPE] Regenerating Message: {0}", new object[]
				{
					this._activeMessage.messageBody
				});
				this._activeModal.onLostFocus -= this.CorePlatformEvents_onLostFocus;
				this.GenerateMessage();
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0002A21D File Offset: 0x0002861D
		private bool TriggerOptionA()
		{
			if (this._activeModal != null)
			{
				this._activeModal.onLostFocus -= this.CorePlatformEvents_onLostFocus;
			}
			base.ActivateOption(0);
			return true;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0002A24F File Offset: 0x0002864F
		private bool TriggerOptionB()
		{
			if (this._activeModal != null)
			{
				this._activeModal.onLostFocus -= this.CorePlatformEvents_onLostFocus;
			}
			base.ActivateOption(1);
			return true;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0002A284 File Offset: 0x00028684
		private void PlatformEvents_OnMainUserStateEvent(bool effect)
		{
			if (BasePlatformManager.Instance.MainUserID < 0)
			{
				return;
			}
			if (Singleton<Stack>.instance.stateMeta.active)
			{
				return;
			}
			if (!effect)
			{
				BasePlatformManager.Instance.ShowSystemMessageOK(LocalizationManager.GetTranslation("SYSTEM/USER/MAIN_LOST", true, 0, true, false, null, null), new Action(this.SignoutOkButton));
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0002A2E4 File Offset: 0x000286E4
		private void SignoutOkButton()
		{
			if (!BasePlatformManager.Instance.MainUserSignedIn)
			{
				CorePlatformUtils.ForcePrimUserOut();
			}
		}

		// Token: 0x04000CD3 RID: 3283
		private ModalOverlay _activeModal;

		// Token: 0x04000CD4 RID: 3284
		private bool wasSystemBlocked;

		// Token: 0x04000CD5 RID: 3285
		public RewiredStandaloneInputModule inputManager;
	}
}
