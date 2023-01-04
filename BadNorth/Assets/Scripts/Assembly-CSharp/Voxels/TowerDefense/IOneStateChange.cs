using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F5 RID: 2037
	public interface IOneStateChange
	{
		// Token: 0x0600356B RID: 13675
		void OnOneStateChange(Stack stack, State state);

		// Token: 0x0600356C RID: 13676
		void OnStateEnlist();
	}
}
