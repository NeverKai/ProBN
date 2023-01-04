using System;
using System.Collections;
using CS.VT;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.Pools;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200090D RID: 2317
	public class SaveGameWidget : MonoBehaviour, IPoolable, IComparable<SaveGameWidget>
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x0011690D File Offset: 0x00114D0D
		// (set) Token: 0x06003E11 RID: 15889 RVA: 0x00116915 File Offset: 0x00114D15
		public CampaignSaveMeta campaignMeta { get; private set; }

		// Token: 0x06003E12 RID: 15890 RVA: 0x00116920 File Offset: 0x00114D20
		private void Initialize()
		{
			this.slotIdLoc = this.slotIdText.GetComponent<Localize>();
			this.slotIdParams = this.slotIdText.GetComponent<LocalizationParamsManager>();
			this.deleteVis = this.deleteIcon.GetComponent<IUIVisibility>();
			this.deleteVis.SetVisible(false, true);
			CanvasGroup component = this.touchDeleteButton.GetComponent<CanvasGroup>();
			this.deleteActionListener.enabled = false;
			CanvasGroup orAddComponent = this.slotIdText.gameObject.GetOrAddComponent<CanvasGroup>();
			this.clickable = base.GetComponent<UIClickable>();
			this.clickable.onStateChanged += delegate(UIInteractable.State s)
			{
				bool visible = this.campaignMeta && (s == UIInteractable.State.Hover || s == UIInteractable.State.PointerButtonDown) && Profile.userSettings.cursorBehaviour != UserSettings.CursorBehaviour.Touch;
				this.deleteVis.SetVisible(visible, false);
				bool flag = s == UIInteractable.State.Focus;
				this.deleteActionListener.enabled = (flag && this.campaignMeta);
				if (flag)
				{
					this.ownerMenu.selected = null;
				}
			};
			float deletePos = this.touchDeleteButton.transform.localPosition.x;
			RectTransform checkRt = this.checkmark.transform as RectTransform;
			AnimatedState expanded = new AnimatedState("expanded", this.stateRoot.rootState, false, false);
			Action<float> setFunc = delegate(float a)
			{
				this.touchDeleteButton.transform.localPosition = this.touchDeleteButton.transform.localPosition.SetX(deletePos + a * 10f);
				this.detailsMargin.transform.localPosition = Vector3.right * a * 55f;
				this.checkmark.alpha = a;
				checkRt.pivot = checkRt.pivot.SetX(Mathf.Lerp(0.7f, 0.5f, a));
				if (this.ownerMenu)
				{
					this.ownerMenu.transform.MarkChildLayoutsForRebuild(true);
				}
			};
			expanded.anim.Subscribe(setFunc);
			expanded.Subscribe(component);
			this.clickable.onSelectedChanged += delegate(bool b)
			{
				expanded.SetActive(b);
			};
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00116A5C File Offset: 0x00114E5C
		public void Setup(SaveGameManagementMenu ownerMenu, CampaignSaveMeta campaignMeta)
		{
			this.ownerMenu = ownerMenu;
			this.campaignMeta = campaignMeta;
			this.loadableContent.alpha = ((!campaignMeta) ? 0f : 1f);
			this.emptyContent.alpha = 1f - this.loadableContent.alpha;
			this.emptyColorModifier.alpha = this.emptyContent.alpha;
			if (campaignMeta)
			{
				this.UpdateDisplay();
			}
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00116AE0 File Offset: 0x00114EE0
		private void UpdateDisplay()
		{
			string displayName = this.campaignMeta.GetDisplayName();
			bool flag = !string.IsNullOrEmpty(displayName);
			if (flag)
			{
				this.slotIdText.text = displayName;
			}
			else
			{
				this.slotIdLoc.Term = "UI/SAVE_LOAD/SLOT_DISPLAY_NAME";
				this.slotIdParams.SetParameterValue("NUM", IntStringCache.GetClean(this.campaignMeta.campaignNumber), true);
			}
			Behaviour behaviour = this.slotIdLoc;
			bool enabled = !flag;
			this.slotIdParams.enabled = enabled;
			behaviour.enabled = enabled;
			bool loadToCheckpoint = this.campaignMeta.loadToCheckpoint;
			int seconds = (!loadToCheckpoint) ? this.campaignMeta.playTime : this.campaignMeta.checkpointPlayTime;
			TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
			int num = Mathf.RoundToInt((float)timeSpan.Minutes);
			int num2 = Mathf.RoundToInt((float)timeSpan.Hours);
			this.playTimeParams.SetParameterValue("TIME", string.Format("{0:0}:{1:00}", num2, num), true);
			string term;
			int i;
			this.GetDisplayTimeSince(this.campaignMeta.savedTime, out term, out i);
			this.timeSinceText.Term = term;
			this.timeSinceParams.SetParameterValue("TIME", IntStringCache.GetClean(i), true);
			this.difficultyText.Term = this.campaignMeta.prefs.difficulty.GetLocTerm();
			float num3 = (!this.campaignMeta.loadToCheckpoint) ? this.campaignMeta.campaignFraction : this.campaignMeta.checkpointFraction;
			bool flag2 = num3 > 0f;
			if (flag2)
			{
				this.percentComplete.text = IntStringCache.percent.Get(Mathf.RoundToInt(num3 * 100f));
			}
			this.percentComplete.gameObject.SetActive(flag2);
			this.progressDivider.SetActive(flag2);
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x00116CD0 File Offset: 0x001150D0
		private void GetDisplayTimeSince(DateTime time, out string term, out int parameter)
		{
			TimeSpan timeSpan = DateTime.UtcNow - time;
			if (timeSpan.TotalMinutes < 2.0)
			{
				term = "UI/SAVE_LOAD/JUST_NOW";
				parameter = 0;
			}
			else if (timeSpan.TotalMinutes < 120.0)
			{
				term = "UI/SAVE_LOAD/MINUTES_AGO";
				parameter = Mathf.FloorToInt((float)timeSpan.TotalMinutes);
			}
			else if (timeSpan.TotalHours < 48.0)
			{
				term = "UI/SAVE_LOAD/HOURS_AGO";
				parameter = Mathf.FloorToInt((float)timeSpan.TotalHours);
			}
			else
			{
				term = "UI/SAVE_LOAD/DAYS_AGO";
				parameter = Mathf.FloorToInt((float)timeSpan.TotalDays);
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00116D84 File Offset: 0x00115184
		public void HandleGenericButton()
		{
			bool flag = this.campaignMeta && this.ownerMenu.selected != this.clickable && InputHelpers.ControllerTypeIs(ControllerType.Mouse) && Profile.userSettings.cursorBehaviour == UserSettings.CursorBehaviour.Touch;
			if (flag)
			{
				this.ownerMenu.selected = this.clickable;
				FabricWrapper.PostEvent(FabricID.uiButtonClick);
			}
			else
			{
				this.ownerMenu.HandleLoadButton(this.campaignMeta);
				this.ownerMenu.selected = null;
			}
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00116E1C File Offset: 0x0011521C
		public void HandleDeleteButton()
		{
			ModalOverlay modalOverlay = ModalOverlayPool.GetInstance().Initialize("UI/SAVE_LOAD/DELETE_SAVE/TITLE", "UI/SAVE_LOAD/DELETE_SAVE/MESSAGE", false).SetOpenAudio(SaveGameWidget.deletePopupAudio);
			modalOverlay.AddCancelButton().SetSuccessAudio(SaveGameWidget.cancelDeleteAudio);
			modalOverlay.AddButton("UI/SAVE_LOAD/DELETE_SAVE/PROMPT", delegate()
			{
				this.DeleteSave();
				return true;
			}, null, null).SetSuccessAudio(SaveGameWidget.confirmDeleteAudio);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00116E7E File Offset: 0x0011527E
		private void DeleteSave()
		{
			base.StartCoroutine(this.DeleteSaveRoutine());
			this.deleteActionListener.enabled = false;
			this.Setup(this.ownerMenu, null);
			this.ownerMenu.OnSaveDeleted();
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00116EB4 File Offset: 0x001152B4
		private IEnumerator DeleteSaveRoutine()
		{
			Callback<int> callback = Profile.meta.DeleteSaveSlot(this.campaignMeta);
			while (callback.State == Callback<int>.CallbackState.Active)
			{
				yield return null;
			}
			if (callback.State != Callback<int>.CallbackState.Complete)
			{
				Debug.LogError("delete save failed");
			}
			yield break;
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00116ECF File Offset: 0x001152CF
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x00116EDC File Offset: 0x001152DC
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.Initialize();
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00116EE4 File Offset: 0x001152E4
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x00116EF2 File Offset: 0x001152F2
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00116F00 File Offset: 0x00115300
		int IComparable<SaveGameWidget>.CompareTo(SaveGameWidget other)
		{
			if (this.campaignMeta && other.campaignMeta)
			{
				return -this.campaignMeta.savedTime.CompareTo(other.campaignMeta.savedTime);
			}
			return (!this.campaignMeta) ? ((!other.campaignMeta) ? 0 : 1) : -1;
		}

		// Token: 0x04002B56 RID: 11094
		[SerializeField]
		private CanvasGroup emptyContent;

		// Token: 0x04002B57 RID: 11095
		[SerializeField]
		private CanvasGroup loadableContent;

		// Token: 0x04002B58 RID: 11096
		[SerializeField]
		private CanvasGroup touchDeleteButton;

		// Token: 0x04002B59 RID: 11097
		[SerializeField]
		private CanvasGroup checkmark;

		// Token: 0x04002B5A RID: 11098
		[SerializeField]
		private RectTransform detailsMargin;

		// Token: 0x04002B5B RID: 11099
		[SerializeField]
		private ColorModifier emptyColorModifier;

		// Token: 0x04002B5C RID: 11100
		[SerializeField]
		private Text slotIdText;

		// Token: 0x04002B5D RID: 11101
		private Localize slotIdLoc;

		// Token: 0x04002B5E RID: 11102
		private LocalizationParamsManager slotIdParams;

		// Token: 0x04002B5F RID: 11103
		[SerializeField]
		private Localize timeSinceText;

		// Token: 0x04002B60 RID: 11104
		[SerializeField]
		private LocalizationParamsManager timeSinceParams;

		// Token: 0x04002B61 RID: 11105
		[SerializeField]
		private LocalizationParamsManager playTimeParams;

		// Token: 0x04002B62 RID: 11106
		[SerializeField]
		private Localize difficultyText;

		// Token: 0x04002B63 RID: 11107
		[SerializeField]
		private Text percentComplete;

		// Token: 0x04002B64 RID: 11108
		[SerializeField]
		private GameObject progressDivider;

		// Token: 0x04002B65 RID: 11109
		[SerializeField]
		private GameObject deleteIcon;

		// Token: 0x04002B66 RID: 11110
		private IUIVisibility deleteVis;

		// Token: 0x04002B67 RID: 11111
		[SerializeField]
		private UIActionListener deleteActionListener;

		// Token: 0x04002B68 RID: 11112
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002B6A RID: 11114
		private SaveGameManagementMenu ownerMenu;

		// Token: 0x04002B6B RID: 11115
		private UIClickable clickable;

		// Token: 0x04002B6C RID: 11116
		private static FabricEventReference deletePopupAudio = "UI/Menu/DeleteSaveSelect";

		// Token: 0x04002B6D RID: 11117
		private static FabricEventReference confirmDeleteAudio = "UI/Menu/DeleteSave";

		// Token: 0x04002B6E RID: 11118
		private static FabricEventReference cancelDeleteAudio = "UI/Menu/DeleteSaveBack";
	}
}
