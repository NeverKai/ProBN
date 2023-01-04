using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000815 RID: 2069
	public class TargetAnimator<T>
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000E9235 File Offset: 0x000E7635
		public TargetAnimator(Func<T> getFunc, Action<T> setFunc, AgentState updateState, ITargetAnimFuncs<T> targetAnimFuncs) : this("TargetAnimator", getFunc, setFunc, updateState, targetAnimFuncs)
		{
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000E9248 File Offset: 0x000E7648
		public TargetAnimator(string name, Func<T> getFunc, Action<T> setFunc, AgentState updateState, ITargetAnimFuncs<T> targetAnimFuncs)
		{
			TargetAnimator<T> $this = this;
			this.getFunc = getFunc;
			this.setFunc = (Action<T>)Delegate.Combine(this.setFunc, setFunc);
			this.state = new AgentState(name, updateState, false, false);
			AgentState agentState = this.state;
			agentState.OnDeactivate = (Action)Delegate.Combine(agentState.OnDeactivate, new Action(delegate()
			{
				$this.setFunc($this.target);
			}));
			AgentState agentState2 = this.state;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				$this.animFuncs.OnActivate(getFunc(), $this.target);
			}));
			this.state.OnUpdate += delegate()
			{
				if (Time.unscaledTime < $this.delayTime)
				{
					return;
				}
				if ($this.onStart != null)
				{
					$this.onStart();
					$this.onStart = null;
				}
				T t = getFunc();
				float num = Mathf.Min(Time.unscaledDeltaTime, 0.05f);
				for (float num2 = 0f; num2 < num; num2 += 0.033333335f)
				{
					t = $this.animFuncs.UpdateCurrent(t, $this.target, Mathf.Min(num - num2, 0.033333335f));
				}
				if (!$this.hasBeenTriggered && $this.animFuncs.ShouldTrigger(t, $this.target))
				{
					if ($this.onTrigger != null)
					{
						$this.onTrigger();
					}
					$this.hasBeenTriggered = true;
				}
				if ($this.hasBeenTriggered && $this.animFuncs.IsDone(t, $this.target))
				{
					$this.setFunc($this.target);
					if ($this.onDone != null)
					{
						$this.onDone();
					}
					$this.state.SetActive(false);
				}
				else
				{
					$this.setFunc(t);
				}
			};
			this.state.OnDebugString.Add(() => string.Format("Current {0}, Target {1}, HasTriggered {2}", getFunc(), $this.target, $this.hasBeenTriggered));
			this.animFuncs = targetAnimFuncs;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000E934E File Offset: 0x000E774E
		public T current
		{
			get
			{
				return this.getFunc();
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000E935B File Offset: 0x000E775B
		// (set) Token: 0x06003618 RID: 13848 RVA: 0x000E9363 File Offset: 0x000E7763
		public T target
		{
			get
			{
				return this._target;
			}
			set
			{
				this.SetTarget(value, null, null, null, 0f, null);
			}
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000E9375 File Offset: 0x000E7775
		public void Subscribe(Action<T> setFunc)
		{
			setFunc(this.current);
			this.setFunc = (Action<T>)Delegate.Combine(this.setFunc, setFunc);
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000E939A File Offset: 0x000E779A
		public void ForceActive()
		{
			this.state.SetActive(true);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000E93A9 File Offset: 0x000E77A9
		public void SetTargetOrCurrent(T value)
		{
			if (this.state.active)
			{
				this.SetTarget(value, null, null, null, 0f, null);
			}
			else
			{
				this._target = value;
				this.setFunc(value);
			}
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000E93E3 File Offset: 0x000E77E3
		public bool SetTargetIfDifferent(T target, Action onTrigger = null, Action onDone = null, ITargetAnimFuncs<T> animFuncs = null, float delay = 0f, Action onStart = null)
		{
			if (target.Equals(this.current))
			{
				return false;
			}
			this.SetTarget(target, onTrigger, onDone, animFuncs, delay, onStart);
			return true;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000E9414 File Offset: 0x000E7814
		public void SetTarget(T target, Action onTrigger = null, Action onDone = null, ITargetAnimFuncs<T> animFuncs = null, float delay = 0f, Action onStart = null)
		{
			this.onTrigger = onTrigger;
			this.onDone = onDone;
			this.onStart = onStart;
			this._target = target;
			this.delayTime = Time.unscaledTime + delay;
			if (animFuncs != null)
			{
				this.animFuncs = animFuncs;
			}
			if (this.state.parent.active)
			{
				this.state.SetActive(true);
			}
			this.hasBeenTriggered = false;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000E9489 File Offset: 0x000E7889
		public void Update()
		{
			this.SetCurrent(this.current);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000E9497 File Offset: 0x000E7897
		public void SetCurrent(T value)
		{
			this.setFunc(value);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000E94A5 File Offset: 0x000E78A5
		public void SetTargetAndCurrent(T value)
		{
			this.setFunc(value);
			this.target = value;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000E94BA File Offset: 0x000E78BA
		public void SetCurrentAndActivate(T value)
		{
			this.setFunc(value);
			this.state.SetActive(true);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000E94D8 File Offset: 0x000E78D8
		public void ForceToTarget()
		{
			this.setFunc(this.target);
			if (!this.hasBeenTriggered)
			{
				if (this.onTrigger != null)
				{
					this.onTrigger();
				}
				this.hasBeenTriggered = true;
			}
			if (this.state.active)
			{
				if (this.onDone != null)
				{
					this.onDone();
				}
				this.state.SetActive(false);
			}
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000E9551 File Offset: 0x000E7951
		public void SetAnimFuncs(ITargetAnimFuncs<T> animFuncs)
		{
			this.animFuncs = animFuncs;
		}

		// Token: 0x040024BB RID: 9403
		public Func<T> getFunc;

		// Token: 0x040024BC RID: 9404
		public Action<T> setFunc = delegate(T A_0)
		{
		};

		// Token: 0x040024BD RID: 9405
		public Action onTrigger;

		// Token: 0x040024BE RID: 9406
		public Action onDone;

		// Token: 0x040024BF RID: 9407
		public Action onStart;

		// Token: 0x040024C0 RID: 9408
		private ITargetAnimFuncs<T> animFuncs;

		// Token: 0x040024C1 RID: 9409
		private bool hasBeenTriggered;

		// Token: 0x040024C2 RID: 9410
		public readonly AgentState state;

		// Token: 0x040024C3 RID: 9411
		private T _target;

		// Token: 0x040024C4 RID: 9412
		private const float maxDt = 0.033333335f;

		// Token: 0x040024C5 RID: 9413
		private float delayTime;
	}
}
