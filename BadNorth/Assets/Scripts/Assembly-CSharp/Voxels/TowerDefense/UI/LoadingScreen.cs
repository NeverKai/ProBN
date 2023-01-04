using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using ReflexCLI.Attributes;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008D7 RID: 2263
	public class LoadingScreen : UIMenu, IGameSetup
	{
		// Token: 0x06003BD2 RID: 15314 RVA: 0x001098F4 File Offset: 0x00107CF4
		public static void BeginLoadingPhase(string description, Action completionCallback = null, params IEnumerator[] routines)
		{
			LoadingScreen._instance.BeginLoadingPhaseInternal(description, completionCallback, routines);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00109904 File Offset: 0x00107D04
		private void BeginLoadingPhaseInternal(string description, Action completionCallback, params IEnumerator[] routines)
		{
			bool isThisFrame = this.closeTime.isThisFrame;
			bool flag = !string.IsNullOrEmpty(description);
			bool flag2 = (this.textVisibility.visible && isThisFrame) || flag;
			this.visibility.SetVisible(true, false);
			this.textVisibility.SetVisible(flag2, false);
			this.backgroundSolidVisibility.SetVisible(!flag2, false);
			base.gameObject.SetActive(true);
			if (isThisFrame)
			{
				if (flag)
				{
					this.descriptions.Add(description);
				}
			}
			else
			{
				this.descriptions.Clear();
				this.UpdateDescriptionImmediate(description);
				this.hintCanvasGroup.alpha = 1f;
				this.UpdateHint();
			}
			this.OpenMenu();
			base.StartCoroutine(this.routineWrapper(completionCallback, routines));
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x001099D4 File Offset: 0x00107DD4
		private IEnumerator routineWrapper(Action completionCallback, params IEnumerator[] routines)
		{
			while (this.visibility.alpha < 1f || (this.textVisibility.visible && this.textVisibility.alpha < 1f))
			{
				yield return null;
			}
			Singleton<Stack>.instance.stateLoading.SetActive(true);
			yield return null;
			base.StartCoroutine(this.DescriptionAnimator());
			base.StartCoroutine(this.HintAnimator());
			foreach (IEnumerator routine in routines)
			{
				if (routine != null)
				{
					bool done = false;
					while (!done)
					{
						try
						{
							done = !routine.MoveNext();
						}
						catch (Exception ex)
						{
							throw ex;
						}
						yield return null;
					}
				}
			}
			while (this.descriptions.Count > 0)
			{
				yield return null;
			}
			this.visibility.SetVisible(false, false);
			Singleton<Stack>.instance.stateLoading.SetActive(false);
			this.CloseMenu();
			base.StopAllCoroutines();
			if (completionCallback != null)
			{
				completionCallback();
			}
			yield break;
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x00109A00 File Offset: 0x00107E00
		private IEnumerator DescriptionAnimator()
		{
			float t = Time.unscaledTime + 0.1f;
			while (t > Time.unscaledTime)
			{
				yield return null;
			}
			for (;;)
			{
				if (this.descriptions.Count > 0)
				{
					RectTransform containerTrans = this.descriptionText.transform.parent as RectTransform;
					float height = containerTrans.rect.height;
					this.descriptionText2.text = this.descriptions[0];
					this.descriptionText2.gameObject.SetActive(true);
					for (float a = 0f; a < 1f; a += Mathf.Min(0.05f, Time.unscaledDeltaTime * 3.5f))
					{
						float a2 = Mathf.Pow(a, 1.75f);
						this.descriptionText2.color = this.descriptionText2.color.SetA(this.defaultDescriptionAlpha * a2);
						this.descriptionText2.transform.localPosition = this.descriptionText2.transform.localPosition.SetY((-1f + a2) * height);
						float a3 = Mathf.Pow(Mathf.Min(a * 1.2f, 1f), 1.5f);
						this.descriptionText.color = this.descriptionText.color.SetA(this.defaultDescriptionAlpha * (1f - a3));
						this.descriptionText.transform.localPosition = this.descriptionText.transform.localPosition.SetY(a3 * height);
						yield return null;
					}
					this.UpdateDescriptionImmediate(this.descriptions[0]);
					float t2 = Time.unscaledTime + 0.1f;
					while (t2 > Time.unscaledTime)
					{
						yield return null;
					}
					this.descriptions.RemoveAt(0);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x00109A1C File Offset: 0x00107E1C
		private IEnumerator HintAnimator()
		{
			for (;;)
			{
				float end = Time.unscaledTime + this.hintUpdateTime;
				while (Time.unscaledTime < end)
				{
					yield return null;
				}
				for (float a = 1f; a > 0f; a -= Mathf.Min(Time.unscaledDeltaTime, 0.05f) * 6f)
				{
					this.hintCanvasGroup.alpha = a;
					yield return null;
				}
				this.hintCanvasGroup.alpha = 0f;
				this.UpdateHint();
				for (float a2 = 0f; a2 < 1f; a2 += Mathf.Min(Time.unscaledDeltaTime, 0.05f) * 3f)
				{
					this.hintCanvasGroup.alpha = a2;
					yield return null;
				}
				this.hintCanvasGroup.alpha = 1f;
			}
			yield break;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x00109A37 File Offset: 0x00107E37
		private void OnDisable()
		{
			this.textVisibility.SetVisible(false, true);
			this.backgroundSolidVisibility.SetVisible(false, true);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x00109A54 File Offset: 0x00107E54
		void IGameSetup.OnGameAwake()
		{
			this.textVisibility = this.textVis.GetComponent<IUIVisibility>();
			this.textVisibility.SetVisible(false, true);
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.backgroundSolidVisibility = this.backgroundSolid.GetComponent<IUIVisibility>();
			LoadingScreen._instance = this;
			this.hints = new SmartShuffler<string>(LocalizationManager.GetTermsList("HINTS"));
			this.hintText = this.hintLocText.GetComponent<Text>();
			this.descriptionText2 = this.descriptionText.transform.parent.gameObject.InstantiateChild(this.descriptionText, null);
			this.descriptionText2.text = "test";
			this.defaultDescriptionAlpha = this.descriptionText.color.a;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00109B28 File Offset: 0x00107F28
		private void UpdateHint()
		{
			string text = this.hintLocText.Term;
			while (text == this.hintLocText.Term && !string.IsNullOrEmpty(text))
			{
				text = this.hints.Get();
			}
			this.hintLocText.Term = text;
			foreach (LoadingScreen.HintData hintData in this.specialHints)
			{
				hintData.gameObject.SetActive(hintData.hintTerm == text);
			}
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x00109BE0 File Offset: 0x00107FE0
		public static void UpdateDescription(string description)
		{
			LoadingScreen._instance.UpdateDescription_Internal(description);
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x00109BED File Offset: 0x00107FED
		private void UpdateDescription_Internal(string description)
		{
			if (this.textVisibility.visible && !string.IsNullOrEmpty(description))
			{
				this.descriptions.Add(description);
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00109C18 File Offset: 0x00108018
		private void UpdateDescriptionImmediate(string description)
		{
			this.descriptionText.text = description;
			this.descriptionText2.text = string.Empty;
			this.descriptionText.color = this.descriptionText.color.SetA(1f);
			this.descriptionText2.color = this.descriptionText2.color.SetA(0f);
			this.descriptionText.gameObject.SetActive(true);
			this.descriptionText2.gameObject.SetActive(false);
			this.descriptionText.transform.localPosition = this.descriptionText.transform.localPosition.SetY(0f);
		}

		// Token: 0x04002996 RID: 10646
		private static LoadingScreen _instance;

		// Token: 0x04002997 RID: 10647
		[SerializeField]
		private Text descriptionText;

		// Token: 0x04002998 RID: 10648
		private Text descriptionText2;

		// Token: 0x04002999 RID: 10649
		[SerializeField]
		private Localize hintLocText;

		// Token: 0x0400299A RID: 10650
		private Text hintText;

		// Token: 0x0400299B RID: 10651
		[SerializeField]
		private CanvasGroup hintCanvasGroup;

		// Token: 0x0400299C RID: 10652
		[SerializeField]
		private GameObject textVis;

		// Token: 0x0400299D RID: 10653
		[SerializeField]
		private GameObject backgroundSolid;

		// Token: 0x0400299E RID: 10654
		[SerializeField]
		[ConsoleCommand("")]
		private bool dbgInfiniteLoad;

		// Token: 0x0400299F RID: 10655
		[SerializeField]
		[ConsoleCommand("")]
		private float hintUpdateTime = 8f;

		// Token: 0x040029A0 RID: 10656
		[SerializeField]
		private List<LoadingScreen.HintData> specialHints = new List<LoadingScreen.HintData>();

		// Token: 0x040029A1 RID: 10657
		private IUIVisibility visibility;

		// Token: 0x040029A2 RID: 10658
		private IUIVisibility backgroundSolidVisibility;

		// Token: 0x040029A3 RID: 10659
		private IUIVisibility textVisibility;

		// Token: 0x040029A4 RID: 10660
		private System.Random hintRand = new System.Random();

		// Token: 0x040029A5 RID: 10661
		private SmartShuffler<string> hints;

		// Token: 0x040029A6 RID: 10662
		private List<string> descriptions = new List<string>(4);

		// Token: 0x040029A7 RID: 10663
		private float defaultDescriptionAlpha = 1f;

		// Token: 0x020008D8 RID: 2264
		[Serializable]
		private struct HintData
		{
			// Token: 0x040029A8 RID: 10664
			[TermsPopup("")]
			public string hintTerm;

			// Token: 0x040029A9 RID: 10665
			public GameObject gameObject;
		}
	}
}
