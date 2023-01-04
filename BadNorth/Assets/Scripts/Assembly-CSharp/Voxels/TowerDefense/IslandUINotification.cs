using System;
using I2.Loc;
using RTM.Pools;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000558 RID: 1368
	public class IslandUINotification : MonoBehaviour, IPoolable
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0006E23B File Offset: 0x0006C63B
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x0006E243 File Offset: 0x0006C643
		public int id { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x0006E24C File Offset: 0x0006C64C
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x0006E254 File Offset: 0x0006C654
		public IslandUINotification.Priority priority { get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0006E25D File Offset: 0x0006C65D
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x0006E265 File Offset: 0x0006C665
		private float flashIntensity
		{
			get
			{
				return this._flashIntensity;
			}
			set
			{
				this._flashIntensity = value;
				this.polygon.color = Color.Lerp(this.baseColor, this.flashColor, value);
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0006E28C File Offset: 0x0006C68C
		public IslandUINotification Setup(int id, string locTerm, IslandUINotification.Priority priority, Sprite iconLeft, Sprite iconRight, bool distanceField, float duration)
		{
			this.id = id;
			this.priority = priority;
			this.localizeTarget.Term = locTerm;
			this.open.SetActive(true);
			this.iconLeftVisibility.SetActive(false);
			this.iconRightVisibility.SetActive(false);
			this.distanceFieldLeftVisibility.SetActive(false);
			this.distanceFieldRightVisibility.SetActive(false);
			if (distanceField)
			{
				if (iconLeft)
				{
					this.distanceFieldLeftVisibility.SetActive(true);
					Image componentInChildren = this.distanceFieldLeftVisibility.GetComponentInChildren<Image>();
					componentInChildren.sprite = iconLeft;
					Vector2 sizeDelta = componentInChildren.rectTransform.sizeDelta;
					sizeDelta.x = sizeDelta.y * (iconLeft.rect.width / iconLeft.rect.height);
					componentInChildren.rectTransform.sizeDelta = sizeDelta;
					this.distanceFieldLeftVisibility.GetComponent<LayoutElement>().preferredWidth = sizeDelta.x;
				}
				if (iconRight)
				{
					this.distanceFieldLeftVisibility.SetActive(true);
					Image componentInChildren2 = this.distanceFieldLeftVisibility.GetComponentInChildren<Image>();
					componentInChildren2.sprite = iconRight;
					Vector2 sizeDelta2 = componentInChildren2.rectTransform.sizeDelta;
					sizeDelta2.x = sizeDelta2.y * (iconRight.rect.width / iconRight.rect.height);
					componentInChildren2.rectTransform.sizeDelta = sizeDelta2;
					this.distanceFieldLeftVisibility.GetComponent<LayoutElement>().preferredWidth = sizeDelta2.x;
				}
			}
			else
			{
				if (iconLeft)
				{
					this.iconLeftVisibility.SetActive(true);
					Image componentInChildren3 = this.iconLeftVisibility.GetComponentInChildren<Image>();
					componentInChildren3.sprite = iconLeft;
					Vector2 sizeDelta3 = componentInChildren3.rectTransform.sizeDelta;
					sizeDelta3.x = sizeDelta3.y * (iconLeft.rect.width / iconLeft.rect.height);
					componentInChildren3.rectTransform.sizeDelta = sizeDelta3;
					this.iconLeftVisibility.GetComponent<LayoutElement>().preferredWidth = sizeDelta3.x;
				}
				if (iconRight)
				{
					this.iconRightVisibility.SetActive(true);
					Image componentInChildren4 = this.iconRightVisibility.GetComponentInChildren<Image>();
					componentInChildren4.sprite = iconRight;
					Vector2 sizeDelta4 = componentInChildren4.rectTransform.sizeDelta;
					sizeDelta4.x = sizeDelta4.y * (iconRight.rect.width / iconRight.rect.height);
					componentInChildren4.rectTransform.sizeDelta = sizeDelta4;
					this.iconRightVisibility.GetComponent<LayoutElement>().preferredWidth = sizeDelta4.x;
				}
			}
			this.expiryTime = ((duration <= 0f) ? float.MaxValue : (this.expiryTime = Time.unscaledTime + duration));
			return this;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0006E575 File Offset: 0x0006C975
		public IslandUINotification SetLocParam(string paramName, string paramValue)
		{
			this.localizeParamsManager.SetParameterValue(paramName, paramValue, true);
			return this;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0006E586 File Offset: 0x0006C986
		public string messageTerm
		{
			get
			{
				return this.localizeTarget.Term;
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0006E593 File Offset: 0x0006C993
		private void ReturnToPool()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0006E5A1 File Offset: 0x0006C9A1
		private void Update()
		{
			this.stateRoot.rootState.Update();
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0006E5B3 File Offset: 0x0006C9B3
		public void Close()
		{
			this.open.SetActive(false);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0006E5C4 File Offset: 0x0006C9C4
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<IslandUINotification>);
			this.rt = (RectTransform)base.transform;
			this.open = new AgentState("Open", this.stateRoot.rootState, false, true);
			LerpTowards openAnim = new LerpTowards(8f, 1f);
			LerpTowards closeAnim = new LerpTowards(8f, 1f);
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			this.anim = new TargetAnimator(() => canvasGroup.alpha, delegate(float x)
			{
				canvasGroup.alpha = x;
			}, this.stateRoot.rootState, openAnim);
			TargetAnimator targetAnimator = this.anim;
			targetAnimator.setFunc = (Action<float>)Delegate.Combine(targetAnimator.setFunc, new Action<float>(delegate(float x)
			{
				this.rt.pivot = this.rt.pivot.SetY(Mathf.Lerp(0.9f, 1f, x));
			}));
			AgentState agentState = this.open;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.anim.SetCurrent(0f);
				this.anim.SetTarget(1f, null, null, openAnim, 0f, null);
				FabricWrapper.PostEvent(this.openAudioId.name);
				this.gameObject.SetActive(true);
			}));
			this.open.OnUpdate += delegate()
			{
				if (Time.unscaledTime > this.expiryTime)
				{
					this.open.SetActive(false);
				}
			};
			AgentState agentState2 = this.open;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				this.anim.SetTarget(0f, null, new Action(this.ReturnToPool), closeAnim, 0f, null);
				FabricWrapper.PostEvent(this.closeAudioId.name);
			}));
			LerpTowards targetAnimFuncs = new LerpTowards(4f, 0.15f);
			this.flashAnim = new TargetAnimator(new Func<float>(this.get_flashIntensity), delegate(float v)
			{
				this.flashIntensity = v;
			}, this.stateRoot.rootState, targetAnimFuncs);
			this.polygon = base.GetComponentInChildren<PolygonMask>();
			this.baseColor = this.polygon.color;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0006E76F File Offset: 0x0006CB6F
		public void Flash()
		{
			this.Flash(IslandUINotification.defaultFlashColor);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0006E77C File Offset: 0x0006CB7C
		public void Flash(Color color)
		{
			this.flashColor = color;
			this.flashAnim.SetCurrentAndActivate(1f);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0006E795 File Offset: 0x0006CB95
		void IPoolable.OnRemoved()
		{
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0006E798 File Offset: 0x0006CB98
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
			this.open.SetActive(false);
			this.anim.SetTarget(0f, null, null, null, 0f, null);
			this.anim.ForceToTarget();
			this.flashAnim.SetTarget(0f, null, null, null, 0f, null);
			this.flashAnim.ForceToTarget();
		}

		// Token: 0x0400162D RID: 5677
		public static readonly Func<IslandUINotification, IslandUINotification.Priority> order = (IslandUINotification x) => x.priority;

		// Token: 0x0400162E RID: 5678
		[SerializeField]
		private Localize localizeTarget;

		// Token: 0x0400162F RID: 5679
		[SerializeField]
		private LocalizationParamsManager localizeParamsManager;

		// Token: 0x04001630 RID: 5680
		[SerializeField]
		private GameObject iconLeftVisibility;

		// Token: 0x04001631 RID: 5681
		[SerializeField]
		private GameObject iconRightVisibility;

		// Token: 0x04001632 RID: 5682
		[SerializeField]
		private GameObject distanceFieldRightVisibility;

		// Token: 0x04001633 RID: 5683
		[SerializeField]
		private GameObject distanceFieldLeftVisibility;

		// Token: 0x04001634 RID: 5684
		[Header("Audio")]
		private FabricEventReference openAudioId = "UI/InGame/NotificationOn";

		// Token: 0x04001635 RID: 5685
		private FabricEventReference closeAudioId = "UI/InGame/NotificationOff";

		// Token: 0x04001636 RID: 5686
		private RectTransform rt;

		// Token: 0x04001637 RID: 5687
		private LocalPool<IslandUINotification> pool;

		// Token: 0x0400163A RID: 5690
		private float expiryTime;

		// Token: 0x0400163B RID: 5691
		private TargetAnimator anim;

		// Token: 0x0400163C RID: 5692
		[SerializeField]
		private AgentStateRoot stateRoot;

		// Token: 0x0400163D RID: 5693
		private AgentState open;

		// Token: 0x0400163E RID: 5694
		private TargetAnimator flashAnim;

		// Token: 0x0400163F RID: 5695
		private static Color defaultFlashColor = Color.Lerp(Color.black, Color.white, 0.3f);

		// Token: 0x04001640 RID: 5696
		private Color flashColor;

		// Token: 0x04001641 RID: 5697
		private Color baseColor;

		// Token: 0x04001642 RID: 5698
		private float _flashIntensity;

		// Token: 0x04001643 RID: 5699
		private PolygonMask polygon;

		// Token: 0x02000559 RID: 1369
		public enum Priority
		{
			// Token: 0x04001645 RID: 5701
			None,
			// Token: 0x04001646 RID: 5702
			WaveIncoming,
			// Token: 0x04001647 RID: 5703
			Tutorial,
			// Token: 0x04001648 RID: 5704
			ActiveAbility
		}
	}
}
