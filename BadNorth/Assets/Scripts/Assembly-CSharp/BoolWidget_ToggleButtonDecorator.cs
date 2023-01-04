using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008C4 RID: 2244
public class BoolWidget_ToggleButtonDecorator : MonoBehaviour
{
	// Token: 0x06003B48 RID: 15176 RVA: 0x00107A54 File Offset: 0x00105E54
	private void Start()
	{
		this.widget.onValueChanged += this.Refresh;
		this.widget.onForceUpdate += this.Refresh;
		this.Refresh(this.widget.GetValue());
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x00107AA0 File Offset: 0x00105EA0
	private void OnDestroy()
	{
		this.widget.onValueChanged -= this.Refresh;
		this.widget.onForceUpdate -= this.Refresh;
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x00107AD0 File Offset: 0x00105ED0
	private void Refresh(bool newValue)
	{
		this.yesNoText.text = ((!newValue) ? "N" : "Y");
		this.yesNoText.color = ((!newValue) ? this.falseTextColor : Color.black);
	}

	// Token: 0x04002936 RID: 10550
	[SerializeField]
	private BoolWidget widget;

	// Token: 0x04002937 RID: 10551
	[SerializeField]
	private Text yesNoText;

	// Token: 0x04002938 RID: 10552
	public Color falseTextColor = Color.Lerp(Color.red, Color.black, 0.6f);
}
