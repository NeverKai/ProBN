using System;
using System.Collections.Generic;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000557 RID: 1367
	public class IslandViewfinder : UIBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland, ITargetAnimFuncs<float>
	{
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x0006DD0F File Offset: 0x0006C10F
		private AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x0006DD1C File Offset: 0x0006C11C
		private RectTransform currentTarget
		{
			get
			{
				return this.targetTransforms.Last<RectTransform>();
			}
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0006DD2C File Offset: 0x0006C12C
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			Action<float> setFunc = delegate(float v)
			{
				this.alpha = v;
				Rect screenSpaceNormalizedRect = this.currentTarget.GetScreenSpaceNormalizedRect(this.levelCamera.cameraRef);
				this.SetView(this.AnchorLerp(this.animStart, screenSpaceNormalizedRect, v));
			};
			this.animFuncs = (this.defaultAnimFuncs = this.lerpAnim);
			this.animator = new TargetAnimator<float>(() => this.alpha, setFunc, this.rootState, this);
			this.levelCamera = manager.levelCamera;
			this.ResetView();
			this.onSafeZoneChanged = delegate(Vector4 x, DeviceOrientation y)
			{
				this.Refresh();
			};
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0006DD9E File Offset: 0x0006C19E
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			MobileScreenDetector.onSafeZoneChanged += this.onSafeZoneChanged;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0006DDAB File Offset: 0x0006C1AB
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			MobileScreenDetector.onSafeZoneChanged -= this.onSafeZoneChanged;
			this.ResetView();
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0006DDBE File Offset: 0x0006C1BE
		public void Push(RectTransform target, ITargetAnimFuncs<float> animOverride = null)
		{
			if (!this.targetTransforms.Contains(target))
			{
				this.targetTransforms.Add(target);
				this.StartAnimate(animOverride);
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0006DDE4 File Offset: 0x0006C1E4
		public void Remove(RectTransform target, ITargetAnimFuncs<float> animOverride = null)
		{
			RectTransform currentTarget = this.currentTarget;
			this.targetTransforms.Remove(target);
			if (this.currentTarget != currentTarget)
			{
				this.StartAnimate(animOverride);
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0006DE20 File Offset: 0x0006C220
		private void StartAnimate(ITargetAnimFuncs<float> animOverride = null)
		{
			this.animFuncs = ((animOverride != null) ? animOverride : this.defaultAnimFuncs);
			this.animStart = this.viewTransform.GetScreenSpaceNormalizedRect(this.levelCamera.cameraRef);
			this.animator.SetTarget(1f, null, null, null, 0f, null);
			this.animator.SetCurrentAndActivate(0f);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0006DE8C File Offset: 0x0006C28C
		private void SetView(Rect rect)
		{
			rect.yMin = 0f;
			rect.yMax = 1f;
			this.viewTransform.anchorMin = rect.min;
			this.viewTransform.anchorMax = rect.max;
			this.levelCamera.UpdateViewfinderTarget(rect);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0006DEE1 File Offset: 0x0006C2E1
		[ConsoleCommand("")]
		private void Refresh()
		{
			this.StartAnimate(null);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0006DEEC File Offset: 0x0006C2EC
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.animator != null && this.levelCamera)
			{
				float current = this.animator.current;
				this.StartAnimate(null);
				this.animator.SetCurrent(Mathf.Max(current, 0.995f));
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0006DF40 File Offset: 0x0006C340
		private Rect AnchorLerp(Rect a, Rect b, float t)
		{
			Vector2 vector = Vector2.Lerp(a.min, b.min, t);
			Vector2 vector2 = Vector2.Lerp(a.max, b.max, t);
			return Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0006DF98 File Offset: 0x0006C398
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0006DFA8 File Offset: 0x0006C3A8
		private void ResetView()
		{
			this.targetTransforms.Clear();
			this.targetTransforms.Add(this.defaultTransform);
			this.animStart = this.defaultTransform.GetWorldSpaceRect();
			this.SetView(this.animStart);
			this.animator.target = 1f;
			this.animator.SetCurrent(1f);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0006E010 File Offset: 0x0006C410
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.matrix = this.levelCamera.cameraRef.cameraToWorldMatrix;
				Rect screenSpaceNormalizedRect = this.viewTransform.GetScreenSpaceNormalizedRect(this.levelCamera.cameraRef);
				Gizmos.DrawLine(this.GizmoPoint(screenSpaceNormalizedRect.xMin, screenSpaceNormalizedRect.yMin), this.GizmoPoint(screenSpaceNormalizedRect.xMin, screenSpaceNormalizedRect.yMax));
				Gizmos.DrawLine(this.GizmoPoint(screenSpaceNormalizedRect.xMin, screenSpaceNormalizedRect.yMax), this.GizmoPoint(screenSpaceNormalizedRect.xMax, screenSpaceNormalizedRect.yMax));
				Gizmos.DrawLine(this.GizmoPoint(screenSpaceNormalizedRect.xMax, screenSpaceNormalizedRect.yMax), this.GizmoPoint(screenSpaceNormalizedRect.xMax, screenSpaceNormalizedRect.yMin));
				Gizmos.DrawLine(this.GizmoPoint(screenSpaceNormalizedRect.xMax, screenSpaceNormalizedRect.yMin), this.GizmoPoint(screenSpaceNormalizedRect.xMin, screenSpaceNormalizedRect.yMin));
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0006E107 File Offset: 0x0006C507
		private Vector3 GizmoPoint(float x, float y)
		{
			return this.GizmoPoint(new Vector2(x, y));
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0006E118 File Offset: 0x0006C518
		private Vector3 GizmoPoint(Vector2 normalizedPoint)
		{
			Vector2 vector = this.levelCamera.cameraRef.GetOrthoHalfSize() * 2f;
			return new Vector3((normalizedPoint.x - 0.5f) * vector.x, (normalizedPoint.y - 0.5f) * vector.y, -(this.levelCamera.cameraRef.nearClipPlane + 0.1f));
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0006E186 File Offset: 0x0006C586
		public float UpdateCurrent(float current, float target, float dt)
		{
			return this.animFuncs.UpdateCurrent(current, target, dt);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0006E196 File Offset: 0x0006C596
		public bool ShouldTrigger(float current, float target)
		{
			return this.animFuncs.ShouldTrigger(current, target);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0006E1A5 File Offset: 0x0006C5A5
		public bool IsDone(float current, float target)
		{
			return this.animFuncs.IsDone(current, target);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0006E1B4 File Offset: 0x0006C5B4
		public void OnActivate(float current, float target)
		{
			this.animFuncs.OnActivate(current, target);
		}

		// Token: 0x04001620 RID: 5664
		[SerializeField]
		private IslandTitle title;

		// Token: 0x04001621 RID: 5665
		[SerializeField]
		private RectTransform defaultTransform;

		// Token: 0x04001622 RID: 5666
		[SerializeField]
		private RectTransform viewTransform;

		// Token: 0x04001623 RID: 5667
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04001624 RID: 5668
		private LerpTowards lerpAnim = new LerpTowards(8f, 0.25f);

		// Token: 0x04001625 RID: 5669
		private ITargetAnimFuncs<float> defaultAnimFuncs;

		// Token: 0x04001626 RID: 5670
		private ITargetAnimFuncs<float> animFuncs;

		// Token: 0x04001627 RID: 5671
		private TargetAnimator<float> animator;

		// Token: 0x04001628 RID: 5672
		private LevelCamera levelCamera;

		// Token: 0x04001629 RID: 5673
		private List<RectTransform> targetTransforms = new List<RectTransform>(4);

		// Token: 0x0400162A RID: 5674
		private Action<Vector4, DeviceOrientation> onSafeZoneChanged;

		// Token: 0x0400162B RID: 5675
		private Rect animStart = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x0400162C RID: 5676
		private float alpha;
	}
}
