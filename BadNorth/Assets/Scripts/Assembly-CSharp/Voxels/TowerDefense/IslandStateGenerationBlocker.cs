using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000532 RID: 1330
	internal class IslandStateGenerationBlocker : MonoBehaviour, IslandGameplayManager.IAwake
	{
		// Token: 0x06002296 RID: 8854 RVA: 0x00064E34 File Offset: 0x00063234
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			IslandStateTree states = manager.states;
			states.root.OnActivate += delegate()
			{
				IslandGenerator.AddBlocker(this, null);
			};
			states.root.OnDeactivate += delegate()
			{
				IslandGenerator.RemoveBlocker(this, this);
			};
			states.Spawning.OnActivate += delegate()
			{
				IslandGenerator.AddBlocker(null, this);
			};
			states.Selectable.OnDeactivate += delegate()
			{
				IslandGenerator.RemoveBlocker(null, this);
			};
		}
	}
}
