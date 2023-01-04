using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200085E RID: 2142
	public class SquadTemplate : MonoBehaviour
	{
		// Token: 0x04002643 RID: 9795
		public string title;

		// Token: 0x04002644 RID: 9796
		public Agent standardPrefab;

		// Token: 0x04002645 RID: 9797
		public Agent agentPrefab;

		// Token: 0x04002646 RID: 9798
		public int level;

		// Token: 0x04002647 RID: 9799
		public List<SquadTemplate> nextSquadTemplates;

		// Token: 0x04002648 RID: 9800
		[Header("Sound")]
		public string moveSound = "Sfx/English/Archer/Move";

		// Token: 0x04002649 RID: 9801
		public string swapSound = "Sfx/English/Archer/Swap";
	}
}
