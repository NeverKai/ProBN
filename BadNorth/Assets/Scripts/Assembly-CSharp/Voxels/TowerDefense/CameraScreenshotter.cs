using System;
using ReflexCLI.Attributes;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B8 RID: 1720
	public class CameraScreenshotter : Singleton<CameraScreenshotter>
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000A56A1 File Offset: 0x000A3AA1
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000A56A9 File Offset: 0x000A3AA9
		public Texture2D screenshotTexture { get; private set; }

		// Token: 0x06002C72 RID: 11378 RVA: 0x000A56B4 File Offset: 0x000A3AB4
		protected override void Awake()
		{
			if (this.isDebug)
			{
				base.Awake();
				Player player = ReInput.players.GetPlayer(0);
				player.controllers.maps.GetMap(ControllerType.Keyboard, player.controllers.Keyboard.id, "Default", "Tools").enabled = true;
				player.AddInputEventDelegate(new Action<InputActionEventData>(this.Screenshot), UpdateLoopType.Update, InputActionEventType.ButtonPressed, this.actionReference);
			}
			this.defaultCullingMask = Singleton<LevelCamera>.instance.cullingMask;
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000A573E File Offset: 0x000A3B3E
		private void Screenshot(InputActionEventData actionEventData)
		{
			if (!ConsoleStatus.IsConsoleOpen())
			{
				this.Screenshot();
			}
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000A5750 File Offset: 0x000A3B50
		[ContextMenu("Screenshot")]
		private void DebugScreenShot()
		{
			this.Screenshot();
			CameraScreenshotter.SavePng(this.screenshotTexture);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000A5764 File Offset: 0x000A3B64
		public void Screenshot()
		{
			Island island = Singleton<IslandGameplayManager>.instance.island;
			if (!island)
			{
				return;
			}
			if (!this.textureCamera)
			{
				this.textureCamera = base.GetComponent<Camera>();
				this.textureCamera.enabled = false;
				this.textureCamera.orthographic = true;
				this.material = new Material(this.shader);
			}
			if (this.referenceCamera)
			{
				this.width = this.referenceCamera.pixelWidth;
				this.height = this.referenceCamera.pixelHeight;
				this.textureCamera.orthographicSize = this.referenceCamera.orthographicSize;
				this.textureCamera.transform.rotation = this.referenceCamera.transform.rotation;
				Vector3 vector = base.transform.InverseTransformPoint(Vector3.zero);
				float value = this.textureCamera.transform.localPosition.x + vector.x;
				this.textureCamera.transform.localPosition = this.textureCamera.transform.localPosition.SetX(value);
			}
			else
			{
				this.textureCamera.orthographicSize = island.fog.maxRad * 1.1f;
				this.textureCamera.transform.rotation = Singleton<LevelCamera>.instance.transform.rotation;
				this.textureCamera.transform.position = base.transform.rotation * Vector3.back * island.fog.maxRad * 3f;
				this.textureCamera.transform.position += Vector3.up * island.size.y / 2f;
			}
			int num = Mathf.NextPowerOfTwo(this.width);
			int num2 = Mathf.NextPowerOfTwo(this.height);
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
				if (this.screenshotTexture)
				{
					UnityEngine.Object.Destroy(this.screenshotTexture);
				}
				this.renderTex = new RenderTexture(num * 2, num2 * 2, 24);
				this.renderTex.name = "RenderTex " + this.renderTexIndex;
				this.renderTex.Create();
				this.textureCamera.targetTexture = this.renderTex;
				this.outputTex = new RenderTexture(num, num2, 24);
				this.outputTex.name = "OutputTex " + this.renderTexIndex++;
				this.outputTex.Create();
			}
			if (!this.screenshotTexture || this.screenshotTexture.width != this.width || this.screenshotTexture.height != this.height)
			{
				if (this.screenshotTexture)
				{
					UnityEngine.Object.Destroy(this.screenshotTexture);
				}
				this.screenshotTexture = new Texture2D(this.width, this.height);
			}
			int num3 = this.defaultCullingMask;
			int mask = LayerMask.GetMask(new string[]
			{
				"UI"
			});
			num3 = ((!this.showUi) ? (this.defaultCullingMask & ~mask) : (this.defaultCullingMask | mask));
			bool flag = Profile.userSettings != null && Profile.userSettings.showBlood;
			if (flag)
			{
				num3 |= 1 << LayerMaster.blood.id;
			}
			else
			{
				num3 &= ~(1 << LayerMaster.blood.id);
			}
			this.textureCamera.cullingMask = num3;
			this.textureCamera.pixelRect = new Rect((float)(this.renderTex.width / 2 - this.width), (float)(this.renderTex.height / 2 - this.height), (float)(this.width * 2), (float)(this.height * 2));
			Shader.SetGlobalFloat(CameraScreenshotter.aAFactorId, this.aaFactor - 1f);
			if (!this.showUi)
			{
				Shader.EnableKeyword("_CINEMATIC_ON");
			}
			this.textureCamera.Render();
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000A5C14 File Offset: 0x000A4014
		private void OnPostRender()
		{
			Shader.SetGlobalFloat(CameraScreenshotter.aAFactorId, 0f);
			this.outputTex.DiscardContents();
			Graphics.Blit(this.renderTex, this.outputTex, this.material);
			if (!this.showUi)
			{
				Shader.DisableKeyword("_CINEMATIC_ON");
			}
			if (this.screenshotTexture.width != this.width || this.screenshotTexture.height != this.screenshotTexture.height)
			{
				this.screenshotTexture.Resize(this.width, this.height);
			}
			RenderTexture.active = this.outputTex;
			this.screenshotTexture.ReadPixels(new Rect((float)((this.outputTex.width - this.width) / 2), (float)((this.outputTex.height - this.height) / 2), (float)this.width, (float)this.height), 0, 0);
			this.screenshotTexture.Apply();
			this.textureCamera.transform.localPosition = Vector3.zero;
			this.textureCamera.transform.localRotation = Quaternion.identity;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000A5D3F File Offset: 0x000A413F
		private static void SavePng(Texture2D texture)
		{
		}

		// Token: 0x04001D20 RID: 7456
		private static readonly ShaderId aAFactorId = new ShaderId("_AAFactor");

		// Token: 0x04001D21 RID: 7457
		[SerializeField]
		private Shader shader;

		// Token: 0x04001D22 RID: 7458
		[SerializeField]
		private RenderTexture renderTex;

		// Token: 0x04001D23 RID: 7459
		[SerializeField]
		private RenderTexture outputTex;

		// Token: 0x04001D24 RID: 7460
		[SerializeField]
		private bool isDebug = true;

		// Token: 0x04001D25 RID: 7461
		[SerializeField]
		private float aaFactor = 2f;

		// Token: 0x04001D26 RID: 7462
		[Header("View")]
		[SerializeField]
		private Camera referenceCamera;

		// Token: 0x04001D27 RID: 7463
		[SerializeField]
		private int width = 4096;

		// Token: 0x04001D28 RID: 7464
		[SerializeField]
		private int height = 4096;

		// Token: 0x04001D29 RID: 7465
		[SerializeField]
		private bool showUi;

		// Token: 0x04001D2A RID: 7466
		private Camera textureCamera;

		// Token: 0x04001D2B RID: 7467
		private Material material;

		// Token: 0x04001D2C RID: 7468
		private int renderTexIndex;

		// Token: 0x04001D2D RID: 7469
		private int defaultCullingMask;

		// Token: 0x04001D2E RID: 7470
		private RewiredActionReference actionReference = "Screenshot";

		// Token: 0x020006B9 RID: 1721
		[ConsoleCommandClassCustomizer("CameraScreenshotter")]
		private static class Console
		{
			// Token: 0x06002C79 RID: 11385 RVA: 0x000A5D52 File Offset: 0x000A4152
			[ConsoleCommand("")]
			private static void Screenshot()
			{
				Singleton<CameraScreenshotter>.instance.DebugScreenShot();
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000A5D5E File Offset: 0x000A415E
			// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000A5D6A File Offset: 0x000A416A
			[ConsoleCommand("")]
			private static float aaFactor
			{
				get
				{
					return Singleton<CameraScreenshotter>.instance.aaFactor;
				}
				set
				{
					Singleton<CameraScreenshotter>.instance.aaFactor = Mathf.Max(1f, value);
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000A5D81 File Offset: 0x000A4181
			// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000A5D8D File Offset: 0x000A418D
			[ConsoleCommand("")]
			private static int width
			{
				get
				{
					return Singleton<CameraScreenshotter>.instance.width;
				}
				set
				{
					Singleton<CameraScreenshotter>.instance.width = value;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000A5D9A File Offset: 0x000A419A
			// (set) Token: 0x06002C7F RID: 11391 RVA: 0x000A5DA6 File Offset: 0x000A41A6
			[ConsoleCommand("")]
			private static int height
			{
				get
				{
					return Singleton<CameraScreenshotter>.instance.height;
				}
				set
				{
					Singleton<CameraScreenshotter>.instance.height = value;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000A5DB3 File Offset: 0x000A41B3
			// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000A5DBF File Offset: 0x000A41BF
			[ConsoleCommand("")]
			private static bool showUi
			{
				get
				{
					return Singleton<CameraScreenshotter>.instance.showUi;
				}
				set
				{
					Singleton<CameraScreenshotter>.instance.showUi = value;
				}
			}
		}
	}
}
