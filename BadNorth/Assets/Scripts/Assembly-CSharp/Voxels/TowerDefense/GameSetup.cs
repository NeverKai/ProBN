using System;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200051A RID: 1306
	public class GameSetup : MonoBehaviour
	{
		// Token: 0x060021EB RID: 8683 RVA: 0x00060CDC File Offset: 0x0005F0DC
		private void Awake()
		{
			foreach (IGameSetup gameSetup in base.transform.GetComponentsInChildren<IGameSetup>(true))
			{
				gameSetup.OnGameAwake();
			}
		}

		// Token: 0x040014AF RID: 5295
		[SerializeField]
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("GameSetup", EVerbosity.Quiet, 0);
	}
}
