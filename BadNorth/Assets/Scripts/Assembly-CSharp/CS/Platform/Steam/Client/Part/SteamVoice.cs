using System;
using System.Threading;
using CS.Platform.Base.Client.Part;
using CS.Platform.Utils.Data;
using Steamworks;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x0200004C RID: 76
	public class SteamVoice : BaseVoiceManager
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0001054C File Offset: 0x0000E94C
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._endMessage.MessageType = MessageTypes.VOICECHAT_END;
			this._voiceBuffer = new byte[8000];
			this._earBuffer = new byte[8000];
			base.ConnectionsEnd();
			this.StopVoice();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000105A0 File Offset: 0x0000E9A0
		public override void StartVoice()
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				if (!base.VoiceRunning && base.RunningThread && base.VoipEnabled)
				{
					SteamUser.StartVoiceRecording();
					SteamFriends.SetInGameVoiceSpeaking((CSteamID)this._Manager.GetUserID, true);
					base.VoiceRunning = true;
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00010618 File Offset: 0x0000EA18
		public override void StopVoice()
		{
			object lockBody = BaseVoiceManager._lockBody;
			lock (lockBody)
			{
				if (base.VoiceRunning)
				{
					SteamUser.StopVoiceRecording();
					SteamFriends.SetInGameVoiceSpeaking((CSteamID)this._Manager.GetUserID, false);
					base.VoiceRunning = false;
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001067C File Offset: 0x0000EA7C
		public override uint PrepNewVoiceData(ref byte[] message, int size)
		{
			uint result = 0U;
			EVoiceResult evoiceResult = SteamUser.DecompressVoice(message, (uint)size, this.convertedMessage, 22000U, out result, 11025U);
			if (evoiceResult != EVoiceResult.k_EVoiceResultDataCorrupted)
			{
				message = this.convertedMessage;
			}
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000106B6 File Offset: 0x0000EAB6
		public override void TellRemoving(int index)
		{
			((SteamManager)this._Manager).PostBox.SendMessage(this._connectedUsers[index].userID, this._endMessage, true);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000106E5 File Offset: 0x0000EAE5
		public override void SetUpVoice(ref VoiceConnection newVoice)
		{
			newVoice.voiceBufferReadSize = 22000;
			newVoice.audioKey = "SteamVoice";
			newVoice.voiceFrequancy = 11025;
			newVoice.voiceSampleLenght = 44100;
			newVoice.audioRawType = VoiceConnection.AudioRawType.SHORT;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010720 File Offset: 0x0000EB20
		public override void StartVoiceThread()
		{
			object lockThread = BaseVoiceManager._lockThread;
			lock (lockThread)
			{
				if (this._voiceThread == null)
				{
					this._voiceThread = new Thread(new ThreadStart(base.VoiceThread));
				}
				if (this._earThread == null)
				{
					this._earThread = new Thread(new ThreadStart(this.EarThread));
				}
				if (!this._RunThread && !this._RunningThread)
				{
					this._RunThread = true;
					this._voiceThread.Start();
					this._earThread.Start();
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000107CC File Offset: 0x0000EBCC
		public override void WaitForVoiceThreadEnd()
		{
			bool flag = true;
			this.StopVoiceThread();
			while (flag)
			{
				object lockThread = BaseVoiceManager._lockThread;
				lock (lockThread)
				{
					this._RunThread = false;
					flag = (this._RunningThread || this._earThreadRunning);
				}
				if (flag)
				{
					this._voiceThread.Join();
				}
				if (flag)
				{
					this._earThread.Join();
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010854 File Offset: 0x0000EC54
		public override void RecordAndSendVoice()
		{
			uint num = 0U;
			EVoiceResult evoiceResult = EVoiceResult.k_EVoiceResultOK;
			while (evoiceResult != EVoiceResult.k_EVoiceResultNotRecording && evoiceResult != EVoiceResult.k_EVoiceResultNoData)
			{
				evoiceResult = SteamUser.GetVoice(true, this._voiceBuffer, (uint)this._voiceBuffer.Length, out num);
				if (base.VoiceRunning && evoiceResult != EVoiceResult.k_EVoiceResultDataCorrupted && num != 0U)
				{
					object lockBody = BaseVoiceManager._lockBody;
					lock (lockBody)
					{
						for (int i = 0; i < this._connectedUsers.Count; i++)
						{
							if (!this._connectedUsers[i].Muted)
							{
								SteamNetworking.SendP2PPacket((CSteamID)this._connectedUsers[i].userID, this._voiceBuffer, num, EP2PSend.k_EP2PSendUnreliableNoDelay, SteamElements.VoiceChannel);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001092C File Offset: 0x0000ED2C
		protected void EarThread()
		{
			bool flag = true;
			while (flag)
			{
				this.GetEarMessages();
				object lockThread = BaseVoiceManager._lockThread;
				lock (lockThread)
				{
					flag = this._RunThread;
				}
				Thread.Sleep(16);
			}
			object lockThread2 = BaseVoiceManager._lockThread;
			lock (lockThread2)
			{
				this._earThreadRunning = false;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000109B0 File Offset: 0x0000EDB0
		protected void GetEarMessages()
		{
			uint val = 0U;
			uint num = 0U;
			CSteamID that;
			if (SteamNetworking.IsP2PPacketAvailable(out num, SteamElements.VoiceChannel) && SteamNetworking.ReadP2PPacket(this._earBuffer, num, out val, out that, SteamElements.VoiceChannel))
			{
				byte[] earBuffer = this._earBuffer;
				this.VoiceDataMessage((ulong)that, earBuffer, (int)Math.Min(val, num));
			}
		}

		// Token: 0x0400015A RID: 346
		public const uint VoiceBufferMaxSizeCpmp = 8000U;

		// Token: 0x0400015B RID: 347
		public const uint VoiceBufferMaxSizeDecomp = 22000U;

		// Token: 0x0400015C RID: 348
		public const uint VoiceSampleRateDecomp = 11025U;

		// Token: 0x0400015D RID: 349
		private byte[] convertedMessage = new byte[22000];

		// Token: 0x0400015E RID: 350
		private int messageOffset;

		// Token: 0x0400015F RID: 351
		protected byte[] _voiceBuffer;

		// Token: 0x04000160 RID: 352
		protected byte[] _earBuffer;

		// Token: 0x04000161 RID: 353
		private PlatformMessageBase _endMessage = new PlatformMessageBase();

		// Token: 0x04000162 RID: 354
		protected Thread _earThread;

		// Token: 0x04000163 RID: 355
		protected bool _earThreadRunning;
	}
}
