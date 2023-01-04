using System;

// Token: 0x020008CA RID: 2250
public class MultiSelectWidget : IntWidget
{
	// Token: 0x06003B7A RID: 15226 RVA: 0x0010879D File Offset: 0x00106B9D
	public MultiSelectWidget SetValues(string[] values, bool wantsLocalizedValues)
	{
		this.values = values;
		this.wantsLocalizedValues = wantsLocalizedValues;
		this.min = 0;
		this.max = values.Length - 1;
		return this;
	}

	// Token: 0x06003B7B RID: 15227 RVA: 0x001087C0 File Offset: 0x00106BC0
	public MultiSelectWidget SetOnOff()
	{
		this.incrementsCycle = true;
		return this.SetValues(MultiSelectWidget.onOffArray, true);
	}

	// Token: 0x06003B7C RID: 15228 RVA: 0x001087D5 File Offset: 0x00106BD5
	public MultiSelectWidget SetYesNo()
	{
		this.incrementsCycle = true;
		return this.SetValues(MultiSelectWidget.yesNoArray, true);
	}

	// Token: 0x0400295B RID: 10587
	public bool wantsLocalizedValues;

	// Token: 0x0400295C RID: 10588
	public string[] values;

	// Token: 0x0400295D RID: 10589
	private static readonly string[] onOffArray = new string[]
	{
		"SETTINGS/OFF",
		"SETTINGS/ON"
	};

	// Token: 0x0400295E RID: 10590
	private static readonly string[] yesNoArray = new string[]
	{
		"UI/COMMON/NO",
		"UI/COMMON/YES"
	};
}
