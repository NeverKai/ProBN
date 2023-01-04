using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008C3 RID: 2243
public class BoolWidget_CheckboxDecorator : MonoBehaviour
{
	// Token: 0x06003B44 RID: 15172 RVA: 0x001079A0 File Offset: 0x00105DA0
	private void Start()
	{
		this.widget.onValueChanged += this.Refresh;
		this.widget.onForceUpdate += this.Refresh;
		this.Refresh(this.widget.GetValue());
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x001079EC File Offset: 0x00105DEC
	private void OnDestroy()
	{
		this.widget.onValueChanged -= this.Refresh;
		this.widget.onForceUpdate -= this.Refresh;
	}

	// Token: 0x06003B46 RID: 15174 RVA: 0x00107A1C File Offset: 0x00105E1C
	private void Refresh(bool newValue)
	{
		this.checkmark.gameObject.SetActive(newValue);
	}

	// Token: 0x04002934 RID: 10548
	[SerializeField]
	private BoolWidget widget;

	// Token: 0x04002935 RID: 10549
	[SerializeField]
	private Image checkmark;
}
