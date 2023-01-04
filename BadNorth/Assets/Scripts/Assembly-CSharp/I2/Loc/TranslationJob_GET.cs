using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace I2.Loc
{
	// Token: 0x020003DD RID: 989
	public class TranslationJob_GET : TranslationJob_WWW
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x000348FC File Offset: 0x00032CFC
		public TranslationJob_GET(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady)
		{
			this._requests = requests;
			this._OnTranslationReady = OnTranslationReady;
			this.mQueries = GoogleTranslation.ConvertTranslationRequest(requests, true);
			this.GetState();
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00034928 File Offset: 0x00032D28
		private void ExecuteNextQuery()
		{
			if (this.mQueries.Count == 0)
			{
				this.mJobState = TranslationJob.eJobState.Succeeded;
				return;
			}
			int index = this.mQueries.Count - 1;
			string arg = this.mQueries[index];
			this.mQueries.RemoveAt(index);
			string uri = string.Format("{0}?action=Translate&list={1}", LocalizationManager.GetWebServiceURL(null), arg);
			this.www = UnityWebRequest.Get(uri);
			I2Utils.SendWebRequest(this.www);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000349A0 File Offset: 0x00032DA0
		public override TranslationJob.eJobState GetState()
		{
			if (this.www != null && this.www.isDone)
			{
				this.ProcessResult(this.www.downloadHandler.data, this.www.error);
				this.www.Dispose();
				this.www = null;
			}
			if (this.www == null)
			{
				this.ExecuteNextQuery();
			}
			return this.mJobState;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00034A14 File Offset: 0x00032E14
		public void ProcessResult(byte[] bytes, string errorMsg)
		{
			if (string.IsNullOrEmpty(errorMsg))
			{
				string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
				errorMsg = GoogleTranslation.ParseTranslationResult(@string, this._requests);
				if (string.IsNullOrEmpty(errorMsg))
				{
					if (this._OnTranslationReady != null)
					{
						this._OnTranslationReady(this._requests, null);
					}
					return;
				}
			}
			this.mJobState = TranslationJob.eJobState.Failed;
			this.mErrorMessage = errorMsg;
		}

		// Token: 0x04000DDE RID: 3550
		private Dictionary<string, TranslationQuery> _requests;

		// Token: 0x04000DDF RID: 3551
		private Action<Dictionary<string, TranslationQuery>, string> _OnTranslationReady;

		// Token: 0x04000DE0 RID: 3552
		private List<string> mQueries;

		// Token: 0x04000DE1 RID: 3553
		public string mErrorMessage;
	}
}
