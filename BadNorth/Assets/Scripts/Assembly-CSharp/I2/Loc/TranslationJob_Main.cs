using System;
using System.Collections.Generic;

namespace I2.Loc
{
	// Token: 0x020003DE RID: 990
	public class TranslationJob_Main : TranslationJob
	{
		// Token: 0x0600166D RID: 5741 RVA: 0x00034A81 File Offset: 0x00032E81
		public TranslationJob_Main(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady)
		{
			this._requests = requests;
			this._OnTranslationReady = OnTranslationReady;
			this.mPost = new TranslationJob_POST(requests, OnTranslationReady);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00034AA4 File Offset: 0x00032EA4
		public override TranslationJob.eJobState GetState()
		{
			if (this.mWeb != null)
			{
				TranslationJob.eJobState state = this.mWeb.GetState();
				if (state == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state != TranslationJob.eJobState.Succeeded)
				{
					if (state == TranslationJob.eJobState.Failed)
					{
						this.mWeb.Dispose();
						this.mWeb = null;
						this.mPost = new TranslationJob_POST(this._requests, this._OnTranslationReady);
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			if (this.mPost != null)
			{
				TranslationJob.eJobState state2 = this.mPost.GetState();
				if (state2 == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state2 != TranslationJob.eJobState.Succeeded)
				{
					if (state2 == TranslationJob.eJobState.Failed)
					{
						this.mPost.Dispose();
						this.mPost = null;
						this.mGet = new TranslationJob_GET(this._requests, this._OnTranslationReady);
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			if (this.mGet != null)
			{
				TranslationJob.eJobState state3 = this.mGet.GetState();
				if (state3 == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state3 != TranslationJob.eJobState.Succeeded)
				{
					if (state3 == TranslationJob.eJobState.Failed)
					{
						this.mErrorMessage = this.mGet.mErrorMessage;
						if (this._OnTranslationReady != null)
						{
							this._OnTranslationReady(this._requests, this.mErrorMessage);
						}
						this.mGet.Dispose();
						this.mGet = null;
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			return this.mJobState;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00034C17 File Offset: 0x00033017
		public override void Dispose()
		{
			if (this.mPost != null)
			{
				this.mPost.Dispose();
			}
			if (this.mGet != null)
			{
				this.mGet.Dispose();
			}
			this.mPost = null;
			this.mGet = null;
		}

		// Token: 0x04000DE2 RID: 3554
		private TranslationJob_WEB mWeb;

		// Token: 0x04000DE3 RID: 3555
		private TranslationJob_POST mPost;

		// Token: 0x04000DE4 RID: 3556
		private TranslationJob_GET mGet;

		// Token: 0x04000DE5 RID: 3557
		private Dictionary<string, TranslationQuery> _requests;

		// Token: 0x04000DE6 RID: 3558
		private Action<Dictionary<string, TranslationQuery>, string> _OnTranslationReady;

		// Token: 0x04000DE7 RID: 3559
		public string mErrorMessage;
	}
}
