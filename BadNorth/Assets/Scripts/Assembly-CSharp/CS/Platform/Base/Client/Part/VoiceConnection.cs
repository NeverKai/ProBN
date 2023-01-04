using System;
using UnityEngine;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000039 RID: 57
	public class VoiceConnection : MonoBehaviour
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000B6D5 File Offset: 0x00009AD5
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B6DD File Offset: 0x00009ADD
		public int voiceBufferReadSize
		{
			get
			{
				return this._voiceBufferReadSize;
			}
			set
			{
				this._voiceBufferReadSize = value;
				this._voiceDataReadBuffer = new byte[this._voiceBufferReadSize];
				this._voiceDataReadBufferRead = new float[this._voiceDataReadBuffer.Length / 4];
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B70C File Offset: 0x00009B0C
		public void DestroySource()
		{
			UnityEngine.Object.Destroy(this.voiceSource);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000B719 File Offset: 0x00009B19
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000B724 File Offset: 0x00009B24
		public ulong userID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this._userID = value;
				this.voiceClip = AudioClip.Create(this.audioKey + "_" + this.userID.ToString(), this.voiceSampleLenght, 1, this.voiceFrequancy, true, new AudioClip.PCMReaderCallback(this.OnReaderCallback));
				if (this.voiceSource == null)
				{
					this.voiceSource = base.gameObject.AddComponent<AudioSource>();
					this.voiceSource.spatialBlend = 0f;
					this.voiceSource.spread = 180f;
					this.voiceSource.panStereo = 0f;
					this.voiceSource.loop = true;
					this.voiceSource.bypassEffects = true;
					this.voiceSource.bypassReverbZones = true;
					this.voiceSource.bypassListenerEffects = true;
				}
				this.voiceSource.clip = this.voiceClip;
				this.voiceSource.Play();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000B820 File Offset: 0x00009C20
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000B860 File Offset: 0x00009C60
		private int ReadPosition
		{
			get
			{
				object lockRead = this._lockRead;
				int readPosition;
				lock (lockRead)
				{
					readPosition = this._readPosition;
				}
				return readPosition;
			}
			set
			{
				object lockRead = this._lockRead;
				lock (lockRead)
				{
					this._readPosition = value;
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000B8A0 File Offset: 0x00009CA0
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000B8E0 File Offset: 0x00009CE0
		private int WritePosition
		{
			get
			{
				object lockWrite = this._lockWrite;
				int writePosition;
				lock (lockWrite)
				{
					writePosition = this._writePosition;
				}
				return writePosition;
			}
			set
			{
				object lockWrite = this._lockWrite;
				lock (lockWrite)
				{
					this._writePosition = value;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000B920 File Offset: 0x00009D20
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000B960 File Offset: 0x00009D60
		public bool Muted
		{
			get
			{
				object lockMute = this._lockMute;
				bool muted;
				lock (lockMute)
				{
					muted = this._muted;
				}
				return muted;
			}
			set
			{
				object lockMute = this._lockMute;
				lock (lockMute)
				{
					this._muted = value;
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B9A0 File Offset: 0x00009DA0
		public void SetMuted(bool value)
		{
			this.Muted = value;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B9AC File Offset: 0x00009DAC
		public void SetNewVoice(byte[] voiceData, uint newDataSize)
		{
			int num = 0;
			int num2 = this.WritePosition;
			if (!this.Muted)
			{
				while ((long)num < (long)((ulong)newDataSize))
				{
					short num3 = BitConverter.ToInt16(voiceData, num);
					this._voiceDataReadBufferRead[num2] = (float)num3 / 32767f;
					num += 2;
					num2++;
					if (num2 >= this._voiceDataReadBufferRead.Length)
					{
						num2 -= this._voiceDataReadBufferRead.Length;
					}
					this.WritePosition = num2;
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000BA1C File Offset: 0x00009E1C
		private void OnReaderCallback(float[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (this.ReadPosition == this.WritePosition)
				{
					while (i < data.Length)
					{
						data[i] = 0f;
						i++;
					}
					break;
				}
				data[i] = this._voiceDataReadBufferRead[this.ReadPosition];
				this._voiceDataReadBufferRead[this.ReadPosition] = 0f;
				this.ReadPosition++;
				if (this.ReadPosition >= this._voiceDataReadBufferRead.Length)
				{
					this.ReadPosition -= this._voiceDataReadBufferRead.Length;
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000BAC8 File Offset: 0x00009EC8
		public void ConvertShort(ref float data)
		{
			short num = BitConverter.ToInt16(this._voiceDataReadBuffer, this.ReadPosition);
			data = (float)num / 32767f;
			for (int i = 0; i < 2; i++)
			{
				this._voiceDataReadBuffer[this.ReadPosition + i] = 0;
			}
			this.ReadPosition += 2;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000BB24 File Offset: 0x00009F24
		public void ConvertFloat(ref float data)
		{
			data = BitConverter.ToSingle(this._voiceDataReadBuffer, this.ReadPosition);
			for (int i = 0; i < 4; i++)
			{
				this._voiceDataReadBuffer[this.ReadPosition + i] = 0;
			}
			this.ReadPosition += 4;
		}

		// Token: 0x040000D8 RID: 216
		public VoiceConnection.AudioRawType audioRawType;

		// Token: 0x040000D9 RID: 217
		public const uint VoiceBufferWriteStart = 16U;

		// Token: 0x040000DA RID: 218
		public const uint VoiceBufferMaxWriteChunk = 1024U;

		// Token: 0x040000DB RID: 219
		private int _voiceBufferReadSize;

		// Token: 0x040000DC RID: 220
		public int voiceSampleLenght;

		// Token: 0x040000DD RID: 221
		public int voiceFrequancy;

		// Token: 0x040000DE RID: 222
		public string audioKey;

		// Token: 0x040000DF RID: 223
		private ulong _userID;

		// Token: 0x040000E0 RID: 224
		private byte[] _voiceDataReadBuffer;

		// Token: 0x040000E1 RID: 225
		private float[] _voiceDataReadBufferRead;

		// Token: 0x040000E2 RID: 226
		private object _lockRead = new object();

		// Token: 0x040000E3 RID: 227
		private int _readPosition;

		// Token: 0x040000E4 RID: 228
		private object _lockWrite = new object();

		// Token: 0x040000E5 RID: 229
		private int _writePosition = 16;

		// Token: 0x040000E6 RID: 230
		private object _lockMute = new object();

		// Token: 0x040000E7 RID: 231
		private bool _muted;

		// Token: 0x040000E8 RID: 232
		public AudioSource voiceSource;

		// Token: 0x040000E9 RID: 233
		public AudioClip voiceClip;

		// Token: 0x0200003A RID: 58
		public enum AudioRawType
		{
			// Token: 0x040000EB RID: 235
			SHORT,
			// Token: 0x040000EC RID: 236
			FLOAT
		}
	}
}
