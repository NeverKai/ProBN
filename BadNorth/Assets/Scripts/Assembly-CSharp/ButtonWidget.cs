using System;
using System.Diagnostics;

// Token: 0x020008C5 RID: 2245
public class ButtonWidget : Widget
{
	// Token: 0x140000C6 RID: 198
	// (add) Token: 0x06003B4C RID: 15180 RVA: 0x00107B28 File Offset: 0x00105F28
	// (remove) Token: 0x06003B4D RID: 15181 RVA: 0x00107B60 File Offset: 0x00105F60
	
	private event Func<bool> action;

	// Token: 0x06003B4E RID: 15182 RVA: 0x00107B96 File Offset: 0x00105F96
	public ButtonWidget Initialize(string locID, Func<bool> action)
	{
		base.Initialize(locID);
		this.action = action;
		return this;
	}

	// Token: 0x06003B4F RID: 15183 RVA: 0x00107BA8 File Offset: 0x00105FA8
	public void OnButtonClickedHandler()
	{
		if (this.action != null && this.action())
		{
			FabricWrapper.PostEvent(base.successAudio);
		}
		else
		{
			FabricWrapper.PostEvent(base.failAudio);
		}
	}
}
