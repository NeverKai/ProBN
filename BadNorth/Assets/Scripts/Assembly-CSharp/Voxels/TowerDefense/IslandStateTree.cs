using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000533 RID: 1331
	public class IslandStateTree : MonoBehaviour
	{
		// Token: 0x04001519 RID: 5401
		public State root;

		// Token: 0x0400151A RID: 5402
		public State pause;

		// Token: 0x0400151B RID: 5403
		public State gameStates;

		// Token: 0x0400151C RID: 5404
		public State loadout;

		// Token: 0x0400151D RID: 5405
		public State Spawning;

		// Token: 0x0400151E RID: 5406
		public State Selectable;

		// Token: 0x0400151F RID: 5407
		public State Prepare;

		// Token: 0x04001520 RID: 5408
		public State tutorial;

		// Token: 0x04001521 RID: 5409
		public State running;

		// Token: 0x04001522 RID: 5410
		public State squadSelected;

		// Token: 0x04001523 RID: 5411
		public State touchMoveConfirm;

		// Token: 0x04001524 RID: 5412
		public State EndOfLevel;

		// Token: 0x04001525 RID: 5413
		public State EOLResult;

		// Token: 0x04001526 RID: 5414
		public State EOLStats;

		// Token: 0x04001527 RID: 5415
		public State EOLCoinDispense;

		// Token: 0x04001528 RID: 5416
		public State EOLContinue;

		// Token: 0x04001529 RID: 5417
		public State EOLTitle;

		// Token: 0x0400152A RID: 5418
		public State gameWon;
	}
}
