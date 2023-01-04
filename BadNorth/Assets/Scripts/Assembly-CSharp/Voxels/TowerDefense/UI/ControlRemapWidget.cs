using System;
using System.Diagnostics;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008C6 RID: 2246
	internal class ControlRemapWidget : Widget
	{
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06003B51 RID: 15185 RVA: 0x00107CB0 File Offset: 0x001060B0
		// (remove) Token: 0x06003B52 RID: 15186 RVA: 0x00107CE8 File Offset: 0x001060E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onStartListening = delegate()
		{
		};

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06003B53 RID: 15187 RVA: 0x00107D20 File Offset: 0x00106120
		// (remove) Token: 0x06003B54 RID: 15188 RVA: 0x00107D58 File Offset: 0x00106158
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onStopListening = delegate()
		{
		};

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06003B55 RID: 15189 RVA: 0x00107D90 File Offset: 0x00106190
		// (remove) Token: 0x06003B56 RID: 15190 RVA: 0x00107DC8 File Offset: 0x001061C8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onMappingChanged = delegate()
		{
		};

		// Token: 0x06003B57 RID: 15191 RVA: 0x00107E00 File Offset: 0x00106200
		public ControlRemapWidget Initialize(string labelLoc, int labelNum, Player player, ActionElementMap aem)
		{
			base.Initialize(labelLoc);
			LocalizationParamsManager component = this.localize.GetComponent<LocalizationParamsManager>();
			component.SetParameterValue("NUM", IntStringCache.GetClean(labelNum), false);
			this.player = player;
			this.aem = aem;
			UIClickable uiClickable = base.GetComponent<UIClickable>();
			this.onStartListening += delegate()
			{
				uiClickable.selected = true;
			};
			this.onStopListening += delegate()
			{
				uiClickable.selected = false;
			};
			this.UpdateDisplay();
			base.enabled = false;
			return this;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x00107E86 File Offset: 0x00106286
		public void OnClickHandler()
		{
			if (this.aem != null)
			{
				FabricWrapper.PostEvent(this.clickWidgetAudioId);
				this.StartListening();
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x00107EB8 File Offset: 0x001062B8
		private void UpdateDisplay()
		{
			KeyCode code = (this.aem == null) ? KeyCode.None : this.aem.keyCode;
			this.valueText.text = KeyCodeDisplayNames.Get(code);
			RewiredRemapUtils.ConflictType conflictType = (this.aem != null) ? RewiredRemapUtils.HasConflict(this.player, this.aem) : RewiredRemapUtils.ConflictType.None;
			this.hasConflict = (conflictType != RewiredRemapUtils.ConflictType.None);
			this.animator.modifyColor.SetActive(this.hasConflict);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x00107F3A File Offset: 0x0010633A
		private void SetListeningValue()
		{
			this.valueText.text = "????";
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00107F4C File Offset: 0x0010634C
		private bool StartListening()
		{
			base.enabled = true;
			this.animator.modifyColor.SetActive(false);
			this.SetListeningValue();
			if (this.onStartListening != null)
			{
				this.onStartListening();
			}
			return true;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00107F84 File Offset: 0x00106384
		private void Update()
		{
			if (this.player.controllers.Keyboard.GetKeyDown(KeyCode.Escape))
			{
				FabricWrapper.PostEvent(this.cancelKeyAudioId);
				base.enabled = false;
				return;
			}
			ElementAssignment elementAssignment;
			RewiredRemapUtils.ConflictType conflictType;
			bool flag = RewiredRemapUtils.PollForKey(this.player, this.aem, this.aem.actionId, out elementAssignment, out conflictType);
			if (flag && conflictType != RewiredRemapUtils.ConflictType.Hard)
			{
				if (elementAssignment.elementIdentifierId == this.aem.elementIdentifierId)
				{
					if (this.hasConflict)
					{
						FabricWrapper.PostEvent(this.softConflictAudioId);
					}
					else
					{
						FabricWrapper.PostEvent(this.assignKeyAudioId);
					}
				}
				else
				{
					if (conflictType == RewiredRemapUtils.ConflictType.Soft)
					{
						FabricWrapper.PostEvent(this.softConflictAudioId);
					}
					else
					{
						FabricWrapper.PostEvent(this.assignKeyAudioId);
					}
					this.aem.controllerMap.ReplaceOrCreateElementMap(elementAssignment);
					this.UpdateAem();
					this.onMappingChanged();
				}
				base.enabled = false;
			}
			else if (conflictType == RewiredRemapUtils.ConflictType.Hard)
			{
				this.animator.modifyColor.anim.SetCurrentAndActivate(1f);
				FabricWrapper.PostEvent(this.hardConflictAudioId);
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x001080B3 File Offset: 0x001064B3
		private void OnDisable()
		{
			this.UpdateDisplay();
			if (this.onStopListening != null)
			{
				this.onStopListening();
			}
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x001080D1 File Offset: 0x001064D1
		public override void ForceUpdate()
		{
			base.ForceUpdate();
			this.UpdateAem();
			this.UpdateDisplay();
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x001080E5 File Offset: 0x001064E5
		private void UpdateAem()
		{
			if (this.aem == null)
			{
				return;
			}
			this.aem = RewiredRemapUtils.GetActionElementMap(this.player, this.aem.actionId, this.aem.axisContribution);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x0010811A File Offset: 0x0010651A
		protected override void OnEnable()
		{
		}

		// Token: 0x0400293A RID: 10554
		[SerializeField]
		private Text valueText;

		// Token: 0x0400293B RID: 10555
		[SerializeField]
		private DistanceFieldAnimator animator;

		// Token: 0x0400293C RID: 10556
		private Player player;

		// Token: 0x0400293D RID: 10557
		private ActionElementMap aem;

		// Token: 0x0400293E RID: 10558
		private bool hasConflict;

		// Token: 0x04002942 RID: 10562
		private FabricEventReference clickWidgetAudioId = "UI/Menu/KeyValueSelection";

		// Token: 0x04002943 RID: 10563
		private FabricEventReference assignKeyAudioId = "UI/Menu/KeyValueAssignment";

		// Token: 0x04002944 RID: 10564
		private FabricEventReference cancelKeyAudioId = "UI/Menu/Back";

		// Token: 0x04002945 RID: 10565
		private FabricEventReference softConflictAudioId = "UI/Menu/KeyValueFail";

		// Token: 0x04002946 RID: 10566
		private FabricEventReference hardConflictAudioId = "UI/Menu/KeyValueHardFail";
	}
}
