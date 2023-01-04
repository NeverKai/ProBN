using System;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000030 RID: 48
	public abstract class BasePostBox<T> : MonoBehaviour where T : struct
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000075B8 File Offset: 0x000059B8
		public void SendMessage(T userID, PlatformMessageBase message, bool reliable)
		{
			object lockSend = BasePostBox<T>._lockSend;
			lock (lockSend)
			{
				message.FallbackID = BasePlatformManager.Instance.GetUserID;
				this.SendNewMessage(userID, message.RawMessage, message.RawMessageSize, reliable);
			}
		}

		// Token: 0x06000199 RID: 409
		public abstract void SendNewMessage(T userID, byte[] message, int size, bool reliable);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007614 File Offset: 0x00005A14
		private bool RunThread
		{
			get
			{
				object lockThread = BasePostBox<T>._lockThread;
				bool running;
				lock (lockThread)
				{
					running = this._messageThread.Running;
				}
				return running;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007658 File Offset: 0x00005A58
		public void StartMessageThread()
		{
			object lockThread = BasePostBox<T>._lockThread;
			lock (lockThread)
			{
				if (this._messageThread == null)
				{
					this._messageThread = new ThreadHandler("BasePostBox");
					this._messageThread.AddPart(new Action(this.SortMessages));
					this._messageThread.RunThreadOnce = false;
					this._messageThread.PauseThread = false;
				}
				this._messageThread.Start();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000076E4 File Offset: 0x00005AE4
		public void StopMessageThread()
		{
			object lockThread = BasePostBox<T>._lockThread;
			lock (lockThread)
			{
				if (this._messageThread != null)
				{
					this._messageThread.Complete();
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007730 File Offset: 0x00005B30
		public void WaitForMessageThreadEnd()
		{
			bool flag = true;
			object lockThread = BasePostBox<T>._lockThread;
			lock (lockThread)
			{
				flag = (this._messageThread != null);
			}
			this.StopMessageThread();
			while (flag)
			{
				object lockThread2 = BasePostBox<T>._lockThread;
				lock (lockThread2)
				{
					flag = this._messageThread.Running;
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000077B8 File Offset: 0x00005BB8
		public void SortMessages()
		{
			T senderID = Activator.CreateInstance<T>();
			DataReader message;
			while (this.GetMessage(ref senderID, out message) && this.RunThread)
			{
				this.ParseMessage(senderID, message);
			}
		}

		// Token: 0x0600019F RID: 415
		protected abstract bool GetMessage(ref T senderID, out DataReader message);

		// Token: 0x060001A0 RID: 416 RVA: 0x000077F4 File Offset: 0x00005BF4
		protected virtual void ParseMessage(T senderID, DataReader message)
		{
			PlatformMessageBase.SetToAfterFallbackID(message);
			switch (message.Flag)
			{
			case 11:
				CS.Platform.Utils.Debug.ThreadLogInfo("[BPB] Ping: {0}", new object[]
				{
					senderID.ToString()
				});
				if (this._pongMessage == null)
				{
					this._pongMessage = new PlatformMessageBase();
					this._pongMessage.MessageType = MessageTypes.PONG;
				}
				this.SendMessage(senderID, this._pongMessage, false);
				break;
			case 12:
				CS.Platform.Utils.Debug.ThreadLogInfo("[BPB] Pong: {0}", new object[]
				{
					senderID.ToString()
				});
				break;
			case 13:
			{
				string message2 = "[BPB] Debug Message : " + senderID.ToString() + " | " + message.ReadString();
				CS.Platform.Utils.Debug.ThreadLogWarning(message2, new object[0]);
				break;
			}
			default:
				CS.Platform.Utils.Debug.ThreadLogWarning("[BPB] Unkown message type: {0}", new object[]
				{
					((MessageTypes)message.Flag).ToString()
				});
				break;
			}
		}

		// Token: 0x0400008F RID: 143
		protected byte[] _newMessage;

		// Token: 0x04000090 RID: 144
		private PlatformMessageBase _pongMessage;

		// Token: 0x04000091 RID: 145
		private static object _lockSend = new object();

		// Token: 0x04000092 RID: 146
		private static object _lockThread = new object();

		// Token: 0x04000093 RID: 147
		protected ThreadHandler _messageThread;
	}
}
