using System;
using UnityEngine;
using Voxels.TowerDefense;

namespace SpriteComposing
{
	// Token: 0x020005C8 RID: 1480
	public class ComposedSpriteInitializer : MonoBehaviour, IGameSetup
	{
		// Token: 0x0600269E RID: 9886 RVA: 0x0007ABAC File Offset: 0x00078FAC
		void IGameSetup.OnGameAwake()
		{
			ComposedSprite.Initialize(512, 512);
		}
	}
}
