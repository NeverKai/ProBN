using System;
using System.Diagnostics;
using RTM.UISystem.Internal;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004DC RID: 1244
	public class UINavigable : MonoBehaviour, IUINavigable
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x00055AE8 File Offset: 0x00053EE8
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x00055ADF File Offset: 0x00053EDF
		public FabricEventReference focusAudio
		{
			get
			{
				return (!this.suppressFocusAudio) ? ((!string.IsNullOrEmpty(this._focusAudio.name)) ? this._focusAudio : FabricID.uiFocus) : FabricEventReference.none;
			}
			set
			{
				this._focusAudio = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x00055B2D File Offset: 0x00053F2D
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x00055B24 File Offset: 0x00053F24
		public FabricEventReference selectAudio
		{
			get
			{
				return (!string.IsNullOrEmpty(this._selectAudio.name)) ? this._selectAudio : this.focusAudio;
			}
			set
			{
				this._selectAudio = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00055B55 File Offset: 0x00053F55
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x00055B5D File Offset: 0x00053F5D
		public bool hasFocus { get; private set; }

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06001FD0 RID: 8144 RVA: 0x00055B68 File Offset: 0x00053F68
		// (remove) Token: 0x06001FD1 RID: 8145 RVA: 0x00055BA0 File Offset: 0x00053FA0
		
		public event Action onClicked = delegate()
		{
		};

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06001FD2 RID: 8146 RVA: 0x00055BD8 File Offset: 0x00053FD8
		// (remove) Token: 0x06001FD3 RID: 8147 RVA: 0x00055C10 File Offset: 0x00054010
		
		public event Action<bool> onFocusChanged = delegate(bool A_0)
		{
		};

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06001FD4 RID: 8148 RVA: 0x00055C48 File Offset: 0x00054048
		// (remove) Token: 0x06001FD5 RID: 8149 RVA: 0x00055C80 File Offset: 0x00054080
		
		public event Action<Vector2> onConsumedNavigation = delegate(Vector2 A_0)
		{
		};

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00055CB6 File Offset: 0x000540B6
		private void Awake()
		{
			this.interactible = base.GetComponent<UIInteractable>();
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00055CC4 File Offset: 0x000540C4
		void IUINavigable.SetFocus(bool focussed, IUINavigable previousFocus)
		{
			bool hasFocus = this.hasFocus;
			this.hasFocus = focussed;
			if (this.hasFocus != hasFocus)
			{
				this.onFocusChanged(this.hasFocus);
			}
			if (this.hasFocus && previousFocus != null)
			{
				FabricWrapper.PostEvent(this.focusAudio);
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00055D1C File Offset: 0x0005411C
		bool IUINavigable.ConsumeNavigation(Vector2 navDirection)
		{
			bool flag = Helpers.IsHorizontal(navDirection);
			if ((this.consumeHorizontalNavigation && flag) || (this.consumeVerticalNavigation && !flag))
			{
				this.onConsumedNavigation(navDirection);
				return true;
			}
			return false;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00055D61 File Offset: 0x00054161
		void IUINavigable.Click()
		{
			this.onClicked();
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00055D6E File Offset: 0x0005416E
		bool IUINavigable.isNavigable
		{
			get
			{
				return base.isActiveAndEnabled && (!this.interactible || this.interactible.isActiveAndEnabled);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00055D9C File Offset: 0x0005419C
		bool IUINavigable.allowRepeatNavigation
		{
			get
			{
				return this._allowRepeatNavigation;
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00055DAA File Offset: 0x000541AA
		// Transform IUINavigable.get_transform()
		// {
		// 	return base.transform;
		// }

		// Token: 0x040013BE RID: 5054
		[SerializeField]
		private bool consumeHorizontalNavigation;

		// Token: 0x040013BF RID: 5055
		[SerializeField]
		private bool consumeVerticalNavigation;

		// Token: 0x040013C0 RID: 5056
		[SerializeField]
		private bool _allowRepeatNavigation = true;

		// Token: 0x040013C1 RID: 5057
		[SerializeField]
		private FabricEventReference _focusAudio = string.Empty;

		// Token: 0x040013C2 RID: 5058
		[SerializeField]
		private FabricEventReference _selectAudio = string.Empty;

		// Token: 0x040013C4 RID: 5060
		[NonSerialized]
		public bool suppressFocusAudio;

		// Token: 0x040013C5 RID: 5061
		private UIInteractable interactible;
	}
}
