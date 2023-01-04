using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C4 RID: 964
	public class RealTimeTranslation : MonoBehaviour
	{
		// Token: 0x06001591 RID: 5521 RVA: 0x0002C8A0 File Offset: 0x0002ACA0
		public void OnGUI()
		{
			GUILayout.Label("Translate:", new GUILayoutOption[0]);
			this.OriginalText = GUILayout.TextArea(this.OriginalText, new GUILayoutOption[]
			{
				GUILayout.Width((float)Screen.width)
			});
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button("English -> Español", new GUILayoutOption[]
			{
				GUILayout.Height(100f)
			}))
			{
				this.StartTranslating("en", "es");
			}
			if (GUILayout.Button("Español -> English", new GUILayoutOption[]
			{
				GUILayout.Height(100f)
			}))
			{
				this.StartTranslating("es", "en");
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.TextArea("Multiple Translation with 1 call:\n'This is an example' -> en,zh\n'Hola' -> en", new GUILayoutOption[0]);
			if (GUILayout.Button("Multi Translate", new GUILayoutOption[]
			{
				GUILayout.ExpandHeight(true)
			}))
			{
				this.ExampleMultiTranslations_Async();
			}
			GUILayout.EndHorizontal();
			GUILayout.TextArea(this.TranslatedText, new GUILayoutOption[]
			{
				GUILayout.Width((float)Screen.width)
			});
			GUILayout.Space(10f);
			if (this.IsTranslating)
			{
				GUILayout.Label("Contacting Google....", new GUILayoutOption[0]);
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0002C9F4 File Offset: 0x0002ADF4
		public void StartTranslating(string fromCode, string toCode)
		{
			this.IsTranslating = true;
			GoogleTranslation.Translate(this.OriginalText, fromCode, toCode, new Action<string, string>(this.OnTranslationReady));
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0002CA16 File Offset: 0x0002AE16
		private void OnTranslationReady(string Translation, string errorMsg)
		{
			this.IsTranslating = false;
			if (errorMsg != null)
			{
				Debug.LogError(errorMsg);
			}
			else
			{
				this.TranslatedText = Translation;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0002CA38 File Offset: 0x0002AE38
		public void ExampleMultiTranslations_Blocking()
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			GoogleTranslation.AddQuery("This is an example", "en", "es", dictionary);
			GoogleTranslation.AddQuery("This is an example", "auto", "zh", dictionary);
			GoogleTranslation.AddQuery("Hola", "es", "en", dictionary);
			if (!GoogleTranslation.ForceTranslate(dictionary, true))
			{
				return;
			}
			Debug.Log(GoogleTranslation.GetQueryResult("This is an example", "en", dictionary));
			Debug.Log(GoogleTranslation.GetQueryResult("This is an example", "zh", dictionary));
			Debug.Log(GoogleTranslation.GetQueryResult("This is an example", string.Empty, dictionary));
			Debug.Log(dictionary["Hola"].Results[0]);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0002CAF0 File Offset: 0x0002AEF0
		public void ExampleMultiTranslations_Async()
		{
			this.IsTranslating = true;
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			GoogleTranslation.AddQuery("This is an example", "en", "es", dictionary);
			GoogleTranslation.AddQuery("This is an example", "auto", "zh", dictionary);
			GoogleTranslation.AddQuery("Hola", "es", "en", dictionary);
			GoogleTranslation.Translate(dictionary, new Action<Dictionary<string, TranslationQuery>, string>(this.OnMultitranslationReady), true);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0002CB5C File Offset: 0x0002AF5C
		private void OnMultitranslationReady(Dictionary<string, TranslationQuery> dict, string errorMsg)
		{
			if (!string.IsNullOrEmpty(errorMsg))
			{
				Debug.LogError(errorMsg);
				return;
			}
			this.IsTranslating = false;
			this.TranslatedText = string.Empty;
			this.TranslatedText = this.TranslatedText + GoogleTranslation.GetQueryResult("This is an example", "es", dict) + "\n";
			this.TranslatedText = this.TranslatedText + GoogleTranslation.GetQueryResult("This is an example", "zh", dict) + "\n";
			this.TranslatedText = this.TranslatedText + GoogleTranslation.GetQueryResult("This is an example", string.Empty, dict) + "\n";
			this.TranslatedText += dict["Hola"].Results[0];
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0002CC25 File Offset: 0x0002B025
		public bool IsWaitingForTranslation()
		{
			return this.IsTranslating;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0002CC2D File Offset: 0x0002B02D
		public string GetTranslatedText()
		{
			return this.TranslatedText;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0002CC35 File Offset: 0x0002B035
		public void SetOriginalText(string text)
		{
			this.OriginalText = text;
		}

		// Token: 0x04000DA9 RID: 3497
		private string OriginalText = "This is an example showing how to use the google translator to translate chat messages within the game.\nIt also supports multiline translations.";

		// Token: 0x04000DAA RID: 3498
		private string TranslatedText = string.Empty;

		// Token: 0x04000DAB RID: 3499
		private bool IsTranslating;
	}
}
