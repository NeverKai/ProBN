using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000816 RID: 2070
	public class AnimatedState : AgentState
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x000E97A4 File Offset: 0x000E7BA4
		public AnimatedState(string name, AgentState parentState, bool active, bool exclusive) : this(name, parentState, active, exclusive, LerpTowards.standard)
		{
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000E97B6 File Offset: 0x000E7BB6
		public AnimatedState(string name, AgentState parentState, bool active, bool exclusive, ITargetAnimFuncs<float> targetAnimFuncs) : this(name, parentState, parentState, active, exclusive, targetAnimFuncs)
		{
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000E97C8 File Offset: 0x000E7BC8
		public AnimatedState(string name, AgentState parentState, AgentState updateState, bool active, bool exclusive, ITargetAnimFuncs<float> targetAnimFuncs) : base(name, parentState, active, exclusive)
		{
			AnimatedState $this = this;
			this.value = (float)((!active) ? 0 : 1);
			this.anim = new TargetAnimator<float>(name, () => $this.value, delegate(float x)
			{
				$this.value = x;
			}, updateState, targetAnimFuncs);
			this.updateState = updateState;
			this.OnActivate = (Action)Delegate.Combine(this.OnActivate, new Action(delegate()
			{
				$this.onActivity(true);
				$this.anim.SetTarget(1f, null, null, null, 0f, null);
			}));
			Action onDeactivate = delegate()
			{
				$this.onActivity(false);
			};
			this.OnDeactivate = (Action)Delegate.Combine(this.OnDeactivate, new Action(delegate()
			{
				$this.anim.SetTarget(0f, null, onDeactivate, null, 0f, null);
			}));
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000E98AA File Offset: 0x000E7CAA
		// (set) Token: 0x06003629 RID: 13865 RVA: 0x000E98B2 File Offset: 0x000E7CB2
		public TargetAnimator<float> anim { get; private set; }

		// Token: 0x0600362A RID: 13866 RVA: 0x000E98BB File Offset: 0x000E7CBB
		public void AddOnActivity(Action<bool> onActivity)
		{
			onActivity(base.active);
			this.onActivity = (Action<bool>)Delegate.Combine(this.onActivity, onActivity);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000E98E0 File Offset: 0x000E7CE0
		public void Subscribe(Animator animator, string parameter)
		{
			int id = Animator.StringToHash(parameter);
			TargetAnimator<float> anim = this.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float x)
			{
				animator.SetFloat(id, x);
			}));
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000E9930 File Offset: 0x000E7D30
		public void Subscribe(CanvasGroup canvasGroup)
		{
			this.Subscribe(new Action<bool>(canvasGroup.gameObject.SetActive), delegate(float x)
			{
				canvasGroup.alpha = x;
			});
			base.Subscribe(delegate(bool x)
			{
				canvasGroup.blocksRaycasts = x;
			});
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000E9984 File Offset: 0x000E7D84
		public void InverseSubscribe(CanvasGroup canvasGroup)
		{
			this.Subscribe(delegate(bool x)
			{
				canvasGroup.gameObject.SetActive(!x);
			}, delegate(float x)
			{
				canvasGroup.alpha = 1f - x;
			});
			base.Subscribe(delegate(bool x)
			{
				canvasGroup.blocksRaycasts = !x;
			});
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000E99D0 File Offset: 0x000E7DD0
		public void InverseSubscribe(Graphic graphic)
		{
			this.Subscribe(delegate(bool x)
			{
				graphic.gameObject.SetActive(!x);
			}, delegate(float x)
			{
				graphic.color = graphic.color.SetA(1f - x);
			});
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000E9A08 File Offset: 0x000E7E08
		public void Subscribe(Graphic graphic)
		{
			this.Subscribe(delegate(bool x)
			{
				graphic.gameObject.SetActive(x);
			}, delegate(float x)
			{
				graphic.color = graphic.color.SetA(x);
			});
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000E9A40 File Offset: 0x000E7E40
		public void Subscribe(Action<bool> onActivity, Action<float> setFunc)
		{
			if (onActivity != null)
			{
				onActivity(base.active);
				this.onActivity = (Action<bool>)Delegate.Combine(this.onActivity, onActivity);
			}
			if (setFunc != null)
			{
				setFunc(this.value);
				TargetAnimator<float> anim = this.anim;
				anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, setFunc);
			}
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000E9AA4 File Offset: 0x000E7EA4
		public void ForceToTarget()
		{
			if (this.anim.state.active)
			{
				this.anim.ForceToTarget();
			}
			else
			{
				this.anim.SetCurrent((float)((!base.active) ? 0 : 1));
				this.onActivity(base.active);
			}
		}

		// Token: 0x040024C7 RID: 9415
		public float value;

		// Token: 0x040024C9 RID: 9417
		public Action<bool> onActivity = delegate(bool A_0)
		{
		};

		// Token: 0x040024CA RID: 9418
		public readonly AgentState updateState;
	}
}
