using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000069 RID: 105
	public class PlatformSystemMessenger : MonoBehaviour
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060004CD RID: 1229 RVA: 0x00006E58 File Offset: 0x00005258
		// (remove) Token: 0x060004CE RID: 1230 RVA: 0x00006E8C File Offset: 0x0000528C
		
		public static event Action<bool> OnMessageChange;

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00006EC0 File Offset: 0x000052C0
		public bool ShowingMessage
		{
			get
			{
				return this._activeMessage != null;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00006ECE File Offset: 0x000052CE
		public int WiatingMessage
		{
			get
			{
				return this._waitingMessages.Count;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00006EDB File Offset: 0x000052DB
		public void ActivateOption(int options)
		{
			if (this._optionChosen == -1)
			{
				this._optionChosen = options;
				BasePlatformManager.Instance.AddToNextUpdate(new Action(this.DoAction));
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00006F08 File Offset: 0x00005308
		private void DoAction()
		{
			if (this._activeMessage != null)
			{
				if (this._optionChosen != 0 && this._activeMessage.HasOptionB)
				{
					if (this._activeMessage.optionBAction != null)
					{
						this._activeMessage.optionBAction();
					}
				}
				else if (this._activeMessage.optionAAction != null)
				{
					this._activeMessage.optionAAction();
				}
				this.DiscardMessage();
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00006F88 File Offset: 0x00005388
		protected virtual void DiscardMessage()
		{
			this._optionChosen = -1;
			if (this._activeMessage != null)
			{
				this._activeMessage = null;
				if (PlatformSystemMessenger.OnMessageChange != null)
				{
					PlatformSystemMessenger.OnMessageChange(false);
				}
			}
			if (this._waitingMessages.Count != 0)
			{
				this.ShowMessage();
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00006FD9 File Offset: 0x000053D9
		protected virtual void ShowMessage()
		{
			if (this._waitingMessages.Count != 0)
			{
				this._activeMessage = this._waitingMessages.Dequeue();
				if (PlatformSystemMessenger.OnMessageChange != null)
				{
					PlatformSystemMessenger.OnMessageChange(true);
				}
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00007011 File Offset: 0x00005411
		public void ShowMessage(PlatformSystemMessenger.MessageInfo newMessage)
		{
			this._waitingMessages.Enqueue(newMessage);
			if (!this.ShowingMessage)
			{
				this.ShowMessage();
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00007030 File Offset: 0x00005430
		public void ShowYESNO(string body, Action yes, Action no)
		{
			this.ShowMessage(new PlatformSystemMessenger.MessageInfo(body, PlatformSystemMessenger.SYSTEM_MESSAGE_YES, yes, PlatformSystemMessenger.SYSTEM_MESSAGE_NO, no));
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000704A File Offset: 0x0000544A
		public void ShowOK(string body, Action action)
		{
			this.ShowMessage(new PlatformSystemMessenger.MessageInfo(body, PlatformSystemMessenger.SYSTEM_MESSAGE_OK, action, null, null));
		}

		// Token: 0x040001ED RID: 493
		public static string SYSTEM_MESSAGE_YES = "YES";

		// Token: 0x040001EE RID: 494
		public static string SYSTEM_MESSAGE_NO = "NO";

		// Token: 0x040001EF RID: 495
		public static string SYSTEM_MESSAGE_OK = "OK";

		// Token: 0x040001F0 RID: 496
		private Queue<PlatformSystemMessenger.MessageInfo> _waitingMessages = new Queue<PlatformSystemMessenger.MessageInfo>();

		// Token: 0x040001F1 RID: 497
		protected PlatformSystemMessenger.MessageInfo _activeMessage;

		// Token: 0x040001F2 RID: 498
		private int _optionChosen = -1;

		// Token: 0x0200006A RID: 106
		public class MessageInfo
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x00007080 File Offset: 0x00005480
			public MessageInfo(string bodyCode, string optionACode, Action onOptionA, string optionBCode = null, Action onOptionB = null)
			{
				this.messageBody = bodyCode;
				this.optionAText = optionACode;
				this.optionAAction = onOptionA;
				this.optionBText = optionBCode;
				this.optionBAction = onOptionB;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x000070AD File Offset: 0x000054AD
			public bool HasOptionB
			{
				get
				{
					return !string.IsNullOrEmpty(this.optionBText);
				}
			}

			// Token: 0x040001F3 RID: 499
			public string messageBody;

			// Token: 0x040001F4 RID: 500
			public string optionAText;

			// Token: 0x040001F5 RID: 501
			public Action optionAAction;

			// Token: 0x040001F6 RID: 502
			public string optionBText;

			// Token: 0x040001F7 RID: 503
			public Action optionBAction;
		}
	}
}
