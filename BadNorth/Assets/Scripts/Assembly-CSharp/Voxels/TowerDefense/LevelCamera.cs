using System;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C4 RID: 1732
	public class LevelCamera : Singleton<LevelCamera>, ILevelCamera, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000A714E File Offset: 0x000A554E
		public Camera cameraRef
		{
			get
			{
				return this._camera;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000A7156 File Offset: 0x000A5556
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000A715E File Offset: 0x000A555E
		public float targetPanY { get; private set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000A7167 File Offset: 0x000A5567
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000A716F File Offset: 0x000A556F
		public bool lockPanY { get; private set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000A7178 File Offset: 0x000A5578
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000A7180 File Offset: 0x000A5580
		public int cullingMask { get; private set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000A7189 File Offset: 0x000A5589
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000A7191 File Offset: 0x000A5591
		public float orthoHeight
		{
			get
			{
				return this._orthoHeight;
			}
			set
			{
				this._orthoHeight = value;
				this.UpdateCameraOrtho();
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000A71A0 File Offset: 0x000A55A0
		private void UpdateCameraOrtho()
		{
			this.cameraRef.orthographicSize = this._orthoHeight * 0.5f * this.currentSquadSelectZoom;
			this.UpdateViewfinderPan();
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000A71C8 File Offset: 0x000A55C8
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x000A71F0 File Offset: 0x000A55F0
		[ConsoleCommand("")]
		public float yaw
		{
			get
			{
				return this.yawTransform.rotation.eulerAngles.y;
			}
			set
			{
				this.yawTransform.rotation = Quaternion.Euler(0f, value, 0f);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000A720D File Offset: 0x000A560D
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000A721C File Offset: 0x000A561C
		[ConsoleCommand("")]
		public Vector3 position
		{
			get
			{
				return this.panYTransform.position;
			}
			set
			{
				if (this.lockPanY)
				{
					value.y = this.panYTransform.position.y;
				}
				this.panYTransform.position = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000A725A File Offset: 0x000A565A
		public Rect screenRect
		{
			get
			{
				return this.currentScreenRect;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000A7262 File Offset: 0x000A5662
		public Rect defaultScreenRect
		{
			get
			{
				return this._defaultScreenRect;
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000A726A File Offset: 0x000A566A
		public void LockPanYPos(float pos)
		{
			this.lockPanY = true;
			this.targetPanY = pos;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000A727A File Offset: 0x000A567A
		public void UnlockPanYPos()
		{
			this.lockPanY = false;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000A7284 File Offset: 0x000A5684
		private void Update()
		{
			EnglishSquad englishSquad = this.squadSelector.selectedSquad;
			if (englishSquad && !englishSquad.alive)
			{
				englishSquad = null;
			}
			float b = (Time.timeScale <= 0f || Time.timeScale >= 1f) ? 1f : this.squadSelectZoom;
			this.currentSquadSelectZoom = Mathf.Lerp(this.currentSquadSelectZoom, b, Time.unscaledDeltaTime * this.squadSelectTransitionSpeed);
			this.UpdateCameraOrtho();
			Vector3 b2 = (!englishSquad) ? Vector3.zero : (englishSquad.heroAgent.transform.position * this.squadSelectPullFactor);
			this.currentSquadSelectOffset = Vector3.Lerp(this.currentSquadSelectOffset, b2, Time.unscaledDeltaTime * this.squadSelectTransitionSpeed);
			this.rootPanTransform.transform.position = this.currentSquadSelectOffset;
			float t = Time.unscaledDeltaTime * 5f;
			if (this.lockPanY)
			{
				this.panYTransform.SetY(Mathf.Lerp(this.panYTransform.position.y, this.targetPanY, t));
			}
			this.cameraRef.useOcclusionCulling = false;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000A73BC File Offset: 0x000A57BC
		public void UpdateViewfinderTarget(Rect rect)
		{
			this.currentScreenRect = rect;
			this.UpdateViewfinderPan();
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000A73CC File Offset: 0x000A57CC
		private void UpdateViewfinderPan()
		{
			this.panXTransform.SetLocalX(-(this.currentScreenRect.center.x - 0.5f) * this.cameraRef.GetOrthoHalfWidth() * 2f);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000A7414 File Offset: 0x000A5814
		protected override void Awake()
		{
			base.Awake();
			this.cullingMask = this.cameraRef.cullingMask;
			this.pitchTransform.localRotation = Quaternion.Euler(30f, 0f, 0f);
			LevelCamera.SubscribeToBloodSettings(this.cameraRef);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000A7464 File Offset: 0x000A5864
		private static void SetBloodSettings(Camera camera, UserSettings settings)
		{
			if (settings.showBlood)
			{
				camera.cullingMask |= 1 << LayerMaster.blood.id;
			}
			else
			{
				camera.cullingMask &= ~(1 << LayerMaster.blood.id);
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000A74BC File Offset: 0x000A58BC
		public static void SubscribeToBloodSettings(Camera camera)
		{
			UserSettings.onUpdated += delegate(UserSettings x)
			{
				LevelCamera.SetBloodSettings(camera, x);
			};
			LevelCamera.SetBloodSettings(camera, Profile.userSettings);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000A74F7 File Offset: 0x000A58F7
		private void OnEnable()
		{
			ClaimableAudioListener.AddClaim(this, false);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000A7500 File Offset: 0x000A5900
		private void OnDisable()
		{
			ClaimableAudioListener.RemoveClaim(this);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000A7508 File Offset: 0x000A5908
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.squadSelector = manager.squadSelector;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000A7516 File Offset: 0x000A5916
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.ResetView();
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000A751E File Offset: 0x000A591E
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.ResetView();
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000A7528 File Offset: 0x000A5928
		private void ResetView()
		{
			this.UnlockPanYPos();
			this.panXTransform.SetLocalX(0f);
			this.currentSquadSelectZoom = 1f;
			this.currentSquadSelectOffset = Vector3.zero;
			this.rootPanTransform.transform.position = Vector3.zero;
			this.currentScreenRect = this.defaultScreenRect;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000A7584 File Offset: 0x000A5984
		public void FocusOnIsland(bool includeCoins, float margin = 0.1f)
		{
			CameraLimiter component = base.GetComponent<CameraLimiter>();
			CameraZoomer component2 = base.GetComponent<CameraZoomer>();
			float num2;
			float num3;
			float num = component.GetIslandHeight(includeCoins, out num2, out num3);
			if (Platform.Is(EPlatform.PC) && Profile.userSettings.pcScaleMode == PlatformCanvasScaler.PCScaleMode.Desktop)
			{
				num = Mathf.Max(num, 7f);
			}
			float b = num * this.GetDisplayRatio() / ((1f - margin) * Constants.upScale);
			component2.ZoomTo(Mathf.Max(component.islandWidth, b));
			component2.LockZoom = true;
			this.LockPanYPos((num3 + num2) * 0.5f);
			Debug.DrawLine(Vector3.up * num2, Vector3.up * num3, Color.red, 12f);
		}

		// Token: 0x04001D72 RID: 7538
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("LevelCamera", EVerbosity.Quiet, 0);

		// Token: 0x04001D73 RID: 7539
		[Header("ObjectReferences")]
		[SerializeField]
		private Camera _camera;

		// Token: 0x04001D74 RID: 7540
		[SerializeField]
		private Transform rootPanTransform;

		// Token: 0x04001D75 RID: 7541
		[SerializeField]
		private Transform panYTransform;

		// Token: 0x04001D76 RID: 7542
		[SerializeField]
		private Transform panXTransform;

		// Token: 0x04001D77 RID: 7543
		[SerializeField]
		private Transform yawTransform;

		// Token: 0x04001D78 RID: 7544
		[SerializeField]
		private Transform pitchTransform;

		// Token: 0x04001D79 RID: 7545
		[Header("ViewPort")]
		[SerializeField]
		private float squadSelectZoom = 0.975f;

		// Token: 0x04001D7A RID: 7546
		[SerializeField]
		private float squadSelectPullFactor = 1.04f;

		// Token: 0x04001D7B RID: 7547
		[SerializeField]
		private float squadSelectTransitionSpeed = 11f;

		// Token: 0x04001D7C RID: 7548
		[SerializeField]
		private Rect _defaultScreenRect = new Rect(0.025f, 0.05f, 0.95f, 0.85f);

		// Token: 0x04001D7D RID: 7549
		private Rect currentScreenRect;

		// Token: 0x04001D81 RID: 7553
		private SquadSelector squadSelector;

		// Token: 0x04001D82 RID: 7554
		private float currentSquadSelectZoom = 1f;

		// Token: 0x04001D83 RID: 7555
		private Vector3 currentSquadSelectOffset = Vector3.zero;

		// Token: 0x04001D84 RID: 7556
		private float _orthoHeight = 10f;
	}
}
