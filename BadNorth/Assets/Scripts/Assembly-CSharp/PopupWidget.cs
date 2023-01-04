using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008CE RID: 2254
internal class PopupWidget : MultiSelectWidget
{
	// Token: 0x1700085A RID: 2138
	// (get) Token: 0x06003B89 RID: 15241 RVA: 0x00108D62 File Offset: 0x00107162
	// (set) Token: 0x06003B8A RID: 15242 RVA: 0x00108D6A File Offset: 0x0010716A
	public PopupWidget_Popup popup { get; private set; }

	// Token: 0x06003B8B RID: 15243 RVA: 0x00108D74 File Offset: 0x00107174
	public PopupWidget SetPopup(PopupWidget_Popup popup)
	{
		this.popup = popup;
		popup.Setup(this);
		base.onValueChanged += this.OnValueChanged;
		base.onForceUpdate += this.OnValueChanged;
		this.OnValueChanged(base.GetValue());
		return this;
	}

	// Token: 0x06003B8C RID: 15244 RVA: 0x00108DC0 File Offset: 0x001071C0
	private void OnValueChanged(int idx)
	{
		this.value.text = this.values[idx];
	}

	// Token: 0x06003B8D RID: 15245 RVA: 0x00108DD5 File Offset: 0x001071D5
	public void ClickHandler()
	{
		this.popup.OpenMenu();
		FabricWrapper.PostEvent(base.successAudio);
	}

	// Token: 0x04002972 RID: 10610
	[SerializeField]
	private Text value;
}
