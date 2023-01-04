using System;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x0200076E RID: 1902
	public class SeededComponent : MonoBehaviour
	{
		// Token: 0x0600315F RID: 12639 RVA: 0x000582BF File Offset: 0x000566BF
		public void SetSeed(int seed)
		{
			this.uniqueSeed = seed;
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000582C8 File Offset: 0x000566C8
		public void Seed(int seed)
		{
			UnityEngine.Random.InitState(seed ^ this.uniqueSeed + 2791);
		}

		// Token: 0x0400211F RID: 8479
		[SerializeField]
		public int uniqueSeed;
	}
}
