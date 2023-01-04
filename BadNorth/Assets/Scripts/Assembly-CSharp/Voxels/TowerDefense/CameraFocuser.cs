using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B2 RID: 1714
	public class CameraFocuser : MonoBehaviour
	{
		// Token: 0x06002C55 RID: 11349 RVA: 0x000A4F48 File Offset: 0x000A3348
		private void LateUpdate()
		{
			Shader.SetGlobalVector("_CameraDir", base.transform.forward);
			Shader.SetGlobalVector("_CameraRight", base.transform.right);
			Shader.SetGlobalFloat("_CameraUpScale", Constants.upScale);
			Shader.SetGlobalFloat("_PixelScale", Singleton<LevelCamera>.instance.cameraRef.orthographicSize * 2f / (float)Screen.height);
		}
	}
}
