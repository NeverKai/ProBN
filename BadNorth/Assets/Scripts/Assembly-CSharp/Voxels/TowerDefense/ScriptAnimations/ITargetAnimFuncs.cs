using System;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000814 RID: 2068
	public interface ITargetAnimFuncs<T>
	{
		// Token: 0x06003610 RID: 13840
		T UpdateCurrent(T current, T target, float dt);

		// Token: 0x06003611 RID: 13841
		bool ShouldTrigger(T current, T target);

		// Token: 0x06003612 RID: 13842
		bool IsDone(T current, T target);

		// Token: 0x06003613 RID: 13843
		void OnActivate(T current, T target);
	}
}
