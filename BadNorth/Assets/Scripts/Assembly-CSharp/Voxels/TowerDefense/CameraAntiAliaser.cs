using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020006AF RID: 1711
	public class CameraAntiAliaser : MonoBehaviour
	{
		// Token: 0x06002C36 RID: 11318 RVA: 0x000A4094 File Offset: 0x000A2494
		private void Awake()
		{
			this.material = new Material(this.shader);
			this.oldCameraLayerMask = this.gameCamera.cullingMask;
			this.textureCamera = base.GetComponent<Camera>();
			this.textureCamera.cullingMask = Singleton<LevelCamera>.instance.cullingMask;
			this.textureCamera.enabled = true;
			this.targetImage.enabled = true;
			UserSettings.onUpdated += this.CheckUserSettings;
			this.CheckUserSettings(Profile.userSettings);
			LevelCamera.SubscribeToBloodSettings(this.textureCamera);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000A4128 File Offset: 0x000A2528
		private void OnPreRender()
		{
			int num = Mathf.NextPowerOfTwo(this.gameCamera.pixelWidth);
			int num2 = Mathf.NextPowerOfTwo(this.gameCamera.pixelHeight);
			if (!this.renderTex || this.outputTex.width != num || this.outputTex.height != num2)
			{
				if (this.renderTex)
				{
					UnityEngine.Object.Destroy(this.renderTex);
				}
				if (this.outputTex)
				{
					UnityEngine.Object.Destroy(this.outputTex);
				}
				this.renderTex = new RenderTexture(num * 2, num2 * 2, 24);
				this.renderTex.Create();
				this.textureCamera.targetTexture = this.renderTex;
				this.outputTex = new RenderTexture(num, num2, 24);
				this.outputTex.Create();
				this.targetImage.texture = this.outputTex;
			}
			this.textureCamera.orthographic = this.gameCamera.orthographic;
			this.textureCamera.orthographicSize = this.gameCamera.orthographicSize;
			Rect rect = Rect.MinMaxRect(0f, 0f, (float)this.gameCamera.pixelWidth / (float)num, (float)this.gameCamera.pixelHeight / (float)num2);
			this.textureCamera.rect = rect;
			this.targetImage.uvRect = rect;
			Shader.SetGlobalFloat(this.aAFactorId, 1f);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000A42A2 File Offset: 0x000A26A2
		private void OnPostRender()
		{
			Shader.SetGlobalFloat(this.aAFactorId, 0f);
			this.outputTex.DiscardContents();
			Graphics.Blit(this.renderTex, this.outputTex, this.material);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000A42DB File Offset: 0x000A26DB
		private void OnEnable()
		{
			this.gameCamera.cullingMask = 0;
			this.gameCamera.allowMSAA = false;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000A42F5 File Offset: 0x000A26F5
		private void OnDisable()
		{
			this.gameCamera.cullingMask = this.oldCameraLayerMask;
			this.gameCamera.allowMSAA = true;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000A431C File Offset: 0x000A271C
		private void CheckUserSettings(UserSettings userSettings)
		{
			bool active = userSettings.antiAliasingLevel == UserSettings.AntiAliasOption.AACamera && this.gameCamera.targetDisplay < Display.displays.Length;
			base.gameObject.SetActive(active);
		}

		// Token: 0x04001CE9 RID: 7401
		[SerializeField]
		private Camera gameCamera;

		// Token: 0x04001CEA RID: 7402
		[SerializeField]
		private RawImage targetImage;

		// Token: 0x04001CEB RID: 7403
		[SerializeField]
		private Shader shader;

		// Token: 0x04001CEC RID: 7404
		private LayerMask oldCameraLayerMask;

		// Token: 0x04001CED RID: 7405
		private Camera textureCamera;

		// Token: 0x04001CEE RID: 7406
		private RenderTexture renderTex;

		// Token: 0x04001CEF RID: 7407
		private RenderTexture outputTex;

		// Token: 0x04001CF0 RID: 7408
		private Material material;

		// Token: 0x04001CF1 RID: 7409
		private ShaderId aAFactorId = new ShaderId("_AAFactor");
	}
}
