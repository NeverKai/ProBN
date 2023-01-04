using System;

// Token: 0x020008C2 RID: 2242
public class BoolWidget : ValueWidget<bool>
{
	// Token: 0x06003B42 RID: 15170 RVA: 0x00107986 File Offset: 0x00105D86
	public void ToggleHandler()
	{
		base.SetValueHandler(!base.GetValue());
	}
}
