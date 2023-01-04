using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000729 RID: 1833
	public class CheckpointHouse : MonoBehaviour, IIslandFirstEnter
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x000C140C File Offset: 0x000BF80C
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			this.house.destroyAudioOverride = CheckpointHouse.destroyAudio;
			yield return new GenInfo("Checkpoint House", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x04001FC1 RID: 8129
		public House house;

		// Token: 0x04001FC2 RID: 8130
		private static FabricEventReference destroyAudio = "Sfx/House/DestroyedChurch";
	}
}
