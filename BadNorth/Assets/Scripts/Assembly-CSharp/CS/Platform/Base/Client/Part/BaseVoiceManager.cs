using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CS.Platform.Utils;
using UnityEngine;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000035 RID: 53
	public class BaseVoiceManager : MonoBehaviour
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009C78 File Offset: 0x00008078
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00009CB8 File Offset: 0x000080B8
		public bool VoiceRunning
		{
			get
			{
				object lockBody = BaseVoiceManager._lockBody;
				bool voiceRunning;
				lock (lockBody)
				{
					voiceRunning = this._voiceRunning;
				}
				return voiceRunning;
			}
			set
			{
				object lockBody = BaseVoiceManager._lockBody;
				lock (lockBody)
				{
					if (this._voiceRunning != value)
					{
						this._voiceRunning = value;
						PlatformEvents.VoiceActiveStateChanged(this._Manager.GetUserBaseInfo(), this._voiceRunning);
						PlatformEvents.VoiceMicChange(this._voiceRunning);
					}
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009D24 File Offset: 0x00008124
		public int TotalConnectedUsers
		{
			get
			{
				object lockBody = BaseVoiceManager._lockBody;
				int count;
				lock (lockBody)
				{
					count = this._connectedUsers.Count;
				}
				return count;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009D68 File Offset: 0x00008168
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00009DA8 File Offset: 0x000081A8
		public float DefaultVolume
		{
			get
			{
				object lockBody = BaseVoiceManager._lockBody;
				float defaultVolume;
				lock (lockBody)
				{
					defaultVolume = this._defaultVolume;
				}
				return defaultVolume;
			}
			set
			{
				object lockBody = BaseVoiceManager._lockBody;
				lock (lockBody)
				{
					if (this._defaultVolume != value)
					{
						this._defaultVolume = value;
						this.SetVoiceVolumes(this._defaultVolume);
					}
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009DFC File Offset: 0x000081FC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009E3C File Offset: 0x0000823C
		public bool DefaultMute
		{
			get
			{
				object lockBody = BaseVoiceManager._lockBody;
				bool defaultMute;
				lock (lockBody)
				{
					defaultMute = this._defaultMute;
				}
				return defaultMute;
			}
			set
			{
				object lockBody = BaseVoiceManager._lockBody;
				lock (lockBody)
				{
					if (this._defaultMute != value)
					{
						this._defaultMute = value;
						this.SetVoiceMutes(this._defaultMute);
					}
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009E90 File Offset: 0x00008290
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00009ED0 File Offset: 0x000082D0
		public bool VoipEnabled
		{
			get
			{
				object lockBody = BaseVoiceManager._lockBody;
				bool voipEnabled;
				lock (lockBody)
				{
					voipEnabled = this._voipEnabled;
				}
				return voipEnabled;
			}
			set
			{
				object lockBody = BaseVoiceManager._lockBody;
				lock (lockBody)
				{
					if (this._voipEnabled != value)
					{
						this._voipEnabled = value;
						if (!this._voipEnabled)
						{
							this.StopVoice();
							this.SetVoiceMutes(true);
						}
						else
						{
							this.SetVoiceMutes(false);
						}
					}
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009F3C File Offset: 0x0000833C
		private void Awake()
		{
			this._Manager = null;
			this._defaultVolume = 1f;
			this.ConnectionsEnd();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009F56 File Offset: 0x00008356
		public virtual void StartVoice()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009F58 File Offset: 0x00008358
		public virtual void StopVoice()
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009F5C File Offset: 0x0000835C
		public virtual void VoiceDataMessage(ulong userID, byte[] message, int size)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (userID == this._connectedUsers[i].userID)
					{
						uint newDataSize = this.PrepNewVoiceData(ref message, size);
						this._connectedUsers[i].SetNewVoice(message, newDataSize);
						i = this._connectedUsers.Count;
					}
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009FF0 File Offset: 0x000083F0
		public virtual uint PrepNewVoiceData(ref byte[] message, int size)
		{
			return (uint)size;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009FF4 File Offset: 0x000083F4
		public void VoiceEndMessage(ulong userID, byte[] message, int size)
		{
			this._Manager.AddToNextUpdate(delegate
			{
				this.ConnectionRemove(userID, false);
			});
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A02C File Offset: 0x0000842C
		public void ConnectionAdd(ulong userID)
		{
			bool flag = false;
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				if (userID != this._Manager.GetUserID)
				{
					int num = 0;
					while (num < this._connectedUsers.Count && !flag)
					{
						if (this._connectedUsers[num].userID == userID)
						{
							flag = true;
						}
						num++;
					}
					if (!flag && Requests.GetVoiceConnectionAttemptResult(new BaseUserInfo(userID, this._Manager.Key, string.Empty)))
					{
						VoiceConnection voiceConnection = this._Manager.gameObject.AddComponent<VoiceConnection>();
						this.SetUpVoice(ref voiceConnection);
						voiceConnection.userID = userID;
						voiceConnection.voiceSource.volume = this._defaultVolume;
						voiceConnection.voiceSource.bypassEffects = true;
						voiceConnection.voiceSource.bypassReverbZones = true;
						voiceConnection.voiceSource.bypassListenerEffects = true;
						voiceConnection.voiceSource.mute = (this.DefaultMute || !this.VoipEnabled);
						this._connectedUsers.Add(voiceConnection);
					}
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A158 File Offset: 0x00008558
		public virtual void SetUpVoice(ref VoiceConnection newVoice)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A15C File Offset: 0x0000855C
		public void ConnectionRemove(ulong userID, bool tellThem = true)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (this._connectedUsers[i].userID == userID)
					{
						VoiceConnection voiceConnection = this._connectedUsers[i];
						if (tellThem)
						{
							this.TellRemoving(i);
						}
						voiceConnection.DestroySource();
						UnityEngine.Object.Destroy(voiceConnection);
						this._connectedUsers.Remove(this._connectedUsers.ElementAt(i));
						i--;
					}
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A208 File Offset: 0x00008608
		public virtual void TellRemoving(int index)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A20C File Offset: 0x0000860C
		public void ConnectionMute(ulong userID, bool setMute)
		{
			bool flag = false;
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				int num = 0;
				while (num < this._connectedUsers.Count && !flag)
				{
					if (this._connectedUsers[num].userID == userID)
					{
						this._connectedUsers[num].SetMuted(setMute || !this.VoipEnabled);
						flag = true;
					}
					num++;
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A2A4 File Offset: 0x000086A4
		public bool ConnectionMuted(ulong userID)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (this._connectedUsers[i].userID == userID)
					{
						return this._connectedUsers[i].Muted;
					}
				}
			}
			return false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A328 File Offset: 0x00008728
		public void SetVoiceMutes(bool setMute)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					this._connectedUsers[i].SetMuted(setMute || !this.VoipEnabled);
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A39C File Offset: 0x0000879C
		public void ConnectionsEnd()
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					this._connectedUsers[i].DestroySource();
					UnityEngine.Object.Destroy(this._connectedUsers[i]);
				}
				this._connectedUsers.Clear();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A41C File Offset: 0x0000881C
		public AudioClip GetAudioClips(ulong userID)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (userID == this._connectedUsers[i].userID)
					{
						return this._connectedUsers[i].voiceClip;
					}
				}
			}
			return null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A4A0 File Offset: 0x000088A0
		public AudioClip GetAudioClips(int index)
		{
			object lockBody = BaseVoiceManager._lockBody;
			AudioClip result;
			lock (lockBody)
			{
				if (0 <= index && index < this._connectedUsers.Count)
				{
					result = this._connectedUsers[index].voiceClip;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A508 File Offset: 0x00008908
		public void SetVoiceVolume(ulong userID, float volume)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (userID == this._connectedUsers[i].userID)
					{
						this.SetVoiceVolume(i, volume);
					}
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A578 File Offset: 0x00008978
		public void SetVoiceVolume(int index, float volume)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				if (0 <= index && index < this._connectedUsers.Count)
				{
					this._connectedUsers[index].voiceSource.volume = volume;
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A5DC File Offset: 0x000089DC
		public void SetVoiceVolumes(float volume)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					this._connectedUsers[i].voiceSource.volume = volume;
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A644 File Offset: 0x00008A44
		public float GetVoiceVolume(ulong userID)
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				for (int i = 0; i < this._connectedUsers.Count; i++)
				{
					if (userID == this._connectedUsers[i].userID)
					{
						return this.GetVoiceVolume(i);
					}
				}
			}
			return 0f;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A6C0 File Offset: 0x00008AC0
		public float GetVoiceVolume(int index)
		{
			object lockBody = BaseVoiceManager._lockBody;
			float result;
			lock (lockBody)
			{
				if (0 <= index && index < this._connectedUsers.Count)
				{
					result = this._connectedUsers[index].voiceSource.volume;
				}
				else
				{
					result = 0f;
				}
			}
			return result;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A730 File Offset: 0x00008B30
		protected bool RunningThread
		{
			get
			{
				object lockThread = BaseVoiceManager._lockThread;
				bool runningThread;
				lock (lockThread)
				{
					runningThread = this._RunningThread;
				}
				return runningThread;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A770 File Offset: 0x00008B70
		public virtual void StartVoiceThread()
		{
			object lockThread = BaseVoiceManager._lockThread;
			lock (lockThread)
			{
				if (this._voiceThread == null)
				{
					this._voiceThread = new Thread(new ThreadStart(this.VoiceThread));
				}
				if (!this._RunThread && !this._RunningThread)
				{
					this._RunThread = true;
					this._voiceThread.Start();
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A7F0 File Offset: 0x00008BF0
		public virtual void StopVoiceThread()
		{
			object lockThread = BaseVoiceManager._lockThread;
			lock (lockThread)
			{
				this.StopVoice();
				this._RunThread = false;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A834 File Offset: 0x00008C34
		public virtual void WaitForVoiceThreadEnd()
		{
			bool flag = true;
			this.StopVoiceThread();
			while (flag)
			{
				object lockThread = BaseVoiceManager._lockThread;
				lock (lockThread)
				{
					this._RunThread = false;
					flag = this._RunningThread;
				}
				if (flag)
				{
					this._voiceThread.Join();
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A89C File Offset: 0x00008C9C
		protected void VoiceThread()
		{
			bool flag = true;
			object lockThread = BaseVoiceManager._lockThread;
			lock (lockThread)
			{
				this._RunningThread = true;
			}
			while (flag)
			{
				this.RecordAndSendVoice();
				object lockThread2 = BaseVoiceManager._lockThread;
				lock (lockThread2)
				{
					flag = this._RunThread;
				}
				Thread.Sleep(16);
			}
			object lockThread3 = BaseVoiceManager._lockThread;
			lock (lockThread3)
			{
				this._RunningThread = false;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A948 File Offset: 0x00008D48
		public virtual void RecordAndSendVoice()
		{
		}

		// Token: 0x040000B6 RID: 182
		protected BasePlatformManager _Manager;

		// Token: 0x040000B7 RID: 183
		protected static object _lockBody = new object();

		// Token: 0x040000B8 RID: 184
		private bool _voiceRunning;

		// Token: 0x040000B9 RID: 185
		protected List<VoiceConnection> _connectedUsers = new List<VoiceConnection>();

		// Token: 0x040000BA RID: 186
		private float _defaultVolume = 1f;

		// Token: 0x040000BB RID: 187
		private bool _defaultMute;

		// Token: 0x040000BC RID: 188
		private bool _voipEnabled = true;

		// Token: 0x040000BD RID: 189
		protected static object _lockThread = new object();

		// Token: 0x040000BE RID: 190
		protected byte[] _messageBuffer;

		// Token: 0x040000BF RID: 191
		protected bool _RunThread;

		// Token: 0x040000C0 RID: 192
		protected bool _RunningThread;

		// Token: 0x040000C1 RID: 193
		protected Thread _voiceThread;
	}
}
