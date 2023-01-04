using System;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000813 RID: 2067
	public class TargetAnimator : TargetAnimator<float>
	{
		// Token: 0x0600360E RID: 13838 RVA: 0x000E9788 File Offset: 0x000E7B88
		public TargetAnimator(string name, Func<float> getFunc, Action<float> setFunc, AgentState updateState, ITargetAnimFuncs<float> targetAnimFuncs) : base(name, getFunc, setFunc, updateState, targetAnimFuncs)
		{
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000E9797 File Offset: 0x000E7B97
		public TargetAnimator(Func<float> getFunc, Action<float> setFunc, AgentState updateState, ITargetAnimFuncs<float> targetAnimFuncs) : base(getFunc, setFunc, updateState, targetAnimFuncs)
		{
		}
	}
}
