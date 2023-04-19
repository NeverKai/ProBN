using System;
using I2.Loc;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200083E RID: 2110
	public abstract class ActiveAbility : UpgradeComponent
	{
		// Token: 0x060036E4 RID: 14052
		public abstract void OnConfirmed();

		// Token: 0x060036E5 RID: 14053 RVA: 0x000EBB70 File Offset: 0x000E9F70
		public virtual bool CanDisplay()
		{
			return true;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000EBB73 File Offset: 0x000E9F73
		public virtual void UpdateSquadSelected()
		{
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000EBB75 File Offset: 0x000E9F75
		public virtual bool WillHandleConfirmation()
		{
			return false;
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000EBB78 File Offset: 0x000E9F78
		public virtual bool blocksMove
		{
			get
			{
				return this._blocksMove;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000EBB80 File Offset: 0x000E9F80
		public bool idle
		{
			get
			{
				return !this.active.active && !this.cooldown.active;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000EBBA3 File Offset: 0x000E9FA3
		public virtual CursorManager.IPointerCursor pointerCursor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x000EBBA6 File Offset: 0x000E9FA6
		public virtual CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000EBBA9 File Offset: 0x000E9FA9
		public virtual void SetGameplayManager(IslandGameplayManager manager)
		{
			this.islandGameplayManager.Target = manager;
			this.wsCursorIcons.Target = manager.worldSpaceCursorIconPool;
			this.notificationManager.Target = manager.notificationManager;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000EBBD9 File Offset: 0x000E9FD9
		public virtual void Cancel()
		{
			this._timeWhenEffectWearsOff = Time.time;
			this.CancelInvoke(new Action(this.OnEffectEnded));
			this.OnEffectEnded();
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000EBBFF File Offset: 0x000E9FFF
		protected virtual void OnEffectEnded()
		{
			this.OnEnded();
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000EBC07 File Offset: 0x000EA007
		public string abilityNameTerm
		{
			get
			{
				return this._abilityNameTerm;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000EBC0F File Offset: 0x000EA00F
		public Color buttonColor
		{
			get
			{
				return this._buttonColor;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000EBC17 File Offset: 0x000EA017
		public Color cooldownCompleteColor
		{
			get
			{
				return this._cooldownCompleteColor;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000EBC1F File Offset: 0x000EA01F
		public Sprite iconSprite
		{
			get
			{
				return this._iconSprite;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000EBC27 File Offset: 0x000EA027
		public float effectDuration
		{
			get
			{
				return (this._effectDuration < 0f) ? float.MaxValue : this._effectDuration;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000EBC49 File Offset: 0x000EA049
		public float timeWhenEffectWearsOff
		{
			get
			{
				return this._timeWhenEffectWearsOff;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000EBC51 File Offset: 0x000EA051
		public bool isCancelable
		{
			get
			{
				return this._isCancelable && this.isActive;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000EBC67 File Offset: 0x000EA067
		public int numberCharges
		{
			get
			{
				return this._chargesPerIsland;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000EBC6F File Offset: 0x000EA06F
		public bool hasLimitedCharges
		{
			get
			{
				return this._chargesPerIsland > 0;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000EBC7A File Offset: 0x000EA07A
		public float cooldownPeriod
		{
			get
			{
				return this._cooldownPeriod;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000EBC82 File Offset: 0x000EA082
		public bool hasCooldown
		{
			get
			{
				return this._cooldownPeriod > 0f;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000EBC91 File Offset: 0x000EA091
		public bool isCoolingDown
		{
			get
			{
				return this.cooldown.active;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000EBC9E File Offset: 0x000EA09E
		public float cooldownRemaining
		{
			get
			{
				return Mathf.Clamp(this.cooldownPeriod - this.cooldown.timeSinceActivation, 0f, this.cooldownPeriod);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000EBCC2 File Offset: 0x000EA0C2
		public float cooldownRatioRemaining
		{
			get
			{
				return (this._cooldownPeriod <= 0f) ? 0f : Mathf.Clamp01(this.cooldownRemaining / this.cooldownPeriod);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000EBCF0 File Offset: 0x000EA0F0
		public int chargesRemaining
		{
			get
			{
				return this._chargesRemaining;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000EBCF8 File Offset: 0x000EA0F8
		public bool isOutOfCharges
		{
			get
			{
				return this.hasLimitedCharges && this.chargesRemaining <= 0;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000EBD14 File Offset: 0x000EA114
		public bool isActive
		{
			get
			{
				return this.active.active;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000EBD21 File Offset: 0x000EA121
		public bool isBlockedByOther
		{
			get
			{
				return this.IsBlocked() && !this.squadUpgradeManager.CanActivateAbility();
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06003701 RID: 14081 RVA: 0x000EBD40 File Offset: 0x000EA140
		// (remove) Token: 0x06003702 RID: 14082 RVA: 0x000EBD78 File Offset: 0x000EA178
		
		public event Action onActivated = delegate()
		{
		};

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000EBDAE File Offset: 0x000EA1AE
		public bool isBanned
		{
			get
			{
				return this.bannedTooltip != null;
			}
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000EBDBC File Offset: 0x000EA1BC
		public void AddCharges(int numToAdd)
		{
			this._chargesRemaining = Mathf.Max(0, this._chargesRemaining + numToAdd);
			this._chargesPerIsland = Mathf.Min(this._chargesPerIsland, this._chargesRemaining);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000EBDE9 File Offset: 0x000EA1E9
		public void modifyCooldown(float multiplier)
		{
			this._cooldownPeriod *= multiplier;
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000EBDF9 File Offset: 0x000EA1F9
		public void BanAbility(string tooltipTerm, string paramName = null, string paramValue = null)
		{
			this.bannedTooltip = tooltipTerm;
			this.bannedParamName = paramName;
			this.bannedParamValue = paramValue;
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000EBE10 File Offset: 0x000EA210
		public virtual bool IsBlocked()
		{
			return this.isBanned || this.isCoolingDown || this.isActive || this.isOutOfCharges || !this.squadUpgradeManager.CanActivateAbility();
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000EBE4F File Offset: 0x000EA24F
		public virtual bool IsAvailable()
		{
			return !this.IsBlocked();
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000EBE5C File Offset: 0x000EA25C
		protected override void DoSquadSpawnAction_Implementation()
		{
			this.squadUpgradeManager = base.squad.upgradeManager;
			this._chargesRemaining = this._chargesPerIsland;
			base.squad.pather.onPathTargetChanged += this.OnPathTargetChanged;
			this.focus = new AgentState(base.nameCache + "_Focus", base.squad.selectedState, false, false);
			this.abilityState = new AgentState(base.nameCache, base.squad.rootState, true, false);
			this.active = new AgentState("Active", this.abilityState, false, false);
			this.cooldown = new AgentState("Cooldown", this.abilityState, false, false);
			AgentState agentState = this.active;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				if (this.hasCooldown && !this.isOutOfCharges)
				{
					this.cooldown.SetActive(true);
				}
			}));
			this.cooldown.OnDebugString.Add(() => this.cooldownRemaining.ToString("F2"));
			this.cooldown.OnUpdate += delegate()
			{
				if (this.cooldown.timeSinceActivation >= this.cooldownPeriod)
				{
					this.cooldown.SetActive(false);
					ScriptableObjectSingleton<PrefabManager>.instance.abilityFloatingIcon.GetInstance<FloatingIcon>().Setup(this.iconSprite, base.squad.heroAgent.transform);
					if (!string.IsNullOrEmpty(this.cooldownCompleteAudioID))
					{
						FabricWrapper.PostEvent(this.cooldownCompleteAudioID, base.squad.standard.gameObject);
					}
				}
			};
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000EBF74 File Offset: 0x000EA374
		protected void OnActivated()
		{
			if (this._cancelMoveOnUse)
			{
				base.squad.navSpotOccupant.SetNavSpot(base.squad.GetHeroNavSpot(), true);
			}
			if (this.hasLimitedCharges)
			{
				this._chargesRemaining--;
			}
			this.active.SetActive(true);
			this._timeWhenEffectWearsOff = Time.time + this.effectDuration;
			if (this.effectDuration > 0f)
			{
				this.CancelInvoke(new Action(this.OnEffectEnded));
				this.Invoke(new Action(this.OnEffectEnded), this.effectDuration);
			}
			FabricWrapper.PostEvent(this.actionConfirmAudioID);
			this.squadUpgradeManager.OnAbilityActivated(this);
			Singleton<SquadSelector>.instance.SelectSquad(null, false);
			this.onActivated();
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000EC04C File Offset: 0x000EA44C
		protected void OnEnded()
		{
			this.active.SetActive(false);
			this.squadUpgradeManager.OnAbilityEnded(this);
			this.islandGameplayManager.Target.navigatorNavSpotPool.SetDirty();
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000EC07C File Offset: 0x000EA47C
		private void OnPathTargetChanged(IPathTarget p)
		{
			if (this.isActive && this._cancelEffectOnMove)
			{
				this.Cancel();
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000EC09C File Offset: 0x000EA49C
		public void UpdateNotification(bool canDisplay)
		{
			if (canDisplay)
			{
				this.UpdateNotification();
			}
			else if (this.currentNotification.Target)
			{
				this.currentNotification.Target.Close();
				this.currentNotification.Target = null;
			}
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000EC0EC File Offset: 0x000EA4EC
		protected virtual void UpdateNotification()
		{
			string text2;
			string text3;
			string text = this.GetNotificationTerm(out text2, out text3);
			text = ((!string.IsNullOrEmpty(text)) ? text : this._tooltipDescription);
			if (this.currentNotification.Target)
			{
				if (this.currentNotification.Target.messageTerm != text)
				{
					this.currentNotification.Target.Close();
					this.currentNotification.Target = null;
				}
			}
			else if (!string.IsNullOrEmpty(text))
			{
				this.currentNotification.Target = this.notificationManager.Target.PostMessage(text, IslandUINotification.Priority.ActiveAbility, null, null, false, 0f);
				if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
				{
					this.currentNotification.Target.SetLocParam(text2, text3);
				}
			}
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000EC1C6 File Offset: 0x000EA5C6
		protected string GetBannedAbilityTerm(out string paramName, out string paramValue)
		{
			paramName = this.bannedParamName;
			paramValue = this.bannedParamValue;
			return this.bannedTooltip;
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000EC1E0 File Offset: 0x000EA5E0
		protected virtual string GetNotificationTerm(out string paramName, out string paramValue)
		{
			string text;
			paramValue = (text = null);
			paramName = text;
			if (this.isBanned)
			{
				return this.GetBannedAbilityTerm(out paramName, out paramValue);
			}
			if (this.isOutOfCharges)
			{
				return "UPGRADES/COMMON/TOOLTIPS/ZERO_CHARGE";
			}
			if (this.isCoolingDown)
			{
				return "UPGRADES/COMMON/TOOLTIPS/COOLDOWN";
			}
			if (this.isBlockedByOther)
			{
				return "UPGRADES/COMMON/TOOLTIPS/ABILITY_IN_USE";
			}
			return string.Empty;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000EC241 File Offset: 0x000EA641
		public override void LevelEnded()
		{
			base.LevelEnded();
			this.cooldown.SetActive(false);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000EC256 File Offset: 0x000EA656
		private void OnDestroy()
		{
			this.focus = null;
			this.abilityState = null;
			this.active = null;
			this.cooldown = null;
			this.squadUpgradeManager = null;
			this.onActivated = null;
		}

		// Token: 0x0400254A RID: 9546
		private const string cooldownToolTipText = "UPGRADES/COMMON/TOOLTIPS/COOLDOWN";

		// Token: 0x0400254B RID: 9547
		private const string defaultConfirmToolTipText = "UPGRADES/COMMON/TOOLTIPS/CONFIRM";

		// Token: 0x0400254C RID: 9548
		private const string cancelToolTipText = "UPGRADES/COMMON/TOOLTIPS/CANCEL";

		// Token: 0x0400254D RID: 9549
		private const string outOfChargesToolTipText = "UPGRADES/COMMON/TOOLTIPS/ZERO_CHARGE";

		// Token: 0x0400254E RID: 9550
		private const string abilityInUseToolTipText = "UPGRADES/COMMON/TOOLTIPS/ABILITY_IN_USE";

		// Token: 0x0400254F RID: 9551
		private RTM.Utilities.WeakReference<IslandGameplayManager> islandGameplayManager = new RTM.Utilities.WeakReference<IslandGameplayManager>(null);

		// Token: 0x04002550 RID: 9552
		private RTM.Utilities.WeakReference<IslandUINotificationManager> notificationManager = new RTM.Utilities.WeakReference<IslandUINotificationManager>(null);

		// Token: 0x04002551 RID: 9553
		private RTM.Utilities.WeakReference<IslandUINotification> currentNotification = new RTM.Utilities.WeakReference<IslandUINotification>(null);

		// Token: 0x04002552 RID: 9554
		public RTM.Utilities.WeakReference<WorldSpaceCursorIconPool> wsCursorIcons = new RTM.Utilities.WeakReference<WorldSpaceCursorIconPool>(null);

		// Token: 0x04002553 RID: 9555
		public AgentState focus;

		// Token: 0x04002554 RID: 9556
		public AgentState abilityState;

		// Token: 0x04002555 RID: 9557
		public AgentState active;

		// Token: 0x04002556 RID: 9558
		public AgentState cooldown;

		// Token: 0x04002557 RID: 9559
		[Header("Display Text")]
		[SerializeField]
		[TermsPopup("")]
		private string _abilityNameTerm = string.Empty;

		// Token: 0x04002558 RID: 9560
		[SerializeField]
		[TermsPopup("")]
		private string _tooltipDescription = string.Empty;

		// Token: 0x04002559 RID: 9561
		[Header("UI Styling")]
		[SerializeField]
		private Color _buttonColor = new Color(154f, 35f, 35f);

		// Token: 0x0400255A RID: 9562
		[SerializeField]
		private Color _cooldownCompleteColor = new Color(200f, 120f, 120f);

		// Token: 0x0400255B RID: 9563
		[SerializeField]
		[SpritePreview]
		private Sprite _iconSprite;

		// Token: 0x0400255C RID: 9564
		[Header("Effect Duration")]
		[SerializeField]
		private float _effectDuration;

		// Token: 0x0400255D RID: 9565
		private float _timeWhenEffectWearsOff = float.MinValue;

		// Token: 0x0400255E RID: 9566
		[SerializeField]
		private bool _isCancelable;

		// Token: 0x0400255F RID: 9567
		[Header("Usage")]
		[SerializeField]
		[FormerlySerializedAs("_chargesPerLevel")]
		[ConsoleCommand("charges")]
		protected int _chargesPerIsland;

		// Token: 0x04002560 RID: 9568
		[SerializeField]
		[ConsoleCommand("cooldown")]
		private float _cooldownPeriod = 30f;

		// Token: 0x04002561 RID: 9569
		[Header("Movement")]
		[SerializeField]
		private bool _cancelMoveOnUse;

		// Token: 0x04002562 RID: 9570
		[SerializeField]
		private bool _cancelEffectOnMove;

		// Token: 0x04002563 RID: 9571
		[SerializeField]
		private bool _blocksMove;

		// Token: 0x04002564 RID: 9572
		[Header("Audio")]
		public string actionConfirmAudioID = "UI/Menu/SelectAbility";

		// Token: 0x04002565 RID: 9573
		[SerializeField]
		private string cooldownCompleteAudioID = string.Empty;

		// Token: 0x04002566 RID: 9574
		protected int _chargesRemaining;

		// Token: 0x04002567 RID: 9575
		protected SquadUpgradeManager squadUpgradeManager;

		// Token: 0x04002569 RID: 9577
		private string bannedTooltip;

		// Token: 0x0400256A RID: 9578
		private string bannedParamName;

		// Token: 0x0400256B RID: 9579
		private string bannedParamValue;
	}
}
