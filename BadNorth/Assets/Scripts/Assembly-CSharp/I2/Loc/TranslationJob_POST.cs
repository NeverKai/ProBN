using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace I2.Loc
{
	// Token: 0x020003DF RID: 991
	public class TranslationJob_POST : TranslationJob_WWW
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x00034C54 File Offset: 0x00033054
		public TranslationJob_POST(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady)
		{
			this._requests = requests;
			this._OnTranslationReady = OnTranslationReady;
			List<string> list = GoogleTranslation.ConvertTranslationRequest(requests, false);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("action", "Translate");
			wwwform.AddField("list", list[0]);
			this.www = UnityWebRequest.Post(LocalizationManager.GetWebServiceURL(null), wwwform);
			I2Utils.SendWebRequest(this.www);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00034CC4 File Offset: 0x000330C4
		public override TranslationJob.eJobState GetState()
		{
			if (this.www != null && this.www.isDone)
			{
				this.ProcessResult(this.www.downloadHandler.data, this.www.error);
				this.www.Dispose();
				this.www = null;
			}
			return this.mJobState;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00034D28 File Offset: 0x00033128
		public void ProcessResult(byte[] bytes, string errorMsg)
		{
			if (!string.IsNullOrEmpty(errorMsg))
			{
				this.mJobState = TranslationJob.eJobState.Failed;
			}
			else
			{
				string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
				errorMsg = GoogleTranslation.ParseTranslationResult(@string, this._requests);
				if (this._OnTranslationReady != null)
				{
					this._OnTranslationReady(this._requests, errorMsg);
				}
				this.mJobState = TranslationJob.eJobState.Succeeded;
			}
		}

		// Token: 0x04000DE8 RID: 3560
		private Dictionary<string, TranslationQuery> _requests;

		// Token: 0x04000DE9 RID: 3561
		private Action<Dictionary<string, TranslationQuery>, string> _OnTranslationReady;
	}
}
