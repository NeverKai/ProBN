using System;
using UnityEngine;
using UnityEngine.Events;

namespace Voxels.TowerDefense
{
	// Token: 0x02000798 RID: 1944
	public class LevelEvent : MonoBehaviour, ILevelComponent
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x000D4993 File Offset: 0x000D2D93
		public void OnValidate()
		{
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000D4995 File Offset: 0x000D2D95
		public void OnSetLevel(Agent agent, int level)
		{
			this.events[level].Invoke();
		}

		// Token: 0x04002210 RID: 8720
		public UnityEvent[] events = new UnityEvent[3];
	}
}
