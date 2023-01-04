using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C0 RID: 1728
	public class CinematicCameraEditorRender : MonoBehaviour
	{
		// Token: 0x06002CCB RID: 11467 RVA: 0x000A6ECE File Offset: 0x000A52CE
		private void LateUpdate()
		{
			this.UpdateDebugRender();
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000A6ED8 File Offset: 0x000A52D8
		private void UpdateDebugRender()
		{
			int pixelWidth = this.cameraRef.pixelWidth;
			int pixelHeight = this.cameraRef.pixelHeight;
			int num = Mathf.NextPowerOfTwo(pixelWidth);
			int num2 = Mathf.NextPowerOfTwo(pixelHeight);
			if (!this.renderTex || this.renderTex.width != num || this.renderTex.height != num2)
			{
				if (this.renderTex)
				{
					UnityEngine.Object.Destroy(this.renderTex);
				}
				this.renderTex = new RenderTexture(num, num2, 24);
				this.renderTex.Create();
				this.cameraRef.targetTexture = this.renderTex;
				this.targetImage.texture = this.renderTex;
				Rect rect = Rect.MinMaxRect(0f, 0f, (float)pixelWidth / (float)num, (float)pixelHeight / (float)num2);
				this.cameraRef.rect = rect;
				this.targetImage.uvRect = rect;
			}
			this.cameraRef.Render();
		}

		// Token: 0x04001D6F RID: 7535
		[Header("DebugRender")]
		[SerializeField]
		private Camera cameraRef;

		// Token: 0x04001D70 RID: 7536
		[SerializeField]
		private RawImage targetImage;

		// Token: 0x04001D71 RID: 7537
		private RenderTexture renderTex;
	}
}
