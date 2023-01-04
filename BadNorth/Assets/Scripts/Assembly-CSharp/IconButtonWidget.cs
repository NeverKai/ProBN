using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008C7 RID: 2247
public class IconButtonWidget : ButtonWidget
{
	// Token: 0x06003B65 RID: 15205 RVA: 0x0010814E File Offset: 0x0010654E
	public IconButtonWidget Initialize(string locID, Func<bool> action, Sprite icon)
	{
		base.Initialize(locID, action);
		this.image.sprite = icon;
		return this;
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x00108166 File Offset: 0x00106566
	public IconButtonWidget SetIcon(Sprite icon)
	{
		this.image.sprite = icon;
		return this;
	}

	// Token: 0x0400294A RID: 10570
	[SerializeField]
	private Image image;
}
