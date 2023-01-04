using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200055F RID: 1375
	public class TouchPauseButton : MonoBehaviour, IslandUIManager.IAwake
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x0006F9E2 File Offset: 0x0006DDE2
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.pauser = manager.gameplayManager.levelPauser;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0006F9F5 File Offset: 0x0006DDF5
		public void HandlePauseButton()
		{
			this.pauser.SetPause(true, true);
		}

		// Token: 0x0400166E RID: 5742
		private LevelPauser pauser;
	}
}
