using System;
using System.Collections.Generic;
using Fabric;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200093F RID: 2367
	public class WorldSpaceNavSpotCursor : MonoBehaviour, CursorManager.IJoystickCursor, CursorManager.ICursor
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x00123254 File Offset: 0x00121654
		private EnglishSquad squad
		{
			get
			{
				return this._squad;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x00123261 File Offset: 0x00121661
		// (set) Token: 0x0600400E RID: 16398 RVA: 0x0012326C File Offset: 0x0012166C
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
			set
			{
				NavSpot navSpot = this._navSpot;
				this._navSpot = value;
				this.SnapTo(this._navSpot);
				if (this.navSpotChangedAction != null && navSpot != this._navSpot)
				{
					this.navSpotChangedAction(this._navSpot);
				}
			}
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x001232C0 File Offset: 0x001216C0
		public void Init(EnglishSquad squad, Island island, Action<NavSpot> selectAction, Action<NavSpot> navSpotChangedAction)
		{
			this._squad.Target = squad;
			this.selectAction = selectAction;
			this.navSpotChangedAction = navSpotChangedAction;
			this.navSpotChangedAction = (Action<NavSpot>)Delegate.Combine(this.navSpotChangedAction, new Action<NavSpot>(this.NavspotHighlightAudio));
			this.mover.Init(island);
			this.mover.onPostMove += this.PostMove;
			this.candidates = new List<NavSpot>(island.navSpotter.navSpots.Count);
			this.islandDiagonal = (island.size - Vector3.one).SetY(0f).magnitude;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0012337C File Offset: 0x0012177C
		private void NavspotHighlightAudio(NavSpot navSpot)
		{
			if (navSpot != null)
			{
				NavSpot heroNavSpot = this.squad.GetHeroNavSpot();
				float num = (!heroNavSpot) ? 0f : (heroNavSpot.transform.position - navSpot.transform.position).SetY(0f).magnitude;
				EventManager.Instance.PostEvent(WorldSpaceNavSpotCursor.hoverAudio, navSpot.gameObject);
				EventManager.Instance.SetParameter(WorldSpaceNavSpotCursor.hoverAudio, "Distance", num / this.islandDiagonal, navSpot.gameObject);
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x00123428 File Offset: 0x00121828
		public void SetCandidates(List<NavSpot> candidates)
		{
			this.candidates.Clear();
			this.candidates.AddRange(candidates);
			if (this.navSpot && !this.candidates.Contains(this.navSpot))
			{
				this.navSpot = null;
			}
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0012347C File Offset: 0x0012187C
		public void SetCandidates(List<TargetNavSpot> candidates)
		{
			this.candidates.Clear();
			foreach (TargetNavSpot targetNavSpot in candidates)
			{
				this.candidates.Add(targetNavSpot.navSpot);
			}
			if (this.navSpot && !this.candidates.Contains(this.navSpot))
			{
				this.navSpot = null;
			}
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00123518 File Offset: 0x00121918
		private void OnInputReleased()
		{
			NavSpot navSpot = this.navSpot;
			if (!this.input.hasMoved)
			{
				Vector3 currentPos = (!this.navSpot) ? this.mover.basePosition : this.navSpot.transform.position;
				NavSpot flickTarget = this.GetFlickTarget(currentPos, this.input.previousVec.normalized);
				navSpot = ((!flickTarget) ? navSpot : flickTarget);
			}
			if (navSpot)
			{
				FabricWrapper.PostEvent("UI/InGame/TileHover", navSpot.gameObject);
			}
			this.navSpot = navSpot;
			this.input.Clear();
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x001235C6 File Offset: 0x001219C6
		private NavSpot GetFlickTarget(Vector3 currentPos, Vector3 inputNormal)
		{
			return this.weighter.GetHighestWeighted<NavSpot>(currentPos, this.candidates, inputNormal);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x001235DC File Offset: 0x001219DC
		private void Update()
		{
			Vector3 floorPos = this.mover.floorPosition;
			if (!this.receivedAnyInput)
			{
				floorPos = this.squad.heroAgent.transform.position;
				this.UpdateNavSpot();
			}
			Ray ray = new Ray(this.mover.transform.position, Vector3.down);
			RaycastHit raycastHit;
			if (Physics.SphereCast(ray, 0.075f, out raycastHit, 10f, LayerMaster.cursorShadowMask))
			{
				floorPos.y = Mathf.Max(raycastHit.point.y, floorPos.y);
			}
			this.icon.UpdateFloorPos(floorPos, false);
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0012368B File Offset: 0x00121A8B
		private void PostMove()
		{
			this.UpdateNavSpot();
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x00123693 File Offset: 0x00121A93
		private void SnapTo(NavSpot navSpot)
		{
			if (navSpot)
			{
				this.mover.TeleportTo(navSpot.transform.position);
			}
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x001236B8 File Offset: 0x00121AB8
		private void UpdateNavSpot()
		{
			if (!this.receivedAnyInput)
			{
				if (!this.requiresSomeInput)
				{
					this.navSpot = this.GetNavSpot(this.squad.heroAgent.transform.position);
				}
				return;
			}
			NavSpot navSpot = this._navSpot;
			this._navSpot = this.GetNavSpot(this.mover.floorPosition);
			if (this._navSpot != navSpot)
			{
				this.input.hasMoved = true;
				if (this.navSpotChangedAction != null)
				{
					this.navSpotChangedAction(this._navSpot);
				}
			}
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x00123754 File Offset: 0x00121B54
		private Vector3 ApplyDeadZone(Vector3 input)
		{
			Vector3 vector;
			vector.x = WorldSpaceNavSpotCursor.ApplyDeadZone(input.x, this.ComputeDeadzone(input.z));
			vector.z = WorldSpaceNavSpotCursor.ApplyDeadZone(input.z, this.ComputeDeadzone(input.x));
			vector.y = 0f;
			vector = vector.normalized * input.magnitude * 1.1f;
			vector = vector.GetClampedMagnitude(1f);
			return vector;
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x001237D8 File Offset: 0x00121BD8
		private float ComputeDeadzone(float complimentaryAxis)
		{
			float f = WorldSpaceNavSpotCursor.ApplyDeadZone(complimentaryAxis, 0.025f);
			return Mathf.Lerp(0.025f, 0.35f, Mathf.Abs(f));
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x00123808 File Offset: 0x00121C08
		private static float ApplyDeadZone(float axis, float deadzone)
		{
			float num = Mathf.Abs(axis);
			if (num < deadzone)
			{
				return 0f;
			}
			float num2 = Mathf.Sign(axis);
			return Mathf.InverseLerp(deadzone, 1f, num) * num2;
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x00123840 File Offset: 0x00121C40
		private NavSpot GetNavSpot(Vector3 position)
		{
			Bounds bounds = new Bounds(position, new Vector3(1.2f, 1f, 1.2f));
			NavSpot result = null;
			float num = float.MaxValue;
			foreach (NavSpot navSpot in this.candidates)
			{
				Bounds meshBounds = navSpot.meshBounds;
				float num2 = Vector3.SqrMagnitude(meshBounds.center - position);
				if (num2 < num && meshBounds.Intersects(bounds))
				{
					result = navSpot;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x001238F4 File Offset: 0x00121CF4
		void CursorManager.ICursor.SetActive(bool active)
		{
			this.receivedAnyInput = false;
			this.navSpot = null;
			if (active)
			{
				if (!this.icon)
				{
					this.icon = Singleton<IslandGameplayManager>.instance.worldSpaceCursorIconPool.GetInstance(this.squad);
					this.icon.UpdateFloorPos(this.squad.heroAgent.transform.position, false);
					if (!this.initAtHero)
					{
						NavSpot navSpot = this.squad.navSpotOccupant.navSpot;
						if (navSpot && navSpot.distanceField.SampleDistance(this.squad.heroAgent.navPos) > 0.33f)
						{
							this.icon.UpdateFloorPos(navSpot.transform.position, false);
							this.navSpot = navSpot;
							this.receivedAnyInput = true;
						}
					}
				}
				if (this.icon.isNew)
				{
					Vector3 currentPos = ((!this.navSpot) ? this.squad.heroAgent.transform.position : this.navSpot.transform.position) - Vector3.up * 0.5f;
					this.icon.SetCurrentPos(currentPos);
				}
			}
			else if (this.icon)
			{
				this.icon.Deactivate();
				this.icon = null;
			}
			this.input.Clear();
			base.gameObject.SetActive(active);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x00123A7C File Offset: 0x00121E7C
		void CursorManager.IJoystickCursor.SetMoveInput(Vector2 moveInput)
		{
			Vector3 vector = Singleton<LevelCamera>.instance.cameraRef.GetTopDownInputVector(moveInput);
			vector = this.ApplyDeadZone(vector);
			if (vector == Vector3.zero)
			{
				if (this.input.previousVec != Vector3.zero)
				{
					if (this.input.time < 0.175f || this.input.releaseTime > 0.125f)
					{
						this.OnInputReleased();
					}
					this.input.releaseTime = this.input.releaseTime + Time.unscaledDeltaTime;
				}
			}
			else
			{
				if (!this.receivedAnyInput)
				{
					this.receivedAnyInput = true;
					if (!this.navSpot)
					{
						this.navSpot = this.GetFlickTarget(this.squad.heroAgent.transform.position, vector.normalized);
						this.mover.InvalidateInput(0.0667f);
						this.input.hasMoved = true;
					}
				}
				this.input.releaseTime = 0f;
				this.input.previousVec = this.input.currentVec;
				this.input.time = this.input.time + Time.unscaledDeltaTime;
			}
			this.input.currentVec = vector;
			this.mover.SetInput(this.input.currentVec);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x00123BE0 File Offset: 0x00121FE0
		void CursorManager.IJoystickCursor.OnSelectButtonDown()
		{
			if (this.selectAction != null && this.navSpot != null)
			{
				this.icon.SetTargetPos(this.navSpot.transform.position - Vector3.up * 0.5f, false);
				this.selectAction(this.navSpot);
				base.gameObject.SetActive(false);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x00123C66 File Offset: 0x00122066
		private void OnDestroy()
		{
			this.icon = null;
			this._navSpot = null;
			this.candidates = null;
			this.selectAction = null;
			this.navSpotChangedAction = null;
			this.mover = null;
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x00123C92 File Offset: 0x00122092
		void CursorManager.IJoystickCursor.OnSelectButtonUp()
		{
		}

		// Token: 0x04002CEF RID: 11503
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("NavSpotCursor", EVerbosity.Quiet, 0);

		// Token: 0x04002CF0 RID: 11504
		private static FabricEventReference hoverAudio = "UI/InGame/TileDistance";

		// Token: 0x04002CF1 RID: 11505
		private const float deadzoneMin = 0.025f;

		// Token: 0x04002CF2 RID: 11506
		private const float deadzoneMax = 0.35f;

		// Token: 0x04002CF3 RID: 11507
		private const float maxFlickTime = 0.175f;

		// Token: 0x04002CF4 RID: 11508
		private const float releaseTime = 0.125f;

		// Token: 0x04002CF5 RID: 11509
		[Header("SCene References")]
		[SerializeField]
		private WorldSpaceCursorMover mover;

		// Token: 0x04002CF6 RID: 11510
		[Header("Tuning")]
		[SerializeField]
		private TopDownWeighter weighter = new TopDownWeighter(50f, 2.3f);

		// Token: 0x04002CF7 RID: 11511
		private WeakReference<EnglishSquad> _squad = new WeakReference<EnglishSquad>(null);

		// Token: 0x04002CF8 RID: 11512
		private WorldSpaceCursorIcon icon;

		// Token: 0x04002CF9 RID: 11513
		private NavSpot _navSpot;

		// Token: 0x04002CFA RID: 11514
		private WorldSpaceNavSpotCursor.InputState input;

		// Token: 0x04002CFB RID: 11515
		private bool receivedAnyInput;

		// Token: 0x04002CFC RID: 11516
		private List<NavSpot> candidates;

		// Token: 0x04002CFD RID: 11517
		private Action<NavSpot> selectAction;

		// Token: 0x04002CFE RID: 11518
		private Action<NavSpot> navSpotChangedAction;

		// Token: 0x04002CFF RID: 11519
		private float islandDiagonal;

		// Token: 0x04002D00 RID: 11520
		[NonSerialized]
		public bool initAtHero = true;

		// Token: 0x04002D01 RID: 11521
		[NonSerialized]
		public bool requiresSomeInput = true;

		// Token: 0x02000940 RID: 2368
		private struct InputState
		{
			// Token: 0x06004023 RID: 16419 RVA: 0x00123CA5 File Offset: 0x001220A5
			public void Clear()
			{
				this = default(WorldSpaceNavSpotCursor.InputState);
			}

			// Token: 0x04002D02 RID: 11522
			public Vector3 previousVec;

			// Token: 0x04002D03 RID: 11523
			public Vector3 currentVec;

			// Token: 0x04002D04 RID: 11524
			public float time;

			// Token: 0x04002D05 RID: 11525
			public float releaseTime;

			// Token: 0x04002D06 RID: 11526
			public bool hasMoved;
		}
	}
}
