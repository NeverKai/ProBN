using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200078E RID: 1934
	public class TopdownCamera : IslandComponent, IIslandFirstEnter, IIslandEnter, IIslandDestroyEntered
	{
		// Token: 0x060031E9 RID: 12777 RVA: 0x000D31C0 File Offset: 0x000D15C0
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			GenInfo genInfo = new GenInfo("TopDownCamera", GenInfo.Mode.interruptable);
			this.cameraRef = base.gameObject.AddComponent<Camera>();
			this.cameraRef.transform.position = new Vector3(0f, island.wind.size.y, 0f);
			this.cameraRef.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			this.cameraRef.enabled = false;
			this.cameraRef.clearFlags = CameraClearFlags.Color;
			this.cameraRef.backgroundColor = new Color(0.5f, 1f, 0.5f, 0f);
			this.cameraRef.cullingMask = LayerMask.GetMask(new string[]
			{
				"Modules",
				"Houses"
			});
			this.cameraRef.orthographic = true;
			this.cameraRef.orthographicSize = island.voxelSpace.voxelSize.x / 2f;
			this.rendTex = new RenderTexture(256, 256, 24, RenderTextureFormat.ARGB32);
			this.cameraRef.targetTexture = this.rendTex;
			this.cameraRef.SetReplacementShader(this.shader, string.Empty);
			yield return genInfo;
			island.aoBaker.ApplyShaderVariables();
			this.cameraRef.Render();
			yield return genInfo;
			yield break;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000D31E2 File Offset: 0x000D15E2
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, new Material(this.blitShader));
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000D31F8 File Offset: 0x000D15F8
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			Shader.SetGlobalTexture("_TopdownTex", this.rendTex);
			yield return new GenInfo("TopdownCamera", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000D3213 File Offset: 0x000D1613
		void IIslandDestroyEntered.OnIslandDestroyEntered(Island island)
		{
			this.rendTex.Release();
			UnityEngine.Object.Destroy(this.rendTex);
		}

		// Token: 0x040021CD RID: 8653
		private Camera cameraRef;

		// Token: 0x040021CE RID: 8654
		[SerializeField]
		private RenderTexture rendTex;

		// Token: 0x040021CF RID: 8655
		[SerializeField]
		private Shader shader;

		// Token: 0x040021D0 RID: 8656
		[SerializeField]
		private Shader blitShader;
	}
}
