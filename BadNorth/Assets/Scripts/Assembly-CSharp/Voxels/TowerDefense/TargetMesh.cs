using System;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense
{
	// Token: 0x0200089D RID: 2205
	public class TargetMesh : MonoBehaviour, IPoolable
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000FC582 File Offset: 0x000FA982
		private Mesh mesh
		{
			get
			{
				return this._mesh;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000FC58F File Offset: 0x000FA98F
		// (set) Token: 0x060039A0 RID: 14752 RVA: 0x000FC597 File Offset: 0x000FA997
		public float visibilityFraction { get; private set; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000FC5A0 File Offset: 0x000FA9A0
		// (set) Token: 0x060039A2 RID: 14754 RVA: 0x000FC5A8 File Offset: 0x000FA9A8
		public float flashFraction { get; private set; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000FC5B1 File Offset: 0x000FA9B1
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000FC5B9 File Offset: 0x000FA9B9
		public float hoverFraction { get; private set; }

		// Token: 0x060039A5 RID: 14757 RVA: 0x000FC5C4 File Offset: 0x000FA9C4
		protected virtual void Init()
		{
			this.onFullyHidden = delegate()
			{
				this.onVisibleChange(false);
				base.gameObject.SetActive(false);
				if (this.autoReturnToPool)
				{
					this.ReturnToPool_Internal();
				}
			};
			this.visibilityAnimator = new TargetAnimator<float>(new Func<float>(this.get_visibilityFraction), delegate(float v)
			{
				this.visibilityFraction = v;
			}, this.stateRoot.rootState, this.fadeInAnim);
			this.hoverAnimator = new TargetAnimator<float>(new Func<float>(this.get_hoverFraction), delegate(float v)
			{
				this.hoverFraction = v;
			}, this.stateRoot.rootState, this.hoverInAnim);
			this.flashAnimator = new TargetAnimator<float>(new Func<float>(this.get_flashFraction), delegate(float v)
			{
				this.flashFraction = v;
			}, this.stateRoot.rootState, this.flashAnim);
			this.visibilityState = new AgentState("Visible", this.stateRoot.rootState, false, false);
			AgentState agentState = this.visibilityState;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				base.gameObject.SetActive(true);
				this.visibilityAnimator.SetTarget(1f, null, null, this.fadeInAnim, 0f, null);
				this.onVisibleChange(true);
			}));
			AgentState agentState2 = this.visibilityState;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				this.visibilityAnimator.SetTarget(0f, null, this.onFullyHidden, this.fadeOutAnim, 0f, null);
			}));
			this.hoverState = new AgentState("hover", this.stateRoot.rootState, false, false);
			AgentState agentState3 = this.hoverState;
			agentState3.OnChange = (Action<bool>)Delegate.Combine(agentState3.OnChange, new Action<bool>(delegate(bool h)
			{
				this.hoverAnimator.SetTarget((!h) ? 0f : 1f, null, null, null, 0f, null);
			}));
			this.navSpotComponents = base.GetComponentsInChildren<TargetMesh.IMeshComponent>(true);
			foreach (TargetMesh.IMeshComponent meshComponent in this.navSpotComponents)
			{
				meshComponent.Init(this);
			}
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000FC761 File Offset: 0x000FAB61
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000FC770 File Offset: 0x000FAB70
		public void Setup(Mesh referenceMesh, Transform referenceTransform)
		{
			using (new ScopedProfiler("SetupTargetNavSpot", null))
			{
				this._mesh.Target = referenceMesh;
				if (referenceTransform)
				{
					base.transform.Set(referenceTransform);
				}
				foreach (TargetMesh.IMeshComponent meshComponent in this.navSpotComponents)
				{
					meshComponent.SetMesh(this.mesh);
				}
				if (!this.mesh)
				{
					base.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000FC818 File Offset: 0x000FAC18
		public void SetVisible(bool visible)
		{
			this.visibilityState.SetActive(visible);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000FC827 File Offset: 0x000FAC27
		public void DoFlash()
		{
			if (this.visibilityState.active)
			{
				this.flashAnimator.SetCurrent(1f);
				this.flashAnimator.SetTarget(0f, null, null, null, 0f, null);
			}
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000FC862 File Offset: 0x000FAC62
		public void SetHover(bool hover)
		{
			this.hoverState.SetActive(hover);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000FC871 File Offset: 0x000FAC71
		protected virtual void SetPool_Internal<T>(LocalPool<T> pool) where T : Component, IPoolable
		{
			this.pool = (pool as LocalPool<TargetMesh>);
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000FC884 File Offset: 0x000FAC84
		protected virtual void ReturnToPool_Internal()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000FC892 File Offset: 0x000FAC92
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.SetPool_Internal<T>(pool);
			this.Init();
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000FC8A1 File Offset: 0x000FACA1
		void IPoolable.OnRemoved()
		{
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000FC8A4 File Offset: 0x000FACA4
		void IPoolable.OnReturned()
		{
			this.SetVisible(false);
			this.SetHover(false);
			this.Setup(null, null);
			float num = 0f;
			this.flashFraction = num;
			num = num;
			this.hoverFraction = num;
			this.visibilityFraction = num;
		}

		// Token: 0x040027B9 RID: 10169
		private WeakReference<Mesh> _mesh = new WeakReference<Mesh>(null);

		// Token: 0x040027BA RID: 10170
		private TargetMesh.IMeshComponent[] navSpotComponents;

		// Token: 0x040027BB RID: 10171
		private LocalPool<TargetMesh> pool;

		// Token: 0x040027BC RID: 10172
		[SerializeField]
		private bool autoReturnToPool = true;

		// Token: 0x040027BD RID: 10173
		[Header("Fade")]
		[SerializeField]
		private LerpTowards fadeInAnim = new LerpTowards(5f, 5f);

		// Token: 0x040027BE RID: 10174
		[SerializeField]
		private LerpTowards fadeOutAnim = new LerpTowards(7f, 3f);

		// Token: 0x040027BF RID: 10175
		private TargetAnimator<float> visibilityAnimator;

		// Token: 0x040027C0 RID: 10176
		[Header("Flash")]
		[SerializeField]
		private LerpTowards flashAnim = new LerpTowards(2f, 0.05f);

		// Token: 0x040027C1 RID: 10177
		private TargetAnimator<float> flashAnimator;

		// Token: 0x040027C2 RID: 10178
		private LerpTowards hoverInAnim = new LerpTowards(8f, 4f);

		// Token: 0x040027C3 RID: 10179
		private LerpTowards hoverOutAnim = new LerpTowards(6f, 1.5f);

		// Token: 0x040027C4 RID: 10180
		private TargetAnimator<float> hoverAnimator;

		// Token: 0x040027C5 RID: 10181
		[Header("hover")]
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040027C6 RID: 10182
		private AgentState visibilityState;

		// Token: 0x040027C7 RID: 10183
		private AgentState hoverState;

		// Token: 0x040027C8 RID: 10184
		public Action<bool> onVisibleChange = delegate(bool A_0)
		{
		};

		// Token: 0x040027CC RID: 10188
		private Action onFullyHidden;

		// Token: 0x0200089E RID: 2206
		public interface IMeshComponent
		{
			// Token: 0x060039B8 RID: 14776
			void Init(TargetMesh owner);

			// Token: 0x060039B9 RID: 14777
			void SetMesh(Mesh mesh);
		}
	}
}
