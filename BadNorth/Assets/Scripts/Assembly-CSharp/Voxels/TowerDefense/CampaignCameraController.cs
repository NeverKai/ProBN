using System;
using System.Diagnostics;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000732 RID: 1842
	public class CampaignCameraController : UIBehaviour, IGameSetup, CampaignManager.INewCampaign, CampaignManager.IExitCampaign, CursorManager.IDragListener, CursorManager.ICursor
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000C36A6 File Offset: 0x000C1AA6
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000C36AE File Offset: 0x000C1AAE
		public float scrollTargetMin { get; private set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000C36B7 File Offset: 0x000C1AB7
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x000C36BF File Offset: 0x000C1ABF
		public float scrollTargetMax { get; private set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000C36C8 File Offset: 0x000C1AC8
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x000C36D0 File Offset: 0x000C1AD0
		public float seekTarget { get; private set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000C36D9 File Offset: 0x000C1AD9
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x000C36E1 File Offset: 0x000C1AE1
		public bool seeking { get; private set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000C36EC File Offset: 0x000C1AEC
		public float currentPos
		{
			get
			{
				return this.cameraRoot.transform.position.x;
			}
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06002FDE RID: 12254 RVA: 0x000C3714 File Offset: 0x000C1B14
		// (remove) Token: 0x06002FDF RID: 12255 RVA: 0x000C374C File Offset: 0x000C1B4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float, float> onLimitsUpdated = delegate(float A_0, float A_1)
		{
		};

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06002FE0 RID: 12256 RVA: 0x000C3784 File Offset: 0x000C1B84
		// (remove) Token: 0x06002FE1 RID: 12257 RVA: 0x000C37BC File Offset: 0x000C1BBC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onDrag = delegate()
		{
		};

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06002FE2 RID: 12258 RVA: 0x000C37F4 File Offset: 0x000C1BF4
		// (remove) Token: 0x06002FE3 RID: 12259 RVA: 0x000C382C File Offset: 0x000C1C2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onBackgroundClick = delegate()
		{
		};

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000C3864 File Offset: 0x000C1C64
		void IGameSetup.OnGameAwake()
		{
			this.clickListener = new CampaignCameraController.ClickListener(delegate()
			{
				this.SetVelocity(Vector2.zero);
			}, delegate()
			{
				this.onBackgroundClick();
			});
			CursorManager component = base.GetComponent<CursorManager>();
			component.Add(this);
			component.Add(this.clickListener);
			component.pointer.onStateChanged += this.OnPointerStateChanged;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C38C8 File Offset: 0x000C1CC8
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.UpdateLimits();
			this.scrollMin = this.scrollTargetMin;
			this.scrollMax = this.scrollTargetMax;
			this.cameraRoot.transform.position = this.cameraRoot.transform.position.SetX(this.campaignScrollMin);
			this.campaign.Target = campaign;
			LevelNode levelNode = campaign.levels[campaign.campaignSave.lastPlayedLevelIdx];
			if (campaign.trialOver)
			{
				this.SnapTo(levelNode);
			}
			else
			{
				this.SeekTo(levelNode, false);
			}
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C3960 File Offset: 0x000C1D60
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.campaign.Target = null;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000C396E File Offset: 0x000C1D6E
		protected override void Awake()
		{
			base.Awake();
			CampaignCameraController.Instance = this;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000C397C File Offset: 0x000C1D7C
		protected override void OnDestroy()
		{
			base.OnDestroy();
			CampaignCameraController.Instance = null;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000C398A File Offset: 0x000C1D8A
		protected override void OnEnable()
		{
			base.OnEnable();
			ClaimableAudioListener.AddClaim(this.cameraRef, false);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000C399E File Offset: 0x000C1D9E
		protected override void OnDisable()
		{
			base.OnDisable();
			ClaimableAudioListener.RemoveClaim(this.cameraRef);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000C39B1 File Offset: 0x000C1DB1
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateLimits();
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000C39B9 File Offset: 0x000C1DB9
		public void SetVelocity(Vector2 vel)
		{
			this.CancelSeek();
			this.velocity = vel;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000C39C8 File Offset: 0x000C1DC8
		public bool IsVisible(float xPos)
		{
			float num = this.borderBack * 0.45f;
			float num2 = this.scrollTargetMin - this.cameraRef.GetOrthoHalfWidth() + num;
			float num3 = this.scrollTargetMax + this.cameraRef.GetOrthoHalfWidth() - num;
			return xPos >= num2 && xPos <= num3;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000C3A20 File Offset: 0x000C1E20
		public void SeekTo(LevelNode levelNode, bool force = false)
		{
			if (levelNode)
			{
				this.SeekTo(levelNode.transform.position.x, force);
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000C3A54 File Offset: 0x000C1E54
		public void SeekTo(float position, bool force = false)
		{
			float x = this.cameraRoot.transform.position.x;
			float num = this.cameraRef.GetOrthoHalfWidth() * this.seekMarginMax;
			float num2 = (!this.seeking) ? 0f : (this.seekTarget - x);
			float num3 = position - x;
			bool flag = num2 * num3 > 0f;
			if (!force && !flag && Mathf.Abs(num3) < num)
			{
				return;
			}
			float num4 = this.cameraRef.GetOrthoHalfWidth() * this.seekMarginMin;
			position = x + Mathf.Max(Mathf.Abs(num3) - num4, 0f) * Mathf.Sign(num3);
			position = Mathf.Clamp(position, this.scrollTargetMin, this.scrollTargetMax);
			this.seekTarget = position;
			this.seeking = true;
			this.velocity = Vector2.zero;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000C3B39 File Offset: 0x000C1F39
		public void CancelSeek()
		{
			this.seekTarget = 0f;
			this.seeking = false;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000C3B50 File Offset: 0x000C1F50
		public void SnapTo(LevelNode levelNode)
		{
			if (levelNode)
			{
				this.SnapTo(levelNode.transform.position.x);
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000C3B84 File Offset: 0x000C1F84
		public void SnapTo(float position)
		{
			this.seeking = false;
			this.velocity = Vector2.zero;
			this.seekTarget = position;
			this.scrollMax = this.scrollTargetMax;
			this.scrollMin = this.scrollTargetMin;
			this.cameraRoot.transform.position = this.cameraRoot.transform.position.SetX(position);
			this.ClampCamera();
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000C3BF0 File Offset: 0x000C1FF0
		private void Update()
		{
			if (this.seeking)
			{
				Vector3 position = this.cameraRoot.transform.position;
				Vector3 position2 = position;
				position2.x = this.seekTarget;
				position.x = Mathf.MoveTowards(position.x, position2.x, Time.unscaledDeltaTime * this.minSeekSpeed);
				position2.x = Mathf.Lerp(position.x, position2.x, Time.unscaledDeltaTime * this.seekLerp);
				position2.x = Mathf.MoveTowards(position.x, position2.x, this.maxSeekSpeed * Time.unscaledDeltaTime);
				this.cameraRoot.transform.position = position2;
				if (position2.x == this.seekTarget)
				{
					this.seeking = false;
				}
			}
			else if (!this.dragging)
			{
				this.cameraRoot.transform.position += this.velocity * Time.deltaTime;
			}
			this.ClampCamera();
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000C3D0C File Offset: 0x000C210C
		private void ClampCamera()
		{
			this.scrollMin = Mathf.Lerp(this.scrollMin, this.scrollTargetMin, Time.unscaledDeltaTime * 3f);
			this.scrollMax = Mathf.Lerp(this.scrollMax, this.scrollTargetMax, Time.unscaledDeltaTime * 3f);
			Vector3 position = this.cameraRoot.transform.position;
			position.x = Mathf.Clamp(position.x, this.scrollMin, this.scrollMax);
			this.cameraRoot.transform.position = position;
			Rect screenSpaceNormalizedRect = this.cameraViewport.GetScreenSpaceNormalizedRect(this.cameraRef);
			Rect screenSpaceNormalizedRect2 = this.cameraViewPortSizeLimiter.GetScreenSpaceNormalizedRect(this.cameraRef);
			screenSpaceNormalizedRect.yMax = Mathf.Min(screenSpaceNormalizedRect.yMax, screenSpaceNormalizedRect2.yMax);
			RectTransform sizeIndicator = this.gameOverHeader.sizeIndicator;
			if (sizeIndicator.gameObject.activeInHierarchy)
			{
				float num = Mathf.Min((sizeIndicator.GetScreenSpaceNormalizedRect(this.cameraRef).yMin - screenSpaceNormalizedRect.yMin) / (1f - screenSpaceNormalizedRect.yMin), 1f);
				screenSpaceNormalizedRect.yMax = screenSpaceNormalizedRect.yMin + screenSpaceNormalizedRect.height * num;
			}
			screenSpaceNormalizedRect.yMin = Mathf.Max(screenSpaceNormalizedRect.yMin, screenSpaceNormalizedRect2.yMin);
			this.cameraRef.orthographicSize = this.campaign.Target.rect.size.y * 0.5f / screenSpaceNormalizedRect.height;
			this.cameraRef.transform.SetLocalY((0.5f - screenSpaceNormalizedRect.center.y) * this.cameraRef.orthographicSize * 2f);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000C3ED8 File Offset: 0x000C22D8
		private void FixedUpdate()
		{
			if (!this.dragging)
			{
				this.velocity *= this.freeMoveSpeedMultiplier;
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000C3EFC File Offset: 0x000C22FC
		public void UpdateLimits()
		{
			Campaign campaign = (!Singleton<CampaignManager>.instance) ? null : Singleton<CampaignManager>.instance.campaign;
			if (!campaign || campaign.levels == null || campaign.levels.Count == 0)
			{
				return;
			}
			float orthoHalfWidth = this.cameraRef.GetOrthoHalfWidth();
			int vikingFrontierPosition = Profile.campaign.vikingFrontierPosition;
			this.scrollTargetMin = float.MaxValue;
			this.scrollTargetMax = -this.scrollTargetMin;
			this.campaignScrollMin = campaign.startLevel.transform.position.x - this.cameraRef.GetOrthoHalfWidth();
			if (!campaign.startLevel.IsBehindFrontier())
			{
				float x = campaign.startLevel.transform.position.x;
				this.scrollTargetMax = x;
				this.scrollTargetMin = x;
			}
			int num = 0;
			float num2 = 0f;
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				float x2 = levelNode.transform.position.x;
				if (levelNode.frontierDepth == vikingFrontierPosition)
				{
					num++;
					num2 += x2;
				}
				if (levelNode.ShouldBeVisible())
				{
					this.scrollTargetMin = Mathf.Min(this.scrollTargetMin, x2);
					this.scrollTargetMax = Mathf.Max(this.scrollTargetMax, x2);
				}
			}
			if (num > 0)
			{
				num2 /= (float)num;
			}
			this.scrollTargetMin = Mathf.Min(this.scrollTargetMin, num2);
			this.scrollTargetMin -= this.borderBack;
			this.scrollTargetMax += this.borderFront;
			this.scrollTargetMin += orthoHalfWidth;
			this.scrollTargetMax = Mathf.Max(this.scrollTargetMin + this.minimumScrollDist, this.scrollTargetMax - orthoHalfWidth);
			if (campaign.campaignSave.wonGame)
			{
				this.scrollTargetMax = campaign.endLevel.transform.position.x;
			}
			this.onLimitsUpdated(this.scrollTargetMin - orthoHalfWidth, this.scrollTargetMax + orthoHalfWidth);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000C4168 File Offset: 0x000C2568
		void CursorManager.ICursor.SetActive(bool active)
		{
			this.dragging = false;
			this.velocity = Vector3.zero;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000C4181 File Offset: 0x000C2581
		void CursorManager.IDragListener.OnDragStart(PointerEventData.InputButton button)
		{
			this.CancelSeek();
			this.upgradesProxy.CloseMenu();
			this.dragging = true;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000C419C File Offset: 0x000C259C
		void CursorManager.IDragListener.OnDragEnd(PointerEventData.InputButton button)
		{
			if (Time.unscaledTime - this.lastDragTime > this.dragReleaseThresholdTime)
			{
				this.SetVelocity(Vector3.zero);
			}
			else
			{
				this.velocity.x = Mathf.Sign(this.velocity.x) * Mathf.Min(Mathf.Abs(this.velocity.x), this.dragReleaseSpeedLimit);
				this.velocity *= this.dragReleaseSpeedMultiplier;
			}
			this.lastDragTime = 0f;
			this.dragging = false;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000C4238 File Offset: 0x000C2638
		void CursorManager.IDragListener.OnDrag(PointerEventData.InputButton button, Vector2 inDelta)
		{
			Vector2 vector = -(inDelta * this.cameraRef.orthographicSize * 2f) / (float)this.cameraRef.pixelHeight;
			vector.y = 0f;
			this.cameraRoot.position += vector;
			float num = Mathf.Max(Time.unscaledTime - this.lastDragTime, Time.unscaledDeltaTime);
			if (num > this.dragReleaseThresholdTime)
			{
				this.SetVelocity(vector / Time.unscaledDeltaTime);
			}
			else
			{
				this.SetVelocity(Vector2.Lerp(this.velocity, vector / num, num * 15f));
			}
			this.lastDragTime = Time.unscaledTime;
			this.onDrag();
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000C430D File Offset: 0x000C270D
		private void OnPointerStateChanged(PointerRationalizer.State state)
		{
			if (state != PointerRationalizer.State.None)
			{
				this.upgradesProxy.CloseMenu();
			}
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000C4320 File Offset: 0x000C2720
		void CursorManager.IDragListener.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
		{
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000C4324 File Offset: 0x000C2724
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Vector3 center = new Vector3(Mathf.Lerp(this.scrollTargetMin, this.scrollTargetMax, 0.5f), 0f, 0f);
			Vector3 size = new Vector3(this.scrollTargetMax - this.scrollTargetMin - 3f + this.cameraRef.GetOrthoHalfWidth() * 2f, this.cameraRef.GetOrthoHalfHeight() * 2f - 3f, 0f);
			Gizmos.DrawWireCube(center, size);
		}

		// Token: 0x04001FF5 RID: 8181
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CampaignCameraController", EVerbosity.Quiet, 100);

		// Token: 0x04001FF6 RID: 8182
		[SerializeField]
		private Transform cameraRoot;

		// Token: 0x04001FF7 RID: 8183
		[SerializeField]
		private Camera cameraRef;

		// Token: 0x04001FF8 RID: 8184
		[SerializeField]
		public RectTransform cameraViewport;

		// Token: 0x04001FF9 RID: 8185
		[SerializeField]
		public RectTransform cameraViewPortSizeLimiter;

		// Token: 0x04001FFA RID: 8186
		[SerializeField]
		private CampaignMapGameOverHeader gameOverHeader;

		// Token: 0x04001FFB RID: 8187
		[SerializeField]
		public SuperUpgradesCampaignProxy upgradesProxy;

		// Token: 0x04001FFC RID: 8188
		[Header("Limits")]
		[SerializeField]
		[ConsoleCommand("")]
		private bool limited = true;

		// Token: 0x04001FFD RID: 8189
		[SerializeField]
		private float borderBack = 60f;

		// Token: 0x04001FFE RID: 8190
		[SerializeField]
		private float borderFront = 20f;

		// Token: 0x04001FFF RID: 8191
		[SerializeField]
		private float minimumScrollDist = 25f;

		// Token: 0x04002000 RID: 8192
		[Header("speed")]
		[SerializeField]
		private float seekLerp = 5f;

		// Token: 0x04002001 RID: 8193
		[SerializeField]
		private float minSeekSpeed = 3f;

		// Token: 0x04002002 RID: 8194
		[SerializeField]
		private float maxSeekSpeed = 230f;

		// Token: 0x04002003 RID: 8195
		[SerializeField]
		[Range(0f, 1f)]
		private float seekMarginMin = 0.025f;

		// Token: 0x04002004 RID: 8196
		[SerializeField]
		[Range(0f, 1f)]
		private float seekMarginMax = 0.3f;

		// Token: 0x04002005 RID: 8197
		[Header("drag")]
		[SerializeField]
		private float dragReleaseSpeedMultiplier = 0.85f;

		// Token: 0x04002006 RID: 8198
		[SerializeField]
		private float dragReleaseSpeedLimit = 220f;

		// Token: 0x04002007 RID: 8199
		[SerializeField]
		private float freeMoveSpeedMultiplier = 0.85f;

		// Token: 0x04002008 RID: 8200
		[SerializeField]
		private float dragReleaseThresholdTime = 0.15f;

		// Token: 0x04002009 RID: 8201
		private float campaignScrollMin;

		// Token: 0x0400200C RID: 8204
		private float scrollMin;

		// Token: 0x0400200D RID: 8205
		private float scrollMax;

		// Token: 0x0400200E RID: 8206
		private bool dragging;

		// Token: 0x0400200F RID: 8207
		private float lastDragTime;

		// Token: 0x04002010 RID: 8208
		private Vector2 velocity;

		// Token: 0x04002013 RID: 8211
		private WeakReference<Campaign> campaign = new WeakReference<Campaign>(null);

		// Token: 0x04002014 RID: 8212
		private CampaignCameraController.ClickListener clickListener;

		// Token: 0x04002018 RID: 8216
		public static CampaignCameraController Instance;

		// Token: 0x02000733 RID: 1843
		private class ClickListener : CursorManager.IPointerCursor, CursorManager.ICursor
		{
			// Token: 0x06003004 RID: 12292 RVA: 0x000C43D3 File Offset: 0x000C27D3
			private ClickListener()
			{
			}

			// Token: 0x06003005 RID: 12293 RVA: 0x000C43DC File Offset: 0x000C27DC
			public ClickListener(Action onButtonDown = null, Action onButtonUp = null)
			{
				Action action;
				if (onButtonDown != null)
				{
					action = onButtonDown;
				}
				else
				{
					action = delegate()
					{
					};
				}
				this.onButtonDown = action;
				Action action2;
				if (onButtonUp != null)
				{
					action2 = onButtonUp;
				}
				else
				{
					action2 = delegate()
					{
					};
				}
				this.onButtonUp = action2;
			}

			// Token: 0x06003006 RID: 12294 RVA: 0x000C444D File Offset: 0x000C284D
			void CursorManager.IPointerCursor.OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
			{
				this.onButtonDown();
			}

			// Token: 0x06003007 RID: 12295 RVA: 0x000C445A File Offset: 0x000C285A
			void CursorManager.IPointerCursor.OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos)
			{
				this.onButtonUp();
			}

			// Token: 0x06003008 RID: 12296 RVA: 0x000C4467 File Offset: 0x000C2867
			void CursorManager.IPointerCursor.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
			{
			}

			// Token: 0x06003009 RID: 12297 RVA: 0x000C4469 File Offset: 0x000C2869
			void CursorManager.ICursor.SetActive(bool active)
			{
			}

			// Token: 0x0600300A RID: 12298 RVA: 0x000C446B File Offset: 0x000C286B
			void CursorManager.IPointerCursor.UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
			{
			}

			// Token: 0x0400201C RID: 8220
			private Action onButtonUp;

			// Token: 0x0400201D RID: 8221
			private Action onButtonDown;
		}
	}
}
