using System;
using UnityEngine;

// Token: 0x020008C8 RID: 2248
public class IntWidget : ValueWidget<int>
{
	// Token: 0x06003B68 RID: 15208 RVA: 0x0010818C File Offset: 0x0010658C
	public IntWidget SetMinMax(int min, int max)
	{
		this.min = min;
		this.max = max;
		base.SetValueHandler(Mathf.Clamp(base.GetValue(), min, max));
		this.ForceUpdate();
		return this;
	}

	// Token: 0x06003B69 RID: 15209 RVA: 0x001081B6 File Offset: 0x001065B6
	public IntWidget SetStepSize(int stepSize)
	{
		this.stepSize = stepSize;
		return this;
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x001081C0 File Offset: 0x001065C0
	public override void DecrementHandler()
	{
		if (this.incrementsCycle)
		{
			this.CycleHandler(-this.stepSize);
		}
		else
		{
			this.ChangeValueHandler(-this.stepSize);
		}
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x001081EC File Offset: 0x001065EC
	public override void IncrementHandler()
	{
		if (this.incrementsCycle)
		{
			this.CycleHandler(this.stepSize);
		}
		else
		{
			this.ChangeValueHandler(this.stepSize);
		}
	}

	// Token: 0x06003B6C RID: 15212 RVA: 0x00108216 File Offset: 0x00106616
	public void CycleHandler()
	{
		this.CycleHandler(this.stepSize);
	}

	// Token: 0x06003B6D RID: 15213 RVA: 0x00108224 File Offset: 0x00106624
	public void CycleHandler(int valueChange)
	{
		int num = base.GetValue();
		num += valueChange;
		base.SetValueHandler((num <= this.max) ? ((num >= this.min) ? num : this.max) : this.min);
	}

	// Token: 0x06003B6E RID: 15214 RVA: 0x00108274 File Offset: 0x00106674
	public void ChangeValueHandler(int valueChange)
	{
		int num = base.GetValue();
		num = Mathf.Clamp(num + valueChange, this.min, this.max);
		base.SetValueHandler(num);
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x001082A4 File Offset: 0x001066A4
	public IntWidget SetIncrementCycling(bool enabled)
	{
		this.incrementsCycle = enabled;
		return this;
	}

	// Token: 0x0400294B RID: 10571
	public int min;

	// Token: 0x0400294C RID: 10572
	public int max = 100;

	// Token: 0x0400294D RID: 10573
	public int stepSize = 1;

	// Token: 0x0400294E RID: 10574
	public bool incrementsCycle;

	// Token: 0x0400294F RID: 10575
	public bool percentDisplay;
}
