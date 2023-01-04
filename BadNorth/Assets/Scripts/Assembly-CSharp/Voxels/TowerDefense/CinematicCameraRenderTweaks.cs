using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C1 RID: 1729
	public class CinematicCameraRenderTweaks : MonoBehaviour
	{
		// Token: 0x06002CCE RID: 11470 RVA: 0x000A6FDD File Offset: 0x000A53DD
		private void OnPreRender()
		{
			Shader.EnableKeyword("_CINEMATIC_ON");
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000A6FE9 File Offset: 0x000A53E9
		private void OnPostRender()
		{
			Shader.DisableKeyword("_CINEMATIC_ON");
		}
	}
}
