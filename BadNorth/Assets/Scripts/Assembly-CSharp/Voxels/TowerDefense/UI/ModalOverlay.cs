using System;
using I2.Loc;
using RTM;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008BD RID: 2237
	public class ModalOverlay : GeneratedMenu, IPoolable
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x00105919 File Offset: 0x00103D19
		public static ModalOverlay GetInstance()
		{
			return ModalOverlayPool.GetInstance();
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00105920 File Offset: 0x00103D20
		public ModalOverlay Initialize(string titleLocTerm, string messageLocTerm, bool cancelable)
		{
			this.titleLocalize.Term = titleLocTerm;
			this.messageLocalize.Term = messageLocTerm;
			this.SetCancelable(cancelable);
			return this;
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x00105943 File Offset: 0x00103D43
		public ModalOverlay InitializeNonLocalized(string titleString, string messageString, bool cancelable)
		{
			this.title.text = titleString;
			this.message.text = messageString;
			this.SetCancelable(cancelable);
			return this;
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00105966 File Offset: 0x00103D66
		public ModalOverlay InitializeOKOnly(string titleLocTerm, string messageLocTerm, Func<bool> okAction = null)
		{
			this.Initialize(titleLocTerm, messageLocTerm, false);
			this.AddOKButton(okAction);
			return this;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0010597B File Offset: 0x00103D7B
		public ModalOverlay InitializeOKCancel(string titleLocTerm, string messageLocTerm, Func<bool> okAction)
		{
			this.Initialize(titleLocTerm, messageLocTerm, true);
			this.AddOKButton(okAction);
			return this;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x00105990 File Offset: 0x00103D90
		public ModalOverlay InitializeTextInput(string titleLocTerm, string messageLocTerm, string placeholder, Func<string, bool> inputHandler, bool cancelable)
		{
			this.inputField.gameObject.SetActive(true);
			Text text = this.inputField.placeholder as Text;
			if (text)
			{
				text.text = placeholder;
			}
			Func<bool> okAction = () => inputHandler(this.inputField.text);
			return (!cancelable) ? this.InitializeOKOnly(titleLocTerm, messageLocTerm, okAction) : this.InitializeOKCancel(titleLocTerm, messageLocTerm, okAction);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x00105A14 File Offset: 0x00103E14
		private ModalOverlay SetCancelable(bool cancelable)
		{
			if (cancelable)
			{
				Widget widget = this.AddButton("UI/COMMON/CANCEL", ModalOverlay.nullAction, null, null).SetSuccessAudio(this.dismissAudio);
				this.cancelButton = widget.GetComponent<IUINavigable>();
			}
			this.cancelListener.SetEnabled(cancelable);
			return this;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x00105A5E File Offset: 0x00103E5E
		public ModalOverlay SetCloseAction(Action closeAction)
		{
			this.closeAction = closeAction;
			return this;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00105A68 File Offset: 0x00103E68
		public ButtonWidget AddCancelButton()
		{
			this.SetCancelable(true);
			return this.cancelButton.transform.GetComponent<ButtonWidget>();
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00105A82 File Offset: 0x00103E82
		public ButtonWidget AddOKButton(Func<bool> okAction = null)
		{
			if (okAction == null)
			{
				okAction = ModalOverlay.nullAction;
			}
			return this.AddButton("UI/COMMON/OK", okAction, null, null);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00105A9F File Offset: 0x00103E9F
		public ModalOverlay SetOpenAudio(FabricEventReference id)
		{
			this.openAudio = id;
			return this;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00105AA9 File Offset: 0x00103EA9
		public ModalOverlay SetDefaultNavigable(IUINavigable navigable)
		{
			this.defaultNavigable = navigable;
			return this;
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x00105AB3 File Offset: 0x00103EB3
		public ModalOverlay SetDefaultNavigable(Widget widget)
		{
			return this.SetDefaultNavigable(widget.GetComponent<IUINavigable>());
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x00105AC4 File Offset: 0x00103EC4
		public override ButtonWidget AddButton(string labelLocTerm, Func<bool> action, Transform overrideParentTransform = null, ButtonWidget overrideButtonWidget = null)
		{
			Func<bool> action2 = () => action() && this.Dismiss();
			return base.AddButton(labelLocTerm, action2, overrideParentTransform, overrideButtonWidget);
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00105B00 File Offset: 0x00103F00
		protected override IUINavigable GetDefaultNavigable()
		{
			if (this.defaultNavigable != null)
			{
				return this.defaultNavigable;
			}
			if (this.cancelButton == null)
			{
				return base.GetDefaultNavigable();
			}
			base.RefreshNavigables();
			foreach (WeakInterfaceReference<IUINavigable> weakInterfaceReference in this.navigables)
			{
				if (weakInterfaceReference.Get() != this.cancelButton)
				{
					return weakInterfaceReference.Get();
				}
			}
			return this.cancelButton;
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00105BA8 File Offset: 0x00103FA8
		protected override void Update()
		{
			base.Update();
			if (!this.playedAudio)
			{
				FabricWrapper.PostEvent(this.openAudio);
			}
			this.playedAudio = true;
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x00105BCE File Offset: 0x00103FCE
		public bool Dismiss()
		{
			if (Singleton<UIManager>.instance && Singleton<UIManager>.instance.StackContains(this))
			{
				this.CloseMenu();
			}
			return true;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x00105BF6 File Offset: 0x00103FF6
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.visibility.SetVisible(true, false);
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x00105C0B File Offset: 0x0010400B
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
			this.visibility.SetVisible(false, false);
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x00105C20 File Offset: 0x00104020
		public override void CloseMenu()
		{
			if (this.closeAction != null)
			{
				this.closeAction();
			}
			this.closeAction = null;
			base.Invoke("InvokeReturn", 0.5f);
			base.CloseMenu();
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x00105C55 File Offset: 0x00104055
		private void InvokeReturn()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x00105C63 File Offset: 0x00104063
		public void HandleClickOff()
		{
			this.HandleBackButton();
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x00105C6C File Offset: 0x0010406C
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<ModalOverlay>);
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.cancelListener = base.GetComponent<UIActionListener>();
			this.cancelListener.action = EUIPadAction.Cancel;
			this.cancelListener.onActionRecieved += delegate()
			{
				this.Dismiss();
				FabricWrapper.PostEvent(this.dismissAudio);
			};
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00105CDE File Offset: 0x001040DE
		void IPoolable.OnRemoved()
		{
			this.OpenMenu();
			this.visibility.SetVisible(true, false);
			base.transform.SetAsLastSibling();
			this.openAudio = this.defaultOpenAudio;
			this.dismissAudio = this.defaultDismissAudio;
			this.playedAudio = false;
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x00105D1D File Offset: 0x0010411D
		void IPoolable.OnReturned()
		{
			this.ClearWidgets();
			this.inputField.gameObject.SetActive(false);
		}

		// Token: 0x040028E2 RID: 10466
		[Header("ModalOverlay")]
		[SerializeField]
		private Text title;

		// Token: 0x040028E3 RID: 10467
		[SerializeField]
		private Localize titleLocalize;

		// Token: 0x040028E4 RID: 10468
		[SerializeField]
		private Text message;

		// Token: 0x040028E5 RID: 10469
		[SerializeField]
		private Localize messageLocalize;

		// Token: 0x040028E6 RID: 10470
		[SerializeField]
		private InputField inputField;

		// Token: 0x040028E7 RID: 10471
		private UIActionListener cancelListener;

		// Token: 0x040028E8 RID: 10472
		private IUINavigable cancelButton;

		// Token: 0x040028E9 RID: 10473
		private IUINavigable defaultNavigable;

		// Token: 0x040028EA RID: 10474
		private FabricEventReference defaultOpenAudio = "UI/Popup";

		// Token: 0x040028EB RID: 10475
		private FabricEventReference defaultDismissAudio = "UI/InGame/Error";

		// Token: 0x040028EC RID: 10476
		private FabricEventReference openAudio;

		// Token: 0x040028ED RID: 10477
		private FabricEventReference dismissAudio;

		// Token: 0x040028EE RID: 10478
		private IUIVisibility visibility;

		// Token: 0x040028EF RID: 10479
		private bool playedAudio;

		// Token: 0x040028F0 RID: 10480
		private static readonly Func<bool> nullAction = () => true;

		// Token: 0x040028F1 RID: 10481
		private Action closeAction;

		// Token: 0x040028F2 RID: 10482
		private LocalPool<ModalOverlay> pool;
	}
}
